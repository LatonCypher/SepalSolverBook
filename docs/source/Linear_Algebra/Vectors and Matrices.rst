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
      0.8117    0.8449    0.6684    0.8081    0.2026    0.7798    0.2737
   
   C = 
      0.9172
      0.1597
      0.8623
      0.6217
      0.1472
   
   M = 
      0.7050    0.1860    0.4204    0.8696    0.5884    0.6869    0.2980
      0.0179    0.6858    0.7391    0.5697    0.2033    0.1979    0.8924
      0.3628    0.2088    0.3700    0.4141    0.6856    0.5186    0.4849
      0.4254    0.2110    0.4733    0.4482    0.7972    0.1358    0.3988
      0.1141    0.1536    0.1241    0.0465    0.4239    0.3341    0.8065
      0.8595    0.7141    0.0470    0.0628    0.0400    0.0396    0.2355
      0.1822    0.4993    0.5343    0.7004    0.8526    0.9871    0.0478
      0.3153    0.1489    0.0945    0.6496    0.7542    0.9179    0.3408
   

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
      0.5233    0.6437    0.5579    0.4363
   
   R2 = 
      0.1366    0.5087    0.9625    0.1359    0.5600
   
   R3 = 
      0.5233    0.6437    0.5579    0.4363    0.1366    0.5087    0.9625    0.1359    0.5600
   
   C1 = 
      0.8501
      0.5254
      0.5393
      0.2649
      0.4166
      0.7254
      0.1634
      0.5674
      0.4259
      0.4591
   
   C2 = 
      0.9242
      0.2261
      0.6034
      0.2541
      0.5654
      0.5804
      0.1527
      0.0210
      0.3361
      0.1345
   
   M = 
      0.8501    0.9242
      0.5254    0.2261
      0.5393    0.6034
      0.2649    0.2541
      0.4166    0.5654
      0.7254    0.5804
      0.1634    0.1527
      0.5674    0.0210
      0.4259    0.3361
      0.4591    0.1345
   


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
      0.4518    0.4768    0.4218    0.0928
   
   R2 = 
      0.7486    0.9363    0.9049    0.8261
   
   M = 
      0.4518    0.4768    0.4218    0.0928
      0.7486    0.9363    0.9049    0.8261
   
   C1 = 
      0.8486
      0.0404
      0.7034
      0.8568
      0.1031
      0.6466
      0.1650
      0.4615
      0.9598
      0.4473
   
   C2 = 
      0.9907
      0.0341
   
   C3 = 
      0.8486
      0.0404
      0.7034
      0.8568
      0.1031
      0.6466
      0.1650
      0.4615
      0.9598
      0.4473
      0.9907
      0.0341
   

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
   

