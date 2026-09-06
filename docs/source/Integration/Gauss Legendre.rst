Gauss Legendre
==============


Gauss-Legendre Quadrature is a numerical integration technique designed to yield maximum algebraic precision with a minimal number of function evaluations.
Unlike standard Newton-Cotes methods (such as the Trapezoidal or Simpson's rules) that use fixed, equally spaced evaluation points, Gauss-Legendre quadrature treats both the evaluation locations (nodes) and their corresponding multipliers (weights) as free variables.

Mathematical Foundation
To evaluate a definite integral over a standardized interval :math:`[-1, 1]`:

.. math::

   \int_{-1}^{1} f(x) , dx \approx \sum_{i=1}^{n} w_i f(x_i)


The nodes :math:`x_i` are selected as the roots of the n-th degree Legendre polynomial :math:`P_n(x)`.
The weights :math:`w_i` are calculated by integrating the Lagrange interpolating polynomials over the interval:

.. math::

   w_i = \frac{2}{(1 - x_i^2) [P_n'(x_i)]^2}


An n-point Gauss-Legendre rule integrates polynomials of degree up to 2n - 1 exactly.

Domain Transformation (Mapping)
For integrals over an arbitrary real interval [a, b], a linear change of variables maps the domain to [-1, 1]:

.. math::

   x(t) = \frac{b - a}{2} t + \frac{a + b}{2}

:math:`dx = \frac{b - a}{2} dt`

The transformation yields:

.. math::

   \int_{a}^{b} f(x) , dx = \frac{b - a}{2} \sum_{i=1}^{n} w_i f\left( \frac{b - a}{2} x_i + \frac{a + b}{2} \right)

---

Applied Examples (Solved via Simple Solver)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


.. admonition:: Example 1 :  Exact Polynomial Integration (2-Point Rule)

   Evaluate 
   
   .. math::
   
      \int_{0}^{2} (x^3 - 3x + 2) , dx
   
   
   .. code-block:: csharp
   
      var I = Integral(x => Polyval([1.0, 0.0, -3.0, 2.0], x), 0, 2);
      Console.WriteLine($"I = {I:F4}");
   
   
   
Ouput
   
   .. terminal::
   
      I = 2.0000
   
   
   Exact Analytical Result:
   
   .. math::
   
      \left[ \frac{x^4}{4} - \frac{3x^2}{2} + 2x \right]_0^2 = (4 - 6 + 4) - 0 = 2
   
---


.. admonition:: Example 2 :  Transcendental Function 

   
   .. math::
   
      \int_{0}^{\pi} \sin(x) , dx
   
   
   
   .. code-block:: csharp
   
      var I = Integral(x => Sin(x), 0, pi);
      Console.WriteLine($"I = {I:F4}");
   
   
   
Ouput
   
   .. terminal::
   
      I = 2.0000
   
   Exact Analytical Result:
   
   .. math::
   
      [-\cos(x)]_0^\pi = 1 - (-1) = 2
   



.. admonition:: Example 3 :  Infinite bound 

   Evaluate 
   
   .. math::
   
      \int_{0}^{\infty} e^{-x^2}(\ln(x))^2 , dx
   
   
   
   .. code-block:: csharp
   
      var I = Integral(x => Exp(-x * x) * Pow(Log(x), 2), 0, inf);
      Console.WriteLine($"I = {I:F5}");
   
   
   
Ouput
   
   .. terminal::
   
      I = 1.94752
   
   Exact Analytical Result:
   
   .. math::
   
      \int_{0}^{\infty} e^{-x^2}(\ln(x))^2 , dx = \frac{\sqrt{\pi}}{16}\left(\pi^2 + 2(\gamma + \ln(4))^2\right) = 1.94752
   
   


Multiple Integral
~~~~~~~~~~~~~~~~~
SepalSolver can handle multiple integral via `IntegralN`. IntegralN is built on Integral running over multiply direction recursively. 
It is abole to compute upto 4 dimensional integrals efficiently.


.. admonition:: Example 4 :  Hypercube domain in 3 dimensions

   Integrate the function :math:`f(x, y, z) = xyz` over the region where :math:`x` ranges from :math:`0` to :math:`1`, 
   :math:`y` ranges from :math:`1` to :math:`2`, and :math:`z` ranges from :math:`2` to :math:`3`, which can be expressed as:
   
   .. math::
   
      \int_{0}^{1} \int_{1}^{2}  \int_{2}^{3} x y z \, dz \, dy \, dx
   
   
   .. code-block:: csharp
   
      // Define the function to integrate
      Func<double, double, double, double> f = (x, y, z) => x * y * z;
   
      // Set the lower bound of x
      double x_1 = 0;
      // Set the upper bound of x
      double x_2 = 1;
      // Set the lower bound of y
      double y_1 = 1;
      // Set the upper bound of y
      double y_2 = 2;
      // Set the lower bound of z
      double z1 = 2;
      // Set the upper bound of z
      double z2 = 3;
   
      // Calculate the integral
      double integral = Integral3(f, x_1, x_2, y_1, y_2, z1, z2);
      // Print the result
      Console.WriteLine($"The triple integral of x*y*z is approximately: {integral:F4}");
   
   
   
Ouput
   
   .. terminal::
   
      The triple integral of x*y*z is approximately: 1.8750



.. admonition:: Example 5 : 

   Integrate the function :math:`f(x, y) = xy` over the region where :math:`x` ranges from :math:`0` to :math:`1`, 
   :math:`y` ranges from :math:`x^2` to :math:`2`, which can be expressed as:
   
   .. math::
   
      \int_{0}^{1} \int_{x^2}^{2} x y \, dy \, dx
   
   
   .. code-block:: csharp
   
      // Define the function to integrate
      Func<double, double, double> f = (x, y) => x * y;
   
      // Set the lower bound of x
      double x_1 = 0;
      // Set the upper bound of x
      double x_2 = 1;
      // Define the lower bound of y as a function of x
      Func<double, double> y_1 = x => x * x;
      // Set the upper bound of y
      double y_2 = 2;
   
      // Calculate the integral
      double integral = Integral2(f, x_1, x_2, y_1, y_2);
      // Print the result
      Console.WriteLine($"The triple integral of x*y*z is approximately: {integral:F4}");
   
   
   
Ouput
   
   .. terminal::
   
      The triple integral of x*y*z is approximately: 0.9167


.. admonition:: Example 6 :  Functional boundary 

   Integrate the function :math:`f(x, y, z) = xyz` over the region where :math:`x` ranges from :math:`0` to :math:`1`, 
   :math:`y` ranges from :math:`x^2` to :math:`\sqrt{x}`, and :math:`z` ranges from :math:`2` to :math:`3`, which can be expressed as:
   
   .. math::
   
      \int_{0}^{1} \int_{x^2}^{\sqrt{x}}  \int_{2}^{3} x y z \, dz \, dy \, dx
   
   
   .. code-block:: csharp
   
      // Define the function to integrate
      Func<double, double, double, double> f = (x, y, z) => x * y * z;
   
      // Set the lower bound of x
      double x_1 = 0;
      // Set the upper bound of x
      double x_2 = 1;
      // Set the lower bound of y as a function of x
      Func<double, double> y_1 = x => x * x;
      // Define the upper bound of y as a function of x
      Func<double, double> y_2 = x => Sqrt(x);
      // Set the lower bound of z
      double z_1 = 2;
      // Set the upper bound of z
      double z_2 = 3;
   
      // Calculate the integral
      double integral = Integral3(f, x_1, x_2, y_1, y_2, z_1, z_2);
      // Print the result
      Console.WriteLine($"The triple integral of x*y*z is approximately: {integral:F4}");
   
   
   
Ouput
   
   .. terminal::
   
      The triple integral of x*y*z is approximately: 0.2083


.. admonition:: Example 7 :  Functional boundary

   Integrate the function :math:`f(x, y, z) = xyz` over the region where :math:`x` ranges from :math:`0` to :math:`1`, 
   :math:`y` ranges from :math:`x^2` to :math:`\sqrt{x}`, and :math:`z` ranges from :math:`xy` to :math:`x+y`, which can be expressed as:
   
   .. math::
   
      \int_{x_1}^{x_2} \int_{x^2}^{\sqrt{x}}  \int_{xy}^{x+y} x y z \, dz \, dy \, dx
   
   
   .. code-block:: csharp
   
      // Define the function to integrate
      Func<double, double, double, double> f = (x, y, z) => x * y * z;
   
      // Set the lower bound of x
      double x_1 = 0;
      // Set the upper bound of x
      double x_2 = 1;
      // Define the lower bound of y as a function of x
      Func<double, double> y_1 = x => x * x;
      // Define the upper bound of y as a function of x
      Func<double, double> y_2 = x => Sqrt(x);
      // Define the lower bound of z as a function of x and y
      Func<double, double, double> z_1 = (x, y) => x * y;
      // Define the upper bound of z as a function of x and y
      Func<double, double, double> z_2 = (x, y) => x + y;
   
      // Calculate the integral
      double integral = Integral3(f, x_1, x_2, y_1, y_2, z_1, z_2);
      // Print the result
      Console.WriteLine($"The triple integral of x*y*z is approximately: {integral:F4}");
   
   
   
Ouput
   
   .. terminal::
   
      The triple integral of x*y*z is approximately: 0.0641
