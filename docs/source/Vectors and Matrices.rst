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
      0.6838    0.5717    0.6752    0.0803    0.1936    0.0291    0.2670
   
   C = 
      0.8791
      0.2761
      0.0180
      0.3910
      0.4787
   
   M = 
      0.0789    0.0585    0.4604    0.2808    0.5773    0.7996    0.8594
      0.0105    0.6075    0.4305    0.3315    0.9527    0.0053    0.7376
      0.3392    0.0835    0.0689    0.0877    0.5114    0.0511    0.9222
      0.0282    0.6029    0.5458    0.5639    0.7110    0.0762    0.2257
      0.9220    0.4511    0.6302    0.3324    0.9953    0.1719    0.9772
      0.2270    0.5201    0.9872    0.2465    0.7338    0.1829    0.0103
      0.0286    0.1331    0.0717    0.4569    0.1586    0.4877    0.9549
      0.1802    0.0892    0.4915    0.5188    0.7994    0.7657    0.3228
   

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
      0.9935    0.0186    0.9186    0.4543
   
   R2 = 
      0.5315    0.2948    0.4410    0.0375    0.8145
   
   R3 = 
      0.9935    0.0186    0.9186    0.4543    0.5315    0.2948    0.4410    0.0375    0.8145
   
   C1 = 
      0.5148
      0.6667
      0.3790
      0.4669
      0.2509
      0.1334
      0.0391
      0.3808
      0.4811
      0.7394
   
   C2 = 
      0.4378
      0.9926
      0.1173
      0.1527
      0.4919
      0.9315
      0.2693
      0.9361
      0.8683
      0.5725
   
   M = 
      0.5148    0.4378
      0.6667    0.9926
      0.3790    0.1173
      0.4669    0.1527
      0.2509    0.4919
      0.1334    0.9315
      0.0391    0.2693
      0.3808    0.9361
      0.4811    0.8683
      0.7394    0.5725
   


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
      0.7372    0.0050    0.6836    0.5712
   
   R2 = 
      0.2472    0.6828    0.4481    0.1623
   
   M = 
      0.7372    0.0050    0.6836    0.5712
      0.2472    0.6828    0.4481    0.1623
   
   C1 = 
      0.0181
      0.4486
      0.1315
      0.5556
      0.9196
      0.0579
      0.4055
      0.2721
      0.5875
      0.0266
   
   C2 = 
      0.4217
      0.9020
   
   C3 = 
      0.0181
      0.4486
      0.1315
      0.5556
      0.9196
      0.0579
      0.4055
      0.2721
      0.5875
      0.0266
      0.4217
      0.9020
   

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
   

