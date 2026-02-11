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
      0.6181    0.9863    0.9101    0.6391    0.3196    0.9879    0.3887
   
   C = 
      0.0082
      0.9930
      0.4967
      0.0241
      0.4243
   
   M = 
      0.0168    0.4920    0.8848    0.4345    0.9593    0.5135    0.8838
      0.7686    0.1020    0.5863    0.3966    0.1780    0.1815    0.0987
      0.9669    0.2820    0.4323    0.7988    0.1457    0.3080    0.9179
      0.7550    0.2801    0.5666    0.3798    0.0321    0.1771    0.2965
      0.4798    0.3110    0.3861    0.9187    0.3820    0.7072    0.7098
      0.5657    0.0546    0.1755    0.5122    0.5259    0.5984    0.6498
      0.2770    0.6624    0.8962    0.4428    0.1603    0.8053    0.4998
      0.4659    0.8967    0.9631    0.9803    0.7413    0.1044    0.6577
   

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
      0.7285    0.3326    0.1423    0.4754
   
   R2 = 
      0.6700    0.6133    0.7288    0.6707    0.1430
   
   R3 = 
      0.7285    0.3326    0.1423    0.4754    0.6700    0.6133    0.7288    0.6707    0.1430
   
   C1 = 
      0.3422
      0.2756
      0.4204
      0.7787
      0.1591
      0.5229
      0.2199
      0.4617
      0.2763
      0.7766
   
   C2 = 
      0.0069
      0.3746
      0.2027
      0.9673
      0.3114
      0.4484
      0.3727
      0.3098
      0.9931
      0.0770
   
   M = 
      0.3422    0.0069
      0.2756    0.3746
      0.4204    0.2027
      0.7787    0.9673
      0.1591    0.3114
      0.5229    0.4484
      0.2199    0.3727
      0.4617    0.3098
      0.2763    0.9931
      0.7766    0.0770
   


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
      0.1062    0.6656    0.1025    0.1853
   
   R2 = 
      0.2234    0.0647    0.8921    0.0254
   
   M = 
      0.1062    0.6656    0.1025    0.1853
      0.2234    0.0647    0.8921    0.0254
   
   C1 = 
      0.0934
      0.8427
      0.1769
      0.2751
      0.9904
      0.7688
      0.8658
      0.7949
      0.6581
      0.5209
   
   C2 = 
      0.5300
      0.5348
   
   C3 = 
      0.0934
      0.8427
      0.1769
      0.2751
      0.9904
      0.7688
      0.8658
      0.7949
      0.6581
      0.5209
      0.5300
      0.5348
   

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
   

