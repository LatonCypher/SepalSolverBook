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
      0.5445    0.1652    0.3109    0.6233    0.8942    0.8860    0.7572
   
   C = 
      0.5176
      0.8211
      0.5282
      0.7977
      0.0156
   
   M = 
      0.5520    0.6672    0.6055    0.1456    0.6411    0.9252    0.6583
      0.2750    0.6095    0.7871    0.9554    0.8255    0.0590    0.7073
      0.2838    0.7945    0.2513    0.9090    0.3151    0.8075    0.8439
      0.0929    0.5414    0.0311    0.3815    0.4742    0.4247    0.3658
      0.8553    0.2005    0.4959    0.5916    0.2122    0.1304    0.6305
      0.7409    0.0928    0.1437    0.7712    0.8131    0.3243    0.6527
      0.0384    0.6976    0.4314    0.0673    0.9547    0.1844    0.8824
      0.9620    0.5669    0.8316    0.5966    0.7825    0.9698    0.7473
   

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
      0.5876    0.3272    0.6778    0.2079
   
   R2 = 
      0.4752    0.0263    0.8981    0.5474    0.2193
   
   R3 = 
      0.5876    0.3272    0.6778    0.2079    0.4752    0.0263    0.8981    0.5474    0.2193
   
   C1 = 
      0.0689
      0.2862
      0.4171
      0.0239
      0.8504
      0.4080
      0.3262
      0.2362
      0.2018
      0.7335
   
   C2 = 
      0.1302
      0.1403
      0.6574
      0.9702
      0.8672
      0.1938
      0.7085
      0.5087
      0.2775
      0.8773
   
   M = 
      0.0689    0.1302
      0.2862    0.1403
      0.4171    0.6574
      0.0239    0.9702
      0.8504    0.8672
      0.4080    0.1938
      0.3262    0.7085
      0.2362    0.5087
      0.2018    0.2775
      0.7335    0.8773
   


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
      0.7625    0.4277    0.9743    0.5204
   
   R2 = 
      0.9034    0.2376    0.7206    0.9900
   
   M = 
      0.7625    0.4277    0.9743    0.5204
      0.9034    0.2376    0.7206    0.9900
   
   C1 = 
      0.2603
      0.3397
      0.6808
      0.6876
      0.5609
      0.2580
      0.3920
      0.3048
      0.6514
      0.0151
   
   C2 = 
      0.6903
      0.1941
   
   C3 = 
      0.2603
      0.3397
      0.6808
      0.6876
      0.5609
      0.2580
      0.3920
      0.3048
      0.6514
      0.0151
      0.6903
      0.1941
   

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
   

