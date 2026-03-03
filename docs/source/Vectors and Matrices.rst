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
      0.0647    0.6325    0.0248    0.6545    0.5229    0.7391    0.6944
   
   C = 
      0.4058
      0.2299
      0.7697
      0.1411
      0.7111
   
   M = 
      0.6426    0.8177    0.3725    0.9194    0.7997    0.5271    0.1236
      0.0964    0.2909    0.7800    0.5463    0.4437    0.4498    0.6599
      0.4087    0.4550    0.0030    0.3583    0.7371    0.8072    0.5338
      0.2945    0.5290    0.7163    0.2080    0.3931    0.1713    0.3534
      0.5991    0.1874    0.6381    0.6649    0.5688    0.0816    0.5888
      0.5319    0.8267    0.0106    0.8091    0.4381    0.9898    0.7849
      0.1897    0.8432    0.8329    0.5877    0.4721    0.8432    0.4805
      0.2565    0.1994    0.4663    0.4713    0.5766    0.4127    0.2594
   

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
      0.7203    0.2056    0.1087    0.2601
   
   R2 = 
      0.5382    0.1193    0.1271    0.2206    0.6192
   
   R3 = 
      0.7203    0.2056    0.1087    0.2601    0.5382    0.1193    0.1271    0.2206    0.6192
   
   C1 = 
      0.5951
      0.5018
      0.9897
      0.2575
      0.4081
      0.9931
      0.6258
      0.7307
      0.3082
      0.0597
   
   C2 = 
      0.6337
      0.9703
      0.8593
      0.2424
      0.3107
      0.9398
      0.0656
      0.0545
      0.0837
      0.7351
   
   M = 
      0.5951    0.6337
      0.5018    0.9703
      0.9897    0.8593
      0.2575    0.2424
      0.4081    0.3107
      0.9931    0.9398
      0.6258    0.0656
      0.7307    0.0545
      0.3082    0.0837
      0.0597    0.7351
   


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
      0.2619    0.7373    0.6798    0.1428
   
   R2 = 
      0.0130    0.9277    0.6844    0.3712
   
   M = 
      0.2619    0.7373    0.6798    0.1428
      0.0130    0.9277    0.6844    0.3712
   
   C1 = 
      0.5281
      0.6627
      0.5076
      0.1437
      0.3053
      0.9096
      0.8472
      0.5222
      0.1719
      0.2428
   
   C2 = 
      0.3049
      0.6269
   
   C3 = 
      0.5281
      0.6627
      0.5076
      0.1437
      0.3053
      0.9096
      0.8472
      0.5222
      0.1719
      0.2428
      0.3049
      0.6269
   

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
   

