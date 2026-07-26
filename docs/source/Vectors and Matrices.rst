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
      0.7441    0.9426    0.9602    0.6413    0.6580    0.4086    0.7626
   
   C = 
      0.6088
      0.2645
      0.4968
      0.4708
      0.7209
   
   M = 
      0.2062    0.2106    0.7086    0.2947    0.0003    0.7364    0.1358
      0.3271    0.1747    0.0639    0.6580    0.1153    0.0533    0.9735
      0.2545    0.5773    0.1168    0.7762    0.0427    0.1129    0.9730
      0.3089    0.7955    0.1732    0.0747    0.9030    0.8966    0.8432
      0.1374    0.3624    0.0236    0.4990    0.8336    0.4985    0.0312
      0.6595    0.4860    0.5197    0.2209    0.0688    0.8285    0.7556
      0.2990    0.4541    0.6041    0.4861    0.6578    0.0315    0.0753
      0.7388    0.0084    0.5721    0.8160    0.4148    0.6608    0.9991
   

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
      0.2349    0.5624    0.0628    0.5989
   
   R2 = 
      0.3530    0.8313    0.0257    0.1859    0.4207
   
   R3 = 
      0.2349    0.5624    0.0628    0.5989    0.3530    0.8313    0.0257    0.1859    0.4207
   
   C1 = 
      0.6276
      0.8382
      0.5572
      0.6553
      0.7021
      0.5696
      0.2036
      0.9217
      0.5956
      0.6716
   
   C2 = 
      0.0514
      0.4108
      0.4728
      0.0351
      0.1628
      0.7653
      0.9959
      0.6169
      0.2499
      0.4063
   
   M = 
      0.6276    0.0514
      0.8382    0.4108
      0.5572    0.4728
      0.6553    0.0351
      0.7021    0.1628
      0.5696    0.7653
      0.2036    0.9959
      0.9217    0.6169
      0.5956    0.2499
      0.6716    0.4063
   


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
      0.4869    0.5007    0.9201    0.7561
   
   R2 = 
      0.0748    0.2594    0.2134    0.4713
   
   M = 
      0.4869    0.5007    0.9201    0.7561
      0.0748    0.2594    0.2134    0.4713
   
   C1 = 
      0.7129
      0.6478
      0.1555
      0.1271
      0.9638
      0.6003
      0.5051
      0.1372
      0.9370
      0.9271
   
   C2 = 
      0.7797
      0.5209
   
   C3 = 
      0.7129
      0.6478
      0.1555
      0.1271
      0.9638
      0.6003
      0.5051
      0.1372
      0.9370
      0.9271
      0.7797
      0.5209
   

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
   

