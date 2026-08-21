Curve Fitting
=============

Curve Fitting
-------------
Curve fitting is a mathematical technique used to construct a curve that best fits a series of data points. It is widely applied in data analysis, statistics, and machine learning to model relationships between variables.

Types of Curve Fitting:
~~~~~~~~~~~~~~~~~~~~~~~
1. Linear Regression: Fits a straight line to the data points.
2. Polynomial Regression: Fits a polynomial curve of degree n to the data points.
3. Nonlinear Regression: Fits a nonlinear model to the data points.

Example: Polynomial Curve Fitting
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Given a set of data points, we can fit a polynomial curve using least squares optimization.


.. math::

   \min_{\mathbf{p}} \sum_{i=1}^{n} (y_i - P(x_i; \mathbf{p}))^2



.. code-block:: csharp

   // Sample data points
   double[] xData = [1, 2, 3, 4, 5];
   double[] yData = [2.2, 3.0, 3.2, 2.5, 1.1];
   // Fit a polynomial of degree 2
   int degree = 2;
   var coefficients = Polyfit(xData, yData, degree);
   Scatter(xData, yData, "*", 15); HoldOn();
   // Generate fitted curve
   double[] xFit = Linspace(1, 5, 100);
   double[] yFit = Polyval(coefficients, xFit);
   Plot(xFit, yFit, Linewidth: 2);
   SaveAs("Polynomial_Fitting.png");
   CloseFig();


.. figure:: images/Polynomial_Fitting.png
   :align: center
   :alt: Polynomial_Fitting.png


Example: Fourier Series Fitting
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Evaluating a Fourier series numerically involves transforming an infinite 
sum of trigonometric terms into a computationally stable, finite calculation
while controlling truncation errors, floating-point precision loss, and
spectral artifacts.

Mathematical Formulation:

A truncated Fourier series approximating a periodic function :math:`f(x)` on 
the interval :math:`[-\pi, \pi]` with :math:`N` harmonics is defined as:


.. math::

   f_N(x) = \frac{a_0}{2} + \sum_{n=1}^{N} \left( a_n \cos(nx) + b_n \sin(nx) \right)


In complex exponential form, which is computationally convenient for many 
numerical implementations, the series is expressed as:


.. math::

   f_N(x) = \sum_{n=-N}^{N} c_n e^{i n x}


where the complex coefficients :math:`c_n` relate to the real coefficients via:


.. math::

   c_0 = \frac{a_0}{2}, \quad c_n = \frac{a_n - i b_n}{2}, \quad c_{-n} = \frac{a_n + i b_n}{2}


.. code-block:: csharp

   ColVec x = Linspace(-10, 10, 1001);
   ColVec Rect = Sign(Sin(x));
   Plot(x, Rect, Linewidth: 2); HoldOn();
   var fourier = Plot(x, 0 * x, "r", Linewidth: 2);
   Axis([x[0], x[^1], -1.5, 1.5]);
   
   byte[] Animfun(int N)
   {
       Matrix A = Zeros(1001, 2 * N + 3);
       A[.., 0] = Ones(1001);
       for (int n = 1; n <= (N + 1); n++)
       {
           A[.., 2 * n-1] = Cos(n * x); 
           A[.., 2 * n] = Sin(n * x);
       }
       ColVec p = Mldivide(A, Rect);
       fourier.Ydata = A * p;
       return GetFrame();
   }
   AnimationMaker(Animfun, "FourierFitting.gif", 5, 100);
   CloseFig();


.. figure:: images/FourierFitting.gif
   :align: center
   :alt: FourierFitting.gif


Example: Bi-Exponential Curve Fitting
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
This exercise covers non-linear parameter estimation using least-squares optimization 
to fit a bi-exponential model to noisy data while visualizing optimizer convergence.

The objective is to fit data points :math:`(x_d, y_d)` to a bi - exponential model:

.. math::

   f(x; \theta) = \theta_2 e^{\theta_0 x} + \theta_3 e^{\theta_1 x}

where  math:`\theta = [\theta_0, \theta_1, \theta_2, \theta_3]^T` represents the unknown parameters.

Find :math:`\hat{\theta}` minimizing the sum of squared residuals:


