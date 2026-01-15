using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1.TrainingFiles.Chapter_0_Basic_Operations_Syntax
{
    public class Section_3_Operators_And_Expressions
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> Operators in C# </header 2>
            /// Operators are symbols used to perform operations on variables and values. In
            /// numerical methods, we rely heavily on arithmetic and comparison operators 
            /// to implement mathematical models. 
            /// 
            /// <header 3> 1. Arithmetic Operators </header 3> 
            /// These operators perform the basic four functions of math, plus the modulus 
            /// (remainder) operator. 
            /// 
            /// <code> Basic Arithmetic 
            { 
                double a = 10.5; double b = 2.0;

                double sum = a + b;
                double difference = a - b;
                double product = a * b;
                double quotient = a / b;
                double remainder = 10 % 3;

                Console.WriteLine($"Sum: {sum}");
                Console.WriteLine($"Product: {product}");
                Console.WriteLine($"Remainder: {remainder}");
            } 
            /// </code> 
            /// 
            /// <header 3> 2. The Integer Division Pitfall </header 3> 
            /// When both operands are integers, C# performs integer division, which 
            /// discards the fractional part. This is a frequent source of bugs in 
            /// engineering formulas. 
            /// 
            /// <code> Integer vs Double Division 
            { 
                // Result is 2 because both are ints
                int intResult = 5 / 2;
                // Result is 2.5 because at least one is a double
                double doubleResult = 5.0 / 2;
                Console.WriteLine($"5 / 2 = {intResult}");
                Console.WriteLine($"5.0 / 2 = {doubleResult}");
            } 
            /// </code> 
            /// 
            /// 
            /// <header 3> 3. Relational and Logical Operators </header 3> 
            /// Relational operators compare two values and return a bool. Logical 
            /// operators allow you to combine multiple conditions. 
            /// 
            /// <code> Comparison Logic 
            { 
                double error = 0.001; double limit = 0.01;
                bool isConverged = error < limit;
                bool isUnstable = error > 100.0 || double.IsNaN(error);
                Console.WriteLine($"Converged: {isConverged}");
                Console.WriteLine($"Unstable: {isUnstable}");
            } 
            /// </code> 
            /// 
            /// <header 3> 4. Increment and Assignment Operators </header 3> 
            /// Shorthand operators are used to update a variable's value based on its 
            /// current value—perfect for iterative solvers. 
            /// 
            /// <code> Assignment Shorthand
            { 
                int k = 0; k++; // Increment k by 1
                double x = 10.0;
                x += 5.0; // x = x + 5.0
                x *= 2.0; // x = x * 2.0

                Console.WriteLine($"Counter k: {k}");
                Console.WriteLine($"Value x: {x}");
            } 
            /// </code> 
            /// 
            /// 
            /// 
            /// <header 2> Examples </header 2> 
            /// 
            /// <example 1> Operator Precedence 
            /// <code> 
            { 
                // C# follows standard mathematical order (BODMAS/PEMDAS)
                double result = 10 + 5 * 2; // 20
                double forced = (10 + 5) * 2; // 30

                Console.WriteLine($"Default: {result}");
                Console.WriteLine($"With Parentheses: {forced}");
            }
            /// </code> 
            /// </example 1>
            /// 
            /// <example 2> Numerical Precision with Large Numbers 
            /// <code> 
            {
                // Watch for overflow with large integers
                int large = int.MaxValue; 
                int overflow = large + 1;

                Console.WriteLine($"Max Int: {large}");
                Console.WriteLine($"Overflow Result: {overflow}");
            }
            /// </code> 
            /// </example 2>
            /// </BookContent>
        }
    }
}
