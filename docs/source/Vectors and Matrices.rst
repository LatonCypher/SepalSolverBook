Vectors and Matrices
####################

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
      0.3751    0.2505    0.1746    0.4958    0.7387    0.3434    0.4085
   
   C = 
      0.4889
      0.0781
      0.8347
      0.5037
      0.8520
   
   M = 
      0.9944    0.4309    0.0602    0.0333    0.0974    0.5494    0.0361
      0.7994    0.1199    0.2416    0.5206    0.9771    0.3347    0.9180
      0.7290    0.4697    0.0133    0.5103    0.4932    0.2560    0.4199
      0.0321    0.9898    0.9270    0.9041    0.3573    0.0586    0.0510
      0.4542    0.8483    0.4297    0.0047    0.2031    0.4126    0.2083
      0.6403    0.8444    0.6667    0.0429    0.2074    0.2015    0.2687
      0.2065    0.8620    0.6920    0.9185    0.6674    0.8401    0.9982
      0.8380    0.2431    0.8865    0.8766    0.2724    0.7992    0.2097
   

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
      0.3566    0.8888    0.3979    0.4080
   
   R2 = 
      0.2364    0.3793    0.7961    0.6692    0.8773
   
   R3 = 
      0.3566    0.8888    0.3979    0.4080    0.2364    0.3793    0.7961    0.6692    0.8773
   
   C1 = 
      0.0202
      0.5695
      0.3286
      0.6401
      0.6490
      0.3013
      0.2975
      0.8968
      0.2418
      0.3297
   
   C2 = 
      0.6297
      0.9283
      0.5974
      0.4611
      0.1345
      0.2031
      0.9884
      0.6949
      0.6627
      0.8606
   
   M = 
      0.0202    0.6297
      0.5695    0.9283
      0.3286    0.5974
      0.6401    0.4611
      0.6490    0.1345
      0.3013    0.2031
      0.2975    0.9884
      0.8968    0.6949
      0.2418    0.6627
      0.3297    0.8606
   


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
      0.2409    0.8250    0.0759    0.4491
   
   R2 = 
      0.4683    0.4830    0.6068    0.2508
   
   M = 
      0.2409    0.8250    0.0759    0.4491
      0.4683    0.4830    0.6068    0.2508
   
   C1 = 
      0.1500
      0.5055
      0.9319
      0.5347
      0.3408
      0.6725
      0.2205
      0.5069
      0.0147
      0.9717
   
   C2 = 
      0.3771
      0.1914
   
   C3 = 
      0.1500
      0.5055
      0.9319
      0.5347
      0.3408
      0.6725
      0.2205
      0.5069
      0.0147
      0.9717
      0.3771
      0.1914
   

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
   

