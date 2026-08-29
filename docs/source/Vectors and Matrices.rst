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
      0.1364    0.8247    0.2859    0.7154    0.2801    0.2501    0.6092
   
   C = 
      0.2344
      0.1048
      0.8899
      0.8420
      0.1811
   
   M = 
      0.2609    0.2181    0.0794    0.2871    0.4707    0.7613    0.3306
      0.5142    0.1051    0.8042    0.1783    0.8197    0.9397    0.6757
      0.6983    0.4098    0.1528    0.0992    0.5808    0.8467    0.1846
      0.6020    0.9964    0.2771    0.1786    0.1692    0.3713    0.5033
      0.9332    0.0260    0.2939    0.9789    0.8539    0.9999    0.2604
      0.6891    0.0203    0.6352    0.0903    0.6382    0.1958    0.9159
      0.1979    0.3612    0.7454    0.4985    0.4418    0.5908    0.1622
      0.6625    0.2819    0.7810    0.5436    0.6048    0.8504    0.2250
   

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
      0.1744    0.2945    0.3751    0.0486
   
   R2 = 
      0.4611    0.6969    0.9389    0.9260    0.4346
   
   R3 = 
      0.1744    0.2945    0.3751    0.0486    0.4611    0.6969    0.9389    0.9260    0.4346
   
   C1 = 
      0.0718
      0.0991
      0.2572
      0.6061
      0.3436
      0.5438
      0.2575
      0.6903
      0.3305
      0.3333
   
   C2 = 
      0.9438
      0.0020
      0.4174
      0.6555
      0.1132
      0.1842
      0.1808
      0.9961
      0.3669
      0.2605
   
   M = 
      0.0718    0.9438
      0.0991    0.0020
      0.2572    0.4174
      0.6061    0.6555
      0.3436    0.1132
      0.5438    0.1842
      0.2575    0.1808
      0.6903    0.9961
      0.3305    0.3669
      0.3333    0.2605
   


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
      0.1223    0.0121    0.6344    0.0719
   
   R2 = 
      0.8740    0.1066    0.8940    0.2115
   
   M = 
      0.1223    0.0121    0.6344    0.0719
      0.8740    0.1066    0.8940    0.2115
   
   C1 = 
      0.1821
      0.7940
      0.2440
      0.9973
      0.7265
      0.1815
      0.0982
      0.9318
      0.3534
      0.9605
   
   C2 = 
      0.4958
      0.4588
   
   C3 = 
      0.1821
      0.7940
      0.2440
      0.9973
      0.7265
      0.1815
      0.0982
      0.9318
      0.3534
      0.9605
      0.4958
      0.4588
   

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
   

