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
      0.4793    0.8171    0.2974    0.3593    0.6876    0.5549    0.5383
   
   C = 
      0.3110
      0.3312
      0.0808
      0.5422
      0.8319
   
   M = 
      0.1110    0.9652    0.1034    0.3173    0.9921    0.7470    0.1930
      0.2225    0.1983    0.4347    0.3929    0.2203    0.8191    0.4171
      0.3110    0.5129    0.4523    0.3561    0.4509    0.7240    0.4462
      0.5677    0.6253    0.6975    0.4121    0.2422    0.0795    0.5848
      0.9140    0.9050    0.9050    0.3687    0.8126    0.3133    0.6817
      0.3695    0.7223    0.2062    0.7703    0.0380    0.1917    0.8039
      0.2621    0.7904    0.6788    0.9994    0.6120    0.0052    0.7005
      0.4819    0.3391    0.8368    0.5372    0.1862    0.2058    0.4572
   

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
      0.5666    0.7408    0.5078    0.5826
   
   R2 = 
      0.3091    0.9877    0.5200    0.4693    0.0585
   
   R3 = 
      0.5666    0.7408    0.5078    0.5826    0.3091    0.9877    0.5200    0.4693    0.0585
   
   C1 = 
      0.6284
      0.4710
      0.0368
      0.2181
      0.0603
      0.2658
      0.7657
      0.8362
      0.8473
      0.2964
   
   C2 = 
      0.8144
      0.5353
      0.8647
      0.0674
      0.2771
      0.6900
      0.6786
      0.9985
      0.3710
      0.4277
   
   M = 
      0.6284    0.8144
      0.4710    0.5353
      0.0368    0.8647
      0.2181    0.0674
      0.0603    0.2771
      0.2658    0.6900
      0.7657    0.6786
      0.8362    0.9985
      0.8473    0.3710
      0.2964    0.4277
   


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
      0.1097    0.9912    0.4374    0.4164
   
   R2 = 
      0.2786    0.9008    0.8826    0.4112
   
   M = 
      0.1097    0.9912    0.4374    0.4164
      0.2786    0.9008    0.8826    0.4112
   
   C1 = 
      0.6931
      0.7498
      0.8275
      0.6760
      0.9724
      0.6681
      0.6473
      0.8815
      0.8432
      0.2612
   
   C2 = 
      0.3850
      0.1429
   
   C3 = 
      0.6931
      0.7498
      0.8275
      0.6760
      0.9724
      0.6681
      0.6473
      0.8815
      0.8432
      0.2612
      0.3850
      0.1429
   

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
   

