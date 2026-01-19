
Polynomials
===========

Polynomials in SepalSolver
--------------------------
While matrices and vectors are the core of linear algebra, many engineering problems—such as curve fitting, signal processing, and finding eigenvalues— revolve around Polynomials. 

In SepalSolver, we represent a polynomial as a specialized class that manages a collection of coefficients and provides methods for evaluation, fitting, convolution, deconvolution, differentiation, and integration of polynomials. 

1. Representation and Degree
~~~~~~~~~~~~~~~~~~~~~~~~~~~~
A polynomial :math:`P(x)=a_0 x^n + a_1 x^{n-1} + a_2 x^{n - 2} + \cdots + a_n` is defined by its coefficients. In SepalSolver, we store these in a double[] array where the index corresponds to the power of x. This makes the "Degree" of the polynomial exactly coefficients.Length - 1. 




.. code-block:: csharp
 { 
     public class Polynomial 
     { 
         private readonly double[] _coeffs;
         public int Degree => _coeffs.Length - 1;
                        ...
                        ...
                        ...
     }
 } 

2. Evaluation via Horner's Method
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Evaluating a polynomial like :math:`3x^3+2x^2+x+5` by calling Math.Pow repeatedly is computationally expensive. Instead, we use Horner's Method, which reduces the operation to a simple loop of multiplications and additions, significantly boosting performance in iterative solvers. 


Examples
--------

.. Admonition:: Example 1 :  Defining and Evaluating a Polynomial 

   Suppose we have a calibration curve for a sensor defined by the quadratic equation :math:`f(x) = 5 + 2x + x^2`. 
   We can define this in one line using the array, and evaluate the sensor output for any given input x. 
   
   
   .. code-block:: csharp
    
      // Represents :math:`x^2 + 2x + 5`
      double[] poly = [1, 2, 5];
   
      // Evaluate at :math:`x = 2.0`
      double x = 2.0;
      double result = Polyval(poly, x); // 5 + 4 + 4 = 13
   
      Console.WriteLine($"P({x}) = {result}");
    
   
   Ouput
   
   .. terminal::
   
      P(2) = 13



.. Admonition:: Example 2 :  Symbolic-like Differentiation 

   In optimization problems, we often need the slope (derivative) of a polynomial. Because the derivative of :math:`ax^n` is simply :math:`n⋅ax^{n-1}` we can implement a method that returns a new Polynomial object representing the derivative. 
   
   .. code-block:: csharp
    
      double[] p = [3, 5, 10]; // 3x^2 + 5x + 10
   
      // Returns a new Polynomial: 6x + 5
      double[] dp = Polyder(p, 1);
      
      Console.WriteLine($"Derivative: [{string.Join(", ", dp)}]");
    
   
   Ouput
   
   .. terminal::
   
      Derivative: [6, 5]


.. Admonition:: Example 3 :  Polynomial Arithmetic 

   Just like numbers, polynomials can be added, subtracted, multiplied (convolution) or devided (deconvolution). 
   By overloading operators, SepalSolver allows you to combine models easily. Adding two polynomials simply involves summing their corresponding coefficients. 
   
   .. code-block:: csharp
    
      double[] p1 = [2, 1]; // 2x + 1
      double[] p2 = [1, 0, 0]; // x^2
      
      // Result: 1 + 2x + x^2
      var p3 = Polyadd(p1, p2);
      
      Console.WriteLine($"p3: {string.Join(", ", p3)}");
    
   
   Ouput
   
   .. terminal::
   
      p3: 1, 2, 1
Root Finding
~~~~~~~~~~~~
One of the most powerful features of the Polynomial class is its ability to interface with our iterative solvers. Finding the roots of a polynomial (where P(x)=0) is a common task that we solve by passing the Evaluate and Differentiate methods into a Newton-Raphson solver. 
But SepalSolver already did this, so, users only need to call the roots method and get the roots of the polynomial. This will be discussed in details under the root section

.. toctree::

   Polynomial Evaluation
   Polynomial Fitting
   Polynomial Arithmetics
   Polynomial Roots
