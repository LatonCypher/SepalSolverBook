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
      0.8445    0.5825    0.2553    0.9472    0.0390    0.2835    0.9627
   
   C = 
      0.7168
      0.7317
      0.4558
      0.1722
      0.7729
   
   M = 
      0.1157    0.4401    0.2746    0.5035    0.4531    0.5328    0.7826
      0.9506    0.2173    0.5178    0.3944    0.6196    0.1498    0.0346
      0.7760    0.5348    0.2760    0.7487    0.8139    0.7360    0.7684
      0.5603    0.0690    0.8583    0.7317    0.2505    0.3038    0.2383
      0.0393    0.5377    0.5548    0.4780    0.3096    0.2500    0.2202
      0.4169    0.7305    0.5274    0.8673    0.4230    0.8648    0.5403
      0.0217    0.6957    0.2573    0.1342    0.8459    0.6890    0.7150
      0.1017    0.6943    0.0935    0.3109    0.2445    0.7089    0.8029
   

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
      0.5556    0.6198    0.5126    0.1924
   
   R2 = 
      0.7793    0.3815    0.5195    0.5695    0.3699
   
   R3 = 
      0.5556    0.6198    0.5126    0.1924    0.7793    0.3815    0.5195    0.5695    0.3699
   
   C1 = 
      0.1921
      0.0364
      0.3372
      0.3959
      0.4669
      0.5475
      0.7573
      0.8007
      0.1981
      0.8192
   
   C2 = 
      0.0476
      0.2542
      0.1852
      0.6748
      0.9948
      0.8710
      0.9304
      0.5882
      0.1990
      0.4792
   
   M = 
      0.1921    0.0476
      0.0364    0.2542
      0.3372    0.1852
      0.3959    0.6748
      0.4669    0.9948
      0.5475    0.8710
      0.7573    0.9304
      0.8007    0.5882
      0.1981    0.1990
      0.8192    0.4792
   


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
      0.9885    0.4360    0.1536    0.1712
   
   R2 = 
      0.6701    0.4193    0.6486    0.4063
   
   M = 
      0.9885    0.4360    0.1536    0.1712
      0.6701    0.4193    0.6486    0.4063
   
   C1 = 
      0.9985
      0.6393
      0.8697
      0.0482
      0.8314
      0.0038
      0.8716
      0.8002
      0.4454
      0.8085
   
   C2 = 
      0.5292
      0.5992
   
   C3 = 
      0.9985
      0.6393
      0.8697
      0.0482
      0.8314
      0.0038
      0.8716
      0.8002
      0.4454
      0.8085
      0.5292
      0.5992
   

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
   

