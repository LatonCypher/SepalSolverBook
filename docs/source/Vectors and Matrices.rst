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
      0.2009    0.0600    0.2221    0.7113    0.7756    0.4121    0.9863
   
   C = 
      0.1611
      0.0616
      0.8576
      0.3446
      0.6830
   
   M = 
      0.3622    0.4888    0.1481    0.4645    0.7466    0.2926    0.4688
      0.8720    0.6459    0.7697    0.5910    0.1886    0.7920    0.4575
      0.7491    0.9149    0.9977    0.8929    0.4518    0.0266    0.5915
      0.4228    0.1903    0.8037    0.6255    0.1713    0.3939    0.6020
      0.1920    0.9822    0.2733    0.5131    0.1208    0.5055    0.6029
      0.3850    0.2096    0.8006    0.7215    0.2024    0.5526    0.7770
      0.6896    0.8602    0.1695    0.7702    0.2812    0.4270    0.2852
      0.3566    0.7830    0.5747    0.5190    0.0899    0.2532    0.7155
   

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
      0.5130    0.7016    0.6166    0.0035
   
   R2 = 
      0.6566    0.5417    0.0021    0.8005    0.5403
   
   R3 = 
      0.5130    0.7016    0.6166    0.0035    0.6566    0.5417    0.0021    0.8005    0.5403
   
   C1 = 
      0.9303
      0.0821
      0.4283
      0.7249
      0.1882
      0.0553
      0.2942
      0.9327
      0.8208
      0.2388
   
   C2 = 
      0.5933
      0.2770
      0.8832
      0.7582
      0.6445
      0.8694
      0.1227
      0.5198
      0.8495
      0.7304
   
   M = 
      0.9303    0.5933
      0.0821    0.2770
      0.4283    0.8832
      0.7249    0.7582
      0.1882    0.6445
      0.0553    0.8694
      0.2942    0.1227
      0.9327    0.5198
      0.8208    0.8495
      0.2388    0.7304
   


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
      0.4632    0.5005    0.5521    0.5862
   
   R2 = 
      0.4080    0.4403    0.9262    0.4796
   
   M = 
      0.4632    0.5005    0.5521    0.5862
      0.4080    0.4403    0.9262    0.4796
   
   C1 = 
      0.7193
      0.7808
      0.6436
      0.9419
      0.9143
      0.5843
      0.1565
      0.5913
      0.2410
      0.8520
   
   C2 = 
      0.5103
      0.0781
   
   C3 = 
      0.7193
      0.7808
      0.6436
      0.9419
      0.9143
      0.5843
      0.1565
      0.5913
      0.2410
      0.8520
      0.5103
      0.0781
   

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
   

