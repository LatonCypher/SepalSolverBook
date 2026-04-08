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
      0.4623    0.0365    0.8859    0.2205    0.3783    0.9404    0.7235
   
   C = 
      0.8807
      0.1329
      0.5276
      0.6138
      0.7398
   
   M = 
      0.5085    0.2765    0.2092    0.0032    0.2881    0.4367    0.9283
      0.6894    0.9108    0.6542    0.6157    0.7346    0.2402    0.2883
      0.6149    0.8331    0.4748    0.3215    0.8355    0.8949    0.0667
      0.2058    0.6340    0.3231    0.2914    0.1425    0.9814    0.8921
      0.0558    0.4041    0.9724    0.1218    0.1563    0.0002    0.6092
      0.9382    0.5970    0.7832    0.5653    0.9791    0.8938    0.8838
      0.7297    0.3508    0.6862    0.8500    0.2873    0.4634    0.3136
      0.7689    0.6023    0.0481    0.3082    0.8077    0.0739    0.4354
   

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
      0.8133    0.3902    0.5657    0.1588
   
   R2 = 
      0.4480    0.7007    0.2662    0.6020    0.8167
   
   R3 = 
      0.8133    0.3902    0.5657    0.1588    0.4480    0.7007    0.2662    0.6020    0.8167
   
   C1 = 
      0.7205
      0.8371
      0.5886
      0.3429
      0.2113
      0.6305
      0.9374
      0.3462
      0.5357
      0.7705
   
   C2 = 
      0.7555
      0.3279
      0.2308
      0.4834
      0.5386
      0.8430
      0.1074
      0.7033
      0.1668
      0.7852
   
   M = 
      0.7205    0.7555
      0.8371    0.3279
      0.5886    0.2308
      0.3429    0.4834
      0.2113    0.5386
      0.6305    0.8430
      0.9374    0.1074
      0.3462    0.7033
      0.5357    0.1668
      0.7705    0.7852
   


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
      0.7293    0.7578    0.1697    0.2646
   
   R2 = 
      0.6161    0.6237    0.0752    0.3414
   
   M = 
      0.7293    0.7578    0.1697    0.2646
      0.6161    0.6237    0.0752    0.3414
   
   C1 = 
      0.2585
      0.7693
      0.0529
      0.2343
      0.3239
      0.7968
      0.3978
      0.9924
      0.2020
      0.4757
   
   C2 = 
      0.4938
      0.4659
   
   C3 = 
      0.2585
      0.7693
      0.0529
      0.2343
      0.3239
      0.7968
      0.3978
      0.9924
      0.2020
      0.4757
      0.4938
      0.4659
   

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
   

