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
      0.1395    0.1895    0.8761    0.0750    0.5171    0.6853    0.9994
   
   C = 
      0.4663
      0.3735
      0.9799
      0.3023
      0.1566
   
   M = 
      0.1632    0.6935    0.3318    0.8027    0.0573    0.3176    0.0098
      0.6721    0.4372    0.1892    0.6362    0.8417    0.6011    0.5815
      0.1517    0.1029    0.1638    0.6107    0.4473    0.0655    0.9592
      0.6453    0.4182    0.2100    0.0052    0.9304    0.2730    0.4016
      0.6693    0.7418    0.6035    0.2864    0.5746    0.6378    0.8705
      0.7312    0.1937    0.3999    0.2715    0.1339    0.5012    0.3341
      0.9549    0.8013    0.3410    0.6441    0.5701    0.6802    0.4503
      0.1202    0.5441    0.1877    0.8259    0.7478    0.0852    0.4043
   

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
      0.6795    0.9232    0.6953    0.7823
   
   R2 = 
      0.8831    0.8175    0.2407    0.5650    0.4701
   
   R3 = 
      0.6795    0.9232    0.6953    0.7823    0.8831    0.8175    0.2407    0.5650    0.4701
   
   C1 = 
      0.5190
      0.5029
      0.4138
      0.4710
      0.3349
      0.7909
      0.2756
      0.8132
      0.7003
      0.8535
   
   C2 = 
      0.1658
      0.4724
      0.7141
      0.9153
      0.3323
      0.1910
      0.7070
      0.2915
      0.2505
      0.7843
   
   M = 
      0.5190    0.1658
      0.5029    0.4724
      0.4138    0.7141
      0.4710    0.9153
      0.3349    0.3323
      0.7909    0.1910
      0.2756    0.7070
      0.8132    0.2915
      0.7003    0.2505
      0.8535    0.7843
   


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
      0.6702    0.4813    0.3442    0.0522
   
   R2 = 
      0.9447    0.7059    0.1818    0.8265
   
   M = 
      0.6702    0.4813    0.3442    0.0522
      0.9447    0.7059    0.1818    0.8265
   
   C1 = 
      0.4935
      0.7155
      0.0321
      0.8173
      0.4211
      0.8441
      0.4074
      0.8346
      0.4501
      0.4731
   
   C2 = 
      0.1741
      0.3569
   
   C3 = 
      0.4935
      0.7155
      0.0321
      0.8173
      0.4211
      0.8441
      0.4074
      0.8346
      0.4501
      0.4731
      0.1741
      0.3569
   

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
   

