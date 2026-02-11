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
      0.9036    0.3725    0.4411    0.2914    0.1734    0.3600    0.3082
   
   C = 
      0.3417
      0.3213
      0.4854
      0.2805
      0.8520
   
   M = 
      0.5320    0.3527    0.9832    0.0488    0.1621    0.3011    0.0087
      0.8301    0.3397    0.8431    0.9508    0.3234    0.2320    0.1458
      0.4761    0.1835    0.1798    0.2121    0.7103    0.1573    0.1132
      0.8864    0.5198    0.7007    0.1877    0.6079    0.7264    0.2316
      0.9877    0.5289    0.6252    0.7399    0.6517    0.9738    0.3191
      0.0668    0.8148    0.7396    0.7875    0.5243    0.6078    0.2326
      0.6170    0.1472    0.4056    0.1222    0.6494    0.4175    0.2783
      0.3588    0.7091    0.4407    0.9884    0.3368    0.6822    0.5620
   

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
      0.6149    0.2462    0.9115    0.7204
   
   R2 = 
      0.4573    0.8049    0.8272    0.1087    0.8951
   
   R3 = 
      0.6149    0.2462    0.9115    0.7204    0.4573    0.8049    0.8272    0.1087    0.8951
   
   C1 = 
      0.7307
      0.6440
      0.9779
      0.7942
      0.0164
      0.7931
      0.2146
      0.7945
      0.3048
      0.3958
   
   C2 = 
      0.8489
      0.1164
      0.8485
      0.3850
      0.3821
      0.6027
      0.8433
      0.4859
      0.3792
      0.5196
   
   M = 
      0.7307    0.8489
      0.6440    0.1164
      0.9779    0.8485
      0.7942    0.3850
      0.0164    0.3821
      0.7931    0.6027
      0.2146    0.8433
      0.7945    0.4859
      0.3048    0.3792
      0.3958    0.5196
   


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
      0.4375    0.7712    0.3531    0.6774
   
   R2 = 
      0.4788    0.9142    0.4761    0.7672
   
   M = 
      0.4375    0.7712    0.3531    0.6774
      0.4788    0.9142    0.4761    0.7672
   
   C1 = 
      0.7103
      0.0413
      0.6353
      0.7960
      0.3167
      0.6832
      0.3713
      0.5492
      0.6988
      0.2734
   
   C2 = 
      0.0130
      0.5754
   
   C3 = 
      0.7103
      0.0413
      0.6353
      0.7960
      0.3167
      0.6832
      0.3713
      0.5492
      0.6988
      0.2734
      0.0130
      0.5754
   

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
   

