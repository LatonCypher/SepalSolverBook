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
      0.0590    0.9188    0.3472    0.0826    0.2074    0.3612    0.6024
   
   C = 
      0.8948
      0.3430
      0.2791
      0.9585
      0.9881
   
   M = 
      0.6855    0.9986    0.7563    0.3975    0.1957    0.1776    0.6258
      0.6006    0.1804    0.7959    0.6634    0.1710    0.7833    0.0518
      0.9064    0.6075    0.7756    0.3641    0.8811    0.1159    0.4514
      0.5433    0.9223    0.0054    0.3521    0.2473    0.4517    0.2243
      0.2626    0.2617    0.0524    0.5473    0.2622    0.1239    0.3632
      0.6428    0.9647    0.7934    0.0027    0.8279    0.9354    0.4038
      0.6374    0.3273    0.8504    0.7460    0.7734    0.6674    0.3462
      0.7266    0.4058    0.4425    0.1669    0.9666    0.0279    0.6152
   

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
      0.8416    0.6664    0.4326    0.0080
   
   R2 = 
      0.6216    0.9844    0.9328    0.8818    0.4896
   
   R3 = 
      0.8416    0.6664    0.4326    0.0080    0.6216    0.9844    0.9328    0.8818    0.4896
   
   C1 = 
      0.7261
      0.9080
      0.8693
      0.8834
      0.8704
      0.0372
      0.4560
      0.1918
      0.3642
      0.6691
   
   C2 = 
      0.3279
      0.5121
      0.6711
      0.5115
      0.2857
      0.1171
      0.2707
      0.1247
      0.8830
      0.7984
   
   M = 
      0.7261    0.3279
      0.9080    0.5121
      0.8693    0.6711
      0.8834    0.5115
      0.8704    0.2857
      0.0372    0.1171
      0.4560    0.2707
      0.1918    0.1247
      0.3642    0.8830
      0.6691    0.7984
   


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
      0.6066    0.3589    0.9423    0.6975
   
   R2 = 
      0.3042    0.4415    0.4642    0.8765
   
   M = 
      0.6066    0.3589    0.9423    0.6975
      0.3042    0.4415    0.4642    0.8765
   
   C1 = 
      0.6096
      0.9265
      0.2276
      0.4340
      0.0724
      0.3277
      0.8237
      0.2838
      0.7666
      0.9321
   
   C2 = 
      0.7850
      0.9484
   
   C3 = 
      0.6096
      0.9265
      0.2276
      0.4340
      0.0724
      0.3277
      0.8237
      0.2838
      0.7666
      0.9321
      0.7850
      0.9484
   

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
   

