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
      0.9468    0.7726    0.3756    0.3067    0.8524    0.0879    0.9026
   
   C = 
      0.8543
      0.8626
      0.4665
      0.7512
      0.7977
   
   M = 
      0.1874    0.6010    0.5581    0.6031    0.7016    0.8151    0.5052
      0.7477    0.3406    0.9882    0.2703    0.5895    0.8814    0.8891
      0.9641    0.1242    0.3860    0.4516    0.5265    0.6053    0.7414
      0.2734    0.6599    0.2186    0.7009    0.3394    0.9039    0.7192
      0.4446    0.3041    0.8190    0.7320    0.4684    0.5303    0.1495
      0.3030    0.8702    0.0613    0.7988    0.5435    0.2880    0.4726
      0.5966    0.8502    0.4333    0.9948    0.9041    0.7624    0.9925
      0.2177    0.2176    0.3835    0.7951    0.5328    0.6329    0.9737
   

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
      0.9236    0.3143    0.8841    0.1915
   
   R2 = 
      0.6958    0.3925    0.8757    0.2859    0.7789
   
   R3 = 
      0.9236    0.3143    0.8841    0.1915    0.6958    0.3925    0.8757    0.2859    0.7789
   
   C1 = 
      0.1912
      0.3668
      0.2668
      0.6441
      0.8044
      0.9261
      0.1648
      0.9434
      0.4042
      0.7276
   
   C2 = 
      0.5816
      0.8726
      0.8970
      0.4492
      0.0436
      0.3431
      0.5495
      0.9244
      0.8605
      0.0211
   
   M = 
      0.1912    0.5816
      0.3668    0.8726
      0.2668    0.8970
      0.6441    0.4492
      0.8044    0.0436
      0.9261    0.3431
      0.1648    0.5495
      0.9434    0.9244
      0.4042    0.8605
      0.7276    0.0211
   


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
      0.7861    0.1332    0.0621    0.0450
   
   R2 = 
      0.0662    0.8602    0.2736    0.5057
   
   M = 
      0.7861    0.1332    0.0621    0.0450
      0.0662    0.8602    0.2736    0.5057
   
   C1 = 
      0.7721
      0.4316
      0.4497
      0.6712
      0.2542
      0.4703
      0.4625
      0.0648
      0.6419
      0.3819
   
   C2 = 
      0.4772
      0.3789
   
   C3 = 
      0.7721
      0.4316
      0.4497
      0.6712
      0.2542
      0.4703
      0.4625
      0.0648
      0.6419
      0.3819
      0.4772
      0.3789
   

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
   

