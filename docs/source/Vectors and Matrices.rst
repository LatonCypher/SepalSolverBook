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
      0.9617    0.8398    0.1036    0.4837    0.6053    0.8159    0.3017
   
   C = 
      0.4987
      0.7427
      0.6620
      0.5784
      0.5886
   
   M = 
      0.4192    0.5553    0.4030    0.1668    0.7521    0.9270    0.6179
      0.7786    0.8049    0.6794    0.7103    0.6946    0.7890    0.1183
      0.1713    0.6589    0.4877    0.9950    0.1782    0.3945    0.3176
      0.1289    0.7135    0.8112    0.5116    0.7667    0.7267    0.0783
      0.3303    0.8619    0.4504    0.8615    0.4194    0.0633    0.7892
      0.0312    0.6327    0.7847    0.6918    0.3177    0.1815    0.5747
      0.7956    0.9151    0.9860    0.4725    0.0620    0.5749    0.5420
      0.1260    0.2162    0.9595    0.1998    0.9779    0.5001    0.8618
   

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
      0.2833    0.6710    0.7410    0.9121
   
   R2 = 
      0.4393    0.5999    0.8097    0.8622    0.7436
   
   R3 = 
      0.2833    0.6710    0.7410    0.9121    0.4393    0.5999    0.8097    0.8622    0.7436
   
   C1 = 
      0.5140
      0.1901
      0.9070
      0.6059
      0.1232
      0.0778
      0.8532
      0.3072
      0.2663
      0.9613
   
   C2 = 
      0.1068
      0.2263
      0.8094
      0.2500
      0.9854
      0.2741
      0.3976
      0.8668
      0.5050
      0.0363
   
   M = 
      0.5140    0.1068
      0.1901    0.2263
      0.9070    0.8094
      0.6059    0.2500
      0.1232    0.9854
      0.0778    0.2741
      0.8532    0.3976
      0.3072    0.8668
      0.2663    0.5050
      0.9613    0.0363
   


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
      0.6686    0.1853    0.8833    0.1547
   
   R2 = 
      0.5919    0.3512    0.8091    0.4156
   
   M = 
      0.6686    0.1853    0.8833    0.1547
      0.5919    0.3512    0.8091    0.4156
   
   C1 = 
      0.5867
      0.8564
      0.5677
      0.9938
      0.6728
      0.5080
      0.0893
      0.0032
      0.4561
      0.4324
   
   C2 = 
      0.8655
      0.0009
   
   C3 = 
      0.5867
      0.8564
      0.5677
      0.9938
      0.6728
      0.5080
      0.0893
      0.0032
      0.4561
      0.4324
      0.8655
      0.0009
   

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
   

