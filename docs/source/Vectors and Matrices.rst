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
      0.4931    0.8158    0.5425    0.3113    0.9040    0.8908    0.1841
   
   C = 
      0.7230
      0.2329
      0.5771
      0.3543
      0.3607
   
   M = 
      0.9831    0.0969    0.3251    0.1865    0.2745    0.4836    0.2229
      0.9458    0.8846    0.5310    0.0178    0.5461    0.6959    0.4121
      0.5216    0.7255    0.1052    0.1770    0.7579    0.3061    0.5535
      0.4691    0.9550    0.8499    0.7569    0.4491    0.2664    0.9335
      0.1004    0.5472    0.6020    0.8668    0.4964    0.7099    0.5036
      0.0233    0.3264    0.6764    0.9089    0.5065    0.3362    0.9347
      0.6507    0.8395    0.0958    0.9564    0.8351    0.0722    0.7469
      0.5763    0.4104    0.9241    0.5514    0.5171    0.4086    0.9773
   

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
      0.6112    0.4007    0.1306    0.3612
   
   R2 = 
      0.7614    0.1839    0.6601    0.0100    0.0886
   
   R3 = 
      0.6112    0.4007    0.1306    0.3612    0.7614    0.1839    0.6601    0.0100    0.0886
   
   C1 = 
      0.8324
      0.6656
      0.0996
      0.9421
      0.5962
      0.7383
      0.9308
      0.9043
      0.9098
      0.6226
   
   C2 = 
      0.9941
      0.3658
      0.4759
      0.9069
      0.3143
      0.9095
      0.6254
      0.7860
      0.7824
      0.7106
   
   M = 
      0.8324    0.9941
      0.6656    0.3658
      0.0996    0.4759
      0.9421    0.9069
      0.5962    0.3143
      0.7383    0.9095
      0.9308    0.6254
      0.9043    0.7860
      0.9098    0.7824
      0.6226    0.7106
   


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
      0.4597    0.9127    0.7886    0.5174
   
   R2 = 
      0.7464    0.2144    0.3644    0.3057
   
   M = 
      0.4597    0.9127    0.7886    0.5174
      0.7464    0.2144    0.3644    0.3057
   
   C1 = 
      0.2392
      0.6100
      0.6500
      0.7844
      0.0166
      0.1832
      0.5307
      0.4586
      0.3529
      0.8143
   
   C2 = 
      0.0806
      0.6937
   
   C3 = 
      0.2392
      0.6100
      0.6500
      0.7844
      0.0166
      0.1832
      0.5307
      0.4586
      0.3529
      0.8143
      0.0806
      0.6937
   

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
   

