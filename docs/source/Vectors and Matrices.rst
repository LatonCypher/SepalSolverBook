Vectors and Matrices
====================

Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: RowVec, ColVec and Matrix. RowVec and ColVec are 1D arrays while Matrix is a 2D array. 

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
      5.0000    6.0000    7.0000    1.0000
   
   C = 
      8.0000
      3.0000
      4.0000
      2.0000
      7.0000
   
   M = 
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   


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
      0.5897    0.8513    0.9818    0.9692    0.7490    0.1148    0.1804
   
   C = 
      0.9601
      0.9091
      0.8581
      0.6052
      0.4189
   
   M = 
      0.6931    0.1479    0.8918    0.3757    0.6275    0.3241    0.5284
      0.5560    0.6094    0.6768    0.1075    0.4978    0.4208    0.9405
      0.3509    0.9418    0.7243    0.4734    0.2725    0.1421    0.5429
      0.6738    0.2274    0.0904    0.0833    0.9107    0.9786    0.7059
      0.1920    0.4588    0.4665    0.0793    0.6882    0.3951    0.0030
      0.8336    0.7671    0.9532    0.1043    0.6923    0.3069    0.8463
      0.3903    0.3618    0.6043    0.5025    0.3476    0.9507    0.9212
      0.3973    0.6079    0.3753    0.2050    0.9959    0.1068    0.7276
   

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
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   
   C = 
      1.0000
      1.0000
      1.0000
      1.0000
      1.0000
   
   M = 
      1.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    1.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    1.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    1.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    1.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    1.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    1.0000
   

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
      0.1624    0.9645    0.6467    0.3380
   
   R2 = 
      0.0748    0.2369    0.5874    0.7718    0.2468
   
   R3 = 
      0.1624    0.9645    0.6467    0.3380    0.0748    0.2369    0.5874    0.7718    0.2468
   
   C1 = 
      0.4076
      0.9933
      0.4763
      0.9297
      0.3636
      0.9145
      0.4820
      0.7014
      0.6194
      0.1242
   
   C2 = 
      0.7908
      0.2388
      0.5970
      0.5140
      0.5400
      0.7830
      0.1188
      0.2936
      0.8340
      0.4913
   
   M = 
      0.4076    0.7908
      0.9933    0.2388
      0.4763    0.5970
      0.9297    0.5140
      0.3636    0.5400
      0.9145    0.7830
      0.4820    0.1188
      0.7014    0.2936
      0.6194    0.8340
      0.1242    0.4913
   


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
      0.9949    0.9381    0.5761    0.3612
   
   R2 = 
      0.2398    0.1641    0.6331    0.2492
   
   M = 
      0.9949    0.9381    0.5761    0.3612
      0.2398    0.1641    0.6331    0.2492
   
   C1 = 
      0.4057
      0.4021
      0.9963
      0.6067
      0.3788
      0.5345
      0.7105
      0.4362
      0.9800
      0.6654
   
   C2 = 
      0.4698
      0.4153
   
   C3 = 
      0.4057
      0.4021
      0.9963
      0.6067
      0.3788
      0.5345
      0.7105
      0.4362
      0.9800
      0.6654
      0.4698
      0.4153
   

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
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   
   Flipud(M) = 
      0.0000    5.0000   -6.0000   -3.0000
      4.0000    8.0000    9.0000    1.0000
      2.0000    1.0000   -7.0000    3.0000
      5.0000   -2.0000    3.0000    7.0000
   
   Fliplr(M) = 
      7.0000    3.0000   -2.0000    5.0000
      3.0000   -7.0000    1.0000    2.0000
      1.0000    9.0000    8.0000    4.0000
     -3.0000   -6.0000    5.0000    0.0000
   

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
      5.0000   -2.0000    3.0000    7.0000
      0.0000    1.0000   -7.0000    3.0000
      0.0000    0.0000    9.0000    1.0000
      0.0000    0.0000    0.0000   -3.0000
   
   Tril(M) = 
      5.0000    0.0000    0.0000    0.0000
      2.0000    1.0000    0.0000    0.0000
      4.0000    8.0000    9.0000    0.0000
      0.0000    5.0000   -6.0000   -3.0000
   

