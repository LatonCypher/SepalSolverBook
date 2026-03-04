Decline Curve Analysis
======================


**Definition:**
Decline Curve Analysis involves fitting a mathematical function to historical
production rate data over time. The three standard models defined by Arps
are Exponential, Hyperbolic, and Harmonic decline.

The Arps General Equation
~~~~~~~~~~~~~~~~~~~~~~~~~
All three decline types are derived from the general equation:

.. math::

   q(t) = \frac{q_i}{(1 + b D_i t)^{1/b}}

Where:

- :math:`q(t)` = production rate at time :math:`t`

- :math:`q_i` = initial production rate

- :math:`D_i` = initial nominal decline rate (1/time)

- :math:`b` = decline exponent (0 for exponential, 1 for harmonic)

Exponential Decline (:math:`b = 0`)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Used when the decline rate is constant. This is common in highly
undersaturated oil reservoirs or wells with constant pressure boundaries.

.. math::

   q(t) = q_i e^{-D t}


**Numerical Example:**

Given:

- :math:`q_i = 1000 \, \text{STB/day}`

- :math:`D = 0.05 \, \text{per month}`

- Find rate after 12 months:


.. math::

   q(12) = 1000 \cdot e^{-(0.05 \cdot 12)} = 1000 \cdot 0.5488 = 548.8 , \text{STB/day}



.. code-block:: csharp

   double qi = 1000; // STB/day
   double D = 0.05; // per month
   double t = 12; // months
   double q_t = qi * Exp(-D * t);
   Console.WriteLine($"Rate after 12 months = {q_t:F2} STB/day");


Ouput

.. terminal::

   Rate after 12 months = 548.81 STB/day

Hyperbolic Decline (:math:`0 < b < 1`)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
The most common decline type. The decline rate itself decreases over time.

**Numerical Example:**

Given:

- :math:`q_i = 1500 \, \text{Mscf/day}`

- :math:`D_i = 0.10 \, \text{per month}`

- :math:`b = 0.5`


.. math::

   q(t) = \frac{1500}{(1 + 0.5 \cdot 0.1 \cdot 12)^{1/0.5}} = \frac{1500}{(1.6)^2} = 585.9 \, \text{Mscf/day}



.. code-block:: csharp

   double qi = 1500;
   double Di = 0.10;
   double b = 0.5;
   double t = 12;
   double q_t = qi / Pow(1 + b * Di * t, 1 / b);
   Console.WriteLine($"Hyperbolic Rate = {q_t:F2} Mscf/day");


Ouput

.. terminal::

   Hyperbolic Rate = 585.94 Mscf/day

Cumulative Production and EUR
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
To calculate the Estimated Ultimate Recovery (EUR), we integrate the rate
over time until a **limit rate** (:math:`q_{limit}`) is reached.

**For Exponential Decline:**

.. math::

   G_p = \frac{q_i - q(t)}{D}



.. code-block:: csharp

   double qi = 1000;
   double q_limit = 50; // Economic limit
   double D = 0.05;
   double EUR = (qi - q_limit) / D;
   Console.WriteLine($"EUR (Exponential) = {EUR:F2} STB");


Ouput

.. terminal::

   EUR (Exponential) = 19000.00 STB

