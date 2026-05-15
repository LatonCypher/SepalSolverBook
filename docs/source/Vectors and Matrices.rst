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
      0.2841    0.2861    0.2418    0.7455    0.6419    0.5029    0.5992
   
   C = 
      0.9388
      0.3282
      0.3612
      0.7838
      0.5070
   
   M = 
      0.9725    0.0775    0.3008    0.2601    0.7257    0.2559    0.4854
      0.3814    0.1692    0.3222    0.4154    0.9713    0.4932    0.7635
      0.3024    0.1292    0.6787    0.8734    0.1162    0.0367    0.1915
      0.6015    0.5949    0.2394    0.5749    0.8740    0.4324    0.4021
      0.5318    0.3468    0.9774    0.1175    0.6320    0.3127    0.7304
      0.7972    0.4105    0.3467    0.0178    0.6286    0.8519    0.8922
      0.2915    0.9473    0.1103    0.6750    0.8824    0.4266    0.0143
      0.6688    0.1376    0.6457    0.2051    0.2703    0.3719    0.1264
   

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
      0.6119    0.1989    0.5326    0.5817
   
   R2 = 
      0.2398    0.9359    0.2079    0.0322    0.4740
   
   R3 = 
      0.6119    0.1989    0.5326    0.5817    0.2398    0.9359    0.2079    0.0322    0.4740
   
   C1 = 
      0.9236
      0.4899
      0.2185
      0.9153
      0.1048
      0.2757
      0.6985
      0.4943
      0.0489
      0.2771
   
   C2 = 
      0.2781
      0.5289
      0.3420
      0.8641
      0.0329
      0.5466
      0.9651
      0.4088
      0.0746
      0.4588
   
   M = 
      0.9236    0.2781
      0.4899    0.5289
      0.2185    0.3420
      0.9153    0.8641
      0.1048    0.0329
      0.2757    0.5466
      0.6985    0.9651
      0.4943    0.4088
      0.0489    0.0746
      0.2771    0.4588
   


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
      0.7880    0.5995    0.4906    0.1161
   
   R2 = 
      0.7916    0.4431    0.6255    0.9069
   
   M = 
      0.7880    0.5995    0.4906    0.1161
      0.7916    0.4431    0.6255    0.9069
   
   C1 = 
      0.1388
      0.0144
      0.2179
      0.3291
      0.3606
      0.3958
      0.4557
      0.9907
      0.6450
      0.4439
   
   C2 = 
      0.2669
      0.2980
   
   C3 = 
      0.1388
      0.0144
      0.2179
      0.3291
      0.3606
      0.3958
      0.4557
      0.9907
      0.6450
      0.4439
      0.2669
      0.2980
   

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
   

