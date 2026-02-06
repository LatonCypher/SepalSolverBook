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
      0.3479    0.9425    0.6510    0.9119    0.1324    0.2586    0.9170
   
   C = 
      0.3094
      0.3882
      0.4483
      0.5440
      0.3083
   
   M = 
      0.8344    0.7128    0.6404    0.1257    0.7837    0.1580    0.7470
      0.1508    0.6865    0.3862    0.8964    0.7281    0.0435    0.1762
      0.1471    0.6337    0.6199    0.2146    0.0831    0.3828    0.2128
      0.2288    0.7357    0.0717    0.8270    0.8323    0.5274    0.6229
      0.1508    0.9032    0.4507    0.6092    0.5214    0.4069    0.9474
      0.0823    0.4070    0.5799    0.3293    0.1560    0.2821    0.0142
      0.1144    0.8193    0.3836    0.7817    0.2270    0.3904    0.1302
      0.4761    0.8604    0.5079    0.2647    0.3718    0.3295    0.8016
   

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
      0.6292    0.8371    0.0027    0.4979
   
   R2 = 
      0.5869    0.6298    0.5425    0.7560    0.3168
   
   R3 = 
      0.6292    0.8371    0.0027    0.4979    0.5869    0.6298    0.5425    0.7560    0.3168
   
   C1 = 
      0.8804
      0.7322
      0.7870
      0.5627
      0.6056
      0.2282
      0.0002
      0.7017
      0.9596
      0.6809
   
   C2 = 
      0.5398
      0.1179
      0.7389
      0.5341
      0.8438
      0.2801
      0.3128
      0.7587
      0.7492
      0.3591
   
   M = 
      0.8804    0.5398
      0.7322    0.1179
      0.7870    0.7389
      0.5627    0.5341
      0.6056    0.8438
      0.2282    0.2801
      0.0002    0.3128
      0.7017    0.7587
      0.9596    0.7492
      0.6809    0.3591
   


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
      0.5078    0.7335    0.1040    0.0928
   
   R2 = 
      0.1592    0.3399    0.9049    0.4651
   
   M = 
      0.5078    0.7335    0.1040    0.0928
      0.1592    0.3399    0.9049    0.4651
   
   C1 = 
      0.9279
      0.6562
      0.5597
      0.7930
      0.4173
      0.5536
      0.3677
      0.0721
      0.7055
      0.8683
   
   C2 = 
      0.0470
      0.6991
   
   C3 = 
      0.9279
      0.6562
      0.5597
      0.7930
      0.4173
      0.5536
      0.3677
      0.0721
      0.7055
      0.8683
      0.0470
      0.6991
   

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
   

