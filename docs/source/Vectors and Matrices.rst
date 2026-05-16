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
      0.8562    0.8653    0.8399    0.4832    0.3550    0.7032    0.7533
   
   C = 
      0.8063
      0.2781
      0.5589
      0.1243
      0.6302
   
   M = 
      0.4109    0.1474    0.8903    0.5840    0.6836    0.6109    0.5087
      0.2043    0.6504    0.4520    0.6040    0.6498    0.7999    0.2549
      0.9222    0.2189    0.3101    0.4824    0.7549    0.4473    0.0948
      0.4826    0.5083    0.3040    0.5025    0.0348    0.6639    0.3140
      0.3224    0.1653    0.8848    0.3335    0.4308    0.5137    0.8151
      0.3299    0.4798    0.1937    0.9753    0.4120    0.1679    0.0912
      0.4261    0.1658    0.1898    0.5261    0.1027    0.2139    0.3608
      0.2067    0.4287    0.8792    0.1679    0.5938    0.0952    0.3695
   

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
      0.5156    0.1398    0.6971    0.0076
   
   R2 = 
      0.2682    0.8784    0.0893    0.4091    0.3213
   
   R3 = 
      0.5156    0.1398    0.6971    0.0076    0.2682    0.8784    0.0893    0.4091    0.3213
   
   C1 = 
      0.2508
      0.4640
      0.2058
      0.1910
      0.5290
      0.3637
      0.9870
      0.3170
      0.4627
      0.3199
   
   C2 = 
      0.0504
      0.0414
      0.9480
      0.7556
      0.8771
      0.3866
      0.8361
      0.6172
      0.9767
      0.7826
   
   M = 
      0.2508    0.0504
      0.4640    0.0414
      0.2058    0.9480
      0.1910    0.7556
      0.5290    0.8771
      0.3637    0.3866
      0.9870    0.8361
      0.3170    0.6172
      0.4627    0.9767
      0.3199    0.7826
   


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
      0.6995    0.7631    0.4912    0.2801
   
   R2 = 
      0.5907    0.0165    0.5952    0.8286
   
   M = 
      0.6995    0.7631    0.4912    0.2801
      0.5907    0.0165    0.5952    0.8286
   
   C1 = 
      0.6913
      0.1991
      0.4338
      0.6680
      0.6492
      0.9752
      0.5630
      0.6483
      0.5820
      0.4457
   
   C2 = 
      0.0692
      0.8259
   
   C3 = 
      0.6913
      0.1991
      0.4338
      0.6680
      0.6492
      0.9752
      0.5630
      0.6483
      0.5820
      0.4457
      0.0692
      0.8259
   

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
   

