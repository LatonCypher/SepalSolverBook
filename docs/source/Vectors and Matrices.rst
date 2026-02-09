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
      0.0751    0.0209    0.0532    0.2488    0.3819    0.3519    0.9144
   
   C = 
      0.2449
      0.9055
      0.7375
      0.9086
      0.4914
   
   M = 
      0.2319    0.6620    0.8159    0.7009    0.9243    0.9312    0.9319
      0.2201    0.9763    0.9694    0.4996    0.3876    0.8890    0.3281
      0.7721    0.6121    0.5634    0.4753    0.8230    0.8856    0.3648
      0.2271    0.2424    0.8151    0.7469    0.5226    0.6804    0.5257
      0.3331    0.0845    0.7409    0.8372    0.4019    0.4931    0.5569
      0.7358    0.2107    0.7678    0.6045    0.6306    0.7542    0.1594
      0.5042    0.0193    0.1896    0.9671    0.8766    0.3849    0.9299
      0.3077    0.5474    0.6396    0.6160    0.3336    0.2746    0.2696
   

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
      0.4383    0.8070    0.5478    0.4300
   
   R2 = 
      0.2119    0.8598    0.4685    0.1033    0.8177
   
   R3 = 
      0.4383    0.8070    0.5478    0.4300    0.2119    0.8598    0.4685    0.1033    0.8177
   
   C1 = 
      0.2342
      0.0423
      0.6658
      0.7802
      0.9242
      0.8629
      0.3277
      0.1834
      0.9477
      0.1340
   
   C2 = 
      0.9041
      0.1325
      0.1574
      0.8536
      0.2199
      0.7307
      0.5266
      0.1792
      0.1944
      0.0586
   
   M = 
      0.2342    0.9041
      0.0423    0.1325
      0.6658    0.1574
      0.7802    0.8536
      0.9242    0.2199
      0.8629    0.7307
      0.3277    0.5266
      0.1834    0.1792
      0.9477    0.1944
      0.1340    0.0586
   


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
      0.4597    0.7457    0.9402    0.9091
   
   R2 = 
      0.2167    0.1657    0.4394    0.2273
   
   M = 
      0.4597    0.7457    0.9402    0.9091
      0.2167    0.1657    0.4394    0.2273
   
   C1 = 
      0.0490
      0.9677
      0.6898
      0.6873
      0.6305
      0.1251
      0.3521
      0.5935
      0.7634
      0.7181
   
   C2 = 
      0.5016
      0.9269
   
   C3 = 
      0.0490
      0.9677
      0.6898
      0.6873
      0.6305
      0.1251
      0.3521
      0.5935
      0.7634
      0.7181
      0.5016
      0.9269
   

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
   

