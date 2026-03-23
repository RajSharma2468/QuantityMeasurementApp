using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QuantityMeasurementModelLayer.DTOs.Auth;
using QuantityMeasurementModelLayer.Common;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementAPILayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;
    private readonly IConfiguration _config;

    public AuthController(IAuthRepository authRepo, IConfiguration config)
    {
        _authRepo = authRepo;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var existingUser = await _authRepo.GetUserByUsername(request.Username);
        if (existingUser != null)
            return BadRequest(ApiResponse<object>.Error("Username already exists"));

        existingUser = await _authRepo.GetUserByEmail(request.Email);
        if (existingUser != null)
            return BadRequest(ApiResponse<object>.Error("Email already exists"));

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = await _authRepo.CreateUser(request, passwordHash);

        return Ok(ApiResponse<object>.Ok(new { user.Id, user.Username }, "User registered successfully"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await _authRepo.GetUserByUsername(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(ApiResponse<object>.Error("Invalid credentials"));

        var token = GenerateJwtToken(user);
        var refreshToken = await _authRepo.CreateRefreshToken(user.Id);

        return Ok(ApiResponse<object>.Ok(new
        {
            token,
            refreshToken = refreshToken.Token,
            expiresAt = DateTime.Now.AddHours(1)
        }));
    }

    private string GenerateJwtToken(User user)
    {
        var jwtKey = _config["Jwt:Key"];
        var jwtIssuer = _config["Jwt:Issuer"];
        var jwtAudience = _config["Jwt:Audience"];
        
        // Check for null values
        if (string.IsNullOrEmpty(jwtKey))
            throw new InvalidOperationException("JWT Key is not configured");
        if (string.IsNullOrEmpty(jwtIssuer))
            throw new InvalidOperationException("JWT Issuer is not configured");
        if (string.IsNullOrEmpty(jwtAudience))
            throw new InvalidOperationException("JWT Audience is not configured");
            
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}