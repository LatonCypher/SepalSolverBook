Vectors and Matrices
====================

Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: RowVec, ColVec and Matrix. RowVec and ColVec are 1D arrays while Matrix is a 2D array. 

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
      5.0000    6.0000    7.0000    1.0000
   
   C = 
      8.0000
      3.0000
      4.0000
      2.0000
      7.0000
   
   M = 
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   


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
      0.3158    0.4508    0.1373    0.0220    0.8602    0.2528    0.1683
   
   C = 
      0.5226
      0.3994
      0.3194
      0.4602
      0.7131
   
   M = 
      0.1188    0.9063    0.2891    0.4783    0.8532    0.2246    0.2136
      0.7370    0.1936    0.3622    0.9565    0.6297    0.4901    0.8616
      0.8435    0.5429    0.9133    0.0202    0.5352    0.3153    0.6041
      0.4386    0.6609    0.7062    0.7509    0.6395    0.9207    0.4222
      0.8127    0.2504    0.1052    0.9912    0.9290    0.0417    0.1574
      0.3087    0.8905    0.2183    0.4789    0.6882    0.4412    0.8948
      0.4368    0.6916    0.7731    0.5361    0.2311    0.7061    0.7711
      0.0253    0.0852    0.2736    0.5145    0.0477    0.8624    0.5412
   

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
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   
   C = 
      1.0000
      1.0000
      1.0000
      1.0000
      1.0000
   
   M = 
      1.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    1.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    1.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    1.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    1.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    1.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    1.0000
   

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
      0.4062    0.4671    0.3104    0.1491
   
   R2 = 
      0.1296    0.2859    0.9442    0.7916    0.6396
   
   R3 = 
      0.4062    0.4671    0.3104    0.1491    0.1296    0.2859    0.9442    0.7916    0.6396
   
   C1 = 
      0.5233
      0.8265
      0.2991
      0.8889
      0.0549
      0.7319
      0.9308
      0.7527
      0.4890
      0.9449
   
   C2 = 
      0.1631
      0.4807
      0.3896
      0.9259
      0.7077
      0.1861
      0.7806
      0.7130
      0.0988
      0.5510
   
   M = 
      0.5233    0.1631
      0.8265    0.4807
      0.2991    0.3896
      0.8889    0.9259
      0.0549    0.7077
      0.7319    0.1861
      0.9308    0.7806
      0.7527    0.7130
      0.4890    0.0988
      0.9449    0.5510
   


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
      0.3684    0.1535    0.9409    0.0958
   
   R2 = 
      0.8720    0.8931    0.2932    0.8399
   
   M = 
      0.3684    0.1535    0.9409    0.0958
      0.8720    0.8931    0.2932    0.8399
   
   C1 = 
      0.9428
      0.8287
      0.2650
      0.4042
      0.4496
      0.6013
      0.7049
      0.1368
      0.5796
      0.5104
   
   C2 = 
      0.2937
      0.9504
   
   C3 = 
      0.9428
      0.8287
      0.2650
      0.4042
      0.4496
      0.6013
      0.7049
      0.1368
      0.5796
      0.5104
      0.2937
      0.9504
   

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
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   
   Flipud(M) = 
      0.0000    5.0000   -6.0000   -3.0000
      4.0000    8.0000    9.0000    1.0000
      2.0000    1.0000   -7.0000    3.0000
      5.0000   -2.0000    3.0000    7.0000
   
   Fliplr(M) = 
      7.0000    3.0000   -2.0000    5.0000
      3.0000   -7.0000    1.0000    2.0000
      1.0000    9.0000    8.0000    4.0000
     -3.0000   -6.0000    5.0000    0.0000
   

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
      5.0000   -2.0000    3.0000    7.0000
      0.0000    1.0000   -7.0000    3.0000
      0.0000    0.0000    9.0000    1.0000
      0.0000    0.0000    0.0000   -3.0000
   
   Tril(M) = 
      5.0000    0.0000    0.0000    0.0000
      2.0000    1.0000    0.0000    0.0000
      4.0000    8.0000    9.0000    0.0000
      0.0000    5.0000   -6.0000   -3.0000
   

