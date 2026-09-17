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
      0.5077    0.6691    0.9477    0.5829    0.1199    0.3182    0.4465
   
   C = 
      0.5936
      0.4832
      0.6950
      0.7254
      0.7841
   
   M = 
      0.2773    0.7688    0.2602    0.9699    0.9352    0.1100    0.1765
      0.4771    0.3579    0.5868    0.3727    0.8811    0.4163    0.9901
      0.4003    0.9600    0.7245    0.8774    0.1147    0.6515    0.1533
      0.7360    0.2193    0.6975    0.3320    0.0266    0.5376    0.4360
      0.1929    0.9521    0.0953    0.4500    0.7821    0.5044    0.5516
      0.4886    0.5680    0.2826    0.7425    0.2524    0.6098    0.7928
      0.2813    0.5513    0.9538    0.9263    0.4066    0.8137    0.9557
      0.7391    0.8060    0.9966    0.2429    0.6189    0.1166    0.7355
   

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
      0.2963    0.8204    0.7981    0.1230
   
   R2 = 
      0.0061    0.3825    0.0793    0.7041    0.3901
   
   R3 = 
      0.2963    0.8204    0.7981    0.1230    0.0061    0.3825    0.0793    0.7041    0.3901
   
   C1 = 
      0.8290
      0.0099
      0.9481
      0.0667
      0.1681
      0.5533
      0.0181
      0.5289
      0.1084
      0.4170
   
   C2 = 
      0.9377
      0.7956
      0.6809
      0.0260
      0.7265
      0.5651
      0.3068
      0.1265
      0.4516
      0.7204
   
   M = 
      0.8290    0.9377
      0.0099    0.7956
      0.9481    0.6809
      0.0667    0.0260
      0.1681    0.7265
      0.5533    0.5651
      0.0181    0.3068
      0.5289    0.1265
      0.1084    0.4516
      0.4170    0.7204
   


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
      0.7070    0.2334    0.1754    0.1462
   
   R2 = 
      0.2417    0.9655    0.0752    0.3628
   
   M = 
      0.7070    0.2334    0.1754    0.1462
      0.2417    0.9655    0.0752    0.3628
   
   C1 = 
      0.6796
      0.1978
      0.3710
      0.5726
      0.3934
      0.6349
      0.7364
      0.8882
      0.6133
      0.5938
   
   C2 = 
      0.4505
      0.7957
   
   C3 = 
      0.6796
      0.1978
      0.3710
      0.5726
      0.3934
      0.6349
      0.7364
      0.8882
      0.6133
      0.5938
      0.4505
      0.7957
   

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
   

