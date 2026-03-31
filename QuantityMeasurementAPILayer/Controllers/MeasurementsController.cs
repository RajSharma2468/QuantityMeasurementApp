using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepositoryLayer.Context;

namespace QuantityMeasurementAPILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeasurementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeasurementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/measurements
        [HttpGet]
        public async Task<IActionResult> GetMeasurements()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { success = false, message = "User not authenticated" });

                int userId = int.Parse(userIdClaim);

                var measurements = await _context.Measurements
                    .Where(m => m.UserId == userId)
                    .OrderByDescending(m => m.Date)
                    .Select(m => new
                    {
                        m.Id,
                        m.Type,
                        m.Value,
                        m.Unit,
                        Date = m.Date.ToLocalTime(),  // Convert UTC to local for display
                        m.Notes,
                        m.CreatedAt,
                        m.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(measurements);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetMeasurements: {ex.Message}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/measurements/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { success = false, message = "User not authenticated" });

                int userId = int.Parse(userIdClaim);

                var measurement = await _context.Measurements
                    .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

                if (measurement == null)
                    return NotFound(new { success = false, message = "Measurement not found" });

                // Convert UTC to local for display
                measurement.Date = measurement.Date.ToLocalTime();

                return Ok(measurement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetMeasurement: {ex.Message}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // POST: api/measurements
        [HttpPost]
        public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementDto request)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { success = false, message = "User not authenticated" });

                int userId = int.Parse(userIdClaim);

                // Validate input
                if (string.IsNullOrEmpty(request.Type))
                    return BadRequest(new { success = false, message = "Measurement type is required" });

                if (request.Value <= 0)
                    return BadRequest(new { success = false, message = "Value must be greater than 0" });

                if (string.IsNullOrEmpty(request.Unit))
                    return BadRequest(new { success = false, message = "Unit is required" });

                Console.WriteLine($"Creating measurement for user: {userId}");
                Console.WriteLine($"Data: Type={request.Type}, Value={request.Value}, Unit={request.Unit}");

                // Convert DateTime to UTC for PostgreSQL
                DateTime dateUtc = request.Date?.ToUniversalTime() ?? DateTime.UtcNow;

                var measurement = new Measurement
                {
                    Type = request.Type,
                    Value = request.Value,
                    Unit = request.Unit,
                    Date = dateUtc,
                    Notes = request.Notes ?? "",
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Measurements.Add(measurement);
                await _context.SaveChangesAsync();

                // Convert back to local for response
                measurement.Date = measurement.Date.ToLocalTime();

                return Ok(new
                {
                    success = true,
                    data = measurement,
                    message = "Measurement created successfully"
                });
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
                Console.WriteLine($"Inner error: {dbEx.InnerException?.Message}");
                return StatusCode(500, new { success = false, message = "Database error: " + (dbEx.InnerException?.Message ?? dbEx.Message) });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating measurement: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // PUT: api/measurements/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] UpdateMeasurementDto request)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { success = false, message = "User not authenticated" });

                int userId = int.Parse(userIdClaim);

                var measurement = await _context.Measurements
                    .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

                if (measurement == null)
                    return NotFound(new { success = false, message = "Measurement not found" });

                // Update fields
                measurement.Type = request.Type;
                measurement.Value = request.Value;
                measurement.Unit = request.Unit;
                if (request.Date.HasValue)
                    measurement.Date = request.Date.Value.ToUniversalTime();  // Convert to UTC
                measurement.Notes = request.Notes ?? measurement.Notes;
                measurement.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                // Convert back to local for response
                measurement.Date = measurement.Date.ToLocalTime();

                return Ok(new
                {
                    success = true,
                    data = measurement,
                    message = "Measurement updated successfully"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating measurement: {ex.Message}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/measurements/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { success = false, message = "User not authenticated" });

                int userId = int.Parse(userIdClaim);

                var measurement = await _context.Measurements
                    .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

                if (measurement == null)
                    return NotFound(new { success = false, message = "Measurement not found" });

                _context.Measurements.Remove(measurement);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Measurement deleted successfully"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting measurement: {ex.Message}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }

    // DTOs
    public class CreateMeasurementDto
    {
        public string Type { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateMeasurementDto
    {
        public string Type { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
    }
}