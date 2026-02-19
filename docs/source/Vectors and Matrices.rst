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
      0.5452    0.0610    0.3015    0.1197    0.2865    0.5638    0.2036
   
   C = 
      0.0661
      0.9275
      0.3841
      0.0424
      0.9086
   
   M = 
      0.7032    0.1017    0.7834    0.5283    0.5487    0.1969    0.7264
      0.9822    0.4498    0.9554    0.6479    0.8715    0.9726    0.2883
      0.0766    0.6736    0.7281    0.2578    0.9386    0.1363    0.2498
      0.5627    0.2521    0.9311    0.6989    0.4892    0.5494    0.1665
      0.2572    0.5333    0.3097    0.0160    0.7534    0.9099    0.4000
      0.5419    0.8653    0.5356    0.0300    0.1054    0.1463    0.4285
      0.7168    0.7532    0.9896    0.4973    0.8646    0.1220    0.2811
      0.2536    0.1949    0.4989    0.3168    0.2595    0.1170    0.0557
   

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
      0.2540    0.8878    0.8842    0.1023
   
   R2 = 
      0.5953    0.5589    0.1756    0.2677    0.8712
   
   R3 = 
      0.2540    0.8878    0.8842    0.1023    0.5953    0.5589    0.1756    0.2677    0.8712
   
   C1 = 
      0.8422
      0.4313
      0.7934
      0.3537
      0.4668
      0.0816
      0.1082
      0.4440
      0.6754
      0.6879
   
   C2 = 
      0.3184
      0.6397
      0.5071
      0.2616
      0.8935
      0.9408
      0.6807
      0.8723
      0.7236
      0.9879
   
   M = 
      0.8422    0.3184
      0.4313    0.6397
      0.7934    0.5071
      0.3537    0.2616
      0.4668    0.8935
      0.0816    0.9408
      0.1082    0.6807
      0.4440    0.8723
      0.6754    0.7236
      0.6879    0.9879
   


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
      0.6202    0.6411    0.2786    0.3439
   
   R2 = 
      0.3294    0.3584    0.9167    0.0057
   
   M = 
      0.6202    0.6411    0.2786    0.3439
      0.3294    0.3584    0.9167    0.0057
   
   C1 = 
      0.2186
      0.6426
      0.4803
      0.6310
      0.9159
      0.2367
      0.3622
      0.8452
      0.9820
      0.0587
   
   C2 = 
      0.6876
      0.0997
   
   C3 = 
      0.2186
      0.6426
      0.4803
      0.6310
      0.9159
      0.2367
      0.3622
      0.8452
      0.9820
      0.0587
      0.6876
      0.0997
   

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
   

