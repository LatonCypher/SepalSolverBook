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
      0.8982    0.9619    0.4857    0.1228    0.2614    0.2107    0.6705
   
   C = 
      0.3775
      0.7446
      0.4693
      0.7526
      0.4349
   
   M = 
      0.1933    0.7431    0.2185    0.7952    0.5362    0.0944    0.3227
      0.8512    0.5744    0.7912    0.4178    0.5225    0.7302    0.2293
      0.7483    0.2272    0.1841    0.7532    0.6664    0.8914    0.4308
      0.9194    0.7365    0.7180    0.5756    0.4135    0.1676    0.6368
      0.4937    0.5983    0.8997    0.7142    0.7140    0.0048    0.0350
      0.0856    0.4998    0.7234    0.1058    0.6548    0.2721    0.4740
      0.6807    0.9259    0.7793    0.7949    0.0280    0.1549    0.3071
      0.9563    0.7493    0.3944    0.1805    0.2946    0.0154    0.8835
   

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
      0.8851    0.1376    0.4866    0.1906
   
   R2 = 
      0.4813    0.2021    0.6392    0.1225    0.1101
   
   R3 = 
      0.8851    0.1376    0.4866    0.1906    0.4813    0.2021    0.6392    0.1225    0.1101
   
   C1 = 
      0.1098
      0.2710
      0.3032
      0.8064
      0.7879
      0.6666
      0.3586
      0.2738
      0.3635
      0.9823
   
   C2 = 
      0.3480
      0.7596
      0.0621
      0.0855
      0.7709
      0.2206
      0.5324
      0.2458
      0.9187
      0.5227
   
   M = 
      0.1098    0.3480
      0.2710    0.7596
      0.3032    0.0621
      0.8064    0.0855
      0.7879    0.7709
      0.6666    0.2206
      0.3586    0.5324
      0.2738    0.2458
      0.3635    0.9187
      0.9823    0.5227
   


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
      0.3397    0.7674    0.8061    0.3113
   
   R2 = 
      0.6959    0.0474    0.2171    0.3263
   
   M = 
      0.3397    0.7674    0.8061    0.3113
      0.6959    0.0474    0.2171    0.3263
   
   C1 = 
      0.5150
      0.7027
      0.5323
      0.5326
      0.5779
      0.5027
      0.7372
      0.7733
      0.5175
      0.3864
   
   C2 = 
      0.2739
      0.5765
   
   C3 = 
      0.5150
      0.7027
      0.5323
      0.5326
      0.5779
      0.5027
      0.7372
      0.7733
      0.5175
      0.3864
      0.2739
      0.5765
   

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
   

