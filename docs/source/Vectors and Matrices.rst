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
      0.8443    0.4689    0.0764    0.2256    0.8805    0.0646    0.9270
   
   C = 
      0.8964
      0.8921
      0.0324
      0.0124
      0.6495
   
   M = 
      0.4595    0.3762    0.1427    0.5350    0.4791    0.4557    0.1951
      0.2866    0.4196    0.5565    0.2826    0.3020    0.0753    0.4809
      0.2184    0.3459    0.6524    0.7045    0.4131    0.5836    0.9888
      0.3510    0.9157    0.7827    0.6484    0.6241    0.8167    0.3288
      0.4878    0.6241    0.6845    0.1007    0.4701    0.4628    0.9052
      0.3731    0.4395    0.7121    0.8326    0.0242    0.6204    0.0100
      0.4292    0.3364    0.6164    0.6800    0.6760    0.5244    0.7650
      0.3997    0.8282    0.4183    0.3739    0.0306    0.2159    0.7850
   

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
      0.7419    0.7180    0.2276    0.7079
   
   R2 = 
      0.7739    0.5031    0.0059    0.0472    0.9863
   
   R3 = 
      0.7419    0.7180    0.2276    0.7079    0.7739    0.5031    0.0059    0.0472    0.9863
   
   C1 = 
      0.1201
      0.5963
      0.3723
      0.8397
      0.7054
      0.2119
      0.2043
      0.5230
      0.7423
      0.2208
   
   C2 = 
      0.8379
      0.6771
      0.9571
      0.6620
      0.4538
      0.3575
      0.3013
      0.2799
      0.2850
      0.7694
   
   M = 
      0.1201    0.8379
      0.5963    0.6771
      0.3723    0.9571
      0.8397    0.6620
      0.7054    0.4538
      0.2119    0.3575
      0.2043    0.3013
      0.5230    0.2799
      0.7423    0.2850
      0.2208    0.7694
   


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
      0.2561    0.4341    0.7287    0.0851
   
   R2 = 
      0.7938    0.0931    0.4679    0.3237
   
   M = 
      0.2561    0.4341    0.7287    0.0851
      0.7938    0.0931    0.4679    0.3237
   
   C1 = 
      0.4710
      0.2878
      0.8644
      0.4574
      0.9171
      0.8797
      0.0050
      0.7708
      0.6965
      0.0612
   
   C2 = 
      0.4384
      0.9840
   
   C3 = 
      0.4710
      0.2878
      0.8644
      0.4574
      0.9171
      0.8797
      0.0050
      0.7708
      0.6965
      0.0612
      0.4384
      0.9840
   

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
   

