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
      0.3852    0.8803    0.4328    0.9662    0.8116    0.2937    0.9797
   
   C = 
      0.4977
      0.1394
      0.7823
      0.2351
      0.7158
   
   M = 
      0.8346    0.3734    0.0540    0.1697    0.1102    0.7772    0.5995
      0.2018    0.9674    0.7099    0.9708    0.2862    0.0826    0.6278
      0.7410    0.9770    0.4147    0.2137    0.9220    0.2382    0.6712
      0.1669    0.1768    0.5729    0.2899    0.3651    0.1527    0.8938
      0.7655    0.6219    0.0174    0.5737    0.3782    0.7533    0.1164
      0.2464    0.3684    0.5140    0.4481    0.8228    0.1732    0.4296
      0.4643    0.3286    0.7032    0.9911    0.7884    0.7602    0.9324
      0.2570    0.9711    0.3693    0.7907    0.7516    0.3923    0.5646
   

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
      0.0616    0.2846    0.3404    0.5250
   
   R2 = 
      0.9093    0.0595    0.9840    0.5170    0.3347
   
   R3 = 
      0.0616    0.2846    0.3404    0.5250    0.9093    0.0595    0.9840    0.5170    0.3347
   
   C1 = 
      0.8672
      0.2566
      0.0768
      0.6820
      0.6647
      0.9537
      0.6687
      0.2149
      0.1315
      0.6373
   
   C2 = 
      0.8468
      0.8265
      0.4727
      0.2492
      0.8753
      0.2796
      0.5733
      0.7001
      0.9398
      0.6649
   
   M = 
      0.8672    0.8468
      0.2566    0.8265
      0.0768    0.4727
      0.6820    0.2492
      0.6647    0.8753
      0.9537    0.2796
      0.6687    0.5733
      0.2149    0.7001
      0.1315    0.9398
      0.6373    0.6649
   


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
      0.6198    0.6230    0.3809    0.4644
   
   R2 = 
      0.1131    0.9996    0.5907    0.5233
   
   M = 
      0.6198    0.6230    0.3809    0.4644
      0.1131    0.9996    0.5907    0.5233
   
   C1 = 
      0.5438
      0.6885
      0.3250
      0.6552
      0.3215
      0.7099
      0.0692
      0.8731
      0.3007
      0.1749
   
   C2 = 
      0.5660
      0.0831
   
   C3 = 
      0.5438
      0.6885
      0.3250
      0.6552
      0.3215
      0.7099
      0.0692
      0.8731
      0.3007
      0.1749
      0.5660
      0.0831
   

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
   

