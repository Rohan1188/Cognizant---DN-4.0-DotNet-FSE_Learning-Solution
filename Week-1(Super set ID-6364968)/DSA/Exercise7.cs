using System;
using System.Collections.Generic;

class FinancialForecastingTool
{
    private static Dictionary<int, double> memo = new Dictionary<int, double>();

    public static void Main(string[] args)
    {
        Console.WriteLine("Financial Forecasting Tool");
        Console.WriteLine("--------------------------");
        
        // Get user input
        double presentValue = GetDoubleInput("Enter present value: ");
        double growthRate = GetDoubleInput("Enter annual growth rate (decimal): ");
        int periods = GetIntInput("Enter number of periods: ");
        bool useMemoization = GetYesNoInput("Use memoization optimization? (y/n): ");

        // Calculate future value
        double futureValue = useMemoization 
            ? CalculateFutureValueWithMemo(presentValue, growthRate, periods)
            : CalculateFutureValue(presentValue, growthRate, periods);

        // Display results
        Console.WriteLine("\nForecast Results:");
        Console.WriteLine($"Present Value: {presentValue:C}");
        Console.WriteLine($"Growth Rate: {growthRate:P2}");
        Console.WriteLine($"Periods: {periods}");
        Console.WriteLine($"Future Value: {futureValue:C}");
        
        // Generate forecast table
        Console.WriteLine("\nYear-by-Year Forecast:");
        GenerateForecastTable(presentValue, growthRate, periods);
    }

    // Basic recursive calculation
    public static double CalculateFutureValue(double presentValue, double growthRate, int periods)
    {
        if (periods == 0)
            return presentValue;
        
        return CalculateFutureValue(presentValue * (1 + growthRate), growthRate, periods - 1);
    }

    // Optimized recursive calculation with memoization
    public static double CalculateFutureValueWithMemo(double presentValue, double growthRate, int periods)
    {
        memo.Clear();
        return CalculateFutureValueMemoized(presentValue, growthRate, periods);
    }

    private static double CalculateFutureValueMemoized(double presentValue, double growthRate, int periods)
    {
        if (periods == 0)
            return presentValue;

        if (memo.TryGetValue(periods, out double cachedValue))
            return cachedValue;

        double value = CalculateFutureValueMemoized(presentValue * (1 + growthRate), growthRate, periods - 1);
        memo[periods] = value;
        return value;
    }

    // Helper methods for user input
    private static double GetDoubleInput(string prompt)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    private static int GetIntInput(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value >= 0)
                return value;
            Console.WriteLine("Invalid input. Please enter a non-negative integer.");
        }
    }

    private static bool GetYesNoInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToLower();
            if (input == "y") return true;
            if (input == "n") return false;
            Console.WriteLine("Please enter 'y' or 'n'.");
        }
    }

    // Generate a forecast table
    private static void GenerateForecastTable(double presentValue, double growthRate, int periods)
    {
        Console.WriteLine("Year\tValue\t\tGrowth");
        Console.WriteLine("----\t-----\t\t------");
        
        double currentValue = presentValue;
        for (int year = 1; year <= periods; year++)
        {
            double previousValue = currentValue;
            currentValue = CalculateFutureValue(presentValue, growthRate, year);
            double yearGrowth = currentValue - previousValue;
            
            Console.WriteLine($"{year}\t{currentValue:C}\t{yearGrowth:C}");
        }
    }
}
