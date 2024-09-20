using Microsoft.AspNetCore.Mvc;
using UselessCalculator.Application;

namespace UselessCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculationService _calculationService;

    public CalculatorController(ICalculationService calculationService)
    {
        _calculationService = calculationService;
    }

    [HttpGet("add")]
    public ActionResult<double> Add([FromQuery] double num1, [FromQuery] double num2)
    {
        var result = _calculationService.Add(num1, num2);
        return Ok(result);
    }

    [HttpGet("substract")]
    public ActionResult<double> Substract([FromQuery] double num1, [FromQuery] double num2)
    {
        var result = _calculationService.Substract(num1, num2);
        return Ok(result);
    }

    [HttpGet("multiply")]
    public ActionResult<double> Multiply([FromQuery] double num1, [FromQuery] double num2)
    {
        var result = _calculationService.Multiply(num1, num2);
        return Ok(result);
    }

    [HttpGet("divide")]
    public ActionResult<double> Divide([FromQuery] double num1, [FromQuery] double num2)
    {
        try
        {
            var result = _calculationService.Multiply(num1, num2);
            return Ok(result);
        }
        catch (DivideByZeroException)
        {
            return BadRequest("Cannot divide by zero");
        }
    }

}
