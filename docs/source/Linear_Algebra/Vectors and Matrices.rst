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
      0.5376    0.4415    0.6320    0.8215    0.1974    0.5031    0.3901
   
   C = 
      0.0990
      0.9485
      0.2262
      0.8368
      0.3425
   
   M = 
      0.8616    0.9476    0.2112    0.0550    0.8554    0.4435    0.4391
      0.2674    0.7676    0.9265    0.9181    0.7170    0.2692    0.6763
      0.9377    0.3494    0.4374    0.1134    0.8880    0.1437    0.2013
      0.3127    0.9888    0.7809    0.7041    0.6793    0.5815    0.4188
      0.5523    0.6060    0.4162    0.5656    0.6756    0.7510    0.7671
      0.5570    0.5919    0.3226    0.6171    0.7733    0.5988    0.5395
      0.3743    0.5456    0.2541    0.0966    0.9577    0.9726    0.4171
      0.1029    0.7907    0.6942    0.7375    0.9067    0.6542    0.2324
   

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
      0.1636    0.4914    0.1586    0.4544
   
   R2 = 
      0.8566    0.1044    0.2296    0.7306    0.1556
   
   R3 = 
      0.1636    0.4914    0.1586    0.4544    0.8566    0.1044    0.2296    0.7306    0.1556
   
   C1 = 
      0.5782
      0.9648
      0.7143
      0.7912
      0.4023
      0.2334
      0.0053
      0.9299
      0.2434
      0.7432
   
   C2 = 
      0.3072
      0.1852
      0.0366
      0.4051
      0.1850
      0.6314
      0.9477
      0.1179
      0.4877
      0.7668
   
   M = 
      0.5782    0.3072
      0.9648    0.1852
      0.7143    0.0366
      0.7912    0.4051
      0.4023    0.1850
      0.2334    0.6314
      0.0053    0.9477
      0.9299    0.1179
      0.2434    0.4877
      0.7432    0.7668
   


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
      0.9411    0.2128    0.1521    0.1072
   
   R2 = 
      0.5598    0.4223    0.5769    0.9218
   
   M = 
      0.9411    0.2128    0.1521    0.1072
      0.5598    0.4223    0.5769    0.9218
   
   C1 = 
      0.8605
      0.6669
      0.0229
      0.5419
      0.9805
      0.7259
      0.5210
      0.2296
      0.9756
      0.0025
   
   C2 = 
      0.8780
      0.6057
   
   C3 = 
      0.8605
      0.6669
      0.0229
      0.5419
      0.9805
      0.7259
      0.5210
      0.2296
      0.9756
      0.0025
      0.8780
      0.6057
   

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
   

