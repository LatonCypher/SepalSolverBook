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
      0.9179    0.6644    0.5155    0.9568    0.0163    0.2929    0.9954
   
   C = 
      0.5361
      0.8189
      0.1181
      0.8516
      0.1644
   
   M = 
      0.3615    0.9476    0.9250    0.2313    0.9047    0.2231    0.4925
      0.9070    0.2664    0.3164    0.0931    0.9646    0.7476    0.8761
      0.4801    0.3908    0.6794    0.0149    0.3749    0.6181    0.1644
      0.2348    0.7959    0.5176    0.9955    0.5983    0.5207    0.0739
      0.6987    0.2889    0.7120    0.6523    0.2856    0.1499    0.3095
      0.1792    0.6519    0.3630    0.9534    0.2836    0.5815    0.1353
      0.6435    0.4959    0.5636    0.2948    0.9754    0.0602    0.0169
      0.2462    0.7127    0.8977    0.0472    0.5650    0.5148    0.5418
   

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
      0.7756    0.4711    0.8106    0.6283
   
   R2 = 
      0.6812    0.1350    0.7137    0.4282    0.5426
   
   R3 = 
      0.7756    0.4711    0.8106    0.6283    0.6812    0.1350    0.7137    0.4282    0.5426
   
   C1 = 
      0.0537
      0.6275
      0.9941
      0.8417
      0.4302
      0.5863
      0.5425
      0.9030
      0.0202
      0.7145
   
   C2 = 
      0.1890
      0.6712
      0.6280
      0.0845
      0.1189
      0.5249
      0.5595
      0.5968
      0.2001
      0.1779
   
   M = 
      0.0537    0.1890
      0.6275    0.6712
      0.9941    0.6280
      0.8417    0.0845
      0.4302    0.1189
      0.5863    0.5249
      0.5425    0.5595
      0.9030    0.5968
      0.0202    0.2001
      0.7145    0.1779
   


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
      0.7495    0.5539    0.0954    0.7838
   
   R2 = 
      0.2476    0.7677    0.2510    0.4771
   
   M = 
      0.7495    0.5539    0.0954    0.7838
      0.2476    0.7677    0.2510    0.4771
   
   C1 = 
      0.3075
      0.9688
      0.7165
      0.6049
      0.8836
      0.5708
      0.1883
      0.1660
      0.0290
      0.6075
   
   C2 = 
      0.2741
      0.8252
   
   C3 = 
      0.3075
      0.9688
      0.7165
      0.6049
      0.8836
      0.5708
      0.1883
      0.1660
      0.0290
      0.6075
      0.2741
      0.8252
   

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
   

