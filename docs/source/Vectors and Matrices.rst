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
      0.7946    0.0917    0.4935    0.7210    0.9448    0.4409    0.0229
   
   C = 
      0.9742
      0.9182
      0.6856
      0.6578
      0.5749
   
   M = 
      0.8052    0.5569    0.3678    0.0604    0.5591    0.7162    0.7017
      0.9260    0.2425    0.7107    0.7154    0.2320    0.0928    0.5662
      0.6097    0.0587    0.4400    0.5985    0.5205    0.6208    0.2685
      0.3728    0.4600    0.3311    0.5282    0.9193    0.1386    0.7091
      0.3721    0.0864    0.0168    0.1944    0.9802    0.8178    0.3991
      0.7668    0.6844    0.3463    0.9832    0.2806    0.6868    0.4549
      0.3542    0.4742    0.1607    0.8268    0.2644    0.4123    0.9549
      0.5223    0.9573    0.8560    0.5528    0.6800    0.7441    0.1607
   

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
      0.1207    0.6848    0.5516    0.0310
   
   R2 = 
      0.2489    0.0581    0.3552    0.7580    0.8480
   
   R3 = 
      0.1207    0.6848    0.5516    0.0310    0.2489    0.0581    0.3552    0.7580    0.8480
   
   C1 = 
      0.8912
      0.1146
      0.0146
      0.0318
      0.0322
      0.5939
      0.4902
      0.4426
      0.5310
      0.5172
   
   C2 = 
      0.4287
      0.9496
      0.1246
      0.8940
      0.6969
      0.0440
      0.3067
      0.2887
      0.5604
      0.7737
   
   M = 
      0.8912    0.4287
      0.1146    0.9496
      0.0146    0.1246
      0.0318    0.8940
      0.0322    0.6969
      0.5939    0.0440
      0.4902    0.3067
      0.4426    0.2887
      0.5310    0.5604
      0.5172    0.7737
   


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
      0.6574    0.4014    0.1729    0.3480
   
   R2 = 
      0.6926    0.0864    0.6233    0.6303
   
   M = 
      0.6574    0.4014    0.1729    0.3480
      0.6926    0.0864    0.6233    0.6303
   
   C1 = 
      0.2864
      0.4264
      0.4592
      0.6987
      0.5212
      0.9920
      0.4045
      0.1066
      0.9493
      0.1101
   
   C2 = 
      0.3521
      0.7934
   
   C3 = 
      0.2864
      0.4264
      0.4592
      0.6987
      0.5212
      0.9920
      0.4045
      0.1066
      0.9493
      0.1101
      0.3521
      0.7934
   

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
   

