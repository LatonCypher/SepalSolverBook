Matrix Vectors Operations Null Spaces
=====================================

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
   


Matrix scalar operation
-----------------------
The simplest matrix scalar operation is multiplication operation. In this operation, the scaler just scales every single entry of the matrix. Addition and subtraction just add or subtracts the scalar from every entry of the matrix respectively. 

Division can be termwise, or the scaler times the inversion of the matrix. 


.. code-block:: csharp

   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };
   Console.WriteLine($"2A = \n{2*A}");
   Console.WriteLine($"A + 5 = \n{A + 5}");
   Console.WriteLine($"A - 5 = \n{A - 5}");
   Console.WriteLine($"2/A = \n{2*(A.Inverse())}");


Ouput

.. terminal::

   2A = 
   
    2   4   6 
    8  10  12 
   14  16  18 
   
   A + 5 = 
   
    6   7   8 
    9  10  11 
   12  13  14 
   
   A - 5 = 
   
   -4  -3  -2 
   -1   0   1 
    2   3   4 
   
   2/A = 
   1e16*
     -0.9007    1.8014   -0.9007
      1.8014   -3.6029    1.8014
     -0.9007    1.8014   -0.9007
   

Operation with row and column vectors
-------------------------------------
Termwise operations between matrices and row or column vectors are also supported. The vector is broadcasted along its singleton dimension to match the matrix dimensions.This is done for addition, subtraction and termwise multiplication and division.

But there are some rules to be followed. For a ``ColVec``-``Matrix`` operation, the number of rows must be equal and the ``ColVec`` is broadasted to have the same number of columns as the ``Matrix``. For ``RowVec``-``Matrix`` operations, the number of columns in the RowVec must be equal to the number of columns in the ``Matrix``, and the RowVec is broadcasted to have the same number of rows as the Matrix.

Matrix multiplication between matrices and row or column vectors are also supported. A ``ColVec`` can be multiplied to the left of a ``RowVec`` to produce a matrix. A ``RowVec`` can be multiplied to the right of a ``ColVec`` to produce a matrix. but a ``ColVec`` can only be multiplied to the right of a matrix if the number of rows in the ``ColVec`` is equal to the number of columns in the matrix. Similarly, a ``RowVec`` can only be multiplied to the left of a matrix if the number of columns in the ``RowVec`` is equal to the number of rows in the matrix.

A ``RowVec`` can be multiplied to the left of a ``ColVec`` to produce a scalar (dot product). 


.. code-block:: csharp

   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };
   ColVec U = new double[] { 1, 2, 3 };
   RowVec P = new double[] { 4, 5, 6 };
   // Addition
   Console.WriteLine($"A + U = \n{A + U}");
   Console.WriteLine($"A + P = \n{A + P}");
   // Subtraction
   Console.WriteLine($"A - U = \n{A - U}");
   Console.WriteLine($"A - P = \n{A - P}");
   // Termwise Multiplication
   Console.WriteLine($"A.Times(U) = \n{A.Times(U)}");
   Console.WriteLine($"A.Times(P) = \n{A.Times(P)}");
   // Termwise Division
   Console.WriteLine($"A.Div(U) = \n{A.Div(U)}");
   Console.WriteLine($"A.Div(P) = \n{A.Div(P)}");
   // Matrix Multiplication
   Console.WriteLine($"U * P = \n{U * P}");
   Console.WriteLine($"P * U = \n{P * U}");
   Console.WriteLine($"A * U = \n{A * U}");
   Console.WriteLine($"P * A = \n{P * A}");


Ouput

.. terminal::

   A + U = 
   
    2   3   4 
    6   7   8 
   10  11  12 
   
   A + P = 
   
    5   7   9 
    8  10  12 
   11  13  15 
   
   A - U = 
   
    0   1   2 
    2   3   4 
    4   5   6 
   
   A - P = 
   
   -3  -3  -3 
    0   0   0 
    3   3   3 
   
   A.Times(U) = 
   
    1   2   3 
    8  10  12 
   21  24  27 
   
   A.Times(P) = 
   
    4  10  18 
   16  25  36 
   28  40  54 
   
   A.Div(U) = 
   
      1.0000    2.0000    3.0000
      2.0000    2.5000    3.0000
      2.3333    2.6667    3.0000
   
   A.Div(P) = 
   
      0.2500    0.4000    0.5000
      1.0000    1.0000    1.0000
      1.7500    1.6000    1.5000
   
   U * P = 
   
    4   5   6 
    8  10  12 
   12  15  18 
   
   P * U = 
   32
   A * U = 
   
   14 
   32 
   50 
   
   P * A = 
   
   66  81  96 
   

Transpose, Inverse, Determinant, Rref
-------------------------------------

Transpose
~~~~~~~~~
* **Definition**: The transpose of a matrix is obtained by flipping it over its diagonal, turning rows into columns and columns into rows.
* **Column Vector**: A column vector becomes a row vector when transposed.


