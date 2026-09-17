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
      0.0223    0.2839    0.9781    0.8568    0.3336    0.0442    0.4674
   
   C = 
      0.7049
      0.7336
      0.1653
      0.8045
      0.9541
   
   M = 
      0.9550    0.6912    0.9040    0.6340    0.1616    0.9133    0.8811
      0.6062    0.6540    0.1855    0.8168    0.9942    0.4753    0.7541
      0.2159    0.4065    0.5127    0.5514    0.3598    0.6464    0.7331
      0.1582    0.4200    0.5151    0.4404    0.5200    0.5669    0.4597
      0.7880    0.1295    0.5585    0.8479    0.7086    0.8803    0.8832
      0.3657    0.6136    0.3135    0.2398    0.9505    0.5519    0.9369
      0.3789    0.0299    0.3114    0.8093    0.7267    0.6268    0.4371
      0.9732    0.9968    0.9746    0.4640    0.5209    0.8737    0.8705
   

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
      0.3279    0.4045    0.6235    0.6760
   
   R2 = 
      0.6329    0.4074    0.4700    0.7340    0.3842
   
   R3 = 
      0.3279    0.4045    0.6235    0.6760    0.6329    0.4074    0.4700    0.7340    0.3842
   
   C1 = 
      0.0883
      0.6310
      0.4232
      0.0518
      0.3236
      0.7956
      0.6763
      0.0338
      0.8539
      0.6127
   
   C2 = 
      0.3870
      0.3413
      0.8524
      0.4522
      0.5432
      0.3748
      0.3028
      0.0715
      0.7319
      0.6326
   
   M = 
      0.0883    0.3870
      0.6310    0.3413
      0.4232    0.8524
      0.0518    0.4522
      0.3236    0.5432
      0.7956    0.3748
      0.6763    0.3028
      0.0338    0.0715
      0.8539    0.7319
      0.6127    0.6326
   


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
      0.5556    0.8294    0.1536    0.7349
   
   R2 = 
      0.0355    0.2743    0.1180    0.6788
   
   M = 
      0.5556    0.8294    0.1536    0.7349
      0.0355    0.2743    0.1180    0.6788
   
   C1 = 
      0.7394
      0.6124
      0.3480
      0.2066
      0.1598
      0.8428
      0.3829
      0.0302
      0.4524
      0.6418
   
   C2 = 
      0.5647
      0.3054
   
   C3 = 
      0.7394
      0.6124
      0.3480
      0.2066
      0.1598
      0.8428
      0.3829
      0.0302
      0.4524
      0.6418
      0.5647
      0.3054
   

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
   

