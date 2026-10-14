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
      0.9257    0.1295    0.7888    0.7438    0.7057    0.7305    0.0552
   
   C = 
      0.8940
      0.8770
      0.5751
      0.8834
      0.6460
   
   M = 
      0.7255    0.7097    0.2422    0.8146    0.8779    0.1925    0.3363
      0.0558    0.5903    0.8301    0.1810    0.9329    0.7845    0.8765
      0.5865    0.2152    0.7039    0.0525    0.6390    0.5546    0.8940
      0.9610    0.4050    0.7965    0.0077    0.1417    0.2465    0.9039
      0.3099    0.1010    0.2435    0.2084    0.7730    0.0096    0.7913
      0.0307    0.8835    0.8527    0.4025    0.4957    0.9500    0.3559
      0.9024    0.2395    0.8279    0.5521    0.6873    0.4191    0.2308
      0.4754    0.2591    0.7333    0.7875    0.0969    0.4731    0.4256
   

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
      0.4021    0.9856    0.0594    0.9818
   
   R2 = 
      0.9782    0.3672    0.9267    0.8404    0.1010
   
   R3 = 
      0.4021    0.9856    0.0594    0.9818    0.9782    0.3672    0.9267    0.8404    0.1010
   
   C1 = 
      0.6314
      0.9396
      0.1385
      0.0878
      0.3400
      0.9136
      0.1112
      0.3373
      0.5808
      0.4713
   
   C2 = 
      0.8307
      0.7383
      0.4332
      0.9262
      0.5842
      0.0005
      0.7256
      0.9793
      0.6673
      0.0701
   
   M = 
      0.6314    0.8307
      0.9396    0.7383
      0.1385    0.4332
      0.0878    0.9262
      0.3400    0.5842
      0.9136    0.0005
      0.1112    0.7256
      0.3373    0.9793
      0.5808    0.6673
      0.4713    0.0701
   


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
      0.9460    0.8310    0.8432    0.4510
   
   R2 = 
      0.8828    0.9255    0.4354    0.9353
   
   M = 
      0.9460    0.8310    0.8432    0.4510
      0.8828    0.9255    0.4354    0.9353
   
   C1 = 
      0.5003
      0.3892
      0.2553
      0.5571
      0.5623
      0.9584
      0.9027
      0.0518
      0.0025
      0.0996
   
   C2 = 
      0.7666
      0.5774
   
   C3 = 
      0.5003
      0.3892
      0.2553
      0.5571
      0.5623
      0.9584
      0.9027
      0.0518
      0.0025
      0.0996
      0.7666
      0.5774
   

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
   

