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
      0.4864    0.9314    0.6005    0.3789    0.1221    0.4262    0.0542
   
   C = 
      0.6727
      0.6696
      0.2823
      0.6832
      0.5291
   
   M = 
      0.0714    0.9022    0.4259    0.5020    0.9882    0.8119    0.4174
      0.7568    0.5786    0.2478    0.4540    0.2618    0.0226    0.0487
      0.2496    0.4833    0.1900    0.1700    0.3418    0.0663    0.7970
      0.2407    0.5256    0.8268    0.4734    0.2690    0.5202    0.4192
      0.8800    0.3291    0.3452    0.1270    0.0797    0.4191    0.9159
      0.7704    0.5586    0.8097    0.0708    0.1356    0.7230    0.5309
      0.0004    0.4404    0.4707    0.8651    0.6455    0.0451    0.7394
      0.9847    0.5026    0.5585    0.4395    0.3602    0.6965    0.6536
   

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
      0.0842    0.0200    0.6570    0.4537
   
   R2 = 
      0.8101    0.5025    0.4618    0.9185    0.2181
   
   R3 = 
      0.0842    0.0200    0.6570    0.4537    0.8101    0.5025    0.4618    0.9185    0.2181
   
   C1 = 
      0.9888
      0.4072
      0.5530
      0.2144
      0.6135
      0.8697
      0.4547
      0.7215
      0.4089
      0.3334
   
   C2 = 
      0.2768
      0.4178
      0.7481
      0.1292
      0.5406
      0.5518
      0.7618
      0.3752
      0.4339
      0.8361
   
   M = 
      0.9888    0.2768
      0.4072    0.4178
      0.5530    0.7481
      0.2144    0.1292
      0.6135    0.5406
      0.8697    0.5518
      0.4547    0.7618
      0.7215    0.3752
      0.4089    0.4339
      0.3334    0.8361
   


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
      0.4087    0.6643    0.9145    0.9745
   
   R2 = 
      0.5694    0.0880    0.1941    0.9599
   
   M = 
      0.4087    0.6643    0.9145    0.9745
      0.5694    0.0880    0.1941    0.9599
   
   C1 = 
      0.9114
      0.2901
      0.0826
      0.6918
      0.3792
      0.6730
      0.5381
      0.7824
      0.4766
      0.4101
   
   C2 = 
      0.3647
      0.8510
   
   C3 = 
      0.9114
      0.2901
      0.0826
      0.6918
      0.3792
      0.6730
      0.5381
      0.7824
      0.4766
      0.4101
      0.3647
      0.8510
   

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
   

