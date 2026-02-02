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
      0.5831    0.7516    0.0863    0.6199    0.5454    0.5289    0.8298
   
   C = 
      0.2833
      0.8958
      0.7682
      0.1568
      0.6678
   
   M = 
      0.4224    0.4033    0.6465    0.3457    0.9199    0.1813    0.3155
      0.0889    0.8592    0.6243    0.3717    0.9820    0.3696    0.1685
      0.9452    0.0517    0.3318    0.8382    0.2630    0.5104    0.5347
      0.6643    0.4466    0.7521    0.3361    0.0976    0.7875    0.4611
      0.3842    0.4608    0.9838    0.1214    0.2947    0.5514    0.8406
      0.1149    0.7796    0.2487    0.3725    0.1714    0.6651    0.7703
      0.4772    0.0434    0.3907    0.2093    0.1769    0.2897    0.6152
      0.7188    0.0106    0.9471    0.5495    0.2673    0.9547    0.7772
   

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
      0.4975    0.8418    0.5987    0.1952
   
   R2 = 
      0.6934    0.5326    0.3165    0.8261    0.9672
   
   R3 = 
      0.4975    0.8418    0.5987    0.1952    0.6934    0.5326    0.3165    0.8261    0.9672
   
   C1 = 
      0.5699
      0.3857
      0.3016
      0.8972
      0.9292
      0.5827
      0.9034
      0.3536
      0.9318
      0.8298
   
   C2 = 
      0.2924
      0.1146
      0.8138
      0.7194
      0.5255
      0.9274
      0.2899
      0.4981
      0.5130
      0.4557
   
   M = 
      0.5699    0.2924
      0.3857    0.1146
      0.3016    0.8138
      0.8972    0.7194
      0.9292    0.5255
      0.5827    0.9274
      0.9034    0.2899
      0.3536    0.4981
      0.9318    0.5130
      0.8298    0.4557
   


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
      0.1863    0.0236    0.2175    0.8304
   
   R2 = 
      0.4374    0.0631    0.3530    0.8952
   
   M = 
      0.1863    0.0236    0.2175    0.8304
      0.4374    0.0631    0.3530    0.8952
   
   C1 = 
      0.1897
      0.7478
      0.5035
      0.3222
      0.6163
      0.3780
      0.1902
      0.7467
      0.8304
      0.9959
   
   C2 = 
      0.0457
      0.0576
   
   C3 = 
      0.1897
      0.7478
      0.5035
      0.3222
      0.6163
      0.3780
      0.1902
      0.7467
      0.8304
      0.9959
      0.0457
      0.0576
   

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
   

