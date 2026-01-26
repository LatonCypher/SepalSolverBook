Nonlinear Equation
==================

Root of a nonlinear equation near initial guess :math:`x_0` can be found using ``Fzero`` or `Fsolve`. It numerically locates a value: :math:`x` such that: :math:`f(x) = 0`. This is particularly useful when analytical solutions are difficult or impossible to obtain.

To solve the equation: :math:`x\exp(x) = 2`, start with initial guess of :math:`x_0 = 0.5`;

.. code-block:: csharp

   //Single nonlinear equation
   double f(double x) => x * Exp(x) - 2;

   // solve equation using fzero
   double x = Fzero(f, 0.5);
   Console.WriteLine($"x = {x}");


Ouput

.. terminal::

   x = 0.8526055020137255
In this case, Fzero first search for an interval that brackets the root. Then uses brent's method to hone in on the root. 
If we are sure of the interval containing the root, we can save the effort spent on bracketing the root by supplying that. 

.. code-block:: csharp

   //Single nonlinear equation (bracketted)
   double f(double x) => x * Exp(x) - 2;
   double x = Fzero(f, [0.5, 1]);
   Console.WriteLine($"x = {x}");


Ouput

.. terminal::

   x = 0.8526055020137254

To have window into the solution process, we can using solver setting `SolverSet()` to get the solver to print out the result after each iteration. 


.. code-block:: csharp

   // Single nonlinear equation
   double f(double x) => x * Exp(x) - 2;

   // set solver behaviour
   var opts = SolverSet(Display: true);

   // solve equation using fzero
   double x = Fzero(f, 0.5, opts);
   Console.WriteLine($"x = {x}");


Ouput

.. terminal::

   
    Search for an interval around 0.5 containing a sign change:
   fun-count     a          f(a)           b          f(b)     Procedure 
       1         5e-1   -1.1756e+0         5e-1   -1.1756e+0   initial interval 
       3    4.7172e-1    -1.244e+0    5.2828e-1    -1.104e+0   search          
       5       4.6e-1   -1.2713e+0       5.4e-1   -1.0734e+0   search          
       7    4.4343e-1   -1.3091e+0    5.5657e-1    -1.029e+0   search          
       9       4.2e-1   -1.3608e+0       5.8e-1    -9.641e-1   search          
      11    3.8686e-1   -1.4304e+0    6.1314e-1   -8.6802e-1   search          
      13       3.4e-1   -1.5223e+0       6.6e-1   -7.2304e-1   search          
      15    2.7373e-1   -1.6401e+0    7.2627e-1   -4.9853e-1   search          
      17       1.8e-1   -1.7845e+0       8.2e-1   -1.3819e-1   search          
      19    4.7452e-2   -1.9502e+0    9.5255e-1     4.693e-1   search          
   
    Solving for solution between 0.047452 and 0.952548
   fun-count     x         f(x)       Procedure 
      19    9.5255e-1     4.693e-1    initial        
      20    7.7699e-1    -3.101e-1    interpolation  
      21    8.4684e-1   -2.4938e-2    interpolation  
      22    8.5264e-1    1.5721e-4    interpolation  
      23    8.5261e-1   -6.9891e-7    interpolation  
      24    8.5261e-1  -1.9465e-11    interpolation  
      25    8.5261e-1         0e+0    interpolation  
   x = 0.8526055020137255

by setting the solver setting in the case of bracketed root, we can see how the solution process differs from the case of a single initial guess. 


.. code-block:: csharp

   // Single nonlinear equation
   double f(double x) => x * Exp(x) - 2;

   // set solver behaviour
   var opts = SolverSet(Display: true);

   // solve equation using fzero
   double x = Fzero(f, [0.5, 1], opts);
   Console.WriteLine($"x = {x}");


Ouput

.. terminal::

   fun-count     x         f(x)       Procedure 
       2         1e+0    7.1828e-1    initial        
       3    8.1037e-1   -1.7768e-1    interpolation  
       4    8.4798e-1    -2.004e-2    interpolation  
       5    8.5263e-1    9.9913e-5    interpolation  
       6    8.5261e-1   -3.5651e-7    interpolation  
       7    8.5261e-1  -6.3105e-12    interpolation  
       8    8.5261e-1  -2.2204e-16    interpolation  
   x = 0.8526055020137254

