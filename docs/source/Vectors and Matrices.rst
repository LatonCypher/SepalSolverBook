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
      0.0139    0.5211    0.5746    0.2081    0.1691    0.2582    0.2901
   
   C = 
      0.4456
      0.1578
      0.7123
      0.4952
      0.8998
   
   M = 
      0.1320    0.3806    0.6652    0.3759    0.5844    0.9582    0.6146
      0.7280    0.2916    0.3682    0.3131    0.5479    0.6587    0.1003
      0.0606    0.4681    0.7542    0.1244    0.4646    0.7840    0.7478
      0.2102    0.7824    0.9467    0.5207    0.1859    0.5377    0.2525
      0.7333    0.8417    0.7930    0.0248    0.0831    0.7456    0.1253
      0.9170    0.4994    0.3042    0.7301    0.4892    0.0524    0.8864
      0.6759    0.3626    0.3971    0.8337    0.1276    0.7071    0.4972
      0.5483    0.6902    0.2237    0.5195    0.2438    0.8551    0.0023
   

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
      0.0502    0.5229    0.5828    0.9921
   
   R2 = 
      0.2788    0.0351    0.1439    0.8459    0.3329
   
   R3 = 
      0.0502    0.5229    0.5828    0.9921    0.2788    0.0351    0.1439    0.8459    0.3329
   
   C1 = 
      0.1939
      0.6746
      0.6759
      0.3601
      0.4741
      0.3528
      0.3536
      0.3183
      0.6930
      0.3710
   
   C2 = 
      0.7484
      0.5929
      0.7164
      0.6530
      0.3092
      0.6812
      0.9160
      0.4307
      0.4482
      0.2048
   
   M = 
      0.1939    0.7484
      0.6746    0.5929
      0.6759    0.7164
      0.3601    0.6530
      0.4741    0.3092
      0.3528    0.6812
      0.3536    0.9160
      0.3183    0.4307
      0.6930    0.4482
      0.3710    0.2048
   


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
      0.7282    0.4772    0.7184    0.6975
   
   R2 = 
      0.7303    0.1295    0.3007    0.5943
   
   M = 
      0.7282    0.4772    0.7184    0.6975
      0.7303    0.1295    0.3007    0.5943
   
   C1 = 
      0.3193
      0.6671
      0.3290
      0.1752
      0.3735
      0.3740
      0.9964
      0.0620
      0.0716
      0.4625
   
   C2 = 
      0.5950
      0.4972
   
   C3 = 
      0.3193
      0.6671
      0.3290
      0.1752
      0.3735
      0.3740
      0.9964
      0.0620
      0.0716
      0.4625
      0.5950
      0.4972
   

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
   

