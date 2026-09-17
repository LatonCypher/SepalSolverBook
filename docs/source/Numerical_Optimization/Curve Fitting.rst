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
        0            5          1.7340e0                       3.5521e0 
        1           11         7.9210e-1     2.5781e-1         1.4749e0 
        2           17         5.1827e-1     2.3665e-1        2.9761e-1 
        3           23         4.4279e-1     5.1360e-1        1.3973e-1 
        4           29         2.6548e-1      1.9926e0        6.7630e-1 
        5           35         1.1572e-1      3.0467e0        7.3029e-1 
        6           41         5.9363e-3     5.5998e-1        6.5553e-2 
        7           48         4.1016e-3     2.2081e-1        4.0305e-2 
        8           55         3.7691e-3     1.6977e-1        2.2291e-2 
        9           62         3.5861e-3     1.2641e-1        1.3166e-2 
       10           68         3.5700e-3     2.7900e-1        6.2329e-2 
       11           75         3.2201e-3     1.5107e-1        2.0912e-2 
       12           82         3.1288e-3     1.1813e-1        1.2889e-2 
       13           89         3.0734e-3     9.3184e-2        8.1534e-3 
       14           96         3.0360e-3     7.7000e-2        5.6219e-3 
       15          102         3.0345e-3     1.8402e-1        3.1574e-2 
       16          109         2.9451e-3     1.2312e-1        1.3701e-2 
       17          116         2.9150e-3     9.7327e-2        8.5215e-3 
       18          123         2.8968e-3     7.9666e-2        5.6353e-3 
       19          130         2.8844e-3     6.7374e-2        3.9789e-3 
       20          137         2.8752e-3     5.8314e-2        2.9435e-3 
       21          144         2.8681e-3     5.1368e-2        2.2568e-3 
       22          150         2.8666e-3     1.2977e-1        1.4211e-2 
       23          157         2.8486e-3     9.7781e-2        7.4972e-3 
       24          164         2.8407e-3     7.9465e-2        4.8543e-3 
       25          171         2.8358e-3     6.6574e-2        3.3329e-3 
       26          178         2.8325e-3     5.7096e-2        2.4040e-3 
       27          185         2.8300e-3     4.9838e-2        1.7998e-3 
       28          192         2.8281e-3     4.4103e-2        1.3874e-3 
       29          199         2.8266e-3     3.9459e-2        1.0949e-3 
       30          205         2.8264e-3     9.9102e-2        6.8498e-3 
       31          212         2.8225e-3     7.6586e-2        3.8281e-3 
       32          219         2.8207e-3     6.1963e-2        2.4560e-3 
       33          226         2.8197e-3     5.1395e-2        1.6577e-3 
       34          233         2.8190e-3     4.3440e-2        1.1650e-3 
       35          240         2.8185e-3     3.7253e-2        8.4476e-4 
       36          247         2.8182e-3     3.2315e-2        6.2792e-4 
       37          254         2.8179e-3     2.8294e-2        4.7623e-4 
       38          260         2.8178e-3     6.5342e-2        2.5281e-3 
       39          267         2.8173e-3     4.7512e-2        1.2784e-3 
       40          274         2.8171e-3     3.5676e-2        7.1253e-4 
       41          281         2.8170e-3     2.7320e-2        4.1375e-4 
       42          287         2.8169e-3     4.6504e-2        1.1960e-3 
       43          293         2.8169e-3     4.1607e-2        9.3251e-4 
       44          299         2.8168e-3     1.5546e-2        1.2619e-4 
       45          305         2.8168e-3     1.8368e-3        1.7387e-6 

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
        0            5          1.5834e3                       1.5156e3 
        1           11          1.3026e3     2.1022e-1         1.3162e3 
        2           17          7.9914e2     4.8420e-1         8.9056e2 
        3           23          3.0420e2     8.0673e-1         3.3770e2 
        4           29          6.1756e1     9.7056e-1         6.9053e1 
        5           35          1.1075e1     6.6167e-1         3.6601e1 
        6           41          5.9596e0     4.0231e-1         2.1888e0 
        7           47          2.8842e0     5.5729e-1         3.5538e0 
        8           53          2.1587e0     5.7965e-1         1.9336e0 
        9           59         9.8881e-1     2.4073e-1        3.5685e-1 
       10           65         9.1178e-1     3.7098e-1        3.4228e-1 
       11           71         9.0451e-1     1.3721e-1        4.0558e-2 
       12           77         9.0440e-1     1.7490e-2        9.7512e-4 
       13           83         9.0440e-1     7.2329e-4        8.1173e-6 
       14           89         9.0440e-1     9.4807e-6        3.4380e-8 
   x = 
      2.1766    4.0607    4.9985    0.4627
   
   c =   -0.7761

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
        0            5          1.5840e3                       1.5154e3 
        1           11          1.2997e3     2.1310e-1         1.3141e3 
        2           17          7.9038e2     4.9151e-1         8.8467e2 
        3           23          2.9119e2     8.2097e-1         3.2801e2 
        4           29          5.4486e1     9.7120e-1         7.0405e1 
        5           35          5.3297e0     6.2856e-1         3.8147e1 
        6           41          2.5601e0     2.0084e-1         4.5172e0 
        7           47          2.3390e0     1.6311e-1         1.0205e0 
        8           53          1.9735e0     3.5900e-1         1.3489e0 
        9           59          1.5238e0     6.3593e-1         1.3675e0 
       10           67          1.4494e0     1.6152e-1        5.3422e-1 
       11           77          1.4474e0     5.0849e-3        5.0360e-1 
       12           85          1.4467e0     1.6060e-3        5.0148e-1 
       13           92          1.4462e0     1.6004e-3        5.0077e-1 
       14           98          1.4459e0     7.9065e-4        2.6611e-1 
       15          104          1.4456e0     8.9839e-4        2.0498e-1 
       16          110          1.4455e0     4.1375e-4        2.0621e-1 
       17          116          1.4455e0     6.1227e-5        2.0796e-1 
       18          122          1.4455e0     8.2459e-5        2.0874e-1 
       19          128          1.4454e0     2.4550e-4        2.1134e-1 
       20          134          1.4452e0     6.4884e-4        2.2153e-1 
       21          140          1.4452e0     2.1287e-4        2.2588e-1 
       22          146          1.4451e0     3.2081e-4        2.3322e-1 
       23          152          1.4450e0     7.4234e-4        2.5350e-1 
       24          158          1.4449e0     1.0867e-3        2.8979e-1 
       25          164          1.4449e0     7.3468e-4        3.1761e-1 
       26          170          1.4449e0     1.8267e-4        3.2486e-1 
       27          176          1.4449e0     1.4650e-5        3.2546e-1 
   x = 
      1.3403    3.7688    5.0229    0.6423
   
   c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


