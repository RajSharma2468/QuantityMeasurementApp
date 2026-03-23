using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces;

public interface IEncryptionRepository
{
    Task SaveEncryptionHistory(int userId, string input, string output, string type);
    Task<List<EncryptionHistory>> GetEncryptionHistory(int userId);
}