Practical Application
---------------------
The gas compressibility factor (Z-factor) measures how much a real gas deviates from ideal gas behavior. It is defined as:

.. math::

   Z = \frac{P V}{n R T}


where:

- :math:`P` = pressure
- :math:`V` = volume
- :math:`n` = number of moles
- :math:`R` = gas constant
- :math:`T` = temperature

Accurate determination of :math:`Z` is essential in petroleum engineering for reservoir simulation, material balance, and pipeline design.
Unlike explicit correlations, which provide :math:`Z` directly as a function of pseudo-reduced pressure (:math:`P_{pr}`) and pseudo-reduced temperature (:math:`T_{pr}`), **implicit correlations** require solving an equation iteratively because :math:`Z` appears on both sides of the equation.

The **Hall–Yarbrough correlation (1973)** is one of the most widely used implicit methods for estimating Z. It was developed based on the hard-sphere equation of state and tested against multiple reservoir gas systems.
The general form is:

.. math::

   \begin{array}{c}
   A = 0.06125t \exp\left(-1.2(1 - t)^2\right) \\
   B = 14.76t - 9.76t^2 + 4.58t^3 \\
   C = 90.7t - 242.2t^2 + 42.4t^3 \\
   D = 2.18 + 2.82t \\
   -AP_{pr} + \cfrac{y + y^2 + y^3 - y^4}{(1 - y)^3} - By^2 + Cy^D = 0 \\
   Z = \cfrac{A P_{pr}}{y}
   \end{array}

where:

- :math:`P_{pr} = P/P_c` (pseudo-reduced pressure)
- :math:`T_{pr} = T/T_c` (pseudo-reduced temperature)
- :math:`t = 1/T_{pr}` 
- :math:`P_c, T_c` = pseudo-critical properties of the gas mixture

Because reduced density equation is nonlinear, iterative numerical methods such as Newton–Raphson or successive substitution are required to solve it.

**Applications**

- Reservoir engineering: material balance calculations and reserves estimation.
- Pipeline design: predicting pressure drop and flow efficiency.
- Simulation software: incorporated into PVT packages for automated Z-factor evaluation.


.. code-block:: csharp

   //Z factor application
   static double ZfactorHY(double Pr, double Tr)
   {
       double z = 1, t, tm1, tm1e2, A, B,
           C, D, r, y2, y3, y4, Den;
       if (Pr != 0)
       {
           t = 1 / Tr;
           tm1 = 1 - t; tm1e2 = tm1 * tm1;
           A = 0.06125 * t * Exp(-1.2 * Pow(1 - t, 2));
           B = t * (14.76 - t * (9.76 - t * 4.58));
           C = t * (90.7 - t * (242.2 - t * 42.4));
           D = 2.18 + 2.82 * t; r = A * Pr;
           var yfunc = new Func<double, double>(y =>
           {
               y2 = y * y; y3 = y2 * y; y4 = y3 * y;
               Den = Pow(1 - y, 3);
               return -A * Pr + (y + y2 + y3 - y4) / Den -
               B * y2 + C * Pow(y, D);
           });
           double y = Fsolve(yfunc, r);
           z = A * Pr / y;
       }
       return z;
   }

   // set up ressure and temperature mesh
   ColVec Pr = Linspace(0.2, 20, 501);
   ColVec Tr = new double[] {1.05,    1.08,   1.12,   1.18,   1.26,   1.35,   1.47,
                             1.61,    1.75,   1.91,   2.09,   2.29,   2.62,   3.00 };

   // compute z factors and plot them
   List<string> Tlabels = [];
   List<ColVec> ZHY = [];
   foreach (var tr in Tr)
   {
       ZHY.Add(Pr.Select(p => ZfactorHY(p, tr)).ToArray());
       Tlabels.Add("Tr = " + tr);
   }

   Plot(Pr, ZHY);
   SaveAs("Zfactor_Hall_Yarborough_.png");



Ouput

.. terminal::

   ⚠️ Runtime Error: rows or cols of X and Y must be equal

.. figure:: images/Zfactor_Hall_Yarborough_.png
   :align: center
   :alt: Zfactor_Hall_Yarborough_.png

