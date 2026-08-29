Reservoir Simulator 1D 2 Phases
===============================

Technical Overview and Mathematical Framework
---------------------------------------------
In this section, we develop a comprehensive, fully implicit 1D two-phase reservoir simulator that models the transient, coupled flow of immiscible oil and water through a heterogeneous porous medium. The simulator is designed to capture the complex fluid-fluid and fluid-rock interactions that occur during waterflooding performance.

The mathematical framework couples the physics of multiphase porous media flow with operational engineering constraints.It incorporates highly nonlinear constitutive relationships—including fluid compressibilities, pressure-dependent viscosities, relative permeability curves, and capillary pressure effects—along with dynamic well control logic and an automated adaptive time-stepping engine.

Governing Flow Equations
~~~~~~~~~~~~~~~~~~~~~~~~
The core of the simulator is governed by mass conservation equations for each fluid phase: water (:math:`w`) and oil (:math:`o`). In a 1D horizontal system, assuming Darcy flow, the partial differential equations(PDEs) are expressed as:
Water Phase Mass Conservation:

.. math::

   \frac{\partial }{\partial x} \left[ \alpha \frac{k \cdot k_ { rw }}{\mu_w B_w} \frac{\partial P_w}{\partial x} \right] \pm q_w = \frac{1}{\beta} \frac{\partial}{\partial t} \left( \frac{\phi S_w}{ B_w} \right)


Oil Phase Mass Conservation:

.. math::

   \frac{\partial}{\partial x} \left[ \alpha \frac{ k \cdot k_{ ro} }{\mu_o B_o} \frac{\partial P_o} {\partial x} \right] \pm q_o = \frac{ 1} {\beta} \frac{\partial} {\partial t} \left( \frac{\phi S_o} { B_o} \right)


Where:
:math:`P_o` and :math:`P_w` are the oil and water phase pressures respectively (:math:`\text{psi}`).

:math:`S_o` and :math:`S_w` are the oil and water phase saturations, satisfying the algebraic constraint: :math:`S_o + S_w = 1.0`.

:math:`k` is the absolute rock permeability(:math:`\text{md}`), and :math:`\phi` is the rock porosity.

:math:`\alpha` (:math:`1.127 \times 10^{ -3}`) and :math:`\beta` (:math:`5.615\ \text{ ft}^3/\text{bbl}`) are conversion constants matching standard field units.

:math:`q_w, q_o` represent volumetric source/sink well terms (:math:`\text{STB/day}`).

Constitutive and Petrophysical Relationships

To close the system of equations, several empirical and thermodynamic relationships are explicitly modeled as functions of the primary state variables (:math:`P_o` and :math:`S_w`):

Phase Pressure Coupling (Capillary Pressure)
The water phase pressure is dynamically linked to the oil phase pressure via the laboratory-measured imbibition capillary pressure function (:math:`P_{ cI}`):

.. math::

   P_w = P_o - P_{ cI} (S_w)


The capillary pressure curve is scaled using an effective saturation profile(:math:`S_{ we}`):

.. math::

   S_{ we} = \frac{ S_w - S_{ wr} }  { 1 - S_{ wr} -S_{ or} }



.. math::

   P_{ cI}(S_w) = P_e \cdot \left[ (S_{we})^{ -0.5}-1 \right]


where :math:`P_e` is the entry capillary pressure, :math:`S_{ wr}` is the residual water saturation, and :math:`S_{ or}` is the residual oil saturation.

Relative Permeability(Modified Corey-Brooks Model)
Multiphase fluid interference inside the pore throat structure is dictated by power-law relative permeability functions:


.. math::

   k_{ rw}(S_w) = k_{ rw0} \cdot(S_{ we})^{ n_w}



.. math::

   k_{ ro}(S_o) = k_{ ro0} \cdot \left(1 - \frac{(1 - S_o) - S_{wr}}{ 1 - S_{ wr} -S_{ or} }\right)^{n_o}


Fluid PVT Behavior (Compressibility and Viscosity)
Fluid volumes and flows scale dynamically with local pressure fields to model slightly compressible behaviors:


.. math::

   B_o(P_o) = B_{ o0} \cdot e^{c_o(2000 - P_o)} \quad \text{and} \quad B_w(P_w) = B_{ w0} \cdot e^{c_w(2500 - P_w)}



.. math::

   \mu_o(P_o) = \mu_{o0} \cdot e^{b_o(P_o - 2000)} \quad \text{and} \quad \mu_w(P_w) = \mu_{w0} \cdot e^{b_w(P_w - 2500)}


Spatial Discretization &Transmissibilities

The continuous governing equations are discretized in space using a block-centered finite volume formulation.For fluid flow between block :math:`i` and block :math:`i+1`, the inter-block absolute permeability is resolved using a harmonic mean:


.. math::

   k_{i+1/2} = \text{Harmmean}(k_i, k_{i+1}) = \cfrac{2}{\cfrac{1}{k_i} + \cfrac{1}{k_{i+1}}}


To ensure numerical stability and avoid unphysical oscillations across displacement fronts, the phase fluid mobilities (:math:`k_{r}/(\mu B)`) are evaluated using Single-Point Upstream Weighting. The properties are chosen entirely from the cell possessing the higher phase pressure:


.. math::

   \left(\cfrac{k_{rw}}{\mu_w B_w} \right)_{i+1/2} = \begin{cases} 
   \left(\cfrac{k_{rw}}{\mu_w B_w} \right)_i & \text{if} P_{w, i} > P_{w,i+1} \\
   \left(\cfrac{k_{rw}}{\mu_w B_w} \right)_{i+1} & \text{if} P_{w, i+1} > P_{ w,i} 
   \end{cases}



Wellbore Boundary Equations and Operational Controls
Wells represent external localized source/sink boundary constraints modeled via a radial inflow Peaceman formulation. The equivalent well block radius (:math:`r_e`) and baseline Well Index (:math:`WI`) are evaluated as:


.. math::

   r_e = 0.14\sqrt{\Delta x^2 + \Delta y^2}



.. math::

   WI = \frac{2\pi \alpha \cdot k \cdot \Delta z}{\ln(r_e / r_w)}


The individual phase flow rates produced or injected in a grid block are functions of the drawdown between the cell pressure and the wellbore's bottomhole flowing pressure (:math:`P_{wf}`):


