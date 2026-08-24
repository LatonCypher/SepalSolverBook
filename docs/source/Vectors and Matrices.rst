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
      0.8847    0.5827    0.5232    0.5755    0.6659    0.5593    0.4661
   
   C = 
      0.0197
      0.2658
      0.9799
      0.5752
      0.5349
   
   M = 
      0.8217    0.6955    0.9084    0.8751    0.7102    0.3438    0.1609
      0.9675    0.8091    0.6539    0.4702    0.5688    0.0025    0.9979
      0.3827    0.2249    0.3309    0.7329    0.6400    0.3382    0.6506
      0.4379    0.2580    0.9678    0.7253    0.3608    0.6225    0.0996
      0.3782    0.0851    0.4592    0.5109    0.2935    0.7565    0.5716
      0.4325    0.2030    0.7366    0.8690    0.5737    0.4007    0.4661
      0.9955    0.8653    0.5067    0.9536    0.5985    0.6185    0.7182
      0.2895    0.0506    0.3247    0.6564    0.5029    0.0376    0.1961
   

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
      0.9266    0.4727    0.6736    0.3988
   
   R2 = 
      0.4053    0.5066    0.1529    0.9324    0.1208
   
   R3 = 
      0.9266    0.4727    0.6736    0.3988    0.4053    0.5066    0.1529    0.9324    0.1208
   
   C1 = 
      0.3378
      0.0879
      0.4776
      0.7106
      0.0584
      0.4778
      0.0229
      0.3077
      0.6443
      0.1318
   
   C2 = 
      0.7086
      0.5213
      0.6097
      0.8407
      0.4503
      0.0810
      0.5963
      0.2543
      0.5502
      0.2725
   
   M = 
      0.3378    0.7086
      0.0879    0.5213
      0.4776    0.6097
      0.7106    0.8407
      0.0584    0.4503
      0.4778    0.0810
      0.0229    0.5963
      0.3077    0.2543
      0.6443    0.5502
      0.1318    0.2725
   


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
      0.1077    0.2718    0.6341    0.2334
   
   R2 = 
      0.1859    0.7949    0.1430    0.3069
   
   M = 
      0.1077    0.2718    0.6341    0.2334
      0.1859    0.7949    0.1430    0.3069
   
   C1 = 
      0.4281
      0.6478
      0.4010
      0.0475
      0.4217
      0.9133
      0.0092
      0.2739
      0.5804
      0.7181
   
   C2 = 
      0.2447
      0.4606
   
   C3 = 
      0.4281
      0.6478
      0.4010
      0.0475
      0.4217
      0.9133
      0.0092
      0.2739
      0.5804
      0.7181
      0.2447
      0.4606
   

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
   

