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
      0.5465    0.1390    0.1053    0.0201    0.9712    0.9770    0.2839
   
   C = 
      0.8874
      0.0014
      0.5631
      0.8221
      0.5682
   
   M = 
      0.5988    0.9724    0.4259    0.3210    0.7390    0.2241    0.9588
      0.4601    0.9619    0.9296    0.1017    0.0597    0.6101    0.0750
      0.6822    0.9290    0.5797    0.7152    0.8759    0.0671    0.4875
      0.0382    0.1399    0.4515    0.5481    0.0245    0.5683    0.5210
      0.2757    0.9310    0.3157    0.4397    0.6594    0.8800    0.7910
      0.9591    0.1780    0.9792    0.8063    0.4492    0.2415    0.4668
      0.0620    0.3286    0.8186    0.5787    0.8659    0.1533    0.0295
      0.7392    0.8972    0.4428    0.5287    0.8129    0.2270    0.3604
   

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
      0.8649    0.5213    0.9060    0.1038
   
   R2 = 
      0.3632    0.6516    0.8658    0.7009    0.0776
   
   R3 = 
      0.8649    0.5213    0.9060    0.1038    0.3632    0.6516    0.8658    0.7009    0.0776
   
   C1 = 
      0.2291
      0.7862
      0.2032
      0.0835
      0.5093
      0.5304
      0.8048
      0.1806
      0.2869
      0.9412
   
   C2 = 
      0.9329
      0.3218
      0.1810
      0.0850
      0.0400
      0.2390
      0.9805
      0.6723
      0.5801
      0.3238
   
   M = 
      0.2291    0.9329
      0.7862    0.3218
      0.2032    0.1810
      0.0835    0.0850
      0.5093    0.0400
      0.5304    0.2390
      0.8048    0.9805
      0.1806    0.6723
      0.2869    0.5801
      0.9412    0.3238
   


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
      0.2477    0.9796    0.5423    0.5510
   
   R2 = 
      0.3325    0.9628    0.5003    0.7070
   
   M = 
      0.2477    0.9796    0.5423    0.5510
      0.3325    0.9628    0.5003    0.7070
   
   C1 = 
      0.4427
      0.7115
      0.2541
      0.8968
      0.8354
      0.9426
      0.0998
      0.8998
      0.3591
      0.0747
   
   C2 = 
      0.7686
      0.1405
   
   C3 = 
      0.4427
      0.7115
      0.2541
      0.8968
      0.8354
      0.9426
      0.0998
      0.8998
      0.3591
      0.0747
      0.7686
      0.1405
   

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
   

