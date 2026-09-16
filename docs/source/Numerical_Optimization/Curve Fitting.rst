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
        0            5          1.7139e0                       3.5860e0 
        1           11         7.8661e-1     2.5435e-1         1.4713e0 
        2           17         5.1704e-1     2.3519e-1        2.9641e-1 
        3           23         4.4037e-1     5.1889e-1        1.4225e-1 
        4           29         2.6224e-1      1.9974e0        6.8301e-1 
        5           35         1.0904e-1      2.9868e0        7.2217e-1 
        6           41         5.2840e-3     4.2218e-1        8.9194e-2 
        7           47         4.3705e-3     4.4232e-1        1.2668e-1 
        8           53         3.9387e-3     3.7549e-1        1.1080e-1 
        9           59         3.3360e-3     2.6699e-1        6.2987e-2 
       10           65         3.0860e-3     1.4179e-1        1.8380e-2 
       11           71         3.0631e-3     4.2659e-2        1.6634e-3 
       12           77         3.0629e-3     5.1624e-3        2.4430e-5 
       13           83         3.0629e-3     1.9514e-4        3.4732e-8 

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
       10           43          9.5049e0     9.0844e-6        5.6776e-6 

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
        0            5          1.5949e3                       1.5198e3 
        1           11          1.3121e3     2.1104e-1         1.3197e3 
        2           17          8.0465e2     4.8628e-1         8.9286e2 
        3           23          3.0504e2     8.1122e-1         3.3803e2 
        4           29          6.1327e1     9.7482e-1         6.9281e1 
        5           35          1.0691e1     6.6042e-1         3.4818e1 
        6           41          5.7741e0     3.9435e-1         2.0841e0 
        7           47          2.7673e0     5.5050e-1         3.5557e0 
        8           53          2.0858e0     5.7432e-1         1.8943e0 
        9           59         9.0552e-1     2.4180e-1        3.4834e-1 
       10           65         8.3682e-1     3.5090e-1        3.0548e-1 
       11           71         8.3026e-1     1.3066e-1        3.6892e-2 
       12           77         8.3017e-1     1.6788e-2        9.0461e-4 
       13           83         8.3017e-1     7.0228e-4        7.7743e-6 
       14           89         8.3017e-1     9.4159e-6        3.3970e-8 
   x = 
      2.1252    4.0644    4.9875    0.4669
   
   c =   -0.7352

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
        0            5          1.5740e3                       1.5096e3 
        1           11          1.2915e3     2.1239e-1         1.3090e3 
        2           17          7.8523e2     4.9001e-1         8.8133e2 
        3           23          2.8811e2     8.1935e-1         3.2653e2 
        4           29          5.3161e1     9.6867e-1         6.9606e1 
        5           35          5.0460e0     6.2214e-1         3.3317e1 
        6           41          2.5279e0     1.9153e-1         3.9887e0 
        7           47          2.3233e0     1.5764e-1         1.0321e0 
        8           53          1.9729e0     3.5153e-1         1.3075e0 
        9           59          1.5366e0     6.2721e-1         1.3085e0 
       10           67          1.4638e0     1.5983e-1        5.2920e-1 
       11           76          1.4575e0     1.5614e-2        4.9315e-1 
       12           83          1.4516e0     1.5212e-2        4.8278e-1 
       13           91          1.4497e0     4.7730e-3        4.7894e-1 
       14           99          1.4492e0     1.5057e-3        4.7777e-1 
       15          106          1.4486e0     1.5018e-3        4.7672e-1 
       16          114          1.4484e0     4.7453e-4        4.7637e-1 
       17          121          1.4483e0     4.7415e-4        4.7603e-1 
       18          127          1.4483e0     2.8039e-4        3.0939e-1 
       19          133          1.4480e0     6.2443e-4        2.2028e-1 
       20          139          1.4478e0     7.0779e-4        1.9101e-1 
       21          145          1.4478e0     3.1605e-4        1.9296e-1 
       22          151          1.4478e0     5.0482e-5        1.9417e-1 
       23          157          1.4478e0     8.6290e-5        1.9449e-1 
       24          163          1.4477e0     2.5700e-4        1.9585e-1 
       25          169          1.4475e0     6.7972e-4        2.0352e-1 
       26          175          1.4475e0     2.2304e-4        2.0727e-1 
       27          181          1.4474e0     3.3630e-4        2.1390e-1 
       28          187          1.4473e0     7.7909e-4        2.3342e-1 
       29          193          1.4472e0     1.1431e-3        2.7020e-1 
       30          199          1.4472e0     7.7547e-4        2.9912e-1 
       31          205          1.4472e0     1.9362e-4        3.0675e-1 
       32          211          1.4472e0     1.5601e-5        3.0737e-1 
   x = 
      1.3373    3.7698    5.0000    0.6366
   
   c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


