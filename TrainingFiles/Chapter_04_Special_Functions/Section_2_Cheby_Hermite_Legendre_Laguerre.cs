using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_3_Special_Functions
{
    internal class Section_2_Cheby_Hermite_Legendre_Laguerre
    {
        public static void Run()
        {
            {
                // Compute Hermite polynomial
                double x = 1.0;
                double hermitePoly = HermiteH(0, x); // Here, we compute the Hermite polynomial of degree 0 at point x = 1.0
                Console.WriteLine($"H_0({1.0}) = {hermitePoly}"); // Expected output: H_0(1.0) = 1
            }
            {
                // Compute ChebyshevT polynomial
                double x1 = 0.5;
                double chebyshevTPoly = ChebyshevT(3, x1); // Here, we compute the Chebyshev polynomial of the first kind of degree 3 at point x = 0.5
                Console.WriteLine($"T_3({0.5}) = {chebyshevTPoly}"); // Expected output: T_3(0.5) = -1

                // Compute ChebyshevU polynomial
                double x = 0.5;
                double chebyshevUPoly = ChebyshevU(3, x); // Here, we compute the Chebyshev polynomial of the second kind of degree 3 at point x = 0.5
                Console.WriteLine($"U_3({0.5}) = {chebyshevUPoly}"); // Expected output: U_3(0.5) = -1
            }
            {
                // Compute Legendre polynomial
         
                double x = 0.5;
                double legendrePoly = LegendreP(3, x); // Here, we compute the Legendre polynomial of degree 3 at point x = 0.5
                Console.WriteLine($"L_3({0.5}) = {legendrePoly}"); // Expected output: P_3(0.5) = -0.4375
            }
            {
                // Compute Laguerre polynomial
                double x = 0.5;
                double laguerrePoly = Laguerre(3, x); // Here, we compute the Laguerre polynomial of degree 3 at point x = 0.5
                Console.WriteLine($"L_3({0.5}) = {laguerrePoly}"); // Expected output: L_3(0.5) = 0.145833
            }
        }
    }
}
