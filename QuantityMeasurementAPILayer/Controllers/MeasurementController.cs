using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurementModelLayer.DTOs.Measurement;
using QuantityMeasurementModelLayer.Common;
using QuantityMeasurementBusinessLayer.Interfaces;  // This is correct

namespace QuantityMeasurementAPILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertRequestDto request)
        {
            try
            {
                if (request == null)
                    return BadRequest(ApiResponse<object>.Error("Request cannot be null"));

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(ApiResponse<object>.Error("User not authenticated"));

                var result = await _measurementService.ConvertAsync(request, userId);
                
                return Ok(ApiResponse<ConvertResponseDto>.Ok(result, "Conversion successful"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.Error($"Conversion failed: {ex.Message}"));
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var history = await _measurementService.GetHistoryAsync(userId);
                
                return Ok(ApiResponse<object>.Ok(history, "History retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.Error($"Failed to retrieve history: {ex.Message}"));
            }
        }

        [HttpGet("units")]
        public IActionResult GetUnits()
        {
            var units = new
            {
                Length = new[] { "Meter", "Kilometer", "Centimeter", "Millimeter", "Mile", "Yard", "Foot", "Inch" },
                Weight = new[] { "Kilogram", "Gram", "Pound", "Ounce" },
                Temperature = new[] { "Celsius", "Fahrenheit", "Kelvin" }
            };
            
            return Ok(ApiResponse<object>.Ok(units, "Units retrieved successfully"));
        }
    }
}