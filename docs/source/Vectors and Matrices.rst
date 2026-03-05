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
      0.2163    0.1158    0.7667    0.8907    0.3552    0.3998    0.9742
   
   C = 
      0.6093
      0.7259
      0.3319
      0.6993
      0.4160
   
   M = 
      0.8388    0.4309    0.7496    0.7971    0.2626    0.4676    0.7409
      0.2465    0.8029    0.5829    0.6995    0.4755    0.9938    0.8319
      0.9336    0.3904    0.8871    0.4957    0.7417    0.2176    0.1664
      0.6458    0.6888    0.7322    0.0136    0.3463    0.0494    0.4539
      0.8756    0.9966    0.7695    0.0772    0.4347    0.0400    0.9397
      0.0898    0.2937    0.6483    0.2365    0.6300    0.8248    0.7352
      0.8394    0.8024    0.8383    0.3673    0.1714    0.0545    0.1664
      0.8141    0.8693    0.5892    0.3516    0.8512    0.2419    0.4857
   

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
      0.6760    0.1043    0.5675    0.9214
   
   R2 = 
      0.1488    0.1650    0.0011    0.5331    0.9296
   
   R3 = 
      0.6760    0.1043    0.5675    0.9214    0.1488    0.1650    0.0011    0.5331    0.9296
   
   C1 = 
      0.8916
      0.0870
      0.7947
      0.6162
      0.8160
      0.9123
      0.2837
      0.9581
      0.0090
      0.8065
   
   C2 = 
      0.5580
      0.1116
      0.1264
      0.0944
      0.7306
      0.9524
      0.7334
      0.7161
      0.9202
      0.4049
   
   M = 
      0.8916    0.5580
      0.0870    0.1116
      0.7947    0.1264
      0.6162    0.0944
      0.8160    0.7306
      0.9123    0.9524
      0.2837    0.7334
      0.9581    0.7161
      0.0090    0.9202
      0.8065    0.4049
   


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
      0.1504    0.7549    0.7841    0.0372
   
   R2 = 
      0.2575    0.1463    0.3654    0.0361
   
   M = 
      0.1504    0.7549    0.7841    0.0372
      0.2575    0.1463    0.3654    0.0361
   
   C1 = 
      0.2380
      0.5014
      0.5879
      0.0026
      0.8889
      0.1156
      0.9505
      0.5277
      0.1015
      0.3314
   
   C2 = 
      0.5808
      0.0889
   
   C3 = 
      0.2380
      0.5014
      0.5879
      0.0026
      0.8889
      0.1156
      0.9505
      0.5277
      0.1015
      0.3314
      0.5808
      0.0889
   

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
   

