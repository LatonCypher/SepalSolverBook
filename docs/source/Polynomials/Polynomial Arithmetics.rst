Polynomial Arithmetics
======================

Polynomial Arithmetic
---------------------
In SepalSolver, polynomials are not just static formulas; they are dynamic objects that can be added, subtracted, multiplied, and divided. By representing polynomials as an array of doubles in descending order, we can leverage classical signal processing algorithms, such as convolution, to perform these operations with high numerical efficiency.

1. Addition and Subtraction (PolyAdd / PolySub)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Adding or subtracting polynomials involves summing the coefficients of corresponding powers. Since our arrays are in descending order, we must carefully align the "tails" of the arrays (the constant terms) before performing the operation, especially if the polynomials have different degrees.



.. code-block:: csharp
 
   double[] p1 = [1.0, 2.0, 3.0]; // x^2 + 2x + 3
   double[] p2 = [4.0, 5.0];      // 4x + 5

   // Result of PolyAdd: { 1.0, 6.0, 8.0 } -> x^2 + 6x + 8
   double[] sum = Polyadd(p1, p2); 
   Console.WriteLine($"Sum:\n = [{string.Join(", ", sum)}]");

   // Result of PolySub: { 1.0, -2.0, -2.0 } -> x^2 - 2x - 2
   double[] diff = Polysub(p1, p2);
   Console.WriteLine($"Diff:\n = [{string.Join(", ", diff)}]");


Ouput

.. terminal::

   Sum:
    = [1, 6, 8]
   Diff:
    = [1, -2, -2]

2. Multiplication (Conv)
~~~~~~~~~~~~~~~~~~~~~~~~
Multiplying two polynomials is mathematically equivalent to the convolution of their coefficient arrays. If you multiply a polynomial of degree :math:`N` by one of degree :math:`M`, the resulting degree will be :math:`N + M`. We use a sliding-window approach to compute each term of the resulting array.



.. code-block:: csharp

   double[] p1 = [1.0, 1.0]; // x + 1
   double[] p2 = [1.0, -1.0]; // x - 1
   
   //Conv(p1, p2) results in { 1.0, 0.0, -1.0 } -> x^2 - 1
   double[] product = Conv(p1, p2);
   Console.WriteLine($"Product:\n = [{string.Join(", ", product)}]");


Ouput

.. terminal::

   Product:
    = [1, 0, -1]

3. Division and Remainder (Deconv)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Polynomial division is implemented via deconvolution (synthetic division). Dividing :math:`P_1` by :math:`P_2` yields two results: the Quotient :math:`(Q)` and the Remainder :math:`(R)`. This is vital for operations like partial fraction expansion or checking for factorability.


.. code-block:: csharp

   double[] dividend = [1.0, 0.0, -1.0]; // x^2 - 1
   double[] divisor = [1.0, 1.0];        // x + 1
   
   // Deconv returns Quotient [1.0, -1.0] (x - 1) and Remainder { 0.0 }
   (double[] Q, double[] R) = Deconv(dividend, divisor);
            
   Console.WriteLine($"Quotient:\n = [{string.Join(", ", Q)}]");
   Console.WriteLine($"Remainder:\n = [{string.Join(", ", R)}]");


Ouput

.. terminal::

   Quotient:
    = [1, -1]
   Remainder:
    = [0, 0, 0]

Examples
--------


.. admonition:: Example 1 :  Combining Structural Models

   Suppose you are combining two transfer functions in a control system. You need to add the characteristic polynomials of two sub-components. Using PolyAdd, the solver automatically handles the degree mismatch between a quadratic and a linear system.
   
   .. code-block:: csharp
   
      double[] modelA = { 1.0, -5.0, 6.0 }; // s^2 - 5s + 6
      double[] modelB = { 1.0, 2.0 };       // s + 2
      double[] combined = Polyadd(modelA, modelB);
      
      // Result: s^2 - 4s + 8
      Console.WriteLine($"New Degree: {combined.Length - 1}");
      Console.WriteLine($"Combined Coefficients: [{string.Join(", ", combined)}]");
   
   
   Ouput
   
   .. terminal::
   
      New Degree: 2
      Combined Coefficients: [1, -4, 8]

.. admonition:: Example 2 :  Expanding a Product of Factors

In root-finding verification, you might want to multiply factors :math:`(x - r_1)(x - r_2)` to see if you recover the original polynomial. Conv makes this/// expansion trivial and numerically stable.
   
   
   .. code-block:: csharp
    
      double[] factor1 = [1.0, -2.0]; // (x - 2)
      double[] factor2 = { 1.0, -3.0 }; // (x - 3)
      double[] expanded = Conv(factor1, factor2);
      
      // Result: { 1.0, -5.0, 6.0 } -> x^2 - 5x + 6
      Console.WriteLine($"Expanded = [{string.Join(", ", expanded)}]");
   
   
   Ouput
   
   .. terminal::
   
      Expanded = [1, -5, 6]


.. admonition:: Example 3 :  Finding Remainders in Signal Filtering

   When dividing polynomials to check for divisibility or performing long division in a filter design, the remainder tells us if one polynomial is a perfect factor of another.
   
   .. code-block:: csharp
   
      double[] P = [1.0, 2.0, 1.0]; // x^2 + 2x + 1
      double[] D = { 1.0, 0.0 };      // x
      var (Q, R) = Deconv(P, D);
      // Q is {1.0, 2.0} (x + 2), R is {1.0} (remainder 1)
      Console.WriteLine($"Remainder is zero? {R.All(val => val == 0)}");
   
   
   Ouput
   
   .. terminal::
   
      Remainder is zero? False
   

Numerical Note: Array Sizing
~~~~~~~~~~~~~~~~~~~~~~~~~~~~
During PolyAdd and PolySub, SepalSolver creates a result array equal to the size of the largest input. During Conv, the result size is always p1.Length + p2.Length - 1. Understanding these allocation rules helps in managing memory when performing recursive polynomial arithmetic.

