using Xunit;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementModelLayer.DTOs.Auth;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementApp.Tests.Repositories
{
    public class AuthRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsUser()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new AuthRepository(context);

            var request = new RegisterRequestDto
            {
                Username = "testuser",
                Email = "test@test.com",
                Password = "password"
            };

            // Act
            var user = await repository.CreateUser(request, BCrypt.Net.BCrypt.HashPassword("password"));

            // Assert
            Assert.NotNull(user);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@test.com", user.Email);
        }

        [Fact]
        public async Task GetUserByUsername_ExistingUser_ReturnsUser()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new AuthRepository(context);

            var user = new User
            {
                Username = "testuser",
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                Role = "User"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetUserByUsername("testuser");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
        }

        [Fact]
        public async Task GetUserByUsername_NonExistingUser_ReturnsNull()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new AuthRepository(context);

            // Act
            var result = await repository.GetUserByUsername("nonexistent");

            // Assert
            Assert.Null(result);
        }
    }
}