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
      0.1765    0.8364    0.8612    0.9384    0.5511    0.7134    0.4719
   
   C = 
      0.8041
      0.7626
      0.4811
      0.4514
      0.2096
   
   M = 
      0.1892    0.5099    0.4204    0.7939    0.5940    0.0727    0.7860
      0.0663    0.6342    0.7068    0.8435    0.8976    0.2174    0.6738
      0.6752    0.9647    0.2811    0.8147    0.9232    0.5454    0.6690
      0.1271    0.1461    0.2012    0.8697    0.8808    0.8554    0.4632
      0.2508    0.0404    0.0232    0.0998    0.7411    0.4407    0.3336
      0.4270    0.6365    0.6786    0.8150    0.3370    0.0629    0.8361
      0.4181    0.2377    0.0268    0.3590    0.9969    0.6466    0.9986
      0.5566    0.8372    0.7891    0.4699    0.7030    0.6860    0.9651
   

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
      0.9476    0.7023    0.6319    0.6182
   
   R2 = 
      0.2687    0.8124    0.5942    0.4275    0.9486
   
   R3 = 
      0.9476    0.7023    0.6319    0.6182    0.2687    0.8124    0.5942    0.4275    0.9486
   
   C1 = 
      0.1989
      0.5432
      0.3162
      0.8148
      0.0777
      0.8291
      0.3387
      0.1388
      0.8125
      0.6466
   
   C2 = 
      0.1145
      0.3229
      0.0453
      0.5690
      0.6986
      0.5507
      0.3718
      0.3131
      0.7391
      0.2089
   
   M = 
      0.1989    0.1145
      0.5432    0.3229
      0.3162    0.0453
      0.8148    0.5690
      0.0777    0.6986
      0.8291    0.5507
      0.3387    0.3718
      0.1388    0.3131
      0.8125    0.7391
      0.6466    0.2089
   


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
      0.6528    0.4331    0.7908    0.5462
   
   R2 = 
      0.2795    0.9549    0.5521    0.1155
   
   M = 
      0.6528    0.4331    0.7908    0.5462
      0.2795    0.9549    0.5521    0.1155
   
   C1 = 
      0.7077
      0.2488
      0.6131
      0.9557
      0.1356
      0.7389
      0.8912
      0.8980
      0.3441
      0.3972
   
   C2 = 
      0.9315
      0.5987
   
   C3 = 
      0.7077
      0.2488
      0.6131
      0.9557
      0.1356
      0.7389
      0.8912
      0.8980
      0.3441
      0.3972
      0.9315
      0.5987
   

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
   

