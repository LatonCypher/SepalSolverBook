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
      0.7722    0.9253    0.1829    0.6741    0.5462    0.0861    0.8544
   
   C = 
      0.3308
      0.8569
      0.2432
      0.7657
      0.6048
   
   M = 
      0.8646    0.9625    0.2065    0.1665    0.7496    0.1108    0.4213
      0.6204    0.9056    0.9274    0.4646    0.7130    0.2810    0.6474
      0.2076    0.5374    0.1551    0.5858    0.1679    0.3337    0.1541
      0.5862    0.5243    0.9743    0.4214    0.2588    0.0871    0.7934
      0.4537    0.9879    0.9064    0.4463    0.6920    0.4279    0.6083
      0.3594    0.1480    0.6239    0.5933    0.6516    0.1136    0.0692
      0.0719    0.0743    0.7906    0.1388    0.3887    0.4478    0.1721
      0.7149    0.5993    0.0025    0.8133    0.6123    0.5072    0.7184
   

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
      0.1524    0.0939    0.0785    0.6363
   
   R2 = 
      0.4956    0.0162    0.8593    0.0175    0.6230
   
   R3 = 
      0.1524    0.0939    0.0785    0.6363    0.4956    0.0162    0.8593    0.0175    0.6230
   
   C1 = 
      0.4974
      0.5742
      0.5785
      0.9201
      0.1060
      0.8537
      0.4416
      0.7350
      0.6044
      0.1721
   
   C2 = 
      0.3330
      0.3685
      0.2326
      0.8532
      0.7848
      0.5007
      0.1539
      0.9553
      0.3602
      0.0113
   
   M = 
      0.4974    0.3330
      0.5742    0.3685
      0.5785    0.2326
      0.9201    0.8532
      0.1060    0.7848
      0.8537    0.5007
      0.4416    0.1539
      0.7350    0.9553
      0.6044    0.3602
      0.1721    0.0113
   


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
      0.9799    0.0140    0.1536    0.9384
   
   R2 = 
      0.6672    0.1892    0.8051    0.7190
   
   M = 
      0.9799    0.0140    0.1536    0.9384
      0.6672    0.1892    0.8051    0.7190
   
   C1 = 
      0.8472
      0.8427
      0.8587
      0.4659
      0.1980
      0.0005
      0.1554
      0.6489
      0.5538
      0.1672
   
   C2 = 
      0.0984
      0.8932
   
   C3 = 
      0.8472
      0.8427
      0.8587
      0.4659
      0.1980
      0.0005
      0.1554
      0.6489
      0.5538
      0.1672
      0.0984
      0.8932
   

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
   

