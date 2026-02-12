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
      0.9882    0.2655    0.0643    0.6863    0.2592    0.5322    0.8461
   
   C = 
      0.2993
      0.4709
      0.6464
      0.9710
      0.3874
   
   M = 
      0.1845    0.0581    0.8536    0.8539    0.2377    0.1386    0.1435
      0.4040    0.5153    0.7376    0.1484    0.0545    0.4331    0.4975
      0.3357    0.3405    0.4396    0.3403    0.5684    0.3166    0.7214
      0.7844    0.6945    0.2235    0.8307    0.5082    0.3437    0.1776
      0.4847    0.5653    0.6562    0.4368    0.7863    0.0132    0.1940
      0.8527    0.7715    0.2897    0.8179    0.6791    0.5625    0.6883
      0.2755    0.3897    0.5095    0.7537    0.7885    0.4761    0.3298
      0.6922    0.2838    0.7409    0.2162    0.2860    0.1626    0.0181
   

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
      0.2968    0.5505    0.7515    0.0726
   
   R2 = 
      0.6402    0.6037    0.2094    0.0206    0.8571
   
   R3 = 
      0.2968    0.5505    0.7515    0.0726    0.6402    0.6037    0.2094    0.0206    0.8571
   
   C1 = 
      0.8773
      0.4164
      0.5926
      0.0912
      0.0820
      0.5224
      0.3141
      0.6936
      0.8592
      0.0521
   
   C2 = 
      0.8727
      0.6418
      0.3752
      0.2224
      0.9956
      0.6987
      0.6725
      0.8136
      0.1653
      0.0166
   
   M = 
      0.8773    0.8727
      0.4164    0.6418
      0.5926    0.3752
      0.0912    0.2224
      0.0820    0.9956
      0.5224    0.6987
      0.3141    0.6725
      0.6936    0.8136
      0.8592    0.1653
      0.0521    0.0166
   


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
      0.6425    0.8682    0.3767    0.0356
   
   R2 = 
      0.6101    0.2757    0.1677    0.0179
   
   M = 
      0.6425    0.8682    0.3767    0.0356
      0.6101    0.2757    0.1677    0.0179
   
   C1 = 
      0.5108
      0.8891
      0.1640
      0.2518
      0.5782
      0.3448
      0.1461
      0.0615
      0.9534
      0.2804
   
   C2 = 
      0.4196
      0.3068
   
   C3 = 
      0.5108
      0.8891
      0.1640
      0.2518
      0.5782
      0.3448
      0.1461
      0.0615
      0.9534
      0.2804
      0.4196
      0.3068
   

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
   

