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
      0.2150    0.3562    0.3584    0.6164    0.0439    0.5094    0.0005
   
   C = 
      0.9388
      0.8801
      0.0808
      0.8755
      0.3984
   
   M = 
      0.4192    0.0849    0.9830    0.9555    0.0528    0.7336    0.2654
      0.2711    0.0819    0.0086    0.4057    0.1940    0.6870    0.7210
      0.8723    0.1515    0.8428    0.6071    0.7151    0.3438    0.9199
      0.5868    0.2060    0.9396    0.1314    0.1196    0.5453    0.1822
      0.6825    0.3831    0.5307    0.9404    0.4343    0.1107    0.1059
      0.1895    0.3022    0.4665    0.8785    0.1650    0.7456    0.8591
      0.7368    0.7342    0.4096    0.2774    0.3444    0.0975    0.2348
      0.1436    0.8354    0.7519    0.5155    0.9788    0.9076    0.9563
   

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
      0.5230    0.7363    0.2519    0.1545
   
   R2 = 
      0.1588    0.2657    0.8657    0.4615    0.8854
   
   R3 = 
      0.5230    0.7363    0.2519    0.1545    0.1588    0.2657    0.8657    0.4615    0.8854
   
   C1 = 
      0.4724
      0.2095
      0.1678
      0.6478
      0.8934
      0.9933
      0.9313
      0.4465
      0.4371
      0.2478
   
   C2 = 
      0.8987
      0.0044
      0.8020
      0.1863
      0.2603
      0.6602
      0.8222
      0.7034
      0.0651
      0.0551
   
   M = 
      0.4724    0.8987
      0.2095    0.0044
      0.1678    0.8020
      0.6478    0.1863
      0.8934    0.2603
      0.9933    0.6602
      0.9313    0.8222
      0.4465    0.7034
      0.4371    0.0651
      0.2478    0.0551
   


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
      0.8010    0.8791    0.5501    0.8019
   
   R2 = 
      0.7870    0.1674    0.8985    0.1022
   
   M = 
      0.8010    0.8791    0.5501    0.8019
      0.7870    0.1674    0.8985    0.1022
   
   C1 = 
      0.7029
      0.8217
      0.6390
      0.2166
      0.0452
      0.7298
      0.1706
      0.7128
      0.9259
      0.1516
   
   C2 = 
      0.6035
      0.2158
   
   C3 = 
      0.7029
      0.8217
      0.6390
      0.2166
      0.0452
      0.7298
      0.1706
      0.7128
      0.9259
      0.1516
      0.6035
      0.2158
   

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
   

