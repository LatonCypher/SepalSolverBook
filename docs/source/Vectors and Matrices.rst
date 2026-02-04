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
      0.3554    0.4244    0.3569    0.8700    0.8636    0.2010    0.8995
   
   C = 
      0.4571
      0.5696
      0.6429
      0.1211
      0.5033
   
   M = 
      0.1931    0.4787    0.2318    0.9339    0.6563    0.0458    0.2103
      0.1992    0.8479    0.0676    0.1164    0.8339    0.5948    0.1610
      0.5970    0.0440    0.7544    0.7057    0.0491    0.8300    0.4649
      0.2105    0.9794    0.4659    0.7629    0.3315    0.0446    0.8355
      0.7474    0.2416    0.3035    0.7430    0.1251    0.9147    0.4904
      0.9772    0.2740    0.7558    0.9327    0.5399    0.3876    0.1799
      0.4728    0.4177    0.3812    0.5498    0.2948    0.7501    0.8158
      0.5658    0.5167    0.8741    0.8866    0.4139    0.1422    0.6633
   

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
      0.8785    0.3987    0.4020    0.4355
   
   R2 = 
      0.1742    0.0426    0.9090    0.0558    0.2138
   
   R3 = 
      0.8785    0.3987    0.4020    0.4355    0.1742    0.0426    0.9090    0.0558    0.2138
   
   C1 = 
      0.5817
      0.2610
      0.0928
      0.5663
      0.5628
      0.6340
      0.7457
      0.7108
      0.9255
      0.2535
   
   C2 = 
      0.5493
      0.6716
      0.2028
      0.4872
      0.8878
      0.1444
      0.4419
      0.7610
      0.0225
      0.9972
   
   M = 
      0.5817    0.5493
      0.2610    0.6716
      0.0928    0.2028
      0.5663    0.4872
      0.5628    0.8878
      0.6340    0.1444
      0.7457    0.4419
      0.7108    0.7610
      0.9255    0.0225
      0.2535    0.9972
   


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
      0.2314    0.1752    0.0888    0.3422
   
   R2 = 
      0.9593    0.9667    0.3870    0.0988
   
   M = 
      0.2314    0.1752    0.0888    0.3422
      0.9593    0.9667    0.3870    0.0988
   
   C1 = 
      0.7155
      0.6145
      0.1161
      0.3442
      0.8984
      0.7389
      0.0206
      0.9636
      0.2220
      0.4747
   
   C2 = 
      0.9155
      0.6020
   
   C3 = 
      0.7155
      0.6145
      0.1161
      0.3442
      0.8984
      0.7389
      0.0206
      0.9636
      0.2220
      0.4747
      0.9155
      0.6020
   

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
   

