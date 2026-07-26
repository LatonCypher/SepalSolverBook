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
      0.7599    0.9532    0.8887    0.2466    0.3447    0.8353    0.7888
   
   C = 
      0.9899
      0.3771
      0.6639
      0.5399
      0.8590
   
   M = 
      0.8844    0.3315    0.0185    0.7041    0.9389    0.1279    0.2200
      0.2731    0.0163    0.2055    0.5840    0.7932    0.7397    0.8309
      0.4259    0.1301    0.9886    0.4181    0.9785    0.8170    0.0403
      0.1484    0.5256    0.9369    0.7330    0.9506    0.2328    0.1743
      0.4055    0.5978    0.5302    0.2829    0.0431    0.9571    0.1741
      0.8940    0.2373    0.1206    0.0268    0.7774    0.2055    0.7311
      0.3720    0.1559    0.1751    0.3450    0.7533    0.7579    0.7318
      0.7558    0.3400    0.2834    0.5875    0.5376    0.4709    0.6527
   

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
      0.8384    0.9602    0.3797    0.8132
   
   R2 = 
      0.3031    0.5328    0.2713    0.2206    0.7694
   
   R3 = 
      0.8384    0.9602    0.3797    0.8132    0.3031    0.5328    0.2713    0.2206    0.7694
   
   C1 = 
      0.7170
      0.2989
      0.2598
      0.7122
      0.3614
      0.6647
      0.2431
      0.1561
      0.7191
      0.0298
   
   C2 = 
      0.1653
      0.2615
      0.4611
      0.4342
      0.3788
      0.0630
      0.4828
      0.7878
      0.1632
      0.7353
   
   M = 
      0.7170    0.1653
      0.2989    0.2615
      0.2598    0.4611
      0.7122    0.4342
      0.3614    0.3788
      0.6647    0.0630
      0.2431    0.4828
      0.1561    0.7878
      0.7191    0.1632
      0.0298    0.7353
   


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
      0.6024    0.4286    0.9818    0.0834
   
   R2 = 
      0.5033    0.2803    0.1813    0.7378
   
   M = 
      0.6024    0.4286    0.9818    0.0834
      0.5033    0.2803    0.1813    0.7378
   
   C1 = 
      0.7201
      0.9331
      0.7693
      0.5261
      0.4656
      0.4712
      0.9665
      0.3047
      0.2269
      0.5904
   
   C2 = 
      0.1064
      0.9106
   
   C3 = 
      0.7201
      0.9331
      0.7693
      0.5261
      0.4656
      0.4712
      0.9665
      0.3047
      0.2269
      0.5904
      0.1064
      0.9106
   

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
   

