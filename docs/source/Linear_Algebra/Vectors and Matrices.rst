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
      0.1305    0.9246    0.4606    0.8687    0.7881    0.9733    0.4797
   
   C = 
      0.4480
      0.1931
      0.6698
      0.3559
      0.6481
   
   M = 
      0.0043    0.8307    0.1043    0.6129    0.0176    0.8442    0.8237
      0.5450    0.7250    0.2027    0.5432    0.0879    0.3437    0.4187
      0.0199    0.6934    0.6850    0.9127    0.9851    0.9044    0.5961
      0.0639    0.3825    0.3028    0.7990    0.9339    0.0980    0.7497
      0.7570    0.3438    0.0247    0.7289    0.3420    0.1088    0.7236
      0.3537    0.8595    0.0739    0.0627    0.0079    0.0180    0.3969
      0.8779    0.6285    0.5057    0.5913    0.2147    0.8723    0.1740
      0.8544    0.8843    0.1348    0.1795    0.7404    0.3102    0.4667
   

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
      0.4838    0.2920    0.4804    0.5956
   
   R2 = 
      0.5910    0.7478    0.3603    0.1838    0.9541
   
   R3 = 
      0.4838    0.2920    0.4804    0.5956    0.5910    0.7478    0.3603    0.1838    0.9541
   
   C1 = 
      0.2402
      0.4832
      0.2293
      0.6625
      0.9258
      0.2282
      0.2432
      0.9028
      0.7939
      0.4165
   
   C2 = 
      0.7490
      0.3069
      0.4482
      0.7124
      0.5793
      0.7261
      0.4063
      0.0105
      0.3884
      0.2003
   
   M = 
      0.2402    0.7490
      0.4832    0.3069
      0.2293    0.4482
      0.6625    0.7124
      0.9258    0.5793
      0.2282    0.7261
      0.2432    0.4063
      0.9028    0.0105
      0.7939    0.3884
      0.4165    0.2003
   


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
      0.0154    0.0736    0.3870    0.7201
   
   R2 = 
      0.1125    0.1973    0.7632    0.9035
   
   M = 
      0.0154    0.0736    0.3870    0.7201
      0.1125    0.1973    0.7632    0.9035
   
   C1 = 
      0.8619
      0.3660
      0.8763
      0.1401
      0.5434
      0.8861
      0.3501
      0.9110
      0.1336
      0.2743
   
   C2 = 
      0.6236
      0.4269
   
   C3 = 
      0.8619
      0.3660
      0.8763
      0.1401
      0.5434
      0.8861
      0.3501
      0.9110
      0.1336
      0.2743
      0.6236
      0.4269
   

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
   

