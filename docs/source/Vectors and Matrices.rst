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
      0.8606    0.8853    0.3307    0.2160    0.1066    0.9871    0.5555
   
   C = 
      0.7858
      0.2515
      0.0916
      0.9776
      0.8012
   
   M = 
      0.1153    0.5540    0.6457    0.1698    0.9958    0.5738    0.0909
      0.8797    0.0422    0.9427    0.5837    0.4928    0.1879    0.4311
      0.2210    0.7381    0.7701    0.8664    0.3678    0.2667    0.1688
      0.3883    0.2088    0.2155    0.9770    0.9740    0.0172    0.8727
      0.2869    0.7179    0.9325    0.1133    0.3001    0.4094    0.3629
      0.0917    0.5639    0.7256    0.8332    0.2466    0.6296    0.8532
      0.0202    0.1156    0.3912    0.3842    0.0279    0.2791    0.7042
      0.6617    0.0846    0.9194    0.6312    0.4603    0.0161    0.3781
   

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
      0.1303    0.7143    0.7150    0.9372
   
   R2 = 
      0.6511    0.1421    0.6939    0.4730    0.8138
   
   R3 = 
      0.1303    0.7143    0.7150    0.9372    0.6511    0.1421    0.6939    0.4730    0.8138
   
   C1 = 
      0.5524
      0.4006
      0.4464
      0.9859
      0.5825
      0.8120
      0.4087
      0.0164
      0.6834
      0.8373
   
   C2 = 
      0.3222
      0.5718
      0.6880
      0.7497
      0.7649
      0.2634
      0.1144
      0.0316
      0.6699
      0.7904
   
   M = 
      0.5524    0.3222
      0.4006    0.5718
      0.4464    0.6880
      0.9859    0.7497
      0.5825    0.7649
      0.8120    0.2634
      0.4087    0.1144
      0.0164    0.0316
      0.6834    0.6699
      0.8373    0.7904
   


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
      0.3553    0.2833    0.4995    0.2166
   
   R2 = 
      0.5362    0.6207    0.4099    0.0549
   
   M = 
      0.3553    0.2833    0.4995    0.2166
      0.5362    0.6207    0.4099    0.0549
   
   C1 = 
      0.1965
      0.1696
      0.1721
      0.8408
      0.1442
      0.6573
      0.8055
      0.7493
      0.4067
      0.6122
   
   C2 = 
      0.3662
      0.8822
   
   C3 = 
      0.1965
      0.1696
      0.1721
      0.8408
      0.1442
      0.6573
      0.8055
      0.7493
      0.4067
      0.6122
      0.3662
      0.8822
   

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
   

