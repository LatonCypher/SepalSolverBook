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
      0.5325    0.9161    0.1819    0.7202    0.5623    0.1877    0.4475
   
   C = 
      0.3470
      0.0518
      0.4443
      0.6926
      0.5381
   
   M = 
      0.9132    0.5333    0.4537    0.5013    0.9175    0.6145    0.9548
      0.2079    0.4815    0.5210    0.8311    0.4001    0.6595    0.9456
      0.8626    0.3519    0.9997    0.7339    0.3819    0.6689    0.2439
      0.4099    0.3112    0.7688    0.3369    0.8133    0.9698    0.5819
      0.2581    0.7470    0.8823    0.6145    0.7889    0.5109    0.5383
      0.0001    0.6746    0.2006    0.6245    0.5911    0.6238    0.1737
      0.5452    0.9934    0.8756    0.2047    0.4147    0.9073    0.9896
      0.7847    0.6752    0.6621    0.1196    0.1107    0.7526    0.7755
   

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
      0.2830    0.4334    0.7487    0.5581
   
   R2 = 
      0.2100    0.0892    0.4742    0.5357    0.8268
   
   R3 = 
      0.2830    0.4334    0.7487    0.5581    0.2100    0.0892    0.4742    0.5357    0.8268
   
   C1 = 
      0.2876
      0.6516
      0.2941
      0.5476
      0.6799
      0.8695
      0.0125
      0.7183
      0.1177
      0.4776
   
   C2 = 
      0.7745
      0.8533
      0.1858
      0.5575
      0.2505
      0.6345
      0.6097
      0.0504
      0.9424
      0.9427
   
   M = 
      0.2876    0.7745
      0.6516    0.8533
      0.2941    0.1858
      0.5476    0.5575
      0.6799    0.2505
      0.8695    0.6345
      0.0125    0.6097
      0.7183    0.0504
      0.1177    0.9424
      0.4776    0.9427
   


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
      0.7191    0.4568    0.8720    0.4164
   
   R2 = 
      0.5628    0.2806    0.0157    0.6337
   
   M = 
      0.7191    0.4568    0.8720    0.4164
      0.5628    0.2806    0.0157    0.6337
   
   C1 = 
      0.2471
      0.1940
      0.7960
      0.7446
      0.9649
      0.4665
      0.2684
      0.3572
      0.9668
      0.6346
   
   C2 = 
      0.1059
      0.8927
   
   C3 = 
      0.2471
      0.1940
      0.7960
      0.7446
      0.9649
      0.4665
      0.2684
      0.3572
      0.9668
      0.6346
      0.1059
      0.8927
   

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
   

