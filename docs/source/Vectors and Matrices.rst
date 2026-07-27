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
      0.9973    0.2407    0.6426    0.4608    0.1879    0.0830    0.9281
   
   C = 
      0.1872
      0.0162
      0.9264
      0.4996
      0.7238
   
   M = 
      0.7277    0.3000    0.3098    0.4442    0.7607    0.3595    0.0732
      0.9134    0.0702    0.9067    0.9519    0.3125    0.8006    0.4182
      0.9044    0.3822    0.3716    0.2564    0.5085    0.7468    0.9493
      0.7749    0.3563    0.2384    0.2911    0.9112    0.4595    0.8126
      0.0094    0.3882    0.6564    0.0811    0.4027    0.9658    0.0105
      0.1614    0.4857    0.4968    0.4897    0.3668    0.0408    0.7665
      0.9880    0.1697    0.3808    0.2663    0.4673    0.2142    0.4167
      0.5337    0.9466    0.5909    0.4074    0.5278    0.0893    0.2458
   

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
      0.8555    0.9171    0.7764    0.0210
   
   R2 = 
      0.5361    0.7656    0.8468    0.6029    0.4054
   
   R3 = 
      0.8555    0.9171    0.7764    0.0210    0.5361    0.7656    0.8468    0.6029    0.4054
   
   C1 = 
      0.8179
      0.2594
      0.7956
      0.2253
      0.6610
      0.8287
      0.2813
      0.7350
      0.2166
      0.7431
   
   C2 = 
      0.2362
      0.8181
      0.6713
      0.6670
      0.7387
      0.1813
      0.0550
      0.2545
      0.5275
      0.4172
   
   M = 
      0.8179    0.2362
      0.2594    0.8181
      0.7956    0.6713
      0.2253    0.6670
      0.6610    0.7387
      0.8287    0.1813
      0.2813    0.0550
      0.7350    0.2545
      0.2166    0.5275
      0.7431    0.4172
   


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
      0.5246    0.3248    0.8878    0.6781
   
   R2 = 
      0.9251    0.2201    0.2339    0.9742
   
   M = 
      0.5246    0.3248    0.8878    0.6781
      0.9251    0.2201    0.2339    0.9742
   
   C1 = 
      0.4638
      0.6049
      0.5191
      0.9755
      0.9212
      0.4375
      0.5407
      0.5670
      0.3337
      0.8443
   
   C2 = 
      0.2428
      0.1459
   
   C3 = 
      0.4638
      0.6049
      0.5191
      0.9755
      0.9212
      0.4375
      0.5407
      0.5670
      0.3337
      0.8443
      0.2428
      0.1459
   

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
   

