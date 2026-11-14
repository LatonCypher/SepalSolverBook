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
      0.0245    0.0493    0.1733    0.1183    0.0049    0.6468    0.1936
   
   C = 
      0.4333
      0.8125
      0.0160
      0.3355
      0.1758
   
   M = 
      0.6812    0.3701    0.3870    0.8678    0.5741    0.9258    0.7724
      0.5658    0.4940    0.4741    0.1064    0.6473    0.1679    0.0041
      0.5791    0.8305    0.2418    0.2245    0.2234    0.2705    0.5158
      0.2962    0.6989    0.6271    0.9773    0.3580    0.5835    0.5587
      0.3883    0.9634    0.1645    0.8242    0.2468    0.8627    0.1459
      0.1411    0.0871    0.2894    0.5167    0.2316    0.2714    0.5466
      0.1335    0.6658    0.9320    0.6446    0.1448    0.9824    0.0986
      0.4101    0.7528    0.4856    0.7590    0.4941    0.7765    0.1127
   

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
      0.6176    0.2271    0.2450    0.4156
   
   R2 = 
      0.8587    0.9237    0.2809    0.8007    0.3987
   
   R3 = 
      0.6176    0.2271    0.2450    0.4156    0.8587    0.9237    0.2809    0.8007    0.3987
   
   C1 = 
      0.9500
      0.6747
      0.8989
      0.5547
      0.6788
      0.8830
      0.2803
      0.1409
      0.2076
      0.4118
   
   C2 = 
      0.7028
      0.6169
      0.9420
      0.6207
      0.4988
      0.3663
      0.9406
      0.6773
      0.9718
      0.9586
   
   M = 
      0.9500    0.7028
      0.6747    0.6169
      0.8989    0.9420
      0.5547    0.6207
      0.6788    0.4988
      0.8830    0.3663
      0.2803    0.9406
      0.1409    0.6773
      0.2076    0.9718
      0.4118    0.9586
   


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
      0.8007    0.4916    0.4552    0.5165
   
   R2 = 
      0.7241    0.4223    0.9832    0.7107
   
   M = 
      0.8007    0.4916    0.4552    0.5165
      0.7241    0.4223    0.9832    0.7107
   
   C1 = 
      0.3402
      0.1312
      0.2841
      0.2114
      0.6633
      0.0534
      0.1635
      0.9916
      0.4571
      0.1543
   
   C2 = 
      0.7551
      0.8228
   
   C3 = 
      0.3402
      0.1312
      0.2841
      0.2114
      0.6633
      0.0534
      0.1635
      0.9916
      0.4571
      0.1543
      0.7551
      0.8228
   

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
   