.. math::

   Q_w = WI \cdot \frac{k_{rw}}{\mu_w B_w} \cdot(P_{ wf} - P_w)



.. math::

   Q_o = WI \cdot \frac{k_{ro}}{\mu_o B_o} \cdot(P_{ wf} - P_o)


Dynamic Constraint Swapping

Wells operate on dual-modifier switching scripts. They enforce target surface volume constraints as long as the system remains within safe pressure limits:

Producer: Operates at a target production rate :math:`Q_{ target}`. If the calculated :math:`P_{ wf}` drops below the mechanical limit :math:`P_{ min}` (:math:`1500\ \text{psi}`), the control loop automatically switches to variable-rate, fixed-BHP mode (:math:`P_{ wf} = P_{ min}`).

Injector: Operates at a target injection rate :math:`Q_{ target}`. If the injection pressure exceeds a formation fracturing threshold :math:`P_{ max}` (:math:`4500\ \text{psi}`), the solver seamlessly overrides the control variables to fixed-pressure boundaries (:math:`P_{wf} = P_{max}`).

Numerical Solution and Post-Processing
Applying backward Euler finite differences (Full_Implicit) in time yields a highly non-linear algebraic system of discrete residual equations at each time level (:math:`n+1`). The total mathematical system vector is assembled explicitly via a custom packing scheme:

.. math::

   \vec{R}(\vec{x}) = \begin{bmatrix} \vec{R_{oil}} \\ \vec{R_{water}} \\ \vec{R_{wellbore-balance}} \\ \vec{R_{operational-controls}} \end{bmatrix} = \vec{0}

This combined system is solved iteratively using the high-performance Fsolve method, which is part of the Solver class inside SepalSolver—a proprietary scientific computing and mathematical library developed by CypherCrescent. To maximize computational throughput, the simulator pairs SepalSolver's root-finding capabilities with an automated adaptive time-stepping loop. If the non-linear solver encounters a convergence failure or an operational constraint violation, the engine automatically cuts the time step size (:math:`\Delta t`) by :math:`75\%` and retries the step. Conversely, when convergence is achieved rapidly (:math:`\text{Iterations} < 4`), the engine safely scales up :math:`\Delta t` by :math:`25\%` for subsequent steps.
Finally, the localized state data histories collected are processed dynamically to output diagnostic performance graphs (tracking fractional water cuts, bottomhole pressures, and overall volumetric sweep efficiency) while compiling a high-speed animated GIF visualizing the propagation of the transient water saturation shock front over time.



