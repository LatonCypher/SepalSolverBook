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
      0.0124    0.0130    0.2591    0.7556    0.1251    0.2333    0.1570
   
   C = 
      0.8155
      0.9731
      0.1315
      0.6809
      0.0122
   
   M = 
      0.3459    0.3054    0.1452    0.4543    0.7357    0.2564    0.3937
      0.8600    0.6591    0.8913    0.8553    0.4565    0.1299    0.5329
      0.4858    0.8942    0.0295    0.9573    0.1313    0.6553    0.7749
      0.6892    0.4664    0.4519    0.7234    0.8846    0.3513    0.9490
      0.6681    0.7620    0.7245    0.1050    0.6268    0.5823    0.6497
      0.9701    0.6774    0.8446    0.9032    0.6574    0.8763    0.9096
      0.7803    0.6165    0.4819    0.4139    0.4912    0.0055    0.2762
      0.4860    0.0610    0.7514    0.5978    0.4537    0.4003    0.7618
   

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
      0.5108    0.4623    0.7702    0.6737
   
   R2 = 
      0.7960    0.7669    0.7623    0.6083    0.5884
   
   R3 = 
      0.5108    0.4623    0.7702    0.6737    0.7960    0.7669    0.7623    0.6083    0.5884
   
   C1 = 
      0.8836
      0.5476
      0.8246
      0.6366
      0.7355
      0.6871
      0.1590
      0.6996
      0.0814
      0.2120
   
   C2 = 
      0.9667
      0.4220
      0.7554
      0.8231
      0.4762
      0.9676
      0.4055
      0.9505
      0.0899
      0.3149
   
   M = 
      0.8836    0.9667
      0.5476    0.4220
      0.8246    0.7554
      0.6366    0.8231
      0.7355    0.4762
      0.6871    0.9676
      0.1590    0.4055
      0.6996    0.9505
      0.0814    0.0899
      0.2120    0.3149
   


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
      0.2686    0.2823    0.4727    0.4267
   
   R2 = 
      0.8017    0.9388    0.5399    0.7211
   
   M = 
      0.2686    0.2823    0.4727    0.4267
      0.8017    0.9388    0.5399    0.7211
   
   C1 = 
      0.7563
      0.9085
      0.9585
      0.3269
      0.7187
      0.3418
      0.0245
      0.7625
      0.7616
      0.5637
   
   C2 = 
      0.9414
      0.3178
   
   C3 = 
      0.7563
      0.9085
      0.9585
      0.3269
      0.7187
      0.3418
      0.0245
      0.7625
      0.7616
      0.5637
      0.9414
      0.3178
   

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
   

