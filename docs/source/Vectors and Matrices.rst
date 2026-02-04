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
      0.8477    0.3879    0.1289    0.5570    0.0071    0.2229    0.8256
   
   C = 
      0.2696
      0.8572
      0.8039
      0.4805
      0.1448
   
   M = 
      0.2963    0.6079    0.5447    0.5044    0.8235    0.5227    0.0944
      0.5958    0.1016    0.4269    0.0642    0.0159    0.9482    0.6322
      0.7985    0.3050    0.4224    0.3186    0.5801    0.6789    0.2503
      0.9291    0.8155    0.7628    0.5123    0.7581    0.4271    0.8970
      0.4313    0.2171    0.6781    0.5496    0.7132    0.2510    0.4126
      0.7246    0.9100    0.8906    0.8446    0.4121    0.8929    0.2092
      0.0615    0.8762    0.9334    0.2137    0.7770    0.5743    0.4615
      0.6504    0.1191    0.7638    0.6709    0.1825    0.2279    0.7635
   

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
      0.3630    0.9515    0.8953    0.7828
   
   R2 = 
      0.9984    0.5612    0.4390    0.0522    0.7185
   
   R3 = 
      0.3630    0.9515    0.8953    0.7828    0.9984    0.5612    0.4390    0.0522    0.7185
   
   C1 = 
      0.1467
      0.9848
      0.2871
      0.3006
      0.2684
      0.7559
      0.1397
      0.6352
      0.6345
      0.3656
   
   C2 = 
      0.9824
      0.8760
      0.7285
      0.2827
      0.7918
      0.8697
      0.6071
      0.9281
      0.2363
      0.1854
   
   M = 
      0.1467    0.9824
      0.9848    0.8760
      0.2871    0.7285
      0.3006    0.2827
      0.2684    0.7918
      0.7559    0.8697
      0.1397    0.6071
      0.6352    0.9281
      0.6345    0.2363
      0.3656    0.1854
   


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
      0.9211    0.9971    0.4377    0.9947
   
   R2 = 
      0.8227    0.0351    0.4038    0.1453
   
   M = 
      0.9211    0.9971    0.4377    0.9947
      0.8227    0.0351    0.4038    0.1453
   
   C1 = 
      0.4421
      0.5083
      0.6217
      0.1917
      0.5427
      0.4363
      0.8572
      0.1487
      0.7004
      0.7512
   
   C2 = 
      0.7585
      0.0144
   
   C3 = 
      0.4421
      0.5083
      0.6217
      0.1917
      0.5427
      0.4363
      0.8572
      0.1487
      0.7004
      0.7512
      0.7585
      0.0144
   

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
   

