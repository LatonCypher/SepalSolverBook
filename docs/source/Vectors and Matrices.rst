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
      0.2599    0.7262    0.1844    0.8876    0.0454    0.2602    0.2300
   
   C = 
      0.6477
      0.2757
      0.7004
      0.3273
      0.9937
   
   M = 
      0.6978    0.5910    0.3991    0.9671    0.3005    0.3665    0.0671
      0.0217    0.5502    0.4659    0.7760    0.9593    0.1062    0.5166
      0.0577    0.5746    0.4017    0.8863    0.2331    0.2373    0.0619
      0.7124    0.2982    0.4354    0.1035    0.9175    0.9497    0.4954
      0.5593    0.3880    0.9130    0.7174    0.8291    0.7773    0.8850
      0.5824    0.6686    0.4939    0.7711    0.9082    0.8133    0.6016
      0.6172    0.2869    0.3504    0.6812    0.4413    0.7411    0.4113
      0.9677    0.0747    0.8872    0.2684    0.6779    0.6576    0.7610
   

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
      0.1049    0.4518    0.0196    0.7088
   
   R2 = 
      0.8744    0.9677    0.5189    0.0245    0.2593
   
   R3 = 
      0.1049    0.4518    0.0196    0.7088    0.8744    0.9677    0.5189    0.0245    0.2593
   
   C1 = 
      0.3859
      0.7017
      0.7519
      0.9677
      0.0823
      0.4887
      0.6291
      0.3950
      0.4182
      0.6137
   
   C2 = 
      0.8739
      0.7851
      0.0190
      0.5724
      0.7250
      0.7809
      0.1516
      0.7586
      0.2595
      0.7961
   
   M = 
      0.3859    0.8739
      0.7017    0.7851
      0.7519    0.0190
      0.9677    0.5724
      0.0823    0.7250
      0.4887    0.7809
      0.6291    0.1516
      0.3950    0.7586
      0.4182    0.2595
      0.6137    0.7961
   


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
      0.9927    0.8608    0.3389    0.5193
   
   R2 = 
      0.4907    0.1198    0.0713    0.0902
   
   M = 
      0.9927    0.8608    0.3389    0.5193
      0.4907    0.1198    0.0713    0.0902
   
   C1 = 
      0.3760
      0.8866
      0.4496
      0.4592
      0.2548
      0.0903
      0.6670
      0.7066
      0.2555
      0.4895
   
   C2 = 
      0.8862
      0.0408
   
   C3 = 
      0.3760
      0.8866
      0.4496
      0.4592
      0.2548
      0.0903
      0.6670
      0.7066
      0.2555
      0.4895
      0.8862
      0.0408
   

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
   

