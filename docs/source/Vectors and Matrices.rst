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
      0.4932    0.9686    0.7854    0.4692    0.4126    0.0742    0.1281
   
   C = 
      0.7965
      0.3784
      0.8462
      0.5175
      0.4536
   
   M = 
      0.1707    0.4543    0.9977    0.4186    0.6673    0.3649    0.0989
      0.7929    0.5505    0.8832    0.3508    0.0054    0.6188    0.3883
      0.1581    0.7111    0.6785    0.9771    0.4322    0.8960    0.1530
      0.5258    0.6390    0.3417    0.1896    0.4147    0.5975    0.9145
      0.4380    0.8055    0.2657    0.6398    0.2926    0.8532    0.7813
      0.7040    0.3243    0.4271    0.3041    0.8439    0.0838    0.5208
      0.3939    0.6886    0.0842    0.7957    0.0634    0.0157    0.6680
      0.8019    0.1519    0.2018    0.5517    0.2123    0.2949    0.3739
   

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
      0.4567    0.6467    0.4399    0.8123
   
   R2 = 
      0.4934    0.1876    0.7090    0.6112    0.9391
   
   R3 = 
      0.4567    0.6467    0.4399    0.8123    0.4934    0.1876    0.7090    0.6112    0.9391
   
   C1 = 
      0.1872
      0.7148
      0.9080
      0.4257
      0.7621
      0.6666
      0.2521
      0.1095
      0.2015
      0.9768
   
   C2 = 
      0.6997
      0.1150
      0.4895
      0.2253
      0.4742
      0.3563
      0.8714
      0.1700
      0.1653
      0.8389
   
   M = 
      0.1872    0.6997
      0.7148    0.1150
      0.9080    0.4895
      0.4257    0.2253
      0.7621    0.4742
      0.6666    0.3563
      0.2521    0.8714
      0.1095    0.1700
      0.2015    0.1653
      0.9768    0.8389
   


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
      0.8340    0.2139    0.3222    0.3427
   
   R2 = 
      0.8896    0.3147    0.4146    0.9518
   
   M = 
      0.8340    0.2139    0.3222    0.3427
      0.8896    0.3147    0.4146    0.9518
   
   C1 = 
      0.1616
      0.3752
      0.4879
      0.7456
      0.5306
      0.3302
      0.4138
      0.6974
      0.7675
      0.2084
   
   C2 = 
      0.3004
      0.5873
   
   C3 = 
      0.1616
      0.3752
      0.4879
      0.7456
      0.5306
      0.3302
      0.4138
      0.6974
      0.7675
      0.2084
      0.3004
      0.5873
   

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
   

