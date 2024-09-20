namespace UselessCalculator.Application
{
    public class CalculationService : ICalculationService
    {
        public double Add(double num1, double num2) => num1 + num2;

        public double Substract(double num1, double num2) => num1 - num2;

        public double Multiply(double num1, double num2) => num1 * num2;

        public double Divide(double num1, double num2)
        {
            if (num2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return num1 / num2;
        }
    }
}
