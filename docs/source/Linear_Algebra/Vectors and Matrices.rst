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
      0.1373    0.5863    0.2191    0.7657    0.8465    0.5588    0.7887
   
   C = 
      0.2371
      0.1243
      0.8842
      0.4605
      0.5475
   
   M = 
      0.2863    0.9573    0.7339    0.9974    0.7934    0.8516    0.2661
      0.8967    0.0166    0.3412    0.5985    0.3521    0.8509    0.7444
      0.8642    0.6493    0.8557    0.0905    0.9182    0.3344    0.2504
      0.3900    0.5912    0.2031    0.9652    0.2571    0.1788    0.5128
      0.3297    0.5821    0.9630    0.2926    0.4054    0.4651    0.0122
      0.1788    0.9580    0.4498    0.8070    0.0401    0.6137    0.8192
      0.1432    0.4965    0.4719    0.8582    0.7607    0.6264    0.7048
      0.9512    0.4700    0.8297    0.8143    0.5690    0.1337    0.1090
   

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
      0.9051    0.6686    0.7515    0.1882
   
   R2 = 
      0.6476    0.7025    0.3007    0.5125    0.6857
   
   R3 = 
      0.9051    0.6686    0.7515    0.1882    0.6476    0.7025    0.3007    0.5125    0.6857
   
   C1 = 
      0.6933
      0.5861
      0.4459
      0.0447
      0.7551
      0.6670
      0.4325
      0.7984
      0.9833
      0.9820
   
   C2 = 
      0.4866
      0.6560
      0.7234
      0.2918
      0.4588
      0.4844
      0.2890
      0.7279
      0.1228
      0.7614
   
   M = 
      0.6933    0.4866
      0.5861    0.6560
      0.4459    0.7234
      0.0447    0.2918
      0.7551    0.4588
      0.6670    0.4844
      0.4325    0.2890
      0.7984    0.7279
      0.9833    0.1228
      0.9820    0.7614
   


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
      0.8053    0.8617    0.1975    0.9161
   
   R2 = 
      0.1659    0.3847    0.4042    0.3523
   
   M = 
      0.8053    0.8617    0.1975    0.9161
      0.1659    0.3847    0.4042    0.3523
   
   C1 = 
      0.6525
      0.0886
      0.9650
      0.7887
      0.7018
      0.4412
      0.2002
      0.7673
      0.5892
      0.1254
   
   C2 = 
      0.7091
      0.2653
   
   C3 = 
      0.6525
      0.0886
      0.9650
      0.7887
      0.7018
      0.4412
      0.2002
      0.7673
      0.5892
      0.1254
      0.7091
      0.2653
   

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
   

