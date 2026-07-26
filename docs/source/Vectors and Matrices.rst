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
      0.1009    0.6744    0.6449    0.1802    0.4421    0.2320    0.9198
   
   C = 
      0.7576
      0.0385
      0.4726
      0.8700
      0.7098
   
   M = 
      0.2647    0.6592    0.6597    0.8962    0.1695    0.8248    0.1092
      0.9716    0.6408    0.0945    0.1483    0.3090    0.0087    0.2581
      0.6159    0.4603    0.2530    0.3693    0.9743    0.5911    0.0476
      0.5387    0.2792    0.7568    0.0079    0.6866    0.1117    0.8428
      0.0806    0.1730    0.5568    0.7785    0.9743    0.9685    0.4057
      0.8465    0.0303    0.2592    0.7473    0.6599    0.6344    0.2738
      0.7716    0.5384    0.8584    0.6053    0.6706    0.2459    0.5134
      0.2140    0.5998    0.5948    0.7264    0.3479    0.5236    0.0276
   

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
      0.9579    0.9122    0.1784    0.1797
   
   R2 = 
      0.9812    0.3023    0.2845    0.1622    0.3650
   
   R3 = 
      0.9579    0.9122    0.1784    0.1797    0.9812    0.3023    0.2845    0.1622    0.3650
   
   C1 = 
      0.6400
      0.6736
      0.3802
      0.9181
      0.6880
      0.2039
      0.1729
      0.3842
      0.9471
      0.6195
   
   C2 = 
      0.5696
      0.9343
      0.9525
      0.4474
      0.7276
      0.5089
      0.1679
      0.2012
      0.5330
      0.8435
   
   M = 
      0.6400    0.5696
      0.6736    0.9343
      0.3802    0.9525
      0.9181    0.4474
      0.6880    0.7276
      0.2039    0.5089
      0.1729    0.1679
      0.3842    0.2012
      0.9471    0.5330
      0.6195    0.8435
   


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
      0.6146    0.1032    0.5877    0.2154
   
   R2 = 
      0.1428    0.2951    0.8562    0.0955
   
   M = 
      0.6146    0.1032    0.5877    0.2154
      0.1428    0.2951    0.8562    0.0955
   
   C1 = 
      0.0175
      0.2496
      0.7079
      0.1860
      0.6307
      0.5172
      0.6606
      0.8952
      0.3538
      0.1001
   
   C2 = 
      0.4098
      0.4945
   
   C3 = 
      0.0175
      0.2496
      0.7079
      0.1860
      0.6307
      0.5172
      0.6606
      0.8952
      0.3538
      0.1001
      0.4098
      0.4945
   

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
   

