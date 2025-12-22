using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_1_Polynomials
{
    public class Section_3_Polynomial_Arithmetics
    {
        public static void Run()
        {

            {
                // Example of Polynomial Addition
                double[] poly3 = [1, 2, 3, 4, 5], poly4 = [4, 5, 6];
                var AddPoly = Polyadd(poly3, poly4);
                Console.WriteLine("AddPoly:\n " + string.Join(", ", AddPoly));
            }


            {
                // Example of Polynomial Subtraction
                double[] poly5 = [1, 2, 3, 4, 5], poly6 = [4, 5, 6];
                var SubPoly = Polysub(poly5, poly6);
                Console.WriteLine("SubPoly:\n " + string.Join(", ", SubPoly));
            }


            {
                // Example of Polynomial Convolution
                double[] poly1 = [1, 2, 3], poly2 = [4, 5, 6];
                var ConvPoly = Conv(poly1, poly2);
                Console.WriteLine("ConvPoly:\n " + string.Join(", ", ConvPoly));
            }


            {
                // Example of Polynomial Deconvolution
                double[] P = [3, 2, 4, 6, 5], D = [4, 5, 6], Q, R;
                (Q, R) = Deconv(P, D);
                Console.WriteLine("Quotient:\n " + string.Join(", ", Q));
                Console.WriteLine("Remainder:\n " + string.Join(", ", R));
            }
        }
    }
}
