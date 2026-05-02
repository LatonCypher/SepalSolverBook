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
      0.0555    0.6546    0.8265    0.0518    0.6476    0.6973    0.7690
   
   C = 
      0.3743
      0.2707
      0.3495
      0.7414
      0.3355
   
   M = 
      0.4766    0.7423    0.7755    0.4884    0.5962    0.4875    0.5880
      0.8251    0.6631    0.5905    0.5213    0.9717    0.7742    0.7858
      0.5369    0.0180    0.7458    0.7355    0.0539    0.8943    0.4828
      0.5539    0.4497    0.5680    0.9690    0.8640    0.3742    0.8990
      0.2705    0.9146    0.6158    0.6166    0.9743    0.7007    0.9495
      0.4091    0.7737    0.7551    0.4733    0.5054    0.9788    0.2427
      0.5375    0.2775    0.8745    0.7334    0.9342    0.1482    0.2351
      0.7127    0.1659    0.3285    0.4804    0.3690    0.8856    0.7275
   

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
      0.8295    0.4035    0.4992    0.7903
   
   R2 = 
      0.8050    0.3448    0.7143    0.6921    0.9656
   
   R3 = 
      0.8295    0.4035    0.4992    0.7903    0.8050    0.3448    0.7143    0.6921    0.9656
   
   C1 = 
      0.5580
      0.6054
      0.7304
      0.3995
      0.6680
      0.3315
      0.3414
      0.6783
      0.4232
      0.8696
   
   C2 = 
      0.7705
      0.8608
      0.6031
      0.4751
      0.3080
      0.9702
      0.0918
      0.6445
      0.2876
      0.4331
   
   M = 
      0.5580    0.7705
      0.6054    0.8608
      0.7304    0.6031
      0.3995    0.4751
      0.6680    0.3080
      0.3315    0.9702
      0.3414    0.0918
      0.6783    0.6445
      0.4232    0.2876
      0.8696    0.4331
   


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
      0.0233    0.1415    0.7268    0.8749
   
   R2 = 
      0.7110    0.8562    0.4561    0.3474
   
   M = 
      0.0233    0.1415    0.7268    0.8749
      0.7110    0.8562    0.4561    0.3474
   
   C1 = 
      0.5934
      0.2534
      0.4718
      0.2211
      0.1242
      0.9090
      0.1273
      0.2023
      0.1311
      0.7483
   
   C2 = 
      0.7780
      0.7491
   
   C3 = 
      0.5934
      0.2534
      0.4718
      0.2211
      0.1242
      0.9090
      0.1273
      0.2023
      0.1311
      0.7483
      0.7780
      0.7491
   

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
   

