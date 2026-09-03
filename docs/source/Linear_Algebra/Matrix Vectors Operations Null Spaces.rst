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


Exploiting Broadcasting (Example)
---------------------------------
We can take advantage of this broadcasting to easily make animation as shown below

.. code-block:: csharp

   // 1 unit circle about origin
   RowVec t = Linspace(0, 2*pi), c = Cos(t), s = Sin(t);

   // mesh of centers
   (Matrix x, Matrix y) = Meshgrid(Linspace(0, 30, 21));
   ColVec X = x.ToArray(), Y = y.ToArray(), T = 0.1*pi*(X-Y);
   double r = 1.35; 

   // Broadcasting circles of radius r
   Matrix xm = X + r * c, ym = Y + r * s;
   Figure(700, 700);
   Plot(xm.T, ym.T, "k"); 
   
   // Add scatter points to circles
   HoldOn();
   double[] scx = [], scy = [];
   var sc = Scatter(scx, scy, "kof");
   HoldOff(); 
   
   // Bound axis [0,30];
   Axis([0, 30, 0, 30]);

   // animate scatter point movement around the circles.
   double dt = 0.02*pi;
   byte[] Animfun(int i)
   {
       T -= dt;
       sc.Xdata = X + r*Cos(T);
       sc.Ydata = Y + r*Sin(T);
       return GetFrame();
   }
   AnimationMaker(Animfun, "Illusion.gif", 30, 100);


.. figure:: images/Illusion.gif
   :align: center
   :alt: Illusion.gif


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



.. code-block:: csharp


   // 3x3 Matrix determinant
   Matrix A = new double[,] { { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 } };
   double det = A.Det();
   Console.WriteLine($"det(A) = {det}");


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

Rref in sepalsolver gives the rref, the row permutation indexer, and the null space basis matrix.
