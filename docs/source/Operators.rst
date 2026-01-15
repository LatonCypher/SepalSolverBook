Operators
=========

Operators in C#
---------------
Operators are symbols used to perform operations on variables and values. In
numerical methods, we rely heavily on arithmetic and comparison operators 
to implement mathematical models. 

1. Arithmetic Operators
~~~~~~~~~~~~~~~~~~~~~~~
These operators perform the basic four functions of math, plus the modulus 
(remainder) operator. 


.. code-block:: csharp
 
   double a = 10.5; double b = 2.0;

   double sum = a + b;
   double difference = a - b;
   double product = a * b;
   double quotient = a / b;
   double remainder = 10 % 3;

   Console.WriteLine($"Sum: {sum}");
   Console.WriteLine($"Product: {product}");
   Console.WriteLine($"Remainder: {remainder}");
 

Ouput

.. terminal::

   Sum: 12.5
   Product: 21
   Remainder: 1

2. The Integer Division Pitfall
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
When both operands are integers, C# performs integer division, which 
discards the fractional part. This is a frequent source of bugs in 
engineering formulas. 


.. code-block:: csharp
 
   // Result is 2 because both are ints
   int intResult = 5 / 2;
   // Result is 2.5 because at least one is a double
   double doubleResult = 5.0 / 2;
   Console.WriteLine($"5 / 2 = {intResult}");
   Console.WriteLine($"5.0 / 2 = {doubleResult}");
 

Ouput

.. terminal::

   5 / 2 = 2
   5.0 / 2 = 2.5


3. Relational and Logical Operators
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Relational operators compare two values and return a bool. Logical 
operators allow you to combine multiple conditions. 


.. code-block:: csharp
 
   double error = 0.001; double limit = 0.01;
   bool isConverged = error < limit;
   bool isUnstable = error > 100.0 || double.IsNaN(error);
   Console.WriteLine($"Converged: {isConverged}");
   Console.WriteLine($"Unstable: {isUnstable}");
 

Ouput

.. terminal::

   Converged: True
   Unstable: False

4. Increment and Assignment Operators
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Shorthand operators are used to update a variable's value based on its 
current value—perfect for iterative solvers. 


.. code-block:: csharp
 
   int k = 0; k++; // Increment k by 1
   double x = 10.0;
   x += 5.0; // x = x + 5.0
   x *= 2.0; // x = x * 2.0

   Console.WriteLine($"Counter k: {k}");
   Console.WriteLine($"Value x: {x}");
 

Ouput

.. terminal::

   Counter k: 1
   Value x: 30



Examples
--------


.. Admonition:: Example 1 :  Operator Precedence 

   
   .. code-block:: csharp
    
      // C# follows standard mathematical order (BODMAS/PEMDAS)
      double result = 10 + 5 * 2; // 20
      double forced = (10 + 5) * 2; // 30
   
      Console.WriteLine($"Default: {result}");
      Console.WriteLine($"With Parentheses: {forced}");
   
   
   Ouput
   
   .. terminal::
   
      Default: 20
      With Parentheses: 30


.. Admonition:: Example 2 :  Numerical Precision with Large Numbers 

   
   .. code-block:: csharp
   
      // Watch for overflow with large integers
      int large = int.MaxValue; 
      int overflow = large + 1;
   
      Console.WriteLine($"Max Int: {large}");
      Console.WriteLine($"Overflow Result: {overflow}");
   
   
   Ouput
   
   .. terminal::
   
      Max Int: 2147483647
      Overflow Result: -2147483648
