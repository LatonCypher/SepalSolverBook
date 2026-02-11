Polynomial Differentiation and Integration
==========================================


Polynomial Differentiation and Integration
------------------------------------------

Differentiation and integration of polynomials are fundamental operations in calculus, widely used in various engineering and scientific applications. In SepalSolver, we implement these operations for polynomials represented as arrays of coefficients.

1. Differentiation
~~~~~~~~~~~~~~~~~~
The derivative of a polynomial :math:`P(x) = a_0 x^n + a_1 x^{n-1} + ... + a_n` is obtained by applying the power rule to each term, resulting in :math:`P'(x) = n a_0 x^{n-1} + (n-1) a_1 x^{n-2} + ... + 0`. In SepalSolver, we create a method that computes the derivative and returns a new polynomial.

.. Admonition:: Example 1 : 

   :math:`\cfrac{d}{dx}(3x^2 + 5x + 10) = 6x + 5`
   
   .. code-block:: csharp
   
      // Example of polynomial differentiation
      double[] p = [3, 5, 10]; // Represents 3x^2 + 5x + 10
      double[] derivative = Polyder(p, 1); // Should represent 6x + 5
      Console.WriteLine($"Derivative: [{string.Join(", ", derivative)}]");
   
   
   Ouput
   
   .. terminal::
   
      Derivative: [6, 5]


2. Integration
~~~~~~~~~~~~~~
The indefinite integral of a polynomial is computed by applying the reverse of the power rule, resulting in :math:`∫P(x)dx = (a_0/n+1)x^{n+1} + (a_1/n)x^n + ... + C`, where C is the constant of integration. We implement this in SepalSolver to return a new polynomial representing the integral.


.. Admonition:: Example 2 : 

   :math:`\int(6x^2 + 4x + 2)dx + C = 2x^3 + 2x^2 + 2x + C`
   
   .. code-block:: csharp
   
      // Example of polynomial integration
      double[] p = [6, 4, 2]; // Represents 6x^2 + 4x + 2
      double C = 9; // Constant of integration
      double[] integral = Polyint(p, C); // Should represent 2x^3 + 2x^2 + 2x + C
      Console.WriteLine($"Integral: [{string.Join(", ", integral)}]");
   
   
   Ouput
   
   .. terminal::
   
      Integral: [2, 2, 2, 9]
