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
      0.7957    0.8643    0.0526    0.3508    0.0116    0.9647    0.1717
   
   C = 
      0.0944
      0.9610
      0.5987
      0.8939
      0.6801
   
   M = 
      0.1586    0.5768    0.3891    0.1117    0.6128    0.2248    0.4573
      0.4109    0.3386    0.3910    0.5891    0.2570    0.3772    0.8468
      0.6898    0.9009    0.2112    0.7346    0.1777    0.4055    0.2476
      0.7642    0.3863    0.7337    0.2844    0.7359    0.3274    0.0662
      0.0546    0.7392    0.9813    0.2544    0.0306    0.5706    0.7474
      0.0158    0.4578    0.5029    0.7185    0.8764    0.7590    0.8368
      0.0308    0.0372    0.6914    0.3489    0.5388    0.8974    0.3982
      0.7646    0.2264    0.5635    0.6640    0.8810    0.7768    0.2852
   

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
      0.5302    0.2472    0.2605    0.6605
   
   R2 = 
      0.4682    0.7846    0.2007    0.0393    0.1959
   
   R3 = 
      0.5302    0.2472    0.2605    0.6605    0.4682    0.7846    0.2007    0.0393    0.1959
   
   C1 = 
      0.1774
      0.5081
      0.2406
      0.1683
      0.1959
      0.2282
      0.1476
      0.6123
      0.4441
      0.6655
   
   C2 = 
      0.8836
      0.0429
      0.0702
      0.2488
      0.4577
      0.6323
      0.6191
      0.1043
      0.7950
      0.2444
   
   M = 
      0.1774    0.8836
      0.5081    0.0429
      0.2406    0.0702
      0.1683    0.2488
      0.1959    0.4577
      0.2282    0.6323
      0.1476    0.6191
      0.6123    0.1043
      0.4441    0.7950
      0.6655    0.2444
   


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
      0.0956    0.0597    0.8029    0.9129
   
   R2 = 
      0.4756    0.8214    0.1616    0.6857
   
   M = 
      0.0956    0.0597    0.8029    0.9129
      0.4756    0.8214    0.1616    0.6857
   
   C1 = 
      0.1989
      0.8282
      0.1855
      0.7672
      0.2135
      0.9038
      0.0605
      0.7149
      0.9889
      0.3711
   
   C2 = 
      0.2774
      0.5231
   
   C3 = 
      0.1989
      0.8282
      0.1855
      0.7672
      0.2135
      0.9038
      0.0605
      0.7149
      0.9889
      0.3711
      0.2774
      0.5231
   

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
   

