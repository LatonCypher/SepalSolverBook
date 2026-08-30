Polynomial Fitting
==================

Polynomial Fitting (Polyfit)
----------------------------
In engineering and data science, we often encounter discrete data points that represent a physical process. Polynomial Fitting is the mathematical technique used to find a continuous function—specifically a polynomial—that minimizes the discrepancy between the curve and the observed data. In SepalSolver, we use the Least Squares approach to determine these coefficients. 

1. Mathematical Objective
~~~~~~~~~~~~~~~~~~~~~~~~~
The goal of ``Polyfit`` is to find a set of coefficients for a polynomial of degree :math:`N`:, :math:`P(x) = a_0 x^N + a_1 x^{N-1} + \dots + a_N` such that the sum of the squares of the residuals (the vertical distance between the data points and the curve) is minimized. 


2. Coefficient Order
~~~~~~~~~~~~~~~~~~~~
Note that while the internal math often builds from the constant term up, SepalSolver returns the resulting `double[] array` in descending order. This means the first element in the array is the coefficient for the highest power :math:`x^N`. 

Examples
--------


.. Admonition:: Example 1 :  Fitting a Perfect Quadratic 

   If our data follows a perfect square relationship, such as :math:`y = x^2`, we expect `Polyfit` to return coefficients that reflect :math:`1x^2 + 0x + 0`. In this example, we fit a degree :math:`N=2` polynomial to a set of coordinates. 
   
   
   .. code-block:: csharp
    
      // Define data points
      double[] X = [1, 2, 3, 4]; 
      double[] Y = [1, 4, 9, 16]; 
      int N = 2;
   
      // Perform the fit
      double[] coefficients = Polyfit(X, Y, N);
   
      // Output: 1, 0, 0 (representing 1x^2 + 0x + 0)
      Console.WriteLine($"Coefficients: [{string.Join(", ", coefficients.Select(n => $"{n:F4}"))}]");
   
      // Plotting the results
      Scatter(X, Y, "fob", 15); HoldOn();
      double[] x = Linspace(1, 4);
      double[] y = [..x.Select(x => Polyval(coefficients, x))];
      Plot(x, y, "r", Linewidth: 2); HoldOff();
      SaveAs("Polyfit_Example1.png");
   
   
   Ouput
   
   .. terminal::
   
      Coefficients: [1.0000, 0.0000, 0.0000]
   
   .. figure:: images/Polyfit_Example1.png
      :align: center
      :alt: Polyfit_Example1.png
   


.. Admonition:: Example 2 :  Linear Regression from Sensor Data

   Imagine you are testing a linear spring. You record the force :math:`Y` applied at various displacements :math:`X`. By fitting a degree :math:`N=1` polynomial, you can find the spring constant :math:`k`, which corresponds to the first coefficient in the returned array.
   
   .. code-block:: csharp
    
      double[] X = [0.1, 0.2, 0.3, 0.4];     // Displacement
      double[] Y = [10.2, 20.1, 29.9, 40.2]; // Force
   
      // Fit a line: P(x) = a0*x + a1
      double[] p = Polyfit(X, Y, 1);
   
      Console.WriteLine($"Spring Constant k: {p[0]} N/m");
    
   
   Ouput
   
   .. terminal::
   
      Spring Constant k: 99.80000000000003 N/m


.. Admonition:: Example 3 :  Handling Noise in Experimental Data 

   Real-world data is rarely perfect. When data points are "noisy," fitting a lower-degree polynomial acts as a filter, capturing the general trend without being distracted by individual measurement errors. 
   
   .. code-block:: csharp
    
      double[] X = [1, 2, 3, 4, 5]; 
      double[] Y = [2.1, 3.9, 6.2, 8.1, 9.8]; // Roughly y = 2x
   
      // Even if we fit a degree 2, the leading coefficient should be near zero
      double[] p = Polyfit(X, Y, 2);
   
      Console.WriteLine($"Quadratic term (should be small): {p[0]}");
   
   
   Ouput
   
   .. terminal::
   
      Quadratic term (should be small): -0.04285714285714191

