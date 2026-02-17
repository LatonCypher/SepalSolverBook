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
      0.8054    0.6755    0.2346    0.8625    0.6190    0.9556    0.2778
   
   C = 
      0.6663
      0.8789
      0.3313
      0.5532
      0.6374
   
   M = 
      0.9149    0.6153    0.8576    0.5555    0.1757    0.8766    0.8032
      0.2713    0.4096    0.3677    0.4435    0.8285    0.8977    0.0130
      0.9280    0.4293    0.1716    0.8959    0.6641    0.8396    0.9609
      0.0159    0.3597    0.0948    0.4327    0.8785    0.7982    0.0844
      0.5519    0.9573    0.7033    0.8666    0.3499    0.5965    0.3663
      0.0027    0.1692    0.9247    0.4470    0.4346    0.7047    0.1401
      0.5625    0.8072    0.0613    0.2958    0.7397    0.0397    0.1893
      0.3951    0.2792    0.8606    0.8503    0.1753    0.3290    0.7285
   

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
      0.4197    0.5609    0.8642    0.1317
   
   R2 = 
      0.2666    0.3609    0.3725    0.6989    0.1431
   
   R3 = 
      0.4197    0.5609    0.8642    0.1317    0.2666    0.3609    0.3725    0.6989    0.1431
   
   C1 = 
      0.0310
      0.3439
      0.4666
      0.6074
      0.8064
      0.1071
      0.9690
      0.4872
      0.3181
      0.7237
   
   C2 = 
      0.0978
      0.8129
      0.4188
      0.4241
      0.5505
      0.1411
      0.0941
      0.2071
      0.6943
      0.3833
   
   M = 
      0.0310    0.0978
      0.3439    0.8129
      0.4666    0.4188
      0.6074    0.4241
      0.8064    0.5505
      0.1071    0.1411
      0.9690    0.0941
      0.4872    0.2071
      0.3181    0.6943
      0.7237    0.3833
   


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
      0.6745    0.9813    0.1366    0.7603
   
   R2 = 
      0.8350    0.6903    0.5087    0.0790
   
   M = 
      0.6745    0.9813    0.1366    0.7603
      0.8350    0.6903    0.5087    0.0790
   
   C1 = 
      0.0532
      0.7373
      0.6106
      0.1396
      0.2033
      0.2028
      0.4493
      0.6646
      0.5759
      0.6310
   
   C2 = 
      0.2077
      0.7976
   
   C3 = 
      0.0532
      0.7373
      0.6106
      0.1396
      0.2033
      0.2028
      0.4493
      0.6646
      0.5759
      0.6310
      0.2077
      0.7976
   

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
   

