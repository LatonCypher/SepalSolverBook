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
      0.7798    0.8423    0.1384    0.3387    0.8974    0.0976    0.6254
   
   C = 
      0.1491
      0.4486
      0.0581
      0.4479
      0.4792
   
   M = 
      0.9076    0.7322    0.8755    0.4511    0.8669    0.7168    0.8639
      0.0146    0.5438    0.1049    0.3933    0.4908    0.3240    0.6979
      0.0654    0.3930    0.3758    0.6509    0.2419    0.3269    0.7104
      0.4055    0.0347    0.1324    0.1303    0.1987    0.5601    0.8886
      0.8777    0.8860    0.5002    0.3734    0.7273    0.0703    0.6355
      0.7357    0.5876    0.7748    0.5430    0.0152    0.8746    0.2450
      0.5417    0.0333    0.0137    0.2647    0.6282    0.4349    0.2563
      0.3949    0.9896    0.4821    0.1121    0.7752    0.6003    0.8290
   

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
      0.5432    0.5004    0.6319    0.5031
   
   R2 = 
      0.4126    0.7002    0.5272    0.8511    0.6689
   
   R3 = 
      0.5432    0.5004    0.6319    0.5031    0.4126    0.7002    0.5272    0.8511    0.6689
   
   C1 = 
      0.8180
      0.8376
      0.2015
      0.6566
      0.7710
      0.4335
      0.1814
      0.8959
      0.1396
      0.7266
   
   C2 = 
      0.8410
      0.0081
      0.9374
      0.0081
      0.7869
      0.1188
      0.9310
      0.5543
      0.2621
      0.3421
   
   M = 
      0.8180    0.8410
      0.8376    0.0081
      0.2015    0.9374
      0.6566    0.0081
      0.7710    0.7869
      0.4335    0.1188
      0.1814    0.9310
      0.8959    0.5543
      0.1396    0.2621
      0.7266    0.3421
   


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
      0.1504    0.2228    0.2753    0.6879
   
   R2 = 
      0.5129    0.8353    0.5690    0.6509
   
   M = 
      0.1504    0.2228    0.2753    0.6879
      0.5129    0.8353    0.5690    0.6509
   
   C1 = 
      0.9416
      0.0395
      0.0059
      0.7373
      0.8290
      0.1117
      0.1789
      0.8938
      0.9105
      0.9534
   
   C2 = 
      0.0354
      0.7400
   
   C3 = 
      0.9416
      0.0395
      0.0059
      0.7373
      0.8290
      0.1117
      0.1789
      0.8938
      0.9105
      0.9534
      0.0354
      0.7400
   

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
   

