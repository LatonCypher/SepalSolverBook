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
      0.0751    0.1104    0.6974    0.6704    0.8899    0.3602    0.1710
   
   C = 
      0.2522
      0.8748
      0.1357
      0.2119
      0.1180
   
   M = 
      0.2447    0.7517    0.8065    0.9411    0.8784    0.8260    0.4164
      0.3519    0.3521    0.7755    0.0662    0.1477    0.5981    0.7856
      0.5844    0.5545    0.2873    0.4839    0.6178    0.0941    0.2595
      0.6330    0.0634    0.6489    0.7465    0.3308    0.8239    0.8357
      0.4119    0.8663    0.1089    0.1478    0.2117    0.2575    0.3312
      0.6612    0.6420    0.0673    0.7152    0.7401    0.7063    0.4709
      0.1564    0.0428    0.5422    0.5288    0.5222    0.3499    0.3295
      0.5106    0.6632    0.1232    0.8202    0.4054    0.5067    0.4385
   

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
      0.5409    0.3888    0.1541    0.9560
   
   R2 = 
      0.9783    0.0805    0.5261    0.1953    0.8611
   
   R3 = 
      0.5409    0.3888    0.1541    0.9560    0.9783    0.0805    0.5261    0.1953    0.8611
   
   C1 = 
      0.2240
      0.7928
      0.7091
      0.0230
      0.6985
      0.1496
      0.6125
      0.4969
      0.6803
      0.0981
   
   C2 = 
      0.6434
      0.6472
      0.6120
      0.3092
      0.8254
      0.3963
      0.2282
      0.4274
      0.8941
      0.4330
   
   M = 
      0.2240    0.6434
      0.7928    0.6472
      0.7091    0.6120
      0.0230    0.3092
      0.6985    0.8254
      0.1496    0.3963
      0.6125    0.2282
      0.4969    0.4274
      0.6803    0.8941
      0.0981    0.4330
   


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
      0.7468    0.8465    0.0661    0.3703
   
   R2 = 
      0.5152    0.5089    0.5843    0.9302
   
   M = 
      0.7468    0.8465    0.0661    0.3703
      0.5152    0.5089    0.5843    0.9302
   
   C1 = 
      0.7213
      0.1996
      0.8217
      0.6165
      0.5264
      0.2246
      0.5309
      0.4629
      0.7598
      0.3097
   
   C2 = 
      0.8336
      0.1873
   
   C3 = 
      0.7213
      0.1996
      0.8217
      0.6165
      0.5264
      0.2246
      0.5309
      0.4629
      0.7598
      0.3097
      0.8336
      0.1873
   

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
   

