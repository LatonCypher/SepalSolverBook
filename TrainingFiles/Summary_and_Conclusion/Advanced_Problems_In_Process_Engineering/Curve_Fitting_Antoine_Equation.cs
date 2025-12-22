using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Process_Engineering
{
    public static class Curve_Fitting_Antoine_Equation
    {
        public static void Run()
        {
            // Curve Fitting for Antoine Equation [lnP = A - B/(T + C)]
            double[] P_Kpa = [3.18, 5.48, 9.45, 16.9, 28.2, 41.9, 66.6, 89.5, 129, 187],
                     T_C = [-18.5, -9.5, 0.2, 11.8, 23.1, 32.7, 44.4, 52.1, 63.3, 75.5];

            // Convert Temperature to Kelvin, Compute the natural logarithm of Pressure
            ColVec xdata = (ColVec)T_C + 273.15, 
                   ydata = Log((ColVec)P_Kpa);

            // Define the model function for the Antoine equation
            static ColVec AntoineModel(ColVec x, ColVec xdata) 
                => x[0] - x[1] / (xdata + x[2]);
            
            // Define initial guess
            double[] x0 = [ 8, 1200, 10 ];

            // Fit the data
            var ans = Lsqcurvefit(AntoineModel, x0, xdata, ydata);

            // Extract fitted parameters
            double A = ans.x[0], B = ans.x[1], C = ans.x[2];
            Console.WriteLine($"Fitted Parameters: A = {A:F4}, B = {B:F4}, C = {C:F4}");

            //Animate Optimization History
            AnimateHistory(AntoineModel, xdata, ydata,
                ans.history, "History_AntoineModel0.gif");
        }
    }
}
