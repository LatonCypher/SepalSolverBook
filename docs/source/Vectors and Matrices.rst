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
      0.5940    0.2759    0.9308    0.1180    0.6608    0.4665    0.0060
   
   C = 
      0.7657
      0.4389
      0.0892
      0.9326
      0.2607
   
   M = 
      0.1017    0.8399    0.4582    0.7983    0.7452    0.9808    0.9049
      0.4136    0.5604    0.7414    0.9060    0.9026    0.6152    0.7086
      0.4978    0.1656    0.6609    0.5215    0.0338    0.7055    0.7379
      0.4696    0.1401    0.4031    0.4616    0.4012    0.4336    0.4665
      0.0615    0.4439    0.0356    0.4570    0.9267    0.3445    0.3820
      0.2343    0.0714    0.6021    0.0082    0.9035    0.5827    0.1757
      0.9025    0.2740    0.2908    0.5009    0.1128    0.8520    0.6547
      0.0633    0.3000    0.3544    0.1259    0.4838    0.6394    0.5250
   

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
      0.3709    0.1844    0.8633    0.0162
   
   R2 = 
      0.7978    0.7232    0.3032    0.3552    0.8640
   
   R3 = 
      0.3709    0.1844    0.8633    0.0162    0.7978    0.7232    0.3032    0.3552    0.8640
   
   C1 = 
      0.4883
      0.4187
      0.3765
      0.5925
      0.5575
      0.5509
      0.3445
      0.4258
      0.2423
      0.4750
   
   C2 = 
      0.0233
      0.5729
      0.7685
      0.3466
      0.6541
      0.8299
      0.2029
      0.5603
      0.6313
      0.3657
   
   M = 
      0.4883    0.0233
      0.4187    0.5729
      0.3765    0.7685
      0.5925    0.3466
      0.5575    0.6541
      0.5509    0.8299
      0.3445    0.2029
      0.4258    0.5603
      0.2423    0.6313
      0.4750    0.3657
   


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
      0.2041    0.4339    0.7457    0.8312
   
   R2 = 
      0.9862    0.0075    0.6408    0.4663
   
   M = 
      0.2041    0.4339    0.7457    0.8312
      0.9862    0.0075    0.6408    0.4663
   
   C1 = 
      0.0485
      0.4041
      0.6526
      0.4927
      0.6203
      0.0226
      0.5736
      0.0612
      0.7216
      0.1090
   
   C2 = 
      0.9758
      0.7423
   
   C3 = 
      0.0485
      0.4041
      0.6526
      0.4927
      0.6203
      0.0226
      0.5736
      0.0612
      0.7216
      0.1090
      0.9758
      0.7423
   

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
   

