Inflow Performance Relation
===========================

**Definition:**
The Inflow Performance Relationship (IPR) describes the relationship between 
the bottom-hole flowing pressure (p_wf) and the production rate (q) of a well. 
It is a fundamental tool in reservoir engineering used to evaluate well 
productivity and forecast performance under different operating conditions.

IPR Above Bubble Point
~~~~~~~~~~~~~~~~~~~~~~
the reservoir pressure is above the bubble point pressure, the fluid 
remains single-phase (oil only). The relationship is linear and can be 
expressed as:

.. math::

   q = J \cdot (p_r - p_{wf})

Where:

- :math:`q` = production rate (STB/day) 

- :math:`J` = productivity index (STB/day/psi)

- :math:`p_r` = average reservoir pressure (psi)

- :math:`p_{wf}` = bottom-hole flowing pressure (psi)

**Numerical Example:**

Given:

- :math:`J = 2 \, \text{STB/day/psi}`

- :math:`p_r = 3000 \, \text{psi}`

- :math:`p_{wf} = 2500 \, \text{psi}`


.. math::

   q = 2 \cdot (3000 - 2500) = 1000 \, \text{STB/day}



.. code-block:: csharp

   double J = 2; // STB/day/psi    
   double p_r = 3000; // psi
   double p_wf = 2500; // psi
   double q = J * (p_r - p_wf); // STB/day
   Console.WriteLine($"Production Rate (q) = {q} STB/day");


Ouput

.. terminal::

   Production Rate (q) = 1000 STB/day

IPR Below Bubble Point
~~~~~~~~~~~~~~~~~~~~~~
When the reservoir pressure falls below the bubble point, gas evolves from 
solution, and the relationship becomes non-linear. Vogel’s empirical equation 
is commonly used:

.. math::

   \frac{q}{q_{max}} = 1 - 0.2 \cdot \frac{p_{wf}}{p_r} - 0.8 \cdot \left(\frac{p_{wf}}{p_r}\right)^2


Where:

-:math:`q_{max}` = maximum flow rate at :math:`p_{wf} = 0`

**Numerical Example:**

Given:

- :math:`q_{max} = 2000 \, \text{STB/day}`

- :math:`p_r = 2500 \, \text{psi}`

- :math:`p_{wf} = 1000 \, \text{psi}`


.. math::

   \frac{q}{2000} = 1 - 0.2 \cdot \frac{1000}{2500} - 0.8 \cdot \left(\frac{1000}{2500}\right)^2\\
   \frac{q}{2000} = 1 - 0.08 - 0.128 = 0.792\\
   q = 2000 \cdot 0.792 = 1584 \, \text{STB/day}



.. code-block:: csharp

   double q_max = 2000; // STB/day
   double p_r = 2500; // psi
   double p_wf = 1000; // psi
   double q = q_max * (1 - 0.2 * (p_wf / p_r) - 0.8 * Pow(p_wf / p_r, 2)); // STB/day
   Console.WriteLine($"Production Rate (q) = {q} STB/day");


Ouput

.. terminal::

   Production Rate (q) = 1583.9999999999998 STB/day

Flow Efficiency and Skin
~~~~~~~~~~~~~~~~~~~~~~~~
**Flow Efficiency (FE):**
Flow efficiency is a measure of how effectively a well produces compared to an 
ideal, undamaged well. It is defined as:


.. math::

   FE = \frac{q_{actual}}{q_{ideal}}



.. code-block:: csharp

   double q_ideal = 1584; // STB/day (from previous example)
   double q_act = 1200; // STB/day (maximum flow rate)
   double FE = q_act / q_ideal; // Flow Efficiency
   Console.WriteLine($"Flow Efficiency (FE) = {FE:P2}");


Ouput

.. terminal::

   Flow Efficiency (FE) = 75.76%

**Skin Factor (s):**
Skin represents additional pressure drop caused by near-wellbore damage or stimulation. The productivity index with skin is:

.. math::

   J_s = \frac{J \ln(r_e/r_w)}{\ln(r_e/r_w) + s}


Where:

- :math:`r_e` = drainage radius

- :math:`r_w` = wellbore radius

- :math:`s` = skin factor

A positive skin reduces productivity, while a negative skin (stimulation) increases productivity.
Numerical Example with Pressure Drop
Consider a reservoir with:

- :math:`p_r = 3000 \, \text{psi}`

- Bubble point pressure :math:`p_b = 2500 \, \text{psi}`

- :math:`q_{max} = 2000 \, \text{STB/day}`

- :math:`J = 2 \, \text{STB/day/psi}`

- :math:`r_e/r_w = 1000`

- :math:`s = +3`

Case 1: **Above Bubble Point** (:math:`p_{wf} = 2800 \, \text{psi}`)

.. math::

   J_s = \cfrac{2 \ln(1000)}{\ln(1000) + 3} \approx  1.3944
   q = 1.3944 \cdot(3000 - 2800) = 278.9 \, \text{ STB/day}



.. code-block:: csharp

   double q_max = 2000; // STB/day
   double p_r = 3000; // psi
   double p_wf = 2800; // psi
   double J = 2; // STB/day/psi
   double r_e_r_w = 1000; // dimensionless
   double s = 3; // dimensionless
   double J_s = J / (1 + s / Log(r_e_r_w)); // STB/day/psi
   double q = J_s * (p_r - p_wf); // STB/day
   Console.WriteLine($"Adjusted Productivity Index (J_s) = {J_s:F4} STB/day/psi");


Ouput

.. terminal::

   Adjusted Productivity Index (J_s) = 1.3944 STB/day/psi


Case 2: **Below Bubble Point** (:math:`p_{wf} = 2000 \, \text{psi}`)

.. math::

   \frac{q}{2000} = 1 - 0.2 \cdot \frac{2000}{3000} - 0.8 \cdot \left(\frac{2000}{3000}\right)^2
   \frac{q}{ 2000} = 1 - 0.133 - 0.356 = 0.511
   q = 2000 \cdot 0.511 = 1022 \, \text{ STB/day}


Adjusted for skin: q_actual = 1022 \cdot \frac{J_s}{J} = 1022 \cdot \frac{1.3944}{2} = 712.5 \, \text{STB/day}


.. code-block:: csharp

   double q_max = 2000; // STB/day
   double p_wf = 1000; // psi
   double p_r = 2500; // psi
   double J = 2;
   double r_e_r_w = 1000;
   double s = 3;
   double J_s = J / (1 + s / Log(r_e_r_w)); // STB/day/psi
   double q_ideal = q_max * (1 - 0.2 * (p_wf / p_r) - 0.8 * Pow(p_wf / p_r, 2)); // STB/day


Case 3: **At Zero Bottom-Hole Pressure** (:math:`p_{wf} = 0`)

.. math::

   q = q_{max} = 2000 \, \text{STB/day}

Adjusted for skin: q_actual = 2000 \cdot \frac{1.3944}{2} = 1394.4 \, \text{STB/day}


.. code-block:: csharp

   double q_max = 2000;
   double q_act = q_max * 1.3944/2;
   Console.WriteLine($"Actual AFP = {q_act} STB/day");


Ouput

.. terminal::

   Actual AFP = 1394.4 STB/day


