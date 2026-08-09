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
      0.5150    0.3849    0.1979    0.2635    0.7363    0.8069    0.3336
   
   C = 
      0.1880
      0.7502
      0.7776
      0.2049
      0.3724
   
   M = 
      0.9110    0.2043    0.8648    0.6061    0.3500    0.6252    0.2294
      0.0848    0.7000    0.6209    0.0762    0.1264    0.6084    0.2717
      0.6592    0.5313    0.5170    0.8294    0.3235    0.3490    0.6480
      0.6187    0.4636    0.1960    0.1564    0.3291    0.4942    0.7670
      0.1596    0.2954    0.7766    0.4456    0.3507    0.9875    0.5673
      0.2107    0.1713    0.3613    0.1216    0.4557    0.1540    0.9457
      0.4687    0.6128    0.4763    0.2237    0.1953    0.8621    0.6461
      0.6386    0.7612    0.0900    0.7670    0.7499    0.4660    0.7104
   

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
      0.0888    0.2719    0.6213    0.1700
   
   R2 = 
      0.5248    0.5053    0.9037    0.2796    0.6689
   
   R3 = 
      0.0888    0.2719    0.6213    0.1700    0.5248    0.5053    0.9037    0.2796    0.6689
   
   C1 = 
      0.9934
      0.2854
      0.1532
      0.4795
      0.7241
      0.7836
      0.4854
      0.1989
      0.0210
      0.8340
   
   C2 = 
      0.9995
      0.5595
      0.4761
      0.8136
      0.6703
      0.5741
      0.0154
      0.8763
      0.7452
      0.9168
   
   M = 
      0.9934    0.9995
      0.2854    0.5595
      0.1532    0.4761
      0.4795    0.8136
      0.7241    0.6703
      0.7836    0.5741
      0.4854    0.0154
      0.1989    0.8763
      0.0210    0.7452
      0.8340    0.9168
   


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
      0.8705    0.2848    0.6655    0.7589
   
   R2 = 
      0.4534    0.5205    0.3004    0.0250
   
   M = 
      0.8705    0.2848    0.6655    0.7589
      0.4534    0.5205    0.3004    0.0250
   
   C1 = 
      0.7801
      0.8973
      0.7956
      0.5113
      0.4168
      0.4376
      0.0398
      0.4902
      0.2056
      0.9063
   
   C2 = 
      0.2566
      0.8022
   
   C3 = 
      0.7801
      0.8973
      0.7956
      0.5113
      0.4168
      0.4376
      0.0398
      0.4902
      0.2056
      0.9063
      0.2566
      0.8022
   

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
   

