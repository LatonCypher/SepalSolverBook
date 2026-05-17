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
      0.7273    0.4853    0.8185    0.7895    0.0782    0.0849    0.5102
   
   C = 
      0.9349
      0.0626
      0.8328
      0.4313
      0.4614
   
   M = 
      0.0863    0.2405    0.7462    0.1759    0.4291    0.6928    0.7853
      0.8807    0.7079    0.4083    0.3576    0.2034    0.0575    0.1115
      0.4597    0.9754    0.1766    0.5837    0.8608    0.1857    0.1576
      0.9627    0.7568    0.1545    0.3470    0.3852    0.0016    0.1564
      0.2408    0.3175    0.9905    0.5236    0.9792    0.8373    0.5802
      0.4562    0.6594    0.5567    0.6149    0.1045    0.1634    0.7762
      0.1314    0.5527    0.8597    0.9161    0.7640    0.6710    0.6980
      0.8951    0.4827    0.1786    0.8836    0.1541    0.0219    0.6495
   

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
      0.5721    0.4783    0.7433    0.9283
   
   R2 = 
      0.8645    0.1208    0.0835    0.9990    0.9751
   
   R3 = 
      0.5721    0.4783    0.7433    0.9283    0.8645    0.1208    0.0835    0.9990    0.9751
   
   C1 = 
      0.8799
      0.9076
      0.5366
      0.7321
      0.9635
      0.7134
      0.8926
      0.1438
      0.8760
      0.5701
   
   C2 = 
      0.2406
      0.1689
      0.9546
      0.6775
      0.8916
      0.5519
      0.9911
      0.6815
      0.5196
      0.0404
   
   M = 
      0.8799    0.2406
      0.9076    0.1689
      0.5366    0.9546
      0.7321    0.6775
      0.9635    0.8916
      0.7134    0.5519
      0.8926    0.9911
      0.1438    0.6815
      0.8760    0.5196
      0.5701    0.0404
   


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
      0.9747    0.6226    0.5949    0.8961
   
   R2 = 
      0.0094    0.3035    0.2942    0.6121
   
   M = 
      0.9747    0.6226    0.5949    0.8961
      0.0094    0.3035    0.2942    0.6121
   
   C1 = 
      0.4092
      0.5913
      0.7727
      0.6893
      0.6008
      0.9028
      0.1487
      0.6453
      0.8803
      0.3371
   
   C2 = 
      0.4735
      0.7288
   
   C3 = 
      0.4092
      0.5913
      0.7727
      0.6893
      0.6008
      0.9028
      0.1487
      0.6453
      0.8803
      0.3371
      0.4735
      0.7288
   

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
   

