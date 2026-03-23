using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.DTOs.Auth;

namespace QuantityMeasurementRepositoryLayer.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByUsername(string username);
    Task<User?> GetUserByEmail(string email);
    Task<User> CreateUser(RegisterRequestDto request, string passwordHash);
    Task<RefreshToken> CreateRefreshToken(int userId);
    Task<RefreshToken?> GetRefreshToken(string token);
    Task RevokeRefreshToken(RefreshToken token);
}