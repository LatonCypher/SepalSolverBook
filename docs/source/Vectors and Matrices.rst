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
      0.7518    0.6493    0.2235    0.9683    0.5170    0.4158    0.7279
   
   C = 
      0.6661
      0.4741
      0.6325
      0.8576
      0.7166
   
   M = 
      0.4674    0.2155    0.3473    0.9313    0.1114    0.1555    0.6736
      0.4304    0.2421    0.5028    0.5655    0.7947    0.1312    0.8042
      0.1815    0.7032    0.4472    0.8158    0.3277    0.3344    0.5816
      0.6404    0.2625    0.6301    0.0948    0.3659    0.1316    0.4032
      0.3962    0.7736    0.4572    0.2269    0.2988    0.0156    0.4650
      0.4453    0.5083    0.7159    0.7606    0.2972    0.4357    0.6588
      0.8990    0.1756    0.0068    0.0616    0.9498    0.9956    0.4699
      0.7855    0.4457    0.8842    0.8217    0.8453    0.8222    0.5059
   

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
      0.4762    0.4625    0.7272    0.6796
   
   R2 = 
      0.5062    0.5169    0.8487    0.9695    0.5971
   
   R3 = 
      0.4762    0.4625    0.7272    0.6796    0.5062    0.5169    0.8487    0.9695    0.5971
   
   C1 = 
      0.8137
      0.8186
      0.2169
      0.1466
      0.5492
      0.6534
      0.2465
      0.7843
      0.8878
      0.8510
   
   C2 = 
      0.5260
      0.5871
      0.8981
      0.7568
      0.9235
      0.5012
      0.8591
      0.7410
      0.8649
      0.0584
   
   M = 
      0.8137    0.5260
      0.8186    0.5871
      0.2169    0.8981
      0.1466    0.7568
      0.5492    0.9235
      0.6534    0.5012
      0.2465    0.8591
      0.7843    0.7410
      0.8878    0.8649
      0.8510    0.0584
   


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
      0.4381    0.0075    0.8062    0.5924
   
   R2 = 
      0.6076    0.1131    0.3772    0.7906
   
   M = 
      0.4381    0.0075    0.8062    0.5924
      0.6076    0.1131    0.3772    0.7906
   
   C1 = 
      0.0681
      0.0510
      0.4156
      0.4079
      0.5228
      0.1312
      0.6295
      0.4557
      0.7202
      0.8462
   
   C2 = 
      0.5822
      0.0721
   
   C3 = 
      0.0681
      0.0510
      0.4156
      0.4079
      0.5228
      0.1312
      0.6295
      0.4557
      0.7202
      0.8462
      0.5822
      0.0721
   

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
   