Usage Warning
~~~~~~~~~~~~~
Ensure that the length of arrays :math:`X` and :math:`Y` are identical. Additionally, to find a unique solution for a degree :math:`N` polynomial, you must provide at least :math:`N+1` data points. Providing fewer points will result in an underdetermined system and numerical instability. 
It is also important to choose the correct degree of the polynomial to get a good fit. 
Overfitting can occur if the degree is too high, while underfitting can happen if the degree is too low.


.. Admonition:: Example 4 :  Underfitting Problem

   In this example, we compare a linear and quaradtic fit for the same data. showing the importance of choosing the right degree for the fitting. 
   
   .. code-block:: csharp
   
      // Example of polynomial fitting
      double[] x = [1, 2, 3, 4, 5], y = [6, 9, 14, 21, 30];
      Scatter(x, y, "fob"); HoldOn();
   
      double[] xp = Linspace(1, 5);
      double[] fit1 = Polyfit(x, y, 1);
      double[] yp1 = [.. xp.Select(x => Polyval(fit1, x))];
      Plot(xp, yp1, "r");
      Console.WriteLine($"""
          Linear fit : [{string.Join(", ", fit1.Select(p=>$"{p:f4}"))}] 
          Residual: {yp1.Zip(y, (l, m) => Pow(l-m, 2)).Sum()}
          """);
   
      double[] fit2 = Polyfit(x, y, 2);
      double[] yp2 = [.. xp.Select(x => Polyval(fit2, x))];
      Plot(xp, yp2, "g");
      Console.WriteLine($"""
          Quaratic fit : [{string.Join(",", fit2.Select(p => $"{p:f4}"))}] 
          Residual: {yp2.Zip(y, (q, m) => Pow(q-m, 2)).Sum()}
          """);
   
      Legend(["Data", "Linear Fit", "Quadratic Fit"]);
      HoldOff();
      SaveAs("Polyfit_Example_4.png");
   
   
   Ouput
   
   .. terminal::
   
      Linear fit : [6.0000, -2.0000] 
      Residual: 1008.4903581267214
      Quaratic fit : [1.0000,-0.0000,5.0000] 
      Residual: 846.5558445323921
   
   .. figure:: images/Polyfit_Example_4.png
      :align: center
      :alt: Polyfit_Example_4.png
   


Multivariate Polynomial Fitting
-------------------------------

In many engineering scenarios, a dependent variable :math:`z` is influenced by multiple independent variables (e.g., :math:`x` and :math:`y`). 
This is known as Multivariate Fitting or Surface Fitting. While a standard Polyfit handles a single line, a multivariate fit finds a surface 
that minimizes the residuals across multiple dimensions.

1. The Mathematical Model
~~~~~~~~~~~~~~~~~~~~~~~~~
For two variables :math:`x` and :math:`y`, a second-order multivariate polynomial takes the form: :math:`z(x, y) = a_0 + a_1x + a_2y + a_3x^2 + a_4xy + a_5y^2`.
The "cross term" :math:`xy` is vital because it accounts for the interaction between the two variables—how the influence of :math:`x` might change depending on 
the current value of :math:`y`.

2. Implementation Logic
~~~~~~~~~~~~~~~~~~~~~~~
In SepalSolver, multivariate fitting is performed by constructing an augmented matrix where each column represents a term in the polynomial expansion 
(1, :math:`x`, :math:`y`, :math:`x^2`, etc.) and solving the resulting linear system.


.. code-block:: csharp
 
   // Data points: (x, y) coordinates and observed z values
   double[] x = [0, 1, 2, 0, 1, 2], y = [0, 0, 0, 1, 1, 1], z = [1.1, 2.0, 3.9, 2.2, 3.1, 5.1];

   // Fitting for a plane: z = a + bx + cy
   // We construct a matrix A where rows are [1, x_i, y_i]
   List<ColVec> A = [Ones(x.Length), x, y];

   // Solve for coefficients using the Normal Equation (Least Squares)
   // A' * A * coeff = A' * z
   var coeff = Mldivide(A, z);

   Console.WriteLine($"Model: z = {coeff[0]:f4} + {coeff[1]:f4}x + {coeff[2]:f4}y");


