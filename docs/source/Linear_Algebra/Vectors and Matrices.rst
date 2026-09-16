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
      0.4275    0.0266    0.6313    0.1506    0.2269    0.4153    0.9863
   
   C = 
      0.1825
      0.5793
      0.3569
      0.3547
      0.9671
   
   M = 
      0.1810    0.3343    0.5778    0.8860    0.2213    0.0021    0.9611
      0.0833    0.3756    0.1895    0.0254    0.9950    0.6660    0.5767
      0.6241    0.2817    0.5149    0.4658    0.1313    0.2241    0.4858
      0.0953    0.8971    0.4539    0.3078    0.3825    0.7081    0.0890
      0.7249    0.4228    0.0160    0.0097    0.4804    0.6007    0.2523
      0.6190    0.3510    0.9007    0.1097    0.8074    0.5005    0.9100
      0.6503    0.0372    0.6140    0.4105    0.9552    0.4896    0.3966
      0.9730    0.2686    0.5991    0.9392    0.1857    0.4916    0.6468
   

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
      0.7958    0.5001    0.0313    0.7496
   
   R2 = 
      0.9225    0.3514    0.0565    0.1232    0.0386
   
   R3 = 
      0.7958    0.5001    0.0313    0.7496    0.9225    0.3514    0.0565    0.1232    0.0386
   
   C1 = 
      0.1218
      0.2569
      0.9946
      0.4240
      0.9930
      0.8805
      0.8741
      0.3668
      0.5650
      0.6421
   
   C2 = 
      0.6069
      0.9108
      0.4439
      0.9874
      0.0377
      0.6845
      0.1792
      0.6643
      0.6944
      0.5454
   
   M = 
      0.1218    0.6069
      0.2569    0.9108
      0.9946    0.4439
      0.4240    0.9874
      0.9930    0.0377
      0.8805    0.6845
      0.8741    0.1792
      0.3668    0.6643
      0.5650    0.6944
      0.6421    0.5454
   


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
      0.5764    0.7729    0.3917    0.9336
   
   R2 = 
      0.7472    0.3642    0.1534    0.3225
   
   M = 
      0.5764    0.7729    0.3917    0.9336
      0.7472    0.3642    0.1534    0.3225
   
   C1 = 
      0.5880
      0.8243
      0.2259
      0.6168
      0.6786
      0.4900
      0.4285
      0.8797
      0.9288
      0.6903
   
   C2 = 
      0.1104
      0.0312
   
   C3 = 
      0.5880
      0.8243
      0.2259
      0.6168
      0.6786
      0.4900
      0.4285
      0.8797
      0.9288
      0.6903
      0.1104
      0.0312
   

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
   

