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
      0.1281    0.1448    0.6429    0.8010    0.1055    0.4424    0.3207
   
   C = 
      0.4794
      0.4945
      0.2295
      0.9422
      0.8871
   
   M = 
      0.9269    0.9834    0.6530    0.3116    0.7166    0.7221    0.5725
      0.4427    0.2799    0.8483    0.7684    0.0013    0.8221    0.1035
      0.4160    0.9373    0.2782    0.6329    0.4448    0.4643    0.2539
      0.3786    0.8052    0.5821    0.8295    0.8742    0.3082    0.7501
      0.8622    0.6803    0.4688    0.1464    0.1567    0.8681    0.8992
      0.2004    0.4074    0.3324    0.1214    0.3422    0.4984    0.2365
      0.9149    0.4046    0.5208    0.2792    0.8457    0.0173    0.3418
      0.5311    0.6609    0.7774    0.4430    0.3168    0.1710    0.7699
   

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
      0.0949    0.8276    0.3476    0.8196
   
   R2 = 
      0.1105    0.9030    0.0653    0.6516    0.6981
   
   R3 = 
      0.0949    0.8276    0.3476    0.8196    0.1105    0.9030    0.0653    0.6516    0.6981
   
   C1 = 
      0.5012
      0.0115
      0.1622
      0.1099
      0.3671
      0.1117
      0.1865
      0.4830
      0.1155
      0.9311
   
   C2 = 
      0.9703
      0.0337
      0.9513
      0.9317
      0.9587
      0.9555
      0.3650
      0.9441
      0.6156
      0.0073
   
   M = 
      0.5012    0.9703
      0.0115    0.0337
      0.1622    0.9513
      0.1099    0.9317
      0.3671    0.9587
      0.1117    0.9555
      0.1865    0.3650
      0.4830    0.9441
      0.1155    0.6156
      0.9311    0.0073
   


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
      0.7320    0.0539    0.8147    0.4087
   
   R2 = 
      0.2236    0.8912    0.8435    0.7532
   
   M = 
      0.7320    0.0539    0.8147    0.4087
      0.2236    0.8912    0.8435    0.7532
   
   C1 = 
      0.8565
      0.3875
      0.9870
      0.6699
      0.5582
      0.4866
      0.0521
      0.3899
      0.7832
      0.7328
   
   C2 = 
      0.7876
      0.6799
   
   C3 = 
      0.8565
      0.3875
      0.9870
      0.6699
      0.5582
      0.4866
      0.0521
      0.3899
      0.7832
      0.7328
      0.7876
      0.6799
   

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
   

