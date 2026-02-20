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
      0.3867    0.4591    0.9948    0.4961    0.7374    0.3520    0.2106
   
   C = 
      0.8132
      0.8357
      0.6498
      0.6973
      0.5437
   
   M = 
      0.5091    0.4628    0.8456    0.3001    0.8907    0.4858    0.4007
      0.1021    0.2557    0.2943    0.1594    0.2327    0.0488    0.9278
      0.5999    0.0054    0.3123    0.8406    0.4336    0.5474    0.1454
      0.8550    0.4154    0.3324    0.1688    0.9801    0.1416    0.7202
      0.0998    0.3581    0.9076    0.0261    0.5111    0.1071    0.5401
      0.2463    0.2275    0.1258    0.9338    0.3925    0.5704    0.1773
      0.5803    0.5541    0.4873    0.6364    0.9621    0.6781    0.6255
      0.4778    0.4363    0.8041    0.9753    0.5459    0.8099    0.1762
   

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
      0.0256    0.4694    0.2094    0.8209
   
   R2 = 
      0.4377    0.3285    0.5147    0.6134    0.7902
   
   R3 = 
      0.0256    0.4694    0.2094    0.8209    0.4377    0.3285    0.5147    0.6134    0.7902
   
   C1 = 
      0.3712
      0.7222
      0.9205
      0.4117
      0.9937
      0.7738
      0.7271
      0.5627
      0.0064
      0.4690
   
   C2 = 
      0.9144
      0.0595
      0.6339
      0.3030
      0.4582
      0.6387
      0.7201
      0.6166
      0.7006
      0.7286
   
   M = 
      0.3712    0.9144
      0.7222    0.0595
      0.9205    0.6339
      0.4117    0.3030
      0.9937    0.4582
      0.7738    0.6387
      0.7271    0.7201
      0.5627    0.6166
      0.0064    0.7006
      0.4690    0.7286
   


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
      0.0576    0.9509    0.0131    0.3041
   
   R2 = 
      0.4430    0.6313    0.2882    0.9387
   
   M = 
      0.0576    0.9509    0.0131    0.3041
      0.4430    0.6313    0.2882    0.9387
   
   C1 = 
      0.6761
      0.5117
      0.2232
      0.6900
      0.8249
      0.0043
      0.6638
      0.9370
      0.2508
      0.8656
   
   C2 = 
      0.6143
      0.9667
   
   C3 = 
      0.6761
      0.5117
      0.2232
      0.6900
      0.8249
      0.0043
      0.6638
      0.9370
      0.2508
      0.8656
      0.6143
      0.9667
   

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
   

