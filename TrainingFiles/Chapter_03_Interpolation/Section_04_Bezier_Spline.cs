namespace ConsoleApp1.TrainingFiles.Chapter_03_Interpolation
{
    internal class Section_04_Bezier_Spline
    {
        public static void Run()
        {
            /// <BookContent> 
            /// 
            /// <header 2> Bézier Splines </header 2> 
            /// Unlike linear interpolation, which creates a jagged path between data points, Bézier Splines create smooth, continuous curves. They are defined by "control points" that influence the shape of the curve without the curve necessarily passing through them (except for the endpoints). This makes them ideal for computer graphics, path planning for robotics, and aerodynamic smoothing. 
            /// 
            /// <header 3> 1. The Mathematical Formula (Quadratic) </header 3> 
            /// The most common form is the Quadratic Bézier curve, defined by three points: :math:`P_0` (start), :math:`P_1` (control), and :math:`P_2` (end). The curve is parameterized by :math:`t`, where :math:`t` ranges from 0 to 1: 
            /// :math:`B(t) = (1 - t)^2 P_0 + 2(1 - t)t P_1 + t^2 P_2`
            /// 
            /// As :math:`t` moves from 0 to 1, the formula calculates a weighted blend of the three points, resulting in a smooth arc that "leans" toward the control point :math:`P_1`. 
            /// 
            /// <header 3> 2. Implementation in SepalSolver </header 3> 
            /// In SepalSolver, the BezierCurve method generates a series of points along a spline. You provide the array of control points and the number of segments (resolution) you wish to generate for the final path. 
            /// 
            /// <code> 
            {
                // Define three control points (X, Y)
                double[] xControl = [0.0, 5.0, 10.0], yControl = [0.0, 10.0, 0.0];

                // Generate 100 points along the smooth curve
                int resolution = 100, L = xControl.Length-1, N = L, M = 0;
                double C = 0;
                ColVec t = Linspace(0, 1, resolution), s = 1 - t;
                double nchoosek(int n, int k) =>
                    Exp(LnGamma(n + 1) - LnGamma(k + 1) - LnGamma(n - k));

                ColVec x = xControl[0]*t.Pow(L), y = yControl[0]*t.Pow(L);
                for (int i = 1; i < L; i++)
                {
                    N -= 1; M += 1;
                    C = nchoosek(L, M);
                    x += C * t.Pow(N).Times(s.Pow(M))*xControl[i];
                    y += C * t.Pow(N).Times(s.Pow(M))*yControl[i];
                }
                x += xControl[L]*s.Pow(L); y += yControl[L]*s.Pow(L);

                Scatter(xControl, yControl, "fob", 12); HoldOn();
                Plot(x, y); HoldOff();
                SaveAs("Bezier_Curve_Example.png");
            }
            /// </code> 
            /// <header 2> Examples </header 2> 
            /// <example 1> Robot Path Smoothing 
            /// A robot might calculate a path as a series of sharp turns (linear). By using these points as control points for a Bézier spline, the robot can follow a smooth trajectory that doesn't require it to come to a complete stop at every corner. 
            /// </example>
            /// </BookContent>
        }
    }
}
