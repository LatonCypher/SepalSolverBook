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
      0.9440    0.1359    0.4913    0.0543    0.5100    0.1626    0.1394
   
   C = 
      0.0970
      0.8195
      0.5733
      0.4884
      0.7950
   
   M = 
      0.3321    0.9628    0.1156    0.5207    0.8945    0.4404    0.2526
      0.2369    0.6592    0.4273    0.0036    0.3743    0.4426    0.0380
      0.6985    0.6078    0.9788    0.9147    0.1572    0.4766    0.6995
      0.9061    0.7459    0.7145    0.4093    0.0514    0.0219    0.1781
      0.0758    0.8477    0.5652    0.7237    0.2201    0.2193    0.7222
      0.6070    0.0155    0.2334    0.3837    0.4870    0.2439    0.0862
      0.4729    0.7809    0.3748    0.9283    0.8189    0.8926    0.1075
      0.4347    0.5685    0.9379    0.0501    0.6182    0.0271    0.8206
   

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
      0.9727    0.0966    0.7321    0.6011
   
   R2 = 
      0.2400    0.8398    0.4226    0.4610    0.1528
   
   R3 = 
      0.9727    0.0966    0.7321    0.6011    0.2400    0.8398    0.4226    0.4610    0.1528
   
   C1 = 
      0.3625
      0.1820
      0.3646
      0.4775
      0.4945
      0.2113
      0.1636
      0.7159
      0.0250
      0.7978
   
   C2 = 
      0.4979
      0.3124
      0.3558
      0.8474
      0.8020
      0.8124
      0.9013
      0.4684
      0.9096
      0.8957
   
   M = 
      0.3625    0.4979
      0.1820    0.3124
      0.3646    0.3558
      0.4775    0.8474
      0.4945    0.8020
      0.2113    0.8124
      0.1636    0.9013
      0.7159    0.4684
      0.0250    0.9096
      0.7978    0.8957
   


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
      0.9869    0.8906    0.9590    0.3377
   
   R2 = 
      0.7716    0.7167    0.0258    0.3441
   
   M = 
      0.9869    0.8906    0.9590    0.3377
      0.7716    0.7167    0.0258    0.3441
   
   C1 = 
      0.9485
      0.2610
      0.8518
      0.1288
      0.1945
      0.0281
      0.6642
      0.0521
      0.2698
      0.3818
   
   C2 = 
      0.5775
      0.0647
   
   C3 = 
      0.9485
      0.2610
      0.8518
      0.1288
      0.1945
      0.0281
      0.6642
      0.0521
      0.2698
      0.3818
      0.5775
      0.0647
   

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
   