.. math::

   \hat{\theta} = \arg\min_{\theta} \sum_{d=1}^D (y_d - f(x_d; \theta))^2



.. code-block:: csharp

   ColVec noise, weight = new double[100]; double[] x0;
   static ColVec fun(ColVec x, ColVec xdata) => x[2] * Exp(x[0] * xdata) + x[3] * Exp(x[1] * xdata);
   ColVec xdata = Linspace(0, 1); noise = Rand(xdata.Numel);
   ColVec ydata = fun(x0 = [-4, -5, 4, -4], xdata) + 0.02 * noise;
   x0 = [-1, -2, 1, -1]; weight[xdata < 0.5] = 1;
   var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
   var ans = Lsqcurvefit(fun, x0, xdata, ydata, options: opts);
   AnimateHistory(fun, xdata, ydata, ans.history, "Bi_Exponential_Fitting.gif");
   CloseFig();


Ouput

.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            5          1.7368e0                       3.6200e0 
        1           11         7.9713e-1     2.5580e-1         1.4818e0 
        2           17         5.2413e-1     2.3709e-1        2.9803e-1 
        3           23         4.4646e-1     5.2349e-1        1.4419e-1 
        4           29         2.6633e-1      2.0265e0        6.9127e-1 
        5           35         1.1072e-1      3.0131e0        7.2001e-1 
        6           41         5.7420e-3     5.5217e-1        5.6635e-2 
        7           48         4.1466e-3     2.1531e-1        3.8427e-2 
        8           55         3.8337e-3     1.6726e-1        2.1451e-2 
        9           62         3.6610e-3     1.2401e-1        1.2589e-2 
       10           68         3.6307e-3     2.7089e-1        5.8267e-2 
       11           75         3.3203e-3     1.4668e-1        1.9637e-2 
       12           82         3.2377e-3     1.1386e-1        1.1932e-2 
       13           89         3.1882e-3     8.9374e-2        7.4902e-3 
       14           96         3.1551e-3     7.3474e-2        5.1227e-3 
       15          102         3.1475e-3     1.7231e-1        2.7680e-2 
       16          109         3.0762e-3     1.1432e-1        1.1960e-2 
       17          116         3.0517e-3     8.9314e-2        7.2832e-3 
       18          123         3.0372e-3     7.2331e-2        4.7347e-3 
       19          130         3.0275e-3     6.0531e-2        3.2864e-3 
       20          137         3.0204e-3     5.1860e-2        2.3910e-3 
       21          143         3.0191e-3     1.2356e-1        1.3411e-2 
       22          150         3.0031e-3     8.8654e-2        6.4923e-3 
       23          157         2.9971e-3     6.9414e-2        3.9261e-3 
       24          164         2.9936e-3     5.6216e-2        2.5339e-3 
       25          171         2.9914e-3     4.6722e-2        1.7258e-3 
       26          178         2.9899e-3     3.9586e-2        1.2237e-3 
       27          184         2.9897e-3     8.7998e-2        6.0031e-3 
       28          191         2.9866e-3     6.1141e-2        2.7371e-3 
       29          198         2.9856e-3     4.5054e-2        1.4695e-3 
       30          205         2.9852e-3     3.4178e-2        8.3631e-4 
       31          211         2.9851e-3     5.8739e-2        2.4614e-3 
       32          217         2.9848e-3     5.4227e-2        2.0305e-3 
       33          223         2.9845e-3     2.1612e-2        3.1000e-4 
       34          229         2.9845e-3     2.8528e-3        5.3087e-6 

.. figure:: images/Bi_Exponential_Fitting.gif
   :align: center
   :alt: Bi_Exponential_Fitting.gif



