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
      0.0425    0.2431    0.5089    0.9606    0.4415    0.4683    0.1339
   
   C = 
      0.6197
      0.3025
      0.7813
      0.4184
      0.5600
   
   M = 
      0.3901    0.6960    0.7780    0.2420    0.9260    0.6661    0.3296
      0.1992    0.2358    0.1901    0.3068    0.2759    0.7424    0.0208
      0.0895    0.5515    0.0687    0.9756    0.3347    0.1121    0.6963
      0.6870    0.2115    0.6678    0.1391    0.0707    0.7081    0.4277
      0.8371    0.3774    0.8758    0.6522    0.4727    0.9023    0.0222
      0.8841    0.3528    0.1101    0.2298    0.4888    0.1455    0.3348
      0.2594    0.6481    0.2485    0.3611    0.5018    0.7502    0.4196
      0.6762    0.4883    0.6971    0.0706    0.5316    0.6912    0.4908
   

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
      0.0893    0.5547    0.6829    0.1755
   
   R2 = 
      0.1989    0.9708    0.1568    0.5888    0.3952
   
   R3 = 
      0.0893    0.5547    0.6829    0.1755    0.1989    0.9708    0.1568    0.5888    0.3952
   
   C1 = 
      0.2868
      0.3912
      0.5446
      0.8456
      0.6491
      0.3127
      0.6152
      0.8899
      0.6065
      0.7113
   
   C2 = 
      0.2874
      0.5966
      0.3198
      0.9362
      0.1638
      0.8255
      0.0142
      0.6445
      0.8967
      0.4716
   
   M = 
      0.2868    0.2874
      0.3912    0.5966
      0.5446    0.3198
      0.8456    0.9362
      0.6491    0.1638
      0.3127    0.8255
      0.6152    0.0142
      0.8899    0.6445
      0.6065    0.8967
      0.7113    0.4716
   


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
      0.6714    0.5768    0.7724    0.1657
   
   R2 = 
      0.6695    0.8216    0.0142    0.6997
   
   M = 
      0.6714    0.5768    0.7724    0.1657
      0.6695    0.8216    0.0142    0.6997
   
   C1 = 
      0.0656
      0.5296
      0.0919
      0.9150
      0.4286
      0.2733
      0.9962
      0.6302
      0.4246
      0.8890
   
   C2 = 
      0.6397
      0.9696
   
   C3 = 
      0.0656
      0.5296
      0.0919
      0.9150
      0.4286
      0.2733
      0.9962
      0.6302
      0.4246
      0.8890
      0.6397
      0.9696
   

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
   