.. code-block:: csharp

   // =========================================================================
   // STAGE 1: GLOBAL SIMULATION PARAMETERS & FIELD CHARACTERISTICS
   // =========================================================================

   // Set root project repository path for exporting tracking diagnostics/GIFs
   //folderpath = "C:\\Users\\lateef.a.kareem\\Documents\\GitHub\\ReservoirSimulation\\";

   // Nblocks: Spatial discretization grid cells
   // L: Maximum block boundary index for flow limits
   // M: Total number of discrete primary variables tracking
   // the porous media blocks (2 variables * 25 blocks)
   // Nwells: Count of source/sink boundaries active in the system
   int Nblocks = 25, L = Nblocks - 1, M = 2*Nblocks, Nwells = 2;

   // Thermodynamic properties, initial pressures (psi),
   // initial fluid saturation baselines,
   // residual saturation limits (Sw_r, So_r),
   // and baseline phase viscosities (cp)
   double Pinit = 3000, Sinit = 0.2, Sw_r = 0.10, So_r = 0.15,
          μw0 = 5.005, μo0 = 2, kro0 = 1.0, krw0 = 0.30, Pe = 2,
          co = 2e-5, cw = 4e-6, cr = 1e-5, bo = 2e-5, bw = 4e-10,
          Bw0 = 1.005, Bo0 = 1.4, no = 2.5, nw = 3;

   // Well operational specifications using named tuples:
   // constraints, metrics, and grid block locations
   var Producer =
       (MinPressure: 1500.0, ProdRate: 0.0,
       OilRate: 0.0, WaterRate: 0.0, Index: 0);
   var Injector =
       (MaxPressure: 4500.0, InjRate: 0.0,
       OilRate: 0.0, WaterRate: 0.0, Index: 24);

   // =========================================================================
   // STAGE 2: DATA STRUCTURE PACKING & UNPACKING (VECTOR TO PHYSICAL FIELDS)
   // =========================================================================

   // Unpacks a monolithic 1D solver array back into distinct,
   // physically meaningful array fields
   (double[], double[], double[], double[]) Unpack(double[] x)
   {
       int indx = 0;
       double[] Po = Zeros(Nblocks), Sw = Zeros(Nblocks),
                Pwells = Zeros(Nwells), Qwells = Zeros(Nwells);

       // Extract interleaved cell variables: [Po_0, Sw_0, Po_1, Sw_1, ...]
       for (int i = 0; i < Nblocks; i++)
       {
           // Extract Oil Pressure
           Po[i] = x[indx++];
           // Extract Water Saturation
           Sw[i] = x[indx++];
       }

       // Extract interleaved well variables: [Pwf_0, Q_0, Pwf_1, Q_1, ...]
       for (int i = 0; i < Nwells; i++)
       {
           // Extract Well Bottomhole Pressure (BHP)
           Pwells[i] = x[indx++];
           // Extract Total Well Volumetric Flow Rate
           Qwells[i] = x[indx++];
       }
       return (Po, Sw, Pwells, Qwells);
   }

   // Packs separate grid-block and well residuals into a single
   // consolidated vector for Fsolve
   double[] Pack(double[] Ro, double[] Rw, double[] Rwells, double[] Rcontrol)
   {
       int indx = 0;
       double[] R_total = Zeros(M + 2*Nwells);

       // Map conservation equations sequentially to match variable indexing
       for (int i = 0; i < Nblocks; i++)
       {
           // Oil mass conservation residual
           R_total[indx++] = Ro[i];
           // Water mass conservation residual
           R_total[indx++] = Rw[i];
       }
       for (int i = 0; i < Nwells; i++)
       {
           // Wellbore mass balance residual
           R_total[indx++] = Rwells[i];
           // Well operating constraint/control residual
           R_total[indx++] = Rcontrol[i];
       }
       return R_total;
   }

   // =========================================================================
   // STAGE 3: CONSTITUTIVE EQUATIONS & PETROPHYSICAL RELATIONS
   // =========================================================================

   // Normalized and effective water saturations used to
   // calculate relative permeabilities and capillary pressures
   double Sws(double Sw) => (Sw - Sw_r)/(1 - Sw_r);
   double Swe(double Sw) => (Sw - Sw_r)/(1 - Sw_r - So_r);

   // Capillary Pressure relationships (Drainage vs Imbibition curves)
   double Pc_D(double Sw) => Pe * Pow(Sws(Sw), -0.5);
   double Pc_I(double Sw) => Pe * (Pow(Swe(Sw), -0.5) - 1);

   // Formation Volume Factors (B-factors) modeling fluid compressibility
   double Bo(double Po) => Bo0 * Exp(co*(1500 - Po));
   double Bw(double Pw) => Bw0 * Exp(cw*(2500 - Pw));

   // Pressure-dependent dynamic fluid viscosities
   double μo(double Po) => μo0 * Exp(bo*(Po - 1500));
   double μw(double Pw) => μw0 * Exp(bw*(Pw - 2500));

   // Modified Corey Brooks models evaluating relative permeability curves
   double Krw(double Sw) => krw0 * Pow(Swe(Sw), nw);
   double Kro(double So) => kro0 * Pow(1 - Swe(1 - So), no);

   // Inter-block transmissibility calculation using the
   // harmonic mean of raw cell permeabilities
   double Harmmean(double x1, double x2) => 2/(1/x1 + 1/x2);

   // Unit conversion coefficients for oilfield standard parameters
   // =================================================================
   // Transmissibility conversion factor (Darcy to Field Units)
   double alpha = 1.127e-3;
   // Geometric productivity factor conversion for radial well inflows
   double alpha_well = alpha*2*pi;
   // Reservoir volume factor (Cubic feet to Stock Tank Barrels)
   double beta = 5.615;

   // Spatial dimensions (ft), block pore volume (STB),
   // and well radius parameters
   double dt, Dx = 200, Dy = 1000, Dz = 20, Ax = Dy*Dz,
          V = Dx*Dy*Dz/beta, rw = 0.5, re, WI, WIw, WIo;

   // Heterogeneous field instantiation using a
   // normal distribution for porosity and permeability
   // Localized Porosity array
   double[] Phi = Randn(Nblocks, 0.2, 0.01);
   // Localized Permeability array (md)
   double[] K = Randn(Nblocks, 900.0, 300.0);

   // Arrays storing baseline values at historical time level (n)
   double[] Po_n, Sw_n, Pwells_n, Qwells_n, Pw_n, So_n;
   // Tracks operational control mode for each well
   // (True = Rate, False = Pressure)
   bool[] RateControl = [true, true];

   // =========================================================================
   // STAGE 4: NONLINEAR RESIDUAL FORMULATION (MASS CONSERVATION & WELL EQUATIONS)
   // =========================================================================
   double[] Residual(double[] xnp1)
   {
       double Po_up, Pw_up, So_up, Sw_up, Tr, Tw, To;

       // Unpack current iteration guess variables for evaluation
       var (Po_np1, Sw_np1, Pwells_np1, Qwells_np1) = Unpack(xnp1);

       // Calculate dependent properties using
       // capillary pressure and saturation definitions
       double[] Pw_np1 = [.. Po_np1.Zip(Sw_np1, (po, sw) => po - Pc_I(sw))],
                So_np1 = [.. Sw_np1.Select(sw => 1 - sw)];

       double[] Rw = Zeros(Nblocks), Ro = Zeros(Nblocks),
                Rwells = Zeros(Nwells), Rcontrol = Zeros(Nwells);

       // Grid block mass balance accumulation loop
       for (int i = 0; i < Nblocks; i++)
       {
           // Inter-block inter-flux flux logic (Left neighbor interaction)
           if (i > 0)
           {
               Tr = alpha*Ax*Harmmean(K[i-1], K[i]);

               // Single-point upstream weighting for water phase stability
               (Pw_up, Sw_up) = Pw_np1[i-1] > Pw_np1[i] ?
                   (Pw_np1[i-1], Sw_np1[i-1]) : (Pw_np1[i], Sw_np1[i]);
               Tw = Tr*Krw(Sw_up)/(μw(Pw_up)*Bw(Pw_up));
               Rw[i] += Tw * (Pw_np1[i - 1] - Pw_np1[i]) * dt / Dx;

               // Single-point upstream weighting for oil phase stability
               (Po_up, So_up) = Po_np1[i-1] > Po_np1[i] ?
                   (Po_np1[i-1], So_np1[i-1]) : (Po_np1[i], So_np1[i]);
               To = Tr*Kro(So_up)/(μo(Po_up)*Bo(Po_up));
               Ro[i] += To*(Po_np1[i-1] - Po_np1[i]) * dt / Dx;
           }

           // Inter-block inter-flux flux logic (Right neighbor interaction)
           if (i < L)
           {
               Tr = alpha*Ax*Harmmean(K[i], K[i+1]);

               // Single-point upstream weighting for water phase stability
               (Pw_up, Sw_up) = Pw_np1[i+1] > Pw_np1[i] ?
                   (Pw_np1[i+1], Sw_np1[i+1]) : (Pw_np1[i], Sw_np1[i]);
               Tw = Tr*Krw(Sw_up)/(μw(Pw_up)*Bw(Pw_up));
               Rw[i] += Tw*(Pw_np1[i+1] - Pw_np1[i]) * dt / Dx;

               // Single-point upstream weighting for oil phase stability
               (Po_up, So_up) = Po_np1[i+1] > Po_np1[i] ?
                   (Po_np1[i+1], So_np1[i+1]) : (Po_np1[i], So_np1[i]);
               To = Tr*Kro(So_up)/(μo(Po_up)*Bo(Po_up));
               Ro[i] += To*(Po_np1[i+1] - Po_np1[i]) * dt / Dx;
           }

           // Time accumulation terms (implicit backward Euler method)
           Rw[i] -= V*Phi[i]*(Sw_np1[i]/Bw(Pw_np1[i]) - Sw_n[i]/Bw(Pw_n[i]));
           Ro[i] -= V*Phi[i]*(So_np1[i]/Bo(Po_np1[i]) - So_n[i]/Bo(Po_n[i]));
       }

       // Peaceman radius calculation for an isolated wellbore
       re = 0.14*Hypot(Dx, Dy);
       int idx;

       // --- Well Formulation: Producer Section ---
       Rwells[0] += Qwells_np1[0];
       idx = Producer.Index;
       // Base Well Index calculation
       WI = alpha_well*K[idx]*Dz/Log(re/rw);
       // Water mobility at well
       WIw = WI*Krw(Sw_np1[idx])/(μw(Pw_np1[idx])*Bw(Pw_np1[idx]));
       // Oil mobility at well
       WIo = WI*Kro(So_np1[idx])/(μo(Po_np1[idx])*Bo(Po_np1[idx]));
       // Calculate phase flow rates using the wellbore
       // pressure and connected grid cell properties
       Producer.WaterRate = (Pwells_np1[0] - Pw_np1[Producer.Index])*WIw;
       Producer.OilRate = (Pwells_np1[0] - Po_np1[Producer.Index])*WIo;
       // Inject sink terms directly back into the connected grid cell
       Rw[Producer.Index] += Producer.WaterRate * dt;
       Ro[Producer.Index] += Producer.OilRate * dt;
       // Well balance check
       Rwells[0] -= Producer.WaterRate + Producer.OilRate;
       // Evaluate dynamic constraint swapping logic
       // (Switch between Target Rate vs Minimum Allowed BHP)
       Rcontrol[0] = RateControl[0] ?
           Qwells_np1[0] - Producer.ProdRate : Pwells_np1[0] - Producer.MinPressure;

       // --- Well Formulation: Injector Section ---
       Rwells[1] += Qwells_np1[1];
       idx = Injector.Index;
       // Base Well Index calculation
       WI = alpha_well*K[idx]*Dz/Log(re/rw);
       // Injecting pure water phase
       WIw = WI*krw0/(μw(Pw_np1[idx])*Bw(Pw_np1[idx]));
       // Calculate injection rate using the
       // wellbore pressure and connected grid cell properties
       Injector.WaterRate = (Pwells_np1[1] - Pw_np1[idx])*WIw;
       // Inject source terms directly back into the connected grid cell
       Rw[idx] += Injector.WaterRate * dt;
       // Well balance check
       Rwells[1] -= Injector.WaterRate;
       // Evaluate dynamic constraint swapping logic
       // (Switch between Target Rate vs Maximum Allowed BHP)
       Rcontrol[1] = RateControl[1] ?
           Qwells_np1[1] - Injector.InjRate : Pwells_np1[1] - Injector.MaxPressure;

       // Consolidate values back into a single vector for solver optimization loops
       return Pack(Ro, Rw, Rwells, Rcontrol);
   }

   // Mathematical mapping and linear interpolation helper utilities
   double betweenab(double a, double b, double f) => a + f*(b-a);
   double interps(List<double> X, List<double> Y, double x)
   {
       int i = X.FindIndex(xi => xi>x);
       double f = (x-X[i-1])/(X[i]-X[i-1]);
       return betweenab(Y[i-1], Y[i], f);
   }
   double[] interpa(List<double> X, List<double[]> Y, double x)
   {
       int i = X.FindIndex(xi => xi>x);
       double f = (x-X[i-1])/(X[i]-X[i-1]);
       return [.. Y[i-1].Zip(Y[i], (a, b) => betweenab(a, b, f))];
   }

   // chunk writer utility for formatted console output of array values
   string Write(double[] data)
   {
       var chunks = data.Chunk(8);
       List<string> sb = [];
       foreach (var chunk in chunks)
           sb.Add(string.Join(", ", chunk.Select(x => x.ToString("F2"))));
       return string.Join(", \n", sb);
   }

   // =========================================================================
   // STAGE 5: RUNTIME EXECUTION MANAGEMENT LOOP & SENSITIVITY DESIGN
   // =========================================================================
   double EndTime = 10000; // Target simulation duration limit (days)
   double delt = EndTime/300; // Time steps for reporting intervals

   // Sensitivity loop testing performance metrics across varying injection-production rates
   for (int rate = 200; rate <= 500; rate += 100)
   {
       dt = 0.01; // Reset to a safe initial time step value

       // Initialize state vectors for a new sensitivity run
       Po_n = Repmat(Pinit, Nblocks); Sw_n = Repmat(Sinit, Nblocks);
       Pwells_n = Repmat(Pinit, Nwells); Qwells_n = Zeros(Nwells);
       Pw_n = [.. Po_n.Zip(Sw_n, (po, sw) => po - Pc_I(sw))];
       So_n = [.. Sw_n.Select(sw => 1 - sw)];

       // Initialize historical data tracking containers for plotting and reporting
       List<double[]> P = [Po_n], S = [Sw_n];
       List<double> Time = [0.0], WaterCut = [0.0], SweepEff = [0.0],
           ProdRate = [Qwells_n[0]], InjRate = [Qwells_n[1]],
           ProdPwf = [Pwells_n[0]], InjPwf = [Pwells_n[1]];

       // Define specific production/injection targets for this scenario run
       Producer.ProdRate = -rate;
       Injector.InjRate = rate;

       // Initialize Plot Subplots for Real-Time Visual Feedback Diagnostics
       Subplot(7, 4, [0, 1, 4, 5]);
       var Pbhp = Plot([0], [0], "r", 2);
       Axis([0, EndTime, 0, Injector.MaxPressure*1.1]);
       Title("Producer BHP");

       Subplot(7, 4, [8, 9, 12, 13]);
       var Prate = Plot([0], [0], "r", 2);
       Axis([0, EndTime, 0, Producer.ProdRate*1.1]);
       Title("Producer Rate");

       Subplot(7, 4, [16, 17, 20, 21]);
       var Pbsw = Plot([0], [0], "r", 2);
       Axis([0, EndTime, 0, 105]);
       Title("Producer WaterCut");

       Subplot(7, 4, [2, 3, 6, 7]);
       var Ibhp = Plot([0], [0], "b", 2);
       Axis([0, EndTime, 0, Injector.MaxPressure*1.1]);
       Title("Injector BHP");

       Subplot(7, 4, [10, 11, 14, 15]);
       var Irate = Plot([0], [0], "b", 2);
       Axis([0, EndTime, 0, Injector.InjRate*1.1]);
       Title("Injector Rate");

       Subplot(7, 4, [18, 19, 22, 23]);
       var Iswp = Plot([0], [0], "b", 2);
       Axis([0, EndTime, 0, 105]);
       Title("Injector Sweep Efficiency");

       // Spatial Profile Schematic setup for the moving Water Saturation Front
       Subplot(7, 4, [24, 25, 26, 27]);
       double[] xpf = [0.3, 0.7], ypf = Linspace(0.3, 0.7, 5);
       var (xperf, yperf) = Meshgrid(xpf, ypf);
       Rectangle([Producer.Index + 0.45, 0.2, 0.1, 1.6]); HoldOn();
       Plot(Producer.Index + xperf, yperf, "k", 2);
       Rectangle([Injector.Index + 0.45, 0.2, 0.1, 1.6]);
       Plot(Injector.Index + xperf, yperf, "k", 2);
       double[] xres = [0, 0, Nblocks, Nblocks], yres = [0, 1, 1, 0];
       Fill(xres, yres, [1, 0, 0], 0.5);
       xpf = Linspace(0.5, Nblocks - 0.5, Nblocks);
       ypf = Repmat(Sinit, Nblocks + 2);
       double[] xplot = [0, 0, .. xpf, Nblocks, Nblocks];
       double[] yplot = [0, .. ypf, 0];
       var Water = Fill(xplot, yplot, [0, 0, 1], 0.5, 0);
       Axis([0, Nblocks, 0, 2.5]);
       Title("Water Saturation Front");
       HoldOff();

       // Package the initial state vector and configure the nonlinear solver options
       double[] xs = Pack(Po_n, Sw_n, Pwells_n, Qwells_n), xn;
       var opts = SolverSet(MaxIter: 10, AbsTol: 1e-6, UseParallel: true);

       Console.WriteLine($"""
       ======================================================================
                  Starting simulation for Rate = {rate} STB/day

       Time: 
       {0:F2} days

       Producer BHP: 
       {Pinit:F2} psi

       Injector BHP: 
       {Pinit:F2} psi
       
       Pressure: 
       {Write(Po_n)}
       
       Saturation:
       {Write(Sw_n)}



       """);

       // =========================================================================
       // STAGE 6: CORE TIME-STEPPING INTERACTION LOOP (NEWTON RAPHSON + CONTROLS)
       // =========================================================================
       double[] displaytimes = Linspace(0, EndTime, 21);
       double printtime = 0;
       while (Time.Last() < EndTime)
       {
           if (displaytimes.Any(t =>
           (Time.Last() - t)*(Time.Last() + dt - t) < 0))
           {
               opts.Display = true;
               printtime = Array.Find(displaytimes,
                   t => (Time.Last() - t)*(Time.Last() + dt - t) < 0);
           }
           else
           {
               opts.Display = false;
           }
           if (opts.Display)
               Console.WriteLine($"""


       Time: 
       {printtime:F2} days
       """);

           // Call the Newton-Raphson nonlinear solver to
           // find the next state solution
           xn = [.. Fsolve(Residual, xs, opts)];

           // Check convergence. If non-converged,
           // chop the time step (time-step cuts) and retry.
           if (!opts.ans.IsConverged)
           {
               dt = 0.25*dt;
               Console.WriteLine("""
                   ================================================
                              Rejected (Non-Convergence)
                   ================================================
                   """);
               continue;
           }

           // Unpack solution values to evaluate operational constraint validations
           var (Po_s, Sw_s, Pwells_s, Qwells_s) = Unpack(xn);

           // Validate Producer constraints:
           // switch to BHP control if pressure falls below minimum limits
           if (Pwells_s[0] < Producer.MinPressure)
           {
               RateControl[0] = Pwells_s[0] > Producer.MinPressure;
               Console.WriteLine("""
                   ================================================
                         Rejected (Minimum Pressure Violated) 
                   ================================================
                   """);
               continue;
           }
           // Validate Injector constraints:
           // switch to BHP control if pressure exceeds fracturing limits
           if (Pwells_s[1] > Injector.MaxPressure)
           {
               RateControl[1] = Pwells_s[1] < Injector.MaxPressure;
               Console.WriteLine("""
                   ================================================
                         Rejected (Maximum Pressure Violated) 
                   ================================================
                   """);
               continue;
           }

           // Accept the validated time step and update internal tracking states
           (Po_n, Sw_n, Pwells_n, Qwells_n) = (Po_s, Sw_s, Pwells_s, Qwells_s);
           Pw_n = [.. Po_n.Zip(Sw_n, (po, sw) => po - Pc_I(sw))];
           So_n = [.. Sw_n.Select(sw => 1 - sw)];

           // Log verified parameters to performance history arrays
           P.Add(Po_n); S.Add(Sw_n);
           ProdPwf.Add(Pwells_n[0]); InjPwf.Add(Pwells_n[1]);
           ProdRate.Add(Qwells_n[0]); InjRate.Add(Qwells_n[1]);
           WaterCut.Add(Producer.WaterRate*100/(Producer.WaterRate + Producer.OilRate));
           SweepEff.Add((Sw_n.Sum()/Nblocks - Sinit)*100/(1 - Sinit));
           Time.Add(Time.Last() + dt);

           // Adaptive Time-Stepping Logic:
           // scale dt up if convergence is fast, scale down if slow
           if (opts.ans.Iter < 4) dt = 1.25*dt;
           if (opts.ans.Iter > 8) dt = 0.5*dt;
           if (dt < 1e-5) throw new Exception("time step is too small");

           // Set current solution vector as the next time step initialization guess (xs)
           xs = [.. xn];


           if (opts.Display)
           {
               Console.WriteLine($"""
       Producer BHP: 
       {interps(Time, ProdPwf, printtime):F2} psi

       Injector BHP: 
       {interps(Time, InjPwf, printtime):F2} psi
       
       Pressure: 
       {Write(interpa(Time, P, printtime))}
       
       Saturation:
       {Write(interpa(Time, S, printtime))}



       """);
           }
       }

       // =========================================================================
       // STAGE 7: POST-PROCESSING VISUALIZATION & ANIMATION EXPORT
       // =========================================================================

       // Dynamic closure map function mapping raw state histories
       // directly onto figure frames
       byte[] Animfun(int i)
       {
           // Sync time dimensions to interpolation index targets
           Pbhp.Xdata = Vcart(Pbhp.Xdata, i*delt);
           Pbhp.Ydata = Vcart(Pbhp.Ydata, interps(Time, ProdPwf, i*delt));

           Prate.Xdata = Pbhp.Xdata;
           Prate.Ydata = Vcart(Prate.Ydata, interps(Time, ProdRate, i*delt));

           Pbsw.Xdata = Pbhp.Xdata;
           Pbsw.Ydata = Vcart(Pbsw.Ydata, interps(Time, WaterCut, i*delt));

           Ibhp.Xdata = Pbhp.Xdata;
           Ibhp.Ydata = Vcart(Ibhp.Ydata, interps(Time, InjPwf, i*delt));

           Irate.Xdata = Pbhp.Xdata;
           Irate.Ydata = Vcart(Irate.Ydata, interps(Time, InjRate, i*delt));

           Iswp.Xdata = Pbhp.Xdata;
           Iswp.Ydata = Vcart(Iswp.Ydata, interps(Time, SweepEff, i*delt));

           // Generate spatial mapping visualization showing fluid front changes over time
           Sw_n = interpa(Time, S, i*delt);
           yplot = [0, Sw_n.First(), .. Sw_n, Sw_n.Last(), 0];
           Water.Ydata = yplot;

           // Capture rendered visual asset frame dimensions
           return GetFrame(800, 1000);
       }

       // Compile and output the processed timeline visualization to an animated GIF
       Console.WriteLine(DateTime.Now);
       AnimationMaker(Animfun, $"1D_WaterFlooding_{rate}.gif", 30, 300);
       Console.WriteLine(DateTime.Now);

       // Clean up graphics devices to free memory hooks for the next loop run
       CloseFig();
   }


