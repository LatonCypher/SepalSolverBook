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
      0.7285    0.6797    0.2312    0.2237    0.0323    0.2829    0.0149
   
   C = 
      0.2576
      0.5695
      0.7649
      0.1801
      0.6306
   
   M = 
      0.0964    0.8173    0.4507    0.0488    0.5916    0.1957    0.1350
      0.1262    0.5390    0.7481    0.7869    0.7992    0.9985    0.5022
      0.5903    0.1483    0.0956    0.7635    0.3190    0.0938    0.4751
      0.0671    0.4447    0.4841    0.5645    0.1069    0.3345    0.7035
      0.0159    0.0685    0.5445    0.4890    0.4619    0.9017    0.6913
      0.9775    0.7400    0.9916    0.5860    0.9740    0.1049    0.1895
      0.8564    0.0107    0.4439    0.3526    0.9534    0.7385    0.6587
      0.7260    0.0379    0.5082    0.0587    0.1689    0.4241    0.6225
   

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
      0.1757    0.5516    0.6832    0.3651
   
   R2 = 
      0.4387    0.6885    0.8723    0.5302    0.4200
   
   R3 = 
      0.1757    0.5516    0.6832    0.3651    0.4387    0.6885    0.8723    0.5302    0.4200
   
   C1 = 
      0.9330
      0.4222
      0.3559
      0.4281
      0.4296
      0.9412
      0.1303
      0.2499
      0.1640
      0.7842
   
   C2 = 
      0.6091
      0.1533
      0.2966
      0.7186
      0.7073
      0.8250
      0.6319
      0.2182
      0.1010
      0.6961
   
   M = 
      0.9330    0.6091
      0.4222    0.1533
      0.3559    0.2966
      0.4281    0.7186
      0.4296    0.7073
      0.9412    0.8250
      0.1303    0.6319
      0.2499    0.2182
      0.1640    0.1010
      0.7842    0.6961
   


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
      0.2122    0.4895    0.2963    0.5256
   
   R2 = 
      0.7972    0.4919    0.3745    0.0713
   
   M = 
      0.2122    0.4895    0.2963    0.5256
      0.7972    0.4919    0.3745    0.0713
   
   C1 = 
      0.3357
      0.8914
      0.8416
      0.6663
      0.4055
      0.4724
      0.4970
      0.8477
      0.4550
      0.2786
   
   C2 = 
      0.7192
      0.0800
   
   C3 = 
      0.3357
      0.8914
      0.8416
      0.6663
      0.4055
      0.4724
      0.4970
      0.8477
      0.4550
      0.2786
      0.7192
      0.0800
   

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
   

