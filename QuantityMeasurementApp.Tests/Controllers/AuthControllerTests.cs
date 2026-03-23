using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAPILayer.Controllers;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.DTOs.Auth;
using QuantityMeasurementModelLayer.Common;

namespace QuantityMeasurementApp.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_NewUser_ReturnsOk()
        {
            // Arrange
            var mockRepo = new Mock<IAuthRepository>();
            mockRepo.Setup(r => r.GetUserByUsername(It.IsAny<string>())).ReturnsAsync((User?)null);
            mockRepo.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);
            mockRepo.Setup(r => r.CreateUser(It.IsAny<RegisterRequestDto>(), It.IsAny<string>()))
                .ReturnsAsync(new User { Id = 1, Username = "test", Email = "test@test.com" });

            var config = new Mock<IConfiguration>();
            var controller = new AuthController(mockRepo.Object, config.Object);

            var request = new RegisterRequestDto
            {
                Username = "test",
                Email = "test@test.com",
                Password = "password"
            };

            // Act
            var result = await controller.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var mockRepo = new Mock<IAuthRepository>();
            mockRepo.Setup(r => r.GetUserByUsername(It.IsAny<string>())).ReturnsAsync((User?)null);

            var config = new Mock<IConfiguration>();
            var controller = new AuthController(mockRepo.Object, config.Object);

            var request = new LoginRequestDto
            {
                Username = "test",
                Password = "wrong"
            };

            // Act
            var result = await controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}