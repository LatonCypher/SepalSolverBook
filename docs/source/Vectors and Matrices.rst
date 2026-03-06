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
      0.6132    0.7387    0.3797    0.3991    0.5226    0.6452    0.5864
   
   C = 
      0.7547
      0.1162
      0.5823
      0.3089
      0.5974
   
   M = 
      0.3393    0.2911    0.7138    0.0438    0.2980    0.0188    0.3811
      0.5434    0.5365    0.2709    0.5725    0.0912    0.4981    0.6875
      0.6199    0.6999    0.1910    0.2760    0.5377    0.4407    0.3457
      0.9995    0.5549    0.8748    0.8043    0.3509    0.4330    0.1704
      0.7652    0.1754    0.0260    0.0173    0.7527    0.7202    0.9039
      0.5060    0.1834    0.3426    0.1291    0.0612    0.6496    0.1399
      0.0222    0.4085    0.3352    0.8591    0.3964    0.8774    0.0145
      0.0241    0.3140    0.9218    0.8875    0.6346    0.8641    0.7769
   

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
      0.9240    0.6865    0.9721    0.9655
   
   R2 = 
      0.3111    0.8039    0.5466    0.7191    0.3626
   
   R3 = 
      0.9240    0.6865    0.9721    0.9655    0.3111    0.8039    0.5466    0.7191    0.3626
   
   C1 = 
      0.2241
      0.6199
      0.6549
      0.3619
      0.1660
      0.7689
      0.6758
      0.1215
      0.9557
      0.8040
   
   C2 = 
      0.6738
      0.0979
      0.7834
      0.4602
      0.8575
      0.1952
      0.8831
      0.6464
      0.1724
      0.3643
   
   M = 
      0.2241    0.6738
      0.6199    0.0979
      0.6549    0.7834
      0.3619    0.4602
      0.1660    0.8575
      0.7689    0.1952
      0.6758    0.8831
      0.1215    0.6464
      0.9557    0.1724
      0.8040    0.3643
   


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
      0.9942    0.7742    0.2263    0.0620
   
   R2 = 
      0.2029    0.7915    0.4666    0.4236
   
   M = 
      0.9942    0.7742    0.2263    0.0620
      0.2029    0.7915    0.4666    0.4236
   
   C1 = 
      0.5957
      0.8498
      0.8792
      0.1879
      0.0785
      0.2027
      0.9798
      0.2789
      0.9199
      0.7336
   
   C2 = 
      0.4693
      0.4046
   
   C3 = 
      0.5957
      0.8498
      0.8792
      0.1879
      0.0785
      0.2027
      0.9798
      0.2789
      0.9199
      0.7336
      0.4693
      0.4046
   

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
   

