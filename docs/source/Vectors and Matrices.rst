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
      0.9546    0.2079    0.9679    0.8895    0.2985    0.4132    0.1190
   
   C = 
      0.3275
      0.2113
      0.0965
      0.4747
      0.9768
   
   M = 
      0.5022    0.2757    0.1793    0.3347    0.5736    0.8696    0.6646
      0.5397    0.1386    0.8927    0.7998    0.0487    0.6844    0.5126
      0.5864    0.7246    0.1852    0.2741    0.4346    0.9433    0.7863
      0.1853    0.7543    0.6770    0.9166    0.0652    0.3177    0.9216
      0.0055    0.2090    0.7990    0.1532    0.5757    0.3680    0.1889
      0.7771    0.9154    0.5308    0.6168    0.4800    0.9115    0.6663
      0.5123    0.6078    0.4635    0.7398    0.2319    0.7971    0.3256
      0.0209    0.9468    0.2117    0.1338    0.6541    0.9461    0.2319
   

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
      0.0512    0.8139    0.6679    0.6443
   
   R2 = 
      0.9819    0.2237    0.1446    0.4255    0.9870
   
   R3 = 
      0.0512    0.8139    0.6679    0.6443    0.9819    0.2237    0.1446    0.4255    0.9870
   
   C1 = 
      0.4280
      0.4435
      0.7141
      0.4987
      0.6997
      0.3022
      0.5426
      0.4340
      0.0802
      0.4873
   
   C2 = 
      0.6963
      0.9837
      0.0777
      0.1936
      0.3053
      0.6342
      0.6830
      0.2788
      0.8450
      0.3342
   
   M = 
      0.4280    0.6963
      0.4435    0.9837
      0.7141    0.0777
      0.4987    0.1936
      0.6997    0.3053
      0.3022    0.6342
      0.5426    0.6830
      0.4340    0.2788
      0.0802    0.8450
      0.4873    0.3342
   


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
      0.5473    0.9874    0.1217    0.4229
   
   R2 = 
      0.5956    0.3291    0.0201    0.7358
   
   M = 
      0.5473    0.9874    0.1217    0.4229
      0.5956    0.3291    0.0201    0.7358
   
   C1 = 
      0.8181
      0.0956
      0.4720
      0.6620
      0.0604
      0.8512
      0.4547
      0.6197
      0.2353
      0.5917
   
   C2 = 
      0.7233
      0.7308
   
   C3 = 
      0.8181
      0.0956
      0.4720
      0.6620
      0.0604
      0.8512
      0.4547
      0.6197
      0.2353
      0.5917
      0.7233
      0.7308
   

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
   

