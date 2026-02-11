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
      0.9966    0.7756    0.8931    0.0677    0.3863    0.3366    0.7077
   
   C = 
      0.4419
      0.5813
      0.1861
      0.9369
      0.4908
   
   M = 
      0.2772    0.2195    0.9467    0.8895    0.6208    0.4827    0.5822
      0.2864    0.4500    0.8536    0.3431    0.6314    0.2697    0.1185
      0.2805    0.9967    0.2598    0.5764    0.2869    0.7139    0.8984
      0.3394    0.9857    0.5884    0.8471    0.5394    0.4590    0.5440
      0.2533    0.3987    0.7248    0.1380    0.8738    0.8940    0.5260
      0.5196    0.1891    0.3383    0.0786    0.4196    0.1344    0.3823
      0.0796    0.2759    0.8288    0.1455    0.9422    0.8532    0.7249
      0.2306    0.3963    0.3017    0.1269    0.2143    0.8971    0.5303
   

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
      0.9276    0.3790    0.2707    0.7786
   
   R2 = 
      0.4692    0.1098    0.8144    0.5281    0.7409
   
   R3 = 
      0.9276    0.3790    0.2707    0.7786    0.4692    0.1098    0.8144    0.5281    0.7409
   
   C1 = 
      0.7036
      0.7732
      0.3838
      0.9565
      0.7536
      0.8082
      0.0967
      0.6945
      0.7893
      0.5354
   
   C2 = 
      0.4262
      0.3408
      0.8103
      0.4402
      0.7610
      0.0873
      0.9499
      0.4731
      0.8588
      0.8711
   
   M = 
      0.7036    0.4262
      0.7732    0.3408
      0.3838    0.8103
      0.9565    0.4402
      0.7536    0.7610
      0.8082    0.0873
      0.0967    0.9499
      0.6945    0.4731
      0.7893    0.8588
      0.5354    0.8711
   


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
      0.5499    0.0611    0.4266    0.9262
   
   R2 = 
      0.6909    0.6263    0.4538    0.2219
   
   M = 
      0.5499    0.0611    0.4266    0.9262
      0.6909    0.6263    0.4538    0.2219
   
   C1 = 
      0.4464
      0.5739
      0.1041
      0.8982
      0.4900
      0.0445
      0.2117
      0.3036
      0.1554
      0.3960
   
   C2 = 
      0.8742
      0.7251
   
   C3 = 
      0.4464
      0.5739
      0.1041
      0.8982
      0.4900
      0.0445
      0.2117
      0.3036
      0.1554
      0.3960
      0.8742
      0.7251
   

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
   

