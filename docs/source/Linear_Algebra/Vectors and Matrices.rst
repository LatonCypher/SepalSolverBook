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
      0.7431    0.1256    0.9622    0.1067    0.8097    0.3377    0.1848
   
   C = 
      0.6867
      0.4149
      0.1565
      0.1476
      0.0610
   
   M = 
      0.4852    0.5230    0.4255    0.1562    0.3095    0.8946    0.3274
      0.8992    0.1993    0.2843    0.2934    0.3616    0.0849    0.3936
      0.7470    0.9607    0.2654    0.6942    0.1519    0.1005    0.6631
      0.7938    0.5335    0.2353    0.7500    0.1645    0.0613    0.3399
      0.9132    0.3574    0.0329    0.3509    0.9906    0.4560    0.8961
      0.2429    0.4629    0.6096    0.2283    0.1276    0.2681    0.4312
      0.9066    0.7980    0.1458    0.8485    0.7010    0.7717    0.9062
      0.5585    0.7854    0.4899    0.4646    0.5532    0.8425    0.2466
   

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
      0.2818    0.2986    0.6450    0.3956
   
   R2 = 
      0.9103    0.4703    0.2194    0.8584    0.5069
   
   R3 = 
      0.2818    0.2986    0.6450    0.3956    0.9103    0.4703    0.2194    0.8584    0.5069
   
   C1 = 
      0.2542
      0.5886
      0.2436
      0.4829
      0.5577
      0.9703
      0.5935
      0.9691
      0.8224
      0.7373
   
   C2 = 
      0.1283
      0.2006
      0.7347
      0.7979
      0.3413
      0.0802
      0.2635
      0.8647
      0.0684
      0.8364
   
   M = 
      0.2542    0.1283
      0.5886    0.2006
      0.2436    0.7347
      0.4829    0.7979
      0.5577    0.3413
      0.9703    0.0802
      0.5935    0.2635
      0.9691    0.8647
      0.8224    0.0684
      0.7373    0.8364
   


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
      0.0933    0.2414    0.0289    0.5024
   
   R2 = 
      0.5441    0.5516    0.5712    0.5648
   
   M = 
      0.0933    0.2414    0.0289    0.5024
      0.5441    0.5516    0.5712    0.5648
   
   C1 = 
      0.1148
      0.9803
      0.9982
      0.4068
      0.2650
      0.1174
      0.1058
      0.0908
      0.8319
      0.6131
   
   C2 = 
      0.9052
      0.7282
   
   C3 = 
      0.1148
      0.9803
      0.9982
      0.4068
      0.2650
      0.1174
      0.1058
      0.0908
      0.8319
      0.6131
      0.9052
      0.7282
   

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
   

