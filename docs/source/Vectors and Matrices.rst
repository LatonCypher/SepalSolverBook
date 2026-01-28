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
      0.9634    0.7443    0.9262    0.9912    0.0407    0.1736    0.8768
   
   C = 
      0.7360
      0.1569
      0.7408
      0.8999
      0.8002
   
   M = 
      0.0722    0.4958    0.2429    0.5069    0.6326    0.1116    0.1421
      0.7981    0.0562    0.5061    0.4911    0.4985    0.0156    0.3535
      0.3199    0.9535    0.4281    0.2679    0.5366    0.7109    0.4176
      0.2530    0.1057    0.1466    0.9778    0.0977    0.1739    0.7244
      0.5113    0.7863    0.5905    0.1480    0.3338    0.1095    0.8077
      0.1733    0.7592    0.9011    0.2941    0.6081    0.0748    0.7554
      0.6756    0.6485    0.0611    0.4655    0.0270    0.2049    0.4993
      0.0706    0.5382    0.6095    0.8830    0.6005    0.6209    0.0319
   

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
      0.2500    0.4566    0.9782    0.0970
   
   R2 = 
      0.4432    0.9002    0.0707    0.8668    0.2211
   
   R3 = 
      0.2500    0.4566    0.9782    0.0970    0.4432    0.9002    0.0707    0.8668    0.2211
   
   C1 = 
      0.1141
      0.7076
      0.2183
      0.4061
      0.5451
      0.0583
      0.7671
      0.0445
      0.9654
      0.2559
   
   C2 = 
      0.9153
      0.4327
      0.0461
      0.0358
      0.4869
      0.2424
      0.0315
      0.3027
      0.3748
      0.0299
   
   M = 
      0.1141    0.9153
      0.7076    0.4327
      0.2183    0.0461
      0.4061    0.0358
      0.5451    0.4869
      0.0583    0.2424
      0.7671    0.0315
      0.0445    0.3027
      0.9654    0.3748
      0.2559    0.0299
   


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
      0.5347    0.5216    0.8409    0.9084
   
   R2 = 
      0.2825    0.6187    0.1731    0.5850
   
   M = 
      0.5347    0.5216    0.8409    0.9084
      0.2825    0.6187    0.1731    0.5850
   
   C1 = 
      0.5146
      0.5010
      0.0994
      0.7661
      0.4538
      0.8814
      0.4853
      0.0700
      0.9653
      0.8515
   
   C2 = 
      0.5861
      0.3784
   
   C3 = 
      0.5146
      0.5010
      0.0994
      0.7661
      0.4538
      0.8814
      0.4853
      0.0700
      0.9653
      0.8515
      0.5861
      0.3784
   

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
   

