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
      0.9101    0.3817    0.1511    0.9280    0.6861    0.8878    0.3658
   
   C = 
      0.3526
      0.8369
      0.8016
      0.5053
      0.1050
   
   M = 
      0.3190    0.7630    0.6603    0.2719    0.5765    0.3205    0.9725
      0.5701    0.2557    0.2590    0.2189    0.8699    0.3703    0.2771
      0.2642    0.4760    0.1273    0.2573    0.2519    0.2593    0.9709
      0.1009    0.9694    0.0388    0.2887    0.7981    0.5264    0.2265
      0.5220    0.0771    0.9321    0.7735    0.1588    0.9606    0.0784
      0.7385    0.8308    0.4290    0.1991    0.7696    0.0597    0.1959
      0.4939    0.5806    0.2722    0.6079    0.8981    0.2054    0.6591
      0.0071    0.1088    0.6836    0.3982    0.2187    0.8064    0.1100
   

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
      0.5354    0.2893    0.1385    0.5940
   
   R2 = 
      0.1105    0.5601    0.8119    0.5764    0.2837
   
   R3 = 
      0.5354    0.2893    0.1385    0.5940    0.1105    0.5601    0.8119    0.5764    0.2837
   
   C1 = 
      0.4751
      0.9484
      0.2266
      0.4677
      0.6607
      0.6982
      0.3762
      0.2227
      0.2130
      0.8549
   
   C2 = 
      0.4160
      0.9595
      0.5684
      0.0648
      0.2080
      0.8480
      0.6929
      0.8536
      0.6597
      0.0218
   
   M = 
      0.4751    0.4160
      0.9484    0.9595
      0.2266    0.5684
      0.4677    0.0648
      0.6607    0.2080
      0.6982    0.8480
      0.3762    0.6929
      0.2227    0.8536
      0.2130    0.6597
      0.8549    0.0218
   


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
      0.7164    0.1592    0.8108    0.9954
   
   R2 = 
      0.5921    0.8740    0.7893    0.8226
   
   M = 
      0.7164    0.1592    0.8108    0.9954
      0.5921    0.8740    0.7893    0.8226
   
   C1 = 
      0.4491
      0.5623
      0.8515
      0.1556
      0.6539
      0.5629
      0.2458
      0.2060
      0.4607
      0.8408
   
   C2 = 
      0.6986
      0.3431
   
   C3 = 
      0.4491
      0.5623
      0.8515
      0.1556
      0.6539
      0.5629
      0.2458
      0.2060
      0.4607
      0.8408
      0.6986
      0.3431
   

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
   

