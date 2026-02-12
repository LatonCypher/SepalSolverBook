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
      0.8210    0.5282    0.3765    0.9870    0.9652    0.6908    0.5176
   
   C = 
      0.1745
      0.0437
      0.9217
      0.9146
      0.3319
   
   M = 
      0.0231    0.4189    0.0784    0.7494    0.9363    0.3391    0.5341
      0.7595    0.0487    0.6395    0.0995    0.2829    0.5380    0.8768
      0.2328    0.0767    0.2119    0.0773    0.2556    0.8717    0.0655
      0.0099    0.9339    0.1197    0.5908    0.5471    0.0968    0.5473
      0.1576    0.8088    0.3848    0.9430    0.4836    0.2344    0.6150
      0.4239    0.5451    0.9194    0.8875    0.7402    0.4932    0.1966
      0.5949    0.7514    0.4186    0.8729    0.0994    0.0203    0.5514
      0.9844    0.3840    0.7879    0.9068    0.8399    0.0238    0.4928
   

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
      0.0012    0.0944    0.1732    0.6376
   
   R2 = 
      0.8079    0.1877    0.1020    0.0745    0.6565
   
   R3 = 
      0.0012    0.0944    0.1732    0.6376    0.8079    0.1877    0.1020    0.0745    0.6565
   
   C1 = 
      0.0302
      0.8157
      0.9065
      0.8590
      0.8479
      0.6445
      0.3476
      0.4273
      0.4830
      0.6318
   
   C2 = 
      0.8715
      0.8746
      0.3182
      0.3302
      0.1823
      0.0544
      0.1308
      0.4162
      0.3561
      0.2000
   
   M = 
      0.0302    0.8715
      0.8157    0.8746
      0.9065    0.3182
      0.8590    0.3302
      0.8479    0.1823
      0.6445    0.0544
      0.3476    0.1308
      0.4273    0.4162
      0.4830    0.3561
      0.6318    0.2000
   


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
      0.0765    0.0051    0.8328    0.4826
   
   R2 = 
      0.0187    0.8656    0.1587    0.0778
   
   M = 
      0.0765    0.0051    0.8328    0.4826
      0.0187    0.8656    0.1587    0.0778
   
   C1 = 
      0.5744
      0.0864
      0.2484
      0.2146
      0.3752
      0.2825
      0.2878
      0.1423
      0.4122
      0.9486
   
   C2 = 
      0.0967
      0.1334
   
   C3 = 
      0.5744
      0.0864
      0.2484
      0.2146
      0.3752
      0.2825
      0.2878
      0.1423
      0.4122
      0.9486
      0.0967
      0.1334
   

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
   

