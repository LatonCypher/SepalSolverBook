using System.Reflection.Metadata;

namespace ConsoleApp1
{
    public class DeclineCurveExtension
    {
        public static (double Exponent, double Constant) SecondaryFluidModel(List<double> Cummulative, List<double> SecondaryFluidRatio)
        {
            double[] coeffs = Polyfit(Cummulative.ToArray(), SecondaryFluidRatio.Select(Log).ToArray(), 1);
            return (coeffs[0], Exp(coeffs[1]));
        }

        public static double AbandonmentRatio (double Exponent, double Constant, double CummAbandonment)
            => Constant * Exp(Exponent * CummAbandonment);
    }
}
