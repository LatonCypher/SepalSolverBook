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
      0.2606    0.0536    0.9816    0.1308    0.3114    0.2899    0.2119
   
   C = 
      0.0231
      0.6344
      0.5551
      0.8802
      0.7014
   
   M = 
      0.6630    0.9699    0.1739    0.9246    0.7330    0.0795    0.8862
      0.2415    0.0854    0.4328    0.1580    0.6228    0.6314    0.1716
      0.1580    0.4775    0.1080    0.4931    0.7578    0.8264    0.5780
      0.0663    0.0262    0.7027    0.2983    0.5363    0.3768    0.1370
      0.7847    0.2263    0.5112    0.7056    0.9530    0.0699    0.5472
      0.1528    0.3614    0.2408    0.5055    0.3180    0.3663    0.8972
      0.4217    0.4092    0.1871    0.4253    0.4993    0.2830    0.4331
      0.8718    0.0733    0.5050    0.3595    0.6069    0.8398    0.1512
   

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
      0.4858    0.1985    0.8715    0.3347
   
   R2 = 
      0.6064    0.2926    0.2024    0.0139    0.3316
   
   R3 = 
      0.4858    0.1985    0.8715    0.3347    0.6064    0.2926    0.2024    0.0139    0.3316
   
   C1 = 
      0.4166
      0.2987
      0.5618
      0.2328
      0.3736
      0.4726
      0.9773
      0.8685
      0.8164
      0.8276
   
   C2 = 
      0.1653
      0.6538
      0.7290
      0.8443
      0.8556
      0.6489
      0.2439
      0.6563
      0.7750
      0.0674
   
   M = 
      0.4166    0.1653
      0.2987    0.6538
      0.5618    0.7290
      0.2328    0.8443
      0.3736    0.8556
      0.4726    0.6489
      0.9773    0.2439
      0.8685    0.6563
      0.8164    0.7750
      0.8276    0.0674
   


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
      0.5221    0.6530    0.8368    0.2504
   
   R2 = 
      0.9828    0.0021    0.7290    0.8697
   
   M = 
      0.5221    0.6530    0.8368    0.2504
      0.9828    0.0021    0.7290    0.8697
   
   C1 = 
      0.7826
      0.0260
      0.8433
      0.0796
      0.2233
      0.5336
      0.4825
      0.3071
      0.3612
      0.0039
   
   C2 = 
      0.4666
      0.1636
   
   C3 = 
      0.7826
      0.0260
      0.8433
      0.0796
      0.2233
      0.5336
      0.4825
      0.3071
      0.3612
      0.0039
      0.4666
      0.1636
   

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
   

