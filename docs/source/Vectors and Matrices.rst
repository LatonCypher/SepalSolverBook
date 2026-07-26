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
      0.4628    0.4545    0.4711    0.7803    0.9129    0.7475    0.3483
   
   C = 
      0.8293
      0.3257
      0.6667
      0.8319
      0.9717
   
   M = 
      0.7144    0.2954    0.4360    0.4210    0.0964    0.1768    0.4410
      0.4067    0.6583    0.3087    0.9058    0.1067    0.9803    0.9083
      0.1329    0.1104    0.7516    0.1797    0.1237    0.5574    0.5847
      0.4357    0.7926    0.1996    0.5715    0.4142    0.9537    0.4662
      0.6269    0.3816    0.5435    0.7994    0.8285    0.6470    0.8072
      0.4520    0.1369    0.4244    0.6012    0.3684    0.9203    0.7233
      0.9580    0.2026    0.4425    0.0690    0.0380    0.5767    0.2559
      0.3780    0.8221    0.9858    0.4782    0.3199    0.6810    0.6207
   

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
      0.5020    0.3387    0.3132    0.0526
   
   R2 = 
      0.9288    0.9187    0.8347    0.8053    0.1477
   
   R3 = 
      0.5020    0.3387    0.3132    0.0526    0.9288    0.9187    0.8347    0.8053    0.1477
   
   C1 = 
      0.3441
      0.5186
      0.9971
      0.8298
      0.6659
      0.4101
      0.4103
      0.8887
      0.1553
      0.9104
   
   C2 = 
      0.2978
      0.3495
      0.2589
      0.6873
      0.3282
      0.5825
      0.2514
      0.3888
      0.7461
      0.3228
   
   M = 
      0.3441    0.2978
      0.5186    0.3495
      0.9971    0.2589
      0.8298    0.6873
      0.6659    0.3282
      0.4101    0.5825
      0.4103    0.2514
      0.8887    0.3888
      0.1553    0.7461
      0.9104    0.3228
   


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
      0.2251    0.1909    0.0651    0.1086
   
   R2 = 
      0.5086    0.3682    0.5632    0.4835
   
   M = 
      0.2251    0.1909    0.0651    0.1086
      0.5086    0.3682    0.5632    0.4835
   
   C1 = 
      0.4400
      0.9977
      0.2540
      0.9063
      0.7640
      0.6282
      0.7919
      0.3688
      0.6928
      0.8257
   
   C2 = 
      0.4560
      0.4997
   
   C3 = 
      0.4400
      0.9977
      0.2540
      0.9063
      0.7640
      0.6282
      0.7919
      0.3688
      0.6928
      0.8257
      0.4560
      0.4997
   

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
   

