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
      0.3159    0.5293    0.0385    0.9880    0.9830    0.3772    0.3103
   
   C = 
      0.1962
      0.8312
      0.4827
      0.7548
      0.9751
   
   M = 
      0.9523    0.2832    0.1405    0.5082    0.7891    0.9002    0.4508
      0.5562    0.1056    0.2247    0.8366    0.3086    0.1471    0.5366
      0.4932    0.7655    0.5433    0.9063    0.0670    0.2046    0.8715
      0.1201    0.8905    0.5537    0.4531    0.6497    0.2705    0.0202
      0.9115    0.8440    0.4559    0.4153    0.0515    0.8992    0.3314
      0.4985    0.3285    0.6489    0.1490    0.0151    0.6709    0.9240
      0.1601    0.3088    0.6044    0.1909    0.7409    0.8245    0.1174
      0.1232    0.8843    0.0505    0.2419    0.4679    0.7746    0.1875
   

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
      0.7766    0.0750    0.6377    0.0837
   
   R2 = 
      0.8839    0.4634    0.9385    0.4796    0.2748
   
   R3 = 
      0.7766    0.0750    0.6377    0.0837    0.8839    0.4634    0.9385    0.4796    0.2748
   
   C1 = 
      0.3240
      0.0411
      0.5135
      0.0240
      0.9036
      0.0831
      0.5551
      0.5661
      0.1468
      0.9786
   
   C2 = 
      0.1880
      0.5327
      0.3906
      0.8834
      0.7026
      0.0723
      0.4446
      0.4999
      0.1582
      0.3100
   
   M = 
      0.3240    0.1880
      0.0411    0.5327
      0.5135    0.3906
      0.0240    0.8834
      0.9036    0.7026
      0.0831    0.0723
      0.5551    0.4446
      0.5661    0.4999
      0.1468    0.1582
      0.9786    0.3100
   


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
      0.8639    0.6378    0.4750    0.9637
   
   R2 = 
      0.3822    0.4125    0.2534    0.7167
   
   M = 
      0.8639    0.6378    0.4750    0.9637
      0.3822    0.4125    0.2534    0.7167
   
   C1 = 
      0.8366
      0.6317
      0.2278
      0.2730
      0.2723
      0.1335
      0.9884
      0.8101
      0.2997
      0.7161
   
   C2 = 
      0.4897
      0.8606
   
   C3 = 
      0.8366
      0.6317
      0.2278
      0.2730
      0.2723
      0.1335
      0.9884
      0.8101
      0.2997
      0.7161
      0.4897
      0.8606
   

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
   

