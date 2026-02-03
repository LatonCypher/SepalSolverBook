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
      0.7980    0.6157    0.4988    0.3546    0.9061    0.3369    0.6085
   
   C = 
      0.7086
      0.5094
      0.4774
      0.0768
      0.5243
   
   M = 
      0.5063    0.5948    0.7856    0.6652    0.1008    0.4102    0.7542
      0.1288    0.2215    0.3213    0.8698    0.5880    0.3373    0.6996
      0.8163    0.0303    0.0622    0.6074    0.3014    0.0108    0.0737
      0.9153    0.4494    0.4716    0.1793    0.4938    0.4431    0.6746
      0.5585    0.5913    0.8141    0.4701    0.6000    0.0132    0.0331
      0.4030    0.4401    0.3133    0.2232    0.5630    0.1365    0.2192
      0.0117    0.5178    0.7706    0.6709    0.4871    0.1749    0.7146
      0.3233    0.2190    0.0768    0.4021    0.8731    0.3778    0.7985
   

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
      0.0443    0.2525    0.0447    0.5236
   
   R2 = 
      0.3751    0.4319    0.5548    0.3411    0.5723
   
   R3 = 
      0.0443    0.2525    0.0447    0.5236    0.3751    0.4319    0.5548    0.3411    0.5723
   
   C1 = 
      0.4921
      0.1507
      0.8327
      0.2897
      0.9487
      0.2496
      0.6188
      0.0734
      0.5876
      0.7077
   
   C2 = 
      0.3727
      0.1056
      0.0480
      0.7451
      0.2491
      0.7365
      0.7696
      0.7237
      0.4880
      0.3880
   
   M = 
      0.4921    0.3727
      0.1507    0.1056
      0.8327    0.0480
      0.2897    0.7451
      0.9487    0.2491
      0.2496    0.7365
      0.6188    0.7696
      0.0734    0.7237
      0.5876    0.4880
      0.7077    0.3880
   


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
      0.2076    0.5845    0.6414    0.4277
   
   R2 = 
      0.6990    0.9169    0.4841    0.2348
   
   M = 
      0.2076    0.5845    0.6414    0.4277
      0.6990    0.9169    0.4841    0.2348
   
   C1 = 
      0.4962
      0.0522
      0.4598
      0.9631
      0.4252
      0.8021
      0.0690
      0.2278
      0.5885
      0.4879
   
   C2 = 
      0.2401
      0.1597
   
   C3 = 
      0.4962
      0.0522
      0.4598
      0.9631
      0.4252
      0.8021
      0.0690
      0.2278
      0.5885
      0.4879
      0.2401
      0.1597
   

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
   

