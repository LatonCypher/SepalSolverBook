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
      0.4239    0.8934    0.9061    0.6914    0.2542    0.6352    0.6089
   
   C = 
      0.0446
      0.2015
      0.1413
      0.2437
      0.1673
   
   M = 
      0.2238    0.3973    0.4669    0.3127    0.6806    0.1163    0.5109
      0.6401    0.9543    0.6631    0.2942    0.8786    0.4868    0.8207
      0.3142    0.2787    0.7917    0.3849    0.7329    0.2756    0.5020
      0.6005    0.0211    0.7728    0.3106    0.0010    0.5743    0.8806
      0.3927    0.8673    0.0993    0.4482    0.2720    0.9767    0.2681
      0.2601    0.4314    0.1237    0.6996    0.8339    0.1165    0.5728
      0.2934    0.2053    0.1917    0.1485    0.7084    0.8946    0.5732
      0.7988    0.4327    0.1477    0.5384    0.1286    0.2413    0.2159
   

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
      0.2233    0.3214    0.8445    0.3719
   
   R2 = 
      0.7657    0.1095    0.2985    0.4808    0.0868
   
   R3 = 
      0.2233    0.3214    0.8445    0.3719    0.7657    0.1095    0.2985    0.4808    0.0868
   
   C1 = 
      0.4852
      0.4963
      0.6452
      0.7028
      0.5330
      0.2552
      0.1785
      0.6916
      0.3585
      0.8931
   
   C2 = 
      0.9976
      0.8796
      0.5505
      0.8356
      0.2304
      0.4393
      0.2509
      0.7795
      0.6519
      0.2050
   
   M = 
      0.4852    0.9976
      0.4963    0.8796
      0.6452    0.5505
      0.7028    0.8356
      0.5330    0.2304
      0.2552    0.4393
      0.1785    0.2509
      0.6916    0.7795
      0.3585    0.6519
      0.8931    0.2050
   


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
      0.6386    0.2283    0.8480    0.7890
   
   R2 = 
      0.8422    0.9400    0.5130    0.4119
   
   M = 
      0.6386    0.2283    0.8480    0.7890
      0.8422    0.9400    0.5130    0.4119
   
   C1 = 
      0.1810
      0.0878
      0.1439
      0.7644
      0.0549
      0.5428
      0.5273
      0.0459
      0.9564
      0.6089
   
   C2 = 
      0.5052
      0.3073
   
   C3 = 
      0.1810
      0.0878
      0.1439
      0.7644
      0.0549
      0.5428
      0.5273
      0.0459
      0.9564
      0.6089
      0.5052
      0.3073
   

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
   

