using System;

class Program
{
    static void Main(string[] args)
    {
        var num1 = GetValidNumber("Enter the first number: ");
        var num2 = GetValidNumber("Enter the second number: ");

        var selection = GetValidSelection();
        
        double result = selection switch
        {
            1 => num1 + num2,
            2 => num1 - num2,
            3 => num1 * num2,
            4 => num1 / num2,
            _ => throw new InvalidOperationException("Invalid operation.")
        };

        Console.WriteLine($"The result is: {result}");
    }

    static double GetValidNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();

                if (input is not null && double.TryParse(input, out var number))
                {
                    return number;
                }

                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

    static int GetValidSelection()
    {
        while (true)
        {
            Console.WriteLine("""
            Enter the desired operation:
            1 --> Sum
            2 --> Subtract
            3 --> Multiply
            4 --> Divide
            """);

            var input = Console.ReadLine();

            if (input is not null && int.TryParse(input, out var selection))
            {
                switch (selection)
                {
                    case >= 1 and <= 4:
                        return selection;
                }
            }

            Console.WriteLine("Invalid selection. Please enter a number between 1 and 4.");
        }
    }
}
