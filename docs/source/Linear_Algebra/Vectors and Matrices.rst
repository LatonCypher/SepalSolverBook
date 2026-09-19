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
      0.7831    0.5650    0.0569    0.5091    0.4916    0.6388    0.7786
   
   C = 
      0.4804
      0.8941
      0.7490
      0.7809
      0.3857
   
   M = 
      0.1183    0.6219    0.8429    0.6694    0.6567    0.9428    0.8093
      0.0049    0.4809    0.5293    0.1412    0.3978    0.5653    0.1883
      0.7625    0.7168    0.5112    0.0936    0.8929    0.2043    0.0584
      0.3723    0.1014    0.9230    0.2076    0.8496    0.7911    0.9916
      0.6043    0.1118    0.9348    0.3144    0.1391    0.1303    0.7857
      0.0696    0.3831    0.2559    0.5510    0.3008    0.1370    0.2453
      0.5839    0.3009    0.2406    0.3545    0.3671    0.5405    0.9300
      0.2855    0.0204    0.2012    0.1274    0.3704    0.1541    0.6423
   

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
      0.2196    0.0978    0.7986    0.5559
   
   R2 = 
      0.5064    0.8824    0.0543    0.6074    0.2884
   
   R3 = 
      0.2196    0.0978    0.7986    0.5559    0.5064    0.8824    0.0543    0.6074    0.2884
   
   C1 = 
      0.1298
      0.3551
      0.7031
      0.6049
      0.0806
      0.4865
      0.7389
      0.8257
      0.3769
      0.6641
   
   C2 = 
      0.0330
      0.6630
      0.7590
      0.8043
      0.1932
      0.2843
      0.3331
      0.2517
      0.3795
      0.7419
   
   M = 
      0.1298    0.0330
      0.3551    0.6630
      0.7031    0.7590
      0.6049    0.8043
      0.0806    0.1932
      0.4865    0.2843
      0.7389    0.3331
      0.8257    0.2517
      0.3769    0.3795
      0.6641    0.7419
   


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
      0.0984    0.2662    0.9554    0.9526
   
   R2 = 
      0.0121    0.8100    0.7846    0.6749
   
   M = 
      0.0984    0.2662    0.9554    0.9526
      0.0121    0.8100    0.7846    0.6749
   
   C1 = 
      0.1224
      0.1063
      0.0577
      0.3549
      0.8823
      0.5521
      0.4665
      0.4011
      0.8694
      0.8032
   
   C2 = 
      0.7605
      0.7555
   
   C3 = 
      0.1224
      0.1063
      0.0577
      0.3549
      0.8823
      0.5521
      0.4665
      0.4011
      0.8694
      0.8032
      0.7605
      0.7555
   

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
   

