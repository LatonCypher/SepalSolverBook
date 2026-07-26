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
      0.4275    0.2610    0.5911    0.0487    0.0481    0.9874    0.1150
   
   C = 
      0.5961
      0.9233
      0.2929
      0.4610
      0.5380
   
   M = 
      0.5659    0.2388    0.8755    0.5549    0.7966    0.7600    0.9130
      0.2956    0.9749    0.6939    0.0040    0.9613    0.4589    0.3102
      0.0682    0.8532    0.5723    0.0130    0.6607    0.1162    0.2503
      0.8768    0.2218    0.7828    0.3608    0.8703    0.0715    0.9609
      0.5913    0.3055    0.5808    0.6242    0.9228    0.1892    0.6727
      0.1088    0.5722    0.4890    0.5700    0.0229    0.6378    0.9870
      0.1852    0.0158    0.7811    0.5232    0.2231    0.3109    0.3675
      0.4671    0.1839    0.2980    0.9867    0.7172    0.5682    0.7046
   

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
      0.4572    0.7564    0.7155    0.0274
   
   R2 = 
      0.1321    0.1761    0.5563    0.7886    0.1238
   
   R3 = 
      0.4572    0.7564    0.7155    0.0274    0.1321    0.1761    0.5563    0.7886    0.1238
   
   C1 = 
      0.5466
      0.0696
      0.7748
      0.8234
      0.0110
      0.3027
      0.8151
      0.1932
      0.4134
      0.0569
   
   C2 = 
      0.3203
      0.6991
      0.6106
      0.3481
      0.3928
      0.6479
      0.2764
      0.4708
      0.0555
      0.2676
   
   M = 
      0.5466    0.3203
      0.0696    0.6991
      0.7748    0.6106
      0.8234    0.3481
      0.0110    0.3928
      0.3027    0.6479
      0.8151    0.2764
      0.1932    0.4708
      0.4134    0.0555
      0.0569    0.2676
   


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
      0.6507    0.0429    0.8933    0.4596
   
   R2 = 
      0.3005    0.2822    0.8329    0.2759
   
   M = 
      0.6507    0.0429    0.8933    0.4596
      0.3005    0.2822    0.8329    0.2759
   
   C1 = 
      0.3762
      0.0439
      0.8656
      0.6247
      0.1441
      0.2789
      0.5062
      0.9892
      0.0157
      0.4261
   
   C2 = 
      0.4398
      0.7983
   
   C3 = 
      0.3762
      0.0439
      0.8656
      0.6247
      0.1441
      0.2789
      0.5062
      0.9892
      0.0157
      0.4261
      0.4398
      0.7983
   

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
   

