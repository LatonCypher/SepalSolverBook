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
      0.0160    0.8649    0.3038    0.0409    0.1977    0.3154    0.3197
   
   C = 
      0.3037
      0.1117
      0.9979
      0.2635
      0.7814
   
   M = 
      0.3459    0.4707    0.4364    0.0418    0.6762    0.3409    0.8817
      0.5876    0.2057    0.8947    0.9805    0.2758    0.4530    0.4920
      0.3275    0.8960    0.4064    0.9460    0.9789    0.1644    0.9371
      0.9013    0.1878    0.6601    0.5203    0.0057    0.7322    0.9579
      0.1717    0.7702    0.4699    0.9043    0.0069    0.3266    0.6933
      0.9260    0.3754    0.6306    0.2948    0.6590    0.4999    0.5493
      0.2025    0.4146    0.2697    0.9136    0.1624    0.3898    0.6449
      0.3466    0.2211    0.9030    0.3146    0.0254    0.5659    0.0435
   

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
      0.2393    0.5910    0.0980    0.6237
   
   R2 = 
      0.2088    0.3255    0.5173    0.9315    0.4827
   
   R3 = 
      0.2393    0.5910    0.0980    0.6237    0.2088    0.3255    0.5173    0.9315    0.4827
   
   C1 = 
      0.4455
      0.0278
      0.4811
      0.4766
      0.7995
      0.7774
      0.0916
      0.4499
      0.4849
      0.6148
   
   C2 = 
      0.7891
      0.6331
      0.7752
      0.2923
      0.5006
      0.0830
      0.2433
      0.9044
      0.3082
      0.5284
   
   M = 
      0.4455    0.7891
      0.0278    0.6331
      0.4811    0.7752
      0.4766    0.2923
      0.7995    0.5006
      0.7774    0.0830
      0.0916    0.2433
      0.4499    0.9044
      0.4849    0.3082
      0.6148    0.5284
   


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
      0.3541    0.4273    0.4285    0.9906
   
   R2 = 
      0.1765    0.6365    0.7304    0.8809
   
   M = 
      0.3541    0.4273    0.4285    0.9906
      0.1765    0.6365    0.7304    0.8809
   
   C1 = 
      0.4198
      0.7732
      0.3939
      0.6736
      0.6902
      0.5751
      0.9969
      0.2342
      0.7566
      0.9547
   
   C2 = 
      0.7595
      0.2802
   
   C3 = 
      0.4198
      0.7732
      0.3939
      0.6736
      0.6902
      0.5751
      0.9969
      0.2342
      0.7566
      0.9547
      0.7595
      0.2802
   

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
   

