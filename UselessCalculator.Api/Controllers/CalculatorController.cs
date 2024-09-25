using Microsoft.AspNetCore.Mvc;
using UselessCalculator.Api.Dtos;
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

    [HttpPost("add")]
    public ActionResult<double> Add([FromBody] CalculationRequestDto request)
    {
        _logger.LogInformation("Log here for Add");
        var result = _calculationService.Add(request.Num1, request.Num2);
        return Ok(result);
    }

    [HttpPost("substract")]
    public ActionResult<double> Substract([FromBody] CalculationRequestDto request)
    {
        _logger.LogInformation("Log here for Substract");
        var result = _calculationService.Substract(request.Num1, request.Num2);
        return Ok(result);
    }

    [HttpPost("multiply")]
    public ActionResult<double> Multiply([FromBody] CalculationRequestDto request)
    {
        _logger.LogInformation("Log here for Multiply");
        var result = _calculationService.Multiply(request.Num1, request.Num2);
        return Ok(result);
    }

    [HttpPost("divide")]
    public ActionResult<double> Divide([FromBody] CalculationRequestDto request)
    {
        
        try
        {
            _logger.LogInformation("Log here for Divide");
            var result = _calculationService.Divide(request.Num1, request.Num2);
            return Ok(result);
        }
        catch (DivideByZeroException)
        {
            _logger.LogError("Error on Division");
            return BadRequest("Cannot divide by zero");
        }
    }

}
