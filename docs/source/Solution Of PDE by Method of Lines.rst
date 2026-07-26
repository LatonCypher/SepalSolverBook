Solution Of PDE by Method of Lines
==================================

The **Method of Lines (MOL)** is a powerful numerical technique used to solve partial differential equations (PDEs), particularly those that are time-dependent (evolutionary).

Instead of discretizing all dimensions(space and time) simultaneously, the core idea is to discretize the spatial variables while leaving the time variable continuous. This transforms a single PDE into a system of coupled **Ordinary Differential Equations(ODEs)**.



How It Works: The 3-Step Process
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Imagine you are solving the heat equation: 


.. math::

   \frac{\partial u}{\partial t} = \alpha \frac{\partial^2 u}{\partial x^2}


1. **Spatial Discretization**: You divide the spatial domain into a grid of points(e.g., :math:`x_1, x_2, \cdot, x_n`). You replace the spatial derivatives :math:`(\frac{\partial^2 u}{\partial x^2})` with finite difference approximations, such as:


.. math::

   \frac{\partial^2 u}{\partial x^2} = \frac{u_{i+1} - 2u_i + u_{i-1}}{(\Delta x)^2}


2. **Conversion to ODEs**: By applying this at every grid point, the PDE becomes a set of ODEs, one for each point :


.. math::

   \frac{\partial u_i(t)}{\partial t} \approx \alpha \frac{u_{i+1}(t) - 2u_i(t) + u_{i-1}(t)}{(\Delta x)^2}


3. **Temporal Integration**: Now that you have a system of ODEs, you can use standard, high-performance ODE solvers like **Runge-Kutta** or **Euler’s method** to step forward in time.



## Why Use It? (Advantages)

- **Leverages Existing Tech**: You can use sophisticated, pre-built ODE solvers(like `NDSolve` in Mathematica or `ode45` in MATLAB) that automatically handle error control and adaptive time-stepping.
- **Flexibility**: You can use different spatial discretization methods, such as finite differences, finite elements, or spectral methods, depending on the geometry of your problem
- **Simplification**: It breaks a complex multi-dimensional problem into a more manageable "line-by-line" temporal evolution.

## Limitations

- **Stiffness**: The resulting system of ODEs is often "stiff," meaning you may need implicit solvers to avoid tiny, inefficient time steps.
- **Not for All PDEs**: It is designed for "evolutionary" problems (parabolic and hyperbolic). It cannot be used directly for purely "steady-state" elliptic equations(like Laplace’s equation) without adding a "pseudo-time" variable.



.. list-table:: Comparison at a Glance
   :header-rows: 1

   * - Feature
     - Standard Finite Difference(FDM)
     - Method of Lines(MOL)
   * - **Time Treatment**
     - Discretized at the start
     - Kept continuous initially
   * - **Solving Logic**
     - Solves the whole grid at once
     - Solves ODEs along "lines" of time
   * - **Software**
     - Requires custom time-stepping
     - Uses off-the-shelf ODE solvers

In sepalsolver, the nnumerical solution of pde (Pdepe) is built on Ode45a: 
our differential algebraic equation solver that relies on L-stable diagonally
implicit Runge Kutta. This allows it to handle the boundary conditions implicitly and 
also correct the egde values in situation where the initial condition is not consitent 
with the boundary condition

Example: Solving the Heat Equation Numerically
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Consider the one-dimensional heat equation:


.. math::

   \frac{\partial u}{\partial t} = \alpha \frac{\partial^2 u}{\partial x^2}


with initial condition :math:`u(x,0) = \sin(\pi x)` and boundary conditions :math:`u(0,t) = u(1,t) = 0`.

.. code-block:: csharp

   // SOLVE_HEAT_EQUATION Solves the 1D heat equation using pdepe
   // Equation:  du/dt = alpha * d^2u/dx^2
   // Domain:    x in [0, 1],  t in [0, 0.5]
   // IC:        u(x,0) = sin(π x)
   // BC:        u(0,t) = 0,  u(1,t) = 0
   //
   // 1. Model Parameters & Mesh Setup
   double alpha = 0.5;       // Thermal diffusivity coefficient
   int m = 0;                // 0 = Slab / Cartesian coordinates
   double L = 1;             // Length of the rod
   double t_final = 0.5;     // Final simulation time

   double[] x = Linspace(0, L, 101);        // 51 spatial grid points
   double[] t = Linspace(0, t_final, 6);   // 6 time steps

   // 2. Defines the PDE components: c* du/ dt = x ^ (-m) * d / dx(x ^ m * f) + s
   (double c, double f, double s) pdefun(double x, double t, double u, double dudx) =>
       (1, alpha * dudx, 0);

   // 3. Initial sinusoidal temperature distribution
   double icfun(double x) => Sin(pi * x);

   // 4. Boundary Conditions defined as: p(x, t, u) + q(x, t) * f = 0
   (double pl, double ql, double pr, double qr) bcfun(double xl, double ul, double xr, double ur, double t) => (ul, 0, ur, 0);

   // 5. Solve the PDE
   (var T, var U) = Pdepe(m, pdefun, icfun, bcfun, x, t);

   // Subplot 2: 2D Temperature Profiles at Selected Times
   Plot(x, U, Linewidth: 2); GridOn();
   Xlabel("Position x"); Ylabel("Temperature T");
   Title("Temperature vs. Position over Time");
   Legend(T.Select(t => $"t = {t:0.00}"));
   SaveAs("Temperature.png");


.. figure:: images/Temperature.png
   :align: center
   :alt: Temperature.png


