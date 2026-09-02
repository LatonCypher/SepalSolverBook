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
      0.1130    0.0112    0.4745    0.6329    0.1386    0.6908    0.0521
   
   C = 
      0.5594
      0.1806
      0.1018
      0.5649
      0.6929
   
   M = 
      0.5989    0.3566    0.4466    0.1154    0.0424    0.2520    0.4793
      0.8034    0.2982    0.1286    0.9888    0.9150    0.4803    0.7967
      0.0035    0.1782    0.4093    0.6795    0.5117    0.7898    0.9228
      0.8185    0.1345    0.4945    0.7576    0.9580    0.5543    0.7480
      0.4403    0.6645    0.6691    0.6559    0.1307    0.8532    0.6788
      0.8839    0.1819    0.8035    0.5294    0.2988    0.1359    0.3643
      0.4967    0.4406    0.3465    0.1075    0.4639    0.5390    0.6780
      0.3288    0.7942    0.8840    0.0584    0.9519    0.4907    0.7314
   

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
      0.5637    0.4491    0.4737    0.7696
   
   R2 = 
      0.1333    0.5473    0.4551    0.4552    0.6395
   
   R3 = 
      0.5637    0.4491    0.4737    0.7696    0.1333    0.5473    0.4551    0.4552    0.6395
   
   C1 = 
      0.1537
      0.0362
      0.8047
      0.9676
      0.9907
      0.1543
      0.6283
      0.6443
      0.7893
      0.6168
   
   C2 = 
      0.3087
      0.1349
      0.7050
      0.1280
      0.3101
      0.7876
      0.6043
      0.4795
      0.3287
      0.2426
   
   M = 
      0.1537    0.3087
      0.0362    0.1349
      0.8047    0.7050
      0.9676    0.1280
      0.9907    0.3101
      0.1543    0.7876
      0.6283    0.6043
      0.6443    0.4795
      0.7893    0.3287
      0.6168    0.2426
   


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
      0.2449    0.8438    0.1127    0.5713
   
   R2 = 
      0.4896    0.1614    0.5659    0.5299
   
   M = 
      0.2449    0.8438    0.1127    0.5713
      0.4896    0.1614    0.5659    0.5299
   
   C1 = 
      0.8524
      0.7126
      0.2980
      0.3618
      0.4636
      0.1369
      0.8361
      0.8429
      0.3069
      0.1088
   
   C2 = 
      0.9963
      0.4808
   
   C3 = 
      0.8524
      0.7126
      0.2980
      0.3618
      0.4636
      0.1369
      0.8361
      0.8429
      0.3069
      0.1088
      0.9963
      0.4808
   

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
   

