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
      0.7092    0.0340    0.0227    0.4224    0.8922    0.3545    0.9145
   
   C = 
      0.4068
      0.9858
      0.7878
      0.7963
      0.9272
   
   M = 
      0.8173    0.8802    0.6554    0.6058    0.4349    0.2473    0.5899
      0.2566    0.6163    0.2234    0.4521    0.3201    0.7550    0.5620
      0.4224    0.2579    0.0204    0.8351    0.5440    0.6682    0.5240
      0.6472    0.5415    0.8035    0.4701    0.8791    0.4851    0.7746
      0.2252    0.6438    0.5217    0.8220    0.6423    0.0604    0.2338
      0.6941    0.4796    0.0296    0.6017    0.1411    0.1159    0.7398
      0.3550    0.4101    0.4927    0.2023    0.2029    0.3481    0.3690
      0.8987    0.8652    0.2164    0.2136    0.3851    0.6286    0.9932
   

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
      0.8996    0.6878    0.8882    0.4988
   
   R2 = 
      0.7952    0.5809    0.2113    0.5867    0.6192
   
   R3 = 
      0.8996    0.6878    0.8882    0.4988    0.7952    0.5809    0.2113    0.5867    0.6192
   
   C1 = 
      0.6146
      0.2088
      0.9949
      0.0337
      0.8492
      0.7319
      0.3113
      0.9952
      0.4700
      0.9639
   
   C2 = 
      0.1897
      0.4800
      0.3169
      0.2991
      0.7083
      0.3335
      0.8095
      0.6037
      0.8084
      0.1774
   
   M = 
      0.6146    0.1897
      0.2088    0.4800
      0.9949    0.3169
      0.0337    0.2991
      0.8492    0.7083
      0.7319    0.3335
      0.3113    0.8095
      0.9952    0.6037
      0.4700    0.8084
      0.9639    0.1774
   


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
      0.8950    0.4518    0.8181    0.8975
   
   R2 = 
      0.0414    0.3467    0.8961    0.4802
   
   M = 
      0.8950    0.4518    0.8181    0.8975
      0.0414    0.3467    0.8961    0.4802
   
   C1 = 
      0.5703
      0.8017
      0.2371
      0.0356
      0.4973
      0.4759
      0.0136
      0.9458
      0.1593
      0.2715
   
   C2 = 
      0.1303
      0.3682
   
   C3 = 
      0.5703
      0.8017
      0.2371
      0.0356
      0.4973
      0.4759
      0.0136
      0.9458
      0.1593
      0.2715
      0.1303
      0.3682
   

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
   

