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
      0.7314    0.9430    0.4555    0.8738    0.7267    0.5162    0.8797
   
   C = 
      0.5731
      0.3002
      0.8201
      0.9624
      0.9908
   
   M = 
      0.5606    0.2327    0.7155    0.0389    0.6958    0.4839    0.3807
      0.7159    0.5426    0.2611    0.5528    0.0749    0.3544    0.4602
      0.6273    0.8507    0.1532    0.9713    0.3256    0.9042    0.4023
      0.9906    0.9912    0.2106    0.2379    0.0354    0.7717    0.6750
      0.9570    0.2877    0.2489    0.3338    0.6231    0.2937    0.8901
      0.8351    0.1811    1.0000    0.0712    0.7034    0.2994    0.0107
      0.4618    0.2622    0.6421    0.6046    0.7347    0.5145    0.5353
      0.3982    0.2658    0.2028    0.3233    0.5939    0.7348    0.6019
   

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
      0.5424    0.2346    0.8880    0.0744
   
   R2 = 
      0.3110    0.7599    0.4708    0.5780    0.2956
   
   R3 = 
      0.5424    0.2346    0.8880    0.0744    0.3110    0.7599    0.4708    0.5780    0.2956
   
   C1 = 
      0.3040
      0.0181
      0.3345
      0.3298
      0.7204
      0.4410
      0.1067
      0.8796
      0.0681
      0.3334
   
   C2 = 
      0.4760
      0.5291
      0.4844
      0.8011
      0.0801
      0.3343
      0.8706
      0.0245
      0.4521
      0.4706
   
   M = 
      0.3040    0.4760
      0.0181    0.5291
      0.3345    0.4844
      0.3298    0.8011
      0.7204    0.0801
      0.4410    0.3343
      0.1067    0.8706
      0.8796    0.0245
      0.0681    0.4521
      0.3334    0.4706
   


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
      0.1780    0.7713    0.0738    0.2662
   
   R2 = 
      0.2652    0.0749    0.5269    0.3290
   
   M = 
      0.1780    0.7713    0.0738    0.2662
      0.2652    0.0749    0.5269    0.3290
   
   C1 = 
      0.2921
      0.5356
      0.8591
      0.3798
      0.4059
      0.1411
      0.4767
      0.8319
      0.5415
      0.6004
   
   C2 = 
      0.2470
      0.2540
   
   C3 = 
      0.2921
      0.5356
      0.8591
      0.3798
      0.4059
      0.1411
      0.4767
      0.8319
      0.5415
      0.6004
      0.2470
      0.2540
   

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
   

