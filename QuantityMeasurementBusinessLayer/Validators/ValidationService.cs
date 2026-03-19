using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public interface IValidationService
{
    Task<ValidationResult> ValidateQuantityInputAsync(QuantityInputDTO input);
    Task<ValidationResult> ValidateCompareOperationAsync(QuantityInputDTO input);
    Task<ValidationResult> ValidateConvertOperationAsync(QuantityInputDTO input);
    Task<ValidationResult> ValidateArithmeticOperationAsync(QuantityInputDTO input);
    Task ThrowIfInvalidAsync(ValidationResult validationResult);
}

public class ValidationService : IValidationService
{
    private readonly ILogger<ValidationService> _logger;
    private readonly QuantityInputDTOValidator _inputValidator;
    private readonly CompareQuantitiesValidator _compareValidator;
    private readonly ConvertQuantitiesValidator _convertValidator;
    private readonly ArithmeticOperationValidator _arithmeticValidator;
    
    public ValidationService(
        ILogger<ValidationService> logger,
        QuantityInputDTOValidator inputValidator,
        CompareQuantitiesValidator compareValidator,
        ConvertQuantitiesValidator convertValidator,
        ArithmeticOperationValidator arithmeticValidator)
    {
        _logger = logger;
        _inputValidator = inputValidator;
        _compareValidator = compareValidator;
        _convertValidator = convertValidator;
        _arithmeticValidator = arithmeticValidator;
    }
    
    public async Task<ValidationResult> ValidateQuantityInputAsync(QuantityInputDTO input)
    {
        try
        {
            var result = await _inputValidator.ValidateAsync(input);
            LogValidationResult(nameof(ValidateQuantityInputAsync), result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating quantity input");
            throw;
        }
    }
    
    public async Task<ValidationResult> ValidateCompareOperationAsync(QuantityInputDTO input)
    {
        try
        {
            var result = await _compareValidator.ValidateAsync(input);
            LogValidationResult(nameof(ValidateCompareOperationAsync), result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating compare operation");
            throw;
        }
    }
    
    public async Task<ValidationResult> ValidateConvertOperationAsync(QuantityInputDTO input)
    {
        try
        {
            var result = await _convertValidator.ValidateAsync(input);
            LogValidationResult(nameof(ValidateConvertOperationAsync), result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating convert operation");
            throw;
        }
    }
    
    public async Task<ValidationResult> ValidateArithmeticOperationAsync(QuantityInputDTO input)
    {
        try
        {
            var result = await _arithmeticValidator.ValidateAsync(input);
            LogValidationResult(nameof(ValidateArithmeticOperationAsync), result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating arithmetic operation");
            throw;
        }
    }
    
    public async Task ThrowIfInvalidAsync(ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            _logger.LogWarning("Validation failed: {Errors}", errors);
            throw new ValidationException(validationResult.Errors);
        }
        
        await Task.CompletedTask;
    }
    
    private void LogValidationResult(string methodName, ValidationResult result)
    {
        if (result.IsValid)
        {
            _logger.LogDebug("{MethodName} validation successful", methodName);
        }
        else
        {
            _logger.LogWarning("{MethodName} validation failed with {ErrorCount} errors", 
                methodName, result.Errors.Count);
        }
    }
}