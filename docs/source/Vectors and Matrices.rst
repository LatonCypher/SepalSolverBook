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
      0.3921    0.2413    0.2539    0.1062    0.8949    0.3095    0.1203
   
   C = 
      0.7028
      0.5727
      0.3684
      0.7747
      0.0742
   
   M = 
      0.1819    0.3832    0.2982    0.6065    0.5322    0.2814    0.6103
      0.7528    0.9015    0.6864    0.1573    0.0720    0.3558    0.8112
      0.1800    0.4537    0.5222    0.5367    0.7235    0.4681    0.5011
      0.6224    0.8968    0.9927    0.5009    0.2770    0.6794    0.1694
      0.4956    0.7296    0.3134    0.9238    0.1433    0.2011    0.0913
      0.2282    0.6746    0.3973    0.0688    0.3447    0.1343    0.7495
      0.0079    0.5311    0.6359    0.2518    0.3378    0.0797    0.8824
      0.9270    0.5837    0.5939    0.4033    0.0027    0.8082    0.4262
   

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
      0.2013    0.6344    0.7344    0.6665
   
   R2 = 
      0.0765    0.1270    0.1938    0.3694    0.1711
   
   R3 = 
      0.2013    0.6344    0.7344    0.6665    0.0765    0.1270    0.1938    0.3694    0.1711
   
   C1 = 
      0.0361
      0.3271
      0.3924
      0.2591
      0.9540
      0.3102
      0.3903
      0.4975
      0.1128
      0.9038
   
   C2 = 
      0.9088
      0.8373
      0.8781
      0.6844
      0.8572
      0.1119
      0.8277
      0.9579
      0.7569
      0.8754
   
   M = 
      0.0361    0.9088
      0.3271    0.8373
      0.3924    0.8781
      0.2591    0.6844
      0.9540    0.8572
      0.3102    0.1119
      0.3903    0.8277
      0.4975    0.9579
      0.1128    0.7569
      0.9038    0.8754
   


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
      0.3830    0.5205    0.1705    0.2577
   
   R2 = 
      0.3017    0.7246    0.8128    0.5212
   
   M = 
      0.3830    0.5205    0.1705    0.2577
      0.3017    0.7246    0.8128    0.5212
   
   C1 = 
      0.8293
      0.2760
      0.3247
      0.8660
      0.0678
      0.6130
      0.3196
      0.1925
      0.6368
      0.7969
   
   C2 = 
      0.7747
      0.3146
   
   C3 = 
      0.8293
      0.2760
      0.3247
      0.8660
      0.0678
      0.6130
      0.3196
      0.1925
      0.6368
      0.7969
      0.7747
      0.3146
   

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
   

