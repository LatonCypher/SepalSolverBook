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
      0.3569    0.9817    0.9907    0.0281    0.0504    0.3256    0.5648
   
   C = 
      0.8437
      0.7023
      0.9610
      0.2895
      0.5173
   
   M = 
      0.9381    0.3250    0.4231    0.1618    0.9742    0.6153    0.5487
      0.8784    0.3481    0.1221    0.5145    0.5532    0.6748    0.0360
      0.9024    0.2472    0.8029    0.9428    0.9161    0.1649    0.9940
      0.6278    0.8277    0.6748    0.8211    0.4996    0.2851    0.5002
      0.0725    0.8100    0.7663    0.1121    0.9699    0.2441    0.3646
      0.8305    0.2503    0.3664    0.5101    0.8757    0.6105    0.5969
      0.5687    0.8056    0.7940    0.3769    0.5797    0.3091    0.6312
      0.2786    0.6151    0.0415    0.2079    0.4515    0.0361    0.9087
   

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
      0.2306    0.3641    0.8133    0.4261
   
   R2 = 
      0.1969    0.1459    0.0254    0.8951    0.1073
   
   R3 = 
      0.2306    0.3641    0.8133    0.4261    0.1969    0.1459    0.0254    0.8951    0.1073
   
   C1 = 
      0.6861
      0.2943
      0.3526
      0.8832
      0.1002
      0.1824
      0.1858
      0.6035
      0.0641
      0.5704
   
   C2 = 
      0.0555
      0.6225
      0.5315
      0.9243
      0.9495
      0.4952
      0.5576
      0.3663
      0.1714
      0.0170
   
   M = 
      0.6861    0.0555
      0.2943    0.6225
      0.3526    0.5315
      0.8832    0.9243
      0.1002    0.9495
      0.1824    0.4952
      0.1858    0.5576
      0.6035    0.3663
      0.0641    0.1714
      0.5704    0.0170
   


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
      0.1152    0.8973    0.0135    0.7322
   
   R2 = 
      0.2535    0.3810    0.1411    0.9477
   
   M = 
      0.1152    0.8973    0.0135    0.7322
      0.2535    0.3810    0.1411    0.9477
   
   C1 = 
      0.9877
      0.4142
      0.0077
      0.3152
      0.3495
      0.8264
      0.3406
      0.5809
      0.4716
      0.0705
   
   C2 = 
      0.8790
      0.5789
   
   C3 = 
      0.9877
      0.4142
      0.0077
      0.3152
      0.3495
      0.8264
      0.3406
      0.5809
      0.4716
      0.0705
      0.8790
      0.5789
   

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
   

