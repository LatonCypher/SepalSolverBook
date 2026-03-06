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
      0.5195    0.9496    0.0508    0.3073    0.4324    0.2356    0.5542
   
   C = 
      0.6848
      0.5434
      0.2847
      0.1501
      0.7793
   
   M = 
      0.0680    0.9703    0.8082    0.5556    0.1058    0.4173    0.9165
      0.0031    0.1388    0.0035    0.7854    0.2692    0.8119    0.7258
      0.3229    0.6023    0.6006    0.2680    0.1369    0.1068    0.1696
      0.9929    0.3738    0.6344    0.5521    0.7073    0.9441    0.3771
      0.1238    0.3796    0.2760    0.7137    0.3995    0.2283    0.1124
      0.0967    0.0019    0.4799    0.2315    0.6687    0.9064    0.8987
      0.4613    0.1955    0.1109    0.1729    0.1529    0.9112    0.8592
      0.7888    0.5815    0.6370    0.2102    0.2849    0.1943    0.8072
   

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
      0.5566    0.1129    0.4086    0.5830
   
   R2 = 
      0.0696    0.4131    0.6369    0.6466    0.7331
   
   R3 = 
      0.5566    0.1129    0.4086    0.5830    0.0696    0.4131    0.6369    0.6466    0.7331
   
   C1 = 
      0.5751
      0.2606
      0.2374
      0.0734
      0.9083
      0.8294
      0.3922
      0.9073
      0.0985
      0.1438
   
   C2 = 
      0.1995
      0.7186
      0.4771
      0.4626
      0.2283
      0.3289
      0.9229
      0.7637
      0.2212
      0.5085
   
   M = 
      0.5751    0.1995
      0.2606    0.7186
      0.2374    0.4771
      0.0734    0.4626
      0.9083    0.2283
      0.8294    0.3289
      0.3922    0.9229
      0.9073    0.7637
      0.0985    0.2212
      0.1438    0.5085
   


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
      0.8625    0.1265    0.1277    0.0019
   
   R2 = 
      0.2902    0.9273    0.9100    0.0166
   
   M = 
      0.8625    0.1265    0.1277    0.0019
      0.2902    0.9273    0.9100    0.0166
   
   C1 = 
      0.1424
      0.9695
      0.8063
      0.7260
      0.9601
      0.6116
      0.1509
      0.4843
      0.9318
      0.8052
   
   C2 = 
      0.0191
      0.8173
   
   C3 = 
      0.1424
      0.9695
      0.8063
      0.7260
      0.9601
      0.6116
      0.1509
      0.4843
      0.9318
      0.8052
      0.0191
      0.8173
   

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
   

