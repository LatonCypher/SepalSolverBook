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
      0.5281    0.0792    0.6474    0.6073    0.3134    0.2146    0.2358
   
   C = 
      0.0539
      0.2015
      0.6977
      0.1876
      0.5728
   
   M = 
      0.9027    0.4374    0.6380    0.2938    0.5092    0.5133    0.5220
      0.1592    0.0443    0.6425    0.1173    0.7607    0.2800    0.3525
      0.8559    0.3456    0.5569    0.8744    0.9854    0.5346    0.8552
      0.3342    0.1837    0.6815    0.4842    0.2351    0.5291    0.2328
      0.2748    0.8552    0.0767    0.2141    0.7639    0.9213    0.3211
      0.5985    0.5004    0.4440    0.9452    0.6214    0.2893    0.5003
      0.0614    0.8182    0.4595    0.1286    0.7464    0.6617    0.0793
      0.4198    0.4172    0.7557    0.7807    0.0851    0.3693    0.6135
   

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
      0.9947    0.0266    0.5321    0.4718
   
   R2 = 
      0.6729    0.1580    0.8621    0.4156    0.8164
   
   R3 = 
      0.9947    0.0266    0.5321    0.4718    0.6729    0.1580    0.8621    0.4156    0.8164
   
   C1 = 
      0.8029
      0.9306
      0.4018
      0.3476
      0.2635
      0.3402
      0.9826
      0.2435
      0.7517
      0.5727
   
   C2 = 
      0.8528
      0.2848
      0.4065
      0.6357
      0.2451
      0.6406
      0.8101
      0.1160
      0.3855
      0.5146
   
   M = 
      0.8029    0.8528
      0.9306    0.2848
      0.4018    0.4065
      0.3476    0.6357
      0.2635    0.2451
      0.3402    0.6406
      0.9826    0.8101
      0.2435    0.1160
      0.7517    0.3855
      0.5727    0.5146
   


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
      0.3196    0.2849    0.6329    0.4749
   
   R2 = 
      0.7665    0.5896    0.8960    0.5006
   
   M = 
      0.3196    0.2849    0.6329    0.4749
      0.7665    0.5896    0.8960    0.5006
   
   C1 = 
      0.7193
      0.1833
      0.6532
      0.2322
      0.6854
      0.0495
      0.0421
      0.3609
      0.2636
      0.6518
   
   C2 = 
      0.4389
      0.7743
   
   C3 = 
      0.7193
      0.1833
      0.6532
      0.2322
      0.6854
      0.0495
      0.0421
      0.3609
      0.2636
      0.6518
      0.4389
      0.7743
   

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
   

