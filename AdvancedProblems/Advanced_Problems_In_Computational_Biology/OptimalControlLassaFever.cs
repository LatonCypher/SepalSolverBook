using System;
using System.Linq;
using System.Collections.Generic;
using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;
using System.Diagnostics;

namespace ConsoleApp1.
          TrainingFiles.
          Summary_and_Conclusion.
          Advanced_Problems_In_Computational_Biology
{
    public class OptimalControlLassaFever
    {
        public class Result
        {
            public RowVec T;
            public Matrix X, U, L;
            public bool flag;
        }
        public static void Run()
        {

        }

        public static Result OptimalControl_Phd(ColVec ControlSwitch)
        {
            Result result = new Result(); double[] temp;
            double test = -1;
            double delta = 0.01; // set tolerance
            int N = 1000, Np1; // number of subdivisions
            int iter = 0;
            double Time = 300; // timespan
            double k = Time / N; // step
            RowVec T = new Indexer(0, Np1 = N + 1).Select(i => i * k).ToArray();
            Matrix tempX, tempL, tempU;


            // Assigning bounds to each of the control variables
            double u1max = 1, u2max = 1, u3max = 1, u4max = 1;

            // Pre - allocating memory space for control variables
            Matrix U = Zeros(4, Np1), Old_U; result.U = U;

            // Pre - allocating memory space for states variables
            Matrix X = Zeros(8, Np1), Old_X; result.U = U;

            //Setting the initial coditions for the state variable
            double P1 = 218541216;
            temp = [0.605389443, 0.000147656, 0.125507567, 0.059062385,
                   0.199335548, 0.002657807, 0.000516796, 0.007382798];
            X["", 0] = P1 * new ColVec(temp);

            // Pre - allocating memory space for adjoint variables
            Matrix L = Zeros(8, Np1), Old_L; result.L = L;

            double Pi = 13920, gamma_1 = 0.04715, gamma_2 = 0.04715, psi_1 = 0.1429,
            psi_2 = 0.1429, omega = 0.02726, alpha = 0.9973, d_1 = 0.002843,
            d_2 = 0.003310, d_12 = 0.04131,
            d_21 = 0.4131, sigma = 0.300, beta_1 = 1.01135, beta_2 = 3.01196,
            epsilon_1 = 0.4750, epsilon_2 = 0.4750, mu = 0.0015, tau_1 = 0.345,
            tau_2 = 0.3543, rho = 0.6, psi_3 = 0.0429,

            ko1 = gamma_1 + mu, k22 = tau_1 + d_12 + psi_1 + mu, k3 = gamma_2 + mu,
            k4 = tau_2 + d_21 + psi_2 + mu;

            // Define your Weight
            double w1 = 1, w2 = 20, w3 = 30, w4 = 1, w5 = 1, w6 = 50, w7 = 50, w8 = 1;

            ColVec dxdt(double t, ColVec x)
            {
                ColVec u = Matrix.Interp1(T, U, t); double[] dx;
                double u1 = u[0], u2 = u[1], u3 = u[2], u4 = u[3],
                       S = x[0], V = x[1], E_1 = x[2], I_1 = x[3], E_2 = x[4], I_2 = x[5], H = x[6], R = x[7],
                       Total = S + V + E_1 + I_1 + E_2 + I_2 + H + R,
                       Gamma_1 = beta_1 * (I_1 + epsilon_1 * H) / Total, Gamma_2 = beta_2 * (I_2 + epsilon_2 * H) / Total;

                dx = [Pi + omega * R - (Gamma_1 + Gamma_2) * S + alpha * V - (u1 + mu) * S,
                      u1 * S - sigma*(Gamma_1 + Gamma_2) * V - (alpha + mu) * V,
                      (S + sigma * V) * Gamma_1 - (gamma_1 + mu) * E_1,
                      gamma_1 * E_1 - (u2 + d_1 + psi_1 + mu) * I_1,
                      (S + sigma * V) * Gamma_2 - (gamma_2 + mu) * E_2,
                      gamma_2 * E_2 - (u3 + d_2 + psi_2 + mu) * I_2,
                      u2 * I_1 +u3 * I_2 - (u4 + mu + d_12 + d_21) * H,
                      psi_1 * I_1 + psi_2 * I_2 + u4 * H - (omega + mu) * R];
                return dx;
            }

            ColVec dldt(double t, ColVec l)
            {
                ColVec x = Matrix.Interp1(T, X, t), u = Matrix.Interp1(T, U, t); double[] dl;
                double u1 = u[0], u2 = u[1], u3 = u[2], u4 = u[3],
                       S = x[0], V = x[1], E_1 = x[2], I_1 = x[3], E_2 = x[4], I_2 = x[5], H = x[6], R = x[7],
                       Total = S + V + E_1 + I_1 + E_2 + I_2 + H + R, Total2 = Total * Total,
                       l1 = l[0], l2 = l[1], l3 = l[2], l4 = l[3], l5 = l[4], l6 = l[5], l7 = l[6], l8 = l[7];

                dl = [-(w1 + l1 * (-(-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * S - beta_1 * (H * epsilon_1 + I_1) / Total - beta_2 * (H * epsilon_2 +  I_2) / Total - u1 - mu) + l2 * (u1 - sigma * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * V) + l3 * (beta_1 * (H * epsilon_1 +  I_1) / Total - (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2) + l5 * (beta_2 * (H * epsilon_2 + I_2) / Total - (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2)),
                     -(l1 * (-(-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * S + alpha) + l2 * (-sigma * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * V - sigma * (beta_1 * (H * epsilon_1 + I_1) / Total + beta_2 * (H * epsilon_2 + I_2) / Total) - alpha - mu) + l3 * (sigma * beta_1 * (H * epsilon_1 + I_1) / Total - (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2) + l5 * (sigma * beta_2 * (H * epsilon_2 + I_2) / Total - (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2)),
                     -(-l1 * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * S - l2 * sigma * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * V + l3 * (-(V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2 - gamma_1 - mu) + l4 * gamma_1 - l5 * (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2),
                     -(w2 - l1 * (beta_1 / Total - beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * S - l2 * sigma * (beta_1 / Total - beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * V + l3 * ((V * sigma + S) * beta_1 / Total - (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2) + l4 * (-u2 - d_1 - psi_1 - mu) - l5 * (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2 + l7 * u2 + l8 * psi_1),
                     -(-l1 * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * S - l2 * sigma * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * V - l3 * (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2 + l5 * (-(V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2 - gamma_2 - mu) + l6 * gamma_2),
                     -(w3 - l1 * (-beta_1 * (H * epsilon_1 + I_1) / Total2 + beta_2 / Total - beta_2 * (H * epsilon_2 + I_2) / Total2) * S - l2 * sigma * (-beta_1 * (H * epsilon_1 + I_1) / Total2 + beta_2 / Total - beta_2 * (H * epsilon_2 + I_2) / Total2) * V - l3 * (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2 + l5 * ((V * sigma + S) * beta_2 / Total - (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2) + l6 * (-u3 - d_2 - psi_2 - mu) + l7 * u3 + l8 * psi_2),
                     -(w4 - l1 * (beta_1 * epsilon_1 / Total - beta_1 * (H * epsilon_1 + I_1) / Total2 + beta_2 * epsilon_2 / Total - beta_2 * (H * epsilon_2 + I_2) / Total2) * S - l2 * sigma * (beta_1 * epsilon_1 / Total2 - beta_1 * (H * epsilon_1 + I_1) / Total2 + beta_2 * epsilon_2 / Total - beta_2 * (H * epsilon_2 + I_2) / Total2) * V + l3 * ((V * sigma + S) * beta_1 * epsilon_1 / Total - (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2) + l5 * ((V * sigma + S) * beta_2 * epsilon_2 / Total2 - (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2) + l7 * (-u4 - mu - d_12 - d_21) + l8 * u4),
                     -(l1 * (omega - (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * S) - l2 * sigma * (-beta_1 * (H * epsilon_1 + I_1) / Total2 - beta_2 * (H * epsilon_2 + I_2) / Total2) * V - l3 * (V * sigma + S) * beta_1 * (H * epsilon_1 + I_1) / Total2 - l5 * (V * sigma + S) * beta_2 * (H * epsilon_2 + I_2) / Total2 + l8 * (-omega - mu))];
                return dl;
            }

            while (test < 0 && iter < 20)
            {
                //This step stores current values  as previous ones
                //Storing the preceeding values of the control variables
                Old_U = U.Duplicate();

                //Storing the preceeding values of the state variables
                Old_X = X.Duplicate();

                //Storing the preceeding values of the adjoint variables
                Old_L = L.Duplicate();

                //forward sweep with RK order 4  to evaluate the state variables
                for (int i = 0; i < N; i++) X["", i + 1] = rk4(dxdt, i * k, X["", i], k);

                //Backward sweep with RK order 4
                for (int j = Np1; j-- > 1;) L["", j - 1] = rk4(dldt, j * k, L["", j], -k);

                // Update U
                U[0, ""] = Max(0, Min(X[0, ""].Times(L[0, ""] - L[1, ""]) / w5, u1max));
                U[1, ""] = Max(0, Min(X[3, ""].Times(L[3, ""] - L[6, ""]) / w6, u2max));
                U[2, ""] = Max(0, Min(X[5, ""].Times(L[5, ""] - L[6, ""]) / w7, u3max));
                U[3, ""] = Max(0, Min(X[6, ""].Times(L[6, ""] - L[7, ""]) / w8, u4max));

                U = 0.5 * ControlSwitch.Times(U + Old_U);
                tempX = delta * Abs(X).Sum(2) - Abs(Old_X - X).Sum(2);
                tempL = delta * Abs(L).Sum(2) - Abs(Old_L - L).Sum(2);
                tempU = delta * Abs(U).Sum(2) - Abs(Old_U - U).Sum(2);
                temp = [tempX.Min(), tempL.Min(), tempU.Min()];
                test = temp.Min();
                iter = iter + 1;
            }

            return result;
        }
    }
}
