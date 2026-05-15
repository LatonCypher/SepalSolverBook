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
      0.5635    0.6891    0.6714    0.3858    0.9696    0.2101    0.5075
   
   C = 
      0.8529
      0.8368
      0.2274
      0.4791
      0.6740
   
   M = 
      0.5627    0.2880    0.5200    0.1901    0.5232    0.3658    0.7679
      0.4948    0.9348    0.8116    0.3152    0.2069    0.8087    0.4086
      0.2417    0.4744    0.1554    0.9833    0.2743    0.2205    0.2183
      0.7343    0.5922    0.8611    0.0745    0.0881    0.0808    0.0674
      0.7655    0.8081    0.4276    0.0238    0.8332    0.9174    0.1328
      0.5759    0.3450    0.3854    0.9393    0.0181    0.0118    0.0202
      0.8677    0.7134    0.5535    0.2198    0.1873    0.6411    0.4283
      0.3507    0.6957    0.7225    0.4183    0.4399    0.1735    0.8519
   

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
      0.0271    0.1299    0.5378    0.9890
   
   R2 = 
      0.0173    0.6702    0.7004    0.6812    0.7758
   
   R3 = 
      0.0271    0.1299    0.5378    0.9890    0.0173    0.6702    0.7004    0.6812    0.7758
   
   C1 = 
      0.8917
      0.8222
      0.5851
      0.7040
      0.7511
      0.0076
      0.9819
      0.9276
      0.3758
      0.9699
   
   C2 = 
      0.0220
      0.9231
      0.9163
      0.6798
      0.7510
      0.1193
      0.8911
      0.4617
      0.1754
      0.2255
   
   M = 
      0.8917    0.0220
      0.8222    0.9231
      0.5851    0.9163
      0.7040    0.6798
      0.7511    0.7510
      0.0076    0.1193
      0.9819    0.8911
      0.9276    0.4617
      0.3758    0.1754
      0.9699    0.2255
   


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
      0.0242    0.0507    0.5425    0.7703
   
   R2 = 
      0.1948    0.1964    0.4251    0.3681
   
   M = 
      0.0242    0.0507    0.5425    0.7703
      0.1948    0.1964    0.4251    0.3681
   
   C1 = 
      0.8756
      0.2556
      0.9919
      0.5393
      0.1437
      0.9917
      0.5410
      0.6778
      0.8580
      0.6240
   
   C2 = 
      0.8291
      0.0468
   
   C3 = 
      0.8756
      0.2556
      0.9919
      0.5393
      0.1437
      0.9917
      0.5410
      0.6778
      0.8580
      0.6240
      0.8291
      0.0468
   

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
   

