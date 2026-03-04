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
      0.7322    0.1464    0.6074    0.2045    0.5271    0.2495    0.3820
   
   C = 
      0.2413
      0.5712
      0.6701
      0.5032
      0.4656
   
   M = 
      0.5050    0.0315    0.5964    0.7890    0.9787    0.6050    0.9008
      0.0943    0.2856    0.5137    0.2865    0.7403    0.6335    0.2401
      0.3437    0.8031    0.3765    0.5862    0.8515    0.8037    0.9732
      0.1998    0.6932    0.4853    0.9025    0.6942    0.3973    0.7837
      0.3509    0.2223    0.6988    0.6417    0.3248    0.8673    0.3772
      0.4995    0.9529    0.3375    0.5191    0.7011    0.1881    0.8011
      0.1291    0.6484    0.9882    0.4556    0.4905    0.9925    0.1940
      0.4327    0.9926    0.7403    0.5113    0.1405    0.6809    0.0271
   

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
      0.8251    0.4465    0.4690    0.1756
   
   R2 = 
      0.4592    0.0119    0.6638    0.3782    0.8991
   
   R3 = 
      0.8251    0.4465    0.4690    0.1756    0.4592    0.0119    0.6638    0.3782    0.8991
   
   C1 = 
      0.8791
      0.0318
      0.8525
      0.6222
      0.3196
      0.0921
      0.1763
      0.4529
      0.2284
      0.0649
   
   C2 = 
      0.9435
      0.4312
      0.8556
      0.3507
      0.9913
      0.5742
      0.2613
      0.6014
      0.0890
      0.2705
   
   M = 
      0.8791    0.9435
      0.0318    0.4312
      0.8525    0.8556
      0.6222    0.3507
      0.3196    0.9913
      0.0921    0.5742
      0.1763    0.2613
      0.4529    0.6014
      0.2284    0.0890
      0.0649    0.2705
   


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
      0.3869    0.2441    0.1219    0.2898
   
   R2 = 
      0.1252    0.0405    0.3054    0.3624
   
   M = 
      0.3869    0.2441    0.1219    0.2898
      0.1252    0.0405    0.3054    0.3624
   
   C1 = 
      0.1509
      0.7141
      0.3070
      0.7801
      0.3067
      0.2386
      0.8644
      0.3226
      0.8227
      0.9658
   
   C2 = 
      0.5012
      0.3538
   
   C3 = 
      0.1509
      0.7141
      0.3070
      0.7801
      0.3067
      0.2386
      0.8644
      0.3226
      0.8227
      0.9658
      0.5012
      0.3538
   

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
   

