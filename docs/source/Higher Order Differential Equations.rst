Higher Order Differential Equations
===================================


Introduction
------------
A higher‑order differential equation involves derivatives of order two or higher.
Many physical systems such as oscillations, mechanical vibrations, and electrical circuits
are naturally modeled by second‑order or higher‑order equations.

Examples:

- Simple harmonic oscillator (second order)
- Damped oscillator
- Forced oscillator
- Beam deflection problems
- RLC electrical circuits

To use SepalSolver to solve higher‑order ODEs, they have to first be converted into equivalent systems
of first‑order equations.

General Form
------------
A general n‑th order ODE can be written as:


.. math::

   \cfrac{d^n y}{dt^n} = f(t, y, y', y'', \dots, y^{(n-1)})


To solve with SepalSolver, we introduce variables:

.. math::

   y_1 = y,\; y_2 = y',\; y_3 = y'',\;\dots,\; y_n = y^{(n-1)}


Then the system becomes:

.. math::

   \begin{array}{c}
   y_1' = y_2 \\
   y_2' = y_3 \\
   \vdots \\
   y_n' = f(t, y_1, y_2, \dots, y_n)
   \end{array}



.. code-block:: csharp
 {
     // Define the ODE system as a function
     double[] dydt(double t, double[] y) => [y[1], y[2], ..., f(t,y)];
 }

Examples
~~~~~~~~
Here are examples of converting and solving various higher‑order ODEs using SepalSolver.


.. Admonition:: Example 1 :  Simple Harmonic Oscillator (Second Order)

   | Equation: :math:`y'' + y = 0`,
   | Converted system:
   
   .. math::
   
      \begin{eqnarray}
      y_1' &= y_2 \\
      y_2' &= -y_1
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
       double[] dydt(double t, double[] y) => [ y[1], -y[0] ];
       double[] y0 = [1.0, 0.0]; // y(0)=1, y'(0)=0
       double[] tspan = [0, 20];
       (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
       Plot(T, Y, Linewidth: 2);
       Legend(["y", "y'"], UpperLeft);
       Title("Simple Harmonic Oscillator");
       SaveAs("HigherOrder_SHO.png");
   
   
   .. figure:: images/HigherOrder_SHO.png
      :align: center
      :alt: HigherOrder_SHO.png
   


.. Admonition:: Example 2 :  Damped Oscillator

   | Equation: :math:`y'' + 2β y' + ω^2 y = 0`,
   | Converted system:
   
   .. math::
   
      \begin{eqnarray}
      y_1' &= y_2 \\
      y_2' &= -2β y_2 - ω^2 y_1
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
       double beta = 0.1, omega = 2.0;
       double[] dydt(double t, double[] y) => [ y[1], -2*beta*y[1] - omega*omega*y[0] ];
       double[] y0 = [1.0, 0.0];
       double[] tspan = [0, 20];
       (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
       Plot(T, Y, Linewidth: 2);
       Legend(["y", "y'"], UpperLeft);
       Title("Damped Oscillator");
       SaveAs("HigherOrder_Damped.png");
   
   
   .. figure:: images/HigherOrder_Damped.png
      :align: center
      :alt: HigherOrder_Damped.png
   


.. Admonition:: Example 3 :  Forced Oscillator

   | Equation: :math:`y'' + y = cos(t)`,
   | Converted system:
   
   .. math::
   
      \begin{eqnarray}
      y_1' &= y_2 \\
      y_2' &= -y_1 + \cos(t)
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
       double[] dydt(double t, double[] y) => [ y[1], -y[0] + Cos(t) ];
       double[] y0 = [0.0, 0.0];
       double[] tspan = [0, 20];
       (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
       Plot(T, Y, Linewidth: 2);
       Legend(["y", "y'"], UpperLeft);
       Title("Forced Oscillator");
       SaveAs("HigherOrder_Forced.png");
   
   
   .. figure:: images/HigherOrder_Forced.png
      :align: center
      :alt: HigherOrder_Forced.png
   


.. Admonition:: Example 4 :  RLC Circuit

   | Equation: :math:`L i'' + R i' + (1/C) i = 0`,
   | Converted system:
   
   .. math::
   
      \begin{eqnarray}
      i_1' &= i_2 \\
      i_2' &= -(R/L) i_2 - (1/(L C)) i_1
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
       double L = 1.0, R = 0.5, C = 0.2;
       double[] dydt(double t, double[] i) => [ i[1], -(R/L)*i[1] - (1.0/(L*C))*i[0] ];
       double[] i0 = [1.0, 0.0];
       double[] tspan = [0, 20];
       (ColVec T, Matrix Y) = Ode45(dydt, i0, tspan);
       Plot(T, Y, Linewidth: 2);
       Legend(["i", "i'"], UpperLeft);
      Title("RLC Circuit");
       SaveAs("HigherOrder_RLC.png");
   
   
   .. figure:: images/HigherOrder_RLC.png
      :align: center
      :alt: HigherOrder_RLC.png
   


.. Admonition:: Example 5 :  Third‑Order Example

   | Equation: :math:`y''' - y = 0`,
   | Converted system:
   
   .. math::
   
      \begin{eqnarray}
      y_1' &= y_2 \\
      y_2' &= y_3 \\
      y_3' &= y_1
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
       double[] dydt(double t, double[] y) => [ y[1], y[2], y[0] ];
       double[] y0 = [1.0, 0.0, 0.0];
       double[] tspan = [0, 3];
       (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
       Plot(T, Y, Linewidth: 2);
       Legend(["y", "y'", "y''"], UpperLeft);
       Title("Third‑Order Example");
       SaveAs("HigherOrder_Third.png");
   
   
   .. figure:: images/HigherOrder_Third.png
      :align: center
      :alt: HigherOrder_Third.png
   

