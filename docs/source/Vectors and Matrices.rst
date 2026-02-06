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
      0.5525    0.4344    0.9079    0.8528    0.9116    0.2498    0.3344
   
   C = 
      0.9594
      0.4278
      0.7567
      0.3109
      0.6147
   
   M = 
      0.7034    0.3000    0.2711    0.5618    0.9241    0.4377    0.7434
      0.2535    0.2514    0.9643    0.3719    0.0188    0.3081    0.7724
      0.9033    0.6640    0.0990    0.1847    0.6713    0.0198    0.7819
      0.3993    0.6575    0.5779    0.8839    0.8135    0.5412    0.7419
      0.0876    0.4676    0.7737    0.0774    0.0478    0.9850    0.9303
      0.4354    0.7371    0.3309    0.3891    0.0093    0.7446    0.9159
      0.6392    0.9806    0.7451    0.0296    0.4357    0.9388    0.1102
      0.8377    0.4552    0.0992    0.1388    0.2305    0.8903    0.1502
   

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
      0.5673    0.8610    0.6896    0.8592
   
   R2 = 
      0.0682    0.7298    0.2033    0.0512    0.3846
   
   R3 = 
      0.5673    0.8610    0.6896    0.8592    0.0682    0.7298    0.2033    0.0512    0.3846
   
   C1 = 
      0.9667
      0.0886
      0.3452
      0.5355
      0.7328
      0.0311
      0.3059
      0.3332
      0.6365
      0.7237
   
   C2 = 
      0.6342
      0.0990
      0.5924
      0.8598
      0.2733
      0.7304
      0.2942
      0.8666
      0.4479
      0.8850
   
   M = 
      0.9667    0.6342
      0.0886    0.0990
      0.3452    0.5924
      0.5355    0.8598
      0.7328    0.2733
      0.0311    0.7304
      0.3059    0.2942
      0.3332    0.8666
      0.6365    0.4479
      0.7237    0.8850
   


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
      0.9163    0.7411    0.9419    0.8571
   
   R2 = 
      0.3962    0.1424    0.7790    0.2712
   
   M = 
      0.9163    0.7411    0.9419    0.8571
      0.3962    0.1424    0.7790    0.2712
   
   C1 = 
      0.2063
      0.0124
      0.8309
      0.0267
      0.0806
      0.8064
      0.0554
      0.5875
      0.9418
      0.8720
   
   C2 = 
      0.6358
      0.4135
   
   C3 = 
      0.2063
      0.0124
      0.8309
      0.0267
      0.0806
      0.8064
      0.0554
      0.5875
      0.9418
      0.8720
      0.6358
      0.4135
   

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
   

