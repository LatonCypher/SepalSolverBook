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
      0.2810    0.3048    0.2725    0.6887    0.0087    0.9022    0.9196
   
   C = 
      0.5218
      0.9946
      0.2931
      0.8333
      0.5727
   
   M = 
      0.2524    0.5817    0.0312    0.7890    0.8203    0.9476    0.2489
      0.5012    0.2343    0.9747    0.2924    0.9644    0.4745    0.3739
      0.5115    0.7885    0.4009    0.9914    0.2496    0.2342    0.5448
      0.7858    0.9309    0.4481    0.4712    0.0345    0.0427    0.1087
      0.8941    0.6182    0.4827    0.0866    0.5203    0.3374    0.8446
      0.4761    0.3163    0.8852    0.2587    0.5038    0.5359    0.0178
      0.2131    0.4733    0.7158    0.5807    0.6399    0.0621    0.4892
      0.6515    0.8285    0.2014    0.3581    0.8182    0.9187    0.5040
   

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
      0.4266    0.0056    0.9352    0.1294
   
   R2 = 
      0.3129    0.2079    0.5775    0.9385    0.4801
   
   R3 = 
      0.4266    0.0056    0.9352    0.1294    0.3129    0.2079    0.5775    0.9385    0.4801
   
   C1 = 
      0.1911
      0.8093
      0.9894
      0.5036
      0.6570
      0.6302
      0.6063
      0.6192
      0.2861
      0.2630
   
   C2 = 
      0.4048
      0.1136
      0.8249
      0.1160
      0.3637
      0.9046
      0.1694
      0.5923
      0.6280
      0.3686
   
   M = 
      0.1911    0.4048
      0.8093    0.1136
      0.9894    0.8249
      0.5036    0.1160
      0.6570    0.3637
      0.6302    0.9046
      0.6063    0.1694
      0.6192    0.5923
      0.2861    0.6280
      0.2630    0.3686
   


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
      0.7328    0.1248    0.7309    0.7035
   
   R2 = 
      0.2035    0.9553    0.3314    0.7194
   
   M = 
      0.7328    0.1248    0.7309    0.7035
      0.2035    0.9553    0.3314    0.7194
   
   C1 = 
      0.7035
      0.0028
      0.7379
      0.6601
      0.5455
      0.3109
      0.3033
      0.1685
      0.9339
      0.6042
   
   C2 = 
      0.1807
      0.9801
   
   C3 = 
      0.7035
      0.0028
      0.7379
      0.6601
      0.5455
      0.3109
      0.3033
      0.1685
      0.9339
      0.6042
      0.1807
      0.9801
   

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
   

