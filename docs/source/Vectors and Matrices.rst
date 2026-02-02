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
      0.5201    0.6132    0.5674    0.7815    0.0176    0.6902    0.2566
   
   C = 
      0.1021
      0.7777
      0.8790
      0.0759
      0.6693
   
   M = 
      0.8049    0.1911    0.7695    0.0994    0.6787    0.9211    0.3785
      0.6615    0.1523    0.3889    0.7093    0.4150    0.4489    0.2671
      0.4128    0.7320    0.1688    0.7039    0.6341    0.2025    0.0791
      0.3597    0.1226    0.8240    0.3263    0.5675    0.3177    0.2459
      0.5928    0.4640    0.1866    0.4764    0.3310    0.6823    0.5269
      0.7484    0.0301    0.6274    0.2151    0.5188    0.4836    0.1183
      0.4177    0.0484    0.1085    0.3466    0.2739    0.2399    0.8960
      0.4335    0.7200    0.5494    0.3891    0.7296    0.9667    0.5645
   

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
      0.9590    0.0860    0.9224    0.9986
   
   R2 = 
      0.9606    0.9836    0.1599    0.9928    0.3553
   
   R3 = 
      0.9590    0.0860    0.9224    0.9986    0.9606    0.9836    0.1599    0.9928    0.3553
   
   C1 = 
      0.0637
      0.0061
      0.5474
      0.8342
      0.2676
      0.2369
      0.4382
      0.7708
      0.8878
      0.5123
   
   C2 = 
      0.5166
      0.6152
      0.4132
      0.2738
      0.6104
      0.0463
      0.3357
      0.2555
      0.7822
      0.1546
   
   M = 
      0.0637    0.5166
      0.0061    0.6152
      0.5474    0.4132
      0.8342    0.2738
      0.2676    0.6104
      0.2369    0.0463
      0.4382    0.3357
      0.7708    0.2555
      0.8878    0.7822
      0.5123    0.1546
   


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
      0.3602    0.4924    0.5728    0.7414
   
   R2 = 
      0.7333    0.6907    0.5068    0.5188
   
   M = 
      0.3602    0.4924    0.5728    0.7414
      0.7333    0.6907    0.5068    0.5188
   
   C1 = 
      0.1839
      0.7106
      0.2566
      0.5993
      0.2819
      0.1194
      0.0366
      0.5386
      0.0507
      0.2513
   
   C2 = 
      0.3904
      0.8878
   
   C3 = 
      0.1839
      0.7106
      0.2566
      0.5993
      0.2819
      0.1194
      0.0366
      0.5386
      0.0507
      0.2513
      0.3904
      0.8878
   

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
   

