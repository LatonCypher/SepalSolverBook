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
      0.0927    0.9801    0.0966    0.9001    0.8399    0.4099    0.7775
   
   C = 
      0.2167
      0.4324
      0.1596
      0.7176
      0.5795
   
   M = 
      0.2974    0.9149    0.9680    0.3701    0.6746    0.0818    0.6096
      0.0544    0.8517    0.0130    0.5343    0.6683    0.7259    0.8753
      0.0993    0.7593    0.5061    0.3625    0.6454    0.1761    0.7327
      0.4386    0.2901    0.1388    0.0081    0.4734    0.3647    0.2011
      0.4000    0.5829    0.2119    0.8716    0.5773    0.1974    0.1302
      0.6525    0.1474    0.4721    0.8950    0.4755    0.5353    0.7609
      0.0090    0.5016    0.4795    0.1782    0.7831    0.3783    0.0318
      0.9220    0.6772    0.4238    0.8548    0.0925    0.1894    0.1763
   

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
      0.0698    0.4155    0.3943    0.6520
   
   R2 = 
      0.9139    0.0773    0.0782    0.1607    0.8722
   
   R3 = 
      0.0698    0.4155    0.3943    0.6520    0.9139    0.0773    0.0782    0.1607    0.8722
   
   C1 = 
      0.9920
      0.6571
      0.7015
      0.9380
      0.4086
      0.1149
      0.3539
      0.1603
      0.2217
      0.3955
   
   C2 = 
      0.5555
      0.4734
      0.2760
      0.9760
      0.5769
      0.6253
      0.2178
      0.2153
      0.5877
      0.3127
   
   M = 
      0.9920    0.5555
      0.6571    0.4734
      0.7015    0.2760
      0.9380    0.9760
      0.4086    0.5769
      0.1149    0.6253
      0.3539    0.2178
      0.1603    0.2153
      0.2217    0.5877
      0.3955    0.3127
   


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
      0.0600    0.7913    0.6180    0.2939
   
   R2 = 
      0.1241    0.8924    0.1248    0.4616
   
   M = 
      0.0600    0.7913    0.6180    0.2939
      0.1241    0.8924    0.1248    0.4616
   
   C1 = 
      0.7571
      0.0527
      0.6948
      0.0012
      0.4989
      0.3303
      0.2640
      0.3592
      0.7688
      0.7074
   
   C2 = 
      0.6497
      0.0471
   
   C3 = 
      0.7571
      0.0527
      0.6948
      0.0012
      0.4989
      0.3303
      0.2640
      0.3592
      0.7688
      0.7074
      0.6497
      0.0471
   

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
   

