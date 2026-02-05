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
      0.5594    0.0078    0.7466    0.8133    0.9375    0.6525    0.0635
   
   C = 
      0.2774
      0.2547
      0.5243
      0.9082
      0.4654
   
   M = 
      0.8881    0.2577    0.7208    0.6948    0.1530    0.9453    0.2944
      0.1979    0.0498    0.2368    0.0539    0.9082    0.5482    0.7151
      0.3469    0.3203    0.9756    0.5452    0.8321    0.9618    0.5643
      0.1094    0.0158    0.2934    0.6976    0.3037    0.7359    0.3982
      0.2729    0.2035    0.6135    0.7247    0.1939    0.7878    0.8831
      0.5249    0.5395    0.5164    0.6738    0.3264    0.2329    0.8510
      0.1769    0.8426    0.4157    0.8302    0.3213    0.3039    0.9644
      0.6068    0.3712    0.5907    0.5675    0.2934    0.4723    0.0905
   

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
      0.1006    0.0044    0.8006    0.3288
   
   R2 = 
      0.2610    0.9036    0.6053    0.9457    0.1794
   
   R3 = 
      0.1006    0.0044    0.8006    0.3288    0.2610    0.9036    0.6053    0.9457    0.1794
   
   C1 = 
      0.9999
      0.2368
      0.9987
      0.8676
      0.7696
      0.2729
      0.3438
      0.9773
      0.5910
      0.4952
   
   C2 = 
      0.2338
      0.0388
      0.4196
      0.5850
      0.3967
      0.9466
      0.4545
      0.5498
      0.9305
      0.4420
   
   M = 
      0.9999    0.2338
      0.2368    0.0388
      0.9987    0.4196
      0.8676    0.5850
      0.7696    0.3967
      0.2729    0.9466
      0.3438    0.4545
      0.9773    0.5498
      0.5910    0.9305
      0.4952    0.4420
   


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
      0.3580    0.5358    0.0555    0.9169
   
   R2 = 
      0.8085    0.6255    0.7808    0.4590
   
   M = 
      0.3580    0.5358    0.0555    0.9169
      0.8085    0.6255    0.7808    0.4590
   
   C1 = 
      0.9135
      0.2233
      0.5501
      0.5330
      0.8729
      0.0686
      0.2246
      0.0770
      0.5342
      0.7754
   
   C2 = 
      0.0802
      0.0716
   
   C3 = 
      0.9135
      0.2233
      0.5501
      0.5330
      0.8729
      0.0686
      0.2246
      0.0770
      0.5342
      0.7754
      0.0802
      0.0716
   

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
   

