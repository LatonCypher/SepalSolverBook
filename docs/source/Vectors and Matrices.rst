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
      0.0996    0.5091    0.1858    0.6438    0.6737    0.4538    0.3640
   
   C = 
      0.8454
      0.8112
      0.4449
      0.7066
      0.3706
   
   M = 
      0.2804    0.1079    0.7197    0.2369    0.9908    0.1036    0.3136
      0.4483    0.9221    0.5266    0.8842    0.2944    0.7883    0.9125
      0.4549    0.7545    0.5734    0.6159    0.6508    0.5645    0.5332
      0.9664    0.9551    0.7076    0.8905    0.8016    0.6104    0.8745
      0.2229    0.1447    0.8591    0.1910    0.5505    0.6826    0.2997
      0.2223    0.1137    0.8502    0.6553    0.2749    0.3287    0.5566
      0.1535    0.6788    0.1455    0.2187    0.3128    0.8430    0.8718
      0.5299    0.3476    0.1282    0.3902    0.8022    0.3442    0.3860
   

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
      0.0225    0.3094    0.3356    0.2403
   
   R2 = 
      0.2514    0.7087    0.4990    0.5655    0.2114
   
   R3 = 
      0.0225    0.3094    0.3356    0.2403    0.2514    0.7087    0.4990    0.5655    0.2114
   
   C1 = 
      0.7465
      0.5668
      0.2521
      0.1967
      0.0648
      0.5428
      0.8546
      0.6306
      0.7878
      0.2989
   
   C2 = 
      0.1119
      0.0524
      0.2802
      0.6404
      0.1808
      0.2341
      0.8303
      0.1520
      0.1508
      0.3054
   
   M = 
      0.7465    0.1119
      0.5668    0.0524
      0.2521    0.2802
      0.1967    0.6404
      0.0648    0.1808
      0.5428    0.2341
      0.8546    0.8303
      0.6306    0.1520
      0.7878    0.1508
      0.2989    0.3054
   


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
      0.0272    0.9429    0.4388    0.1511
   
   R2 = 
      0.4713    0.2696    0.4237    0.1808
   
   M = 
      0.0272    0.9429    0.4388    0.1511
      0.4713    0.2696    0.4237    0.1808
   
   C1 = 
      0.7729
      0.6107
      0.3250
      0.5147
      0.5051
      0.6526
      0.6757
      0.0607
      0.1333
      0.7691
   
   C2 = 
      0.7698
      0.0886
   
   C3 = 
      0.7729
      0.6107
      0.3250
      0.5147
      0.5051
      0.6526
      0.6757
      0.0607
      0.1333
      0.7691
      0.7698
      0.0886
   

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
   

