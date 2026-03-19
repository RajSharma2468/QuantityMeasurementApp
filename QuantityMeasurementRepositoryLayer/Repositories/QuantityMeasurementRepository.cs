using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Context;

namespace QuantityMeasurementRepositoryLayer.Repositories;

public class QuantityMeasurementRepository : IQuantityMeasurementRepository
{
    private readonly ApplicationDbContext _context;
    
    public QuantityMeasurementRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<QuantityMeasurement> AddAsync(QuantityMeasurement entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _context.QuantityMeasurements.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<List<QuantityMeasurement>> GetAllAsync()
    {
        return await _context.QuantityMeasurements
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<QuantityMeasurement?> GetByIdAsync(long id)
    {
        return await _context.QuantityMeasurements.FindAsync(id);
    }
    
    public async Task<List<QuantityMeasurement>> GetByOperationAsync(string operation)
    {
        return await _context.QuantityMeasurements
            .Where(q => q.Operation.ToLower() == operation.ToLower())
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<List<QuantityMeasurement>> GetByMeasurementTypeAsync(string measurementType)
    {
        return await _context.QuantityMeasurements
            .Where(q => q.ThisMeasurementType.ToLower() == measurementType.ToLower())
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<List<QuantityMeasurement>> GetErrorsAsync()
    {
        return await _context.QuantityMeasurements
            .Where(q => q.IsError)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<long> CountByOperationAsync(string operation)
    {
        return await _context.QuantityMeasurements
            .Where(q => q.Operation.ToLower() == operation.ToLower() && !q.IsError)
            .LongCountAsync();
    }
    
    public async Task<bool> UpdateAsync(QuantityMeasurement entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.QuantityMeasurements.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;
            
        _context.QuantityMeasurements.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}