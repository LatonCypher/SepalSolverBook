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
      0.8885    0.5949    0.6800    0.7848    0.1034    0.5359    0.7929
   
   C = 
      0.6574
      0.3658
      0.0340
      0.7138
      0.6271
   
   M = 
      0.8826    0.2415    0.7260    0.1338    0.2227    0.9389    0.7589
      0.3815    0.9026    0.0767    0.5160    0.2933    0.2566    0.7159
      0.2866    0.3995    0.6730    0.7475    0.0184    0.3619    0.8959
      0.0575    0.8781    0.5534    0.8219    0.6420    0.7271    0.8990
      0.4758    0.0113    0.0378    0.0884    0.5696    0.7095    0.3247
      0.3573    0.4149    0.7101    0.2426    0.6573    0.8518    0.1166
      0.9905    0.3184    0.9781    0.5108    0.1879    0.5855    0.9118
      0.5271    0.3138    0.3621    0.0617    0.3795    0.8958    0.6898
   

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
      0.9848    0.0046    0.4602    0.0709
   
   R2 = 
      0.4555    0.7633    0.2836    0.8224    0.9313
   
   R3 = 
      0.9848    0.0046    0.4602    0.0709    0.4555    0.7633    0.2836    0.8224    0.9313
   
   C1 = 
      0.0812
      0.6485
      0.6707
      0.5190
      0.5027
      0.1804
      0.0858
      0.5479
      0.9727
      0.7763
   
   C2 = 
      0.0657
      0.6223
      0.4762
      0.8133
      0.7168
      0.3159
      0.4295
      0.8839
      0.3840
      0.7670
   
   M = 
      0.0812    0.0657
      0.6485    0.6223
      0.6707    0.4762
      0.5190    0.8133
      0.5027    0.7168
      0.1804    0.3159
      0.0858    0.4295
      0.5479    0.8839
      0.9727    0.3840
      0.7763    0.7670
   


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
      0.6545    0.1708    0.6214    0.1665
   
   R2 = 
      0.9182    0.4982    0.2657    0.4739
   
   M = 
      0.6545    0.1708    0.6214    0.1665
      0.9182    0.4982    0.2657    0.4739
   
   C1 = 
      0.2451
      0.9719
      0.4942
      0.2465
      0.3703
      0.4914
      0.0985
      0.3124
      0.1087
      0.1360
   
   C2 = 
      0.4833
      0.4826
   
   C3 = 
      0.2451
      0.9719
      0.4942
      0.2465
      0.3703
      0.4914
      0.0985
      0.3124
      0.1087
      0.1360
      0.4833
      0.4826
   

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
   