Ouput

.. terminal::

   ======================================================================
              Starting simulation for Rate = 200 STB/day
   
   Time: 
   0.00 days
   
   Producer BHP: 
   3000.00 psi
   
   Injector BHP: 
   3000.00 psi
   
   Pressure: 
   3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 
   3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 
   3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 
   3000.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          16.2586        start      
        1            56       1.949e-005      0.56681     
        2            57       3.489e-009     1.057e-005   
        3            58       4.013e-011     1.070e-009   
   Producer BHP: 
   2162.72 psi
   
   Injector BHP: 
   2767.12 psi
   
   Pressure: 
   2277.86, 2294.66, 2302.93, 2309.42, 2316.20, 2325.17, 2332.94, 2340.37, 
   2347.93, 2354.91, 2361.96, 2368.57, 2380.31, 2391.83, 2398.46, 2404.57, 
   2410.09, 2415.13, 2419.64, 2425.39, 2431.40, 2437.08, 2442.51, 2454.45, 
   2522.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.37, 
   0.69
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          125.580        start      
        1            56         4552.86       3436.22     
        2            57         4376.65       450.375     
        3            58         4118.47       212.733     
        4            59         3598.40       352.041     
        5            60         115.384       2349.08     
        6            61         178.672       45.0621     
        7            62         130.314       32.5595     
        8            63         7.72704       82.4398     
        9            64         0.33098       4.97119     
        10           65         0.00123       0.22162     
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          31.3950        start      
        1            55         42.8247       38.7811     
        2            56         21.4644       19.4459     
        3            57       1.063e-004      19.5407     
        4            58       2.805e-008     1.020e-004   
        5            59       5.021e-009     2.096e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2144.14 psi
   
   Pressure: 
   1610.69, 1626.91, 1634.92, 1641.23, 1647.83, 1656.59, 1664.20, 1671.48, 
   1678.90, 1685.76, 1692.70, 1699.20, 1710.76, 1722.11, 1728.64, 1734.67, 
   1740.10, 1745.06, 1749.50, 1755.15, 1761.06, 1766.92, 1788.32, 1836.36, 
   1899.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.50, 0.71, 
   0.76
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          24.5690        start      
        1            56         0.00762       0.04240     
        2            57         0.00113       0.00810     
        3            58       3.355e-008      0.00105     
        4            59       2.673e-009     2.859e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2138.39 psi
   
   Pressure: 
   1583.24, 1595.51, 1601.62, 1606.48, 1611.60, 1618.47, 1624.49, 1630.32, 
   1636.32, 1641.92, 1647.65, 1653.10, 1662.88, 1672.59, 1678.26, 1683.55, 
   1688.39, 1692.88, 1696.94, 1702.20, 1709.15, 1747.30, 1791.45, 1835.32, 
   1893.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.76, 
   0.78
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          23.6584        start      
        1            56       7.366e-006      0.02224     
        2            57       2.879e-009     1.379e-006   
        3            58       4.506e-011     4.291e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2180.89 psi
   
   Pressure: 
   1583.29, 1595.58, 1601.69, 1606.55, 1611.68, 1618.55, 1624.58, 1630.41, 
   1636.41, 1642.02, 1647.75, 1653.20, 1662.98, 1672.70, 1678.37, 1683.67, 
   1688.52, 1693.01, 1697.08, 1705.74, 1752.00, 1797.20, 1838.59, 1880.17, 
   1935.86
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.32, 0.66, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          39.9479        start      
        1            55       4.887e-005      0.03350     
        2            56       3.763e-008     5.921e-006   
        3            57       5.899e-011     4.483e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2216.26 psi
   
   Pressure: 
   1583.31, 1595.60, 1601.71, 1606.57, 1611.70, 1618.57, 1624.60, 1630.43, 
   1636.43, 1642.04, 1647.77, 1653.22, 1663.01, 1672.73, 1678.40, 1683.69, 
   1688.54, 1693.08, 1702.63, 1747.60, 1794.47, 1837.30, 1876.92, 1917.05, 
   1971.25
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.39, 0.68, 0.74, 0.76, 0.77, 0.79, 
   0.80
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          43.5612        start      
        1            55       8.647e-005      0.06765     
        2            56       5.772e-008     1.271e-005   
        3            57       6.104e-011     9.743e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2247.32 psi
   
   Pressure: 
   1583.18, 1595.45, 1601.56, 1606.41, 1611.53, 1618.39, 1624.41, 1630.23, 
   1636.22, 1641.82, 1647.54, 1652.98, 1662.75, 1672.45, 1678.11, 1683.40, 
   1688.47, 1706.54, 1741.68, 1785.72, 1830.39, 1871.68, 1910.10, 1949.20, 
   2002.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.50, 0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 
   0.81
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.483        start      
        1            56         341.092       191.851     
        2            57         331.984       5.13745     
        3            58         757.032       612.358     
        4            59         7.92368       421.164     
        5            60         5.30946       7.46802     
        6            61         1.30331       2.25401     
        7            62         0.06020       0.69914     
        8            63       1.564e-004      0.03395     
        9            64       1.234e-006     8.726e-005   
        10           65       1.644e-007     7.867e-007   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          37.6208        start      
        1            56       4.243e-005      0.08785     
        2            57       5.588e-009     4.471e-006   
        3            58       5.558e-011     1.081e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2286.89 psi
   
   Pressure: 
   1583.01, 1595.25, 1601.34, 1606.18, 1611.29, 1618.14, 1624.15, 1629.96, 
   1635.94, 1641.53, 1647.24, 1652.67, 1662.42, 1672.10, 1677.75, 1684.06, 
   1715.72, 1754.42, 1788.12, 1830.28, 1873.53, 1913.73, 1951.27, 1989.60, 
   2041.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.59, 0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          104.507        start      
        1            55         17025.9       5391.12     
        2            56         8519.94       2697.86     
        3            57         13.6240       2703.48     
        4            58         13.4690       0.21992     
        5            59         13.4664       0.00198     
        6            60         14.1715       0.52949     
        7            61         0.02013       10.6273     
        8            62       9.239e-004      0.01441     
        9            63         0.00304       0.00158     
        10           64         0.00192      8.412e-004   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          40.8410        start      
        1            56         2046.72       1800.15     
        2            57         1024.81       898.604     
        3            58         0.47228       901.126     
        4            59       9.824e-004      0.41953     
        5            60       1.924e-006     8.602e-004   
        6            61       1.796e-006     3.273e-006   
        7            62       1.237e-007     1.471e-006   
        8            63       2.865e-007     1.433e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2328.19 psi
   
   Pressure: 
   1583.06, 1595.32, 1601.41, 1606.25, 1611.37, 1618.22, 1624.23, 1630.04, 
   1636.03, 1641.62, 1647.34, 1652.77, 1662.53, 1672.25, 1680.96, 1723.33, 
   1764.72, 1801.74, 1834.26, 1875.25, 1917.52, 1956.93, 1993.81, 2031.56, 
   2083.28
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.64, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 
   0.82
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          41.2897        start      
        1            55         0.01778       0.08499     
        2            56       4.445e-004      0.01633     
        3            57       1.294e-007     3.985e-004   
        4            58       2.623e-009     1.136e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2373.46 psi
   
   Pressure: 
   1582.96, 1595.19, 1601.28, 1606.11, 1611.22, 1618.06, 1624.06, 1629.87, 
   1635.84, 1641.42, 1647.13, 1652.55, 1662.37, 1683.26, 1731.52, 1776.40, 
   1816.03, 1851.85, 1883.52, 1923.60, 1965.06, 2003.81, 2040.13, 2077.39, 
   2128.59
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          101.253        start      
        1            53         1944.80       666.088     
        2            54         984.763       337.537     
        3            55         25.6078       348.028     
        4            56         25.5813       0.02131     
        5            57         25.2472       0.25670     
        6            58         6.45256       24.3595     
        7            59         0.12867       4.85963     
        8            60         0.00147       0.10000     
        9            61         0.00496       0.00268     
        10           62         0.00283       0.00163     
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          31.6363        start      
        1            54         16.8637       15.2693     
        2            55         16.7877       0.06880     
        3            56         38.5078       19.6675     
        4            57         0.00309       34.8660     
        5            58         0.02012       0.02103     
        6            59         0.00934       0.00975     
        7            60       2.396e-005      0.00848     
        8            61       1.955e-007     2.188e-005   
        9            62       1.925e-007     3.513e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2451.30 psi
   
   Pressure: 
   1582.54, 1594.71, 1600.76, 1605.57, 1610.65, 1617.46, 1623.43, 1629.20, 
   1635.14, 1640.69, 1646.37, 1651.94, 1686.41, 1770.36, 1817.83, 1860.81, 
   1899.19, 1934.08, 1965.03, 2004.30, 2045.03, 2083.15, 2118.96, 2155.77, 
   2206.50
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.47, 0.69, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          10.5966        start      
        1            56         21.1736       20.0569     
        2            57         18.2481       2.77129     
        3            58         0.00414       17.2817     
        4            59       5.188e-004      0.00442     
        5            60       5.186e-005     4.423e-004   
        6            61       1.656e-007     4.896e-005   
        7            62       2.065e-007     3.525e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2521.64 psi
   
   Pressure: 
   1582.70, 1594.90, 1600.96, 1605.78, 1610.87, 1617.69, 1623.68, 1629.46, 
   1635.42, 1640.99, 1647.57, 1681.93, 1766.42, 1847.62, 1893.55, 1935.53, 
   1973.19, 2007.50, 2038.00, 2076.73, 2116.95, 2154.64, 2190.07, 2226.54, 
   2276.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.26768        start      
        1            56         177.829       172.646     
        2            57         88.9140       86.3232     
        3            58       5.052e-004      86.3222     
        4            59       2.409e-007     4.375e-004   
        5            60       5.334e-008     2.859e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2562.59 psi
   
   Pressure: 
   1582.80, 1595.01, 1601.09, 1605.92, 1611.01, 1617.84, 1623.83, 1629.63, 
   1635.61, 1643.47, 1687.58, 1734.08, 1815.17, 1893.88, 1938.81, 1980.07, 
   2017.16, 2051.01, 2081.12, 2119.40, 2159.20, 2196.52, 2231.63, 2267.83, 
   2317.89
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.29, 0.63, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          25.2498        start      
        1            56         636.181       575.564     
        2            57         318.098       287.783     
        3            58         0.00993       287.778     
        4            59       1.445e-006      0.00431     
        5            60       2.262e-007     1.075e-006   
        6            61       1.595e-007     6.033e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2602.00 psi
   
   Pressure: 
   1582.89, 1595.12, 1601.20, 1606.03, 1611.13, 1617.96, 1623.96, 1629.79, 
   1641.43, 1688.12, 1736.91, 1781.73, 1860.66, 1937.74, 1981.94, 2022.60, 
   2059.22, 2092.66, 2122.44, 2160.33, 2199.75, 2236.75, 2271.59, 2307.54, 
   2357.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.36, 0.66, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          10.2080        start      
        1            56         39.5988       37.7903     
        2            57         19.7994       18.8952     
        3            58       9.760e-006      18.8953     
        4            59       1.048e-008     5.846e-006   
        5            60       1.395e-009     1.136e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2646.21 psi
   
   Pressure: 
   1582.79, 1595.00, 1601.07, 1605.90, 1610.99, 1617.81, 1623.96, 1643.03, 
   1694.85, 1742.28, 1789.28, 1832.98, 1910.34, 1986.11, 2029.66, 2069.78, 
   2105.95, 2139.01, 2168.47, 2205.99, 2245.05, 2281.74, 2316.32, 2352.04, 
   2401.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.46, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.03605        start      
        1            53         823.037       793.388     
        2            54         437.702       371.452     
        3            55         0.00109       421.935     
        4            56         0.00494       0.00609     
        5            57         0.00216       0.00267     
        6            58       2.411e-006      0.00208     
        7            59       2.255e-006     4.498e-006   
        8            60       9.379e-007     1.270e-006   
        9            61       5.839e-007     3.412e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2690.42 psi
   
   Pressure: 
   1582.46, 1594.62, 1600.67, 1605.47, 1610.55, 1617.95, 1649.09, 1699.23, 
   1749.36, 1795.06, 1840.85, 1883.69, 1959.70, 2034.30, 2077.23, 2116.83, 
   2152.56, 2185.25, 2214.41, 2251.56, 2290.27, 2326.67, 2361.00, 2396.51, 
   2445.84
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.54, 0.71, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.96327        start      
        1            54         22.1639       21.6420     
        2            55         11.0819       10.8209     
        3            56       8.255e-007      10.8209     
        4            57       2.801e-010     5.997e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2738.26 psi
   
   Pressure: 
   1582.24, 1594.36, 1600.40, 1605.19, 1611.40, 1656.99, 1708.50, 1756.69, 
   1805.27, 1849.91, 1894.87, 1937.05, 2011.99, 2085.62, 2128.04, 2167.20, 
   2202.56, 2234.92, 2263.81, 2300.63, 2339.04, 2375.18, 2409.29, 2444.60, 
   2493.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.16539        start      
        1            55         2.79244       2.77367     
        2            56         1.39621       1.38682     
        3            57       1.065e-008      1.38680     
        4            58       3.420e-009     6.604e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2781.75 psi
   
   Pressure: 
   1582.73, 1594.92, 1601.00, 1608.38, 1649.32, 1708.18, 1758.08, 1805.28, 
   1853.17, 1897.32, 1941.89, 1983.75, 2058.15, 2131.29, 2173.43, 2212.35, 
   2247.50, 2279.68, 2308.39, 2345.01, 2383.22, 2419.18, 2453.13, 2488.30, 
   2537.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.30, 0.65, 0.72, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          92.9496        start      
        1            55         2752.26       1013.93     
        2            56         1378.01       509.018     
        3            57         11.2444       513.299     
        4            58         11.1827       0.03507     
        5            59         11.1662       0.01238     
        6            60         0.62415       8.87754     
        7            61         0.14457       0.57880     
        8            62         0.06593       0.05923     
        9            63         0.12310       0.14233     
        10           64         0.00546       0.09680     
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          29.0578        start      
        1            56       1.782e-005      0.05513     
        2            57       9.336e-009     3.325e-006   
        3            58       6.518e-011     1.656e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2814.40 psi
   
   Pressure: 
   1582.77, 1595.06, 1607.36, 1648.06, 1691.60, 1748.37, 1797.12, 1843.54, 
   1890.79, 1934.43, 1978.54, 2020.01, 2093.77, 2166.30, 2208.12, 2246.75, 
   2281.65, 2313.61, 2342.14, 2378.54, 2416.53, 2452.31, 2486.10, 2521.13, 
   2569.95
   
   Saturation:
   0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          41.6091        start      
        1            53         340.958       291.432     
        2            54         177.435       139.843     
        3            55         0.10329       151.507     
        4            56       7.802e-004      0.08821     
        5            57       2.499e-006     6.655e-004   
        6            58       1.032e-008     2.117e-006   
        7            59       2.451e-009     1.092e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2864.68 psi
   
   Pressure: 
   1583.32, 1620.27, 1672.26, 1712.89, 1754.58, 1809.49, 1856.99, 1902.40, 
   1948.71, 1991.56, 2034.94, 2075.77, 2148.47, 2220.01, 2261.30, 2299.48, 
   2333.99, 2365.62, 2393.88, 2429.97, 2467.66, 2503.19, 2536.78, 2571.63, 
   2620.27
   
   Saturation:
   0.21, 0.44, 0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          9.64496        start      
        1            54         2524.70       2468.84     
        2            55         1262.43       1234.41     
        3            56         0.13805       1234.44     
        4            57       1.839e-005      0.09584     
        5            58       4.281e-007     1.300e-005   
        6            59       3.925e-007     8.026e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3082.00 psi
   
   Pressure: 
   1812.05, 1905.20, 1950.48, 1985.78, 2022.62, 2071.65, 2114.46, 2155.68, 
   2197.99, 2237.37, 2277.47, 2315.44, 2383.38, 2450.57, 2489.55, 2525.77, 
   2558.68, 2588.98, 2616.18, 2651.07, 2687.70, 2722.39, 2755.32, 2789.66, 
   2837.81
   
   Saturation:
   0.51, 0.70, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   8/27/2026 12:11:20 PM
   8/27/2026 12:13:22 PM
   ======================================================================
              Starting simulation for Rate = 300 STB/day
   
   Time: 
   0.00 days
   
   Producer BHP: 
   3000.00 psi
   
   Injector BHP: 
   3000.00 psi
   
   Pressure: 
   3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 
   3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 
   3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 3000.00, 
   3000.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ⚠️ Runtime Error: time step is too small

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

