using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        services.AddScoped<IValidationService, ValidationService>();
        
        // Register validators
        services.AddScoped<QuantityDTOValidator>();
        services.AddScoped<QuantityInputDTOValidator>();
        services.AddScoped<CompareQuantitiesValidator>();
        services.AddScoped<ConvertQuantitiesValidator>();
        services.AddScoped<ArithmeticOperationValidator>();  // No parameters needed
        
        return services;
    }
    
    public static async Task ValidateAndThrowAsync<T>(this IValidator<T> validator, T instance)
    {
        var result = await validator.ValidateAsync(instance);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
    
    public static string GetValidationErrors(this ValidationResult result)
    {
        if (result.IsValid)
        {
            return string.Empty;
        }
        
        return string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
    }
    
    public static Dictionary<string, string[]> ToErrorDictionary(this ValidationResult result)
    {
        return result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}