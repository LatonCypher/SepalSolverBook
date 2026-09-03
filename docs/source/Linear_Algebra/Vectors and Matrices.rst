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
      0.1136    0.0942    0.7844    0.9328    0.1272    0.3811    0.6392
   
   C = 
      0.9913
      0.5992
      0.3034
      0.5806
      0.5262
   
   M = 
      0.7854    0.2475    0.0273    0.3632    0.2947    0.2811    0.3594
      0.1007    0.6605    0.5942    0.9684    0.8962    0.7234    0.3310
      0.4666    0.4078    0.8790    0.9455    0.3803    0.5832    0.2066
      0.1833    0.3511    0.3069    0.3116    0.5042    0.3232    0.1680
      0.4210    0.5747    0.2416    0.8081    0.5884    0.6300    0.5697
      0.2847    0.5992    0.0114    0.1535    0.0558    0.6807    0.6533
      0.0309    0.1802    0.2727    0.1489    0.3589    0.4208    0.1665
      0.6815    0.7970    0.6567    0.6744    0.1307    0.8009    0.1269
   

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
      0.6558    0.8483    0.1708    0.1772
   
   R2 = 
      0.9821    0.1572    0.7950    0.6328    0.6295
   
   R3 = 
      0.6558    0.8483    0.1708    0.1772    0.9821    0.1572    0.7950    0.6328    0.6295
   
   C1 = 
      0.8569
      0.2695
      0.5719
      0.1068
      0.6651
      0.0344
      0.5488
      0.0565
      0.3931
      0.3455
   
   C2 = 
      0.8597
      0.6811
      0.3256
      0.5036
      0.6735
      0.3555
      0.1932
      0.5668
      0.3889
      0.9201
   
   M = 
      0.8569    0.8597
      0.2695    0.6811
      0.5719    0.3256
      0.1068    0.5036
      0.6651    0.6735
      0.0344    0.3555
      0.5488    0.1932
      0.0565    0.5668
      0.3931    0.3889
      0.3455    0.9201
   


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
      0.8432    0.6710    0.5889    0.4196
   
   R2 = 
      0.9526    0.5315    0.3125    0.7406
   
   M = 
      0.8432    0.6710    0.5889    0.4196
      0.9526    0.5315    0.3125    0.7406
   
   C1 = 
      0.6332
      0.8421
      0.1807
      0.8097
      0.3858
      0.1826
      0.1853
      0.5840
      0.1382
      0.3119
   
   C2 = 
      0.7769
      0.5657
   
   C3 = 
      0.6332
      0.8421
      0.1807
      0.8097
      0.3858
      0.1826
      0.1853
      0.5840
      0.1382
      0.3119
      0.7769
      0.5657
   

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
   

