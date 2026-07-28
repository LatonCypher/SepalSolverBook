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
      0.0499    0.5771    0.4849    0.5022    0.4788    0.4998    0.4675
   
   C = 
      0.9801
      0.5882
      0.4308
      0.1131
      0.3555
   
   M = 
      0.0628    0.5077    0.5473    0.3518    0.6162    0.0656    0.1582
      0.3300    0.1820    0.9888    0.8289    0.1683    0.8551    0.7018
      0.0213    0.8404    0.7066    0.4427    0.8195    0.8510    0.7540
      0.9802    0.8412    0.9212    0.9420    0.9912    0.3814    0.1750
      0.0622    0.6246    0.6133    0.7482    0.3947    0.2348    0.6294
      0.5891    0.7506    0.1264    0.7262    0.8430    0.6762    0.3769
      0.8938    0.6246    0.8066    0.5984    0.1388    0.3666    0.4563
      0.4298    0.4459    0.2879    0.9987    0.2834    0.8867    0.0597
   

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
      0.5139    0.8321    0.2286    0.5871
   
   R2 = 
      0.6778    0.0826    0.9572    0.1676    0.4348
   
   R3 = 
      0.5139    0.8321    0.2286    0.5871    0.6778    0.0826    0.9572    0.1676    0.4348
   
   C1 = 
      0.7939
      0.1822
      0.8445
      0.1979
      0.9162
      0.6963
      0.5943
      0.5827
      0.4820
      0.9151
   
   C2 = 
      0.3617
      0.3190
      0.8276
      0.8924
      0.5989
      0.2376
      0.5789
      0.8355
      0.5671
      0.4739
   
   M = 
      0.7939    0.3617
      0.1822    0.3190
      0.8445    0.8276
      0.1979    0.8924
      0.9162    0.5989
      0.6963    0.2376
      0.5943    0.5789
      0.5827    0.8355
      0.4820    0.5671
      0.9151    0.4739
   


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
      0.1192    0.8342    0.8890    0.7210
   
   R2 = 
      0.8657    0.2694    0.3564    0.4329
   
   M = 
      0.1192    0.8342    0.8890    0.7210
      0.8657    0.2694    0.3564    0.4329
   
   C1 = 
      0.1064
      0.7859
      0.8764
      0.0820
      0.0106
      0.6440
      0.8032
      0.0705
      0.2636
      0.8447
   
   C2 = 
      0.5808
      0.5178
   
   C3 = 
      0.1064
      0.7859
      0.8764
      0.0820
      0.0106
      0.6440
      0.8032
      0.0705
      0.2636
      0.8447
      0.5808
      0.5178
   

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
   

