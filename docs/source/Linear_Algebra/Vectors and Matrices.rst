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
      0.9877    0.7946    0.2568    0.0510    0.6224    0.9037    0.7438
   
   C = 
      0.3676
      0.9317
      0.3437
      0.7891
      0.9767
   
   M = 
      0.7630    0.1427    0.7334    0.5410    0.0024    0.4033    0.8302
      0.3518    0.1847    0.7347    0.5007    0.9106    0.3175    0.8479
      0.2174    0.5266    0.3738    0.5084    0.0412    0.5108    0.7054
      0.5434    0.8696    0.4344    0.5676    0.8307    0.4759    0.0376
      0.3734    0.2606    0.3537    0.5600    0.7310    0.4558    0.9482
      0.6095    0.2342    0.5322    0.8203    0.0611    0.3828    0.3610
      0.9876    0.6696    0.1624    0.8838    0.0036    0.2659    0.6811
      0.6809    0.1175    0.1981    0.0684    0.3361    0.4822    0.6940
   

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
      0.3985    0.9754    0.7891    0.8682
   
   R2 = 
      0.5660    0.7703    0.3998    0.0795    0.2420
   
   R3 = 
      0.3985    0.9754    0.7891    0.8682    0.5660    0.7703    0.3998    0.0795    0.2420
   
   C1 = 
      0.1366
      0.3641
      0.6461
      0.4205
      0.0318
      0.7988
      0.4189
      0.9214
      0.3064
      0.4380
   
   C2 = 
      0.1445
      0.3568
      0.9342
      0.9326
      0.2710
      0.5148
      0.8020
      0.8873
      0.2894
      0.3602
   
   M = 
      0.1366    0.1445
      0.3641    0.3568
      0.6461    0.9342
      0.4205    0.9326
      0.0318    0.2710
      0.7988    0.5148
      0.4189    0.8020
      0.9214    0.8873
      0.3064    0.2894
      0.4380    0.3602
   


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
      0.7763    0.5971    0.9403    0.7616
   
   R2 = 
      0.0388    0.5329    0.8259    0.0542
   
   M = 
      0.7763    0.5971    0.9403    0.7616
      0.0388    0.5329    0.8259    0.0542
   
   C1 = 
      0.9004
      0.7589
      0.3516
      0.0789
      0.4352
      0.3434
      0.4804
      0.3805
      0.3202
      0.6556
   
   C2 = 
      0.2051
      0.4085
   
   C3 = 
      0.9004
      0.7589
      0.3516
      0.0789
      0.4352
      0.3434
      0.4804
      0.3805
      0.3202
      0.6556
      0.2051
      0.4085
   

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
   

