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
      0.0901    0.5629    0.7078    0.8190    0.9389    0.3904    0.8586
   
   C = 
      0.9652
      0.9656
      0.7733
      0.0083
      0.6287
   
   M = 
      0.5882    0.2210    0.0132    0.9545    0.9924    0.6402    0.7649
      0.5998    0.7188    0.9469    0.2303    0.2554    0.5929    0.5631
      0.9459    0.6372    0.1845    0.7557    0.8849    0.9954    0.9403
      0.2042    0.0213    0.8513    0.7299    0.7041    0.1264    0.1948
      0.8386    0.7674    0.6271    0.5217    0.3597    0.2330    0.6771
      0.2370    0.2784    0.8882    0.3548    0.0437    0.8658    0.6634
      0.8013    0.0224    0.5649    0.7634    0.6859    0.3996    0.1319
      0.0118    0.6004    0.3802    0.9295    0.2642    0.7050    0.0573
   

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
      0.2581    0.1191    0.0729    0.9279
   
   R2 = 
      0.7590    0.0302    0.4201    0.1300    0.1411
   
   R3 = 
      0.2581    0.1191    0.0729    0.9279    0.7590    0.0302    0.4201    0.1300    0.1411
   
   C1 = 
      0.7889
      0.7261
      0.8535
      0.8657
      0.6998
      0.5606
      0.1033
      0.0380
      0.4726
      0.8254
   
   C2 = 
      0.8589
      0.7152
      0.9396
      0.8654
      0.1729
      0.7018
      0.4987
      0.1676
      0.5575
      0.4747
   
   M = 
      0.7889    0.8589
      0.7261    0.7152
      0.8535    0.9396
      0.8657    0.8654
      0.6998    0.1729
      0.5606    0.7018
      0.1033    0.4987
      0.0380    0.1676
      0.4726    0.5575
      0.8254    0.4747
   


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
      0.6397    0.2494    0.7602    0.8118
   
   R2 = 
      0.8570    0.0646    0.7356    0.9324
   
   M = 
      0.6397    0.2494    0.7602    0.8118
      0.8570    0.0646    0.7356    0.9324
   
   C1 = 
      0.6754
      0.8191
      0.2588
      0.4355
      0.7888
      0.4255
      0.8389
      0.8257
      0.8648
      0.9095
   
   C2 = 
      0.6885
      0.2949
   
   C3 = 
      0.6754
      0.8191
      0.2588
      0.4355
      0.7888
      0.4255
      0.8389
      0.8257
      0.8648
      0.9095
      0.6885
      0.2949
   

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
   

