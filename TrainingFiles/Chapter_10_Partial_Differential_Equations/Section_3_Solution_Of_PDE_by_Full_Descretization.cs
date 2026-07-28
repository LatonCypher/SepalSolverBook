using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_10_Partial_Differential_Equations
{
    internal class Section_3_Solution_Of_PDE_by_Full_Descretization
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// The Full Discretization Method (often referred to as direct Finite Difference Methods for space-time grids) approaches partial differential equations (PDEs) by discretizing both the spatial and temporal dimensions simultaneously onto a discrete grid.
            /// 
            /// Unlike the Method of Lines—which retains continuous time to yield an ODE system—full discretization converts the PDE directly into a system of algebraic equations that can be solved step-by-step or via matrix inversion.
            /// 
            /// <header 3> How It Works: </header 3>
            /// 
            /// The 3-Step Process 
            /// Consider the 1D transient heat equation:
            /// <math>
            /// \frac{\partial u}{\partial t} = \alpha \frac{\partial^2 u}{\partial x^2}
            /// </math>
            /// 1. Grid Generation: We partition space into steps of size :math:`\Delta x` (:math:`x_i = i \Delta x`) and time into steps of size :math:`\Delta t` (:math:`t^n = n \Delta t`). The discrete approximation to :math:`u(x_i, t^n)` is denoted as :math:`u_i^n`.
            /// 
            /// 2. Algebraic Substitution: Both the time derivative and spatial derivative are replaced with algebraic finite difference approximations. For example, using a forward difference in time and a central difference in space:
            /// <math>
            /// \frac{u_i^{n+1} - u_i^n}{\Delta t} \approx \alpha \frac{u_{i+1}^n - 2u_i^n + u_{i-1}^n}{(\Delta x)^2}
            /// </math>
            /// 3. Time Marching / Matrix Solution: Re-arranging the algebraic equation allows us to explicitly compute values at the next time level :math:`n+1` from known values at level :math:`n` (Explicit Scheme), or solve a linear system :math:`A u^{n+1} = B u^n` (Implicit Scheme).
            /// 
            ///  ## Explicit vs. Implicit Schemes
            ///  
            /// When discretizing space and time simultaneously, the evaluation point of the spatial derivative defines the nature of the solver:
            /// - Explicit Schemes (Forward Euler): Evaluate spatial derivatives entirely at the current time step $n$. Fast per iteration, but strictly bounded by stability constraints.
            /// - Implicit Schemes (Backward Euler / Crank-Nicolson): Evaluate spatial derivatives at the future time step :math:`n+1` (or a weighted blend). Unconditionally stable, but requires solving a system of linear equations at each time step.
            /// 
            /// Comparison: Full Discretization vs Method of Lines
            /// Feature | Method of Lines (MOL) | Full Discretization (FDM)
            /// Time Integration | Continuous (Handed to ODE solver) | Discrete (Fixed algebraic updates)
            /// Solvers | High-order adaptive ODE integrators | Custom time-stepping loops / Matrix solvers
            /// Error Control | Automated adaptive time-stepping | Manual step size selection (:math:`\Delta t, \Delta x`)
            /// Implementation | Abstract function interfaces | Direct matrix-vector operations
            /// 
            /// <header 3> Example 1: Explicit Method (FTCS Scheme) </header 3>
            /// The Forward-Time Central-Space (FTCS) scheme explicitly updates each grid point based on its immediate neighbors at the previous time level:
            /// <math>
            /// u_i^{n+1} = u_i^n + r \left(u_{i+1}^n - 2u_i^n + u_{i-1}^n \right), \quad r = \frac{\alpha \Delta t}{(\Delta x)^2}
            /// </math>
            /// 
            /// Stability Warning: The 1D explicit FTCS scheme is stable if and only if :math:`r \le 0.5`. Exceeding this limit causes catastrophic non-physical oscillations.
            /// 
            /// <code>
            {
                // SOLVE_HEAT_EXPLICIT Solves 1D heat equation using Explicit FTCS scheme
                // Equation: du/dt = alpha * d^2u/dx^2
                double alpha = 0.5;
                double L = 1.0;
                double t_final = 0.5;

                int Nx = 100;
                double dx = L / Nx;
                ColVec x = Linspace(0, L, Nx + 1);

                // Select dt to satisfy the stability criterion: r = alpha * dt / dx^2 <= 0.5
                double CFL = 0.45, r = CFL;
                double dt = CFL * (dx * dx) / alpha;
                int Nt = (int)(t_final / dt);

                // Initial condition: u(x,0) = sin(pi * x)
                ColVec Un = Sin(pi * x), Unp1 = Zeros(Nx + 1);

                // Plot the initial condition
                var TempProfile = Plot(x, Un, Linewidth: 2);

                byte[] Animfun(int n)
                {
                    // Internal grid point update
                    for (int i = 1; i < Nx; i++)
                        Unp1[i] = Un[i] + r * (Un[i + 1] - 2.0 * Un[i] + Un[i - 1]);

                    // Dirichlet Boundary Conditions
                    Unp1[0] = 0.0; Unp1[Nx] = 0.0;
                    Un = Unp1.Duplicate();
                    TempProfile.Ydata = Un;
                    return GetFrame();
                }
                AnimationMaker(Animfun, "TemperaturePrile_Explicit.gif", 10, Nt);
                CloseFig();
            }
            /// </code>
            /// 
            ///  <header 3> Example 2: Implicit Crank-Nicolson Scheme </header 3>
            ///  The Crank-Nicolson scheme is a second-order accurate implicit method formed by averaging the central differences at time steps :math:`n` and :math:`n+1`:
            /// <math> 
            /// -\frac{r}{2} u_{i-1}^{n+1} + (1 + r) u_i^{n+1} -\frac{r}{2} u_{i+1}^{n+1} = \frac{r}{2} u_{i-1}^n + (1 - r) u_i^n + \frac{r}{2} u_{i+1}^n
            /// </math>
            /// Expressed in matrix-vector form:
            /// <math>
            /// \mathbf{A} \mathbf{u}^{n+1} = \mathbf{B} \mathbf{u}^n
            /// </math>
            /// where :math:`\mathbf{A}` and :math:`\mathbf{B}` are tridiagonal matrices. This scheme is unconditionally stable for any choice of :math:`\Delta t`.
            /// 
            /// <code>
            {
                // SOLVE_HEAT_CRANK_NICOLSON Solves 1D heat equation via implicit matrix linear system
                double alpha = 0.5;
                double L = 1.0;
                double t_final = 0.5;

                int Nx = 100;
                double dx = L / Nx;
                double dt = 0.01;                        // Can take much larger time steps safely
                double r = alpha * dt / (dx * dx);

                ColVec x = Linspace(0, L, Nx + 1);
                ColVec Un = Sin(pi * x);

                // Plot the initial condition
                var TempProfile = Plot(x, Un, Linewidth: 2); GridOn();
                Title("Crank-Nicolson Implicit Solution");
                Xlabel("Position x"); Ylabel("Temperature T");

                // Construct Tridiagonal System A * u^{n+1} = B * u^n
                int N = Nx + 1;
                Matrix A = Zeros(N, N), B = Zeros(N, N);

                // Internal nodes
                for (int i = 1; i < Nx; i++)
                {
                    A[i, i - 1] = -0.5 * r;
                    A[i, i] = 1.0 + r;
                    A[i, i + 1] = -0.5 * r;

                    B[i, i - 1] = 0.5 * r;
                    B[i, i] = 1.0 - r;
                    B[i, i + 1] = 0.5 * r;
                }

                // Enforce Dirichlet Boundary Conditions implicitly
                A[0, 0] = 1.0; B[0, 0] = 0.0;
                A[Nx, Nx] = 1.0; B[Nx, Nx] = 0.0;
                int Nt = (int)(t_final / dt);

                byte[] Animfun(int n)
                {
                    // Internal grid point update
                    ColVec rhs = B * Un;

                    // Linear solve using SepalSolver matrix operator (Mldivide / Thomas Algorithm)
                    Un = Mldivide(A, rhs);
                    TempProfile.Ydata = Un;
                    return GetFrame();
                }
                AnimationMaker(Animfun, "TemperaturePrile_CrankNicolson.gif", 10, Nt);
            }
            /// </code>
            /// 
            /// </BookContent> 
        }
    }
}
