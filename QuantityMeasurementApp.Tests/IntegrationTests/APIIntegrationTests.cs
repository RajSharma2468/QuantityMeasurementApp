using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementAPILayer;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementRepositoryLayer.Context;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace QuantityMeasurementApp.Tests.IntegrationTests;

public class APIIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public APIIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CompareQuantities_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO 
            { 
                Value = 1, 
                Unit = "Feet", 
                MeasurementType = "LengthUnit" 
            },
            ThatQuantity = new QuantityDTO 
            { 
                Value = 12, 
                Unit = "Inches", 
                MeasurementType = "LengthUnit" 
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/Quantities/compare", input);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<QuantityResultDTO>();
        Assert.NotNull(result);
        Assert.Equal("true", result.ResultString);
        Assert.False(result.IsError);
    }

    [Fact]
    public async Task CompareQuantities_InvalidInput_ReturnsBadRequest()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO 
            { 
                Value = 1, 
                Unit = "Feet", 
                MeasurementType = "LengthUnit" 
            },
            ThatQuantity = new QuantityDTO 
            { 
                Value = 1, 
                Unit = "Kilogram", 
                MeasurementType = "WeightUnit" 
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/Quantities/compare", input);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ConvertQuantities_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO 
            { 
                Value = 1, 
                Unit = "Feet", 
                MeasurementType = "LengthUnit" 
            },
            ThatQuantity = new QuantityDTO 
            { 
                Value = 0, 
                Unit = "Inches", 
                MeasurementType = "LengthUnit" 
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/Quantities/convert", input);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<QuantityResultDTO>();
        Assert.NotNull(result);
        Assert.Equal(12, result.ResultValue);
    }

    [Fact]
    public async Task GetOperationHistory_ReturnsSuccess()
    {
        // Arrange
        var operation = "compare";
        var input = GetSampleInput();
        await _client.PostAsJsonAsync("/api/v1/Quantities/compare", input);

        // Act
        var response = await _client.GetAsync($"/api/v1/Quantities/history/operation/{operation}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var history = await response.Content.ReadFromJsonAsync<List<QuantityResultDTO>>();
        Assert.NotNull(history);
        Assert.NotEmpty(history);
    }

    [Fact]
    public async Task HealthCheck_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Healthy", content);
    }

    private QuantityInputDTO GetSampleInput()
    {
        return new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO 
            { 
                Value = 1, 
                Unit = "Feet", 
                MeasurementType = "LengthUnit" 
            },
            ThatQuantity = new QuantityDTO 
            { 
                Value = 12, 
                Unit = "Inches", 
                MeasurementType = "LengthUnit" 
            }
        };
    }
}