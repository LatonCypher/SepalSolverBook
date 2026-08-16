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
      0.3408    0.4771    0.2018    0.1315    0.8247    0.7431    0.0112
   
   C = 
      0.6404
      0.3563
      0.4054
      0.6549
      0.5426
   
   M = 
      0.2200    0.3615    0.6376    0.1093    0.4242    0.5938    0.3550
      0.2720    0.9079    0.7637    0.3128    0.9002    0.8761    0.1103
      0.8454    0.1511    0.5838    0.4349    0.8613    0.2663    0.4054
      0.3745    0.3098    0.9405    0.3829    0.0364    0.0178    0.9488
      0.3864    0.4172    0.1403    0.4578    0.0883    0.2745    0.1012
      0.9598    0.7470    0.3624    0.5528    0.8384    0.5277    0.8321
      0.0870    0.4344    0.9954    0.4480    0.1671    0.4440    0.2958
      0.2129    0.9812    0.8039    0.6421    0.5354    0.7338    0.5285
   

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
      0.9723    0.5128    0.2176    0.5148
   
   R2 = 
      0.3392    0.5221    0.9440    0.2666    0.8323
   
   R3 = 
      0.9723    0.5128    0.2176    0.5148    0.3392    0.5221    0.9440    0.2666    0.8323
   
   C1 = 
      0.0111
      0.9927
      0.1004
      0.0450
      0.6581
      0.4342
      0.9595
      0.2445
      0.5650
      0.5547
   
   C2 = 
      0.2701
      0.8769
      0.4659
      0.5733
      0.4500
      0.7537
      0.0837
      0.9517
      0.2559
      0.8796
   
   M = 
      0.0111    0.2701
      0.9927    0.8769
      0.1004    0.4659
      0.0450    0.5733
      0.6581    0.4500
      0.4342    0.7537
      0.9595    0.0837
      0.2445    0.9517
      0.5650    0.2559
      0.5547    0.8796
   


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
      0.3412    0.2229    0.7419    0.5755
   
   R2 = 
      0.4963    0.0361    0.2773    0.9844
   
   M = 
      0.3412    0.2229    0.7419    0.5755
      0.4963    0.0361    0.2773    0.9844
   
   C1 = 
      0.6298
      0.1612
      0.8009
      0.7666
      0.6063
      0.1633
      0.7198
      0.5983
      0.0308
      0.4191
   
   C2 = 
      0.4352
      0.6507
   
   C3 = 
      0.6298
      0.1612
      0.8009
      0.7666
      0.6063
      0.1633
      0.7198
      0.5983
      0.0308
      0.4191
      0.4352
      0.6507
   

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
   

