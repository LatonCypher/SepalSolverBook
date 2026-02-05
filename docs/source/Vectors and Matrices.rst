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
      0.6389    0.9922    0.4109    0.0325    0.5310    0.7613    0.6784
   
   C = 
      0.0488
      0.0067
      0.6401
      0.5672
      0.3307
   
   M = 
      0.6679    0.8066    0.8592    0.6440    0.4716    0.5179    0.7947
      0.3828    0.5208    0.3551    0.9766    0.3450    0.7382    0.4333
      0.8791    0.2903    0.3916    0.3637    0.9964    0.9675    0.1428
      0.9101    0.5802    0.2608    0.7834    0.9084    0.3221    0.9113
      0.5679    0.5873    0.7597    0.9143    0.3183    0.7474    0.0824
      0.4755    0.6083    0.1013    0.5999    0.6128    0.1313    0.0738
      0.8733    0.2708    0.7094    0.5032    0.1951    0.9649    0.3035
      0.1707    0.2391    0.1018    0.3803    0.5945    0.3476    0.5902
   

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
      0.5799    0.9553    0.6747    0.6063
   
   R2 = 
      0.7423    0.1524    0.8231    0.4102    0.6818
   
   R3 = 
      0.5799    0.9553    0.6747    0.6063    0.7423    0.1524    0.8231    0.4102    0.6818
   
   C1 = 
      0.0319
      0.5383
      0.5543
      0.9354
      0.8004
      0.2181
      0.4377
      0.3072
      0.1445
      0.5892
   
   C2 = 
      0.5635
      0.4552
      0.6471
      0.8811
      0.6963
      0.2632
      0.5121
      0.8439
      0.5420
      0.1614
   
   M = 
      0.0319    0.5635
      0.5383    0.4552
      0.5543    0.6471
      0.9354    0.8811
      0.8004    0.6963
      0.2181    0.2632
      0.4377    0.5121
      0.3072    0.8439
      0.1445    0.5420
      0.5892    0.1614
   


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
      0.9042    0.7658    0.0790    0.9894
   
   R2 = 
      0.1928    0.8694    0.5465    0.7727
   
   M = 
      0.9042    0.7658    0.0790    0.9894
      0.1928    0.8694    0.5465    0.7727
   
   C1 = 
      0.7490
      0.2586
      0.5367
      0.7652
      0.5092
      0.8246
      0.8697
      0.7995
      0.5911
      0.3869
   
   C2 = 
      0.7918
      0.6831
   
   C3 = 
      0.7490
      0.2586
      0.5367
      0.7652
      0.5092
      0.8246
      0.8697
      0.7995
      0.5911
      0.3869
      0.7918
      0.6831
   

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
   

