using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly ApplicationDbContext _context;

        public MeasurementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveMeasurementAsync(Measurement measurement)
        {
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Measurement>> GetMeasurementHistoryAsync(int userId)
        {
            return await _context.Measurements
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<Measurement>> GetAllMeasurementsAsync()
        {
            return await _context.Measurements
                .Include(m => m.User)
                .OrderByDescending(m => m.CreatedAt)
                .Take(50)
                .ToListAsync();
        }

        public async Task<Measurement?> GetMeasurementByIdAsync(int id)
        {
            return await _context.Measurements
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteMeasurementAsync(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement != null)
            {
                _context.Measurements.Remove(measurement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMeasurementAsync(Measurement measurement)
        {
            _context.Measurements.Update(measurement);
            await _context.SaveChangesAsync();
        }
    }
}