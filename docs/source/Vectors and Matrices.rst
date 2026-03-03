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
      0.9126    0.2990    0.0672    0.4469    0.5411    0.2591    0.7830
   
   C = 
      0.5342
      0.1784
      0.8012
      0.7167
      0.1731
   
   M = 
      0.2027    0.6863    0.2760    0.0428    0.6087    0.2528    0.8457
      0.8341    0.1108    0.2311    0.2635    0.9045    0.7881    0.1152
      0.1849    0.7269    0.4844    0.4322    0.8566    0.7801    0.9350
      0.4042    0.6368    0.9522    0.4131    0.2705    0.9042    0.0166
      0.3898    0.8039    0.0659    0.0053    0.9780    0.8702    0.2565
      0.1874    0.2063    0.4649    0.4914    0.8744    0.9036    0.2863
      0.0107    0.5269    0.0596    0.8304    0.9532    0.0546    0.5673
      0.7066    0.3948    0.2656    0.3100    0.5511    0.8665    0.5552
   

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
      0.9865    0.5202    0.2194    0.2561
   
   R2 = 
      0.6777    0.1447    0.0575    0.2939    0.1792
   
   R3 = 
      0.9865    0.5202    0.2194    0.2561    0.6777    0.1447    0.0575    0.2939    0.1792
   
   C1 = 
      0.4035
      0.2467
      0.7390
      0.2089
      0.8216
      0.7182
      0.4270
      0.0310
      0.2737
      0.9304
   
   C2 = 
      0.9532
      0.3632
      0.1159
      0.8729
      0.9720
      0.6033
      0.0029
      0.8887
      0.3204
      0.4754
   
   M = 
      0.4035    0.9532
      0.2467    0.3632
      0.7390    0.1159
      0.2089    0.8729
      0.8216    0.9720
      0.7182    0.6033
      0.4270    0.0029
      0.0310    0.8887
      0.2737    0.3204
      0.9304    0.4754
   


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
      0.1835    0.2255    0.6207    0.1802
   
   R2 = 
      0.5081    0.2998    0.7494    0.5794
   
   M = 
      0.1835    0.2255    0.6207    0.1802
      0.5081    0.2998    0.7494    0.5794
   
   C1 = 
      0.6589
      0.6000
      0.6055
      0.3187
      0.3849
      0.8502
      0.6236
      0.4538
      0.5478
      0.4993
   
   C2 = 
      0.0776
      0.8720
   
   C3 = 
      0.6589
      0.6000
      0.6055
      0.3187
      0.3849
      0.8502
      0.6236
      0.4538
      0.5478
      0.4993
      0.0776
      0.8720
   

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
   

