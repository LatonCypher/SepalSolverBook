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
      0.7060    0.0670    0.1824    0.0533    0.3220    0.6403    0.1749
   
   C = 
      0.7441
      0.0545
      0.0973
      0.0161
      0.6146
   
   M = 
      0.7609    0.5082    0.4893    0.8672    0.0706    0.0467    0.1119
      0.1886    0.5725    0.3832    0.8687    0.6725    0.5139    0.9824
      0.7159    0.2250    0.1097    0.4811    0.9343    0.4858    0.4172
      0.6201    0.7051    0.4152    0.4911    0.4031    0.1911    0.4696
      0.7957    0.2718    0.4431    0.7753    0.3024    0.0833    0.3474
      0.8140    0.1625    0.5701    0.0884    0.5757    0.4587    0.5843
      0.6904    0.2574    0.1689    0.3415    0.3009    0.2114    0.7156
      0.0808    0.4752    0.7101    0.3139    0.0330    0.7670    0.9125
   

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
      0.5019    0.5947    0.9837    0.7058
   
   R2 = 
      0.0083    0.0432    0.5750    0.3129    0.6574
   
   R3 = 
      0.5019    0.5947    0.9837    0.7058    0.0083    0.0432    0.5750    0.3129    0.6574
   
   C1 = 
      0.9570
      0.5021
      0.6598
      0.5078
      0.9392
      0.0344
      0.9619
      0.8150
      0.4899
      0.1829
   
   C2 = 
      0.1925
      0.8157
      0.7671
      0.8651
      0.2858
      0.4367
      0.5753
      0.6934
      0.0807
      0.7986
   
   M = 
      0.9570    0.1925
      0.5021    0.8157
      0.6598    0.7671
      0.5078    0.8651
      0.9392    0.2858
      0.0344    0.4367
      0.9619    0.5753
      0.8150    0.6934
      0.4899    0.0807
      0.1829    0.7986
   


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
      0.7535    0.0812    0.6575    0.5026
   
   R2 = 
      0.7600    0.0938    0.6611    0.5007
   
   M = 
      0.7535    0.0812    0.6575    0.5026
      0.7600    0.0938    0.6611    0.5007
   
   C1 = 
      0.4578
      0.1695
      0.8563
      0.1358
      0.3196
      0.0297
      0.7218
      0.5969
      0.6311
      0.3471
   
   C2 = 
      0.9826
      0.2517
   
   C3 = 
      0.4578
      0.1695
      0.8563
      0.1358
      0.3196
      0.0297
      0.7218
      0.5969
      0.6311
      0.3471
      0.9826
      0.2517
   

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
   

