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
      0.1277    0.7601    0.3007    0.0976    0.6322    0.3382    0.6913
   
   C = 
      0.4137
      0.2026
      0.1357
      0.3582
      0.6194
   
   M = 
      0.8144    0.6297    0.0242    0.8202    0.2996    0.5836    0.9862
      0.8079    0.6920    0.7763    0.5659    0.7976    0.0204    0.2955
      0.0764    0.0447    0.5042    0.9882    0.8542    0.1968    0.2232
      0.0096    0.4395    0.9174    0.1568    0.1241    0.8107    0.8546
      0.9906    0.6458    0.4780    0.3693    0.5138    0.5509    0.0558
      0.4918    0.2398    0.9013    0.5229    0.1121    0.4985    0.9641
      0.6403    0.6906    0.4187    0.3740    0.1299    0.5277    0.9499
      0.9256    0.8582    0.6317    0.6575    0.8134    0.4056    0.7943
   

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
      0.8403    0.5620    0.1830    0.5980
   
   R2 = 
      0.5649    0.1659    0.5604    0.4443    0.6191
   
   R3 = 
      0.8403    0.5620    0.1830    0.5980    0.5649    0.1659    0.5604    0.4443    0.6191
   
   C1 = 
      0.2651
      0.1942
      0.6313
      0.0398
      0.2612
      0.1493
      0.5078
      0.6888
      0.4249
      0.6224
   
   C2 = 
      0.3129
      0.6724
      0.2780
      0.6680
      0.1049
      0.0961
      0.3190
      0.0496
      0.9094
      0.3044
   
   M = 
      0.2651    0.3129
      0.1942    0.6724
      0.6313    0.2780
      0.0398    0.6680
      0.2612    0.1049
      0.1493    0.0961
      0.5078    0.3190
      0.6888    0.0496
      0.4249    0.9094
      0.6224    0.3044
   


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
      0.7365    0.5409    0.5727    0.2529
   
   R2 = 
      0.4737    0.9333    0.7321    0.1841
   
   M = 
      0.7365    0.5409    0.5727    0.2529
      0.4737    0.9333    0.7321    0.1841
   
   C1 = 
      0.2476
      0.4882
      0.6888
      0.5065
      0.6379
      0.1060
      0.8688
      0.4040
      0.7143
      0.1348
   
   C2 = 
      0.7827
      0.1899
   
   C3 = 
      0.2476
      0.4882
      0.6888
      0.5065
      0.6379
      0.1060
      0.8688
      0.4040
      0.7143
      0.1348
      0.7827
      0.1899
   

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
   

