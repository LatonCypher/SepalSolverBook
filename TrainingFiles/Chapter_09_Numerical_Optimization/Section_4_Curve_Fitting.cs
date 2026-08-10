using ScottPlot;
using ScottPlot.Colormaps;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1.TrainingFiles.Chapter_09_Numerical_Optimization
{
    internal class Section_4_Curve_Fitting
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> Curve Fitting </header>
            /// Curve fitting is a mathematical technique used to construct a curve that best fits a series of data points. It is widely applied in data analysis, statistics, and machine learning to model relationships between variables.
            ///
            /// <header 3> Types of Curve Fitting: </header 3>
            /// 1. Linear Regression: Fits a straight line to the data points.
            /// 2. Polynomial Regression: Fits a polynomial curve of degree n to the data points.
            /// 3. Nonlinear Regression: Fits a nonlinear model to the data points.
            ///
            /// <header 3> Example: Polynomial Curve Fitting </header 3>
            /// Given a set of data points, we can fit a polynomial curve using least squares optimization.
            ///
            /// <math>
            /// \min_{\mathbf{p}} \sum_{i=1}^{n} (y_i - P(x_i; \mathbf{p}))^2
            /// </math>
            /// 
            /// <code>
            {
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
            }
            /// </code>
            /// 
            /// <header 3> Example: Fourier Series Fitting </header 3>
            /// Evaluating a Fourier series numerically involves transforming an infinite 
            /// sum of trigonometric terms into a computationally stable, finite calculation
            /// while controlling truncation errors, floating-point precision loss, and
            /// spectral artifacts.
            /// 
            /// Mathematical Formulation:
            /// 
            /// A truncated Fourier series approximating a periodic function :math:`f(x)` on 
            /// the interval :math:`[-\pi, \pi]` with :math:`N` harmonics is defined as:
            /// 
            /// <math>
            ///     f_N(x) = \frac{a_0}{2} + \sum_{n=1}^{N} \left( a_n \cos(nx) + b_n \sin(nx) \right)
            /// </math>
            /// 
            /// In complex exponential form, which is computationally convenient for many 
            /// numerical implementations, the series is expressed as:
            /// 
            /// <math>
            ///    f_N(x) = \sum_{n=-N}^{N} c_n e^{i n x}
            /// </math>
            /// 
            /// where the complex coefficients :math:`c_n` relate to the real coefficients via:
            /// 
            /// <math>
            ///     c_0 = \frac{a_0}{2}, \quad c_n = \frac{a_n - i b_n}{2}, \quad c_{-n} = \frac{a_n + i b_n}{2}
            /// </math>
            /// <code>
            {
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
            }
            /// </code>
            /// 
            /// <header 3> Example: Bi-Exponential Curve Fitting </header 3>
            /// This exercise covers non-linear parameter estimation using least-squares optimization 
            /// to fit a bi-exponential model to noisy data while visualizing optimizer convergence.
            /// 
            /// The objective is to fit data points :math:`(x_d, y_d)` to a bi - exponential model:
            /// <math>
            ///     f(x; \theta) = \theta_2 e^{\theta_0 x} + \theta_3 e^{\theta_1 x}
            /// </math>
            /// where: math:`\theta = [\theta_0, \theta_1, \theta_2, \theta_3] ^ T` represents the unknown parameters.
            /// 
            /// Find :math:`\hat{\theta}` minimizing the sum of squared residuals:
            /// 
            /// <math>
            ///     \hat{\theta} = \arg\min_{\theta} \sum_{d=1}^D (y_d - f(x_d; \theta))^2
            /// </math>
            /// 
            /// <code>
            {
                ColVec noise, weight = new double[100]; double[] x0;
                static ColVec fun(ColVec x, ColVec xdata) => x[2] * Exp(x[0] * xdata) + x[3] * Exp(x[1] * xdata);
                ColVec xdata = Linspace(0, 1); noise = Rand(xdata.Numel);
                ColVec ydata = fun(x0 = [-4, -5, 4, -4], xdata) + 0.02 * noise;
                x0 = [-1, -2, 1, -1]; weight[xdata < 0.5] = 1;
                var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
                var ans = Lsqcurvefit(fun, x0, xdata, ydata, options: opts);
                AnimateHistory(fun, xdata, ydata, ans.history, "Bi_Exponential_Fitting.gif");
                CloseFig();
            }
            /// </code>
            /// 
            /// <code>
            {
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
            }
            /// </code>
            /// 
            /// Lsqcurvefit allows the use of constraints. 
            /// 1. Seed data for reproducability
            /// <code>
            {
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
            }
            /// </code>
            /// 
            /// 2. Fitting with Linear constraint
            /// <code>
            {
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
            }
            /// </code>
            /// 
            /// 3. Fitting with nonlinear constraint.
            /// <code>
            {
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
            }
            /// </code>
            /// 
            /// </BookContent>
        }
    }
}
