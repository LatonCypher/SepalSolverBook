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
      0.5092    0.8355    0.6114    0.7097    0.8373    0.8769    0.6281
   
   C = 
      0.2803
      0.9433
      0.2559
      0.1240
      0.0191
   
   M = 
      0.1862    0.0825    0.0595    0.1748    0.8366    0.4258    0.8707
      0.3070    0.6231    0.5937    0.9551    0.2113    0.8850    0.7009
      0.5052    0.7646    0.8477    0.2728    0.6205    0.6203    0.1371
      0.9889    0.2616    0.9592    0.5120    0.7037    0.5322    0.6400
      0.5805    0.3480    0.2863    0.8635    0.9253    0.9441    0.8960
      0.7754    0.3070    0.3931    0.6281    0.5768    0.8295    0.7657
      0.5444    0.8326    0.6934    0.4860    0.6534    0.9132    0.8959
      0.7955    0.8486    0.2787    0.6290    0.9729    0.4325    0.1067
   

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
      0.2969    0.0989    0.2895    0.5808
   
   R2 = 
      0.4832    0.4436    0.5250    0.9092    0.8186
   
   R3 = 
      0.2969    0.0989    0.2895    0.5808    0.4832    0.4436    0.5250    0.9092    0.8186
   
   C1 = 
      0.7678
      0.8563
      0.6299
      0.6494
      0.2574
      0.5617
      0.2326
      0.7249
      0.7674
      0.8528
   
   C2 = 
      0.8397
      0.5836
      0.3459
      0.7625
      0.2169
      0.0497
      0.0457
      0.2918
      0.0735
      0.6655
   
   M = 
      0.7678    0.8397
      0.8563    0.5836
      0.6299    0.3459
      0.6494    0.7625
      0.2574    0.2169
      0.5617    0.0497
      0.2326    0.0457
      0.7249    0.2918
      0.7674    0.0735
      0.8528    0.6655
   


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
      0.2498    0.2969    0.0355    0.4073
   
   R2 = 
      0.9187    0.5745    0.7928    0.4851
   
   M = 
      0.2498    0.2969    0.0355    0.4073
      0.9187    0.5745    0.7928    0.4851
   
   C1 = 
      0.7386
      0.5902
      0.6965
      0.4887
      0.3462
      0.0621
      0.1395
      0.0648
      0.9829
      0.4881
   
   C2 = 
      0.4356
      0.4837
   
   C3 = 
      0.7386
      0.5902
      0.6965
      0.4887
      0.3462
      0.0621
      0.1395
      0.0648
      0.9829
      0.4881
      0.4356
      0.4837
   

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
   

