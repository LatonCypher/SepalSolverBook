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
      0.3276    0.0616    0.3859    0.7386    0.0290    0.1039    0.7715
   
   C = 
      0.5037
      0.1030
      0.6961
      0.9116
      0.6527
   
   M = 
      0.3682    0.3528    0.6462    0.8240    0.3216    0.1975    0.1593
      0.5704    0.9569    0.9402    0.3881    0.2263    0.3488    0.0305
      0.3960    0.7266    0.5963    0.2309    0.0905    0.9619    0.2928
      0.8486    0.9430    0.2739    0.3921    0.4114    0.3297    0.7517
      0.2918    0.1308    0.6905    0.8052    0.8388    0.4244    0.4963
      0.8395    0.4691    0.9830    0.9276    0.4949    0.3640    0.4407
      0.2784    0.3192    0.8326    0.5100    0.8795    0.5376    0.5431
      0.9323    0.3673    0.3025    0.9654    0.2101    0.7765    0.2931
   

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
      0.1237    0.6987    0.4740    0.3730
   
   R2 = 
      0.5906    0.8409    0.2345    0.5904    0.3593
   
   R3 = 
      0.1237    0.6987    0.4740    0.3730    0.5906    0.8409    0.2345    0.5904    0.3593
   
   C1 = 
      0.1312
      0.4638
      0.9254
      0.8145
      0.3336
      0.4332
      0.8315
      0.3312
      0.6563
      0.6753
   
   C2 = 
      0.2724
      0.3107
      0.1007
      0.1683
      0.3400
      0.4169
      0.5856
      0.8125
      0.3021
      0.8497
   
   M = 
      0.1312    0.2724
      0.4638    0.3107
      0.9254    0.1007
      0.8145    0.1683
      0.3336    0.3400
      0.4332    0.4169
      0.8315    0.5856
      0.3312    0.8125
      0.6563    0.3021
      0.6753    0.8497
   


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
      0.6486    0.5500    0.4623    0.8250
   
   R2 = 
      0.1142    0.7484    0.6296    0.8685
   
   M = 
      0.6486    0.5500    0.4623    0.8250
      0.1142    0.7484    0.6296    0.8685
   
   C1 = 
      0.2740
      0.2507
      0.5528
      0.7157
      0.3896
      0.1758
      0.1641
      0.5295
      0.7884
      0.7999
   
   C2 = 
      0.9843
      0.8815
   
   C3 = 
      0.2740
      0.2507
      0.5528
      0.7157
      0.3896
      0.1758
      0.1641
      0.5295
      0.7884
      0.7999
      0.9843
      0.8815
   

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
   

