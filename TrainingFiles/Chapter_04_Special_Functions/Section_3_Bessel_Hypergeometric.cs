using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_3_Special_Functions
{
    internal class Section_3_Bessel_Hypergeometric
    {
        public static void Run()
        {
            {
                // BesselJ function
                double x = 2.5;
                double y = BesselJ(0, x); //Here, we compute the Bessel function of the first kind of order 0 at x = 2.5
                Console.WriteLine(y); // Expected output: -0.497094102271155

                // Plot BesselJ Functions of orders 0 to 9
                ColVec x1 = Linspace(0, 20);
                Matrix y1 = Enumerable.Range(0, 10).Select(i => BesselJ(i, x1)).ToList();
                // Plot result
                Plot(x1, y1);
                Xlabel("x-axis");
                Ylabel("y-axis");
                Title("Bessel function J");
            }
            {
                // BeselI function
                double x = 2.5;
                double y = BesselI(0, x); //Here, we compute the Modified Bessel function of the first kind of order 0 at x = 2.5
                Console.WriteLine(y); // Expected output: 3.28983914405

                // Plot BesselI Functions of orders 0 to 3
                ColVec x1 = Linspace(0, 5);
                Matrix y1 = Enumerable.Range(0, 4).Select(i => BesselI(i, x1)).ToList();

                // Plot result
                Plot(x1, y1);
                Axis([0, 5, 0, 15]);
                Xlabel("x-axis"); Ylabel("y-axis");
                Title("Bessel function I");
                Legend(Enumerable.Range(0, 4).Select(i => "I_" + i + "(x1)"));
            }
            {
                // BesselY function
                double x = 5.0;
                double y = BesselY(2, x); // Here, we compute the Bessel function of the second kind of order 2 at x = 5.0
                Console.WriteLine($"Y_{2}({x}) = {y}"); // Expected output: J_2(5.0) = -0.308517625249033

                // Plot BesselY Functions of orders 0 to 3
                ColVec x1 = Linspace(0.01, 10, 500); // Avoid x = 0 to prevent singularity
                Matrix y1 = Enumerable.Range(0, 4).Select(i => BesselY(i, x1)).ToList();

                // Plot result
                Plot(x1, y1);
                Axis([0, 10, -2, 1]);
                Xlabel("x-axis"); Ylabel("y-axis");
                Title("Bessel function Y");
                Legend(Enumerable.Range(0, 4).Select(i => "Y_" + i + "(x1)"), LowerRight);
            }

            {
                // BesselK function
                double x = 2;
                double y = BesselK(1, x); // Here we compute the Modified Bessel function of the second kind of order 1 at x = 2.0
                Console.WriteLine($"Y_{2}({x}) = {y}"); // Expected output: K_1(2.0) = 0.113893872749533

                // Plot BesselK Functions of orders 0 to 4
                ColVec x1 = Linspace(0.1, 10, 500); // Avoid x = 0 to prevent singularity
                Matrix y1 = Enumerable.Range(0, 5).Select(i => BesselK(i, x1)).ToList();

                Plot(x1, y1);
                Axis([0, 5, 0, 5]);
                Xlabel("x-axis"); Ylabel("y-axis");
                Title("Bessel function K");
                Legend(Enumerable.Range(0, 5).Select(i => "K_" + i + "(x1)"), UpperRight);
            }
            {
                // Hypergeometric function 2F1(a, b; c; z)
                double a = 2;
                double b = 2;
                double x = 3;
                double hypergeom = HyperGeom(a, b, x); // Here we compute the hypergeometric function 2F1(2, 2; 3)
                Console.WriteLine($"2F1({a}, {b}; 3; {x}) = {hypergeom}"); // Expected output: 2F1(2, 2, 3) = 20.0855
            }
        }
    }
}
