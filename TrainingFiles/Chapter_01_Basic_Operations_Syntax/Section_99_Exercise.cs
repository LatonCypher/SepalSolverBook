
namespace ConsoleApp1.TrainingFiles.Chapter_01_Basic_Operations_Syntax
{
    internal class Section_99_Exercise
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// <header 2> Exercise: Basic Operations and Syntax </header 2>
            /// 
            /// This exercise is designed to reinforce your understanding of basic operations and syntax in C#. You will implement a simple console application that performs arithmetic operations, handles user input, and displays results.
            /// 
            /// 
            /// <code>
            {
                // Task 1: Arithmetic Operations 
                // Create a method that takes two integers as input and returns their sum, difference, product, and quotient.
                int Add(int a, int b) => a + b;
                int Subtract(int a, int b) => a - b;
                int Multiply(int a, int b) => a * b;
                double Divide(int a, int b) => b != 0 ? (double)a / b : throw new DivideByZeroException("Cannot divide by zero.");


                // Example usage
                // write the test cases

                //Task 2: User Input Handling
                //Implement a method that prompts the user to enter two integers and then calls the arithmetic methods to display the results.


            }
            /// </code>
            /// 
            /// </BookContent>
        }
    }
}
