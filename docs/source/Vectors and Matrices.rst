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
      0.5634    0.1912    0.1978    0.6796    0.8954    0.6046    0.0119
   
   C = 
      0.7607
      0.8399
      0.9302
      0.0790
      0.7884
   
   M = 
      0.1164    0.1028    0.5068    0.2391    0.0621    0.1423    0.2771
      0.2802    0.5622    0.8474    0.5604    0.2131    0.5215    0.8550
      0.0767    0.1178    0.5095    0.2114    0.9516    0.2248    0.5550
      0.2454    0.2487    0.6437    0.3405    0.5908    0.0933    0.0255
      0.8091    0.7559    0.3958    0.9843    0.1424    0.6306    0.0031
      0.2428    0.7407    0.8720    0.6834    0.9945    0.5655    0.1225
      0.1806    0.0397    0.1017    0.0070    0.6606    0.9919    0.9742
      0.7790    0.4210    0.8639    0.6757    0.6768    0.9923    0.2319
   

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
      0.1768    0.4678    0.6065    0.2024
   
   R2 = 
      0.8579    0.2170    0.0510    0.1463    0.5294
   
   R3 = 
      0.1768    0.4678    0.6065    0.2024    0.8579    0.2170    0.0510    0.1463    0.5294
   
   C1 = 
      0.2244
      0.7002
      0.3323
      0.2952
      0.8162
      0.9732
      0.2311
      0.0938
      0.5605
      0.8070
   
   C2 = 
      0.8182
      0.4056
      0.0838
      0.4140
      0.5339
      0.5064
      0.4812
      0.5670
      0.7095
      0.0392
   
   M = 
      0.2244    0.8182
      0.7002    0.4056
      0.3323    0.0838
      0.2952    0.4140
      0.8162    0.5339
      0.9732    0.5064
      0.2311    0.4812
      0.0938    0.5670
      0.5605    0.7095
      0.8070    0.0392
   


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
      0.1025    0.5737    0.3038    0.6865
   
   R2 = 
      0.4581    0.0025    0.3082    0.0988
   
   M = 
      0.1025    0.5737    0.3038    0.6865
      0.4581    0.0025    0.3082    0.0988
   
   C1 = 
      0.9670
      0.3979
      0.5830
      0.5082
      0.7492
      0.3117
      0.9310
      0.9092
      0.2943
      0.9937
   
   C2 = 
      0.4249
      0.0769
   
   C3 = 
      0.9670
      0.3979
      0.5830
      0.5082
      0.7492
      0.3117
      0.9310
      0.9092
      0.2943
      0.9937
      0.4249
      0.0769
   

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
   

