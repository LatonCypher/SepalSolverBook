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
      0.9388    0.9495    0.8497    0.5412    0.7816    0.3886    0.5512
   
   C = 
      0.5640
      0.1655
      0.9186
      0.8144
      0.6733
   
   M = 
      0.1508    0.1953    0.7752    0.9670    0.7718    0.4159    0.5410
      0.0236    0.9266    0.5037    0.4354    0.5157    0.6612    0.6911
      0.9232    0.0413    0.1452    0.8059    0.2692    0.6878    0.4870
      0.3085    0.6701    0.9566    0.1138    0.8105    0.4691    0.7516
      0.8578    0.1322    0.0738    0.4094    0.3918    0.6136    0.9158
      0.7492    0.4428    0.0251    0.7685    0.9142    0.8652    0.9216
      0.7950    0.3794    0.3656    0.4481    0.2268    0.9595    0.1464
      0.2265    0.2060    0.3734    0.5518    0.8947    0.9755    0.8211
   

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
      0.0665    0.0225    0.7744    0.1616
   
   R2 = 
      0.3050    0.6651    0.0623    0.1793    0.2107
   
   R3 = 
      0.0665    0.0225    0.7744    0.1616    0.3050    0.6651    0.0623    0.1793    0.2107
   
   C1 = 
      0.8835
      0.7138
      0.6986
      0.2473
      0.2769
      0.7091
      0.3387
      0.1612
      0.1398
      0.3691
   
   C2 = 
      0.1958
      0.7830
      0.8187
      0.1979
      0.7665
      0.0251
      0.0956
      0.6606
      0.7974
      0.3618
   
   M = 
      0.8835    0.1958
      0.7138    0.7830
      0.6986    0.8187
      0.2473    0.1979
      0.2769    0.7665
      0.7091    0.0251
      0.3387    0.0956
      0.1612    0.6606
      0.1398    0.7974
      0.3691    0.3618
   


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
      0.2059    0.8966    0.0413    0.0797
   
   R2 = 
      0.3277    0.3796    0.3741    0.4531
   
   M = 
      0.2059    0.8966    0.0413    0.0797
      0.3277    0.3796    0.3741    0.4531
   
   C1 = 
      0.9308
      0.2022
      0.8373
      0.9069
      0.5608
      0.7739
      0.6408
      0.0807
      0.0231
      0.0003
   
   C2 = 
      0.2081
      0.9738
   
   C3 = 
      0.9308
      0.2022
      0.8373
      0.9069
      0.5608
      0.7739
      0.6408
      0.0807
      0.0231
      0.0003
      0.2081
      0.9738
   

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
   

