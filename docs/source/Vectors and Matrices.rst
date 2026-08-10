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
      0.6061    0.4232    0.2828    0.8240    0.2423    0.4274    0.4366
   
   C = 
      0.5366
      0.9410
      0.7842
      0.3800
      0.2304
   
   M = 
      0.9397    0.5832    0.3902    0.1056    0.2574    0.9973    0.6936
      0.5973    0.3251    0.9594    0.6489    0.1718    0.9180    0.9554
      0.3599    0.4340    0.0987    0.8451    0.2748    0.6254    0.4882
      0.7314    0.6769    0.2394    0.9702    0.6509    0.6747    0.4228
      0.3575    0.7147    0.9083    0.6733    0.8814    0.3995    0.6770
      0.8878    0.8757    0.2078    0.4546    0.4043    0.1812    0.5594
      0.2796    0.0206    0.3097    0.7640    0.2784    0.7415    0.1531
      0.6512    0.3032    0.0398    0.6264    0.0244    0.0226    0.9144
   

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
      0.8515    0.7263    0.9025    0.1339
   
   R2 = 
      0.4030    0.9852    0.7535    0.5424    0.8193
   
   R3 = 
      0.8515    0.7263    0.9025    0.1339    0.4030    0.9852    0.7535    0.5424    0.8193
   
   C1 = 
      0.4812
      0.0766
      0.8973
      0.4415
      0.3950
      0.8179
      0.3416
      0.2154
      0.1008
      0.2639
   
   C2 = 
      0.3248
      0.5804
      0.4123
      0.9157
      0.2953
      0.6842
      0.8756
      0.2168
      0.9366
      0.9644
   
   M = 
      0.4812    0.3248
      0.0766    0.5804
      0.8973    0.4123
      0.4415    0.9157
      0.3950    0.2953
      0.8179    0.6842
      0.3416    0.8756
      0.2154    0.2168
      0.1008    0.9366
      0.2639    0.9644
   


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
      0.5923    0.8627    0.5594    0.5986
   
   R2 = 
      0.9236    0.3543    0.3617    0.7362
   
   M = 
      0.5923    0.8627    0.5594    0.5986
      0.9236    0.3543    0.3617    0.7362
   
   C1 = 
      0.2061
      0.3017
      0.3806
      0.9906
      0.2005
      0.8350
      0.2780
      0.6642
      0.1039
      0.5890
   
   C2 = 
      0.0889
      0.4292
   
   C3 = 
      0.2061
      0.3017
      0.3806
      0.9906
      0.2005
      0.8350
      0.2780
      0.6642
      0.1039
      0.5890
      0.0889
      0.4292
   

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
   

