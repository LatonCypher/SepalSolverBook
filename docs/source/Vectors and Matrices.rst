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
      0.1353    0.4971    0.6271    0.2106    0.8792    0.5355    0.4106
   
   C = 
      0.2292
      0.3561
      0.7660
      0.7159
      0.5549
   
   M = 
      0.0432    0.2023    0.0145    0.6272    0.7385    0.4775    0.1654
      0.4393    0.8386    0.0776    0.7621    0.2209    0.6779    0.8501
      0.2670    0.9245    0.9754    0.4055    0.4418    0.2692    0.9730
      0.2678    0.2854    0.5300    0.1228    0.4178    0.8708    0.2413
      0.6445    0.9090    0.3618    0.2370    0.0891    0.0171    0.8208
      0.7855    0.4696    0.2872    0.2560    0.6455    0.7791    0.5994
      0.4452    0.2238    0.0443    0.3041    0.9901    0.9257    0.8689
      0.6719    0.0573    0.6239    0.2895    0.9768    0.7063    0.6970
   

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
      0.5573    0.1622    0.4128    0.8859
   
   R2 = 
      0.7191    0.5058    0.0247    0.9263    0.1704
   
   R3 = 
      0.5573    0.1622    0.4128    0.8859    0.7191    0.5058    0.0247    0.9263    0.1704
   
   C1 = 
      0.4941
      0.9689
      0.4097
      0.2545
      0.7530
      0.6756
      0.0649
      0.6725
      0.2363
      0.6590
   
   C2 = 
      0.7754
      0.1610
      0.3023
      0.2435
      0.5140
      0.0608
      0.4398
      0.5947
      0.1420
      0.5344
   
   M = 
      0.4941    0.7754
      0.9689    0.1610
      0.4097    0.3023
      0.2545    0.2435
      0.7530    0.5140
      0.6756    0.0608
      0.0649    0.4398
      0.6725    0.5947
      0.2363    0.1420
      0.6590    0.5344
   


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
      0.4212    0.7305    0.1584    0.6190
   
   R2 = 
      0.4698    0.4986    0.4268    0.1074
   
   M = 
      0.4212    0.7305    0.1584    0.6190
      0.4698    0.4986    0.4268    0.1074
   
   C1 = 
      0.9836
      0.1645
      0.7137
      0.9296
      0.7898
      0.0881
      0.6315
      0.4903
      0.1194
      0.7597
   
   C2 = 
      0.0507
      0.8880
   
   C3 = 
      0.9836
      0.1645
      0.7137
      0.9296
      0.7898
      0.0881
      0.6315
      0.4903
      0.1194
      0.7597
      0.0507
      0.8880
   

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
   

