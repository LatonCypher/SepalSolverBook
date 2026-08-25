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
               Rw[i] += Tw*(Pw_np1[i-1] - Pw_np1[i])/Dx;

               // Single-point upstream weighting for oil phase stability
               (Po_up, So_up) = Po_np1[i-1] > Po_np1[i] ?
                   (Po_np1[i-1], So_np1[i-1]) : (Po_np1[i], So_np1[i]);
               To = Tr*Kro(So_up)/(μo(Po_up)*Bo(Po_up));
               Ro[i] += To*(Po_np1[i-1] - Po_np1[i])/Dx;
           }

           // Inter-block inter-flux flux logic (Right neighbor interaction)
           if (i < L)
           {
               Tr = alpha*Ax*Harmmean(K[i], K[i+1]);

               // Single-point upstream weighting for water phase stability
               (Pw_up, Sw_up) = Pw_np1[i+1] > Pw_np1[i] ?
                   (Pw_np1[i+1], Sw_np1[i+1]) : (Pw_np1[i], Sw_np1[i]);
               Tw = Tr*Krw(Sw_up)/(μw(Pw_up)*Bw(Pw_up));
               Rw[i] += Tw*(Pw_np1[i+1] - Pw_np1[i])/Dx;

               // Single-point upstream weighting for oil phase stability
               (Po_up, So_up) = Po_np1[i+1] > Po_np1[i] ?
                   (Po_np1[i+1], So_np1[i+1]) : (Po_np1[i], So_np1[i]);
               To = Tr*Kro(So_up)/(μo(Po_up)*Bo(Po_up));
               Ro[i] += To*(Po_np1[i+1] - Po_np1[i])/Dx;
           }

           // Time accumulation terms (implicit backward Euler method)
           Rw[i] -= V*Phi[i]*(Sw_np1[i]/Bw(Pw_np1[i]) - Sw_n[i]/Bw(Pw_n[i]))/dt;
           Ro[i] -= V*Phi[i]*(So_np1[i]/Bo(Po_np1[i]) - So_n[i]/Bo(Po_n[i]))/dt;
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
       Rw[Producer.Index] += Producer.WaterRate;
       Ro[Producer.Index] += Producer.OilRate;
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
       Rw[idx] += Injector.WaterRate;
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
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          200.891        start      
        1            56       1.926e-004      0.45646     
        2            57       2.909e-008     6.894e-006   
        3            58       6.005e-010     6.851e-010   
   Producer BHP: 
   2235.27 psi
   
   Injector BHP: 
   2717.53 psi
   
   Pressure: 
   2271.10, 2278.51, 2286.65, 2293.59, 2302.04, 2312.88, 2322.69, 2330.45, 
   2335.91, 2344.04, 2359.67, 2373.78, 2380.88, 2386.92, 2392.92, 2399.85, 
   2406.38, 2423.80, 2441.28, 2448.41, 2456.16, 2463.09, 2469.09, 2482.40, 
   2533.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.42, 
   0.71
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          181.289        start      
        1            56         452.305       57.3478     
        2            57         226.314       28.6930     
        3            58       7.180e-005      28.7340     
        4            59       2.904e-007     9.132e-006   
        5            60       1.757e-007     5.917e-008   
   Producer BHP: 
   1567.55 psi
   
   Injector BHP: 
   2093.43 psi
   
   Pressure: 
   1603.47, 1610.89, 1619.03, 1625.97, 1634.40, 1645.23, 1655.01, 1662.75, 
   1668.19, 1676.28, 1691.82, 1705.84, 1712.88, 1718.87, 1724.81, 1731.67, 
   1738.12, 1755.33, 1772.58, 1779.60, 1787.23, 1794.45, 1819.96, 1862.90, 
   1908.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.51, 0.72, 
   0.77
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          153.755        start      
        1            55       3.089e-004      0.11452     
        2            56       4.265e-008     1.153e-005   
        3            57       1.410e-010     3.343e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2037.61 psi
   
   Pressure: 
   1525.88, 1531.30, 1537.30, 1542.48, 1548.86, 1557.15, 1564.74, 1570.81, 
   1575.14, 1581.65, 1594.33, 1605.91, 1611.81, 1616.90, 1622.02, 1628.01, 
   1633.74, 1649.20, 1664.91, 1671.41, 1679.88, 1722.56, 1771.10, 1810.21, 
   1853.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.24, 0.59, 0.72, 0.76, 
   0.79
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          154.636        start      
        1            52         189.398       77.2984     
        2            53         94.7145       38.6551     
        3            54         0.00388       38.6415     
        4            55       3.179e-007      0.00145     
        5            56       1.138e-007     1.337e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2094.73 psi
   
   Pressure: 
   1525.89, 1531.30, 1537.31, 1542.49, 1548.87, 1557.16, 1564.75, 1570.82, 
   1575.15, 1581.66, 1594.34, 1605.93, 1611.83, 1616.91, 1622.03, 1628.03, 
   1633.75, 1649.22, 1664.96, 1674.42, 1730.94, 1786.39, 1831.88, 1868.96, 
   1910.25
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.29, 0.64, 0.73, 0.76, 0.78, 
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
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.982        start      
        1            55         0.01397       0.18647     
        2            56       3.884e-004     3.039e-004   
        3            57       3.708e-007     8.546e-006   
        4            58       1.470e-010     8.293e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2153.95 psi
   
   Pressure: 
   1525.83, 1531.23, 1537.22, 1542.38, 1548.75, 1557.02, 1564.58, 1570.64, 
   1574.96, 1581.45, 1594.09, 1605.65, 1611.54, 1616.61, 1621.71, 1627.69, 
   1633.40, 1648.90, 1681.71, 1736.97, 1797.66, 1850.03, 1893.48, 1929.26, 
   1969.51
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.37, 0.67, 0.73, 0.76, 0.77, 0.79, 
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
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.917        start      
        1            56         1.55265       0.84244     
        2            57         1.64523       0.04383     
        3            58         0.86923       0.40190     
        4            59         0.00516       0.44775     
        5            60       6.838e-006      0.00268     
        6            61       9.670e-009     3.549e-006   
        7            62       3.212e-008     1.139e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2283.44 psi
   
   Pressure: 
   1525.71, 1531.08, 1537.05, 1542.19, 1548.52, 1556.75, 1564.28, 1570.31, 
   1574.61, 1581.07, 1593.65, 1605.15, 1611.01, 1616.06, 1621.13, 1627.08, 
   1632.93, 1684.21, 1820.67, 1874.96, 1932.52, 1982.79, 2024.80, 2059.62, 
   2099.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.46, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
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
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.118        start      
        1            55       1.548e-004      0.08704     
        2            56       2.090e-008     5.162e-006   
        3            57       2.682e-010     1.622e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2385.78 psi
   
   Pressure: 
   1525.82, 1531.21, 1537.20, 1542.36, 1548.72, 1556.98, 1564.55, 1570.60, 
   1574.91, 1581.40, 1594.04, 1605.59, 1611.47, 1616.54, 1621.65, 1628.55, 
   1665.15, 1799.51, 1930.32, 1982.49, 2038.30, 2087.31, 2128.40, 2162.58, 
   2201.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 
   0.59, 0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
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
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.870        start      
        1            55       1.006e-004      0.06101     
        2            56       6.581e-008     3.245e-006   
        3            57       2.261e-010     1.405e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2424.96 psi
   
   Pressure: 
   1525.86, 1531.27, 1537.27, 1542.44, 1548.81, 1557.08, 1564.66, 1570.72, 
   1575.04, 1581.54, 1594.20, 1605.78, 1611.67, 1616.76, 1624.13, 1671.52, 
   1720.45, 1848.56, 1974.75, 2025.50, 2080.05, 2128.10, 2168.49, 2202.18, 
   2240.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.64, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
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
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          162.946        start      
        1            54         0.00311       0.05380     
        2            55       1.359e-005     4.872e-005   
        3            56       6.586e-009     2.118e-007   
        4            57       1.593e-009     1.306e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2455.96 psi
   
   Pressure: 
   1525.89, 1531.29, 1537.30, 1542.47, 1548.85, 1557.13, 1564.71, 1570.78, 
   1575.10, 1581.61, 1594.27, 1605.86, 1611.79, 1621.99, 1664.87, 1715.89, 
   1762.84, 1886.85, 2009.91, 2059.60, 2113.14, 2160.41, 2200.23, 2233.53, 
   2271.71
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 0.66, 0.73, 
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
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          175.631        start      
        1            54         689.787       223.790     
        2            55         348.147       88.9955     
        3            56         94.4639       83.4968     
        4            57         56.2553       20.7758     
        5            58         0.09086       30.6324     
        6            59         0.02029       0.05791     
        7            60         0.00322       0.00927     
        8            61       5.092e-006      0.00175     
        9            62       5.964e-007     2.975e-006   
        10           63       1.060e-007     3.434e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2490.20 psi
   
   Pressure: 
   1525.85, 1531.25, 1537.24, 1542.41, 1548.77, 1557.04, 1564.61, 1570.67, 
   1574.98, 1581.48, 1594.12, 1605.96, 1624.08, 1668.08, 1711.44, 1760.44, 
   1806.07, 1927.14, 2047.71, 2096.51, 2149.19, 2195.80, 2235.12, 2268.07, 
   2305.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.45, 0.70, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
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
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.134        start      
        1            56         14.2373       3.28858     
        2            57         6.80093       1.62686     
        3            58         0.68055       1.57823     
        4            59         0.82760       0.01735     
        5            60       3.586e-005      0.09767     
        6            61       4.154e-005     6.319e-007   
        7            62       4.811e-005     7.262e-007   
        8            63       2.829e-006     5.114e-006   
        9            64       1.071e-005     1.168e-006   
        10           65       4.221e-006     6.708e-007   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.112        start      
        1            56       3.320e-005      0.04893     
        2            57       2.503e-009     9.016e-007   
        3            58       8.859e-010     8.312e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2551.04 psi
   
   Pressure: 
   1525.50, 1530.82, 1536.74, 1541.83, 1548.11, 1556.26, 1563.73, 1569.70, 
   1573.96, 1580.36, 1593.80, 1650.92, 1701.54, 1743.68, 1785.01, 1832.32, 
   1876.65, 1994.62, 2112.45, 2160.26, 2211.99, 2257.86, 2296.65, 2329.24, 
   2366.86
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.53, 0.70, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
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
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.063        start      
        1            53       7.653e-004      0.63479     
        2            54       5.025e-007     3.728e-005   
        3            55       4.195e-010     1.792e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2655.42 psi
   
   Pressure: 
   1525.44, 1530.75, 1536.65, 1541.73, 1548.00, 1556.13, 1563.58, 1569.55, 
   1573.79, 1581.67, 1669.49, 1768.23, 1816.85, 1857.62, 1897.89, 1944.23, 
   1987.76, 2103.75, 2219.77, 2266.89, 2317.95, 2363.27, 2401.64, 2433.95, 
   2471.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.25, 0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
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
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.331        start      
        1            56       8.105e-004      0.20896     
        2            57       1.802e-006     3.302e-005   
        3            58       1.859e-009     4.253e-008   
        4            59       1.279e-010     5.231e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2716.58 psi
   
   Pressure: 
   1525.80, 1531.19, 1537.17, 1542.32, 1548.68, 1556.93, 1564.48, 1570.54, 
   1577.31, 1629.75, 1738.33, 1834.30, 1882.05, 1922.39, 1962.34, 2008.39, 
   2051.67, 2167.00, 2282.36, 2329.21, 2379.97, 2425.02, 2463.18, 2495.31, 
   2532.51
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.31, 0.65, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 
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
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.584        start      
        1            56         0.00103       0.13925     
        2            57       1.891e-006     4.838e-005   
        3            58       1.588e-010     9.290e-008   
        4            59       8.591e-011     3.096e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2744.95 psi
   
   Pressure: 
   1525.83, 1531.22, 1537.21, 1542.37, 1548.72, 1556.98, 1564.62, 1579.20, 
   1615.99, 1671.43, 1776.04, 1869.63, 1916.49, 1956.22, 1995.66, 2041.17, 
   2084.00, 2198.19, 2312.47, 2358.91, 2409.26, 2453.98, 2491.89, 2523.85, 
   2560.90
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
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
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.239        start      
        1            56         0.00128       0.31135     
        2            57       1.727e-006     8.650e-005   
        3            58       3.515e-010     1.403e-007   
        4            59       8.677e-011     2.781e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2790.70 psi
   
   Pressure: 
   1525.64, 1530.99, 1536.93, 1542.04, 1548.35, 1556.95, 1588.86, 1641.23, 
   1677.54, 1730.75, 1832.30, 1923.70, 1969.62, 2008.66, 2047.46, 2092.31, 
   2134.56, 2247.31, 2360.26, 2406.21, 2456.07, 2500.40, 2538.03, 2569.79, 
   2606.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.51, 0.70, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
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
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.576        start      
        1            56         38.8782       18.4003     
        2            57         19.4337       9.19957     
        3            58       1.024e-004      9.19443     
        4            59       3.111e-008     1.993e-005   
        5            60       1.146e-009     9.183e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2850.76 psi
   
   Pressure: 
   1525.52, 1530.85, 1536.76, 1541.86, 1549.11, 1599.33, 1664.21, 1714.57, 
   1749.63, 1801.49, 1900.99, 1990.80, 2036.00, 2074.48, 2112.78, 2157.08, 
   2198.84, 2310.36, 2422.16, 2467.67, 2517.10, 2561.09, 2598.46, 2630.05, 
   2666.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
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
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.124        start      
        1            54       6.905e-005      0.09666     
        2            55       3.224e-008     2.170e-006   
        3            56       3.427e-010     7.650e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2905.44 psi
   
   Pressure: 
   1525.68, 1531.05, 1537.00, 1544.11, 1593.01, 1664.01, 1726.81, 1776.06, 
   1810.58, 1861.85, 1960.43, 2049.51, 2094.37, 2132.59, 2170.62, 2214.63, 
   2256.13, 2366.95, 2478.08, 2523.32, 2572.47, 2616.23, 2653.42, 2684.88, 
   2721.51
   
   Saturation:
   0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 0.74, 0.75, 
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
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.750        start      
        1            56       2.037e-005      0.02587     
        2            57       3.947e-009     5.309e-007   
        3            58       8.958e-010     1.124e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2941.01 psi
   
   Pressure: 
   1525.78, 1531.18, 1541.91, 1584.64, 1639.05, 1707.66, 1769.14, 1817.66, 
   1851.79, 1902.56, 2000.32, 2088.72, 2133.27, 2171.23, 2209.03, 2252.77, 
   2294.03, 2404.22, 2514.73, 2559.74, 2608.64, 2652.20, 2689.23, 2720.57, 
   2757.11
   
   Saturation:
   0.20, 0.20, 0.34, 0.66, 0.73, 0.74, 0.75, 0.76, 
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
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.150        start      
        1            56         40.9855       15.8852     
        2            57         20.4928       7.94105     
        3            58       5.134e-004      7.93276     
        4            59       9.857e-007     6.785e-005   
        5            60       1.777e-009     4.207e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2980.38 psi
   
   Pressure: 
   1526.15, 1540.95, 1592.49, 1636.39, 1688.93, 1755.91, 1816.33, 1864.15, 
   1897.85, 1948.06, 2044.82, 2132.39, 2176.54, 2214.19, 2251.68, 2295.09, 
   2336.05, 2445.49, 2555.28, 2600.02, 2648.65, 2691.98, 2728.85, 2760.07, 
   2796.51
   
   Saturation:
   0.21, 0.43, 0.68, 0.73, 0.75, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
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
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.085        start      
        1            55         239.872       123.815     
        2            56         119.981       61.8974     
        3            57         0.04391       61.9266     
        4            58       2.607e-005      0.01383     
        5            59       9.646e-009     1.423e-005   
        6            60       1.731e-009     4.547e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3055.13 psi
   
   Pressure: 
   1606.89, 1651.71, 1700.37, 1741.32, 1790.97, 1854.74, 1912.57, 1958.50, 
   1990.96, 2039.46, 2133.16, 2218.16, 2261.10, 2297.79, 2334.40, 2376.88, 
   2417.03, 2524.49, 2632.51, 2676.60, 2724.61, 2767.48, 2804.02, 2835.03, 
   2871.31
   
   Saturation:
   0.51, 0.70, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   8/25/2026 1:00:43 AM
   8/25/2026 1:01:50 AM
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
   ⚠️ Runtime Error: time step is too small

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

