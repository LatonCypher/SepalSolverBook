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
      0.3780    0.3117    0.7418    0.1895    0.3754    0.7963    0.6074
   
   C = 
      0.2464
      0.8910
      0.6529
      0.0367
      0.2242
   
   M = 
      0.1659    0.1363    0.6718    0.0691    0.1226    0.7617    0.6358
      0.0302    0.8872    0.3080    0.1508    0.9571    0.7186    0.4835
      0.6341    0.8511    0.5880    0.7955    0.8888    0.3530    0.2865
      0.9346    0.1304    0.4066    0.5440    0.5054    0.8938    0.4920
      0.2325    0.1615    0.1127    0.3462    0.4241    0.9987    0.7960
      0.5884    0.8615    0.3407    0.4021    0.1388    0.8689    0.7953
      0.8826    0.9268    0.1131    0.4707    0.0971    0.3399    0.6301
      0.9902    0.6664    0.6779    0.1424    0.2599    0.5979    0.5291
   

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
      0.9374    0.0304    0.7292    0.9103
   
   R2 = 
      0.0888    0.5474    0.0232    0.5551    0.6152
   
   R3 = 
      0.9374    0.0304    0.7292    0.9103    0.0888    0.5474    0.0232    0.5551    0.6152
   
   C1 = 
      0.3352
      0.8005
      0.2933
      0.3190
      0.6649
      0.5387
      0.9601
      0.0878
      0.5661
      0.5564
   
   C2 = 
      0.4066
      0.2252
      0.9604
      0.2908
      0.4566
      0.8836
      0.7125
      0.5440
      0.9824
      0.5859
   
   M = 
      0.3352    0.4066
      0.8005    0.2252
      0.2933    0.9604
      0.3190    0.2908
      0.6649    0.4566
      0.5387    0.8836
      0.9601    0.7125
      0.0878    0.5440
      0.5661    0.9824
      0.5564    0.5859
   


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
      0.7492    0.3066    0.3837    0.6532
   
   R2 = 
      0.6167    0.3538    0.4695    0.7460
   
   M = 
      0.7492    0.3066    0.3837    0.6532
      0.6167    0.3538    0.4695    0.7460
   
   C1 = 
      0.0581
      0.9909
      0.4372
      0.5221
      0.8877
      0.9809
      0.6344
      0.5692
      0.7243
      0.3134
   
   C2 = 
      0.6306
      0.8046
   
   C3 = 
      0.0581
      0.9909
      0.4372
      0.5221
      0.8877
      0.9809
      0.6344
      0.5692
      0.7243
      0.3134
      0.6306
      0.8046
   

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
   

