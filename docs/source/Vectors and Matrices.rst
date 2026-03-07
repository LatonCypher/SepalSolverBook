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
      0.9965    0.4844    0.3684    0.6943    0.3917    0.6972    0.8465
   
   C = 
      0.5070
      0.1415
      0.3800
      0.1638
      0.0622
   
   M = 
      0.1493    0.6206    0.2224    0.1935    0.1242    0.9859    0.1910
      0.5790    0.6627    0.2737    0.5646    0.4446    0.9380    0.0261
      0.1009    0.0050    0.0771    0.6294    0.7214    0.1283    0.9440
      0.2968    0.3994    0.2485    0.2912    0.9822    0.1031    0.6145
      0.8590    0.0314    0.4847    0.2081    0.6043    0.3622    0.6894
      0.5540    0.9966    0.4171    0.8350    0.8286    0.2805    0.3553
      0.8869    0.9213    0.5583    0.9019    0.9940    0.8208    0.8957
      0.6952    0.4038    0.5643    0.4983    0.5803    0.4595    0.8931
   

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
      0.8961    0.5108    0.4174    0.7763
   
   R2 = 
      0.3971    0.3861    0.6260    0.7188    0.6801
   
   R3 = 
      0.8961    0.5108    0.4174    0.7763    0.3971    0.3861    0.6260    0.7188    0.6801
   
   C1 = 
      0.5009
      0.8659
      0.7696
      0.9058
      0.6406
      0.2961
      0.0949
      0.9323
      0.1641
      0.0956
   
   C2 = 
      0.4120
      0.1688
      0.1598
      0.2910
      0.4101
      0.9594
      0.0724
      0.5793
      0.9589
      0.9531
   
   M = 
      0.5009    0.4120
      0.8659    0.1688
      0.7696    0.1598
      0.9058    0.2910
      0.6406    0.4101
      0.2961    0.9594
      0.0949    0.0724
      0.9323    0.5793
      0.1641    0.9589
      0.0956    0.9531
   


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
      0.7033    0.9738    0.9945    0.8154
   
   R2 = 
      0.6270    0.9768    0.0101    0.6733
   
   M = 
      0.7033    0.9738    0.9945    0.8154
      0.6270    0.9768    0.0101    0.6733
   
   C1 = 
      0.4644
      0.9600
      0.6954
      0.1019
      0.6888
      0.7035
      0.1558
      0.9151
      0.8655
      0.0277
   
   C2 = 
      0.4774
      0.4544
   
   C3 = 
      0.4644
      0.9600
      0.6954
      0.1019
      0.6888
      0.7035
      0.1558
      0.9151
      0.8655
      0.0277
      0.4774
      0.4544
   

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
   

