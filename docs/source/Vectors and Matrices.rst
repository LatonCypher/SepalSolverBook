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
      0.8070    0.5522    0.8700    0.5110    0.9562    0.3319    0.3332
   
   C = 
      0.0135
      0.9434
      0.1197
      0.4346
      0.4332
   
   M = 
      0.5806    0.8673    0.2394    0.0000    0.7189    0.3402    0.2181
      0.5496    0.2387    0.7700    0.3972    0.5299    0.3839    0.5419
      0.6099    0.1176    0.0474    0.7000    0.8748    0.5063    0.6012
      0.6304    0.6124    0.4895    0.1138    0.0985    0.9493    0.0159
      0.3193    0.8706    0.5782    0.7997    0.5783    0.0632    0.3312
      0.6606    0.4909    0.1689    0.5112    0.4654    0.0140    0.2897
      0.2778    0.2799    0.0294    0.5301    0.5665    0.4324    0.8220
      0.4709    0.7061    0.9389    0.1524    0.3148    0.8523    0.7809
   

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
      0.9649    0.3704    0.4510    0.0507
   
   R2 = 
      0.1955    0.3288    0.9355    0.1947    0.9872
   
   R3 = 
      0.9649    0.3704    0.4510    0.0507    0.1955    0.3288    0.9355    0.1947    0.9872
   
   C1 = 
      0.2479
      0.5778
      0.7561
      0.9606
      0.1130
      0.9687
      0.5229
      0.5055
      0.2296
      0.6684
   
   C2 = 
      0.6796
      0.5504
      0.7549
      0.4525
      0.4527
      0.2260
      0.2439
      0.8729
      0.1688
      0.6809
   
   M = 
      0.2479    0.6796
      0.5778    0.5504
      0.7561    0.7549
      0.9606    0.4525
      0.1130    0.4527
      0.9687    0.2260
      0.5229    0.2439
      0.5055    0.8729
      0.2296    0.1688
      0.6684    0.6809
   


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
      0.4058    0.0277    0.7773    0.1518
   
   R2 = 
      0.9121    0.8101    0.5621    0.6680
   
   M = 
      0.4058    0.0277    0.7773    0.1518
      0.9121    0.8101    0.5621    0.6680
   
   C1 = 
      0.0006
      0.3431
      0.9468
      0.8614
      0.1421
      0.2755
      0.1850
      0.1607
      0.1658
      0.0755
   
   C2 = 
      0.9670
      0.8013
   
   C3 = 
      0.0006
      0.3431
      0.9468
      0.8614
      0.1421
      0.2755
      0.1850
      0.1607
      0.1658
      0.0755
      0.9670
      0.8013
   

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
   

