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
      0.7340    0.4561    0.9371    0.3868    0.7091    0.2976    0.0271
   
   C = 
      0.1179
      0.4281
      0.1486
      0.4033
      0.8627
   
   M = 
      0.8582    0.2656    0.2011    0.2179    0.4824    0.6762    0.4872
      0.9010    0.3510    0.2032    0.2352    0.4000    0.5043    0.9363
      0.5026    0.2100    0.9835    0.2196    0.7199    0.8725    0.4979
      0.9404    0.6938    0.7142    0.9969    0.1364    0.0340    0.1676
      0.5080    0.3924    0.9032    0.8517    0.2813    0.9598    0.3822
      0.5372    0.0811    0.2092    0.7679    0.3316    0.6008    0.2199
      0.6437    0.9529    0.5081    0.4056    0.6470    0.8255    0.7908
      0.5226    0.7253    0.0749    0.0582    0.6861    0.7645    0.1734
   

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
      0.0387    0.0461    0.2111    0.3096
   
   R2 = 
      0.9227    0.9098    0.3912    0.4235    0.9994
   
   R3 = 
      0.0387    0.0461    0.2111    0.3096    0.9227    0.9098    0.3912    0.4235    0.9994
   
   C1 = 
      0.9462
      0.2415
      0.1491
      0.2523
      0.6439
      0.4201
      0.0954
      0.6234
      0.6416
      0.9978
   
   C2 = 
      0.5176
      0.1266
      0.6129
      0.4893
      0.0632
      0.5382
      0.6058
      0.9103
      0.3947
      0.6244
   
   M = 
      0.9462    0.5176
      0.2415    0.1266
      0.1491    0.6129
      0.2523    0.4893
      0.6439    0.0632
      0.4201    0.5382
      0.0954    0.6058
      0.6234    0.9103
      0.6416    0.3947
      0.9978    0.6244
   


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
      0.3407    0.8156    0.0987    0.0711
   
   R2 = 
      0.5484    0.9849    0.3642    0.8135
   
   M = 
      0.3407    0.8156    0.0987    0.0711
      0.5484    0.9849    0.3642    0.8135
   
   C1 = 
      0.3095
      0.0369
      0.6538
      0.3644
      0.1155
      0.9441
      0.8619
      0.3276
      0.0269
      0.4042
   
   C2 = 
      0.9778
      0.6609
   
   C3 = 
      0.3095
      0.0369
      0.6538
      0.3644
      0.1155
      0.9441
      0.8619
      0.3276
      0.0269
      0.4042
      0.9778
      0.6609
   

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
   

