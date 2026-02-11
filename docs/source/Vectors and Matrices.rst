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
      0.9794    0.5068    0.6631    0.5496    0.7366    0.8677    0.9935
   
   C = 
      0.1751
      0.3768
      0.8939
      0.5463
      0.3201
   
   M = 
      0.7607    0.2646    0.7619    0.6924    0.9229    0.6992    0.6504
      0.6480    0.0628    0.6490    0.3466    0.0885    0.7680    0.9830
      0.3093    0.9298    0.0011    0.0654    0.9640    0.7022    0.4934
      0.9804    0.1287    0.0235    0.4024    0.8062    0.5318    0.7234
      0.7391    0.7272    0.1735    0.5939    0.4670    0.9901    0.0185
      0.7493    0.1031    0.7460    0.5815    0.7581    0.7914    0.4160
      0.2617    0.7316    0.1628    0.7596    0.4540    0.5154    0.4643
      0.0775    0.1215    0.8611    0.8740    0.4179    0.3915    0.0471
   

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
      0.2848    0.6765    0.5969    0.7394
   
   R2 = 
      0.7308    0.4400    0.4375    0.6972    0.7412
   
   R3 = 
      0.2848    0.6765    0.5969    0.7394    0.7308    0.4400    0.4375    0.6972    0.7412
   
   C1 = 
      0.4141
      0.1177
      0.7359
      0.1551
      0.5404
      0.7852
      0.5628
      0.2781
      0.1615
      0.8723
   
   C2 = 
      0.2980
      0.8646
      0.6311
      0.9285
      0.3851
      0.2901
      0.6117
      0.4465
      0.2186
      0.8932
   
   M = 
      0.4141    0.2980
      0.1177    0.8646
      0.7359    0.6311
      0.1551    0.9285
      0.5404    0.3851
      0.7852    0.2901
      0.5628    0.6117
      0.2781    0.4465
      0.1615    0.2186
      0.8723    0.8932
   


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
      0.6893    0.8284    0.5938    0.0105
   
   R2 = 
      0.3880    0.0667    0.0356    0.0808
   
   M = 
      0.6893    0.8284    0.5938    0.0105
      0.3880    0.0667    0.0356    0.0808
   
   C1 = 
      0.3606
      0.6983
      0.4656
      0.1267
      0.9524
      0.1290
      0.4725
      0.7123
      0.1068
      0.6086
   
   C2 = 
      0.8363
      0.2334
   
   C3 = 
      0.3606
      0.6983
      0.4656
      0.1267
      0.9524
      0.1290
      0.4725
      0.7123
      0.1068
      0.6086
      0.8363
      0.2334
   

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
   

