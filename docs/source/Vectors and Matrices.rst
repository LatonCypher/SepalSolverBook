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
      0.8280    0.2164    0.0647    0.3883    0.2709    0.3402    0.8907
   
   C = 
      0.5978
      0.8661
      0.9634
      0.8788
      0.3247
   
   M = 
      0.7770    0.8080    0.7471    0.4435    0.3174    0.2458    0.6779
      0.1959    0.9123    0.0742    0.8282    0.5450    0.5267    0.5661
      0.5539    0.1338    0.9718    0.8410    0.6066    0.7569    0.2565
      0.2202    0.6891    0.0470    0.9351    0.0926    0.5774    0.5884
      0.8602    0.4769    0.2745    0.9813    0.3773    0.7550    0.7633
      0.4797    0.0527    0.5752    0.5922    0.7850    0.5497    0.1541
      0.7040    0.9104    0.8267    0.4835    0.0113    0.5942    0.6189
      0.6117    0.4023    0.5243    0.6157    0.7890    0.6098    0.4805
   

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
      0.5546    0.5494    0.3577    0.2205
   
   R2 = 
      0.1931    0.6991    0.1952    0.1509    0.1962
   
   R3 = 
      0.5546    0.5494    0.3577    0.2205    0.1931    0.6991    0.1952    0.1509    0.1962
   
   C1 = 
      0.1225
      0.8101
      0.6067
      0.2121
      0.7448
      0.4352
      0.7413
      0.6793
      0.0053
      0.2568
   
   C2 = 
      0.6472
      0.6887
      0.0401
      0.5435
      0.0231
      0.8729
      0.2289
      0.6398
      0.7501
      0.6852
   
   M = 
      0.1225    0.6472
      0.8101    0.6887
      0.6067    0.0401
      0.2121    0.5435
      0.7448    0.0231
      0.4352    0.8729
      0.7413    0.2289
      0.6793    0.6398
      0.0053    0.7501
      0.2568    0.6852
   


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
      0.3854    0.8201    0.8116    0.9693
   
   R2 = 
      0.7155    0.9359    0.8425    0.9655
   
   M = 
      0.3854    0.8201    0.8116    0.9693
      0.7155    0.9359    0.8425    0.9655
   
   C1 = 
      0.0629
      0.0503
      0.3559
      0.6285
      0.5034
      0.2345
      0.7209
      0.6851
      0.3640
      0.8556
   
   C2 = 
      0.6293
      0.5393
   
   C3 = 
      0.0629
      0.0503
      0.3559
      0.6285
      0.5034
      0.2345
      0.7209
      0.6851
      0.3640
      0.8556
      0.6293
      0.5393
   

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
   

