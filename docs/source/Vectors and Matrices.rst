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
      0.0151    0.9603    0.3872    0.6612    0.8760    0.1932    0.2661
   
   C = 
      0.1605
      0.4473
      0.0476
      0.5696
      0.7520
   
   M = 
      0.1618    0.8591    0.6788    0.5614    0.2731    0.4968    0.2682
      0.4865    0.0461    0.6319    0.8880    0.5754    0.0198    0.0456
      0.2531    0.8500    0.8890    0.0246    0.4633    0.2219    0.9425
      0.9604    0.3680    0.1009    0.6115    0.1895    0.2646    0.2798
      0.6671    0.2267    0.8891    0.7081    0.1320    0.8363    0.3940
      0.3998    0.6661    0.3425    0.7924    0.7175    0.5421    0.3889
      0.1854    0.0084    0.0447    0.9268    0.0446    0.6677    0.5458
      0.5655    0.8854    0.8182    0.2815    0.3816    0.3314    0.5352
   

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
      0.9589    0.6886    0.6559    0.3742
   
   R2 = 
      0.0673    0.0005    0.1431    0.7362    0.2189
   
   R3 = 
      0.9589    0.6886    0.6559    0.3742    0.0673    0.0005    0.1431    0.7362    0.2189
   
   C1 = 
      0.4090
      0.6038
      0.8254
      0.2879
      0.4713
      0.8586
      0.2499
      0.8658
      0.8138
      0.0113
   
   C2 = 
      0.5600
      0.8640
      0.7855
      0.2803
      0.6938
      0.4224
      0.6782
      0.7132
      0.5633
      0.8360
   
   M = 
      0.4090    0.5600
      0.6038    0.8640
      0.8254    0.7855
      0.2879    0.2803
      0.4713    0.6938
      0.8586    0.4224
      0.2499    0.6782
      0.8658    0.7132
      0.8138    0.5633
      0.0113    0.8360
   


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
      0.5309    0.9408    0.8458    0.4058
   
   R2 = 
      0.8595    0.1295    0.2394    0.1872
   
   M = 
      0.5309    0.9408    0.8458    0.4058
      0.8595    0.1295    0.2394    0.1872
   
   C1 = 
      0.4039
      0.5736
      0.8507
      0.1983
      0.2940
      0.6915
      0.9098
      0.6716
      0.7060
      0.2522
   
   C2 = 
      0.1856
      0.1110
   
   C3 = 
      0.4039
      0.5736
      0.8507
      0.1983
      0.2940
      0.6915
      0.9098
      0.6716
      0.7060
      0.2522
      0.1856
      0.1110
   

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
   

