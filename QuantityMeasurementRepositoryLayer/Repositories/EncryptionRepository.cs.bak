using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories;

public class EncryptionRepository : IEncryptionRepository
{
    private readonly ApplicationDbContext _context;

    public EncryptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveEncryptionHistory(int userId, string input, string output, string type)
    {
        var history = new EncryptionHistory
        {
            UserId = userId,
            InputText = input,
            OutputText = output,
            EncryptionType = type,
            CreatedAt = DateTime.UtcNow
        };

        _context.EncryptionHistories.Add(history);
        await _context.SaveChangesAsync();
    }

    public async Task<List<EncryptionHistory>> GetEncryptionHistory(int userId)
    {
        return await _context.EncryptionHistories
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.CreatedAt)
            .Take(10)
            .ToListAsync();
    }
}