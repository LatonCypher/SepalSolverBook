Vectors and Matrices
====================

Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: ``RowVec``, ``ColVec`` and ``Matrix``. ``RowVec`` and ``ColVec`` are 1D arrays while ``Matrix`` is a 2D array. 

Creating Vectors and Matrices
-----------------------------


.. code-block:: csharp

   // Row vector
   RowVec R = new double[] { 5, 6, 7, 1 };
   Console.WriteLine($"R = {R}");

   // Column vector
   ColVec C = new double[] { 8, 3, 4, 2, 7 };
   Console.WriteLine($"C = {C}");

   // Matrix
   Matrix M = new double[,] 
   {
       {5, -2, 3, 7 },
       {2, 1, -7, 3 },
       {4, 8, 9, 1 },
       {0, 5, -6, -3 }
   };
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R = 
    5   6   7   1 
   
   C = 
    8 
    3 
    4 
    2 
    7 
   
   M = 
    5  -2   3   7 
    2   1  -7   3 
    4   8   9   1 
    0   5  -6  -3 
   


Vectors and Matrices can also be initialized using random
---------------------------------------------------------

.. code-block:: csharp

   // Row vector
   RowVec R = Rand(7);
   Console.WriteLine($"R = {R}");

   // Column vector
   ColVec C = Rand(5);
   Console.WriteLine($"C = {C}");

   // Matrix
   Matrix M = Rand(8, 7);
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R = 
      0.1036    0.2394    0.3227    0.6191    0.7805    0.5645    0.0123
   
   C = 
      0.8171
      0.0771
      0.4437
      0.3545
      0.4379
   
   M = 
      0.0652    0.4935    0.5791    0.0835    0.7799    0.5161    0.4238
      0.2515    0.0074    0.1821    0.0676    0.7095    0.4658    0.9707
      0.4680    0.7664    0.1236    0.8909    0.0622    0.0411    0.2349
      0.2420    0.2007    0.1019    0.9773    0.7915    0.0854    0.3329
      0.3205    0.2898    0.0365    0.6218    0.5178    0.7144    0.6849
      0.5550    0.6039    0.5019    0.8375    0.7836    0.4393    0.1347
      0.9840    0.5684    0.5013    0.9612    0.0800    0.7188    0.8316
      0.7472    0.6086    0.2096    0.6707    0.6467    0.6080    0.8026
   

Vectors can be initialized using Zeros, Ones, Eye etc
-----------------------------------------------------

.. code-block:: csharp

   // Row vector
   RowVec R = Zeros(7);
   Console.WriteLine($"R = {R}");

   // Column vector
   ColVec C = Ones(5);
   Console.WriteLine($"C = {C}");

   // Matrix
   Matrix M = Eye(7, 7);
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R = 
    0   0   0   0   0   0   0 
   
   C = 
    1 
    1 
    1 
    1 
    1 
   
   M = 
    1   0   0   0   0   0   0 
    0   1   0   0   0   0   0 
    0   0   1   0   0   0   0 
    0   0   0   1   0   0   0 
    0   0   0   0   1   0   0 
    0   0   0   0   0   1   0 
    0   0   0   0   0   0   1 
   

Vectors and Matrices can be concatenated
----------------------------------------

