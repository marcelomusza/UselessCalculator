using UselessCalculator.Application;

namespace UselessCalculator.Tests;

[TestFixture]
public class CalculatorServiceTests
{

	private ICalculationService _calculatorService;

	[SetUp]
	public void Setup()
	{
		_calculatorService = new CalculationService();
	}

	[Test]
	public void Add_WithValidNumbers_ReturnsCorrectSum()
	{
		//Arrange
		var num1 = 5;
		var num2 = 10;

		//Act
		var result = _calculatorService.Add(num1, num2);

		//Assert
		Assert.That(result, Is.EqualTo(15));
	}

    [Test]
    public void Subtract_WithValidNumbers_ReturnsCorrectDifference()
    {
        //Arrange
        var num1 = 10;
        var num2 = 5;

        //Act
        var result = _calculatorService.Substract(num1, num2);

        //Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Multiply_WithValidNumbers_ReturnsCorrectProduct()
    {
        //Arrange
        var num1 = 5;
        var num2 = 10;

        //Act
        var result = _calculatorService.Multiply(num1, num2);

        //Assert
        Assert.That(result, Is.EqualTo(50));
    }

    [Test]
    public void Divide_WithValidNumbers_ReturnsCorrectQuotient()
    {
        //Arrange
        var num1 = 10;
        var num2 = 2;

        //Act
        var result = _calculatorService.Divide(num1, num2);

        //Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var num1 = 10;
        var num2 = 0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => _calculatorService.Divide(num1, num2));
    }
}
