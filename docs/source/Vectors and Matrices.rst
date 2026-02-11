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
      0.7544    0.4862    0.4103    0.8462    0.4070    0.5822    0.8855
   
   C = 
      0.7669
      0.3594
      0.9116
      0.6216
      0.0737
   
   M = 
      0.5908    0.6633    0.2688    0.9884    0.9176    0.7428    0.5617
      0.5820    0.0891    0.8681    0.1807    0.8676    0.4655    0.9331
      0.4701    0.3357    0.8871    0.4379    0.8949    0.2100    0.2327
      0.3091    0.5465    0.3345    0.8919    0.5817    0.9986    0.0917
      0.8874    0.0193    0.7250    0.5157    0.3198    0.9224    0.1780
      0.2629    0.6680    0.9588    0.4096    0.8207    0.4280    0.2522
      0.7213    0.8727    0.1873    0.4528    0.3791    0.1943    0.2041
      0.5387    0.2679    0.4802    0.8977    0.5410    0.1012    0.7974
   

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
      0.1442    0.4301    0.8783    0.3811
   
   R2 = 
      0.1780    0.0471    0.8682    0.9234    0.0432
   
   R3 = 
      0.1442    0.4301    0.8783    0.3811    0.1780    0.0471    0.8682    0.9234    0.0432
   
   C1 = 
      0.3460
      0.6889
      0.5394
      0.7942
      0.7639
      0.0094
      0.7589
      0.9575
      0.8572
      0.7930
   
   C2 = 
      0.9168
      0.3029
      0.2954
      0.2186
      0.2542
      0.2973
      0.5273
      0.3900
      0.2972
      0.5894
   
   M = 
      0.3460    0.9168
      0.6889    0.3029
      0.5394    0.2954
      0.7942    0.2186
      0.7639    0.2542
      0.0094    0.2973
      0.7589    0.5273
      0.9575    0.3900
      0.8572    0.2972
      0.7930    0.5894
   


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
      0.7430    0.9272    0.7098    0.1368
   
   R2 = 
      0.0219    0.5228    0.3477    0.2866
   
   M = 
      0.7430    0.9272    0.7098    0.1368
      0.0219    0.5228    0.3477    0.2866
   
   C1 = 
      0.1863
      0.4717
      0.6113
      0.0235
      0.3908
      0.1182
      0.3399
      0.6389
      0.1442
      0.3906
   
   C2 = 
      0.8521
      0.8507
   
   C3 = 
      0.1863
      0.4717
      0.6113
      0.0235
      0.3908
      0.1182
      0.3399
      0.6389
      0.1442
      0.3906
      0.8521
      0.8507
   

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
   

