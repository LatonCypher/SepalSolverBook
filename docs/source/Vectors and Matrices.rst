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
      0.1719    0.7169    0.4278    0.4132    0.0380    0.4298    0.7926
   
   C = 
      0.1945
      0.4690
      0.4252
      0.2497
      0.5397
   
   M = 
      0.6037    0.3290    0.8176    0.5400    0.1831    0.6036    0.7689
      0.4859    0.5492    0.0441    0.1479    0.4544    0.9732    0.7558
      0.5056    0.3824    0.3301    0.9976    0.4115    0.9550    0.4003
      0.1162    0.3206    0.5038    0.6967    0.2244    0.6315    0.6906
      0.3810    0.7237    0.8522    0.6089    0.9067    0.3684    0.3333
      0.4466    0.3336    0.1609    0.1977    0.8319    0.2937    0.5628
      0.1521    0.3081    0.2425    0.4692    0.6090    0.2554    0.0999
      0.6983    0.4585    0.7771    0.3691    0.3695    0.3739    0.6197
   

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
      0.5480    0.8546    0.3579    0.0863
   
   R2 = 
      0.3451    0.4075    0.1714    0.2999    0.3172
   
   R3 = 
      0.5480    0.8546    0.3579    0.0863    0.3451    0.4075    0.1714    0.2999    0.3172
   
   C1 = 
      0.2236
      0.6536
      0.5507
      0.6532
      0.8703
      0.4024
      0.4642
      0.6002
      0.4168
      0.3468
   
   C2 = 
      0.0075
      0.5205
      0.1858
      0.9748
      0.3277
      0.6222
      0.6635
      0.7372
      0.7367
      0.9667
   
   M = 
      0.2236    0.0075
      0.6536    0.5205
      0.5507    0.1858
      0.6532    0.9748
      0.8703    0.3277
      0.4024    0.6222
      0.4642    0.6635
      0.6002    0.7372
      0.4168    0.7367
      0.3468    0.9667
   


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
      0.3690    0.6773    0.8141    0.4787
   
   R2 = 
      0.5432    0.1677    0.9185    0.9747
   
   M = 
      0.3690    0.6773    0.8141    0.4787
      0.5432    0.1677    0.9185    0.9747
   
   C1 = 
      0.0267
      0.8026
      0.1560
      0.1504
      0.3406
      0.6684
      0.4867
      0.9658
      0.7092
      0.4212
   
   C2 = 
      0.2787
      0.9092
   
   C3 = 
      0.0267
      0.8026
      0.1560
      0.1504
      0.3406
      0.6684
      0.4867
      0.9658
      0.7092
      0.4212
      0.2787
      0.9092
   

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
   

