Linear Interpolation
====================


Linear Interpolation
--------------------

Linear Interpolation is the simplest form of interpolation used to estimate a value between two known data points. It assumes that the change between the points follows a straight line. While :math:Polyfit seeks to find a single curve for an entire dataset, linear interpolation works "locally" between adjacent coordinates.

1. The Mathematical Formula
~~~~~~~~~~~~~~~~~~~~~~~~~~~
Given two points :math:`(x_0, y_0)` and :math:`(x_1, y_1)`, the value :math:y for any :math:x in the interval :math:`[x_0, x_1]` is calculated as:

:math:`y = y_0 + (x - x_0) \cfrac{y_1 - y_0}{x_1 - x_0}`

This formula represents the weighted average of the two endpoints based on how close :math:x is to either side.



2. Implementation in SepalSolver
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
In SepalSolver, the Interp1 method performs 1D linear interpolation. It first searches the input array to find the correct "bin" (the two closest :math:x values) and then applies the linear formula.


.. code-block:: csharp

   double[] xData = [0.0, 10.0, 20.0];
   double[] yData = [0.0, 100.0, 400.0];

   // Estimate the value at x = 5.0 (midway between 0 and 10)
   double queryX = 5.0;
   double result = Interp1(xData, yData, queryX);

   // Result: 50.0
   Console.WriteLine($"Interpolated value at {queryX} is {result}");



Ouput

.. terminal::

   Interpolated value at 5 is 50

Examples
--------


.. admonition:: Example 1 :  Reading Steam Tables

   Engineers often use tables of data where values are only provided at fixed intervals (e.g., every 5 degrees). Linear interpolation allows for the estimation of properties at specific, non-tabulated temperatures.
   
   .. code-block:: csharp
   
      double[] temps = [100, 105, 110];
      double[] pressure = [101.3, 120.8, 143.3];
   
      double currentTemp = 107.5;
      double pEst = Interp1(temps, pressure, currentTemp);
   
      Console.WriteLine($"Estimated pressure at {currentTemp}C: :math:{pEst} kPa");
   
   
   
Ouput
   
   .. terminal::
   
      Estimated pressure at 107.5C: :math:132.05 kPa


.. admonition:: Example 2 :  Lookup Tables in Control Systems

   In real-time control, complex mathematical functions are often replaced by "lookup tables" to save processing power. Linear interpolation provides a fast way to get reasonably accurate intermediate values from these tables.
   
   .. code-block:: csharp
   
      double[] rpm = [0, 1000, 2000, 3000, 4000];
      double[] torque = [0, 150, 280, 310, 290];
   
      double currentRPM = 2500;
      double tEst = Interp1(rpm, torque, currentRPM);
   
      Console.WriteLine($"Torque at {currentRPM} RPM: :math:`{tEst}` Nm");
   
   
   
Ouput
   
   .. terminal::
   
      Torque at 2500 RPM: :math:`295` Nm

Exercise: Manual Implementation
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
**Task**: Implement the core logic of a linear interpolator between two specific points.

.. code-block:: csharp

   double x0 = 2.0, y0 = 10.0;
   double x1 = 4.0, y1 = 20.0;
   double targetX = 3.0;

   // Calculate the slope (m)
   double slope = (____ - ____) / (____ - ____);

   // Calculate interpolated Y
   double interpolatedY = y0 + slope * (targetX - x0);

   // Expected: 15.0
   Console.WriteLine($"Value at 3.0: :math:{interpolatedY}");



2D Linear Interpolation (Interp2)
---------------------------------
2D Interpolation, or Bilinear Interpolation, is used to estimate values of a function :math:`z = f(x, y)` when the data is provided as a grid. This is essential in fields like meteorology (mapping temperatures across a geographic area) or structural engineering (calculating stress over a surface).

1. How it Works
~~~~~~~~~~~~~~~
Bilinear interpolation is essentially a two-step linear interpolation. To find the value at a point :math:`(x, y)` inside a rectangular cell defined by four points:

1.  Interpolate along X: Perform linear interpolation between the points in the :math:`x`-direction to find two intermediate values at the target :math:`x` coordinate.

2.  Interpolate along Y: Perform a final linear interpolation between those two intermediate values in the :math:`y`-direction to find the result :math:`z`.


