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
   5 6 7 1
   
   C = 
   8
   3
   4
   2
   7
   
   M = 
   5 -2 3 7
   2 1 -7 3
   4 8 9 1
   0 5 -6 -3
   


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
      0.0404    0.3589    0.9744    0.7866    0.2069    0.1776    0.0873
   
   C = 
      0.6551
      0.5714
      0.9719
      0.7377
      0.2571
   
   M = 
      0.1779    0.9953    0.9952    0.1017    0.5006    0.5553    0.1147
      0.3354    0.8922    0.2953    0.1347    0.8329    0.4429    0.0350
      0.2239    0.2540    0.7259    0.9407    0.4447    0.8296    0.2770
      0.7533    0.0067    0.3042    0.1047    0.1708    0.5257    0.7707
      0.6806    0.3493    0.0035    0.2206    0.2610    0.1618    0.6773
      0.4125    0.3728    0.3924    0.8936    0.0035    0.7143    0.2279
      0.7665    0.6716    0.7048    0.9101    0.3061    0.3386    0.8040
      0.6261    0.3117    0.5192    0.1597    0.5715    0.5718    0.8115
   

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
   0 0 0 0 0 0 0
   
   C = 
   1
   1
   1
   1
   1
   
   M = 
   1 0 0 0 0 0 0
   0 1 0 0 0 0 0
   0 0 1 0 0 0 0
   0 0 0 1 0 0 0
   0 0 0 0 1 0 0
   0 0 0 0 0 1 0
   0 0 0 0 0 0 1
   

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
      0.6633    0.8949    0.8311    0.1191
   
   R2 = 
      0.7124    0.7388    0.3275    0.6223    0.6387
   
   R3 = 
      0.6633    0.8949    0.8311    0.1191    0.7124    0.7388    0.3275    0.6223    0.6387
   
   C1 = 
      0.1367
      0.7580
      0.4677
      0.5421
      0.9326
      0.6289
      0.7456
      0.7926
      0.5720
      0.5952
   
   C2 = 
      0.4733
      0.2052
      0.7777
      0.1892
      0.1351
      0.5269
      0.5576
      0.4927
      0.1032
      0.2902
   
   M = 
      0.1367    0.4733
      0.7580    0.2052
      0.4677    0.7777
      0.5421    0.1892
      0.9326    0.1351
      0.6289    0.5269
      0.7456    0.5576
      0.7926    0.4927
      0.5720    0.1032
      0.5952    0.2902
   


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
      0.1803    0.6044    0.8899    0.9050
   
   R2 = 
      0.6654    0.9583    0.7906    0.6213
   
   M = 
      0.1803    0.6044    0.8899    0.9050
      0.6654    0.9583    0.7906    0.6213
   
   C1 = 
      0.1514
      0.1210
      0.7650
      0.1956
      0.9721
      0.6164
      0.8203
      0.1630
      0.2857
      0.1688
   
   C2 = 
      0.4578
      0.9210
   
   C3 = 
      0.1514
      0.1210
      0.7650
      0.1956
      0.9721
      0.6164
      0.8203
      0.1630
      0.2857
      0.1688
      0.4578
      0.9210
   

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
   5 -2 3 7
   2 1 -7 3
   4 8 9 1
   0 5 -6 -3
   
   Flipud(M) = 
   0 5 -6 -3
   4 8 9 1
   2 1 -7 3
   5 -2 3 7
   
   Fliplr(M) = 
   7 3 -2 5
   3 -7 1 2
   1 9 8 4
   -3 -6 5 0
   

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
   5 -2 3 7
   0 1 -7 3
   0 0 9 1
   0 0 0 -3
   
   Tril(M) = 
   5 0 0 0
   2 1 0 0
   4 8 9 0
   0 5 -6 -3
   

