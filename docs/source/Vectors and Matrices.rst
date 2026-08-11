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
      0.0457    0.6960    0.4265    0.6258    0.0195    0.6637    0.2802
   
   C = 
      0.1069
      0.5139
      0.0396
      0.8509
      0.0506
   
   M = 
      0.0669    0.2257    0.6977    0.0690    0.2490    0.7512    0.3056
      0.5227    0.3331    0.4188    0.2127    0.7585    0.9675    0.9617
      0.7113    0.0117    0.9145    0.3837    0.9964    0.1398    0.4694
      0.9630    0.6640    0.7771    0.3040    0.7006    0.3404    0.2754
      0.2500    0.1260    0.9859    0.8027    0.8361    0.2660    0.7850
      0.7144    0.3085    0.9012    0.4632    0.7785    0.3182    0.0455
      0.4466    0.4830    0.9849    0.1941    0.7569    0.1367    0.3135
      0.2949    0.8514    0.8671    0.1497    0.9362    0.6226    0.8692
   

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
      0.3591    0.1926    0.8092    0.4359
   
   R2 = 
      0.2724    0.8096    0.9001    0.6489    0.7309
   
   R3 = 
      0.3591    0.1926    0.8092    0.4359    0.2724    0.8096    0.9001    0.6489    0.7309
   
   C1 = 
      0.5393
      0.5096
      0.4447
      0.8014
      0.3930
      0.1361
      0.3705
      0.3238
      0.9414
      0.7738
   
   C2 = 
      0.0474
      0.3411
      0.4769
      0.7558
      0.8398
      0.0422
      0.7442
      0.3258
      0.0278
      0.4722
   
   M = 
      0.5393    0.0474
      0.5096    0.3411
      0.4447    0.4769
      0.8014    0.7558
      0.3930    0.8398
      0.1361    0.0422
      0.3705    0.7442
      0.3238    0.3258
      0.9414    0.0278
      0.7738    0.4722
   


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
      0.9212    0.2251    0.8514    0.9139
   
   R2 = 
      0.5784    0.4112    0.2402    0.5039
   
   M = 
      0.9212    0.2251    0.8514    0.9139
      0.5784    0.4112    0.2402    0.5039
   
   C1 = 
      0.6822
      0.7920
      0.3569
      0.8008
      0.0231
      0.5465
      0.7862
      0.9023
      0.3141
      0.8245
   
   C2 = 
      0.2274
      0.9545
   
   C3 = 
      0.6822
      0.7920
      0.3569
      0.8008
      0.0231
      0.5465
      0.7862
      0.9023
      0.3141
      0.8245
      0.2274
      0.9545
   

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
   

