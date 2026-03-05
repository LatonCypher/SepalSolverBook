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
      0.5013    0.4453    0.0350    0.8332    0.7757    0.7307    0.9381
   
   C = 
      0.9040
      0.5531
      0.2175
      0.7211
      0.9099
   
   M = 
      0.2913    0.5939    0.2158    0.8202    0.8704    0.4748    0.9386
      0.1175    0.0755    0.5345    0.5381    0.8766    0.4877    0.5162
      0.3362    0.3230    0.6371    0.3638    0.0119    0.7473    0.1380
      0.5455    0.9573    0.9607    0.1584    0.1229    0.9779    0.0798
      0.7362    0.8400    0.8249    0.4735    0.8858    0.2299    0.9992
      0.2909    0.9492    0.9006    0.9611    0.4885    0.4325    0.3346
      0.4351    0.0703    0.1658    0.4970    0.2781    0.6187    0.4686
      0.6395    0.3289    0.8887    0.6194    0.8949    0.1262    0.4499
   

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
      0.7010    0.1193    0.2217    0.4803
   
   R2 = 
      0.1453    0.6810    0.0863    0.4299    0.1718
   
   R3 = 
      0.7010    0.1193    0.2217    0.4803    0.1453    0.6810    0.0863    0.4299    0.1718
   
   C1 = 
      0.4549
      0.4783
      0.7824
      0.6693
      0.5660
      0.9835
      0.6048
      0.1007
      0.0332
      0.0474
   
   C2 = 
      0.7685
      0.0554
      0.6642
      0.8299
      0.4069
      0.8778
      0.6548
      0.8574
      0.2379
      0.6304
   
   M = 
      0.4549    0.7685
      0.4783    0.0554
      0.7824    0.6642
      0.6693    0.8299
      0.5660    0.4069
      0.9835    0.8778
      0.6048    0.6548
      0.1007    0.8574
      0.0332    0.2379
      0.0474    0.6304
   


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
      0.9415    0.6012    0.8791    0.4569
   
   R2 = 
      0.4522    0.3240    0.2640    0.5754
   
   M = 
      0.9415    0.6012    0.8791    0.4569
      0.4522    0.3240    0.2640    0.5754
   
   C1 = 
      0.4255
      0.5749
      0.0066
      0.9386
      0.7813
      0.2952
      0.7470
      0.6593
      0.2586
      0.0773
   
   C2 = 
      0.7083
      0.5865
   
   C3 = 
      0.4255
      0.5749
      0.0066
      0.9386
      0.7813
      0.2952
      0.7470
      0.6593
      0.2586
      0.0773
      0.7083
      0.5865
   

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
   

