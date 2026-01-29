Vectors and Matrices
====================

Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: RowVec, ColVec and Matrix. RowVec and ColVec are 1D arrays while Matrix is a 2D array. 

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
      5.0000    6.0000    7.0000    1.0000
   
   C = 
      8.0000
      3.0000
      4.0000
      2.0000
      7.0000
   
   M = 
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   


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
      0.4827    0.9305    0.3567    0.9961    0.7906    0.8740    0.2148
   
   C = 
      0.6982
      0.9656
      0.9812
      0.3876
      0.3799
   
   M = 
      0.8285    0.0924    0.8934    0.4500    0.0435    0.7307    0.6396
      0.7435    0.4808    0.8244    0.0257    0.9753    0.4599    0.8871
      0.8940    0.5675    0.9646    0.2082    0.9420    0.1110    0.1397
      0.5901    0.3463    0.6483    0.0452    0.6661    0.9356    0.8779
      0.1980    0.8872    0.1683    0.8552    0.5275    0.4730    0.9329
      0.1738    0.5930    0.6746    0.5651    0.6573    0.4777    0.8625
      0.6129    0.9854    0.5169    0.3282    0.8045    0.7123    0.1606
      0.8604    0.7256    0.5401    0.3940    0.9099    0.9361    0.2809
   

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
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   
   C = 
      1.0000
      1.0000
      1.0000
      1.0000
      1.0000
   
   M = 
      1.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    1.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    1.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    1.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    1.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    1.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    1.0000
   

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
      0.4142    0.5392    0.3031    0.7500
   
   R2 = 
      0.3021    0.1028    0.6995    0.2947    0.8867
   
   R3 = 
      0.4142    0.5392    0.3031    0.7500    0.3021    0.1028    0.6995    0.2947    0.8867
   
   C1 = 
      0.4790
      0.4145
      0.4971
      0.9840
      0.9767
      0.2928
      0.9395
      0.3423
      0.4291
      0.9890
   
   C2 = 
      0.2687
      0.9144
      0.3875
      0.6517
      0.8334
      0.2199
      0.4270
      0.0772
      0.5960
      0.3252
   
   M = 
      0.4790    0.2687
      0.4145    0.9144
      0.4971    0.3875
      0.9840    0.6517
      0.9767    0.8334
      0.2928    0.2199
      0.9395    0.4270
      0.3423    0.0772
      0.4291    0.5960
      0.9890    0.3252
   


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
      0.5451    0.8801    0.4560    0.3586
   
   R2 = 
      0.4317    0.8867    0.3895    0.5013
   
   M = 
      0.5451    0.8801    0.4560    0.3586
      0.4317    0.8867    0.3895    0.5013
   
   C1 = 
      0.1080
      0.4610
      0.4002
      0.4178
      0.9582
      0.0826
      0.1435
      0.5562
      0.4847
      0.0390
   
   C2 = 
      0.2139
      0.8452
   
   C3 = 
      0.1080
      0.4610
      0.4002
      0.4178
      0.9582
      0.0826
      0.1435
      0.5562
      0.4847
      0.0390
      0.2139
      0.8452
   

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
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   
   Flipud(M) = 
      0.0000    5.0000   -6.0000   -3.0000
      4.0000    8.0000    9.0000    1.0000
      2.0000    1.0000   -7.0000    3.0000
      5.0000   -2.0000    3.0000    7.0000
   
   Fliplr(M) = 
      7.0000    3.0000   -2.0000    5.0000
      3.0000   -7.0000    1.0000    2.0000
      1.0000    9.0000    8.0000    4.0000
     -3.0000   -6.0000    5.0000    0.0000
   

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
      5.0000   -2.0000    3.0000    7.0000
      0.0000    1.0000   -7.0000    3.0000
      0.0000    0.0000    9.0000    1.0000
      0.0000    0.0000    0.0000   -3.0000
   
   Tril(M) = 
      5.0000    0.0000    0.0000    0.0000
      2.0000    1.0000    0.0000    0.0000
      4.0000    8.0000    9.0000    0.0000
      0.0000    5.0000   -6.0000   -3.0000
   