.. math::

   \begin{bmatrix} a \\ b \\ c \end{bmatrix}^T =
   \begin{bmatrix} a & b & c \end{bmatrix}



* **Row Vector**: A row vector becomes a column vector when transposed.

.. math::

   \begin{bmatrix} x & y & z \end{bmatrix}^T =
   \begin{bmatrix} x \\ y \\ z \end{bmatrix}


* **Matrix**: Each element :math:`a_{ij}` moves to position :math:`a_{ji}`.


.. code-block:: csharp

   // Matrix transpose
   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };
   Console.WriteLine($"A^T = \n{A.T}");


Ouput

.. terminal::

   A^T = 
   
    1   4   7 
    2   5   8 
    3   6   9 
   

Inverse
~~~~~~~
* **Definition**: The inverse of a square matrix :math:`A` is another matrix :math:`A^{-1}` such that:

.. math::

   A \cdot A^{-1} = I


where :math:`I` is the identity matrix.
* **Column/Row Vectors**: Vectors are not square matrices, so they do not have inverses in the matrix sense.
* **Matrix**: Only square matrices can have inverses. A matrix has an inverse if and only if its determinant is non-zero.


.. code-block:: csharp

   // Matrix inverse
   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };
   Console.WriteLine($"A^-1 = \n{A.Inverse()}");


Ouput

.. terminal::

   A^-1 = 
   1e16*
     -0.4504    0.9007   -0.4504
      0.9007   -1.8014    0.9007
     -0.4504    0.9007   -0.4504
   

Determinant
~~~~~~~~~~~
* **Definition**: A scalar value computed from a square matrix, representing scaling factor and orientation of linear transformation.
* **Column/Row Vectors**: Not square, so determinant is not defined.
* **Matrix**: For a :math:`2 \times 2` matrix:


.. math::

   \det \begin{bmatrix} a & b \\ c & d \end{bmatrix} = ad - bc



.. code-block:: csharp

   // 2x2 Matrix determinant
   Matrix A = new double[,] { { 1, 2 },
                              { 3, 4 } };
   double det = A.Det();
   Console.WriteLine($"det(A) = {det}");


Ouput

.. terminal::

   det(A) = -2


.. code-block:: csharp


   // 3x3 Matrix determinant
   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };
   double det = A.Det();
   Console.WriteLine($"det(A) = {det}");


Ouput

.. terminal::

   det(A) = 6.661338147750939E-16

For larger matrices, determinants are computed using expansion or row-reduction methods.

RREF (Reduced Row Echelon Form)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
* **Definition**: A matrix is in RREF if:
- Each leading entry is 1.
- Each leading 1 is the only non-zero entry in its column.
- Leading 1s move to the right as you go down rows.

* **Column Vector**: Reduces to a form where the first non-zero entry is 1, and all entries below are 0.


.. math::

   \begin{bmatrix} 4 \\ 8 \\ 0 \end{bmatrix}
   \;\;\to\;\;
   \begin{bmatrix} 1 \\ 0 \\ 0 \end{bmatrix}


* **Row Vector**: Reduces to a single row with leading 1 and zeros elsewhere.

.. math::

   \begin{bmatrix} 5 & 10 & 0 \end{bmatrix}
   \;\;\to\;\;
   \begin{bmatrix} 1 & 0 & 0 \end{bmatrix}


* **Matrix**: RREF is used to solve systems of linear equations.

.. math::

   \begin{bmatrix} 1 & 2 & 3 \\ 4 & 5 & 6 \end{bmatrix}
   \;\;\to\;\;
   \begin{bmatrix} 1 & 0 & -1 \\ 0 & 1 & 2 \end{bmatrix}


.. code-block:: csharp

   // Matrix Rref
   Matrix A = new double[,] { { 8,    1,    6,    1,  16 },
                              { 3,    5,    6,    1,  15 },
                              { 4,    7,    2,    1,  14 } };
   (Matrix R, Indexer P, Matrix N) = A.Rref();
   Console.WriteLine("\n A = \n" + A
                   + "\n R = \n" + R
                   + "\n P = \n" + P
                   + "\n N = \n" + N);


Ouput

.. terminal::

   
    A = 
   
    8   1   6   1  16 
    3   5   6   1  15 
    4   7   2   1  14 
   
    R = 
   
      1.0000    0.0000    0.0000    0.0690    1.0690
      0.0000    1.0000    0.0000    0.0862    1.0862
      0.0000    0.0000    1.0000    0.0603    1.0603
   
    P = 
   0,	1,	2
    N = 
   
     -0.0690   -1.0690
     -0.0862   -1.0862
     -0.0603   -1.0603
      1.0000    0.0000
      0.0000    1.0000
   
Rref in sepalsolver gives te rref, the row permutation indexer, and the null space basis matrix.
