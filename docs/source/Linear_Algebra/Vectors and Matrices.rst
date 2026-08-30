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
      0.7241    0.9434    0.1401    0.4182    0.8040    0.5372    0.7865
   
   C = 
      0.7731
      0.0597
      0.3874
      0.2783
      0.8926
   
   M = 
      0.6757    0.9444    0.1078    0.6408    0.3567    0.2492    0.0363
      0.8457    0.9841    0.3584    0.7786    0.1688    0.6890    0.0465
      0.0470    0.4908    0.3529    0.5167    0.7122    0.9410    0.3141
      0.6403    0.2486    0.4336    0.7152    0.0813    0.3445    0.3351
      0.5640    0.0497    0.2675    0.5076    0.0177    0.0879    0.9451
      0.8755    0.0931    0.6784    0.3017    0.0619    0.2421    0.6959
      0.7627    0.8103    0.2480    0.7437    0.7832    0.2113    0.9275
      0.7418    0.8856    0.6725    0.3042    0.8717    0.8157    0.2634
   

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
      0.1694    0.6915    0.1512    0.5960
   
   R2 = 
      0.2473    0.0258    0.7514    0.0859    0.1509
   
   R3 = 
      0.1694    0.6915    0.1512    0.5960    0.2473    0.0258    0.7514    0.0859    0.1509
   
   C1 = 
      0.3744
      0.2512
      0.1815
      0.1497
      0.8106
      0.7865
      0.7269
      0.8185
      0.9547
      0.0864
   
   C2 = 
      0.8879
      0.5188
      0.8071
      0.3605
      0.3133
      0.3730
      0.3794
      0.6738
      0.4178
      0.6570
   
   M = 
      0.3744    0.8879
      0.2512    0.5188
      0.1815    0.8071
      0.1497    0.3605
      0.8106    0.3133
      0.7865    0.3730
      0.7269    0.3794
      0.8185    0.6738
      0.9547    0.4178
      0.0864    0.6570
   


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
      0.9927    0.6604    0.1733    0.8793
   
   R2 = 
      0.0053    0.0283    0.8216    0.3880
   
   M = 
      0.9927    0.6604    0.1733    0.8793
      0.0053    0.0283    0.8216    0.3880
   
   C1 = 
      0.3792
      0.1332
      0.4641
      0.6148
      0.6243
      0.1250
      0.8764
      0.3778
      0.6085
      0.9469
   
   C2 = 
      0.3624
      0.3705
   
   C3 = 
      0.3792
      0.1332
      0.4641
      0.6148
      0.6243
      0.1250
      0.8764
      0.3778
      0.6085
      0.9469
      0.3624
      0.3705
   

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
   