.. code-block:: csharp

   RowVec R1 = Rand(4);
   Console.WriteLine($"R1 = {R1}");
   RowVec R2 = Rand(5);
   Console.WriteLine($"R2 = {R2}");

   // Horizontal concatenation
   RowVec R3 = Hcart(R1, R2);
   Console.WriteLine($"R3 = {R3}");

   ColVec C1 = Rand(10);
   Console.WriteLine($"C1 = {C1}");
   ColVec C2 = Rand(10);
   Console.WriteLine($"C2 = {C2}");

   // Horizontal concatenation
   Matrix M = Hcart(C1, C2);
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R1 = 
      0.0676    0.2757    0.5357    0.2676
   
   R2 = 
      0.7821    0.8422    0.4030    0.2267    0.2656
   
   R3 = 
      0.0676    0.2757    0.5357    0.2676    0.7821    0.8422    0.4030    0.2267    0.2656
   
   C1 = 
      0.8823
      0.9858
      0.0961
      0.3731
      0.1491
      0.9997
      0.2745
      0.6432
      0.1158
      0.9167
   
   C2 = 
      0.3924
      0.9815
      0.6253
      0.4723
      0.6318
      0.8541
      0.7101
      0.5275
      0.0360
      0.8800
   
   M = 
      0.8823    0.3924
      0.9858    0.9815
      0.0961    0.6253
      0.3731    0.4723
      0.1491    0.6318
      0.9997    0.8541
      0.2745    0.7101
      0.6432    0.5275
      0.1158    0.0360
      0.9167    0.8800
   


Vertical Concatenation
----------------------

.. code-block:: csharp

   RowVec R1 = Rand(4);
   Console.WriteLine($"R1 = {R1}");
   RowVec R2 = Rand(4);
   Console.WriteLine($"R2 = {R2}");

   // Vertical concatenation
   Matrix M = Vcart(R1, R2);
   Console.WriteLine($"M = {M}");

   ColVec C1 = Rand(10);
   Console.WriteLine($"C1 = {C1}");
   ColVec C2 = Rand(2);
   Console.WriteLine($"C2 = {C2}");

   // Vertical concatenation
   ColVec C3 = Vcart(C1, C2);
   Console.WriteLine($"C3 = {C3}");


Ouput

.. terminal::

   R1 = 
      0.8531    0.8967    0.3351    0.2274
   
   R2 = 
      0.6486    0.7398    0.2239    0.9582
   
   M = 
      0.8531    0.8967    0.3351    0.2274
      0.6486    0.7398    0.2239    0.9582
   
   C1 = 
      0.3648
      0.4545
      0.7617
      0.8606
      0.7694
      0.6549
      0.3449
      0.3755
      0.3344
      0.2748
   
   C2 = 
      0.9253
      0.8587
   
   C3 = 
      0.3648
      0.4545
      0.7617
      0.8606
      0.7694
      0.6549
      0.3449
      0.3755
      0.3344
      0.2748
      0.9253
      0.8587
   

Flipping a Matrix
-----------------
We can flip a Matrix vertically (flipud) or horizontally (fliplr). 


.. code-block:: csharp


   Matrix M = new double[,]
   {
       {5, -2, 3, 7 },
       {2, 1, -7, 3 },
       {4, 8, 9, 1 },
       {0, 5, -6, -3 }
   };
   Console.WriteLine($"M = {M}");
   Console.WriteLine($"Flipud(M) = {Flipud(M)}");
   Console.WriteLine($"Fliplr(M) = {Fliplr(M)}");


Ouput

.. terminal::

   M = 
    5  -2   3   7 
    2   1  -7   3 
    4   8   9   1 
    0   5  -6  -3 
   
   Flipud(M) = 
    0   5  -6  -3 
    4   8   9   1 
    2   1  -7   3 
    5  -2   3   7 
   
   Fliplr(M) = 
    7   3  -2   5 
    3  -7   1   2 
    1   9   8   4 
   -3  -6   5   0 
   

Extract a Triangular Portion of Matrix
--------------------------------------

.. code-block:: csharp

   Matrix M = new double[,]
   {
       {5, -2, 3, 7 },
       {2, 1, -7, 3 },
       {4, 8, 9, 1 },
       {0, 5, -6, -3 }
   };

   Console.WriteLine($"Triu(M) = {Triu(M)}");
   Console.WriteLine($"Tril(M) = {Tril(M)}");



Ouput

.. terminal::

   Triu(M) = 
    5  -2   3   7 
    0   1  -7   3 
    0   0   9   1 
    0   0   0  -3 
   
   Tril(M) = 
    5   0   0   0 
    2   1   0   0 
    4   8   9   0 
    0   5  -6  -3 
   

