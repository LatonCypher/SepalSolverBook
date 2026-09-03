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
        0            5          1.7310e0                       3.5356e0 
        1           11         7.9746e-1     2.5656e-1         1.4686e0 
        2           17         5.2560e-1     2.3675e-1        2.9659e-1 
        3           23         4.4878e-1     5.1962e-1        1.4227e-1 
        4           29         2.6896e-1      2.0129e0        6.8356e-1 
        5           35         1.1448e-1      3.0428e0        7.2885e-1 
        6           41         6.3035e-3     4.8159e-1        8.4627e-2 
        7           47         5.9932e-3     5.0998e-1        1.7185e-1 
        8           53         5.3652e-3     4.3261e-1        1.5410e-1 
        9           59         4.5196e-3     3.5154e-1        1.1173e-1 
       10           65         3.8547e-3     2.4460e-1        5.4115e-2 
       11           71         3.6664e-3     1.2121e-1        1.2758e-2 
       12           77         3.6554e-3     3.1117e-2        8.2004e-4 
       13           83         3.6554e-3     2.9585e-3        7.5267e-6 
       14           89         3.6554e-3     8.4661e-5        6.2245e-9 

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
        0            5          1.5792e3                       1.5083e3 
        1           11          1.3000e3     2.0968e-1         1.3097e3 
        2           17          7.9927e2     4.8318e-1         8.8583e2 
        3           23          3.0563e2     8.0618e-1         3.3547e2 
        4           29          6.2190e1     9.7198e-1         6.9528e1 
        5           35          1.0950e1     6.6428e-1         3.8053e1 
        6           41          5.7135e0     4.0424e-1         2.4946e0 
        7           47          2.7123e0     5.5155e-1         3.3901e0 
        8           53          2.1545e0     5.7089e-1         1.8840e0 
        9           59         8.7099e-1     2.5193e-1        3.3692e-1 
       10           65         8.3170e-1     2.6448e-1        1.9570e-1 
       11           71         8.2804e-1     9.7254e-2        2.3849e-2 
       12           77         8.2799e-1     1.2318e-2        6.0513e-4 
       13           83         8.2799e-1     5.0716e-4        5.4566e-6 
       14           89         8.2799e-1     6.6649e-6        2.1427e-8 
   x = 
      2.0377    4.0210    5.0101    0.4989
   
   c =   -0.5496

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
        0            5          1.5802e3                       1.5164e3 
        1           11          1.2961e3     2.1299e-1         1.3150e3 
        2           17          7.8720e2     4.9116e-1         8.8541e2 
        3           23          2.8901e2     8.2016e-1         3.2839e2 
        4           29          5.3452e1     9.6996e-1         6.9363e1 
        5           35          5.1011e0     6.2590e-1         3.7659e1 
        6           41          2.4331e0     2.0112e-1         4.0680e0 
        7           47          2.1718e0     1.8012e-1         1.2303e0 
        8           53          1.7022e0     4.0678e-1         1.5675e0 
        9           59          1.1219e0     7.2282e-1         1.6489e0 
       10           69          1.1085e0     2.2594e-2        6.9673e-1 
       11           77          1.1046e0     7.1209e-3        6.8608e-1 
       12           85          1.1034e0     2.2395e-3        6.8643e-1 
       13           92          1.1021e0     2.2268e-3        6.8702e-1 
       14          103          1.1021e0     2.2267e-5        6.8703e-1 
       15          109          1.1023e0     1.2940e-5        6.6140e-1 
       16          115          1.1029e0     3.8515e-5        5.9816e-1 
       17          121          1.1024e0     1.2211e-4        4.9170e-1 
       18          127          1.1018e0     3.8351e-4        3.8849e-1 
       19          133          1.1015e0     8.3023e-4        3.1123e-1 
       20          139          1.1013e0     9.3503e-4        2.7935e-1 
       21          145          1.1013e0     4.1170e-4        2.8253e-1 
       22          151          1.1013e0     5.1863e-5        2.8401e-1 
       23          157          1.1013e0     7.9700e-6        2.8406e-1 
       24          163          1.1013e0     2.5017e-5        2.8401e-1 
       25          169          1.1012e0     7.8212e-5        2.8389e-1 
       26          175          1.1011e0     2.3868e-4        2.8387e-1 
       27          181          1.1009e0     6.7714e-4        2.8669e-1 
       28          187          1.1004e0     1.5715e-3        3.0875e-1 
       29          193          1.0999e0     2.3142e-3        3.7233e-1 
       30          199          1.0998e0     1.5779e-3        4.2969e-1 
       31          205          1.0997e0     3.9563e-4        4.4534e-1 
       32          211          1.0997e0     3.1783e-5        4.4663e-1 
       33          217          1.0997e0     1.0809e-6        4.4668e-1 
   x = 
      1.3600    3.7617    5.0153    0.6349
   
   c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


