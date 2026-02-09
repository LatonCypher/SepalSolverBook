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
      0.3592    0.7224    0.0217    0.4217    0.7108    0.9650    0.1893
   
   C = 
      0.4206
      0.9698
      0.9211
      0.8360
      0.6338
   
   M = 
      0.7548    0.4013    0.9533    0.4056    0.1320    0.5636    0.5410
      0.6277    0.8442    0.3616    0.6781    0.5428    0.0765    0.1025
      0.1323    0.3358    0.1412    0.3207    0.7348    0.8914    0.1792
      0.8727    0.6851    0.8819    0.5481    0.9600    0.2875    0.6092
      0.6932    0.0819    0.3947    0.5149    0.7234    0.1952    0.2897
      0.8832    0.5418    0.3902    0.8152    0.9399    0.0666    0.8958
      0.8102    0.1081    0.2058    0.0326    0.9508    0.0041    0.7866
      0.7274    0.4215    0.3305    0.1177    0.6820    0.8386    0.7439
   

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
      0.6867    0.1716    0.4647    0.3855
   
   R2 = 
      0.2533    0.0422    0.2550    0.1255    0.6290
   
   R3 = 
      0.6867    0.1716    0.4647    0.3855    0.2533    0.0422    0.2550    0.1255    0.6290
   
   C1 = 
      0.8414
      0.4598
      0.3194
      0.2205
      0.1621
      0.8700
      0.6698
      0.9531
      0.5731
      0.2649
   
   C2 = 
      0.2849
      0.3717
      0.4746
      0.7529
      0.9904
      0.8643
      0.0564
      0.7198
      0.7745
      0.9972
   
   M = 
      0.8414    0.2849
      0.4598    0.3717
      0.3194    0.4746
      0.2205    0.7529
      0.1621    0.9904
      0.8700    0.8643
      0.6698    0.0564
      0.9531    0.7198
      0.5731    0.7745
      0.2649    0.9972
   


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
      0.7893    0.4983    0.4580    0.5155
   
   R2 = 
      0.9456    0.1949    0.3905    0.7890
   
   M = 
      0.7893    0.4983    0.4580    0.5155
      0.9456    0.1949    0.3905    0.7890
   
   C1 = 
      0.2795
      0.8944
      0.2011
      0.2065
      0.3928
      0.4216
      0.0234
      0.3802
      0.4583
      0.7275
   
   C2 = 
      0.5001
      0.8688
   
   C3 = 
      0.2795
      0.8944
      0.2011
      0.2065
      0.3928
      0.4216
      0.0234
      0.3802
      0.4583
      0.7275
      0.5001
      0.8688
   

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
   

