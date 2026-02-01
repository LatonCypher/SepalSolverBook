Polynomial Evaluation
=====================

Polynomial Representation and Order
-----------------------------------
As in other numerical analysis and engineering software, SepalSolver uses the standard convention of represent polynomials with coefficients in Descending Order. This means the first element of the array corresponds to the highest power of :math:`x`, making it easier to read and align with long-hand mathematical notation.

1. The Descending Order Convention
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
A polynomial :math:`P(x)= a_{n}x^{n} + a_{n-1}x^{n-1} + a_{n-2}x^{n-2} + \cdots + a_{1}x + a_{0}` is stored in an array where coeffs[0] is :math:`a_n`, coeffs[1] is :math:`a_{n-1}`, and so on, down to coeffs[n] which is :math:`a_0`. This ordering simplifies both the evaluation and manipulation of polynomials in code.

.. code-block:: csharp
 
   // P(x) = 5x^2 + 2x + 1 
   // Power: 2 1 0
   double[] poly = [5, 2, 1];
   
   // Degree is determined by (Length - 1)
   int degree = poly.Length - 1; // 2



2. Horner's Method (Descending)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
When coefficients are in descending order, Horner's method becomes particularly elegant. We start with the first coefficient and repeatedly multiply by x and add the next coefficient: 
:math:`P(x) = ( \cdot ((a_{n}x + a_{n-1})x + a_{n-2})x + \cdots + a_{1})x + a_{0}` 


Examples
--------

.. Admonition:: Example 1 :  Real Value Evaluation (Descending)

   We define a cubic polynomial :math:`P(x)= 2x^3 −6x^2 + 2x−1`. Notice how the input sequence exactly matches the mathematical coefficients written from highest to lowest degree.
   
   .. code-block:: csharp
    
      // 2x^3 - 6x^2 + 2x - 1
      double[] poly = [2, -6, 2, -1.0];
      double result = Polyval(poly, 3.0);
      Console.WriteLine($"P(3.0) = {result}");
   
   
   Ouput
   
   .. terminal::
   
      P(3.0) = 5


.. Admonition:: Example 2 :  Complex Evaluation (Descending) 

   Evaluating at a complex point :math:`s = \sigma + j \omega` is common in control theory. Here we evaluate :math:`P(s)=1s^2 + 0s + 1` (which is :math:`s^2 + 1`) at the imaginary unit i.
   
   .. code-block:: csharp
    
      double[] poly = [1.0, 0.0, 1.0];
      Complex s = new(0, 1); // s = i
      Complex result = Polyval(poly, s);
      Console.WriteLine($"P(i) = {result}");
    
   
   Ouput
   
   .. terminal::
   
      P(i) =   0.0000 + 0.0000i 


.. Admonition:: Example 3 :  Column Vector Evaluation (Vectorized) 

In this case, we have a set of measurements in a ColVec and we want to pass them through our polynomial model. SepalSolver iterates /// through the vector, applying the descending-order Horner's method /// to each element. 
   
   .. code-block:: csharp
    
      // P(x) = x^2 + 2x + 3
      double[] poly = [1.0, 2.0, 3.0];
   
      ColVec x = new double[] { 0, 1, 2 };
      ColVec y = x.Select(x=>Polyval(poly, x)).ToArray();
      Console.WriteLine($"Result at x = {x.T} is: {y.T}"); // 4 + 4 + 3 = 11
    
   
   Ouput
   
   .. terminal::
   
      Result at x = 
      0 1 2
       is: 
      3 6 11
      
   

Implementation Tip: Power Mapping
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Because we use descending order, the power associated with a coefficient at index i is calculated as Degree - i. This is important when performing differentiation, as the derivative of the term at coeffs[i] involves multiplying by (Degree - i).
