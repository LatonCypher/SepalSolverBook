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
      0.1055    0.4552    0.7883    0.2535    0.4154    0.2621    0.9609
   
   C = 
      0.9975
      0.1854
      0.7608
      0.5034
      0.3729
   
   M = 
      0.2510    0.3573    0.1696    0.6245    0.1364    0.8627    0.7786
      0.3581    0.9757    0.0266    0.3504    0.0730    0.7240    0.9774
      0.2097    0.8682    0.6920    0.1339    0.6865    0.2079    0.2240
      0.0484    0.8061    0.3278    0.8128    0.2799    0.6799    0.6056
      0.5582    0.3669    0.7706    0.3694    0.0163    0.7279    0.2126
      0.0780    0.7715    0.0868    0.9311    0.3978    0.4765    0.8228
      0.3723    0.5563    0.2665    0.8445    0.5752    0.2060    0.0762
      0.4351    0.6480    0.6910    0.3117    0.9190    0.4786    0.7251
   

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
      0.0542    0.2756    0.3420    0.2002
   
   R2 = 
      0.4021    0.7418    0.0486    0.8917    0.5770
   
   R3 = 
      0.0542    0.2756    0.3420    0.2002    0.4021    0.7418    0.0486    0.8917    0.5770
   
   C1 = 
      0.8365
      0.3143
      0.4530
      0.1227
      0.0142
      0.9886
      0.5751
      0.9836
      0.4851
      0.4903
   
   C2 = 
      0.3157
      0.7123
      0.4344
      0.9683
      0.0670
      0.5888
      0.2496
      0.6120
      0.5864
      0.4516
   
   M = 
      0.8365    0.3157
      0.3143    0.7123
      0.4530    0.4344
      0.1227    0.9683
      0.0142    0.0670
      0.9886    0.5888
      0.5751    0.2496
      0.9836    0.6120
      0.4851    0.5864
      0.4903    0.4516
   


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
      0.9604    0.9388    0.2120    0.3553
   
   R2 = 
      0.6201    0.8744    0.8297    0.7284
   
   M = 
      0.9604    0.9388    0.2120    0.3553
      0.6201    0.8744    0.8297    0.7284
   
   C1 = 
      0.4563
      0.4717
      0.6384
      0.6689
      0.2240
      0.6844
      0.8275
      0.8523
      0.7654
      0.9042
   
   C2 = 
      0.4210
      0.0141
   
   C3 = 
      0.4563
      0.4717
      0.6384
      0.6689
      0.2240
      0.6844
      0.8275
      0.8523
      0.7654
      0.9042
      0.4210
      0.0141
   

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
   

