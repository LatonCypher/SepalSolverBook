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
      0.6826    0.7596    0.7026    0.0216    0.9450    0.5101    0.7939
   
   C = 
      0.4662
      0.0463
      0.9803
      0.0604
      0.0066
   
   M = 
      0.2647    0.5741    0.0937    0.7565    0.6447    0.0245    0.6783
      0.1000    0.1605    0.6432    0.8589    0.6363    0.7116    0.1099
      0.0637    0.1991    0.9985    0.2728    0.5557    0.4760    0.0145
      0.9284    0.5808    0.7632    0.3654    0.2082    0.4112    0.2758
      0.2349    0.9489    0.1489    0.1948    0.0856    0.1410    0.6826
      0.4566    0.0816    0.2130    0.4043    0.0538    0.9004    0.6680
      0.6070    0.4288    0.1891    0.7155    0.9687    0.2739    0.4309
      0.4912    0.8795    0.9010    0.9989    0.3016    0.4015    0.7278
   

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
      0.0528    0.1873    0.7401    0.2610
   
   R2 = 
      0.1505    0.4216    0.1173    0.0075    0.1410
   
   R3 = 
      0.0528    0.1873    0.7401    0.2610    0.1505    0.4216    0.1173    0.0075    0.1410
   
   C1 = 
      0.0677
      0.7077
      0.9190
      0.2557
      0.5874
      0.1080
      0.3560
      0.9825
      0.2535
      0.0920
   
   C2 = 
      0.6835
      0.8219
      0.7383
      0.4589
      0.5673
      0.8511
      0.9664
      0.8259
      0.4228
      0.7979
   
   M = 
      0.0677    0.6835
      0.7077    0.8219
      0.9190    0.7383
      0.2557    0.4589
      0.5874    0.5673
      0.1080    0.8511
      0.3560    0.9664
      0.9825    0.8259
      0.2535    0.4228
      0.0920    0.7979
   


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
      0.5486    0.7819    0.0858    0.8146
   
   R2 = 
      0.7071    0.0975    0.4156    0.6410
   
   M = 
      0.5486    0.7819    0.0858    0.8146
      0.7071    0.0975    0.4156    0.6410
   
   C1 = 
      0.7535
      0.6942
      0.8159
      0.8843
      0.2845
      0.7604
      0.5956
      0.6371
      0.0039
      0.5341
   
   C2 = 
      0.6748
      0.9478
   
   C3 = 
      0.7535
      0.6942
      0.8159
      0.8843
      0.2845
      0.7604
      0.5956
      0.6371
      0.0039
      0.5341
      0.6748
      0.9478
   

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
   

