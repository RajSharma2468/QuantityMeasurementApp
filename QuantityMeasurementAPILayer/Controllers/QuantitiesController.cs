using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Interfaces;

namespace QuantityMeasurementAPILayer.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class QuantitiesController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly ILogger<QuantitiesController> _logger;
    
    public QuantitiesController(
        IQuantityMeasurementService service,
        ILogger<QuantitiesController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    /// <summary>
    /// Compare two quantities
    /// </summary>
    /// <param name="input">The quantities to compare</param>
    /// <returns>Comparison result</returns>
    /// <response code="200">Returns the comparison result</response>
    /// <response code="400">If the input is invalid</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost("compare")]
    [ProducesResponseType(typeof(QuantityResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuantityResultDTO>> Compare([FromBody] QuantityInputDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _service.CompareQuantitiesAsync(input);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation in compare");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in compare");
            return StatusCode(500, new { error = "An internal error occurred" });
        }
    }
    
    /// <summary>
    /// Convert a quantity to another unit
    /// </summary>
    /// <param name="input">The quantity to convert</param>
    /// <returns>Converted value</returns>
    [HttpPost("convert")]
    [ProducesResponseType(typeof(QuantityResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuantityResultDTO>> Convert([FromBody] QuantityInputDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _service.ConvertQuantitiesAsync(input);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation in convert");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in convert");
            return StatusCode(500, new { error = "An internal error occurred" });
        }
    }
    
    /// <summary>
    /// Add two quantities
    /// </summary>
    /// <param name="input">The quantities to add</param>
    /// <returns>Sum of quantities</returns>
    [HttpPost("add")]
    [ProducesResponseType(typeof(QuantityResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuantityResultDTO>> Add([FromBody] QuantityInputDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _service.AddQuantitiesAsync(input);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation in add");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in add");
            return StatusCode(500, new { error = "An internal error occurred" });
        }
    }
    
    /// <summary>
    /// Subtract two quantities
    /// </summary>
    /// <param name="input">The quantities to subtract</param>
    /// <returns>Difference of quantities</returns>
    [HttpPost("subtract")]
    [ProducesResponseType(typeof(QuantityResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuantityResultDTO>> Subtract([FromBody] QuantityInputDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _service.SubtractQuantitiesAsync(input);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation in subtract");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in subtract");
            return StatusCode(500, new { error = "An internal error occurred" });
        }
    }
    
    /// <summary>
    /// Multiply two quantities
    /// </summary>
    /// <param name="input">The quantities to multiply</param>
    /// <returns>Product of quantities</returns>
    [HttpPost("multiply")]
    [ProducesResponseType(typeof(QuantityResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuantityResultDTO>> Multiply([FromBody] QuantityInputDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _service.MultiplyQuantitiesAsync(input);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation in multiply");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in multiply");
            return StatusCode(500, new { error = "An internal error occurred" });
        }
    }
    
    /// <summary>
    /// Divide two quantities
    /// </summary>
    /// <param name="input">The quantities to divide</param>
    /// <returns>Quotient of quantities</returns>
    [HttpPost("divide")]
    [ProducesResponseType(typeof(QuantityResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuantityResultDTO>> Divide([FromBody] QuantityInputDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _service.DivideQuantitiesAsync(input);
            return Ok(result);
        }
        catch (DivideByZeroException ex)
        {
            _logger.LogWarning(ex, "Division by zero");
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation in divide");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in divide");
            return StatusCode(500, new { error = "An internal error occurred" });
        }
    }
    
    /// <summary>
    /// Get operation history
    /// </summary>
    /// <param name="operation">Operation type (compare, convert, add, etc.)</param>
    /// <returns>List of operations</returns>
    [HttpGet("history/operation/{operation}")]
    [ProducesResponseType(typeof(List<QuantityResultDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<QuantityResultDTO>>> GetOperationHistory(string operation)
    {
        if (string.IsNullOrWhiteSpace(operation))
        {
            return BadRequest(new { error = "Operation type is required" });
        }
        
        var result = await _service.GetOperationHistoryAsync(operation);
        return Ok(result);
    }
    
    /// <summary>
    /// Get measurements by type
    /// </summary>
    /// <param name="measurementType">Measurement type (LengthUnit, WeightUnit, etc.)</param>
    /// <returns>List of measurements</returns>
    [HttpGet("history/type/{measurementType}")]
    [ProducesResponseType(typeof(List<QuantityResultDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<QuantityResultDTO>>> GetMeasurementsByType(string measurementType)
    {
        if (string.IsNullOrWhiteSpace(measurementType))
        {
            return BadRequest(new { error = "Measurement type is required" });
        }
        
        var result = await _service.GetMeasurementsByTypeAsync(measurementType);
        return Ok(result);
    }
    
    /// <summary>
    /// Get operation count
    /// </summary>
    /// <param name="operation">Operation type</param>
    /// <returns>Count of successful operations</returns>
    [HttpGet("count/{operation}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<long>> GetOperationCount(string operation)
    {
        if (string.IsNullOrWhiteSpace(operation))
        {
            return BadRequest(new { error = "Operation type is required" });
        }
        
        var count = await _service.GetOperationCountAsync(operation);
        return Ok(count);
    }
    
    /// <summary>
    /// Get error history
    /// </summary>
    /// <returns>List of failed operations</returns>
    [HttpGet("history/errors")]
    [ProducesResponseType(typeof(List<QuantityResultDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<QuantityResultDTO>>> GetErrorHistory()
    {
        var result = await _service.GetErrorHistoryAsync();
        return Ok(result);
    }
}