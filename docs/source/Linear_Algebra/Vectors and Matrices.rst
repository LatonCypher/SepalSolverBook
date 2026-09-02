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
      0.7852    0.2329    0.5768    0.0458    0.7989    0.3165    0.4740
   
   C = 
      0.8423
      0.4753
      0.3436
      0.7274
      0.0740
   
   M = 
      0.6422    0.9841    0.1266    0.9182    0.1758    0.0387    0.7598
      0.6730    0.6484    0.8307    0.2734    0.9462    0.1673    0.3168
      0.0726    0.0913    0.3776    0.3624    0.4539    0.6539    0.1626
      0.2066    0.5399    0.2513    0.8492    0.7884    0.7887    0.6928
      0.6850    0.1167    0.0600    0.0698    0.5172    0.2156    0.7873
      0.3357    0.4454    0.3775    0.5938    0.5530    0.5047    0.4318
      0.2533    0.1448    0.1656    0.1688    0.5225    0.1183    0.9444
      0.1363    0.6327    0.4702    0.3637    0.7309    0.4894    0.3843
   

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
      0.1030    0.8821    0.4459    0.9012
   
   R2 = 
      0.5121    0.1837    0.1885    0.1905    0.1021
   
   R3 = 
      0.1030    0.8821    0.4459    0.9012    0.5121    0.1837    0.1885    0.1905    0.1021
   
   C1 = 
      0.1535
      0.3443
      0.7521
      0.1331
      0.6164
      0.7982
      0.2649
      0.4118
      0.1181
      0.0112
   
   C2 = 
      0.9570
      0.2213
      0.5890
      0.9033
      0.2968
      0.2641
      0.3341
      0.1987
      0.5129
      0.4827
   
   M = 
      0.1535    0.9570
      0.3443    0.2213
      0.7521    0.5890
      0.1331    0.9033
      0.6164    0.2968
      0.7982    0.2641
      0.2649    0.3341
      0.4118    0.1987
      0.1181    0.5129
      0.0112    0.4827
   


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
      0.5665    0.4221    0.7720    0.1580
   
   R2 = 
      0.3456    0.0766    0.0234    0.7802
   
   M = 
      0.5665    0.4221    0.7720    0.1580
      0.3456    0.0766    0.0234    0.7802
   
   C1 = 
      0.5711
      0.7424
      0.9957
      0.2616
      0.5543
      0.3411
      0.6359
      0.0004
      0.7305
      0.0981
   
   C2 = 
      0.4279
      0.8549
   
   C3 = 
      0.5711
      0.7424
      0.9957
      0.2616
      0.5543
      0.3411
      0.6359
      0.0004
      0.7305
      0.0981
      0.4279
      0.8549
   

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
   

