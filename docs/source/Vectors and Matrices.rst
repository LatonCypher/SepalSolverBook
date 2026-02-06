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
      0.0437    0.5865    0.2210    0.3777    0.9270    0.3193    0.2987
   
   C = 
      0.5935
      0.2301
      0.5057
      0.5929
      0.5418
   
   M = 
      0.2285    0.5729    0.3051    0.6449    0.2995    0.4620    0.4058
      0.0172    0.6279    0.5894    0.2750    0.3099    0.7566    0.6821
      0.2145    0.7173    0.9633    0.6957    0.5785    0.2164    0.9672
      0.7770    0.8676    0.9412    0.5779    0.2778    0.3535    0.8759
      0.5593    0.9090    0.5773    0.4811    0.0527    0.3203    0.4581
      0.9094    0.6406    0.2780    0.2588    0.0308    0.8345    0.3732
      0.1298    0.2414    0.9857    0.9303    0.0198    0.6096    0.9315
      0.9825    0.5679    0.5382    0.8538    0.6792    0.2598    0.3678
   

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
      0.2239    0.0660    0.4705    0.3867
   
   R2 = 
      0.0713    0.8776    0.1849    0.4561    0.7279
   
   R3 = 
      0.2239    0.0660    0.4705    0.3867    0.0713    0.8776    0.1849    0.4561    0.7279
   
   C1 = 
      0.0317
      0.4845
      0.8518
      0.7415
      0.4449
      0.6523
      0.4765
      0.6725
      0.6244
      0.8524
   
   C2 = 
      0.9559
      0.8609
      0.9345
      0.9812
      0.5897
      0.1416
      0.7822
      0.0546
      0.3914
      0.9577
   
   M = 
      0.0317    0.9559
      0.4845    0.8609
      0.8518    0.9345
      0.7415    0.9812
      0.4449    0.5897
      0.6523    0.1416
      0.4765    0.7822
      0.6725    0.0546
      0.6244    0.3914
      0.8524    0.9577
   


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
      0.5524    0.0891    0.2086    0.2695
   
   R2 = 
      0.5530    0.8425    0.4904    0.3948
   
   M = 
      0.5524    0.0891    0.2086    0.2695
      0.5530    0.8425    0.4904    0.3948
   
   C1 = 
      0.5814
      0.3060
      0.3607
      0.0924
      0.3493
      0.1187
      0.8517
      0.0102
      0.3623
      0.6103
   
   C2 = 
      0.0769
      0.2574
   
   C3 = 
      0.5814
      0.3060
      0.3607
      0.0924
      0.3493
      0.1187
      0.8517
      0.0102
      0.3623
      0.6103
      0.0769
      0.2574
   

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
   

