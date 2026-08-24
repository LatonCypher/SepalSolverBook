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
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          193.841        start      
        1            56         4.09980       0.72929     
        2            57         1.06903       0.37385     
        3            58       7.457e-006      0.13186     
        4            59       5.969e-008     8.603e-007   
        5            60       1.029e-009     7.464e-009   
   Producer BHP: 
   2250.59 psi
   
   Injector BHP: 
   2697.38 psi
   
   Pressure: 
   2283.53, 2295.38, 2308.26, 2316.80, 2325.26, 2335.73, 2345.26, 2352.80, 
   2360.16, 2366.34, 2371.42, 2376.74, 2384.42, 2391.51, 2397.43, 2412.31, 
   2426.52, 2432.02, 2437.06, 2441.97, 2446.85, 2453.28, 2461.00, 2477.63, 
   2530.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.70
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          173.275        start      
        1            56       1.806e-004      0.52688     
        2            57       2.251e-008     7.797e-006   
        3            58       8.048e-010     8.152e-010   
   Producer BHP: 
   1591.09 psi
   
   Injector BHP: 
   2103.46 psi
   
   Pressure: 
   1624.11, 1635.97, 1648.86, 1657.40, 1665.84, 1676.30, 1685.81, 1693.32, 
   1700.65, 1706.79, 1711.85, 1717.12, 1724.75, 1731.77, 1737.63, 1752.35, 
   1766.39, 1771.81, 1776.79, 1781.62, 1786.42, 1793.25, 1831.80, 1888.05, 
   1935.96
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.54, 0.72, 
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
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.132        start      
        1            56         290.241       173.342     
        2            57         145.119       86.6746     
        3            58         0.02131       86.6596     
        4            59       9.883e-007      0.00834     
        5            60       2.732e-008     2.948e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2027.54 psi
   
   Pressure: 
   1523.81, 1532.47, 1541.98, 1548.37, 1554.76, 1562.78, 1570.16, 1576.06, 
   1581.90, 1586.86, 1591.00, 1595.38, 1601.80, 1607.79, 1612.86, 1625.78, 
   1638.29, 1643.20, 1647.75, 1652.25, 1658.11, 1702.11, 1764.34, 1815.51, 
   1859.96
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.73, 0.76, 
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
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.129        start      
        1            55         0.08129       0.08044     
        2            56         0.07912       0.00110     
        3            57         0.01431       0.03303     
        4            58       1.522e-004      0.00722     
        5            59       9.990e-007     7.812e-005   
        6            60       2.000e-008     5.195e-007   
        7            61       2.571e-008     2.912e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2062.73 psi
   
   Pressure: 
   1523.83, 1532.51, 1542.03, 1548.42, 1554.82, 1562.84, 1570.23, 1576.14, 
   1581.98, 1586.95, 1591.09, 1595.48, 1601.90, 1607.90, 1612.98, 1625.91, 
   1638.42, 1643.34, 1647.92, 1656.28, 1694.15, 1745.50, 1803.84, 1852.44, 
   1895.16
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.35, 0.66, 0.73, 0.76, 0.78, 
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
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          176.488        start      
        1            56         63.8900       18.5399     
        2            57         27.7904       8.11332     
        3            58         8.69812       6.28619     
        4            59         8.74990       0.02474     
        5            60         0.02272       4.17028     
        6            61         0.00341       0.00923     
        7            62       6.886e-004      0.00196     
        8            63       1.133e-006     3.296e-004   
        9            64       4.297e-008     5.618e-007   
        10           65       2.366e-007     9.152e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2094.67 psi
   
   Pressure: 
   1523.82, 1532.49, 1542.00, 1548.39, 1554.78, 1562.80, 1570.18, 1576.09, 
   1581.92, 1586.88, 1591.02, 1595.41, 1601.83, 1607.82, 1612.89, 1625.81, 
   1638.31, 1643.34, 1657.13, 1696.02, 1734.07, 1782.64, 1838.54, 1885.50, 
   1927.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.44, 0.69, 0.74, 0.76, 0.78, 0.79, 
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
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.836        start      
        1            56       6.428e-004      0.16546     
        2            57       2.818e-007     2.401e-005   
        3            58       2.460e-010     1.956e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2135.70 psi
   
   Pressure: 
   1523.74, 1532.38, 1541.86, 1548.22, 1554.60, 1562.59, 1569.94, 1575.83, 
   1581.64, 1586.59, 1590.71, 1595.08, 1601.48, 1607.45, 1612.51, 1625.38, 
   1639.28, 1667.60, 1707.07, 1744.38, 1780.63, 1827.37, 1881.57, 1927.35, 
   1968.15
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.23, 0.56, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
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
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.435        start      
        1            56       1.847e-004      0.22521     
        2            57       8.700e-008     1.157e-005   
        3            58       1.932e-010     3.968e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2223.28 psi
   
   Pressure: 
   1523.59, 1532.17, 1541.59, 1547.91, 1554.25, 1562.18, 1569.49, 1575.34, 
   1581.12, 1586.04, 1590.14, 1594.48, 1600.84, 1606.78, 1611.80, 1628.65, 
   1722.60, 1764.36, 1801.83, 1837.54, 1872.53, 1917.92, 1970.79, 2015.64, 
   2055.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 
   0.62, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
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
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.120        start      
        1            56       1.865e-004      0.12291     
        2            57       1.728e-007     6.808e-006   
        3            58       1.821e-010     4.589e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2321.65 psi
   
   Pressure: 
   1523.70, 1532.32, 1541.78, 1548.13, 1554.49, 1562.47, 1569.81, 1575.68, 
   1581.49, 1586.43, 1590.54, 1594.91, 1601.29, 1607.28, 1615.63, 1722.09, 
   1828.46, 1868.54, 1904.81, 1939.63, 1973.87, 2018.38, 2070.36, 2114.54, 
   2154.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.65, 
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
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.740        start      
        1            54       8.617e-005      0.03081     
        2            55       3.233e-008     2.813e-006   
        3            56       2.983e-010     1.000e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2359.71 psi
   
   Pressure: 
   1523.79, 1532.44, 1541.94, 1548.31, 1554.69, 1562.70, 1570.06, 1575.96, 
   1581.78, 1586.74, 1590.87, 1595.24, 1601.72, 1616.37, 1659.83, 1769.62, 
   1871.83, 1910.84, 1946.31, 1980.48, 2014.15, 2058.00, 2109.29, 2152.97, 
   2192.30
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 0.68, 0.73, 
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
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.830        start      
        1            53         28.4184       14.8352     
        2            54         25.0126       27.8903     
        3            55         0.02312       13.0443     
        4            56       2.609e-004      0.01216     
        5            57       2.486e-006     1.348e-004   
        6            58       3.586e-008     1.317e-006   
        7            59       4.791e-009     2.128e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2401.61 psi
   
   Pressure: 
   1523.72, 1532.34, 1541.80, 1548.16, 1554.52, 1562.49, 1569.84, 1575.71, 
   1581.52, 1586.45, 1590.57, 1595.11, 1619.63, 1671.55, 1714.18, 1819.43, 
   1918.64, 1956.74, 1991.47, 2025.03, 2058.15, 2101.36, 2152.00, 2195.22, 
   2234.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.49, 0.70, 0.74, 0.75, 
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
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.373        start      
        1            56         0.05755       0.08145     
        2            57         0.05965       0.00108     
        3            58         0.00416       0.02864     
        4            59       1.594e-005      0.00215     
        5            60       1.167e-007     8.169e-006   
        6            61       8.596e-008     1.046e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2442.35 psi
   
   Pressure: 
   1523.73, 1532.35, 1541.82, 1548.17, 1554.54, 1562.51, 1569.86, 1575.74, 
   1581.55, 1586.48, 1591.13, 1616.47, 1672.03, 1722.07, 1763.26, 1865.87, 
   1963.12, 2000.58, 2034.78, 2067.87, 2100.57, 2143.28, 2193.41, 2236.25, 
   2274.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.23, 0.57, 0.71, 0.74, 0.75, 0.76, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
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
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.674        start      
        1            55         500.452       254.457     
        2            56         250.263       127.222     
        3            57         0.36871       127.096     
        4            58       4.144e-004      0.13692     
        5            59       4.627e-007     2.201e-004   
        6            60       1.577e-007     3.154e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2469.60 psi
   
   Pressure: 
   1523.74, 1532.36, 1541.84, 1548.20, 1554.56, 1562.54, 1569.89, 1575.77, 
   1581.59, 1587.97, 1617.17, 1654.79, 1708.16, 1756.70, 1796.97, 1897.71, 
   1993.46, 2030.41, 2064.18, 2096.88, 2129.22, 2171.52, 2221.21, 2263.73, 
   2302.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
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
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.955        start      
        1            56       3.829e-004      0.12886     
        2            57       5.436e-007     1.461e-005   
        3            58       2.103e-010     1.525e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2502.48 psi
   
   Pressure: 
   1523.74, 1532.37, 1541.84, 1548.20, 1554.56, 1562.54, 1569.89, 1575.79, 
   1584.84, 1624.71, 1660.16, 1696.39, 1748.27, 1795.81, 1835.38, 1934.62, 
   2029.09, 2065.59, 2098.97, 2131.33, 2163.36, 2205.28, 2254.58, 2296.82, 
   2335.16
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.31, 0.65, 0.72, 0.75, 0.76, 0.76, 0.77, 0.77, 
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
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.800        start      
        1            56         41.6324       18.8670     
        2            57         20.8157       9.43373     
        3            58       1.227e-004      9.43348     
        4            59       9.668e-009     1.894e-005   
        5            60       3.370e-010     4.032e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2540.84 psi
   
   Pressure: 
   1523.74, 1532.36, 1541.84, 1548.19, 1554.56, 1562.54, 1569.93, 1581.19, 
   1630.16, 1672.39, 1706.55, 1741.87, 1792.75, 1839.53, 1878.54, 1976.52, 
   2069.90, 2106.01, 2139.05, 2171.10, 2202.85, 2244.44, 2293.39, 2335.38, 
   2373.54
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 
   0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
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
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.322        start      
        1            54         136.392       5.21309     
        2            55         68.1925       2.60650     
        3            56         0.00106       2.60718     
        4            57       5.749e-008     2.972e-004   
        5            58       5.750e-008     1.337e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2583.45 psi
   
   Pressure: 
   1523.66, 1532.25, 1541.69, 1548.02, 1554.36, 1562.44, 1582.82, 1633.37, 
   1682.75, 1723.44, 1756.70, 1791.31, 1841.32, 1887.39, 1925.87, 2022.61, 
   2114.91, 2150.63, 2183.34, 2215.09, 2246.57, 2287.83, 2336.44, 2378.19, 
   2416.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.43, 0.68, 
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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          167.042        start      
        1            56       8.807e-005      0.07458     
        2            57       1.983e-008     3.539e-006   
        3            58       4.262e-010     8.214e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2640.57 psi
   
   Pressure: 
   1523.54, 1532.10, 1541.49, 1547.79, 1554.43, 1587.86, 1651.43, 1700.99, 
   1748.60, 1788.24, 1820.83, 1854.84, 1904.08, 1949.51, 1987.50, 2083.08, 
   2174.34, 2209.69, 2242.07, 2273.54, 2304.75, 2345.70, 2393.99, 2435.51, 
   2473.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
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
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.008        start      
        1            56       1.758e-004      0.14595     
        2            57       3.805e-008     7.076e-006   
        3            58       2.337e-010     2.593e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2697.42 psi
   
   Pressure: 
   1523.54, 1532.08, 1541.47, 1548.69, 1587.07, 1655.98, 1717.34, 1765.38, 
   1811.97, 1850.97, 1883.10, 1916.71, 1965.40, 2010.35, 2047.96, 2142.65, 
   2233.11, 2268.16, 2300.29, 2331.51, 2362.51, 2403.20, 2451.22, 2492.54, 
   2530.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.24, 0.57, 0.71, 0.74, 0.75, 
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
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.060        start      
        1            55         994.864       520.734     
        2            56         525.179       245.093     
        3            57         0.91158       275.205     
        4            58         0.07869       0.39258     
        5            59         0.00901       0.04723     
        6            60       5.737e-006      0.00485     
        7            61       2.441e-007     2.953e-006   
        8            62       1.118e-007     1.906e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2745.35 psi
   
   Pressure: 
   1523.55, 1532.11, 1544.54, 1591.58, 1646.18, 1712.58, 1772.35, 1819.46, 
   1865.35, 1903.87, 1935.65, 1968.92, 2017.16, 2061.72, 2099.01, 2192.94, 
   2282.70, 2317.50, 2349.40, 2380.43, 2411.24, 2451.70, 2499.48, 2540.62, 
   2578.19
   
   Saturation:
   0.20, 0.20, 0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 
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
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.648        start      
        1            56       8.596e-004      0.43439     
        2            57       1.806e-006     4.984e-005   
        3            58       4.916e-010     1.427e-007   
        4            59       9.231e-011     4.982e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2809.09 psi
   
   Pressure: 
   1523.53, 1537.69, 1614.09, 1668.14, 1720.65, 1785.20, 1843.73, 1890.01, 
   1935.21, 1973.21, 2004.60, 2037.49, 2085.20, 2129.30, 2166.23, 2259.28, 
   2348.26, 2382.77, 2414.43, 2445.23, 2475.83, 2516.05, 2563.58, 2604.53, 
   2641.97
   
   Saturation:
   0.20, 0.32, 0.65, 0.72, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
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
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.964        start      
        1            55         235.010       144.815     
        2            56         117.505       72.4059     
        3            57         0.00792       72.4080     
        4            58       1.435e-006     4.033e-004   
        5            59       9.037e-008     3.226e-007   
        6            60       8.289e-007     5.666e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2887.25 psi
   
   Pressure: 
   1553.46, 1626.52, 1706.21, 1758.08, 1809.06, 1872.14, 1929.55, 1975.04, 
   2019.56, 2057.03, 2088.02, 2120.51, 2167.69, 2211.32, 2247.88, 2340.05, 
   2428.24, 2462.46, 2493.88, 2524.45, 2554.85, 2594.82, 2642.10, 2682.87, 
   2720.17
   
   Saturation:
   0.39, 0.68, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   8/24/2026 10:58:18 PM
   8/24/2026 10:59:35 PM
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
   ⚠️ Runtime Error: time step is too small

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

