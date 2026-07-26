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
      0.8772    0.0546    0.6530    0.2367    0.6038    0.8703    0.6426
   
   C = 
      0.1381
      0.7489
      0.2353
      0.6816
      0.3516
   
   M = 
      0.4167    0.6381    0.9009    0.9455    0.6315    0.5202    0.5887
      0.0158    0.9731    0.6431    0.0730    0.6268    0.7383    0.9534
      0.2949    0.2884    0.0700    0.3758    0.5980    0.7322    0.4034
      0.7737    0.3431    0.3707    0.6626    0.4964    0.3864    0.4897
      0.8780    0.5764    0.0246    0.8148    0.0047    0.7348    0.0217
      0.4999    0.0899    0.5068    0.2392    0.0720    0.2603    0.0232
      0.8511    0.1596    0.0220    0.7800    0.1317    0.9595    0.4631
      0.5288    0.1097    0.2755    0.9901    0.6159    0.1879    0.8878
   

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
      0.3123    0.0317    0.7075    0.9488
   
   R2 = 
      0.4738    0.0734    0.1636    0.9973    0.7952
   
   R3 = 
      0.3123    0.0317    0.7075    0.9488    0.4738    0.0734    0.1636    0.9973    0.7952
   
   C1 = 
      0.4531
      0.0669
      0.1599
      0.7952
      0.0442
      0.5472
      0.5682
      0.5954
      0.7132
      0.7705
   
   C2 = 
      0.2807
      0.2631
      0.9406
      0.7579
      0.4705
      0.2870
      0.7833
      0.9197
      0.7986
      0.2248
   
   M = 
      0.4531    0.2807
      0.0669    0.2631
      0.1599    0.9406
      0.7952    0.7579
      0.0442    0.4705
      0.5472    0.2870
      0.5682    0.7833
      0.5954    0.9197
      0.7132    0.7986
      0.7705    0.2248
   


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
      0.1754    0.5160    0.9162    0.5772
   
   R2 = 
      0.2711    0.7487    0.9731    0.2928
   
   M = 
      0.1754    0.5160    0.9162    0.5772
      0.2711    0.7487    0.9731    0.2928
   
   C1 = 
      0.9445
      0.7552
      0.0093
      0.3390
      0.8126
      0.8926
      0.2607
      0.5571
      0.7165
      0.8631
   
   C2 = 
      0.2418
      0.9778
   
   C3 = 
      0.9445
      0.7552
      0.0093
      0.3390
      0.8126
      0.8926
      0.2607
      0.5571
      0.7165
      0.8631
      0.2418
      0.9778
   

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
   

