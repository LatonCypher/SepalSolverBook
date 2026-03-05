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
      0.1246    0.8493    0.2271    0.9887    0.2101    0.4541    0.9198
   
   C = 
      0.1404
      0.3795
      0.1393
      0.2800
      0.5905
   
   M = 
      0.0685    0.2497    0.6446    0.0262    0.3866    0.3200    0.4027
      0.6106    0.2317    0.0123    0.1955    0.6900    0.3746    0.6144
      0.3858    0.1754    0.0571    0.0860    0.6696    0.8660    0.1237
      0.8389    0.9498    0.7315    0.0259    0.0438    0.4515    0.1309
      0.0776    0.3147    0.8415    0.7034    0.8959    0.2047    0.1092
      0.1358    0.5655    0.2982    0.6952    0.7693    0.6751    0.0283
      0.4853    0.1561    0.4521    0.9062    0.6301    0.4339    0.1669
      0.6233    0.2050    0.6106    0.5607    0.7878    0.5824    0.9701
   

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
      0.0053    0.2527    0.3309    0.5002
   
   R2 = 
      0.3785    0.2857    0.5123    0.6734    0.9173
   
   R3 = 
      0.0053    0.2527    0.3309    0.5002    0.3785    0.2857    0.5123    0.6734    0.9173
   
   C1 = 
      0.1667
      0.4246
      0.2692
      0.0280
      0.0118
      0.0216
      0.1769
      0.6332
      0.6510
      0.8405
   
   C2 = 
      0.7366
      0.7111
      0.5919
      0.8480
      0.6780
      0.5707
      0.9055
      0.1093
      0.1372
      0.4710
   
   M = 
      0.1667    0.7366
      0.4246    0.7111
      0.2692    0.5919
      0.0280    0.8480
      0.0118    0.6780
      0.0216    0.5707
      0.1769    0.9055
      0.6332    0.1093
      0.6510    0.1372
      0.8405    0.4710
   


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
      0.7375    0.0362    0.1132    0.6651
   
   R2 = 
      0.0650    0.1781    0.2057    0.9153
   
   M = 
      0.7375    0.0362    0.1132    0.6651
      0.0650    0.1781    0.2057    0.9153
   
   C1 = 
      0.0463
      0.8250
      0.4336
      0.6530
      0.1768
      0.8567
      0.2823
      0.2942
      0.8532
      0.9065
   
   C2 = 
      0.2780
      0.7727
   
   C3 = 
      0.0463
      0.8250
      0.4336
      0.6530
      0.1768
      0.8567
      0.2823
      0.2942
      0.8532
      0.9065
      0.2780
      0.7727
   

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
   

