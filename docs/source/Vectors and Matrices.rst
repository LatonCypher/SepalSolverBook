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
      0.8976    0.0166    0.1779    0.9937    0.9562    0.0504    0.2196
   
   C = 
      0.7498
      0.8158
      0.1223
      0.7826
      0.6117
   
   M = 
      0.4928    0.7463    0.5994    0.9732    0.0714    0.2694    0.7911
      0.6495    0.4053    0.0257    0.8079    0.4494    0.5258    0.7689
      0.0877    0.6099    0.4868    0.4575    0.5069    0.7662    0.2560
      0.1391    0.0790    0.1670    0.9027    0.9733    0.7421    0.2082
      0.9476    0.5462    0.9330    0.2948    0.1499    0.7958    0.7809
      0.1460    0.3706    0.6487    0.6971    0.4516    0.1184    0.1664
      0.6830    0.4742    0.3232    0.6846    0.9105    0.6339    0.7310
      0.4726    0.5513    0.4816    0.9517    0.9282    0.4228    0.7555
   

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
      0.7179    0.7125    0.2107    0.1016
   
   R2 = 
      0.9684    0.5004    0.1291    0.4052    0.9295
   
   R3 = 
      0.7179    0.7125    0.2107    0.1016    0.9684    0.5004    0.1291    0.4052    0.9295
   
   C1 = 
      0.9409
      0.7566
      0.5004
      0.9324
      0.7504
      0.7418
      0.8353
      0.7173
      0.1658
      0.2137
   
   C2 = 
      0.7734
      0.0787
      0.9347
      0.1756
      0.3035
      0.4266
      0.6255
      0.8198
      0.4511
      0.9479
   
   M = 
      0.9409    0.7734
      0.7566    0.0787
      0.5004    0.9347
      0.9324    0.1756
      0.7504    0.3035
      0.7418    0.4266
      0.8353    0.6255
      0.7173    0.8198
      0.1658    0.4511
      0.2137    0.9479
   


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
      0.9616    0.1903    0.6963    0.0173
   
   R2 = 
      0.6292    0.2666    0.5391    0.9893
   
   M = 
      0.9616    0.1903    0.6963    0.0173
      0.6292    0.2666    0.5391    0.9893
   
   C1 = 
      0.1806
      0.5255
      0.8965
      0.0942
      0.0982
      0.9176
      0.2732
      0.7127
      0.6228
      0.5486
   
   C2 = 
      0.8121
      0.9790
   
   C3 = 
      0.1806
      0.5255
      0.8965
      0.0942
      0.0982
      0.9176
      0.2732
      0.7127
      0.6228
      0.5486
      0.8121
      0.9790
   

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
   

