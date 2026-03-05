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
      0.3305    0.8240    0.3592    0.4201    0.2702    0.9703    0.7571
   
   C = 
      0.0300
      0.3821
      0.1231
      0.1224
      0.9477
   
   M = 
      0.5771    0.5458    0.8340    0.4607    0.4038    0.5643    0.0133
      0.3801    0.1194    0.0399    0.2929    0.7372    0.5455    0.5920
      0.6486    0.5141    0.8015    0.3653    0.9620    0.6177    0.3383
      0.4216    0.0590    0.2960    0.2776    0.8316    0.9985    0.2754
      0.1653    0.0337    0.4187    0.1944    0.6454    0.2584    0.4173
      0.0288    0.4733    0.0813    0.3640    0.5433    0.6369    0.6336
      0.5660    0.4625    0.3312    0.4378    0.2281    0.9874    0.2295
      0.7989    0.9135    0.1132    0.4632    0.9483    0.6849    0.1061
   

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
      0.9954    0.5182    0.9692    0.2727
   
   R2 = 
      0.7923    0.6415    0.8443    0.1107    0.1889
   
   R3 = 
      0.9954    0.5182    0.9692    0.2727    0.7923    0.6415    0.8443    0.1107    0.1889
   
   C1 = 
      0.2919
      0.0238
      0.6915
      0.5648
      0.1815
      0.9062
      0.2380
      0.1070
      0.0207
      0.4446
   
   C2 = 
      0.3906
      0.3930
      0.1615
      0.9914
      0.8276
      0.7538
      0.6671
      0.8157
      0.5909
      0.9236
   
   M = 
      0.2919    0.3906
      0.0238    0.3930
      0.6915    0.1615
      0.5648    0.9914
      0.1815    0.8276
      0.9062    0.7538
      0.2380    0.6671
      0.1070    0.8157
      0.0207    0.5909
      0.4446    0.9236
   


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
      0.6015    0.3442    0.8028    0.2963
   
   R2 = 
      0.4260    0.5243    0.9141    0.1224
   
   M = 
      0.6015    0.3442    0.8028    0.2963
      0.4260    0.5243    0.9141    0.1224
   
   C1 = 
      0.3932
      0.0046
      0.3225
      0.0739
      0.5167
      0.2619
      0.3191
      0.5635
      0.5915
      0.3251
   
   C2 = 
      0.5262
      0.9275
   
   C3 = 
      0.3932
      0.0046
      0.3225
      0.0739
      0.5167
      0.2619
      0.3191
      0.5635
      0.5915
      0.3251
      0.5262
      0.9275
   

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
   

