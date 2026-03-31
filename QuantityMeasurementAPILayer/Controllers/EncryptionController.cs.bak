using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using QuantityMeasurementModelLayer.DTOs.Encryption;
using QuantityMeasurementModelLayer.Common;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementAPILayer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EncryptionController : ControllerBase
{
    private readonly IEncryptionRepository _encryptionRepo;

    public EncryptionController(IEncryptionRepository encryptionRepo)
    {
        _encryptionRepo = encryptionRepo;
    }

    [HttpPost("encrypt")]
    public async Task<IActionResult> Encrypt(EncryptRequestDto request)
    {
        try
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(request.Key.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];

            var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(request.PlainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            var cipherText = Convert.ToBase64String(cipherBytes);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            await _encryptionRepo.SaveEncryptionHistory(userId, request.PlainText, cipherText, "AES");

            return Ok(ApiResponse<EncryptResponseDto>.Ok(
                new EncryptResponseDto { CipherText = cipherText, Algorithm = "AES" }));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Error($"Encryption failed: {ex.Message}"));
        }
    }

    [HttpPost("decrypt")]
    public async Task<IActionResult> Decrypt(DecryptRequestDto request)
    {
        try
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(request.Key.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(request.CipherText);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            var plainText = Encoding.UTF8.GetString(plainBytes);

            return Ok(ApiResponse<object>.Ok(new { decryptedText = plainText }));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Error($"Decryption failed: {ex.Message}"));
        }
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var history = await _encryptionRepo.GetEncryptionHistory(userId);
        return Ok(ApiResponse<object>.Ok(history));
    }
}