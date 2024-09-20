namespace UselessCalculator.Application
{
    public interface ICalculationService
    {
        double Add(double num1, double num2);
        double Substract(double num1, double num2);
        double Multiply(double num1, double num2);
        double Divide(double num1, double num2);
    }
}
