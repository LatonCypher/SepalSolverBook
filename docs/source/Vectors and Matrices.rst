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
      0.1099    0.9624    0.0676    0.8878    0.6551    0.3072    0.9157
   
   C = 
      0.4785
      0.1322
      0.8369
      0.2616
      0.0288
   
   M = 
      0.4185    0.7042    0.9735    0.8798    0.3658    0.6955    0.1678
      0.0571    0.8272    0.8390    0.6739    0.5809    0.7833    0.9410
      0.2171    0.9825    0.1159    0.1874    0.6515    0.5081    0.9149
      0.0954    0.3553    0.1090    0.0776    0.0441    0.0790    0.9479
      0.8627    0.3043    0.2898    0.5654    0.0327    0.1244    0.5651
      0.2747    0.1118    0.1594    0.1867    0.3035    0.3333    0.6820
      0.7187    0.7666    0.4814    0.2095    0.6926    0.9012    0.0688
      0.4332    0.2337    0.9420    0.2815    0.6344    0.3534    0.0747
   

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
      0.5446    0.7478    0.3843    0.4696
   
   R2 = 
      0.7973    0.2751    0.8085    0.9575    0.3813
   
   R3 = 
      0.5446    0.7478    0.3843    0.4696    0.7973    0.2751    0.8085    0.9575    0.3813
   
   C1 = 
      0.4683
      0.9155
      0.2661
      0.6724
      0.3398
      0.4986
      0.1932
      0.4781
      0.7097
      0.8124
   
   C2 = 
      0.2375
      0.0172
      0.6831
      0.9219
      0.6403
      0.1016
      0.6168
      0.5435
      0.2841
      0.5077
   
   M = 
      0.4683    0.2375
      0.9155    0.0172
      0.2661    0.6831
      0.6724    0.9219
      0.3398    0.6403
      0.4986    0.1016
      0.1932    0.6168
      0.4781    0.5435
      0.7097    0.2841
      0.8124    0.5077
   


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
      0.1204    0.7328    0.0774    0.8132
   
   R2 = 
      0.0184    0.4486    0.6834    0.5434
   
   M = 
      0.1204    0.7328    0.0774    0.8132
      0.0184    0.4486    0.6834    0.5434
   
   C1 = 
      0.9191
      0.7943
      0.6286
      0.5324
      0.3654
      0.7174
      0.6828
      0.6710
      0.0302
      0.5067
   
   C2 = 
      0.1731
      0.5735
   
   C3 = 
      0.9191
      0.7943
      0.6286
      0.5324
      0.3654
      0.7174
      0.6828
      0.6710
      0.0302
      0.5067
      0.1731
      0.5735
   

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
   

