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
      0.0763    0.7889    0.4415    0.9104    0.6380    0.9300    0.5399
   
   C = 
      0.8145
      0.1050
      0.5031
      0.9387
      0.6038
   
   M = 
      0.5976    0.1354    0.3829    0.1843    0.6581    0.4458    0.5642
      0.1256    0.8250    0.2902    0.5315    0.8070    0.2186    0.4611
      0.7363    0.7823    0.7441    0.9484    0.9464    0.2501    0.6063
      0.1886    0.2526    0.9118    0.0103    0.2069    0.4417    0.8211
      0.2066    0.8453    0.4104    0.4687    0.1631    0.2685    0.6152
      0.5392    0.4436    0.3893    0.1724    0.6317    0.0554    0.7551
      0.3631    0.3687    0.2386    0.7273    0.4092    0.7676    0.2432
      0.7750    0.0698    0.5909    0.1945    0.8117    0.8712    0.1769
   

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
      0.5196    0.8495    0.5361    0.2461
   
   R2 = 
      0.0469    0.6401    0.5130    0.1532    0.9856
   
   R3 = 
      0.5196    0.8495    0.5361    0.2461    0.0469    0.6401    0.5130    0.1532    0.9856
   
   C1 = 
      0.9025
      0.9276
      0.3917
      0.0503
      0.1968
      0.8167
      0.1794
      0.9989
      0.1102
      0.4262
   
   C2 = 
      0.6649
      0.7314
      0.7032
      0.5190
      0.1684
      0.5531
      0.4317
      0.3851
      0.1967
      0.3636
   
   M = 
      0.9025    0.6649
      0.9276    0.7314
      0.3917    0.7032
      0.0503    0.5190
      0.1968    0.1684
      0.8167    0.5531
      0.1794    0.4317
      0.9989    0.3851
      0.1102    0.1967
      0.4262    0.3636
   


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
      0.1411    0.1632    0.6309    0.1505
   
   R2 = 
      0.6411    0.3650    0.9623    0.5966
   
   M = 
      0.1411    0.1632    0.6309    0.1505
      0.6411    0.3650    0.9623    0.5966
   
   C1 = 
      0.6869
      0.6739
      0.8456
      0.4802
      0.2961
      0.0215
      0.3823
      0.1124
      0.6561
      0.4875
   
   C2 = 
      0.0732
      0.0795
   
   C3 = 
      0.6869
      0.6739
      0.8456
      0.4802
      0.2961
      0.0215
      0.3823
      0.1124
      0.6561
      0.4875
      0.0732
      0.0795
   

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
   

