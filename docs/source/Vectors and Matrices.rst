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
      0.0426    0.7529    0.4089    0.8623    0.0680    0.7499    0.3502
   
   C = 
      0.6787
      0.3242
      0.5862
      0.3196
      0.7294
   
   M = 
      0.8120    0.4533    0.7822    0.3944    0.7232    0.8249    0.7830
      0.4254    0.1832    0.7133    0.6029    0.0934    0.2461    0.7703
      0.1941    0.0589    0.7154    0.8660    0.6060    0.6105    0.5354
      0.4866    0.2518    0.0542    0.1234    0.2889    0.1606    0.9441
      0.5012    0.0147    0.8839    0.3913    0.2914    0.5018    0.0186
      0.6418    0.5784    0.3834    0.5869    0.8040    0.8086    0.3842
      0.8771    0.9248    0.7576    0.0055    0.7772    0.9732    0.3719
      0.3316    0.6008    0.7168    0.0129    0.4439    0.5852    0.6496
   

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
      0.7301    0.2105    0.7184    0.0706
   
   R2 = 
      0.2283    0.2838    0.2281    0.7471    0.0158
   
   R3 = 
      0.7301    0.2105    0.7184    0.0706    0.2283    0.2838    0.2281    0.7471    0.0158
   
   C1 = 
      0.6468
      0.1288
      0.8417
      0.4017
      0.1739
      0.5475
      0.2114
      0.1194
      0.2309
      0.1106
   
   C2 = 
      0.8083
      0.5491
      0.2625
      0.5042
      0.4223
      0.4834
      0.2211
      0.9570
      0.9252
      0.4556
   
   M = 
      0.6468    0.8083
      0.1288    0.5491
      0.8417    0.2625
      0.4017    0.5042
      0.1739    0.4223
      0.5475    0.4834
      0.2114    0.2211
      0.1194    0.9570
      0.2309    0.9252
      0.1106    0.4556
   


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
      0.5915    0.5021    0.4880    0.4796
   
   R2 = 
      0.6650    0.7882    0.3451    0.2312
   
   M = 
      0.5915    0.5021    0.4880    0.4796
      0.6650    0.7882    0.3451    0.2312
   
   C1 = 
      0.1583
      0.1942
      0.8452
      0.0193
      0.8251
      0.1413
      0.5986
      0.1672
      0.1981
      0.9027
   
   C2 = 
      0.8927
      0.6341
   
   C3 = 
      0.1583
      0.1942
      0.8452
      0.0193
      0.8251
      0.1413
      0.5986
      0.1672
      0.1981
      0.9027
      0.8927
      0.6341
   

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
   

