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
      0.2394    0.9529    0.1613    0.5607    0.3314    0.8495    0.4077
   
   C = 
      0.1345
      0.8651
      0.6523
      0.0295
      0.5529
   
   M = 
      0.2413    0.3805    0.0269    0.9902    0.6797    0.0402    0.7812
      0.3610    0.1255    0.9354    0.8882    0.3230    0.8249    0.3052
      0.0936    0.3345    0.9042    0.9037    0.7255    0.4453    0.9656
      0.1168    0.2725    0.0106    0.1995    0.7627    0.3619    0.5480
      0.8209    0.3080    0.6554    0.5221    0.8131    0.3065    0.1754
      0.2960    0.3207    0.1988    0.7860    0.1506    0.3247    0.8951
      0.7066    0.9810    0.0721    0.8754    0.2776    0.1635    0.6151
      0.6244    0.1836    0.5323    0.3360    0.4194    0.1446    0.4457
   

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
      0.8790    0.1890    0.3063    0.3247
   
   R2 = 
      0.7719    0.8243    0.5683    0.6188    0.0096
   
   R3 = 
      0.8790    0.1890    0.3063    0.3247    0.7719    0.8243    0.5683    0.6188    0.0096
   
   C1 = 
      0.7578
      0.0151
      0.9192
      0.0563
      0.5861
      0.3159
      0.6319
      0.4681
      0.8414
      0.9946
   
   C2 = 
      0.6643
      0.2794
      0.1827
      0.7643
      0.2777
      0.0556
      0.3861
      0.1596
      0.8197
      0.6069
   
   M = 
      0.7578    0.6643
      0.0151    0.2794
      0.9192    0.1827
      0.0563    0.7643
      0.5861    0.2777
      0.3159    0.0556
      0.6319    0.3861
      0.4681    0.1596
      0.8414    0.8197
      0.9946    0.6069
   


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
      0.5308    0.7807    0.8091    0.2550
   
   R2 = 
      0.1310    0.3096    0.9431    0.7063
   
   M = 
      0.5308    0.7807    0.8091    0.2550
      0.1310    0.3096    0.9431    0.7063
   
   C1 = 
      0.4212
      0.7641
      0.1307
      0.7727
      0.4712
      0.6617
      0.9001
      0.9025
      0.8642
      0.6164
   
   C2 = 
      0.8697
      0.0100
   
   C3 = 
      0.4212
      0.7641
      0.1307
      0.7727
      0.4712
      0.6617
      0.9001
      0.9025
      0.8642
      0.6164
      0.8697
      0.0100
   

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
   

