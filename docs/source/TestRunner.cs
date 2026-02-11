internal class TestRunner
{
    public static void RunTests()
    {
		
		double[] dydt(double t, double[] y) => [ y[1], y[2], y[0] ];
		double[] y0 = [1.0, 0.0, 0.0];
		double[] tspan = [0, 3];
		(ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
		Plot(T, Y, Linewidth: 2);
		Legend(["y", "y'", "y''"], UpperLeft);
		Title("Third‑Order Example");
		SaveAs("HigherOrder_Third.png");
		
    }
}