Ouput

.. terminal::

   Model: z = 0.9083 + 1.4250x + 1.1333y

Examples
--------



.. Admonition:: Example 1 : 

   The Antoine equation relates the vapor pressure of a substance to its temperature:
   
   .. math::
   
      \log_{10}(p) = A - \frac{B}{C + T}
   
   
   Where:
   | :math:`p` = vapor pressure
   | :math:`T` = temperature
   | :math:`A, B, C` = substance-specific constants
   
   In other to estimate the values of this constants, we express the equation in a linear form
   
   Step 1: Multiplying Both Sides by :math:`(C + T)`
   
   .. math::
   
      (C + T)\log_{10}(p) = A(C + T) - B
   
   Step 2: Expanding Both Sides
   
   .. math::
   
      C\log_{10}(p) + T\log_{10}(p) = AC + AT - B
   
   
   Step 3: Rearranging the terms:
   
   
   .. math::
   
      T\log_{10}(p) = A T - C\log_{10}(p) + (AC - B)
   
   
   This is a linear equation in terms of :math:`T` and :math:`\log10(p)`, which can be used to estimate the constants :math:`A`, :math:`B`, and :math:`C` 
   via multiple linear regression.
   
   - Slope with respect to :math:`T \to A`
   - Slope with respect to :math:`\log10(p) \to -C`
   - Intercept  :math:`\to AC - B`
   
   
   :math:`A`, :math:`B`, and :math:`C` can then be solved systematically from regression
   results.
   
   To estimate the parameters :math:`A`, :math:`B`, and :math:`C`
   
   .. math::
   
      T\log_{10}(p) = \alpha T + \beta C\log_{10}(p) + \gamma
   
   
   Hence:
   - :math:`A = \alpha`
   - :math:`C = -\beta`
   - :math:`B = AC-\gamma`
   
   
   .. code-block:: csharp
   
      // Curve Fitting for Antoine Equation [lnP = A - B/(T + C)]
      double[] P_Kpa = [3.18, 5.48, 9.45, 16.9, 28.2, 41.9, 66.6, 89.5, 129, 187],
               T_C = [-18.5, -9.5, 0.2, 11.8, 23.1, 32.7, 44.4, 52.1, 63.3, 75.5];
   
      // Convert Temperature to Kelvin, Compute the natural logarithm of Pressure
      ColVec T = (ColVec)T_C + 273.15, LogP = Log10((ColVec)P_Kpa);
      ColVec TLogP = T.Times(LogP), One = Ones(T_C.Length);
   
      // Multilinear fit
      List<ColVec> M = [T, LogP, One];
      var x = Mldivide(M, TLogP);
   
      // Extract fitted parameters
      double A = x[0], C = -x[1], B = A*C-x[2];
      Console.WriteLine($"Fitted Parameters: A = {A:F4}, B = {B:F4}, C = {C:F4}");
   
   
   Ouput
   
   .. terminal::
   
      Fitted Parameters: A = 6.1681, B = 1170.7321, C = -48.0075


.. Admonition:: Example 2 :  

   Material Stress Analysis The stress(:math:`\sigma`) on a component might depend on both Temperature(:math:`T`) and Pressure(:math:`P`). 
   A multivariate fit allows you to create a "Stress Surface" that can predict failure points at any combination of: math:`T` and :math:`P`. 
   


.. Admonition:: Example 3 :  

   Aero-Efficiency Mapping For a wing, the Lift Coefficient(:math:`C_L`) is a function of both the Angle of Attack(:math:`\alpha`) 
   and the Mach Number(:math:`M`). Using multivariate fitting, flight computers can interpolate lift values instantly across the entire flight envelope.
