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
        0            5          1.7207e0                       3.6024e0 
        1           11         7.7957e-1     2.5675e-1         1.4805e0 
        2           17         5.0692e-1     2.3416e-1        2.9800e-1 
        3           23         4.3373e-1     5.0280e-1        1.3549e-1 
        4           29         2.6094e-1      1.9404e0        6.6305e-1 
        5           35         1.1629e-1      3.0296e0        7.4260e-1 
        6           41         5.3312e-3     4.1766e-1        1.0163e-1 
        7           47         3.9934e-3     4.2066e-1        1.1476e-1 
        8           53         3.6205e-3     3.5959e-1        1.0014e-1 
        9           59         3.0924e-3     2.4396e-1        5.2031e-2 
       10           65         2.9168e-3     1.1784e-1        1.2616e-2 
       11           71         2.9060e-3     2.9367e-2        7.8728e-4 
       12           77         2.9059e-3     2.4546e-3        5.5155e-6 
       13           83         2.9059e-3     3.0309e-5        1.1443e-9 

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
        0            5          1.5897e3                       1.5180e3 
        1           11          1.3079e3     2.1061e-1         1.3183e3 
        2           17          8.0262e2     4.8512e-1         8.9200e2 
        3           23          3.0539e2     8.0851e-1         3.3824e2 
        4           29          6.1881e1     9.7306e-1         6.9477e1 
        5           35          1.0910e1     6.6199e-1         3.6403e1 
        6           41          5.8757e0     3.9687e-1         2.3191e0 
        7           47          2.9258e0     5.4632e-1         3.4022e0 
        8           53          2.5052e0     5.6708e-1         1.8459e0 
        9           59          1.1071e0     2.6287e-1        3.2873e-1 
       10           65          1.1062e0     2.6762e-3        1.0732e-1 
       11           71          1.0862e0     2.2740e-1        1.1186e-1 
       12           77          1.0859e0     2.8984e-2        2.0196e-3 
       13           83          1.0859e0     1.1987e-3        1.3994e-5 
       14           89          1.0859e0     1.5826e-5        5.1286e-8 
   x = 
      1.9185    3.9803    5.0038    0.5163
   
   c =   -0.3787

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
        0            5          1.5875e3                       1.5151e3 
        1           11          1.3028e3     2.1330e-1         1.3137e3 
        2           17          7.9244e2     4.9218e-1         8.8424e2 
        3           23          2.9132e2     8.2322e-1         3.2732e2 
        4           29          5.3894e1     9.7432e-1         6.9946e1 
        5           35          4.9905e0     6.2826e-1         3.6287e1 
        6           41          2.3101e0     1.9996e-1         4.2162e0 
        7           47          2.0681e0     1.7202e-1         1.1184e0 
        8           53          1.6512e0     3.8342e-1         1.4502e0 
        9           59          1.1358e0     6.8092e-1         1.5048e0 
       10           68          1.1020e0     6.3459e-2        6.3059e-1 
       11           77          1.0989e0     6.3070e-3        6.2384e-1 
       12           84          1.0965e0     6.2397e-3        6.2102e-1 
       13           90          1.0951e0     2.0752e-3        2.7958e-1 
       14           96          1.0946e0     9.7005e-4        2.7357e-1 
       15          102          1.0945e0     2.3593e-4        2.8117e-1 
       16          108          1.0945e0     7.2717e-5        2.8302e-1 
       17          114          1.0945e0     2.1948e-4        2.8812e-1 
       18          120          1.0943e0     5.7940e-4        3.0317e-1 
       19          126          1.0943e0     1.8996e-4        3.0859e-1 
       20          132          1.0943e0     2.8610e-4        3.1710e-1 
       21          138          1.0942e0     6.6076e-4        3.3834e-1 
       22          144          1.0941e0     9.6341e-4        3.7266e-1 
       23          150          1.0941e0     6.4732e-4        3.9752e-1 
       24          156          1.0941e0     1.5955e-4        4.0385e-1 
       25          162          1.0941e0     1.2614e-5        4.0435e-1 
   x = 
      1.3267    3.7736    5.0163    0.6428
   
   c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


