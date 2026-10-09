using ScottPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Mechanical_Engineering
{
    internal class Section_2_ViscousFluid
    {
        public static void Run()
        {
            /// <BookContent>
            /// <code>
            {
                ColVec? T = null; Matrix? Y = null;
                double err(double alp)
                {
                    (T, Y) = Ode45((t, y) => [y[1], y[2], -0.5 * y[0] * y[2]], [0, 0, alp], [0, 6]);
                    return Y[^1, 1] - 1;
                }
                //Use a root finding method to find the value of alp that satisfies the boundary condition at t = 6
                double alp = Fsolve(err, 0.5);
                //Compare with the solution of the BVP using a direct method
                Console.WriteLine($"Solution is {alp}");
                //Plot the solution
                Plot(T, Y, Linewidth: 2);
                Legend(["f", "f'", "f''"]);
                Axis([0, 6, 0, 1.5]);
                Xlabel("$\\eta$", interpreter: Latex); Ylabel("f and its derivatives");
                Title("Blasius Equation With Estimated f''(0)");
                SaveAs("Blasius-bounary-layer.png");
            }
            /// </code>
            /// 
            /// <code>
            {
                // define parameters
                double rhomu_h, drhomu_h_eta, gamma, Pr, C;
                ColVec? T = null; Matrix? Y = null;

                // define functions and their derivatives
                Func<double, double> rho, drhodh, mu, dmudh, rhomu;
                Func<double, double, double> drhomu;

                //define time span and intial guess
                double[] tspan, y0, y35guess;

                // define intexer for the unknwon initial conditions
                int[] I = [1, 3];

                //define function for solution of howarth transformation
                (ColVec, Matrix) HowarthTransform(double M)
                {
                    // assign parameters, functions anf their derivatives
                    gamma = 1.4;
                    Pr = 0.7;
                    C = Pr * (gamma - 1) * M * M;
                    rho = h => 1.0 / h;
                    drhodh = h => -1 / (h * h);
                    mu = h => Pow(h, 2.0 / 3);
                    dmudh = h => 2.0 / 3 * Pow(h, -1.0 / 3);
                    rhomu = h => rho(h) * mu(h);
                    drhomu = (h, dh) => (rho(h) * dmudh(h) + drhodh(h) * mu(h)) * dh;

                    // define the differential equation
                    ColVec dydt(double t, ColVec y)
                    {
                        rhomu_h = rhomu(y[3]);
                        drhomu_h_eta = drhomu(y[3], y[4]);
                        double[] dy = [y[1],
                       y[2],
                       -(2*drhomu_h_eta + y[0])*y[2]/(2*rhomu_h),
                       y[4],
                       -(drhomu_h_eta*y[4] + Pr*y[0]*y[4] + C*rhomu_h*y[2]*y[2])/rhomu_h ];
                        return dy;
                    }

                    // set time span and intial guess
                    tspan = [0, 5]; y35guess = [0.1, 0.2];

                    // define the nonlinear system to compute the initial condition
                    ColVec fun(ColVec y35_0)
                    {
                        y0 = [0, 0, y35_0[0], 2, y35_0[1]];
                        (T, Y) = Ode45(dydt, y0, tspan);
                        return Y[^1, [1,3]].T - 1;
                    }

                    // solve for the unknown initial conditions
                    Fsolve(fun, y35guess);
                    return (T, Y);
                }

                // generator solution for M = 0 and plot
                (T, Y) = HowarthTransform(0);
                Plot(T, Y[.., 1], "b", 2); HoldOn();
                Plot(T, Y[.., 3] - 1, "r", 2);

                // generator solution for M = 5 and plot
                (T, Y) = HowarthTransform(5);
                Plot(T, Y[.., 1], "b", 2);
                Plot(T, Y[.., 3] - 1, "r", 2); HoldOff();

                // add legend, axis label and title
                Legend(["f'", "h-1"], UpperRight);
                Xlabel("η", interpreter:Latex); Title("Howarth Transformation");
                Axis([0, 5, 0, 2]);
                SaveAs("Howarth-Transformation.png");
            }
            /// </code>
            /// 
            /// </BookContent>
        }
    }
}
