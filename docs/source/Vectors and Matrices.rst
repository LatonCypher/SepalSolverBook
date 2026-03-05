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
      0.1110    0.7805    0.7062    0.2808    0.6513    0.3025    0.9801
   
   C = 
      0.5227
      0.1669
      0.0793
      0.5208
      0.1364
   
   M = 
      0.9287    0.8435    0.2736    0.0580    0.3812    0.3596    0.4696
      0.3243    0.7497    0.8681    0.3642    0.8561    0.5653    0.7633
      0.7046    0.0758    0.3939    0.6695    0.4666    0.1086    0.9136
      0.7987    0.1913    0.5216    0.9158    0.6958    0.1930    0.7713
      0.8911    0.8586    0.5085    0.6666    0.0642    0.0766    0.4060
      0.9093    0.4499    0.7108    0.2035    0.6969    0.8470    0.5796
      0.4568    0.3927    0.6890    0.8861    0.3939    0.0287    0.8557
      0.6817    0.2810    0.3141    0.5320    0.5903    0.7885    0.0221
   

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
      0.0751    0.3628    0.9948    0.7358
   
   R2 = 
      0.4814    0.4267    0.9770    0.1303    0.6534
   
   R3 = 
      0.0751    0.3628    0.9948    0.7358    0.4814    0.4267    0.9770    0.1303    0.6534
   
   C1 = 
      0.9180
      0.3347
      0.4646
      0.2369
      0.4137
      0.6477
      0.9053
      0.3944
      0.5690
      0.7332
   
   C2 = 
      0.0560
      0.4102
      0.6744
      0.3278
      0.3570
      0.3513
      0.6515
      0.5783
      0.9688
      0.7932
   
   M = 
      0.9180    0.0560
      0.3347    0.4102
      0.4646    0.6744
      0.2369    0.3278
      0.4137    0.3570
      0.6477    0.3513
      0.9053    0.6515
      0.3944    0.5783
      0.5690    0.9688
      0.7332    0.7932
   


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
      0.7489    0.9590    0.4911    0.7151
   
   R2 = 
      0.3487    0.9484    0.4193    0.1948
   
   M = 
      0.7489    0.9590    0.4911    0.7151
      0.3487    0.9484    0.4193    0.1948
   
   C1 = 
      0.1324
      0.6978
      0.3367
      0.6363
      0.5155
      0.8748
      0.6424
      0.8862
      0.7377
      0.2005
   
   C2 = 
      0.5205
      0.7907
   
   C3 = 
      0.1324
      0.6978
      0.3367
      0.6363
      0.5155
      0.8748
      0.6424
      0.8862
      0.7377
      0.2005
      0.5205
      0.7907
   

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
   

