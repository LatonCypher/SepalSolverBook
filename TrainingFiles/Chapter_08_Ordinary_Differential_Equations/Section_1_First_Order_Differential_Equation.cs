using ScottPlot;
using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;
using static SepalSolver.PlotLib.Chart.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_7_Ordinary_Differential_Equations
{
    internal class Section_1_First_Order_Differential_Equation
    {
        public static void Run()
        {
            {
                // Simple Harmonic Oscillator
                double[] tspan = { 0, 10 };
                double[] y0 = { 1, 0 };
                Func<double, ColVec, ColVec> f = (t, y) => new double[] { y[1], -y[0] };
                var options = Odeset(Stats: true, AbsTol: 1e-10, RelTol: 1e-4);
                (ColVec T, Matrix Y) = Ode45(f, y0, tspan, options);
                Plot(T, Y, Linewidth: 2); SaveAs("ode_example.png");
            }

            {
                // Damping System

                double stiffness = 3.5, damping = 0.5, mass = 2.0, k = stiffness/mass, c = damping/mass;
                Func<double, ColVec, ColVec> dydt = (t, y) => new double[] { y[1], -(k*y[0] + c*y[1]) };
                (ColVec T, Matrix Y) = Ode45(dydt, [0.7, 0], Linspace(0, 30, 451));
                Plot(T, Y, Linewidth: 2); SaveAs("ode_example.png");
            }

            {
                // Predator Prey Model
                double alpha = 0.01, beta = 0.02;
                Func<double, ColVec, ColVec> dydt = (t, y) => new double[] { (1 - alpha*y[1])*y[0], (-1 + beta*y[0])*y[1] };
                (ColVec T, Matrix Y) = Ode45(dydt, [20, 20], [0, 15]);
                Plot(T, Y, Linewidth: 2); SaveAs("ode_example.png");

            }

            {
                // Blausius Boundary Layer

                // define function
                ColVec dydt(double t, ColVec y)
                {
                    double[] dy = [y[1], y[2], -0.5 * y[2] * y[0]];
                    return dy;
                }

                // set time span
                double[] tspan = [0, 6];

                double[] y0 = [0, 0, 0.5];
                (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);

                // plot the result
                Plot(T, Y, Linewidth: 2);
                Legend(["f", "f'", "f''"], UpperLeft);
                Axis([0, 6, 0, 2]); Xlabel("η"); Title("Blasius Boundary layer");
            }
        }

    }
}
