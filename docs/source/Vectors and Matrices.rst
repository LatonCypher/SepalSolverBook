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
      0.3082    0.5529    0.4496    0.2369    0.5124    0.4900    0.7502
   
   C = 
      0.5702
      0.2090
      0.0049
      0.0926
      0.0918
   
   M = 
      0.8433    0.5374    0.4124    0.3842    0.2251    0.3890    0.4336
      0.7226    0.8157    0.7124    0.1158    0.2344    0.7699    0.4976
      0.0485    0.0027    0.1485    0.6335    0.4245    0.5265    0.6788
      0.1374    0.2155    0.1529    0.6093    0.7200    0.7756    0.6998
      0.1250    0.0020    0.9953    0.6319    0.2900    0.8806    0.7950
      0.8128    0.8447    0.9339    0.9153    0.9152    0.4586    0.6160
      0.0991    0.0605    0.8831    0.1263    0.5223    0.5990    0.1611
      0.3311    0.1542    0.6973    0.9732    0.5168    0.9295    0.6378
   

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
      0.9435    0.3534    0.1922    0.1882
   
   R2 = 
      0.6247    0.9128    0.8703    0.2622    0.8560
   
   R3 = 
      0.9435    0.3534    0.1922    0.1882    0.6247    0.9128    0.8703    0.2622    0.8560
   
   C1 = 
      0.9804
      0.8228
      0.7791
      0.2367
      0.0693
      0.1884
      0.8637
      0.3525
      0.2027
      0.1851
   
   C2 = 
      0.8443
      0.0153
      0.9487
      0.4831
      0.8334
      0.0734
      0.8422
      0.8483
      0.6354
      0.8307
   
   M = 
      0.9804    0.8443
      0.8228    0.0153
      0.7791    0.9487
      0.2367    0.4831
      0.0693    0.8334
      0.1884    0.0734
      0.8637    0.8422
      0.3525    0.8483
      0.2027    0.6354
      0.1851    0.8307
   


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
      0.1128    0.5616    0.9411    0.3507
   
   R2 = 
      0.3098    0.7309    0.2056    0.0004
   
   M = 
      0.1128    0.5616    0.9411    0.3507
      0.3098    0.7309    0.2056    0.0004
   
   C1 = 
      0.9987
      0.2956
      0.8587
      0.7234
      0.0154
      0.7733
      0.1849
      0.9640
      0.3568
      0.6376
   
   C2 = 
      0.0826
      0.9106
   
   C3 = 
      0.9987
      0.2956
      0.8587
      0.7234
      0.0154
      0.7733
      0.1849
      0.9640
      0.3568
      0.6376
      0.0826
      0.9106
   

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
   

