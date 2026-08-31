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
      0.7557    0.6893    0.5764    0.2064    0.2009    0.0968    0.8731
   
   C = 
      0.0933
      0.9617
      0.3286
      0.5893
      0.5096
   
   M = 
      0.4656    0.4657    0.4816    0.6297    0.1253    0.7151    0.3242
      0.4364    0.2794    0.1721    0.7858    0.2710    0.9327    0.3612
      0.2316    0.9864    0.5642    0.3974    0.0359    0.4259    0.1078
      0.6049    0.0726    0.1252    0.7314    0.5600    0.2900    0.0778
      0.6964    0.2075    0.2361    0.5590    0.7256    0.7607    0.2897
      0.1655    0.8681    0.4354    0.6576    0.9140    0.2663    0.0005
      0.1254    0.6461    0.7864    0.1141    0.9678    0.4399    0.2518
      0.4896    0.9970    0.6951    0.8584    0.7043    0.3311    0.8329
   

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
      0.2805    0.9300    0.3960    0.2767
   
   R2 = 
      0.6092    0.3994    0.5198    0.8946    0.5002
   
   R3 = 
      0.2805    0.9300    0.3960    0.2767    0.6092    0.3994    0.5198    0.8946    0.5002
   
   C1 = 
      0.9621
      0.8849
      0.0764
      0.5507
      0.5685
      0.5011
      0.3152
      0.5766
      0.8499
      0.4051
   
   C2 = 
      0.1172
      0.1453
      0.0824
      0.3297
      0.9216
      0.7245
      0.0998
      0.5974
      0.5417
      0.7990
   
   M = 
      0.9621    0.1172
      0.8849    0.1453
      0.0764    0.0824
      0.5507    0.3297
      0.5685    0.9216
      0.5011    0.7245
      0.3152    0.0998
      0.5766    0.5974
      0.8499    0.5417
      0.4051    0.7990
   


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
      0.4974    0.2599    0.1373    0.7369
   
   R2 = 
      0.1962    0.2382    0.0306    0.9117
   
   M = 
      0.4974    0.2599    0.1373    0.7369
      0.1962    0.2382    0.0306    0.9117
   
   C1 = 
      0.8007
      0.9776
      0.8024
      0.9187
      0.1232
      0.6475
      0.0566
      0.1088
      0.5277
      0.7115
   
   C2 = 
      0.3473
      0.0997
   
   C3 = 
      0.8007
      0.9776
      0.8024
      0.9187
      0.1232
      0.6475
      0.0566
      0.1088
      0.5277
      0.7115
      0.3473
      0.0997
   

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
   

