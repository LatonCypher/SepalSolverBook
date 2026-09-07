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
      0.3522    0.1878    0.3922    0.4188    0.7119    0.3791    0.4447
   
   C = 
      0.0673
      0.6592
      0.1130
      0.2337
      0.9289
   
   M = 
      0.7959    0.7915    0.3732    0.6759    0.7971    0.9672    0.6140
      0.5853    0.1154    0.8002    0.8689    0.3505    0.4480    0.5277
      0.3848    0.0579    0.9502    0.9978    0.6547    0.3884    0.5512
      0.0751    0.2075    0.4367    0.5001    0.2070    0.9004    0.6917
      0.5933    0.7577    0.0959    0.5505    0.7578    0.4140    0.4540
      0.7726    0.0630    0.3861    0.6610    0.2144    0.7187    0.6460
      0.0599    0.2756    0.4862    0.7354    0.0386    0.3930    0.6352
      0.6992    0.3305    0.8774    0.5749    0.7269    0.9950    0.0104
   

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
      0.8701    0.0823    0.9481    0.3218
   
   R2 = 
      0.8029    0.3916    0.2296    0.5607    0.0169
   
   R3 = 
      0.8701    0.0823    0.9481    0.3218    0.8029    0.3916    0.2296    0.5607    0.0169
   
   C1 = 
      0.9389
      0.5672
      0.5138
      0.9309
      0.8250
      0.9536
      0.6931
      0.4519
      0.7599
      0.2801
   
   C2 = 
      0.3473
      0.5358
      0.8297
      0.2163
      0.5604
      0.8855
      0.8624
      0.2483
      0.2383
      0.8427
   
   M = 
      0.9389    0.3473
      0.5672    0.5358
      0.5138    0.8297
      0.9309    0.2163
      0.8250    0.5604
      0.9536    0.8855
      0.6931    0.8624
      0.4519    0.2483
      0.7599    0.2383
      0.2801    0.8427
   


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
      0.8603    0.3359    0.0453    0.1159
   
   R2 = 
      0.3174    0.3772    0.8342    0.8803
   
   M = 
      0.8603    0.3359    0.0453    0.1159
      0.3174    0.3772    0.8342    0.8803
   
   C1 = 
      0.0339
      0.1266
      0.6309
      0.0750
      0.7620
      0.0944
      0.5670
      0.1043
      0.1609
      0.7194
   
   C2 = 
      0.3630
      0.0941
   
   C3 = 
      0.0339
      0.1266
      0.6309
      0.0750
      0.7620
      0.0944
      0.5670
      0.1043
      0.1609
      0.7194
      0.3630
      0.0941
   

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
   

