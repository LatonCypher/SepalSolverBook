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
      0.5090    0.6214    0.1813    0.9419    0.2934    0.0683    0.4508
   
   C = 
      0.7249
      0.2276
      0.0236
      0.9160
      0.9077
   
   M = 
      0.7990    0.3068    0.2744    0.6204    0.2292    0.5759    0.5515
      0.9366    0.8835    0.7762    0.1044    0.1769    0.9286    0.8072
      0.2094    0.7917    0.0755    0.6855    0.0537    0.9964    0.0748
      0.3498    0.6538    0.1688    0.0272    0.8902    0.5304    0.8751
      0.5661    0.1337    0.7052    0.7390    0.7617    0.3568    0.5218
      0.5915    0.6260    0.9698    0.5457    0.3134    0.0868    0.6500
      0.0024    0.6652    0.5787    0.9938    0.5262    0.3962    0.1266
      0.4545    0.1473    0.3689    0.3617    0.0947    0.9629    0.0844
   

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
      0.4410    0.1039    0.9357    0.3631
   
   R2 = 
      0.9910    0.3831    0.0707    0.3953    0.8177
   
   R3 = 
      0.4410    0.1039    0.9357    0.3631    0.9910    0.3831    0.0707    0.3953    0.8177
   
   C1 = 
      0.8820
      0.5961
      0.5867
      0.8405
      0.8846
      0.2967
      0.3389
      0.2858
      0.9688
      0.5117
   
   C2 = 
      0.5375
      0.0834
      0.8818
      0.2606
      0.6647
      0.3237
      0.8489
      0.3072
      0.6751
      0.4502
   
   M = 
      0.8820    0.5375
      0.5961    0.0834
      0.5867    0.8818
      0.8405    0.2606
      0.8846    0.6647
      0.2967    0.3237
      0.3389    0.8489
      0.2858    0.3072
      0.9688    0.6751
      0.5117    0.4502
   


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
      0.3960    0.4180    0.6317    0.5398
   
   R2 = 
      0.1125    0.8722    0.8923    0.1231
   
   M = 
      0.3960    0.4180    0.6317    0.5398
      0.1125    0.8722    0.8923    0.1231
   
   C1 = 
      0.7158
      0.2499
      0.9697
      0.8580
      0.1313
      0.0743
      0.3668
      0.3642
      0.0601
      0.3602
   
   C2 = 
      0.6752
      0.0329
   
   C3 = 
      0.7158
      0.2499
      0.9697
      0.8580
      0.1313
      0.0743
      0.3668
      0.3642
      0.0601
      0.3602
      0.6752
      0.0329
   

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
   

