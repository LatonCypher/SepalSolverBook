using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Process_Engineering
{
    public static class Linear_Fitting_Antoine_Equation
    {
        public static void Run()
        {
            // Curve Fitting for Antoine Equation [lnP = A - B/(T + C)]
            double[] P_Kpa = [3.18, 5.48, 9.45, 16.9, 28.2, 41.9, 66.6, 89.5, 129, 187],
                     T_C = [-18.5, -9.5, 0.2, 11.8, 23.1, 32.7, 44.4, 52.1, 63.3, 75.5];

            // Convert Temperature to Kelvin, Compute the natural logarithm of Pressure
            ColVec xdata = (ColVec)T_C + 273.15, 
                   ydata = Log((ColVec)P_Kpa);

            // Multilinear fit
            List<ColVec> M = [Ones(xdata.Numel), 1.0 / xdata, ydata.Div(xdata)];
            var x = Mldivide(M, ydata);

            // Extract fitted parameters
            double A = x[0], C = -x[2], B = A*C-x[1];
            Console.WriteLine($"Fitted Parameters: A = {A:F4}, B = {B:F4}, C = {C:F4}");
        }
    }
}
