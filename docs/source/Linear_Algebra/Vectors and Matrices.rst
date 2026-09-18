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
      0.9808    0.7952    0.8511    0.1682    0.5131    0.0558    0.3313
   
   C = 
      0.8201
      0.5920
      0.4292
      0.5425
      0.0304
   
   M = 
      0.1205    0.3406    0.2363    0.4642    0.7349    0.8600    0.1598
      0.2805    0.2579    0.0782    0.7818    0.1188    0.7211    0.9215
      0.0325    0.7118    0.8421    0.7695    0.3550    0.3303    0.1100
      0.3064    0.1979    0.5758    0.6467    0.2174    0.3591    0.5115
      0.4800    0.1664    0.9291    0.8803    0.5649    0.5184    0.2609
      0.4292    0.4987    0.4350    0.0175    0.4797    0.8509    0.4964
      0.0084    0.2875    0.7796    0.8405    0.8990    0.5141    0.6141
      0.6394    0.5131    0.4638    0.4938    0.9475    0.2088    0.5281
   

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
      0.4686    0.6101    0.6152    0.4226
   
   R2 = 
      0.9496    0.5528    0.8496    0.2675    0.9965
   
   R3 = 
      0.4686    0.6101    0.6152    0.4226    0.9496    0.5528    0.8496    0.2675    0.9965
   
   C1 = 
      0.3492
      0.4748
      0.4186
      0.3202
      0.2662
      0.8931
      0.8872
      0.8294
      0.4730
      0.9840
   
   C2 = 
      0.3162
      0.9010
      0.2413
      0.9743
      0.9804
      0.8179
      0.3290
      0.9311
      0.8523
      0.0223
   
   M = 
      0.3492    0.3162
      0.4748    0.9010
      0.4186    0.2413
      0.3202    0.9743
      0.2662    0.9804
      0.8931    0.8179
      0.8872    0.3290
      0.8294    0.9311
      0.4730    0.8523
      0.9840    0.0223
   


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
      0.4877    0.8546    0.1557    0.6704
   
   R2 = 
      0.4759    0.9904    0.9491    0.8504
   
   M = 
      0.4877    0.8546    0.1557    0.6704
      0.4759    0.9904    0.9491    0.8504
   
   C1 = 
      0.7749
      0.0003
      0.0620
      0.3221
      0.5562
      0.8487
      0.6097
      0.9611
      0.5246
      0.3860
   
   C2 = 
      0.4977
      0.7987
   
   C3 = 
      0.7749
      0.0003
      0.0620
      0.3221
      0.5562
      0.8487
      0.6097
      0.9611
      0.5246
      0.3860
      0.4977
      0.7987
   

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
   

