using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using QuantityMeasurementAPILayer.Controllers;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.DTOs.Encryption;

namespace QuantityMeasurementApp.Tests.Services
{
    public class EncryptionServiceTests
    {
        [Fact]
        public async Task Encrypt_ValidInput_ReturnsEncryptedText()
        {
            // Arrange
            var mockRepo = new Mock<IEncryptionRepository>();
            var controller = new EncryptionController(mockRepo.Object);
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var request = new EncryptRequestDto
            {
                PlainText = "Hello World",
                Key = "12345678901234567890123456789012"
            };

            // Act
            var result = await controller.Encrypt(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}