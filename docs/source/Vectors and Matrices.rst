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
      0.9068    0.5522    0.6906    0.4623    0.0334    0.0662    0.9917
   
   C = 
      0.2604
      0.6575
      0.9037
      0.0022
      0.1485
   
   M = 
      0.4227    0.0372    0.7223    0.4479    0.3987    0.1460    0.8052
      0.4022    0.1392    0.3889    0.8740    0.2865    0.0173    0.7580
      0.2388    0.3616    0.6660    0.7696    0.0697    0.6408    0.8775
      0.6761    0.9816    0.9513    0.2891    0.9042    0.4312    0.8448
      0.1265    0.7607    0.0854    0.9324    0.9444    0.6450    0.2346
      0.6317    0.4717    0.3285    0.7028    0.6556    0.7061    0.0363
      0.4595    0.5362    0.0575    0.0786    0.3723    0.0858    0.6475
      0.2078    0.1647    0.2244    0.1549    0.2924    0.5659    0.2953
   

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
      0.0340    0.3152    0.2459    0.1266
   
   R2 = 
      0.6651    0.4829    0.3943    0.5039    0.2698
   
   R3 = 
      0.0340    0.3152    0.2459    0.1266    0.6651    0.4829    0.3943    0.5039    0.2698
   
   C1 = 
      0.1415
      0.3895
      0.9087
      0.8982
      0.1672
      0.8171
      0.6768
      0.2325
      0.4947
      0.3698
   
   C2 = 
      0.1473
      0.6092
      0.2062
      0.7866
      0.2791
      0.3964
      0.6259
      0.5719
      0.3724
      0.4473
   
   M = 
      0.1415    0.1473
      0.3895    0.6092
      0.9087    0.2062
      0.8982    0.7866
      0.1672    0.2791
      0.8171    0.3964
      0.6768    0.6259
      0.2325    0.5719
      0.4947    0.3724
      0.3698    0.4473
   


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
      0.9575    0.4492    0.0453    0.8871
   
   R2 = 
      0.8946    0.4484    0.7545    0.5712
   
   M = 
      0.9575    0.4492    0.0453    0.8871
      0.8946    0.4484    0.7545    0.5712
   
   C1 = 
      0.3386
      0.9894
      0.6627
      0.6482
      0.8723
      0.4462
      0.4524
      0.4084
      0.8780
      0.0793
   
   C2 = 
      0.8885
      0.2785
   
   C3 = 
      0.3386
      0.9894
      0.6627
      0.6482
      0.8723
      0.4462
      0.4524
      0.4084
      0.8780
      0.0793
      0.8885
      0.2785
   

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
   