2. Data Structure
~~~~~~~~~~~~~~~~~
In SepalSolver, `Interp2` requires:
* `double[] X`: A vector of coordinates for the columns.
* `double[] Y`: A vector of coordinates for the rows.
* `double[,] Z`: A 2D matrix where `Z[i, j]` is the value at `(X[j], Y[i])`.


.. code-block:: csharp

   double[] xCoords = [0.0, 1.0];
   double[] yCoords = [0.0, 1.0];
   // Define Z values at (0,0), (1,0), (0,1), (1,1)
   double[,] zGrid = {
       { 10.0, 20.0 }, // y = 0
       { 30.0, 40.0 }  // y = 1
   };

   double qX = 0.5, qY = 0.5;
   double result = Interp2(xCoords, yCoords, zGrid, qX, qY);

   // Result: 25.0 (the center of the square)
   Console.WriteLine($"Value at (0.5, 0.5): {result}");



Ouput

.. terminal::

   Value at (0.5, 0.5): 25

Examples
--------


.. admonition:: Example 1 :  Topographic Height Estimation

   Imagine you have a grid of elevation data from a survey. You want to know the height at a specific coordinate that wasn't directly measured.
   
   .. code-block:: csharp
   
      double[] longitude = [100.0, 101.0, 102.0];
      double[] latitude = [50.0, 51.0, 52.0];
      double[,] elevation = {
          { 500, 550, 600 },
          { 520, 580, 630 },
          { 540, 610, 650 }
      };
   
      double lon = 100.5, lat = 50.5;
      double height = Interp2(longitude, latitude, elevation, lon, lat);
      Console.WriteLine($"Height at ({lon}, {lat}): :math:{height} m");
   
   
   
Ouput
   
   .. terminal::
   
      Height at (100.5, 50.5): :math:537.5 m


.. admonition:: Example 2 :  Pressure Mapping

   In aerodynamics, sensors on a wing provide pressure data at specific :math:`(x, y)` points. ``Interp2`` allows the solver to reconstruct the pressure distribution across the entire surface for lift calculations.
   
   
   .. code-block:: csharp
   
      double[] x = [0, 10];
      double[] y = [0, 10];
      double[,] z = { { 0, 100 }, { 100, 200 } };
   
      // Target: Exactly 25% into the grid from the origin (2.5, 2.5)
      double targetX = 2.5;
      double targetY = 2.5;
   
      double val = Interp2(x, y, z, targetX, targetY);
   
      Console.WriteLine($"Interpolated Z: {val}");
   
   
   
Ouput
   
   .. terminal::
   
      Interpolated Z: 50


.. admonition:: Example 3 :  Reading steam table

   In thermodynamics, the properties of water and steam (such as Enthalpy :math:`h`, Entropy :math:`s`, or Specific Volume :math:`v`) are determined by two independent state variables, typically Pressure (:math:`P`) and Temperature (:math:`T`).
   
   these properties are governed by complex non-linear physics, they are often provided in massive, discrete grids known as Steam Tables.When an engineer needs the enthalpy at a specific state that isn't exactly in the table, they must use Bilinear Interpolation (Interp2).
   
   
   .. code-block:: csharp
   
   
      double[] Temperature = [1000, 1100, 1200, 1300, 1400, 1500, 1600, 1800, 2000],
          Pressure = [0.01, 0.02, 0.03, 0.04, 0.05, 0.06];
      double[,] SpecificVolume = new double[,]
      {
          {58.758, 29.379, 19.586, 14.689, 11.751, 9.7927},
          {63.373, 31.686, 21.124, 15.843, 12.674, 10.562},
          {67.988, 33.994, 22.663, 16.997, 13.598, 11.331},
          {72.604, 36.302, 24.201, 18.151, 14.521, 12.101},
          {77.219, 38.61,  25.74,  19.305, 15.444, 12.87},
          {81.834, 40.917, 27.278, 20.459, 16.367, 13.639},
          {86.45,  43.225, 28.817, 21.613, 17.29,  14.409},
          {95.68,  47.84,  31.894, 23.92,  19.136, 15.947},
          {104.91, 52.455, 34.97,  26.228, 20.982, 17.485}
      };
   
      double T = 1350, P = 0.0373;
      double sv = Interp2(Temperature, Pressure, SpecificVolume, T, P);
      Console.WriteLine($"Specific Volume of superheated water at T = {T}, P = {P} is {sv}");
   
   
   
Ouput
   
   .. terminal::
   
      Specific Volume of superheated water at T = 1350, P = 0.0373 is 20.413475
