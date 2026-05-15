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
      0.7286    0.2697    0.2644    0.3771    0.5349    0.2584    0.5283
   
   C = 
      0.9817
      0.4944
      0.0021
      0.3853
      0.8638
   
   M = 
      0.8293    0.4590    0.8960    0.4356    0.2462    0.3380    0.1968
      0.5580    0.2325    0.0565    0.9436    0.1409    0.5661    0.6390
      0.4707    0.9607    0.2764    0.5060    0.6271    0.6906    0.3892
      0.8945    0.6180    0.3838    0.6372    0.1283    0.3760    0.0248
      0.5125    0.6158    0.7787    0.9989    0.9380    0.4487    0.8928
      0.0154    0.6481    0.8035    0.5609    0.3472    0.1919    0.3750
      0.1852    0.0952    0.2938    0.7105    0.1713    0.7185    0.0977
      0.2108    0.5162    0.8636    0.0203    0.8960    0.9636    0.9499
   

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
      0.1306    0.8769    0.7190    0.9998
   
   R2 = 
      0.9786    0.0505    0.2496    0.6044    0.2970
   
   R3 = 
      0.1306    0.8769    0.7190    0.9998    0.9786    0.0505    0.2496    0.6044    0.2970
   
   C1 = 
      0.2198
      0.2659
      0.8312
      0.8917
      0.8045
      0.7322
      0.0375
      0.6122
      0.7456
      0.4827
   
   C2 = 
      0.5061
      0.2160
      0.7427
      0.0061
      0.5754
      0.1454
      0.9414
      0.4533
      0.2732
      0.1666
   
   M = 
      0.2198    0.5061
      0.2659    0.2160
      0.8312    0.7427
      0.8917    0.0061
      0.8045    0.5754
      0.7322    0.1454
      0.0375    0.9414
      0.6122    0.4533
      0.7456    0.2732
      0.4827    0.1666
   


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
      0.4961    0.1472    0.2738    0.7153
   
   R2 = 
      0.5169    0.1681    0.9857    0.3088
   
   M = 
      0.4961    0.1472    0.2738    0.7153
      0.5169    0.1681    0.9857    0.3088
   
   C1 = 
      0.1154
      0.6415
      0.7059
      0.6043
      0.9377
      0.9823
      0.7948
      0.1656
      0.8804
      0.7554
   
   C2 = 
      0.2968
      0.6035
   
   C3 = 
      0.1154
      0.6415
      0.7059
      0.6043
      0.9377
      0.9823
      0.7948
      0.1656
      0.8804
      0.7554
      0.2968
      0.6035
   

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
   

