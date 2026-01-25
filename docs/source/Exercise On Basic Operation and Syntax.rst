Exercise On Basic Operation and Syntax
======================================


Exercise: Basic Operations and Syntax
-------------------------------------

This exercise is designed to reinforce your understanding of basic operations and syntax in C#. You will implement a simple console application that performs arithmetic operations, handles user input, and displays results.

**Instruction**: Complete the following tasks in the provided code structure.

Task 1: Arithmetic Operations 

.. code-block:: csharp

   // Create a method that takes two integers as input and returns their sum, difference, product, and quotient.
   int Add(int a, int b) => ;// implement addition of a and b before the semicolon;
   int Subtract(int a, int b) => ; // implement subtraction of b from a the semicolon;
   int Multiply(int a, int b) => ; // implement the multiplication of a and b before the semicolon;
   double Divide(int a, int b) => b != 0 ? (double)a / b : throw new DivideByZeroException("Cannot divide by zero.");
   
   
   // Example usage
   // write the test cases
   int number1 = 10;
   int number2 = 5;

   // Display results
   Console.WriteLine($"Addition: {Add(number1, number2)}");
   Console.WriteLine($"Subtraction: {}"); // call Subtract method and display result
   Console.WriteLine($"Multiplication: {}"); // call Multiply method and display result
   Console.WriteLine($"Division: {}"); // call Divide method and display result


Task 2: User Input Handling

.. code-block:: csharp

   //Implement a method that prompts the user to enter two integers and then calls the arithmetic methods to display the results.
   // Hint: use Int.Parse and Console.Readline to get user input


Task 3: Complete Newton Raphson algorithm for computing the squareroot of a number

.. code-block:: csharp

   double NewtonSqrt(double f)
   {
       double s = f/2;    // initial guess of the squareroot
       double m = ;       // find m given m * s = f;
       double tol = 1e-6; // Tolerance

       // measure convergence. check if m and s are as close as measured by a tolerance
       bool isconverged = Abs(m - s) < tol; // 
       while (!isconverged)
       {
           // replace s with arithmetic mean of s and m
       
       
           // update m
   
   
           // check convergence
       
       return s;
   }

   // Example usage of NewtonSqrt
   double number = 20.0;
   Console.WriteLine($"Square root of {number} is approximately {NewtonSqrt(number):10:4}");


Task 4: Modify the Newton Raphson algorithm in Task 3 to not exceed 10 iterations. 
Hint: Either change to ``for`` loop, or declare a iteration counter `int i = 0` and break when it equals the maximum iteration




