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
      0.5011    0.2973    0.6502    0.4918    0.4299    0.2285    0.0495
   
   C = 
      0.7821
      0.2248
      0.9033
      0.7597
      0.8164
   
   M = 
      0.6956    0.8429    0.8774    0.4007    0.1284    0.1553    0.3194
      0.1216    0.9019    0.6702    0.8126    0.2548    0.6798    0.8117
      0.8818    0.9518    0.6989    0.5387    0.0478    0.7149    0.8755
      0.6698    0.1857    0.3613    0.6093    0.2253    0.7889    0.4780
      0.5471    0.7695    0.1104    0.1174    0.9504    0.6858    0.6956
      0.3808    0.1301    0.8870    0.9102    0.5426    0.3436    0.2374
      0.6694    0.2285    0.8199    0.3826    0.2102    0.4114    0.1494
      0.6506    0.0441    0.1681    0.6436    0.0032    0.4820    0.4951
   

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
      0.6370    0.3429    0.2436    0.5327
   
   R2 = 
      0.2909    0.0644    0.3709    0.0705    0.3000
   
   R3 = 
      0.6370    0.3429    0.2436    0.5327    0.2909    0.0644    0.3709    0.0705    0.3000
   
   C1 = 
      0.3194
      0.8627
      0.3833
      0.4805
      0.4261
      0.7512
      0.9378
      0.4737
      0.3549
      0.3288
   
   C2 = 
      0.3043
      0.9772
      0.9734
      0.9400
      0.1580
      0.5636
      0.3588
      0.3791
      0.2591
      0.8119
   
   M = 
      0.3194    0.3043
      0.8627    0.9772
      0.3833    0.9734
      0.4805    0.9400
      0.4261    0.1580
      0.7512    0.5636
      0.9378    0.3588
      0.4737    0.3791
      0.3549    0.2591
      0.3288    0.8119
   


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
      0.9563    0.9127    0.3033    0.2268
   
   R2 = 
      0.6787    0.9757    0.8213    0.8311
   
   M = 
      0.9563    0.9127    0.3033    0.2268
      0.6787    0.9757    0.8213    0.8311
   
   C1 = 
      0.5732
      0.7051
      0.9991
      0.7628
      0.5468
      0.6357
      0.9789
      0.5567
      0.0098
      0.5018
   
   C2 = 
      0.0626
      0.4715
   
   C3 = 
      0.5732
      0.7051
      0.9991
      0.7628
      0.5468
      0.6357
      0.9789
      0.5567
      0.0098
      0.5018
      0.0626
      0.4715
   

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
   

