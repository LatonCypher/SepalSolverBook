using System;
using System.Collections.Generic;
using System.Linq;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_6_Integration
{
    internal class Section_1_Trapezoidal_Method
    {
        public static void Run()
        {
            // Example 6.1
            // Define the function to be integrated
            Func<double, double> f = x => Sin(x)*Sin(2*x);
            // Define the limits of integration
            double a = 0, b = pi;
            // Perform the integration using the trapezoidal rule
            double result = Trapz(f, a, b);
            // Print the result
            Console.WriteLine($"The integral of sin(x) from {a} to {b} is approximately {result}");
        }
    }
}
