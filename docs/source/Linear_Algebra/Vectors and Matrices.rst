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
      0.8637    0.2738    0.9771    0.5146    0.4209    0.9651    0.5033
   
   C = 
      0.5720
      0.1499
      0.1553
      0.4940
      0.1552
   
   M = 
      0.9637    0.3525    0.8809    0.6440    0.0719    0.5056    0.6787
      0.9949    0.0459    0.5202    0.2191    0.3542    0.3362    0.5525
      0.2593    0.1202    0.2260    0.3708    0.7216    0.8974    0.8964
      0.6859    0.7413    0.2166    0.0173    0.4618    0.6321    0.6627
      0.3630    0.3765    0.1267    0.8171    0.3532    0.2220    0.2188
      0.1197    0.5592    0.6099    0.4906    0.1213    0.4374    0.2489
      0.3039    0.6775    0.6610    0.2390    0.0443    0.1190    0.4083
      0.2203    0.4492    0.9788    0.5827    0.3913    0.5848    0.3361
   

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
      0.8479    0.2093    0.5896    0.0943
   
   R2 = 
      0.1786    0.5574    0.7072    0.4583    0.2616
   
   R3 = 
      0.8479    0.2093    0.5896    0.0943    0.1786    0.5574    0.7072    0.4583    0.2616
   
   C1 = 
      0.8535
      0.3917
      0.7934
      0.7702
      0.7430
      0.2589
      0.1015
      0.4626
      0.3416
      0.0869
   
   C2 = 
      0.8124
      0.3100
      0.4197
      0.8859
      0.4358
      0.8202
      0.1616
      0.6934
      0.3541
      0.1516
   
   M = 
      0.8535    0.8124
      0.3917    0.3100
      0.7934    0.4197
      0.7702    0.8859
      0.7430    0.4358
      0.2589    0.8202
      0.1015    0.1616
      0.4626    0.6934
      0.3416    0.3541
      0.0869    0.1516
   


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
      0.4714    0.6065    0.4851    0.8199
   
   R2 = 
      0.9481    0.2645    0.0695    0.4438
   
   M = 
      0.4714    0.6065    0.4851    0.8199
      0.9481    0.2645    0.0695    0.4438
   
   C1 = 
      0.6484
      0.6341
      0.9686
      0.0946
      0.6324
      0.1619
      0.6133
      0.8941
      0.4618
      0.7924
   
   C2 = 
      0.5818
      0.4891
   
   C3 = 
      0.6484
      0.6341
      0.9686
      0.0946
      0.6324
      0.1619
      0.6133
      0.8941
      0.4618
      0.7924
      0.5818
      0.4891
   

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
   

