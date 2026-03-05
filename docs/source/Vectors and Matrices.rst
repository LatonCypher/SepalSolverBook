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
      0.3881    0.8082    0.8362    0.1158    0.8004    0.3257    0.0879
   
   C = 
      0.9141
      0.8943
      0.6136
      0.6501
      0.7180
   
   M = 
      0.3246    0.2925    0.5605    0.0903    0.3658    0.8013    0.3701
      0.3132    0.6795    0.0176    0.7280    0.7451    0.2846    0.9750
      0.1915    0.2109    0.5048    0.2687    0.8242    0.7226    0.9793
      0.7532    0.7550    0.5048    0.0609    0.0609    0.6407    0.7861
      0.4134    0.0523    0.2876    0.9905    0.9530    0.5233    0.0187
      0.3926    0.0629    0.5874    0.6899    0.0787    0.4926    0.2336
      0.8279    0.8040    0.6886    0.6976    0.9321    0.8843    0.3989
      0.5784    0.9109    0.4832    0.9618    0.0440    0.6818    0.0368
   

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
      0.3144    0.6351    0.3924    0.6845
   
   R2 = 
      0.7969    0.1839    0.6742    0.2632    0.2109
   
   R3 = 
      0.3144    0.6351    0.3924    0.6845    0.7969    0.1839    0.6742    0.2632    0.2109
   
   C1 = 
      0.8668
      0.9467
      0.8743
      0.3449
      0.9095
      0.5240
      0.5833
      0.0600
      0.2597
      0.1341
   
   C2 = 
      0.8698
      0.6807
      0.1753
      0.5052
      0.3812
      0.6878
      0.8674
      0.6527
      0.7276
      0.4410
   
   M = 
      0.8668    0.8698
      0.9467    0.6807
      0.8743    0.1753
      0.3449    0.5052
      0.9095    0.3812
      0.5240    0.6878
      0.5833    0.8674
      0.0600    0.6527
      0.2597    0.7276
      0.1341    0.4410
   


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
      0.3781    0.2416    0.5142    0.2956
   
   R2 = 
      0.5423    0.4295    0.0044    0.8024
   
   M = 
      0.3781    0.2416    0.5142    0.2956
      0.5423    0.4295    0.0044    0.8024
   
   C1 = 
      0.1076
      0.6869
      0.3156
      0.1422
      0.5666
      0.1742
      0.9619
      0.8249
      0.2702
      0.1817
   
   C2 = 
      0.2872
      0.8664
   
   C3 = 
      0.1076
      0.6869
      0.3156
      0.1422
      0.5666
      0.1742
      0.9619
      0.8249
      0.2702
      0.1817
      0.2872
      0.8664
   

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
   

