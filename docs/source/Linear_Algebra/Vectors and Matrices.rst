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
      0.5631    0.8860    0.5334    0.8497    0.3014    0.9599    0.1363
   
   C = 
      0.8853
      0.3021
      0.9121
      0.3306
      0.3799
   
   M = 
      0.4098    0.0399    0.5303    0.0563    0.9860    0.4422    0.1127
      0.6473    0.4923    0.9553    0.2358    0.4494    0.9075    0.6372
      0.2517    0.6087    0.7475    0.9537    0.4968    0.0286    0.0586
      0.5561    0.8677    0.5422    0.5085    0.4502    0.1338    0.6783
      0.5685    0.1985    0.9049    0.2649    0.4940    0.2404    0.8201
      0.5359    0.4885    0.0482    0.3687    0.8046    0.5477    0.8097
      0.3638    0.5891    0.1786    0.5595    0.2892    0.0747    0.3994
      0.4310    0.0315    0.6459    0.8946    0.1265    0.9721    0.4775
   

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
      0.5951    0.9071    0.9705    0.4421
   
   R2 = 
      0.5633    0.8993    0.4613    0.4120    0.7431
   
   R3 = 
      0.5951    0.9071    0.9705    0.4421    0.5633    0.8993    0.4613    0.4120    0.7431
   
   C1 = 
      0.3688
      0.7347
      0.2154
      0.6124
      0.4374
      0.7238
      0.7425
      0.1060
      0.2622
      0.1107
   
   C2 = 
      0.1641
      0.3440
      0.5340
      0.3889
      0.8178
      0.2279
      0.4490
      0.4135
      0.2687
      0.6236
   
   M = 
      0.3688    0.1641
      0.7347    0.3440
      0.2154    0.5340
      0.6124    0.3889
      0.4374    0.8178
      0.7238    0.2279
      0.7425    0.4490
      0.1060    0.4135
      0.2622    0.2687
      0.1107    0.6236
   


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
      0.2490    0.8710    0.4955    0.4399
   
   R2 = 
      0.1107    0.9967    0.4210    0.1539
   
   M = 
      0.2490    0.8710    0.4955    0.4399
      0.1107    0.9967    0.4210    0.1539
   
   C1 = 
      0.0076
      0.0122
      0.4228
      0.4478
      0.9400
      0.2438
      0.7088
      0.4797
      0.2498
      0.4626
   
   C2 = 
      0.0910
      0.6797
   
   C3 = 
      0.0076
      0.0122
      0.4228
      0.4478
      0.9400
      0.2438
      0.7088
      0.4797
      0.2498
      0.4626
      0.0910
      0.6797
   

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
   

