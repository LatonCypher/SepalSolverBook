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
      0.1792    0.5445    0.4344    0.1324    0.0686    0.6634    0.2571
   
   C = 
      0.9748
      0.9552
      0.5211
      0.3946
      0.0597
   
   M = 
      0.4377    0.4229    0.8363    0.3346    0.0209    0.3305    0.7475
      0.4604    0.9100    0.8655    0.6155    0.5917    0.5109    0.9386
      0.3440    0.0522    0.9236    0.1559    0.2019    0.1690    0.7635
      0.2124    0.0969    0.9768    0.7013    0.2576    0.5952    0.4517
      0.7743    0.4177    0.4042    0.7986    0.1042    0.8197    0.1492
      0.5272    0.3764    0.4157    0.5618    0.8519    0.8608    0.7440
      0.6477    0.3562    0.4050    0.9407    0.4991    0.7332    0.2367
      0.4893    0.7940    0.3252    0.6647    0.2432    0.8307    0.2446
   

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
      0.1631    0.3462    0.8888    0.3336
   
   R2 = 
      0.3865    0.1740    0.7442    0.6402    0.9747
   
   R3 = 
      0.1631    0.3462    0.8888    0.3336    0.3865    0.1740    0.7442    0.6402    0.9747
   
   C1 = 
      0.2723
      0.9985
      0.2075
      0.1108
      0.4853
      0.9344
      0.4009
      0.7077
      0.2081
      0.1582
   
   C2 = 
      0.1414
      0.8322
      0.6317
      0.3244
      0.1993
      0.4194
      0.7098
      0.9065
      0.2102
      0.7779
   
   M = 
      0.2723    0.1414
      0.9985    0.8322
      0.2075    0.6317
      0.1108    0.3244
      0.4853    0.1993
      0.9344    0.4194
      0.4009    0.7098
      0.7077    0.9065
      0.2081    0.2102
      0.1582    0.7779
   


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
      0.6976    0.1185    0.4727    0.2275
   
   R2 = 
      0.7202    0.0607    0.2391    0.5784
   
   M = 
      0.6976    0.1185    0.4727    0.2275
      0.7202    0.0607    0.2391    0.5784
   
   C1 = 
      0.3585
      0.2657
      0.8989
      0.0212
      0.7095
      0.7277
      0.7055
      0.1829
      0.1176
      0.1566
   
   C2 = 
      0.0127
      0.4124
   
   C3 = 
      0.3585
      0.2657
      0.8989
      0.0212
      0.7095
      0.7277
      0.7055
      0.1829
      0.1176
      0.1566
      0.0127
      0.4124
   

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
   

