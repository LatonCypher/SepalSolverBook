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
        0            5          1.7399e0                       3.5660e0 
        1           11         8.0551e-1     2.5594e-1         1.4727e0 
        2           17         5.3322e-1     2.3802e-1        2.9699e-1 
        3           23         4.5432e-1     5.2902e-1        1.4635e-1 
        4           29         2.7124e-1      2.0518e0        6.9707e-1 
        5           35         1.1111e-1      3.0355e0        7.2031e-1 
        6           41         5.5844e-3     5.1672e-1        7.0306e-2 
        7           48         3.8902e-3     2.1498e-1        3.6023e-2 
        8           55         3.6130e-3     1.6201e-1        1.9308e-2 
        9           62         3.4634e-3     1.2083e-1        1.1407e-2 
       10           68         3.4119e-3     2.5637e-1        4.9839e-2 
       11           75         3.1741e-3     1.4034e-1        1.7362e-2 
       12           82         3.1067e-3     1.0698e-1        1.0211e-2 
       13           89         3.0675e-3     8.3182e-2        6.3215e-3 
       14           95         3.0640e-3     1.7784e-1        2.8588e-2 
       15          102         2.9917e-3     1.1012e-1        1.1106e-2 
       16          109         2.9714e-3     8.2537e-2        6.2645e-3 
       17          116         2.9606e-3     6.4334e-2        3.8043e-3 
       18          123         2.9539e-3     5.2016e-2        2.4832e-3 
       19          129         2.9517e-3     1.0867e-1        1.0741e-2 
       20          136         2.9410e-3     7.0830e-2        4.4068e-3 
       21          143         2.9378e-3     5.0947e-2        2.2716e-3 
       22          149         2.9374e-3     8.1933e-2        5.8581e-3 
       23          155         2.9355e-3     7.1904e-2        4.3756e-3 
       24          161         2.9342e-3     2.9945e-2        7.3206e-4 
       25          167         2.9342e-3     4.9489e-3        1.9780e-5 
       26          173         2.9342e-3     2.8588e-4        6.6609e-8 

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
        0            5          1.5919e3                       1.5162e3 
        1           11          1.3101e3     2.1067e-1         1.3167e3 
        2           17          8.0436e2     4.8547e-1         8.9075e2 
        3           23          3.0577e2     8.1022e-1         3.3723e2 
        4           29          6.1771e1     9.7472e-1         6.9795e1 
        5           35          1.0675e1     6.6117e-1         3.6104e1 
        6           41          5.6807e0     3.9332e-1         2.5118e0 
        7           47          2.8227e0     5.3839e-1         3.2510e0 
        8           53          2.6077e0     5.5737e-1         1.7731e0 
        9           59          1.0686e0     2.7579e-1        3.2868e-1 
       10           65          1.0646e0     5.1904e-3        3.6699e-2 
       11           71          1.0622e0     7.8002e-2        1.7333e-2 
       12           77          1.0622e0     9.9940e-3        4.8832e-4 
       13           83          1.0622e0     4.2571e-4        1.1875e-5 
       14           89          1.0622e0     6.2777e-6        2.4034e-7 
   x = 
      1.7528    3.9351    5.0061    0.5518
   
   c =   -0.1300

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
        0            5          1.5758e3                       1.5051e3 
        1           11          1.2941e3     2.1221e-1         1.3050e3 
        2           17          7.8880e2     4.8982e-1         8.7822e2 
        3           23          2.9116e2     8.2040e-1         3.2490e2 
        4           29          5.3721e1     9.7384e-1         6.9519e1 
        5           35          4.9612e0     6.2780e-1         3.8205e1 
        6           41          2.2276e0     2.0014e-1         4.3819e0 
        7           47          1.9955e0     1.6754e-1         1.0560e0 
        8           53          1.6066e0     3.7039e-1         1.3889e0 
        9           59          1.1256e0     6.5770e-1         1.4274e0 
       10           68          1.0942e0     6.1288e-2        6.0949e-1 
       11           76          1.0851e0     1.8881e-2        5.9569e-1 
       12           83          1.0764e0     1.8383e-2        5.8330e-1 
       13           91          1.0737e0     5.7679e-3        5.7851e-1 
       14           98          1.0711e0     5.7216e-3        5.7436e-1 
       15          106          1.0702e0     1.8050e-3        5.7291e-1 
       16          113          1.0695e0     1.8004e-3        5.7158e-1 
       17          119          1.0690e0     9.5188e-4        3.0937e-1 
       18          125          1.0686e0     1.0528e-3        2.3291e-1 
       19          131          1.0686e0     4.9143e-4        2.3386e-1 
       20          137          1.0685e0     7.5757e-5        2.3594e-1 
       21          143          1.0685e0     9.5147e-5        2.3688e-1 
       22          149          1.0684e0     2.8315e-4        2.3996e-1 
       23          155          1.0682e0     7.4725e-4        2.5192e-1 
       24          161          1.0682e0     2.4497e-4        2.5698e-1 
       25          167          1.0681e0     3.6885e-4        2.6550e-1 
       26          173          1.0680e0     8.5150e-4        2.8889e-1 
       27          179          1.0678e0     1.2405e-3        3.3033e-1 
       28          185          1.0678e0     8.3283e-4        3.6177e-1 
       29          191          1.0678e0     2.0528e-4        3.6989e-1 
       30          197          1.0678e0     1.6274e-5        3.7054e-1 
       31          203          1.0678e0     5.4928e-7        3.7057e-1 
   x = 
      1.3249    3.7742    5.0159    0.6472
   
   c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


