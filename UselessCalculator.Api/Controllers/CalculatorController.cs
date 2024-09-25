using Microsoft.AspNetCore.Mvc;
using UselessCalculator.Application;

namespace UselessCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculationService _calculationService;
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ICalculationService calculationService,
                                ILogger<CalculatorController> logger)
    {
        _calculationService = calculationService;
        _logger = logger;
    }

    [HttpGet("add")]
    public ActionResult<double> Add([FromQuery] double num1, [FromQuery] double num2)
    {
        _logger.LogInformation("Log here for Add");
        var result = _calculationService.Add(num1, num2);
        return Ok(result);
    }

    [HttpGet("substract")]
    public ActionResult<double> Substract([FromQuery] double num1, [FromQuery] double num2)
    {
        _logger.LogInformation("Log here for Substract");
        var result = _calculationService.Substract(num1, num2);
        return Ok(result);
    }

    [HttpGet("multiply")]
    public ActionResult<double> Multiply([FromQuery] double num1, [FromQuery] double num2)
    {
        _logger.LogInformation("Log here for Multiply");
        var result = _calculationService.Multiply(num1, num2);
        return Ok(result);
    }

    [HttpGet("divide")]
    public ActionResult<double> Divide([FromQuery] double num1, [FromQuery] double num2)
    {
        
        try
        {
            _logger.LogInformation("Log here for Divide");
            var result = _calculationService.Multiply(num1, num2);
            return Ok(result);
        }
        catch (DivideByZeroException)
        {
            _logger.LogError("Error on Division");
            return BadRequest("Cannot divide by zero");
        }
    }

}
