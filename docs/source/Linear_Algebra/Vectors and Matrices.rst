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
      0.9610    0.1685    0.0785    0.3003    0.9086    0.1473    0.0771
   
   C = 
      0.0679
      0.7418
      0.9006
      0.5798
      0.4972
   
   M = 
      0.2080    0.5927    0.7541    0.2431    0.3722    0.5034    0.5036
      0.9451    0.4322    0.5306    0.2607    0.5836    0.6898    0.3442
      0.9132    0.2553    0.5087    0.3271    0.2022    0.9967    0.1958
      0.4265    0.9014    0.0220    0.2234    0.4032    0.3298    0.8946
      0.3679    0.0641    0.6718    0.7573    0.8764    0.8913    0.6800
      0.7000    0.5488    0.9850    0.2560    0.3669    0.2899    0.7957
      0.4747    0.6906    0.0544    0.4794    0.9713    0.6593    0.9320
      0.6378    0.8463    0.9915    0.4857    0.4243    0.8705    0.9168
   

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
      0.5394    0.8050    0.0969    0.5772
   
   R2 = 
      0.2293    0.7225    0.3905    0.5383    0.9621
   
   R3 = 
      0.5394    0.8050    0.0969    0.5772    0.2293    0.7225    0.3905    0.5383    0.9621
   
   C1 = 
      0.4302
      0.8984
      0.9754
      0.0877
      0.9881
      0.4692
      0.1040
      0.4321
      0.4612
      0.7270
   
   C2 = 
      0.5848
      0.4476
      0.7928
      0.9329
      0.7066
      0.5687
      0.9885
      0.1229
      0.3234
      0.3249
   
   M = 
      0.4302    0.5848
      0.8984    0.4476
      0.9754    0.7928
      0.0877    0.9329
      0.9881    0.7066
      0.4692    0.5687
      0.1040    0.9885
      0.4321    0.1229
      0.4612    0.3234
      0.7270    0.3249
   


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
      0.8614    0.4820    0.1118    0.6282
   
   R2 = 
      0.9041    0.5899    0.2005    0.1284
   
   M = 
      0.8614    0.4820    0.1118    0.6282
      0.9041    0.5899    0.2005    0.1284
   
   C1 = 
      0.4238
      0.7911
      0.3368
      0.6608
      0.0926
      0.1426
      0.2815
      0.0637
      0.5133
      0.5250
   
   C2 = 
      0.3545
      0.8785
   
   C3 = 
      0.4238
      0.7911
      0.3368
      0.6608
      0.0926
      0.1426
      0.2815
      0.0637
      0.5133
      0.5250
      0.3545
      0.8785
   

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
   

