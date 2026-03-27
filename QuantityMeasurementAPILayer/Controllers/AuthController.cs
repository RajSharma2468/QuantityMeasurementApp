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
        try
        {
            // Check if username already exists
            var existingUser = await _authRepo.GetUserByUsername(request.Username);
            if (existingUser != null)
                return BadRequest(ApiResponse<object>.Error("Username already exists"));

            // Check if email already exists
            existingUser = await _authRepo.GetUserByEmail(request.Email);
            if (existingUser != null)
                return BadRequest(ApiResponse<object>.Error("Email already exists"));

            // Hash password and create user
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = await _authRepo.CreateUser(request, passwordHash);

            return Ok(ApiResponse<object>.Ok(new { user.Id, user.Username }, "User registered successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Error(ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        try
        {
            // Find user by email
            var user = await _authRepo.GetUserByEmail(request.Email);
            
            if (user == null)
                return Unauthorized(ApiResponse<object>.Error("Invalid email or password"));

            // Verify password
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            
            if (!isValidPassword)
                return Unauthorized(ApiResponse<object>.Error("Invalid email or password"));

            // Generate JWT token
            var token = GenerateJwtToken(user);
            var refreshToken = await _authRepo.CreateRefreshToken(user.Id);

            return Ok(ApiResponse<object>.Ok(new
            {
                token = token,
                refreshToken = refreshToken.Token,
                expiresAt = DateTime.Now.AddHours(24),
                userId = user.Id,
                username = user.Username,
                email = user.Email,
                firstName = user.FirstName ?? "",
                lastName = user.LastName ?? ""
            }, "Login successful"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Error(ex.Message));
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            // Get user ID from JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(ApiResponse<object>.Error("Not authenticated"));

            var userId = int.Parse(userIdClaim.Value);
            var user = await _authRepo.GetUserById(userId);
            
            if (user == null)
                return NotFound(ApiResponse<object>.Error("User not found"));

            return Ok(ApiResponse<object>.Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role
            }));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Error(ex.Message));
        }
    }

    private string GenerateJwtToken(User user)
    {
        // Get JWT settings from configuration
        var jwtKey = _config["Jwt:Key"];
        var jwtIssuer = _config["Jwt:Issuer"];
        var jwtAudience = _config["Jwt:Audience"];
        
        // Default values if not configured
        if (string.IsNullOrEmpty(jwtKey))
            jwtKey = "ThisIsMySuperSecretKeyForJWT1234567890";
        if (string.IsNullOrEmpty(jwtIssuer))
            jwtIssuer = "QuantityMeasurementAPI";
        if (string.IsNullOrEmpty(jwtAudience))
            jwtAudience = "QuantityMeasurementClient";
            
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}