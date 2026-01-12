Higher Order Differential Equations
===================================


.. Admonition:: Example 3 : 

   Solve a second order ODE (simple harmonic oscillator) by first converting to system of first order equation and 
   then solve the system of first-order ODEs representing the simple harmonic oscillator:
   
   .. math:: \frac{d^2y}{dt^2} = -4y
   .. math:: y_0 = 0, \quad y'_0 = 5, \quad t = [0, 10];
   
   | To solve this, we first transform the problem into a system of first order differential equations:
   | Let :math:`v = \cfrac{dy}{dt}`,   hence :math:`\cfrac{dv}{dt} = -4y, \quad y_0 = 0, \quad v_0 = 5`, 
   | Now we have 2 equations :math:`\cfrac{dy}{dt} = v, \quad \cfrac{dv}{dt} = -4y`
   
   .. code-block:: csharp
   
      // Simple Harmonic Oscillator
      double[] dydt(double t, double[] y) =>
          [y[1], -4*y[0]];
      (ColVec T, Matrix Y) = Ode45(dydt, [0, 5], [0, 10]);
      Plot(T, Y, Linewidth: 2);
      SaveAs("Simple_Harmonic_Oscillator.png");
   
   
   .. figure:: images/Simple_Harmonic_Oscillator.png
      :align: center
      :alt: Simple_Harmonic_Oscillator.png
   


.. Admonition:: Example 4 : 

   lets look at harmonic oscillator with damping
   
   .. math:: m\cfrac{d^2y}{dt^2} + c\cfrac{dy}{dt} + ky = 0
   .. math:: y_0 = 0.7, \quad y'_0 = 0, \quad t = [0, 30];
   
   | where :math:`m` is the mass, :math:`c` is the damping coefficient, and :math:`k` is the spring constant.
   | To solve this, we first transform the problem into a system of first order differential equations:
   
   | Let :math:`v = \cfrac{dy}{dt}`,   hence :math:`\cfrac{dv}{dt} =  -\cfrac{c}{m}v - \cfrac{k}{m}y, \quad y_0 = 0.7, \quad v_0 = 0`,
   | Now we have 2 equations :math:`\cfrac{dy}{dt} = v, \quad \cfrac{dv}{dt} = -\cfrac{c}{m}v - \cfrac{k}{m}y`
   
   
   .. code-block:: csharp
   
      // Damped System
      double k = 3.5, c = 0.5, m = 2.0, k_m = k/m, c_m = c/m;
      double[] dydt(double t, double[] y) =>
          [y[1], -k_m * y[0] - c_m * y[1]];
      (ColVec T, Matrix Y) = Ode45(dydt, [0.7, 0], [0, 30]);
      Plot(T, Y, Linewidth: 2);
      SaveAs("Damped_Harmonic_Oscillator.png");
   
   
   .. figure:: images/Damped_Harmonic_Oscillator.png
      :align: center
      :alt: Damped_Harmonic_Oscillator.png
   


.. Admonition:: Example 5 : 

   
   .. code-block:: csharp
   
      // Predator Prey Model
      double alpha = 0.01, beta = 0.02;
      double[] dydt(double t, double[] y) =>
          [(1 - alpha*y[1])*y[0], (-1 + beta*y[0])*y[1]];
      (ColVec T, Matrix Y) = Ode45(dydt, [20, 20], [0, 15]);
      Plot(T, Y, Linewidth: 2);
      SaveAs("Predator_Prey_Model.png");
   
   
   .. figure:: images/Predator_Prey_Model.png
      :align: center
      :alt: Predator_Prey_Model.png
   


.. Admonition:: Example 6 : 

   
   .. code-block:: csharp
   
      // Blausius Boundary Layer
   
      // define function
      double[] dydt(double t, double[] y) =>
          [y[1], y[2], -0.5 * y[2] * y[0]];
   
      // set time span
      double[] tspan = [0, 6];
   
      double[] y0 = [0, 0, 0.5];
      (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
   
      // plot the result
      Plot(T, Y, Linewidth: 2);
      Legend(["f", "f'", "f''"], UpperLeft);
      Axis([0, 6, 0, 2]); Xlabel("η");
      Title("Blasius Boundary Layer");
      SaveAs("Blasius_Boundary_Layer.png");
   
   
   .. figure:: images/Blasius_Boundary_Layer.png
      :align: center
      :alt: Blasius_Boundary_Layer.png
   
