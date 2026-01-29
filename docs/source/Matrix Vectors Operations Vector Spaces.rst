Matrix Vectors Operations Vector Spaces
=======================================

Arithemetic Operations on Vectors and Matrices
----------------------------------------------
The SepalSolver comes with overloaded operators to allow easy arithmetic operations on Vectors and matrices. 
Vector and Matrix addition and subtraction when conformable can easily be achieved by using the "+" and "-" signs respectively. 
Addition and subtraction of column and row vectors invokes their broadcast in the direction of each other (I mean along their first singleton).  
for matrices, conformity requires that they have exactly the same dimensions. Vectors can be added to matrices. The dimension of the vector has to match that of the matrix. ie. for a ColVec-Matrix Operation, the number of rows must be equal and the ColVec is broadasted to have the same number of columns as the Matrix. 
RowVec are treated the same way. The number of columns in the RowVec must be equal to the number of columns in the Matrix, and the RowVec is broadcasted to have the same number of rows as the Matrix. 
This rules is also apply when carrying out termwise operations between ColVec and RowVec, ColVec and Matrix, and RowVec and Matrix.
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

Examples
~~~~~~~~

.. code-block:: csharp

   // Declarations
   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };

   Matrix B = new double[,] { { 9, 8, 7 },
                              { 6, 5, 4 },
                              { 3, 2, 1 } };

   ColVec U = new double[] { 1, 2, 3 };
   RowVec P = new double[] { 4, 5, 6 };


   // Matrix-Matrix Addition
   Matrix C = A + B;
   Console.WriteLine($"A + B = \n{C}");

   // Matrix-Matrix Subtraction
   Matrix D = A - B;
   Console.WriteLine($"A - B = \n{D}");

   // Matrix-matrix Multiplication
   Matrix E = A * B; // 
   Console.WriteLine($"E = \n{E}");
   Matrix F = B * A;
   Console.WriteLine($"F = \n{F}");

   // Matrix-Matrix Division
   Matrix G = Mldivide(A, B); // A\B
   Console.WriteLine($"G = \n{G}");
   Matrix H = Mrdivide(A, B); // A/B
   Console.WriteLine($"H = \n{H}");


   Matrix I = A.Div(B);
   Console.WriteLine($"I = \n{I}");

   // Power (A.^B)
   Matrix J = A.Pow(B);
   Console.WriteLine($"J = \n{J}");

   // Power (B.^A)
   Matrix K = B.Pow(A);
   Console.WriteLine($"B.^A = B.Pow(A) = \n{K}");



Ouput

.. terminal::

   A + B = 
   
     10.0000   10.0000   10.0000
     10.0000   10.0000   10.0000
     10.0000   10.0000   10.0000
   
   A - B = 
   
     -8.0000   -6.0000   -4.0000
     -2.0000    0.0000    2.0000
      4.0000    6.0000    8.0000
   
   E = 
   
     30.0000   24.0000   18.0000
     84.0000   69.0000   54.0000
    138.0000  114.0000   90.0000
   
   F = 
   
     90.0000  114.0000  138.0000
     54.0000   69.0000   84.0000
     18.0000   24.0000   30.0000
   
   G = 
   
    -25.0000  -26.0000  -19.0000
     38.0000   41.0000   28.0000
    -14.0000  -16.0000  -10.0000
   
   H = 
   
      0.0000    1.3333   -2.3333
     -0.0000    2.3333   -3.3333
     -1.0000    5.3333   -5.3333
   
   I = 
   
      0.1111    0.2500    0.4286
      0.6667    1.0000    1.5000
      2.3333    4.0000    9.0000
   
   J = 
   1e3*
      0.0010    0.2560    2.1870
      4.0960    3.1250    1.2960
      0.3430    0.0640    0.0090
   
   B.^A = B.Pow(A) = 
   1e3*
      0.0090    0.0640    0.3430
      1.2960    3.1250    4.0960
      2.1870    0.2560    0.0010
   

