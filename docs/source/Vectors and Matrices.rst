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
      0.2614    0.8906    0.9497    0.1931    0.2457    0.7743    0.9137
   
   C = 
      0.7935
      0.7779
      0.3889
      0.9426
      0.2149
   
   M = 
      0.7041    0.7260    0.9531    0.9354    0.0969    0.8624    0.7415
      0.7484    0.9296    0.4486    0.3406    0.7491    0.0350    0.8651
      0.6824    0.5002    0.0252    0.5563    0.3963    0.7648    0.6094
      0.4391    0.0844    0.5258    0.4980    0.0409    0.3613    0.3975
      0.5144    0.0370    0.9811    0.8171    0.7302    0.0317    0.6079
      0.4795    0.9497    0.6493    0.3106    0.9056    0.9849    0.2162
      0.3519    0.2209    0.4585    0.6715    0.7749    0.6731    0.7138
      0.3082    0.4998    0.6649    0.4883    0.1333    0.5729    0.9176
   

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
      0.8529    0.3767    0.7276    0.2071
   
   R2 = 
      0.7773    0.3438    0.6181    0.4571    0.3453
   
   R3 = 
      0.8529    0.3767    0.7276    0.2071    0.7773    0.3438    0.6181    0.4571    0.3453
   
   C1 = 
      0.0986
      0.9465
      0.2520
      0.2170
      0.3592
      0.6618
      0.4702
      0.2524
      0.6996
      0.5994
   
   C2 = 
      0.7454
      0.8187
      0.0926
      0.1882
      0.5105
      0.0435
      0.0525
      0.9854
      0.1682
      0.0023
   
   M = 
      0.0986    0.7454
      0.9465    0.8187
      0.2520    0.0926
      0.2170    0.1882
      0.3592    0.5105
      0.6618    0.0435
      0.4702    0.0525
      0.2524    0.9854
      0.6996    0.1682
      0.5994    0.0023
   


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
      0.5767    0.6611    0.4310    0.9571
   
   R2 = 
      0.9875    0.8547    0.0103    0.3841
   
   M = 
      0.5767    0.6611    0.4310    0.9571
      0.9875    0.8547    0.0103    0.3841
   
   C1 = 
      0.2377
      0.4776
      0.0455
      0.9444
      0.8586
      0.7370
      0.5324
      0.6716
      0.3442
      0.0023
   
   C2 = 
      0.9307
      0.6004
   
   C3 = 
      0.2377
      0.4776
      0.0455
      0.9444
      0.8586
      0.7370
      0.5324
      0.6716
      0.3442
      0.0023
      0.9307
      0.6004
   

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
   

