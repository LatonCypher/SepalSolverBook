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
      0.7643    0.0591    0.8479    0.1237    0.3845    0.6268    0.0968
   
   C = 
      0.4055
      0.1943
      0.2481
      0.3484
      0.2198
   
   M = 
      0.1801    0.3824    0.2727    0.5159    0.8336    0.3563    0.6336
      0.6882    0.7471    0.2857    0.1065    0.4947    0.5697    0.3711
      0.3752    0.2766    0.7753    0.4964    0.9485    0.1104    0.9614
      0.1895    0.0210    0.0889    0.6849    0.2575    0.4619    0.8868
      0.3461    0.0823    0.7456    0.9817    0.0740    0.3192    0.2867
      0.5608    0.5651    0.4926    0.5474    0.2093    0.0472    0.7632
      0.2010    0.7165    0.7035    0.3232    0.4801    0.9961    0.0533
      0.9134    0.4031    0.4916    0.1847    0.1593    0.7460    0.6675
   

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
      0.9542    0.7808    0.3574    0.3441
   
   R2 = 
      0.0249    0.2566    0.2515    0.6418    0.5027
   
   R3 = 
      0.9542    0.7808    0.3574    0.3441    0.0249    0.2566    0.2515    0.6418    0.5027
   
   C1 = 
      0.1215
      0.9920
      0.5514
      0.0225
      0.1318
      0.9910
      0.7838
      0.4669
      0.3948
      0.1224
   
   C2 = 
      0.2261
      0.8163
      0.4740
      0.3827
      0.3204
      0.5638
      0.2016
      0.8730
      0.0022
      0.2453
   
   M = 
      0.1215    0.2261
      0.9920    0.8163
      0.5514    0.4740
      0.0225    0.3827
      0.1318    0.3204
      0.9910    0.5638
      0.7838    0.2016
      0.4669    0.8730
      0.3948    0.0022
      0.1224    0.2453
   


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
      0.8965    0.6249    0.3004    0.5137
   
   R2 = 
      0.0197    0.2311    0.1866    0.3568
   
   M = 
      0.8965    0.6249    0.3004    0.5137
      0.0197    0.2311    0.1866    0.3568
   
   C1 = 
      0.5882
      0.7474
      0.5661
      0.6679
      0.6345
      0.4839
      0.0775
      0.8129
      0.7234
      0.0516
   
   C2 = 
      0.6063
      0.5317
   
   C3 = 
      0.5882
      0.7474
      0.5661
      0.6679
      0.6345
      0.4839
      0.0775
      0.8129
      0.7234
      0.0516
      0.6063
      0.5317
   

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
   

