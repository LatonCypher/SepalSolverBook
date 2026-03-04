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
      0.1050    0.1144    0.6165    0.3864    0.4598    0.1259    0.3273
   
   C = 
      0.4250
      0.3070
      0.1270
      0.4688
      0.4270
   
   M = 
      0.5402    0.4313    0.3677    0.0398    0.7632    0.7799    0.3754
      0.9365    0.3569    0.1159    0.2431    0.3637    0.0267    0.9207
      0.9929    0.3891    0.6203    0.9713    0.8851    0.9095    0.5377
      0.4578    0.7351    0.7041    0.5497    0.5193    0.9280    0.4898
      0.4687    0.3235    0.8534    0.8399    0.5844    0.2784    0.1034
      0.6393    0.6840    0.0349    0.8128    0.7417    0.6718    0.4094
      0.7863    0.9678    0.2691    0.5856    0.5403    0.5210    0.0912
      0.7660    0.0820    0.9708    0.5654    0.7660    0.2203    0.2643
   

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
      0.2374    0.6722    0.0077    0.4086
   
   R2 = 
      0.3353    0.2971    0.4974    0.8187    0.0220
   
   R3 = 
      0.2374    0.6722    0.0077    0.4086    0.3353    0.2971    0.4974    0.8187    0.0220
   
   C1 = 
      0.5877
      0.2801
      0.3054
      0.9755
      0.3218
      0.5365
      0.5516
      0.3511
      0.8737
      0.5848
   
   C2 = 
      0.2684
      0.4009
      0.6319
      0.1565
      0.1033
      0.8333
      0.5470
      0.1330
      0.7747
      0.3334
   
   M = 
      0.5877    0.2684
      0.2801    0.4009
      0.3054    0.6319
      0.9755    0.1565
      0.3218    0.1033
      0.5365    0.8333
      0.5516    0.5470
      0.3511    0.1330
      0.8737    0.7747
      0.5848    0.3334
   


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
      0.5466    0.7917    0.0511    0.7760
   
   R2 = 
      0.1110    0.4217    0.3629    0.0710
   
   M = 
      0.5466    0.7917    0.0511    0.7760
      0.1110    0.4217    0.3629    0.0710
   
   C1 = 
      0.5428
      0.3330
      0.7826
      0.1789
      0.0090
      0.5295
      0.7223
      0.8633
      0.3853
      0.3276
   
   C2 = 
      0.0580
      0.6538
   
   C3 = 
      0.5428
      0.3330
      0.7826
      0.1789
      0.0090
      0.5295
      0.7223
      0.8633
      0.3853
      0.3276
      0.0580
      0.6538
   

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
   

