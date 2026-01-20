Solution Of PDE by Method of Lines
==================================

The **Method of Lines (MOL)** is a powerful numerical technique used to solve partial differential equations (PDEs), particularly those that are time-dependent (evolutionary).

Instead of discretizing all dimensions(space and time) simultaneously, the core idea is to discretize the spatial variables while leaving the time variable continuous. This transforms a single PDE into a system of coupled **Ordinary Differential Equations(ODEs)**.
            

---

## How It Works: The 3-Step Process

Imagine you are solving the heat equation: 

1. **Spatial Discretization**: You divide the spatial domain into a grid of points(e.g., ). You replace the spatial derivatives() with finite difference approximations, such as:


2. **Conversion to ODEs**: By applying this at every grid point, the PDE becomes a set of ODEs, one for each point :


3. **Temporal Integration**: Now that you have a system of ODEs, you can use standard, high-performance ODE solvers like **Runge-Kutta**or **Euler’s method**to step forward in time.

---

## Why Use It? (Advantages)

- **Leverages Existing Tech**: You can use sophisticated, pre-built ODE solvers(like `NDSolve` in Mathematica or `ode45` in MATLAB) that automatically handle error control and adaptive time-stepping.
- **Flexibility**: You can use different spatial discretization methods, such as finite differences, finite elements, or spectral methods, depending on the geometry of your problem
- **Simplification**: It breaks a complex multi-dimensional problem into a more manageable "line-by-line" temporal evolution.

## Limitations

- **Stiffness**: The resulting system of ODEs is often "stiff," meaning you may need implicit solvers to avoid tiny, inefficient time steps.
- **Not for All PDEs**: It is designed for "evolutionary" problems (parabolic and hyperbolic). It cannot be used directly for purely "steady-state" elliptic equations(like Laplace’s equation) without adding a "pseudo-time" variable.

---

### Comparison at a Glance

.. list-table:: 
   :header-rows: 1

   * - Feature
     - Standard Finite Difference(FDM)
     - Method of Lines(MOL)
     - 
   * - **Time Treatment**
     - Discretized at the start
     - Kept continuous initially
   * - **Solving Logic**
     - Solves the whole grid at once
     - Solves ODEs along "lines" of time
   * - **Software**
     - Requires custom time-stepping
     - Uses off-the-shelf ODE solvers
     - 
   * - 
