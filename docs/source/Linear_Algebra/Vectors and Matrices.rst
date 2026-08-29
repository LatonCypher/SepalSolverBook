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
      0.2690    0.2249    0.2594    0.5791    0.1911    0.4847    0.6275
   
   C = 
      0.9465
      0.8114
      0.1508
      0.0142
      0.9329
   
   M = 
      0.4004    0.4586    0.1442    0.6440    0.6611    0.9053    0.5716
      0.3025    0.9510    0.2591    0.2102    0.9526    0.8682    0.3114
      0.4180    0.2040    0.5784    0.6246    0.3978    0.1673    0.3763
      0.6292    0.6859    0.8549    0.8309    0.1325    0.2726    0.4533
      0.5613    0.3276    0.0797    0.7408    0.9754    0.5641    0.6835
      0.4221    0.8391    0.7076    0.1619    0.5597    0.1119    0.0781
      0.6302    0.1439    0.1248    0.3654    0.1297    0.7661    0.0302
      0.9651    0.7699    0.4197    0.1319    0.1409    0.8088    0.4901
   

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
      0.2411    0.6431    0.5022    0.4997
   
   R2 = 
      0.6884    0.6817    0.7998    0.4628    0.4361
   
   R3 = 
      0.2411    0.6431    0.5022    0.4997    0.6884    0.6817    0.7998    0.4628    0.4361
   
   C1 = 
      0.2730
      0.9564
      0.2025
      0.2858
      0.6543
      0.7934
      0.7274
      0.6022
      0.1736
      0.4279
   
   C2 = 
      0.5823
      0.6103
      0.7922
      0.3891
      0.6138
      0.5388
      0.2657
      0.2764
      0.1021
      0.7841
   
   M = 
      0.2730    0.5823
      0.9564    0.6103
      0.2025    0.7922
      0.2858    0.3891
      0.6543    0.6138
      0.7934    0.5388
      0.7274    0.2657
      0.6022    0.2764
      0.1736    0.1021
      0.4279    0.7841
   


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
      0.7379    0.0899    0.9110    0.3079
   
   R2 = 
      0.3947    0.6147    0.4243    0.9162
   
   M = 
      0.7379    0.0899    0.9110    0.3079
      0.3947    0.6147    0.4243    0.9162
   
   C1 = 
      0.8426
      0.2288
      0.8495
      0.7880
      0.7553
      0.6746
      0.3284
      0.9411
      0.9797
      0.5774
   
   C2 = 
      0.8429
      0.4148
   
   C3 = 
      0.8426
      0.2288
      0.8495
      0.7880
      0.7553
      0.6746
      0.3284
      0.9411
      0.9797
      0.5774
      0.8429
      0.4148
   

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
   

