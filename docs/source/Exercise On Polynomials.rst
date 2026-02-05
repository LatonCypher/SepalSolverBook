Exercise On Polynomials
#######################


Exercise: Polynomial Operations and Calculus
--------------------------------------------

This set of exercises is designed to solidify your mastery of polynomial manipulation within the SepalSolver ecosystem. You will apply coefficient logic, calculus rules, and numerical fitting to solve engineering-inspired problems.

Instruction: Complete the code blocks by filling in the logic for fitting, arithmetic, and calculus tasks.

Task 1: Polynomial Representation and Evaluation

.. code-block:: csharp

   // Define the polynomial P(x) = 4x^3 - 2x + 5
   // Note: Remember to include the zero coefficient for the x^2 term
   double[] P = [ ]; // Fill the array in descending order

   // Evaluate P(x) at x = 2.0
   double x = 2.0;
   double result = ; // call the Evaluate method

   Console.WriteLine($":math:`P({x}) = {result}`");


Task 2: Arithmetic Alignment (PolyAdd)

.. code-block:: csharp

   // P1 = x^2 + 5, P2 = 2x + 1
   double[] p1 = [1.0, 0.0, 5.0];
   double[] p2 = [2.0, 1.0];

   // Add p1 and p2. The library handles the degree mismatch automatically.
   double[] sum = ; // use PolyAdd
   
   // Expected result: [1.0, 2.0, 6.0] -> x^2 + 2x + 6
   Console.WriteLine($"Sum Coefficients: {string.Join(", ", sum)}");


Task 3: Multiplication via Convolution (Conv)

.. code-block:: csharp

   // Multiply (x - 1) by (x + 1)
   double[] p1 = [1.0, -1.0];
   double[] p2 = [1.0, 1.0];

   // Use convolution to multiply
   double[] product = ; // call Conv

   // Expected: [1.0, 0.0, -1.0] -> x^2 - 1
   Console.WriteLine($"Product: {string.Join(", ", product)}");


Task 4: Polynomial Long Division (Deconv)

.. code-block:: csharp

   // Divide (x^2 - 1) by (x - 1)
   double[] dividend = [1.0, 0.0, -1.0];
   double[] divisor = [1.0, -1.0];

   // Deconv returns a tuple (Quotient, Remainder)
   var (Q, R) = ; // call Deconv

   Console.WriteLine($"Quotient: {string.Join(", ", Q)}"); // Expected: [1.0, 1.0]
   Console.WriteLine($"Remainder: {string.Join(", ", R)}"); // Expected: [0.0]


Task 5: Differentiation (Polyder)

.. code-block:: csharp

   // Define P(x) = 5x^2 + 10x + 2
   double[] P = [5.0, 10.0, 2.0];

   // Find the derivative dP/dx
   double[] dP = ; // call Polyder

   // Expected: [10.0, 10.0] -> 10x + 10
   Console.WriteLine($"Derivative: {string.Join(", ", dP)}");


Task 6: Integration with Constant (Polyint)

.. code-block:: csharp

   // Integrate F(x) = 3x^2
   double[] F = [3.0, 0.0, 0.0];

   // Perform integration with a constant of integration C = 10.0
   double[] integral = ; // call Polyint(F, 10.0)

   // Expected: [1.0, 0.0, 0.0, 10.0] -> x^3 + 10
   Console.WriteLine($"Integral: {string.Join(", ", integral)}");


Task 7: Data Fitting (Polyfit)

.. code-block:: csharp

   double[] xData = [0, 1, 2, 3];
   double[] yData = [1, 3, 9, 19]; // Follows y = 2x^2 + 1 roughly

   // Fit a 2nd degree polynomial to the points
   int degree = 2;
   double[] fit = ; // call Polyfit

   Console.WriteLine($"Fitted Coefficients: {string.Join(", ", fit)}");




Task 8: Finding Roots

.. code-block:: csharp

   // Solve x^2 - 4 = 0
   double[] P = [1.0, 0.0, -4.0];

   // Find the roots (should return ±2)
   var roots = ; // call Roots

   foreach(var r in roots) Console.WriteLine($":math:`x = {r.Real}`");


Task 9: Finding Maximums via Calculus

.. code-block:: csharp

   // P = -x^2 + 6x (A parabola opening downwards)
   double[] P = [-1.0, 6.0, 0.0];

   // The maximum occurs where the derivative is zero.
   double[] dP = Polyder(P);
   var criticalPoints = ; // find roots of the derivative

   // Expected root: x = 3.0
   Console.WriteLine($"Maximum occurs at :math:x = {criticalPoints[0].Real}");


Task 10: Combining Operations (Area Under Curve)

.. code-block:: csharp

   // Calculate area of y = x under the interval [0, 2]
   double[] line = [1.0, 0.0];

   // 1. Integrate
   double[] areaPoly = Polyint(line);

   // 2. Evaluate at bounds: Integral(2) - Integral(0)
   double area = ; // Use Evaluate(areaPoly, 2.0) - ...

   // Expected: 2.0
   Console.WriteLine($"Area: {area}");


