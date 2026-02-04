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
      0.5779    0.7109    0.4811    0.9811    0.7438    0.5664    0.2442
   
   C = 
      0.6342
      0.5073
      0.3821
      0.2469
      0.6058
   
   M = 
      0.1898    0.8184    0.8422    0.9565    0.1434    0.2640    0.1822
      0.6339    0.1449    0.0814    0.9870    0.6080    0.8482    0.4783
      0.4032    0.7167    0.4616    0.0774    0.1079    0.3311    0.0523
      0.1042    0.7434    0.7854    0.4829    0.7227    0.2398    0.4723
      0.7600    0.2941    0.4848    0.6816    0.7327    0.1970    0.4263
      0.1126    0.5848    0.4439    0.1709    0.8392    0.7482    0.8361
      0.7358    0.5308    0.3566    0.6631    0.2850    0.9016    0.6921
      0.9773    0.0573    0.5534    0.5430    0.5420    0.5135    0.0329
   

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
      0.1511    0.8281    0.2348    0.1816
   
   R2 = 
      0.5587    0.0082    0.5709    0.2773    0.3782
   
   R3 = 
      0.1511    0.8281    0.2348    0.1816    0.5587    0.0082    0.5709    0.2773    0.3782
   
   C1 = 
      0.9653
      0.6080
      0.0232
      0.1743
      0.8834
      0.7276
      0.0816
      0.6065
      0.9929
      0.9445
   
   C2 = 
      0.1389
      0.1549
      0.9450
      0.0104
      0.5798
      0.4654
      0.1177
      0.1541
      0.6053
      0.0776
   
   M = 
      0.9653    0.1389
      0.6080    0.1549
      0.0232    0.9450
      0.1743    0.0104
      0.8834    0.5798
      0.7276    0.4654
      0.0816    0.1177
      0.6065    0.1541
      0.9929    0.6053
      0.9445    0.0776
   


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
      0.0888    0.4672    0.4969    0.9315
   
   R2 = 
      0.1116    0.5634    0.4253    0.3720
   
   M = 
      0.0888    0.4672    0.4969    0.9315
      0.1116    0.5634    0.4253    0.3720
   
   C1 = 
      0.6585
      0.1109
      0.9393
      0.1137
      0.7311
      0.4666
      0.8088
      0.2025
      0.8204
      0.5072
   
   C2 = 
      0.2922
      0.9966
   
   C3 = 
      0.6585
      0.1109
      0.9393
      0.1137
      0.7311
      0.4666
      0.8088
      0.2025
      0.8204
      0.5072
      0.2922
      0.9966
   

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
   

