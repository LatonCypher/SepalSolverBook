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
      0.6110    0.5690    0.3921    0.4807    0.5833    0.4333    0.8248
   
   C = 
      0.9295
      0.5043
      0.5116
      0.8849
      0.6491
   
   M = 
      0.3572    0.5037    0.1761    0.9596    0.9902    0.8151    0.3733
      0.4907    0.7746    0.6327    0.5416    0.3410    0.4487    0.6808
      0.9340    0.1052    0.9963    0.0710    0.0110    0.3382    0.4879
      0.2843    0.3147    0.1020    0.1062    0.8597    0.7691    0.4675
      0.7021    0.8376    0.1257    0.1604    0.9168    0.8416    0.3505
      0.5489    0.4485    0.6534    0.1887    0.7228    0.8708    0.3165
      0.5200    0.6663    0.0700    0.5800    0.9872    0.9002    0.7624
      0.9173    0.2730    0.1538    0.5331    0.0432    0.6079    0.8856
   

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
      0.3300    0.9861    0.2418    0.0954
   
   R2 = 
      0.9762    0.5539    0.3894    0.2105    0.8210
   
   R3 = 
      0.3300    0.9861    0.2418    0.0954    0.9762    0.5539    0.3894    0.2105    0.8210
   
   C1 = 
      0.3448
      0.2203
      0.7218
      0.2007
      0.4472
      0.1445
      0.8644
      0.4501
      0.2814
      0.9800
   
   C2 = 
      0.3371
      0.0476
      0.7466
      0.2401
      0.3204
      0.0620
      0.9403
      0.3075
      0.6263
      0.9227
   
   M = 
      0.3448    0.3371
      0.2203    0.0476
      0.7218    0.7466
      0.2007    0.2401
      0.4472    0.3204
      0.1445    0.0620
      0.8644    0.9403
      0.4501    0.3075
      0.2814    0.6263
      0.9800    0.9227
   


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
      0.5473    0.9160    0.3395    0.7158
   
   R2 = 
      0.6831    0.1237    0.3933    0.1054
   
   M = 
      0.5473    0.9160    0.3395    0.7158
      0.6831    0.1237    0.3933    0.1054
   
   C1 = 
      0.0812
      0.9792
      0.6805
      0.6022
      0.8288
      0.4648
      0.1626
      0.0638
      0.2695
      0.1689
   
   C2 = 
      0.9292
      0.6688
   
   C3 = 
      0.0812
      0.9792
      0.6805
      0.6022
      0.8288
      0.4648
      0.1626
      0.0638
      0.2695
      0.1689
      0.9292
      0.6688
   

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
   

