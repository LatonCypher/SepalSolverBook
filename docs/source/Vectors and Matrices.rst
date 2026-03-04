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
      0.4590    0.6433    0.8075    0.2855    0.3158    0.7041    0.1223
   
   C = 
      0.2128
      0.5181
      0.8204
      0.6271
      0.9044
   
   M = 
      0.0125    0.0139    0.2425    0.5279    0.7837    0.4021    0.4168
      0.4504    0.9429    0.9772    0.5011    0.4323    0.0282    0.6047
      0.3419    0.1561    0.9785    0.0660    0.3695    0.2893    0.8172
      0.9882    0.8089    0.5170    0.9701    0.9654    0.1725    0.0815
      0.1854    0.1537    0.3591    0.0199    0.5698    0.8592    0.8973
      0.6573    0.7686    0.8625    0.5786    0.4594    0.4853    0.4404
      0.8276    0.1769    0.9990    0.7154    0.2498    0.1638    0.7089
      0.5704    0.8881    0.2238    0.8245    0.2986    0.3360    0.5632
   

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
      0.2278    0.9761    0.9609    0.3077
   
   R2 = 
      0.0877    0.6382    0.1932    0.1734    0.1136
   
   R3 = 
      0.2278    0.9761    0.9609    0.3077    0.0877    0.6382    0.1932    0.1734    0.1136
   
   C1 = 
      0.9123
      0.5697
      0.0220
      0.1484
      0.9973
      0.5535
      0.0980
      0.1875
      0.3430
      0.1711
   
   C2 = 
      0.2537
      0.2621
      0.1511
      0.8330
      0.7920
      0.4394
      0.7200
      0.5108
      0.7700
      0.4599
   
   M = 
      0.9123    0.2537
      0.5697    0.2621
      0.0220    0.1511
      0.1484    0.8330
      0.9973    0.7920
      0.5535    0.4394
      0.0980    0.7200
      0.1875    0.5108
      0.3430    0.7700
      0.1711    0.4599
   


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
      0.9527    0.9261    0.3312    0.0782
   
   R2 = 
      0.9503    0.1534    0.7099    0.5657
   
   M = 
      0.9527    0.9261    0.3312    0.0782
      0.9503    0.1534    0.7099    0.5657
   
   C1 = 
      0.6203
      0.4930
      0.9227
      0.9265
      0.3027
      0.0512
      0.7992
      0.5842
      0.9003
      0.6299
   
   C2 = 
      0.2116
      0.6063
   
   C3 = 
      0.6203
      0.4930
      0.9227
      0.9265
      0.3027
      0.0512
      0.7992
      0.5842
      0.9003
      0.6299
      0.2116
      0.6063
   

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
   

