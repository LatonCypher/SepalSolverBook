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
      0.5836    0.9510    0.1284    0.1980    0.1783    0.2753    0.5521
   
   C = 
      0.2942
      0.2542
      0.6674
      0.6168
      0.3284
   
   M = 
      0.9777    0.3232    0.6727    0.7213    0.8874    0.1942    0.0426
      0.4201    0.3466    0.6759    0.3583    0.4545    0.6824    0.2070
      0.9541    0.0612    0.8128    0.9424    0.0220    0.9553    0.3495
      0.9233    0.9971    0.0639    0.9163    0.8395    0.5583    0.3374
      0.7180    0.8732    0.4499    0.5504    0.9954    0.6701    0.6161
      0.2188    0.9794    0.5605    0.5528    0.0513    0.5075    0.9078
      0.4910    0.2345    0.4197    0.0725    0.4244    0.6149    0.1361
      0.7324    0.4194    0.3059    0.5610    0.7719    0.4201    0.1208
   

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
      0.3299    0.6130    0.9470    0.3680
   
   R2 = 
      0.9826    0.1982    0.6193    0.8651    0.2065
   
   R3 = 
      0.3299    0.6130    0.9470    0.3680    0.9826    0.1982    0.6193    0.8651    0.2065
   
   C1 = 
      0.5977
      0.9643
      0.7752
      0.9456
      0.3857
      0.8732
      0.0678
      0.2987
      0.6570
      0.2903
   
   C2 = 
      0.6657
      0.2221
      0.3323
      0.5292
      0.8484
      0.6200
      0.0973
      0.4383
      0.6949
      0.0343
   
   M = 
      0.5977    0.6657
      0.9643    0.2221
      0.7752    0.3323
      0.9456    0.5292
      0.3857    0.8484
      0.8732    0.6200
      0.0678    0.0973
      0.2987    0.4383
      0.6570    0.6949
      0.2903    0.0343
   


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
      0.3306    0.4095    0.6238    0.1047
   
   R2 = 
      0.1108    0.1405    0.5370    0.8455
   
   M = 
      0.3306    0.4095    0.6238    0.1047
      0.1108    0.1405    0.5370    0.8455
   
   C1 = 
      0.0616
      0.3775
      0.9717
      0.6768
      0.8552
      0.3554
      0.6497
      0.1747
      0.1288
      0.9496
   
   C2 = 
      0.5442
      0.0107
   
   C3 = 
      0.0616
      0.3775
      0.9717
      0.6768
      0.8552
      0.3554
      0.6497
      0.1747
      0.1288
      0.9496
      0.5442
      0.0107
   

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
   

