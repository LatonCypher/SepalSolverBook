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
      0.2030    0.3970    0.1630    0.6817    0.8004    0.3770    0.1689
   
   C = 
      0.5907
      0.6091
      0.4655
      0.1486
      0.9583
   
   M = 
      0.7805    0.9733    0.5949    0.1193    0.3034    0.1794    0.4904
      0.3508    0.4467    0.9906    0.6967    0.6515    0.7181    0.5223
      0.1657    0.2360    0.6922    0.4493    0.0440    0.4583    0.0883
      0.5477    0.8599    0.5492    0.9249    0.2556    0.6078    0.4423
      0.2301    0.7814    0.8917    0.3909    0.7322    0.9018    0.7668
      0.5962    0.5738    0.6797    0.5177    0.6521    0.3281    0.9982
      0.6407    0.9436    0.6022    0.8336    0.8750    0.7232    0.7501
      0.2304    0.6246    0.1486    0.9337    0.9725    0.3599    0.9487
   

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
      0.2666    0.7268    0.0452    0.9647
   
   R2 = 
      0.1729    0.2958    0.2412    0.5983    0.3891
   
   R3 = 
      0.2666    0.7268    0.0452    0.9647    0.1729    0.2958    0.2412    0.5983    0.3891
   
   C1 = 
      0.7207
      0.3793
      0.0116
      0.1387
      0.5379
      0.2783
      0.0025
      0.2003
      0.2378
      0.8629
   
   C2 = 
      0.8826
      0.1582
      0.4376
      0.7088
      0.7963
      0.6009
      0.4866
      0.0832
      0.9374
      0.6352
   
   M = 
      0.7207    0.8826
      0.3793    0.1582
      0.0116    0.4376
      0.1387    0.7088
      0.5379    0.7963
      0.2783    0.6009
      0.0025    0.4866
      0.2003    0.0832
      0.2378    0.9374
      0.8629    0.6352
   


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
      0.0959    0.5417    0.4953    0.3566
   
   R2 = 
      0.4650    0.4140    0.3811    0.9432
   
   M = 
      0.0959    0.5417    0.4953    0.3566
      0.4650    0.4140    0.3811    0.9432
   
   C1 = 
      0.1048
      0.6083
      0.9873
      0.4051
      0.8284
      0.1623
      0.3318
      0.9760
      0.5172
      0.1109
   
   C2 = 
      0.4075
      0.3614
   
   C3 = 
      0.1048
      0.6083
      0.9873
      0.4051
      0.8284
      0.1623
      0.3318
      0.9760
      0.5172
      0.1109
      0.4075
      0.3614
   

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
   

