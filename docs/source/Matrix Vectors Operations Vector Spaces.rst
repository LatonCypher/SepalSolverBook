Matrix Vectors Operations Vector Spaces
=======================================

Arithemetic Operations on Vectors and Matrices
----------------------------------------------
The SepalSolver comes with overloaded operators to allow easy arithmetic operations on Vectors and matrices. 
Vector and Matrix addition and subtraction when conformable can easily be achieved by using the "+" and "-" signs respectively. 
Addition and subtraction of column and row vectors invokes their broadcast in the direction of each other (I mean along their first singleton).  
for matrices, conformity requires that they have exactly the same dimensions. Vectors can be added to matrices. The dimension of the vector has to match that of the matrix. ie. for a ``ColVec``-``Matrix`` Operation, the number of rows must be equal and the ``ColVec`` is broadasted to have the same number of columns as the ``Matrix``. 
``RowVec`` are treated the same way. The number of columns in the RowVec must be equal to the number of columns in the ``Matrix``, and the RowVec is broadcasted to have the same number of rows as the Matrix. 
This rules is also apply when carrying out termwise operations between ``ColVec-RowVec``, ``ColVec-Matrix``, and ``RowVec-Matrix``.
Aside the rules, the standard conformity rules applies. 
This table provides the list of operators in Matlab and the corresponding operators in SepalSolver


.. list-table:: 
   :header-rows: 1

   * - **Operation**
     - **MATLAB Syntax**
     - **SepalSolver Syntax**
   * - Addition
     - ``A+B``
     - ``A + B``
   * - Subtraction
     - ``A-B``
     - ``A - B``
   * - Matrix Multiplication
     - ``A*B``
     - ``A * B``
   * - Matrix Left Division
     - ``A\B``
     - ``Mldivide(A, B)``
   * - Matrix Right Division
     - ``A/B``
     - ``Mrdivide(A, B)``
   * - Element-wise Mult.
     - ``A.*B``
     - ``A.Times(B)``
   * - Element-wise Div.
     - ``A./B``
     - ``A.Div(B)``
   * - Element-wise Power
     - ``A.^B``
     - ``A.Pow(B)``
   * - Element-wise Mod Div
     - ``mod(A,B)``
     - ``A % B``

Examples
~~~~~~~~

.. code-block:: csharp

   // Declarations
   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 0, 6 },
                              { 7, 8, 9 } };

   Matrix B = new double[,] { { 9, 8, 7 },
                              { 6, 0, 4 },
                              { 3, 2, 1 } };

   ColVec U = new double[] { 1, 2, 3 };
   RowVec P = new double[] { 4, 5, 6 };

   // Matrix-Matrix Addition
   Console.WriteLine($"A + B = \n{A + B}");

   // Matrix-Matrix Subtraction
   Console.WriteLine($"A - B = \n{A - B}");

   // Matrix-matrix Multiplication
   Console.WriteLine($"A * B = \n{A * B}");
   Console.WriteLine($"B * A = \n{B * A}");
   Console.WriteLine($"B.Times(A) = \n{B.Times(A)}");

   // Matrix-Matrix Division
   Console.WriteLine($"Mldivide(A, B) = \n{Mldivide(A, B)}");
   Console.WriteLine($"Mrdivide(A, B) = \n{Mrdivide(A, B)}");
   Console.WriteLine($"A.Div(B) = \n{A.Div(B)}");
   Console.WriteLine($"Mod(A, B) = \n{Mod(A, B)}");

   // Power (A.^B)
   Console.WriteLine($"A.Pow(B) = \n{A.Pow(B)}");
   Console.WriteLine($"B.Pow(A) = \n{B.Pow(A)}");



Ouput

.. terminal::

   A + B = 
   
   10  10  10 
   10   0  10 
   10  10  10 
   
   A - B = 
   
   -8  -6  -4 
   -2   0   2 
    4   6   8 
   
   A * B = 
   
   30  14  18 
   54  44  34 
   138 74  90 
   
   B * A = 
   
   90  74  138
   34  44  54 
   18  14  30 
   
   B.Times(A) = 
   
    9  16  21 
   24   0  24 
   21  16   9 
   
   Mldivide(A, B) = 
   
     -6.0000   -6.0000   -5.0000
      0.0000    1.0000    0.0000
      5.0000    4.0000    4.0000
   
   Mrdivide(A, B) = 
   
      0.6667   -0.0000   -1.6667
      0.6667    1.0000   -2.6667
      1.6667   -0.0000   -2.6667
   
   A.Div(B) = 
   
      0.1111    0.2500    0.4286
      0.6667       NaN    1.5000
      2.3333    4.0000    9.0000
   
   Mod(A, B) = 
   
      1.0000    2.0000    3.0000
      4.0000       NaN    2.0000
      1.0000    0.0000    0.0000
   
   A.Pow(B) = 
   1e3*
      0.0010    0.2560    2.1870
      4.0960    0.0010    1.2960
      0.3430    0.0640    0.0090
   
   B.Pow(A) = 
   1e3*
      0.0090    0.0640    0.3430
      1.2960    0.0010    4.0960
      2.1870    0.2560    0.0010
   

