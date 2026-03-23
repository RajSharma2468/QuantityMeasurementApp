using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurementModelLayer.DTOs.User;
using QuantityMeasurementModelLayer.Common;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementAPILayer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthRepository _authRepo;

    public UserController(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return Unauthorized(ApiResponse<object>.Error("User not found"));

        var user = await _authRepo.GetUserByUsername(username);
        if (user == null)
            return NotFound(ApiResponse<object>.Error("User not found"));

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };

        return Ok(ApiResponse<UserResponseDto>.Ok(response));
    }
}