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
      0.2987    0.1579    0.8684    0.3864    0.3737    0.9936    0.3524
   
   C = 
      0.8252
      0.3711
      0.8292
      0.7332
      0.5025
   
   M = 
      0.0976    0.9240    0.3039    0.7787    0.1172    0.8735    0.0721
      0.9350    0.5270    0.2782    0.4796    0.2525    0.4564    0.1870
      0.4808    0.2040    0.8246    0.7354    0.9570    0.1052    0.4995
      0.0972    0.8136    0.7356    0.9839    0.2392    0.7998    0.6454
      0.2965    0.7619    0.6313    0.4264    0.7436    0.6096    0.6127
      0.0624    0.6376    0.7411    0.8937    0.9262    0.6638    0.7616
      0.1953    0.3903    0.6008    0.6421    0.9174    0.9547    0.9830
      0.8792    0.6090    0.4276    0.1528    0.8898    0.3698    0.7744
   

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
      0.4161    0.0042    0.7668    0.3960
   
   R2 = 
      0.8702    0.0141    0.5255    0.6609    0.9513
   
   R3 = 
      0.4161    0.0042    0.7668    0.3960    0.8702    0.0141    0.5255    0.6609    0.9513
   
   C1 = 
      0.2325
      0.9555
      0.9871
      0.1234
      0.8798
      0.3614
      0.4293
      0.0927
      0.3229
      0.0106
   
   C2 = 
      0.2892
      0.3078
      0.2015
      0.6988
      0.4970
      0.1359
      0.1561
      0.2051
      0.6006
      0.8812
   
   M = 
      0.2325    0.2892
      0.9555    0.3078
      0.9871    0.2015
      0.1234    0.6988
      0.8798    0.4970
      0.3614    0.1359
      0.4293    0.1561
      0.0927    0.2051
      0.3229    0.6006
      0.0106    0.8812
   


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
      0.8139    0.0880    0.7639    0.5464
   
   R2 = 
      0.2872    0.8063    0.0338    0.6089
   
   M = 
      0.8139    0.0880    0.7639    0.5464
      0.2872    0.8063    0.0338    0.6089
   
   C1 = 
      0.0304
      0.1740
      0.0070
      0.7009
      0.7737
      0.1168
      0.4776
      0.7210
      0.1161
      0.9211
   
   C2 = 
      0.5098
      0.0858
   
   C3 = 
      0.0304
      0.1740
      0.0070
      0.7009
      0.7737
      0.1168
      0.4776
      0.7210
      0.1161
      0.9211
      0.5098
      0.0858
   

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
   

