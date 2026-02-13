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
      0.5350    0.2844    0.9905    0.1845    0.8658    0.1419    0.6502
   
   C = 
      0.3983
      0.8473
      0.8209
      0.3972
      0.8729
   
   M = 
      0.3242    0.5089    0.7766    0.7167    0.4224    0.6300    0.8712
      0.7400    0.8819    0.9759    0.6601    0.1046    0.4408    0.1075
      0.6584    0.8338    0.9042    0.7484    0.8877    0.8967    0.8528
      0.6846    0.8199    0.9765    0.0593    0.9219    0.8117    0.2838
      0.4542    0.6631    0.2977    0.2320    0.7390    0.5064    0.3469
      0.2734    0.7950    0.7817    0.4041    0.1727    0.2210    0.0303
      0.8514    0.0032    0.7838    0.2801    0.9579    0.5404    0.6001
      0.4307    0.7852    0.5865    0.1128    0.9337    0.7526    0.5755
   

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
      0.8689    0.9457    0.3999    0.6061
   
   R2 = 
      0.3246    0.2307    0.4912    0.7475    0.1547
   
   R3 = 
      0.8689    0.9457    0.3999    0.6061    0.3246    0.2307    0.4912    0.7475    0.1547
   
   C1 = 
      0.9243
      0.8639
      0.3950
      0.5973
      0.8171
      0.3470
      0.0786
      0.4965
      0.8305
      0.3214
   
   C2 = 
      0.0624
      0.1645
      0.8043
      0.6403
      0.5987
      0.9867
      0.4048
      0.5628
      0.2704
      0.3355
   
   M = 
      0.9243    0.0624
      0.8639    0.1645
      0.3950    0.8043
      0.5973    0.6403
      0.8171    0.5987
      0.3470    0.9867
      0.0786    0.4048
      0.4965    0.5628
      0.8305    0.2704
      0.3214    0.3355
   


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
      0.8711    0.6492    0.2585    0.1409
   
   R2 = 
      0.9373    0.5693    0.6776    0.3304
   
   M = 
      0.8711    0.6492    0.2585    0.1409
      0.9373    0.5693    0.6776    0.3304
   
   C1 = 
      0.3760
      0.3946
      0.6652
      0.2950
      0.1869
      0.1105
      0.0240
      0.6870
      0.1184
      0.2040
   
   C2 = 
      0.7399
      0.7465
   
   C3 = 
      0.3760
      0.3946
      0.6652
      0.2950
      0.1869
      0.1105
      0.0240
      0.6870
      0.1184
      0.2040
      0.7399
      0.7465
   

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
   

