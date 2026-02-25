Trapezoidal Method
==================

Trapezoidal Integration in SepalSolver
--------------------------------------
The Trapezoidal Rule is a numerical method used to approximate the definiintegral of a function. It works by approximating the region under the graph of the function :math: f(x) as a trapezoid and calculating its area.

Mathematical Definition
~~~~~~~~~~~~~~~~~~~~~~~
To compute the integral over the interval :math:`[a, b]`, we partition the interval into :math:`n` sub-intervals, each of width :math:`h = \cfrac{b-a}{n}`. The composite trapezoidal rule is defined as:

.. math::

   \int_{a}^{b}f(x) , dx \approx \cfrac{h}{2} \left[f(x_0) + 2 \sum_{i=1}^{n-1} f(x_i) + f(x_n) \right]


SepalSolver Implementation: Manual Approach
Writing the algorithm manually allows for a deeper understanding of how the weighting of the endpoints :math:x_0 and :math:x_n differs from the interior points.

.. code-block:: csharp

   // Define the function and interval
   double a = 0;                      // Lower bound
   double b = pi;                     // Upper bound
   int n = 50;                        // Number of segments
   double h = (b - a) / n;            // Step size

   // Generate points
   ColVec x = Linspace(a, b, n+1); 
   ColVec y = Sin(x);
   
   // The first and last points are multiplied by 1
   // The internal points are multiplied by 2
   double integral_result = (h/2) * (y[0] + 2*y[1..^2].Sum() + y[^1]);
   Console.WriteLine($"The approximate integral is: \n{integral_result}");


Ouput

.. terminal::

   The approximate integral is: 
   1.9953967383788942

Error Analysis
~~~~~~~~~~~~~~
The error in the trapezoidal rule, often denoted as :math:`E_t`, is proportional to the square of the step size :math:`h`. Specifically, for a function that is twice continuously differentiable, the error is:

.. math::

   E_t = -\frac{(b-a)}{12}h^2 f''(\xi)


where :math:`\xi` is some number in the interval :math:`[a, b]`. This indicates that the method is :math: `O(h^2)`, meaning that halving the step size :math:`h` will approximately reduce the error by a factor of four.



.. list-table:: Comparison of Numerical Integration Methods
   :header-rows: 1

   * - Method
     - Accuracy Order
     - Weighting Logic
   * - Left Riemann
     - :math:`O(h)`
     - Uses :math:`f(x_{ i-1})`
   * - Trapezoidal
     - :math:`O(h^2)`
     - Average of endpoints
   * - Simpson's
     - :math:`O(h^4)`
     - Parabolic fit (1-4-1)

.. note::

   When using discrete data where the spacing :math: h is not constant, the manual summation above must be adjusted, or you should use SepalSolver's `Trapz(x, y)` function, which handles non-uniform spacing automatically.



.. code-block:: csharp


   // Define the function to be integrated
   Func<double, double> f = x => Sin(x);
   // Perform the integration using the trapezoidal rule
   double result = Trapz(f, 0, pi);
   // Print the result
   Console.WriteLine($"The integral of sin(x) from {0} to pi is approximately {result}");


Ouput

.. terminal::

   The integral of sin(x) from 0 to pi is approximately 2.000000000000283
