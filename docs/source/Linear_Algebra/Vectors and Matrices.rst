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
      0.1069    0.9026    0.5474    0.1414    0.6593    0.2053    0.0398
   
   C = 
      0.9336
      0.5772
      0.9812
      0.7347
      0.0568
   
   M = 
      0.3787    0.1623    0.9769    0.2170    0.4053    0.3583    0.4039
      0.1100    0.7451    0.6552    0.5267    0.3586    0.9360    0.9374
      0.9191    0.7209    0.7321    0.2732    0.9958    0.7935    0.0230
      0.9073    0.4378    0.0630    0.3380    0.1577    0.8332    0.2353
      0.9585    0.1117    0.4604    0.7726    0.9118    0.5125    0.2575
      0.2647    0.3322    0.1704    0.2976    0.6410    0.2544    0.6634
      0.1410    0.7298    0.2248    0.4083    0.4212    0.3494    0.9724
      0.0351    0.7222    0.3862    0.8642    0.1273    0.4232    0.2988
   

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
      0.6690    0.6358    0.4549    0.2708
   
   R2 = 
      0.2140    0.6149    0.6056    0.9207    0.1112
   
   R3 = 
      0.6690    0.6358    0.4549    0.2708    0.2140    0.6149    0.6056    0.9207    0.1112
   
   C1 = 
      0.8515
      0.6806
      0.5330
      0.8842
      0.6101
      0.1562
      0.8257
      0.9054
      0.2315
      0.6868
   
   C2 = 
      0.1992
      0.0085
      0.0614
      0.2340
      0.2055
      0.3886
      0.3290
      0.7385
      0.7382
      0.9163
   
   M = 
      0.8515    0.1992
      0.6806    0.0085
      0.5330    0.0614
      0.8842    0.2340
      0.6101    0.2055
      0.1562    0.3886
      0.8257    0.3290
      0.9054    0.7385
      0.2315    0.7382
      0.6868    0.9163
   


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
      0.1018    0.4372    0.6833    0.5692
   
   R2 = 
      0.9242    0.5083    0.8717    0.7834
   
   M = 
      0.1018    0.4372    0.6833    0.5692
      0.9242    0.5083    0.8717    0.7834
   
   C1 = 
      0.1615
      0.0261
      0.5574
      0.1245
      0.9093
      0.7289
      0.3396
      0.3318
      0.4485
      0.5884
   
   C2 = 
      0.7917
      0.6171
   
   C3 = 
      0.1615
      0.0261
      0.5574
      0.1245
      0.9093
      0.7289
      0.3396
      0.3318
      0.4485
      0.5884
      0.7917
      0.6171
   

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
   

