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
      0.3356    0.5224    0.2974    0.8039    0.1979    0.7305    0.2787
   
   C = 
      0.4680
      0.5352
      0.3512
      0.5763
      0.7220
   
   M = 
      0.3661    0.2837    0.0269    0.5309    0.1862    0.0116    0.8266
      0.5619    0.9796    0.9545    0.0435    0.0345    0.5488    0.9254
      0.0696    0.8701    0.5492    0.5910    0.3123    0.3687    0.1294
      0.1659    0.1726    0.7344    0.8860    0.3690    0.3597    0.3576
      0.0780    0.1579    0.4402    0.1586    0.6650    0.8255    0.5550
      0.2583    0.5154    0.3480    0.7153    0.1334    0.4484    0.2256
      0.4922    0.8090    0.4198    0.5706    0.7951    0.2638    0.7483
      0.0914    0.1310    0.4361    0.8992    0.7771    0.9868    0.0933
   

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
      0.9003    0.7844    0.9728    0.7656
   
   R2 = 
      0.6831    0.7899    0.6226    0.6729    0.7617
   
   R3 = 
      0.9003    0.7844    0.9728    0.7656    0.6831    0.7899    0.6226    0.6729    0.7617
   
   C1 = 
      0.2337
      0.9053
      0.1876
      0.4041
      0.2377
      0.8809
      0.7953
      0.5670
      0.4088
      0.2294
   
   C2 = 
      0.8666
      0.0536
      0.0858
      0.7899
      0.0366
      0.3508
      0.1920
      0.2079
      0.1424
      0.5751
   
   M = 
      0.2337    0.8666
      0.9053    0.0536
      0.1876    0.0858
      0.4041    0.7899
      0.2377    0.0366
      0.8809    0.3508
      0.7953    0.1920
      0.5670    0.2079
      0.4088    0.1424
      0.2294    0.5751
   


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
      0.4345    0.0754    0.4229    0.8739
   
   R2 = 
      0.5738    0.3132    0.5466    0.5484
   
   M = 
      0.4345    0.0754    0.4229    0.8739
      0.5738    0.3132    0.5466    0.5484
   
   C1 = 
      0.8749
      0.4062
      0.4020
      0.4078
      0.2409
      0.5982
      0.4990
      0.3497
      0.7836
      0.0407
   
   C2 = 
      0.7658
      0.8250
   
   C3 = 
      0.8749
      0.4062
      0.4020
      0.4078
      0.2409
      0.5982
      0.4990
      0.3497
      0.7836
      0.0407
      0.7658
      0.8250
   

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
   

