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
      0.0052    0.7309    0.5361    0.6115    0.0375    0.0374    0.0935
   
   C = 
      0.6245
      0.9443
      0.6542
      0.2545
      0.2058
   
   M = 
      0.7147    0.9312    0.8434    0.4512    0.9898    0.4819    0.5858
      0.1474    0.3587    0.4126    0.8368    0.9770    0.0551    0.7852
      0.8101    0.4785    0.2623    0.2814    0.3893    0.3886    0.2934
      0.8187    0.1623    0.0398    0.9287    0.0819    0.2721    0.7164
      0.7161    0.9240    0.2814    0.3347    0.1027    0.0152    0.8265
      0.5227    0.6465    0.7084    0.1322    0.6277    0.4080    0.6939
      0.0127    0.3664    0.1258    0.7430    0.4088    0.6231    0.6727
      0.5196    0.4561    0.3337    0.0164    0.0711    0.4954    0.7610
   

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
      0.2060    0.0980    0.2511    0.5993
   
   R2 = 
      0.3768    0.2868    0.6342    0.3621    0.0091
   
   R3 = 
      0.2060    0.0980    0.2511    0.5993    0.3768    0.2868    0.6342    0.3621    0.0091
   
   C1 = 
      0.2671
      0.5847
      0.0177
      0.6312
      0.0478
      0.8739
      0.0001
      0.2718
      0.9630
      0.0296
   
   C2 = 
      0.0356
      0.4524
      0.8959
      0.9938
      0.2742
      0.4609
      0.8954
      0.3808
      0.5503
      0.4714
   
   M = 
      0.2671    0.0356
      0.5847    0.4524
      0.0177    0.8959
      0.6312    0.9938
      0.0478    0.2742
      0.8739    0.4609
      0.0001    0.8954
      0.2718    0.3808
      0.9630    0.5503
      0.0296    0.4714
   


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
      0.6969    0.3296    0.8398    0.2735
   
   R2 = 
      0.1098    0.8963    0.2497    0.1492
   
   M = 
      0.6969    0.3296    0.8398    0.2735
      0.1098    0.8963    0.2497    0.1492
   
   C1 = 
      0.3865
      0.0841
      0.8652
      0.7027
      0.7714
      0.0915
      0.1801
      0.7609
      0.8874
      0.9288
   
   C2 = 
      0.4685
      0.0216
   
   C3 = 
      0.3865
      0.0841
      0.8652
      0.7027
      0.7714
      0.0915
      0.1801
      0.7609
      0.8874
      0.9288
      0.4685
      0.0216
   

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
   

