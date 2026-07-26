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
      0.4590    0.0672    0.5089    0.1610    0.2372    0.7853    0.5443
   
   C = 
      0.0428
      0.9254
      0.7549
      0.8949
      0.4404
   
   M = 
      0.2180    0.2423    0.1644    0.6254    0.3791    0.5054    0.3507
      0.0112    0.5156    0.0442    0.8006    0.9235    0.2333    0.2434
      0.8187    0.1712    0.0185    0.2354    0.8102    0.0300    0.8190
      0.7095    0.5125    0.7748    0.3433    0.3250    0.6951    0.9108
      0.5502    0.4955    0.0531    0.3666    0.4930    0.8492    0.6974
      0.7690    0.0306    0.4648    0.0190    0.7329    0.7184    0.8032
      0.7972    0.1937    0.8048    0.7685    0.7396    0.2109    0.9390
      0.5022    0.8480    0.8679    0.2994    0.5403    0.8743    0.3614
   

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
      0.4263    0.8243    0.1788    0.4522
   
   R2 = 
      0.5082    0.6058    0.1923    0.4179    0.9220
   
   R3 = 
      0.4263    0.8243    0.1788    0.4522    0.5082    0.6058    0.1923    0.4179    0.9220
   
   C1 = 
      0.2278
      0.2376
      0.7592
      0.6722
      0.9130
      0.7355
      0.6312
      0.4216
      0.8833
      0.1965
   
   C2 = 
      0.8698
      0.1723
      0.4626
      0.4749
      0.3571
      0.7444
      0.9752
      0.0328
      0.2421
      0.2810
   
   M = 
      0.2278    0.8698
      0.2376    0.1723
      0.7592    0.4626
      0.6722    0.4749
      0.9130    0.3571
      0.7355    0.7444
      0.6312    0.9752
      0.4216    0.0328
      0.8833    0.2421
      0.1965    0.2810
   


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
      0.1055    0.4742    0.8750    0.7150
   
   R2 = 
      0.5922    0.3431    0.8448    0.4278
   
   M = 
      0.1055    0.4742    0.8750    0.7150
      0.5922    0.3431    0.8448    0.4278
   
   C1 = 
      0.0763
      0.5044
      0.4799
      0.1407
      0.0108
      0.2219
      0.5907
      0.7500
      0.9053
      0.0495
   
   C2 = 
      0.0392
      0.5916
   
   C3 = 
      0.0763
      0.5044
      0.4799
      0.1407
      0.0108
      0.2219
      0.5907
      0.7500
      0.9053
      0.0495
      0.0392
      0.5916
   

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
   

