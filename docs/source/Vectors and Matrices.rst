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
      0.3895    0.9663    0.7306    0.5145    0.1926    0.1902    0.0024
   
   C = 
      0.2959
      0.1161
      0.7629
      0.8722
      0.6813
   
   M = 
      0.5943    0.6073    0.4092    0.9785    0.9080    0.9039    0.5023
      0.7874    0.1339    0.5812    0.8819    0.1078    0.5491    0.3552
      0.9414    0.4637    0.8292    0.2580    0.7095    0.6220    0.3407
      0.2571    0.6446    0.7934    0.9738    0.4866    0.6162    0.2387
      0.8240    0.4486    0.6783    0.1619    0.2191    0.8452    0.0045
      0.3285    0.5034    0.8573    0.9604    0.4369    0.7518    0.6718
      0.2317    0.3045    0.2045    0.0141    0.5426    0.4643    0.6493
      0.2671    0.0779    0.8922    0.3324    0.0444    0.1854    0.8112
   

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
      0.5974    0.4427    0.3488    0.2742
   
   R2 = 
      0.7712    0.2958    0.3053    0.9064    0.1981
   
   R3 = 
      0.5974    0.4427    0.3488    0.2742    0.7712    0.2958    0.3053    0.9064    0.1981
   
   C1 = 
      0.6019
      0.4764
      0.0337
      0.9819
      0.6867
      0.5680
      0.3012
      0.8706
      0.6453
      0.4135
   
   C2 = 
      0.5013
      0.5280
      0.1851
      0.0159
      0.5809
      0.9962
      0.7240
      0.9189
      0.8339
      0.6716
   
   M = 
      0.6019    0.5013
      0.4764    0.5280
      0.0337    0.1851
      0.9819    0.0159
      0.6867    0.5809
      0.5680    0.9962
      0.3012    0.7240
      0.8706    0.9189
      0.6453    0.8339
      0.4135    0.6716
   


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
      0.7762    0.5886    0.7233    0.3986
   
   R2 = 
      0.0666    0.1845    0.7731    0.7245
   
   M = 
      0.7762    0.5886    0.7233    0.3986
      0.0666    0.1845    0.7731    0.7245
   
   C1 = 
      0.4236
      0.9343
      0.9964
      0.6115
      0.1089
      0.3276
      0.9354
      0.7857
      0.4981
      0.7466
   
   C2 = 
      0.3822
      0.4008
   
   C3 = 
      0.4236
      0.9343
      0.9964
      0.6115
      0.1089
      0.3276
      0.9354
      0.7857
      0.4981
      0.7466
      0.3822
      0.4008
   

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
   

