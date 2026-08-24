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
      0.2198    0.9123    0.0272    0.0876    0.5015    0.1205    0.5709
   
   C = 
      0.6064
      0.5669
      0.4946
      0.9004
      0.2832
   
   M = 
      0.6531    0.1013    0.7375    0.4397    0.8967    0.7696    0.8833
      0.2472    0.0737    0.7611    0.7839    0.9233    0.5094    0.4107
      0.4284    0.8086    0.5351    0.6064    0.5840    0.5736    0.6015
      0.5073    0.3798    0.3478    0.5495    0.4819    0.8716    0.5950
      0.4780    0.3150    0.4743    0.8325    0.8866    0.5326    0.7230
      0.2083    0.5083    0.7882    0.4869    0.7340    0.8734    0.5094
      0.4911    0.9552    0.1025    0.0547    0.9439    0.1199    0.9322
      0.3703    0.0770    0.5652    0.4036    0.6352    0.8637    0.0590
   

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
      0.8071    0.6490    0.0059    0.6060
   
   R2 = 
      0.4509    0.7079    0.3679    0.1684    0.0836
   
   R3 = 
      0.8071    0.6490    0.0059    0.6060    0.4509    0.7079    0.3679    0.1684    0.0836
   
   C1 = 
      0.8960
      0.2656
      0.3751
      0.4695
      0.3145
      0.5897
      0.5989
      0.2957
      0.5933
      0.7998
   
   C2 = 
      0.1675
      0.1759
      0.7203
      0.9725
      0.6617
      0.8275
      0.9691
      0.7784
      0.6732
      0.7378
   
   M = 
      0.8960    0.1675
      0.2656    0.1759
      0.3751    0.7203
      0.4695    0.9725
      0.3145    0.6617
      0.5897    0.8275
      0.5989    0.9691
      0.2957    0.7784
      0.5933    0.6732
      0.7998    0.7378
   


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
      0.4499    0.5328    0.3771    0.9774
   
   R2 = 
      0.1903    0.9446    0.3263    0.3623
   
   M = 
      0.4499    0.5328    0.3771    0.9774
      0.1903    0.9446    0.3263    0.3623
   
   C1 = 
      0.1345
      0.9181
      0.1225
      0.1260
      0.7118
      0.7338
      0.3459
      0.4773
      0.3000
      0.2363
   
   C2 = 
      0.7029
      0.7483
   
   C3 = 
      0.1345
      0.9181
      0.1225
      0.1260
      0.7118
      0.7338
      0.3459
      0.4773
      0.3000
      0.2363
      0.7029
      0.7483
   

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
   

