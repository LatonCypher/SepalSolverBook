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
      0.3330    0.9940    0.2030    0.1263    0.5269    0.9312    0.2238
   
   C = 
      0.0450
      0.1743
      0.6918
      0.7835
      0.2875
   
   M = 
      0.3174    0.5079    0.8358    0.3578    0.6520    0.3635    0.2014
      0.3015    0.8143    0.7246    0.0919    0.7606    0.8810    0.4066
      0.4105    0.0828    0.4504    0.7706    0.2282    0.7530    0.6869
      0.3690    0.3285    0.9572    0.5041    0.4267    0.2547    0.2864
      0.2783    0.7563    0.0644    0.7196    0.2130    0.9811    0.5251
      0.8705    0.6126    0.6688    0.0316    0.6762    0.3397    0.9566
      0.9459    0.8689    0.8139    0.9982    0.1603    0.5874    0.4062
      0.8359    0.0081    0.4405    0.4994    0.9170    0.2404    0.2761
   

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
      0.9760    0.9748    0.1000    0.3494
   
   R2 = 
      0.9695    0.1160    0.4864    0.5133    0.7094
   
   R3 = 
      0.9760    0.9748    0.1000    0.3494    0.9695    0.1160    0.4864    0.5133    0.7094
   
   C1 = 
      0.0654
      0.9497
      0.0487
      0.5855
      0.6437
      0.8259
      0.2573
      0.6307
      0.4128
      0.2848
   
   C2 = 
      0.2853
      0.4852
      0.7083
      0.4954
      0.9136
      0.8829
      0.5781
      0.5028
      0.8959
      0.2013
   
   M = 
      0.0654    0.2853
      0.9497    0.4852
      0.0487    0.7083
      0.5855    0.4954
      0.6437    0.9136
      0.8259    0.8829
      0.2573    0.5781
      0.6307    0.5028
      0.4128    0.8959
      0.2848    0.2013
   


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
      0.4918    0.8422    0.8026    0.0972
   
   R2 = 
      0.6822    0.9025    0.1470    0.8410
   
   M = 
      0.4918    0.8422    0.8026    0.0972
      0.6822    0.9025    0.1470    0.8410
   
   C1 = 
      0.8884
      0.7271
      0.9959
      0.4495
      0.0496
      0.4554
      0.1112
      0.2194
      0.2899
      0.7681
   
   C2 = 
      0.7724
      0.6039
   
   C3 = 
      0.8884
      0.7271
      0.9959
      0.4495
      0.0496
      0.4554
      0.1112
      0.2194
      0.2899
      0.7681
      0.7724
      0.6039
   

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
   