.. code-block:: csharp

   ColVec xdata, ydata, times, y_est, filltime, sgy, filly, lower, upper;

   double[] x_dat = [0.9, 1.5, 13.8, 19.8, 24.1, 28.2, 35.2, 60.3, 74.6, 81.3];
   double[] y_dat = [455.2, 428.6, 124.1, 67.3, 43.2, 28.1, 13.1, -0.4, -1.3, -1.5];
   xdata = x_dat; ydata = y_dat; times = Linspace(x_dat[0], x_dat[9]);
   double[] x0 = [100, -1];

   static ColVec fun(ColVec x, ColVec xdata) => x[0] * Exp(x[1] * xdata);
   var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
   var ans = Lsqcurvefit(fun, x0, xdata, ydata, options: opts);

   Scatter(xdata, ydata); HoldOn();
   Plot(times, y_est = fun(ans.x, times), "r", Linewidth: 2);
   filltime = Vcart(times, times.Reverse().ToList());
   sgy = Interp1(xdata, ans.sigma_y, times);
   lower = y_est - 20 * sgy; upper = y_est + 20 * sgy;
   filly = Vcart(lower, upper.Reverse().ToList());
   Fill(filltime, filly, "g", 0.2); HoldOff();
   Axis([xdata.Min()-0.01*xdata.Range(), xdata.Max()+0.01*xdata.Range(),
   ydata.Min()-0.1*ydata.Range(), ydata.Max()+0.1*ydata.Range()]);
   SaveAs("CurveFitting.png");
   AnimateHistory(fun, xdata, ydata, ans.history, "CurveFitting.gif");
   CloseFig();


Ouput

.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            3          3.5968e5                       2.8768e4 
        1            7          2.9148e5      4.5301e1         6.3631e4 
        2           11          1.4328e5      7.0536e1         1.8724e5 
        3           15          5.8838e4      8.1015e1         1.7583e5 
        4           19          2.1604e4      7.9171e1         1.3573e5 
        5           23          2.4371e3      8.1537e1         4.6492e4 
        6           27          6.2429e1      3.5477e1         8.8212e3 
        7           31          9.6405e0      5.5200e0         5.2344e2 
        8           35          9.5049e0     2.7383e-1         4.5771e0 
        9           39          9.5049e0     3.5902e-3        1.3319e-2 
       10           43          9.5049e0     9.0844e-6        5.6927e-6 

.. figure:: images/CurveFitting.png
   :align: center
   :alt: CurveFitting.png


.. figure:: images/CurveFitting.gif
   :align: center
   :alt: CurveFitting.gif


Lsqcurvefit allows the use of constraints. 
1. Seed data for reproducability

.. code-block:: csharp

   int seed = 23;
   Random rng = new(seed);
   ColVec xdata, ydata, noise = Randn(100);
   double[] xstar = [2, 4, 5, 0.5];

   ColVec model(ColVec x, ColVec xdata) => x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;
   xdata = Linspace(2,7); ydata = model(xstar, xdata) + noise/10;
   Scatter(xdata, ydata, "ro");

   Xlabel("x"); Ylabel("y"); 
   SaveAs("Seeded_Curve_Fitting_Data.png");
   CloseFig();


.. figure:: images/Seeded_Curve_Fitting_Data.png
   :align: center
   :alt: Seeded_Curve_Fitting_Data.png


2. Fitting with Linear constraint

.. code-block:: csharp

   int seed = 23;
   Random rng = new(seed);
   ColVec xdata, ydata, noise = Randn(100);
   double[] xstar = [2, 4, 5, 0.5], startpt = [1, 2, 3, 1];

   ColVec model(ColVec x, ColVec xdata) => x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;
   RowVec A = new double[] { -1, -1, 1, 1 };
   ColVec fineq(ColVec x) => A * x; ColVec lb = Zeros(4), ub = 7 + lb;
   xdata = Linspace(2, 7); ydata = model(xstar, xdata) + noise / 10;

   var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
   var ans = Lsqcurvefit(model, startpt, xdata, ydata, fineq, null, lb, ub, options: opts);
   Console.WriteLine($"x = {ans.x.T}");
   Console.WriteLine($"c = {fineq(ans.x)}");

   Scatter(xdata, ydata, "ro"); HoldOn();
   Plot(xdata, ans.y_hat, "-b", Linewidth: 2);

   Xlabel("x"); Ylabel("y");
   Legend(["Measured Data", "Model Estimate"], UpperRight);
   SaveAs("Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png");
   CloseFig();


Ouput

