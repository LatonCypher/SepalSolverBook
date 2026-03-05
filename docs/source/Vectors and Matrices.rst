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
      0.2881    0.8970    0.3273    0.0339    0.3169    0.1624    0.8978
   
   C = 
      0.8777
      0.0105
      0.6203
      0.8206
      0.5373
   
   M = 
      0.2471    0.2027    0.1591    0.0709    0.8427    0.2184    0.6219
      0.1862    0.9839    0.6920    0.7693    0.5975    0.3927    0.6073
      0.4453    0.5895    0.9036    0.1726    0.9733    0.1795    0.9770
      0.0694    0.7206    0.8033    0.5735    0.1021    0.5245    0.8969
      0.7812    0.5251    0.9193    0.3513    0.8699    0.9233    0.2101
      0.5315    0.0086    0.6975    0.0865    0.0375    0.9048    0.3433
      0.6339    0.8014    0.7348    0.9596    0.9021    0.7459    0.8336
      0.5947    0.1093    0.5578    0.8839    0.6675    0.2300    0.6753
   

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
      0.3921    0.5574    0.9454    0.5792
   
   R2 = 
      0.5314    0.8845    0.1808    0.5323    0.8688
   
   R3 = 
      0.3921    0.5574    0.9454    0.5792    0.5314    0.8845    0.1808    0.5323    0.8688
   
   C1 = 
      0.9068
      0.9799
      0.2640
      0.0770
      0.3340
      0.2042
      0.6356
      0.3851
      0.7003
      0.6022
   
   C2 = 
      0.3593
      0.4575
      0.2978
      0.9958
      0.8302
      0.0432
      0.3619
      0.0198
      0.7900
      0.8268
   
   M = 
      0.9068    0.3593
      0.9799    0.4575
      0.2640    0.2978
      0.0770    0.9958
      0.3340    0.8302
      0.2042    0.0432
      0.6356    0.3619
      0.3851    0.0198
      0.7003    0.7900
      0.6022    0.8268
   


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
      0.3622    0.0323    0.5964    0.6790
   
   R2 = 
      0.8427    0.2428    0.4477    0.3861
   
   M = 
      0.3622    0.0323    0.5964    0.6790
      0.8427    0.2428    0.4477    0.3861
   
   C1 = 
      0.7030
      0.2169
      0.7838
      0.9648
      0.6363
      0.1556
      0.7040
      0.7623
      0.7835
      0.0773
   
   C2 = 
      0.6033
      0.5633
   
   C3 = 
      0.7030
      0.2169
      0.7838
      0.9648
      0.6363
      0.1556
      0.7040
      0.7623
      0.7835
      0.0773
      0.6033
      0.5633
   

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
   

