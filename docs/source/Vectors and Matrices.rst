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
   5	6	7	1
   
   C = 
   8
   3
   4
   2
   7
   
   M = 
   5	-2	3	7
   2	1	-7	3
   4	8	9	1
   0	5	-6	-3
   


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
      0.8955    0.1359    0.3599    0.8706    0.1392    0.1402    0.8446
   
   C = 
      0.6314
      0.9723
      0.1729
      0.4738
      0.4924
   
   M = 
      0.9556    0.3399    0.1373    0.6958    0.2774    0.2819    0.5693
      0.3513    0.5269    0.2877    0.7255    0.5266    0.2241    0.4014
      0.6479    0.4951    0.7197    0.2476    0.0130    0.2243    0.3215
      0.8658    0.8900    0.1246    0.9911    0.9115    0.0682    0.5518
      0.1759    0.6709    0.5476    0.2619    0.5236    0.5186    0.3331
      0.3134    0.9989    0.6836    0.3895    0.1134    0.6121    0.4374
      0.4744    0.0492    0.0252    0.8795    0.6110    0.8167    0.2383
      0.9167    0.2690    0.8183    0.4480    0.6685    0.5196    0.5996
   

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
   0	0	0	0	0	0	0
   
   C = 
   1
   1
   1
   1
   1
   
   M = 
   1	0	0	0	0	0	0
   0	1	0	0	0	0	0
   0	0	1	0	0	0	0
   0	0	0	1	0	0	0
   0	0	0	0	1	0	0
   0	0	0	0	0	1	0
   0	0	0	0	0	0	1
   

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
      0.8546    0.5551    0.3941    0.7450
   
   R2 = 
      0.1031    0.5990    0.8938    0.1202    0.6244
   
   R3 = 
      0.8546    0.5551    0.3941    0.7450    0.1031    0.5990    0.8938    0.1202    0.6244
   
   C1 = 
      0.1701
      0.9813
      0.4980
      0.4554
      0.6827
      0.0376
      0.9244
      0.9368
      0.5406
      0.7626
   
   C2 = 
      0.9823
      0.8355
      0.1166
      0.8857
      0.7675
      0.5607
      0.4533
      0.6580
      0.5430
      0.7251
   
   M = 
      0.1701    0.9823
      0.9813    0.8355
      0.4980    0.1166
      0.4554    0.8857
      0.6827    0.7675
      0.0376    0.5607
      0.9244    0.4533
      0.9368    0.6580
      0.5406    0.5430
      0.7626    0.7251
   


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
      0.7230    0.8539    0.1409    0.5254
   
   R2 = 
      0.7960    0.5312    0.5506    0.4368
   
   M = 
      0.7230    0.8539    0.1409    0.5254
      0.7960    0.5312    0.5506    0.4368
   
   C1 = 
      0.7025
      0.3127
      0.5257
      0.4808
      0.0518
      0.1808
      0.9480
      0.3134
      0.6663
      0.9384
   
   C2 = 
      0.5680
      0.3835
   
   C3 = 
      0.7025
      0.3127
      0.5257
      0.4808
      0.0518
      0.1808
      0.9480
      0.3134
      0.6663
      0.9384
      0.5680
      0.3835
   

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
   5	-2	3	7
   2	1	-7	3
   4	8	9	1
   0	5	-6	-3
   
   Flipud(M) = 
   0	5	-6	-3
   4	8	9	1
   2	1	-7	3
   5	-2	3	7
   
   Fliplr(M) = 
   7	3	-2	5
   3	-7	1	2
   1	9	8	4
   -3	-6	5	0
   

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
   5	-2	3	7
   0	1	-7	3
   0	0	9	1
   0	0	0	-3
   
   Tril(M) = 
   5	0	0	0
   2	1	0	0
   4	8	9	0
   0	5	-6	-3
   

