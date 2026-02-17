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
      0.1839    0.5891    0.6326    0.7399    0.6608    0.0432    0.2101
   
   C = 
      0.1146
      0.0135
      0.0830
      0.2884
      0.5106
   
   M = 
      0.9521    0.3405    0.1861    0.2349    0.2370    0.9410    0.0988
      0.6998    0.1301    0.9357    0.2359    0.6048    0.3605    0.4295
      0.6191    0.6654    0.6263    0.2452    0.8105    0.2039    0.5761
      0.3573    0.9979    0.7998    0.1437    0.2350    0.5847    0.8689
      0.4983    0.2791    0.3408    0.4944    0.9151    0.4256    0.8061
      0.9299    0.4384    0.3239    0.5990    0.9213    0.4502    0.5894
      0.4108    0.4773    0.1794    0.4153    0.3987    0.3574    0.6393
      0.3296    0.3054    0.6002    0.7367    0.0427    0.1392    0.6900
   

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
      0.9050    0.7798    0.1646    0.5996
   
   R2 = 
      0.3381    0.7115    0.7890    0.6245    0.8242
   
   R3 = 
      0.9050    0.7798    0.1646    0.5996    0.3381    0.7115    0.7890    0.6245    0.8242
   
   C1 = 
      0.6742
      0.2722
      0.5258
      0.7005
      0.8595
      0.4133
      0.3698
      0.7361
      0.5622
      0.8196
   
   C2 = 
      0.6180
      0.7473
      0.1646
      0.2301
      0.6443
      0.2806
      0.8729
      0.8482
      0.4080
      0.5143
   
   M = 
      0.6742    0.6180
      0.2722    0.7473
      0.5258    0.1646
      0.7005    0.2301
      0.8595    0.6443
      0.4133    0.2806
      0.3698    0.8729
      0.7361    0.8482
      0.5622    0.4080
      0.8196    0.5143
   


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
      0.3938    0.1662    0.0202    0.0481
   
   R2 = 
      0.3665    0.4803    0.1326    0.3576
   
   M = 
      0.3938    0.1662    0.0202    0.0481
      0.3665    0.4803    0.1326    0.3576
   
   C1 = 
      0.6923
      0.4511
      0.1682
      0.5043
      0.7755
      0.4254
      0.0565
      0.5455
      0.5237
      0.6496
   
   C2 = 
      0.9828
      0.6038
   
   C3 = 
      0.6923
      0.4511
      0.1682
      0.5043
      0.7755
      0.4254
      0.0565
      0.5455
      0.5237
      0.6496
      0.9828
      0.6038
   

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
   

