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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            var measurements = await _context.Measurements
                .Where(m => m.UserId.ToString() == userId)
                .OrderByDescending(m => m.Date)
                .Select(m => new
                {
                    m.Id,
                    m.Type,
                    m.Value,
                    m.Unit,
                    m.Date,
                    m.Notes,
                    m.CreatedAt
                })
                .ToListAsync();

            return Ok(measurements);
        }

        // GET: api/measurements/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.ToString() == userId);

            if (measurement == null)
                return NotFound(new { message = "Measurement not found" });

            return Ok(measurement);
        }

        // POST: api/measurements
        [HttpPost]
        public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            var measurement = new Measurement
            {
                Type = request.Type,
                Value = request.Value,
                Unit = request.Unit,
                Date = request.Date ?? DateTime.UtcNow,
                Notes = request.Notes,
                UserId = int.Parse(userId),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = measurement,
                message = "Measurement created successfully"
            });
        }

        // PUT: api/measurements/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] UpdateMeasurementDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.ToString() == userId);

            if (measurement == null)
                return NotFound(new { message = "Measurement not found" });

            measurement.Type = request.Type;
            measurement.Value = request.Value;
            measurement.Unit = request.Unit;
            if (request.Date.HasValue)
                measurement.Date = request.Date.Value;
            measurement.Notes = request.Notes;
            measurement.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = measurement,
                message = "Measurement updated successfully"
            });
        }

        // DELETE: api/measurements/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.ToString() == userId);

            if (measurement == null)
                return NotFound(new { message = "Measurement not found" });

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Measurement deleted successfully"
            });
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