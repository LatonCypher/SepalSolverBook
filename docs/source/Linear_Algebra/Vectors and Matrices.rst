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
      0.4293    0.3510    0.2079    0.2522    0.3361    0.9809    0.3667
   
   C = 
      0.9060
      0.4455
      0.6493
      0.2947
      0.5297
   
   M = 
      0.7912    0.3790    0.5967    0.8918    0.8310    0.3145    0.0392
      0.6318    0.0921    0.3352    0.9742    0.1952    0.3435    0.1602
      0.6385    0.3146    0.7811    0.2591    0.6985    0.9596    0.2888
      0.9833    0.6062    0.7096    0.6897    0.1705    0.1975    0.3454
      0.9310    0.9832    0.5021    0.1794    0.2757    0.6121    0.9937
      0.5570    0.9388    0.5399    0.8657    0.3109    0.4780    0.4965
      0.9832    0.7371    0.4206    0.8507    0.0923    0.7650    0.7791
      0.6589    0.7429    0.3405    0.3072    0.4538    0.1473    0.9120
   

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
      0.2446    0.2553    0.2686    0.0769
   
   R2 = 
      0.1395    0.5174    0.3629    0.2799    0.0615
   
   R3 = 
      0.2446    0.2553    0.2686    0.0769    0.1395    0.5174    0.3629    0.2799    0.0615
   
   C1 = 
      0.6293
      0.7602
      0.1535
      0.6486
      0.6460
      0.2574
      0.8639
      0.3094
      0.0167
      0.0645
   
   C2 = 
      0.7293
      0.0762
      0.4632
      0.4215
      0.2789
      0.9995
      0.1041
      0.4839
      0.8380
      0.7855
   
   M = 
      0.6293    0.7293
      0.7602    0.0762
      0.1535    0.4632
      0.6486    0.4215
      0.6460    0.2789
      0.2574    0.9995
      0.8639    0.1041
      0.3094    0.4839
      0.0167    0.8380
      0.0645    0.7855
   


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
      0.6219    0.7513    0.5655    0.7016
   
   R2 = 
      0.5634    0.8015    0.1604    0.1335
   
   M = 
      0.6219    0.7513    0.5655    0.7016
      0.5634    0.8015    0.1604    0.1335
   
   C1 = 
      0.2321
      0.6693
      0.5188
      0.1665
      0.9134
      0.5514
      0.0703
      0.0379
      0.9754
      0.7395
   
   C2 = 
      0.5751
      0.9509
   
   C3 = 
      0.2321
      0.6693
      0.5188
      0.1665
      0.9134
      0.5514
      0.0703
      0.0379
      0.9754
      0.7395
      0.5751
      0.9509
   

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
   

