using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Process_Engineering
{
    public static class Two_Phase_Flash
    {
        public static void Run()
        {
            double[] z = [0.3, 0.25, 0.45], K = [3.20, 1.80, 0.55];
            double f(double V) => 
                z.Zip(K, (zi, Ki) => zi * (Ki - 1) / (1 + V * (Ki - 1))).Sum();
            double V = Fzero(f, [0.0, 1.0]);
            Console.WriteLine($"Vapor fraction V = {V:F4}");
            Console.WriteLine($"RachfordRice residual f(V) = {f(V):F4}");
        }
    }
}
