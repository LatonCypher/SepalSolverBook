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
      0.5907    0.8494    0.1934    0.7483    0.4377    0.5317    0.8803
   
   C = 
      0.0627
      0.2577
      0.2502
      0.5811
      0.7839
   
   M = 
      0.2862    0.6404    0.2286    0.9062    0.1334    0.2815    0.9379
      0.4744    0.0521    0.4877    0.0470    0.2948    0.7402    0.7124
      0.7971    0.3990    0.4084    0.5177    0.5528    0.6364    0.4851
      0.2233    0.8535    0.7129    0.0190    0.8244    0.3433    0.7472
      0.0718    0.5444    0.4712    0.9589    0.1766    0.9137    0.9435
      0.5154    0.2368    0.1640    0.8095    0.7392    0.7245    0.4306
      0.7808    0.2176    0.9483    0.6331    0.6227    0.1000    0.2478
      0.8201    0.1786    0.3397    0.0419    0.8758    0.5383    0.7280
   

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
      0.1477    0.0433    0.8642    0.6069
   
   R2 = 
      0.1334    0.4884    0.7602    0.2550    0.1335
   
   R3 = 
      0.1477    0.0433    0.8642    0.6069    0.1334    0.4884    0.7602    0.2550    0.1335
   
   C1 = 
      0.5014
      0.4815
      0.9043
      0.5963
      0.2639
      0.2050
      0.7777
      0.7707
      0.7955
      0.7905
   
   C2 = 
      0.3165
      0.8601
      0.9987
      0.5781
      0.8460
      0.3286
      0.5275
      0.4238
      0.6126
      0.6831
   
   M = 
      0.5014    0.3165
      0.4815    0.8601
      0.9043    0.9987
      0.5963    0.5781
      0.2639    0.8460
      0.2050    0.3286
      0.7777    0.5275
      0.7707    0.4238
      0.7955    0.6126
      0.7905    0.6831
   


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
      0.2760    0.3254    0.9533    0.5242
   
   R2 = 
      0.0990    0.4840    0.0672    0.6723
   
   M = 
      0.2760    0.3254    0.9533    0.5242
      0.0990    0.4840    0.0672    0.6723
   
   C1 = 
      0.9002
      0.7031
      0.4520
      0.4926
      0.5762
      0.2662
      0.2404
      0.0080
      0.5515
      0.9346
   
   C2 = 
      0.3246
      0.7416
   
   C3 = 
      0.9002
      0.7031
      0.4520
      0.4926
      0.5762
      0.2662
      0.2404
      0.0080
      0.5515
      0.9346
      0.3246
      0.7416
   

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
   

