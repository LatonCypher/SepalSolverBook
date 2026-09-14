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
      0.7253    0.6896    0.7176    0.7058    0.3158    0.8204    0.5126
   
   C = 
      0.4057
      0.7329
      0.4288
      0.6445
      0.5089
   
   M = 
      0.9415    0.8502    0.8534    0.8084    0.7240    0.2289    0.4946
      0.2512    0.0970    0.4165    0.4559    0.5894    0.8896    0.6144
      0.5566    0.0889    0.8463    0.4387    0.9730    0.0986    0.9721
      0.6280    0.3000    0.6596    0.3376    0.8951    0.3412    0.6945
      0.5419    0.4267    0.7114    0.5395    0.0212    0.8011    0.4802
      0.9335    0.0541    0.4318    0.8158    0.4931    0.9989    0.3657
      0.1507    0.5930    0.3743    0.1493    0.5348    0.1133    0.3063
      0.2983    0.1286    0.8791    0.7449    0.7546    0.4371    0.8109
   

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
      0.5218    0.7013    0.8537    0.8103
   
   R2 = 
      0.2795    0.6936    0.5452    0.5084    0.7332
   
   R3 = 
      0.5218    0.7013    0.8537    0.8103    0.2795    0.6936    0.5452    0.5084    0.7332
   
   C1 = 
      0.3024
      0.2730
      0.5465
      0.7205
      0.3883
      0.6777
      0.1650
      0.9662
      0.1714
      0.7851
   
   C2 = 
      0.5899
      0.9343
      0.6918
      0.4391
      0.2026
      0.1720
      0.5380
      0.9661
      0.7673
      0.2491
   
   M = 
      0.3024    0.5899
      0.2730    0.9343
      0.5465    0.6918
      0.7205    0.4391
      0.3883    0.2026
      0.6777    0.1720
      0.1650    0.5380
      0.9662    0.9661
      0.1714    0.7673
      0.7851    0.2491
   


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
      0.2347    0.1094    0.6961    0.5788
   
   R2 = 
      0.0854    0.6563    0.8532    0.7534
   
   M = 
      0.2347    0.1094    0.6961    0.5788
      0.0854    0.6563    0.8532    0.7534
   
   C1 = 
      0.6598
      0.4792
      0.5625
      0.1657
      0.9119
      0.9511
      0.6207
      0.1112
      0.4036
      0.1495
   
   C2 = 
      0.0539
      0.1862
   
   C3 = 
      0.6598
      0.4792
      0.5625
      0.1657
      0.9119
      0.9511
      0.6207
      0.1112
      0.4036
      0.1495
      0.0539
      0.1862
   

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
   

