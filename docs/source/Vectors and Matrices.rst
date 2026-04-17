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
      0.1838    0.2463    0.9298    0.2847    0.7853    0.2487    0.6619
   
   C = 
      0.5887
      0.7034
      0.1859
      0.0812
      0.0684
   
   M = 
      0.3907    0.0858    0.3805    0.1917    0.0753    0.7724    0.9121
      0.0678    0.1354    0.5759    0.6814    0.1986    0.9363    0.3311
      0.0463    0.7919    0.1827    0.5925    0.8561    0.6375    0.2728
      0.9209    0.6830    0.4131    0.3835    0.0600    0.6154    0.0628
      0.2223    0.4961    0.5266    0.8901    0.2269    0.2584    0.8671
      0.4947    0.5747    0.8390    0.5046    0.0042    0.6684    0.8492
      0.1814    0.7493    0.2329    0.7782    0.4051    0.1768    0.3235
      0.8735    0.8859    0.8575    0.3670    0.8065    0.6826    0.8313
   

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
      0.4705    0.8277    0.4198    0.8798
   
   R2 = 
      0.2512    0.1096    0.7497    0.6909    0.5534
   
   R3 = 
      0.4705    0.8277    0.4198    0.8798    0.2512    0.1096    0.7497    0.6909    0.5534
   
   C1 = 
      0.8596
      0.9286
      0.4115
      0.3411
      0.8366
      0.0885
      0.0590
      0.1428
      0.5237
      0.8138
   
   C2 = 
      0.9836
      0.4254
      0.2017
      0.7397
      0.8537
      0.4784
      0.9812
      0.4745
      0.1830
      0.7864
   
   M = 
      0.8596    0.9836
      0.9286    0.4254
      0.4115    0.2017
      0.3411    0.7397
      0.8366    0.8537
      0.0885    0.4784
      0.0590    0.9812
      0.1428    0.4745
      0.5237    0.1830
      0.8138    0.7864
   


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
      0.7446    0.8715    0.3253    0.8700
   
   R2 = 
      0.3032    0.6418    0.3922    0.8491
   
   M = 
      0.7446    0.8715    0.3253    0.8700
      0.3032    0.6418    0.3922    0.8491
   
   C1 = 
      0.3458
      0.2684
      0.4014
      0.0359
      0.3073
      0.2915
      0.1580
      0.8608
      0.0541
      0.9658
   
   C2 = 
      0.2133
      0.7234
   
   C3 = 
      0.3458
      0.2684
      0.4014
      0.0359
      0.3073
      0.2915
      0.1580
      0.8608
      0.0541
      0.9658
      0.2133
      0.7234
   

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
   

