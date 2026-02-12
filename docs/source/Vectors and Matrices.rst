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
      0.4241    0.3607    0.2532    0.1265    0.2296    0.3273    0.0364
   
   C = 
      0.8875
      0.6915
      0.3439
      0.8771
      0.7668
   
   M = 
      0.8155    0.9286    0.9277    0.9276    0.9956    0.0494    0.2873
      0.3472    0.6918    0.2014    0.2233    0.2109    0.0440    0.7121
      0.5793    0.3573    0.3185    0.9510    0.5817    0.4490    0.1799
      0.5042    0.9214    0.8105    0.7113    0.3231    0.6555    0.9256
      0.4937    0.5252    0.2297    0.4599    0.7589    0.4738    0.1571
      0.3899    0.3539    0.8956    0.7474    0.1116    0.8680    0.8367
      0.2816    0.6292    0.3315    0.1698    0.5184    0.5525    0.0029
      0.4734    0.8598    0.5962    0.9740    0.2203    0.8964    0.9239
   

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
      0.5723    0.3527    0.0683    0.8300
   
   R2 = 
      0.5851    0.7655    0.2889    0.3222    0.8224
   
   R3 = 
      0.5723    0.3527    0.0683    0.8300    0.5851    0.7655    0.2889    0.3222    0.8224
   
   C1 = 
      0.0832
      0.5566
      0.4540
      0.3351
      0.7747
      0.0396
      0.1517
      0.5456
      0.9666
      0.1785
   
   C2 = 
      0.3169
      0.8630
      0.1372
      0.3623
      0.6967
      0.6983
      0.2493
      0.8208
      0.5628
      0.4619
   
   M = 
      0.0832    0.3169
      0.5566    0.8630
      0.4540    0.1372
      0.3351    0.3623
      0.7747    0.6967
      0.0396    0.6983
      0.1517    0.2493
      0.5456    0.8208
      0.9666    0.5628
      0.1785    0.4619
   


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
      0.2518    0.4213    0.1593    0.6279
   
   R2 = 
      0.1659    0.9238    0.9827    0.7063
   
   M = 
      0.2518    0.4213    0.1593    0.6279
      0.1659    0.9238    0.9827    0.7063
   
   C1 = 
      0.6100
      0.4691
      0.6885
      0.7655
      0.9182
      0.4731
      0.5287
      0.6065
      0.8191
      0.1380
   
   C2 = 
      0.8094
      0.9741
   
   C3 = 
      0.6100
      0.4691
      0.6885
      0.7655
      0.9182
      0.4731
      0.5287
      0.6065
      0.8191
      0.1380
      0.8094
      0.9741
   

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
   