.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            5          1.5713e3                       1.5087e3 
        1           11          1.2924e3     2.0954e-1         1.3102e3 
        2           17          7.9219e2     4.8272e-1         8.8642e2 
        3           23          3.0005e2     8.0436e-1         3.3599e2 
        4           29          6.0089e1     9.6560e-1         6.8985e1 
        5           35          1.0357e1     6.5214e-1         3.4681e1 
        6           41          5.5815e0     3.8608e-1         2.2564e0 
        7           47          2.7402e0     5.3571e-1         3.3465e0 
        8           53          2.5560e0     5.5824e-1         1.7663e0 
        9           59         9.7977e-1     2.7933e-1        3.2610e-1 
       10           65         9.7517e-1     5.6085e-3        2.8506e-2 
       11           71         9.7374e-1     6.1200e-2        1.1985e-2 
       12           77         9.7372e-1     7.8489e-3        3.3835e-4 
       13           83         9.7372e-1     3.2608e-4        3.7055e-6 
       14           89         9.7372e-1     4.2641e-6        2.1004e-8 
   x = 
      1.7277    3.9102    4.9845    0.5515
   
   c =   -0.1020

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png


3. Fitting with nonlinear constraint.

.. code-block:: csharp

   int seed = 23;
   Random rng = new(seed);
   ColVec xdata, ydata, noise = Randn(100);
   double[] xstar = [2, 4, 5, 0.5], startpt = [1, 2, 3, 1];

   ColVec model(ColVec x, ColVec xdata) => x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;
   ColVec fineq(ColVec x) => x[0] * x[0] + x[1] * x[1] - 16; ColVec lb = Zeros(4), ub = 7 + lb;
   xdata = Linspace(2, 7); ydata = model(xstar, xdata) + noise / 10;

   var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
   var ans = Lsqcurvefit(model, startpt, xdata, ydata, fineq, null, lb, ub, options: opts);
   Console.WriteLine($"x = {ans.x.T}");
   Console.WriteLine($"c = {fineq(ans.x)}");

   Scatter(xdata, ydata, "ro"); HoldOn();
   Plot(xdata, ans.y_hat, "-b", Linewidth: 2);

   Xlabel("x"); Ylabel("y");
   Legend(["Measured Data", "Model Estimate"], UpperRight);
   SaveAs("Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png");
   CloseFig();


Ouput

.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            5          1.5836e3                       1.5212e3 
        1           11          1.2982e3     2.1344e-1         1.3192e3 
        2           17          7.8713e2     4.9215e-1         8.8827e2 
        3           23          2.8785e2     8.2145e-1         3.2931e2 
        4           29          5.3228e1     9.6889e-1         6.9212e1 
        5           35          5.1247e0     6.2439e-1         3.6865e1 
        6           41          2.4689e0     2.0170e-1         4.0644e0 
        7           47          2.1992e0     1.8298e-1         1.2368e0 
        8           53          1.7164e0     4.1246e-1         1.5892e0 
        9           59          1.1202e0     7.3234e-1         1.6918e0 
       10           69          1.1065e0     2.2813e-2        7.1045e-1 
       11           78          1.1052e0     2.3048e-3        6.9757e-1 
       12           85          1.1039e0     2.2950e-3        6.9663e-1 
       13           93          1.1036e0     7.2438e-4        6.9682e-1 
       14           99          1.1034e0     3.9560e-4        4.5593e-1 
       15          105          1.1029e0     8.7789e-4        3.2514e-1 
       16          111          1.1026e0     1.0160e-3        2.8527e-1 
       17          117          1.1026e0     4.5853e-4        2.8832e-1 
       18          123          1.1026e0     7.3571e-5        2.9002e-1 
       19          129          1.1025e0     1.2549e-4        2.9032e-1 
       20          135          1.1024e0     3.7405e-4        2.9185e-1 
       21          141          1.1020e0     9.9171e-4        3.0195e-1 
       22          147          1.1019e0     3.2559e-4        3.0711e-1 
       23          153          1.1017e0     4.9170e-4        3.1641e-1 
       24          159          1.1015e0     1.1437e-3        3.4440e-1 
       25          165          1.1012e0     1.6918e-3        3.9860e-1 
       26          171          1.1011e0     1.1610e-3        4.4214e-1 
       27          177          1.1011e0     2.9332e-4        4.5380e-1 
       28          183          1.1011e0     2.3766e-5        4.5476e-1 
       29          189          1.1011e0     8.1426e-7        4.5479e-1 
   x = 
      1.3716    3.7575    5.0177    0.6314
   
   c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


