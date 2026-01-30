Vectors and Matrices
====================

Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: RowVec, ColVec and Matrix. RowVec and ColVec are 1D arrays while Matrix is a 2D array. 

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
      5.0000    6.0000    7.0000    1.0000
   
   C = 
      8.0000
      3.0000
      4.0000
      2.0000
      7.0000
   
   M = 
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   


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
      0.7780    0.4177    0.6034    0.6056    0.7868    0.6949    0.9964
   
   C = 
      0.5511
      0.6959
      0.2335
      0.9346
      0.1496
   
   M = 
      0.3745    0.9963    0.0209    0.8272    0.7824    0.7315    0.3898
      0.3112    0.7322    0.2601    0.8990    0.6272    0.7920    0.3455
      0.1728    0.7020    0.8260    0.0882    0.8550    0.3826    0.4705
      0.8467    0.8811    0.3916    0.9814    0.7333    0.0817    0.2765
      0.2599    0.1358    0.9157    0.3376    0.3728    0.8323    0.7839
      0.1295    0.8693    0.0429    0.9960    0.7379    0.3682    0.3031
      0.5838    0.4058    0.3730    0.1918    0.6028    0.0541    0.3553
      0.1734    0.7053    0.4804    0.3636    0.8297    0.2685    0.3652
   

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
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   
   C = 
      1.0000
      1.0000
      1.0000
      1.0000
      1.0000
   
   M = 
      1.0000    0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    1.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    1.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    1.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    1.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    1.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000    1.0000
   

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
      0.7318    0.0206    0.5088    0.3298
   
   R2 = 
      0.8085    0.6883    0.4834    0.8707    0.7860
   
   R3 = 
      0.7318    0.0206    0.5088    0.3298    0.8085    0.6883    0.4834    0.8707    0.7860
   
   C1 = 
      0.0230
      0.2553
      0.9374
      0.3658
      0.6536
      0.0224
      0.3062
      0.7843
      0.0976
      0.3593
   
   C2 = 
      0.4995
      0.8106
      0.6288
      0.1967
      0.3791
      0.3736
      0.0207
      0.9414
      0.4342
      0.7929
   
   M = 
      0.0230    0.4995
      0.2553    0.8106
      0.9374    0.6288
      0.3658    0.1967
      0.6536    0.3791
      0.0224    0.3736
      0.3062    0.0207
      0.7843    0.9414
      0.0976    0.4342
      0.3593    0.7929
   


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
      0.3472    0.6237    0.5683    0.9377
   
   R2 = 
      0.6349    0.7344    0.3818    0.9797
   
   M = 
      0.3472    0.6237    0.5683    0.9377
      0.6349    0.7344    0.3818    0.9797
   
   C1 = 
      0.9596
      0.9130
      0.0690
      0.4476
      0.2463
      0.5784
      0.2208
      0.2559
      0.7966
      0.4378
   
   C2 = 
      0.2795
      0.5804
   
   C3 = 
      0.9596
      0.9130
      0.0690
      0.4476
      0.2463
      0.5784
      0.2208
      0.2559
      0.7966
      0.4378
      0.2795
      0.5804
   

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
      5.0000   -2.0000    3.0000    7.0000
      2.0000    1.0000   -7.0000    3.0000
      4.0000    8.0000    9.0000    1.0000
      0.0000    5.0000   -6.0000   -3.0000
   
   Flipud(M) = 
      0.0000    5.0000   -6.0000   -3.0000
      4.0000    8.0000    9.0000    1.0000
      2.0000    1.0000   -7.0000    3.0000
      5.0000   -2.0000    3.0000    7.0000
   
   Fliplr(M) = 
      7.0000    3.0000   -2.0000    5.0000
      3.0000   -7.0000    1.0000    2.0000
      1.0000    9.0000    8.0000    4.0000
     -3.0000   -6.0000    5.0000    0.0000
   

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
      5.0000   -2.0000    3.0000    7.0000
      0.0000    1.0000   -7.0000    3.0000
      0.0000    0.0000    9.0000    1.0000
      0.0000    0.0000    0.0000   -3.0000
   
   Tril(M) = 
      5.0000    0.0000    0.0000    0.0000
      2.0000    1.0000    0.0000    0.0000
      4.0000    8.0000    9.0000    0.0000
      0.0000    5.0000   -6.0000   -3.0000
   

