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
      0.8707    0.2562    0.7381    0.4738    0.2059    0.0263    0.2770
   
   C = 
      0.7559
      0.6278
      0.9456
      0.5463
      0.6889
   
   M = 
      0.4376    0.5104    0.0651    0.3905    0.0188    0.9750    0.6226
      0.8969    0.3871    0.4211    0.2138    0.9598    0.7166    0.8044
      0.4407    0.4362    0.8588    0.2345    0.9817    0.4866    0.6157
      0.7541    0.2727    0.9399    0.3892    0.1851    0.0897    0.3802
      0.2759    0.2449    0.4649    0.9285    0.9259    0.8837    0.9187
      0.1512    0.9281    0.6468    0.2717    0.8531    0.8804    0.2787
      0.3869    0.3745    0.8257    0.7533    0.1930    0.0597    0.8577
      0.2224    0.1390    0.4569    0.0205    0.4186    0.2663    0.8786
   

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
      0.9682    0.7510    0.7400    0.5696
   
   R2 = 
      0.2500    0.0093    0.0124    0.6323    0.3963
   
   R3 = 
      0.9682    0.7510    0.7400    0.5696    0.2500    0.0093    0.0124    0.6323    0.3963
   
   C1 = 
      0.3561
      0.8531
      0.5987
      0.8101
      0.5312
      0.0736
      0.5261
      0.3549
      0.5377
      0.7547
   
   C2 = 
      0.1819
      0.4101
      0.8697
      0.0536
      0.7697
      0.6155
      0.0572
      0.4790
      0.6510
      0.3955
   
   M = 
      0.3561    0.1819
      0.8531    0.4101
      0.5987    0.8697
      0.8101    0.0536
      0.5312    0.7697
      0.0736    0.6155
      0.5261    0.0572
      0.3549    0.4790
      0.5377    0.6510
      0.7547    0.3955
   


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
      0.4489    0.0766    0.1288    0.0286
   
   R2 = 
      0.5999    0.1816    0.5344    0.9367
   
   M = 
      0.4489    0.0766    0.1288    0.0286
      0.5999    0.1816    0.5344    0.9367
   
   C1 = 
      0.5911
      0.5747
      0.5900
      0.6315
      0.0543
      0.8106
      0.4901
      0.7976
      0.2473
      0.0390
   
   C2 = 
      0.5498
      0.7300
   
   C3 = 
      0.5911
      0.5747
      0.5900
      0.6315
      0.0543
      0.8106
      0.4901
      0.7976
      0.2473
      0.0390
      0.5498
      0.7300
   

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
   

