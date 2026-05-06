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
      0.8109    0.7440    0.0109    0.4011    0.0907    0.5822    0.9973
   
   C = 
      0.4827
      0.8048
      0.5486
      0.0176
      0.3608
   
   M = 
      0.0667    0.0665    0.7647    0.0653    0.0089    0.5021    0.1812
      0.9027    0.4285    0.2219    0.6432    0.8477    0.0767    0.0056
      0.9134    0.0774    0.4791    0.0067    0.6962    0.9905    0.5762
      0.0174    0.6370    0.0861    0.3824    0.5415    0.8587    0.6883
      0.9294    0.7634    0.2483    0.0995    0.6658    0.1245    0.3170
      0.8784    0.5856    0.5504    0.7332    0.7925    0.7042    0.3812
      0.4274    0.7506    0.3470    0.7358    0.0923    0.9504    0.9724
      0.5998    0.9773    0.8317    0.1921    0.5491    0.1944    0.9261
   

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
      0.3226    0.5619    0.0253    0.7736
   
   R2 = 
      0.5756    0.5266    0.3169    0.6605    0.7079
   
   R3 = 
      0.3226    0.5619    0.0253    0.7736    0.5756    0.5266    0.3169    0.6605    0.7079
   
   C1 = 
      0.9877
      0.1410
      0.0980
      0.0389
      0.1817
      0.5351
      0.5766
      0.8481
      0.6010
      0.6865
   
   C2 = 
      0.5837
      0.4912
      0.7342
      0.5042
      0.2369
      0.5376
      0.2385
      0.3805
      0.9409
      0.5359
   
   M = 
      0.9877    0.5837
      0.1410    0.4912
      0.0980    0.7342
      0.0389    0.5042
      0.1817    0.2369
      0.5351    0.5376
      0.5766    0.2385
      0.8481    0.3805
      0.6010    0.9409
      0.6865    0.5359
   


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
      0.6198    0.0777    0.5235    0.3652
   
   R2 = 
      0.1517    0.5396    0.0645    0.9950
   
   M = 
      0.6198    0.0777    0.5235    0.3652
      0.1517    0.5396    0.0645    0.9950
   
   C1 = 
      0.9653
      0.2888
      0.7260
      0.2100
      0.2580
      0.4406
      0.5038
      0.3778
      0.5589
      0.4546
   
   C2 = 
      0.0032
      0.1648
   
   C3 = 
      0.9653
      0.2888
      0.7260
      0.2100
      0.2580
      0.4406
      0.5038
      0.3778
      0.5589
      0.4546
      0.0032
      0.1648
   

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
   

