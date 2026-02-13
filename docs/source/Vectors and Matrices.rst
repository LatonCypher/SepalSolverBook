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
      0.9237    0.8985    0.6091    0.4851    0.3303    0.3214    0.9888
   
   C = 
      0.1370
      0.5810
      0.4518
      0.4424
      0.5598
   
   M = 
      0.7354    0.5372    0.0075    0.1068    0.4037    0.4801    0.6852
      0.7842    0.8378    0.0677    0.7440    0.0737    0.6067    0.2624
      0.6513    0.9876    0.8534    0.0517    0.4767    0.6087    0.9897
      0.3297    0.9738    0.4779    0.2365    0.4456    0.3371    0.8045
      0.5260    0.4393    0.4717    0.6105    0.0567    0.3555    0.0243
      0.1565    0.7934    0.1462    0.3331    0.0127    0.4920    0.2395
      0.4907    0.0443    0.1934    0.3519    0.9665    0.1101    0.1026
      0.2881    0.1557    0.1039    0.7609    0.9143    0.3540    0.9826
   

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
      0.4471    0.0500    0.0029    0.6934
   
   R2 = 
      0.6750    0.7240    0.7592    0.0937    0.3921
   
   R3 = 
      0.4471    0.0500    0.0029    0.6934    0.6750    0.7240    0.7592    0.0937    0.3921
   
   C1 = 
      0.1354
      0.3727
      0.6126
      0.2594
      0.9746
      0.6682
      0.4134
      0.8855
      0.0077
      0.0855
   
   C2 = 
      0.7781
      0.4924
      0.5653
      0.3071
      0.0058
      0.3692
      0.6746
      0.8043
      0.1647
      0.0198
   
   M = 
      0.1354    0.7781
      0.3727    0.4924
      0.6126    0.5653
      0.2594    0.3071
      0.9746    0.0058
      0.6682    0.3692
      0.4134    0.6746
      0.8855    0.8043
      0.0077    0.1647
      0.0855    0.0198
   


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
      0.3742    0.7008    0.4961    0.9609
   
   R2 = 
      0.9756    0.8769    0.8188    0.2101
   
   M = 
      0.3742    0.7008    0.4961    0.9609
      0.9756    0.8769    0.8188    0.2101
   
   C1 = 
      0.5979
      0.4255
      0.1828
      0.7324
      0.4846
      0.0244
      0.2628
      0.0555
      0.3388
      0.4471
   
   C2 = 
      0.6275
      0.4428
   
   C3 = 
      0.5979
      0.4255
      0.1828
      0.7324
      0.4846
      0.0244
      0.2628
      0.0555
      0.3388
      0.4471
      0.6275
      0.4428
   

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
   

