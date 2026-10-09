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
      0.9501    0.0652    0.5164    0.7482    0.1423    0.7232    0.5257
   
   C = 
      0.6058
      0.3304
      0.7940
      0.7259
      0.1082
   
   M = 
      0.7286    0.0131    0.8215    0.1195    0.9729    0.2017    0.4594
      0.4923    0.4974    0.6860    0.8634    0.1873    0.3682    0.4498
      0.3757    0.7177    0.8748    0.1982    0.5639    0.5235    0.8296
      0.5287    0.1903    0.1446    0.8917    0.5992    0.8224    0.1092
      0.5912    0.6967    0.8024    0.2074    0.7791    0.2418    0.5874
      0.5166    0.8754    0.3796    0.6505    0.4377    0.4157    0.0034
      0.5654    0.4168    0.8309    0.9665    0.8845    0.9250    0.1197
      0.7773    0.1754    0.9257    0.0266    0.3101    0.7781    0.7711
   

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
      0.6953    0.0065    0.1885    0.5087
   
   R2 = 
      0.3668    0.1017    0.0761    0.7912    0.1705
   
   R3 = 
      0.6953    0.0065    0.1885    0.5087    0.3668    0.1017    0.0761    0.7912    0.1705
   
   C1 = 
      0.7978
      0.5064
      0.9607
      0.7874
      0.6847
      0.0145
      0.1191
      0.1163
      0.2733
      0.2984
   
   C2 = 
      0.1540
      0.6136
      0.3394
      0.0804
      0.5472
      0.7102
      0.9925
      0.8977
      0.6114
      0.3234
   
   M = 
      0.7978    0.1540
      0.5064    0.6136
      0.9607    0.3394
      0.7874    0.0804
      0.6847    0.5472
      0.0145    0.7102
      0.1191    0.9925
      0.1163    0.8977
      0.2733    0.6114
      0.2984    0.3234
   


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
      0.7845    0.9983    0.1726    0.0285
   
   R2 = 
      0.0363    0.0776    0.2658    0.7010
   
   M = 
      0.7845    0.9983    0.1726    0.0285
      0.0363    0.0776    0.2658    0.7010
   
   C1 = 
      0.6565
      0.8307
      0.7989
      0.7920
      0.4673
      0.3944
      0.7166
      0.9830
      0.4176
      0.3207
   
   C2 = 
      0.4002
      0.3788
   
   C3 = 
      0.6565
      0.8307
      0.7989
      0.7920
      0.4673
      0.3944
      0.7166
      0.9830
      0.4176
      0.3207
      0.4002
      0.3788
   

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
   

