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
      0.1242    0.7177    0.4531    0.3311    0.3628    0.5531    0.8820
   
   C = 
      0.9828
      0.7748
      0.3009
      0.5014
      0.6498
   
   M = 
      0.8986    0.4386    0.0372    0.3128    0.0354    0.1086    0.8853
      0.4452    0.8722    0.9542    0.5873    0.6497    0.5756    0.6394
      0.3858    0.7267    0.9795    0.9115    0.7741    0.8915    0.2487
      0.3515    0.2108    0.7873    0.1643    0.3707    0.0062    0.0072
      0.9587    0.9937    0.0212    0.9796    0.9388    0.7642    0.9662
      0.7491    0.7891    0.9485    0.4379    0.8726    0.0653    0.3244
      0.5485    0.4306    0.8189    0.1238    0.2801    0.6254    0.7953
      0.5406    0.2088    0.4762    0.7503    0.0613    0.9129    0.6633
   

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
      0.3002    0.0020    0.3141    0.1612
   
   R2 = 
      0.1540    0.9634    0.5826    0.0653    0.6416
   
   R3 = 
      0.3002    0.0020    0.3141    0.1612    0.1540    0.9634    0.5826    0.0653    0.6416
   
   C1 = 
      0.0691
      0.2293
      0.1325
      0.2712
      0.6024
      0.1319
      0.2068
      0.4569
      0.5513
      0.2568
   
   C2 = 
      0.1294
      0.5016
      0.4771
      0.1104
      0.1163
      0.9549
      0.6046
      0.9822
      0.4934
      0.9812
   
   M = 
      0.0691    0.1294
      0.2293    0.5016
      0.1325    0.4771
      0.2712    0.1104
      0.6024    0.1163
      0.1319    0.9549
      0.2068    0.6046
      0.4569    0.9822
      0.5513    0.4934
      0.2568    0.9812
   


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
      0.1026    0.2463    0.5756    0.3808
   
   R2 = 
      0.2491    0.4818    0.2486    0.7904
   
   M = 
      0.1026    0.2463    0.5756    0.3808
      0.2491    0.4818    0.2486    0.7904
   
   C1 = 
      0.3619
      0.0110
      0.7875
      0.3706
      0.9087
      0.6707
      0.4279
      0.6343
      0.3111
      0.7518
   
   C2 = 
      0.1826
      0.3393
   
   C3 = 
      0.3619
      0.0110
      0.7875
      0.3706
      0.9087
      0.6707
      0.4279
      0.6343
      0.3111
      0.7518
      0.1826
      0.3393
   

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
   

