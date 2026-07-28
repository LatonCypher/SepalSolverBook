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
   
   
   
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          197.975        start      
        1            56         0.00267       5.75726     
        2            57       3.175e-006     7.248e-004   
        3            58       1.017e-008     1.541e-007   
        4            59       7.532e-011     7.730e-010   
   Producer BHP: 
   2247.14 psi
   
   Injector BHP: 
   2937.90 psi
   
   Pressure: 
   2289.32, 2298.97, 2306.67, 2312.93, 2320.93, 2327.37, 2331.25, 2337.10, 
   2343.75, 2350.40, 2357.57, 2364.41, 2371.85, 2381.66, 2393.06, 2401.25, 
   2407.18, 2413.95, 2420.14, 2426.83, 2433.95, 2445.06, 2457.92, 2477.46, 
   2566.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          178.900        start      
        1            56         0.00308       6.12115     
        2            57       1.672e-006     5.539e-004   
        3            58       2.152e-009     1.098e-007   
        4            59       7.182e-011     2.098e-010   
   Producer BHP: 
   1577.35 psi
   
   Injector BHP: 
   2351.47 psi
   
   Pressure: 
   1619.62, 1629.29, 1636.99, 1643.25, 1651.24, 1657.66, 1661.53, 1667.36, 
   1673.98, 1680.59, 1687.70, 1694.49, 1701.87, 1711.58, 1722.85, 1730.94, 
   1736.79, 1743.47, 1749.57, 1756.14, 1763.13, 1774.69, 1832.38, 1899.13, 
   1979.37
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.52, 0.71, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.890        start      
        1            56         0.00254       0.53361     
        2            57       3.724e-006     1.022e-004   
        3            58       6.101e-009     1.220e-007   
        4            59       3.957e-011     2.349e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2334.65 psi
   
   Pressure: 
   1530.41, 1537.44, 1543.12, 1547.78, 1553.81, 1558.72, 1561.72, 1566.29, 
   1571.55, 1576.88, 1582.69, 1588.31, 1594.51, 1602.77, 1612.50, 1619.59, 
   1624.78, 1630.79, 1636.36, 1642.46, 1650.62, 1723.23, 1827.34, 1888.00, 
   1962.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.037        start      
        1            56         0.00449       0.21152     
        2            57       2.280e-005     2.201e-004   
        3            58       3.921e-008     9.346e-007   
        4            59       5.735e-011     2.514e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2390.64 psi
   
   Pressure: 
   1530.51, 1537.56, 1543.26, 1547.93, 1553.98, 1558.91, 1561.91, 1566.50, 
   1571.78, 1577.12, 1582.95, 1588.59, 1594.80, 1603.09, 1612.85, 1619.96, 
   1625.17, 1631.20, 1636.80, 1647.27, 1702.77, 1791.68, 1889.27, 1946.86, 
   2018.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.33, 0.66, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.704        start      
        1            56         0.00631       0.22753     
        2            57       2.829e-005     3.059e-004   
        3            58       1.848e-008     1.688e-006   
        4            59       4.181e-011     8.209e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2427.73 psi
   
   Pressure: 
   1530.50, 1537.56, 1543.25, 1547.92, 1553.97, 1558.89, 1561.89, 1566.48, 
   1571.76, 1577.10, 1582.92, 1588.56, 1594.77, 1603.06, 1612.81, 1619.91, 
   1625.12, 1631.22, 1644.66, 1697.36, 1752.77, 1836.78, 1930.19, 1985.81, 
   2055.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.40, 0.68, 0.74, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.744        start      
        1            56         0.00885       0.50423     
        2            57       3.365e-005     6.209e-004   
        3            58       7.377e-009     3.233e-006   
        4            59       4.627e-011     4.321e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2468.59 psi
   
   Pressure: 
   1530.41, 1537.45, 1543.12, 1547.78, 1553.81, 1558.72, 1561.71, 1566.29, 
   1571.55, 1576.87, 1582.68, 1588.30, 1594.49, 1602.74, 1612.47, 1619.55, 
   1625.02, 1650.18, 1698.61, 1749.66, 1802.46, 1883.29, 1973.81, 2028.03, 
   2096.59
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.50, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.029        start      
        1            56         0.00366       0.49039     
        2            57       8.940e-006     1.281e-004   
        3            58       7.341e-009     3.566e-007   
        4            59       4.061e-011     3.866e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2515.86 psi
   
   Pressure: 
   1530.39, 1537.42, 1543.09, 1547.75, 1553.77, 1558.68, 1561.67, 1566.24, 
   1571.50, 1576.82, 1582.62, 1588.24, 1594.43, 1602.68, 1612.41, 1621.21, 
   1657.47, 1709.45, 1755.86, 1804.82, 1855.98, 1934.68, 2023.15, 2076.35, 
   2143.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 
   0.60, 0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.698        start      
        1            56         0.00478       0.49283     
        2            57       2.361e-005     3.118e-004   
        3            58       1.193e-008     1.476e-006   
        4            59       5.773e-011     1.357e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2569.38 psi
   
   Pressure: 
   1530.38, 1537.40, 1543.07, 1547.72, 1553.74, 1558.65, 1561.64, 1566.21, 
   1571.46, 1576.77, 1582.58, 1588.19, 1594.38, 1602.65, 1619.43, 1678.16, 
   1722.53, 1772.14, 1816.84, 1864.41, 1914.38, 1991.45, 2078.33, 2130.72, 
   2197.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          173.636        start      
        1            56         0.00782       0.53822     
        2            57       3.843e-005     5.967e-004   
        3            58       8.062e-009     3.366e-006   
        4            59       3.837e-011     5.384e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2642.15 psi
   
   Pressure: 
   1530.36, 1537.38, 1543.04, 1547.69, 1553.71, 1558.61, 1561.60, 1566.16, 
   1571.41, 1576.72, 1582.52, 1588.13, 1594.40, 1617.12, 1701.60, 1761.59, 
   1804.12, 1852.15, 1895.68, 1942.21, 1991.23, 2066.97, 2152.53, 2204.24, 
   2270.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.21, 0.42, 0.69, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.387        start      
        1            56         0.00761       0.58994     
        2            57       2.668e-005     5.252e-004   
        3            58       7.171e-009     2.635e-006   
        4            59       4.465e-011     4.351e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2699.71 psi
   
   Pressure: 
   1530.34, 1537.35, 1543.00, 1547.65, 1553.66, 1558.55, 1561.54, 1566.10, 
   1571.34, 1576.64, 1582.44, 1588.38, 1616.03, 1687.84, 1769.53, 1827.23, 
   1868.58, 1915.50, 1958.16, 2003.86, 2052.11, 2126.77, 2211.26, 2262.41, 
   2328.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.22, 0.52, 0.71, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.674        start      
        1            56         0.00407       0.61546     
        2            57       2.276e-006     1.047e-004   
        3            58       1.939e-009     1.794e-007   
        4            59       4.005e-011     2.131e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2742.64 psi
   
   Pressure: 
   1530.31, 1537.31, 1542.96, 1547.60, 1553.60, 1558.49, 1561.48, 1566.03, 
   1571.27, 1576.57, 1583.43, 1619.14, 1672.60, 1741.42, 1820.35, 1876.60, 
   1917.10, 1963.19, 2005.14, 2050.18, 2097.80, 2171.56, 2255.14, 2305.84, 
   2371.03
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.24, 0.58, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.438        start      
        1            56         0.00298       0.46027     
        2            57       1.340e-005     1.928e-004   
        3            58       2.777e-008     3.748e-007   
        4            59       5.046e-011     9.524e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2785.06 psi
   
   Pressure: 
   1530.36, 1537.38, 1543.03, 1547.68, 1553.70, 1558.59, 1561.58, 1566.15, 
   1571.40, 1579.05, 1624.90, 1673.27, 1724.65, 1791.53, 1868.81, 1924.10, 
   1963.99, 2009.44, 2050.87, 2095.39, 2142.49, 2215.53, 2298.37, 2348.69, 
   2413.52
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.29, 0.64, 0.72, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.381        start      
        1            56         0.00512       0.36297     
        2            57       2.636e-005     2.040e-004   
        3            58       1.031e-008     1.166e-006   
        4            59       5.453e-011     8.947e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2819.64 psi
   
   Pressure: 
   1530.40, 1537.42, 1543.09, 1547.74, 1553.76, 1558.67, 1561.66, 1566.25, 
   1576.33, 1620.62, 1670.24, 1716.79, 1766.85, 1832.44, 1908.49, 1963.01, 
   2002.40, 2047.32, 2088.29, 2132.36, 2179.03, 2251.44, 2333.65, 2383.64, 
   2448.15
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.66, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.150        start      
        1            56         0.00778       0.35247     
        2            57       3.843e-005     3.911e-004   
        3            58       1.735e-008     2.440e-006   
        4            59       4.556e-011     9.778e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2855.07 psi
   
   Pressure: 
   1530.40, 1537.43, 1543.09, 1547.74, 1553.77, 1558.67, 1561.73, 1575.62, 
   1621.16, 1666.50, 1714.40, 1759.80, 1808.93, 1873.51, 1948.52, 2002.37, 
   2041.31, 2085.75, 2126.31, 2169.97, 2216.24, 2288.08, 2369.71, 2419.41, 
   2483.63
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.44, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          161.991        start      
        1            56         0.00647       0.30805     
        2            57       2.073e-005     2.351e-004   
        3            58       3.289e-009     1.265e-006   
        4            59       3.895e-011     8.482e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2881.42 psi
   
   Pressure: 
   1530.42, 1537.45, 1543.12, 1547.77, 1553.80, 1559.08, 1572.99, 1612.73, 
   1657.15, 1700.99, 1747.83, 1792.45, 1840.89, 1904.67, 1978.83, 2032.11, 
   2070.67, 2114.69, 2154.90, 2198.20, 2244.12, 2315.46, 2396.60, 2446.03, 
   2510.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.52, 0.71, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.644        start      
        1            56         0.00378       0.62242     
        2            57       1.741e-006     9.771e-005   
        3            58       1.869e-009     1.790e-007   
        4            59       5.095e-011     2.329e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2906.55 psi
   
   Pressure: 
   1530.25, 1537.24, 1542.87, 1547.50, 1554.56, 1585.28, 1611.01, 1649.20, 
   1692.12, 1734.82, 1780.71, 1824.56, 1872.24, 1935.11, 2008.29, 2060.92, 
   2099.03, 2142.59, 2182.39, 2225.30, 2270.84, 2341.65, 2422.27, 2471.44, 
   2535.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.788        start      
        1            56         0.00261       0.64544     
        2            57       1.007e-005     1.989e-004   
        3            58       1.942e-008     3.092e-007   
        4            59       4.979e-011     5.135e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2947.85 psi
   
   Pressure: 
   1530.23, 1537.21, 1542.85, 1549.12, 1594.52, 1636.67, 1661.53, 1698.70, 
   1740.76, 1782.76, 1828.03, 1871.34, 1918.48, 1980.69, 2053.16, 2105.28, 
   2143.06, 2186.25, 2225.73, 2268.32, 2313.54, 2383.91, 2464.07, 2513.02, 
   2576.54
   
   Saturation:
   0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.239        start      
        1            56         0.00474       0.47209     
        2            57       2.451e-005     1.953e-004   
        3            58       9.657e-009     1.248e-006   
        4            59       4.703e-011     1.068e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2981.88 psi
   
   Pressure: 
   1530.32, 1537.34, 1547.40, 1585.03, 1636.64, 1677.42, 1701.74, 1738.35, 
   1779.90, 1821.48, 1866.35, 1909.31, 1956.11, 2017.88, 2089.85, 2141.64, 
   2179.18, 2222.10, 2261.36, 2303.70, 2348.69, 2418.72, 2498.53, 2547.29, 
   2610.63
   
   Saturation:
   0.20, 0.20, 0.34, 0.65, 0.73, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.154        start      
        1            56         0.00723       0.59311     
        2            57       3.534e-005     6.082e-004   
        3            58       3.012e-008     3.900e-006   
        4            59       4.639e-011     3.160e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3025.17 psi
   
   Pressure: 
   1530.74, 1550.12, 1598.94, 1638.63, 1688.42, 1728.19, 1752.08, 1788.14, 
   1829.16, 1870.25, 1914.64, 1957.18, 2003.54, 2064.77, 2136.14, 2187.52, 
   2224.77, 2267.39, 2306.38, 2348.46, 2393.18, 2462.84, 2542.27, 2590.83, 
   2653.98
   
   Saturation:
   0.21, 0.43, 0.69, 0.73, 0.75, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          155.744        start      
        1            56         0.00637       3.35840     
        2            57       1.627e-005      0.00176     
        3            58       8.014e-009     6.438e-006   
        4            59       3.750e-011     3.266e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3122.15 psi
   
   Pressure: 
   1623.45, 1681.31, 1726.80, 1763.45, 1810.09, 1847.64, 1870.33, 1904.72, 
   1943.97, 1983.41, 2026.15, 2067.21, 2112.08, 2171.48, 2240.88, 2290.95, 
   2327.34, 2369.05, 2407.28, 2448.65, 2492.72, 2561.49, 2640.11, 2688.29, 
   2751.10
   
   Saturation:
   0.51, 0.70, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/28/2026 7:47:31 AM
   7/28/2026 7:49:08 AM
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
   
   
   
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          243.459        start      
        1            56         0.00329       0.40062     
        2            57       1.102e-005     2.465e-004   
        3            58       7.423e-009     5.147e-007   
        4            59       8.316e-011     4.925e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2538.52 psi
   
   Pressure: 
   1545.76, 1556.35, 1564.89, 1571.91, 1580.98, 1588.38, 1592.89, 1599.78, 
   1607.70, 1615.71, 1624.46, 1632.92, 1642.24, 1654.68, 1669.33, 1679.99, 
   1687.81, 1696.85, 1705.23, 1714.41, 1724.32, 1740.02, 1765.10, 1855.13, 
   1980.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.64, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          230.313        start      
        1            56         0.00386       0.78965     
        2            57       4.130e-006     1.702e-004   
        3            58       5.855e-009     1.373e-007   
        4            59       4.871e-011     1.721e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2748.55 psi
   
   Pressure: 
   1545.54, 1556.07, 1564.57, 1571.55, 1580.58, 1587.94, 1592.42, 1599.28, 
   1607.16, 1615.13, 1623.83, 1632.26, 1641.53, 1653.90, 1668.48, 1679.09, 
   1686.87, 1695.87, 1704.21, 1713.34, 1725.42, 1833.34, 1989.19, 2079.73, 
   2190.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          243.542        start      
        1            56         0.00540       0.53951     
        2            57       6.556e-006     2.714e-004   
        3            58       6.138e-009     6.390e-007   
        4            59       8.918e-011     3.381e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2860.35 psi
   
   Pressure: 
   1545.62, 1556.17, 1564.68, 1571.67, 1580.71, 1588.08, 1592.57, 1599.44, 
   1607.33, 1615.31, 1624.03, 1632.46, 1641.75, 1654.14, 1668.73, 1679.35, 
   1687.13, 1696.15, 1705.38, 1756.42, 1841.79, 1970.55, 2112.83, 2197.22, 
   2302.81
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.23, 0.56, 0.72, 0.75, 0.77, 0.79, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          250.903        start      
        1            56         0.00826       0.55417     
        2            57       2.431e-005     5.007e-004   
        3            58       7.491e-009     1.836e-006   
        4            59       7.156e-011     4.436e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2949.34 psi
   
   Pressure: 
   1545.57, 1556.11, 1564.62, 1571.60, 1580.63, 1587.99, 1592.48, 1599.34, 
   1607.22, 1615.19, 1623.89, 1632.32, 1641.59, 1653.96, 1668.53, 1679.14, 
   1687.28, 1723.98, 1796.67, 1873.12, 1952.13, 2073.04, 2208.41, 2289.47, 
   2391.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.50, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          261.751        start      
        1            56         0.00786       0.58512     
        2            57       2.497e-005     5.227e-004   
        3            58       6.127e-009     1.895e-006   
        4            59       6.290e-011     4.051e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3048.66 psi
   
   Pressure: 
   1545.51, 1556.03, 1564.52, 1571.49, 1580.51, 1587.86, 1592.34, 1599.18, 
   1607.05, 1615.01, 1623.70, 1632.11, 1641.37, 1653.72, 1668.69, 1705.42, 
   1773.08, 1848.97, 1917.00, 1989.11, 2064.69, 2181.09, 2312.13, 2391.04, 
   2491.52
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.47, 
   0.70, 0.74, 0.75, 0.77, 0.78, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          260.171        start      
        1            56         0.00692       0.59111     
        2            57       2.530e-005     4.562e-004   
        3            58       2.477e-009     1.763e-006   
        4            59       7.425e-011     1.488e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3207.37 psi
   
   Pressure: 
   1545.43, 1555.94, 1564.42, 1571.37, 1580.38, 1587.71, 1592.19, 1599.02, 
   1606.88, 1614.83, 1623.50, 1631.90, 1641.26, 1673.62, 1800.17, 1889.98, 
   1953.59, 2025.42, 2090.50, 2160.05, 2233.32, 2346.52, 2474.39, 2551.67, 
   2650.58
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.42, 0.69, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.767        start      
        1            56         0.00445       0.38976     
        2            57       1.666e-005     1.762e-004   
        3            58       7.632e-009     6.412e-007   
        4            59       7.525e-011     4.053e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3330.28 psi
   
   Pressure: 
   1545.54, 1556.07, 1564.56, 1571.54, 1580.56, 1587.92, 1592.40, 1599.25, 
   1607.13, 1615.10, 1623.84, 1639.53, 1718.11, 1823.83, 1944.09, 2029.44, 
   2090.73, 2160.36, 2223.68, 2291.57, 2363.27, 2474.24, 2599.86, 2675.98, 
   2773.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.35, 0.67, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          223.827        start      
        1            56         0.00255       0.59512     
        2            57       7.969e-006     1.297e-004   
        3            58       9.349e-009     2.046e-007   
        4            59       7.411e-011     2.698e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3419.45 psi
   
   Pressure: 
   1545.33, 1555.81, 1564.27, 1571.21, 1580.19, 1587.51, 1591.98, 1598.80, 
   1606.65, 1617.59, 1685.29, 1757.65, 1834.43, 1934.31, 2049.69, 2132.23, 
   2191.79, 2259.64, 2321.49, 2387.96, 2458.30, 2567.36, 2691.09, 2766.25, 
   2863.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.28, 0.63, 0.72, 0.75, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          222.756        start      
        1            56         0.00456       0.72772     
        2            57       3.149e-006     1.489e-004   
        3            58       4.294e-009     3.120e-007   
        4            59       1.008e-010     2.274e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3497.33 psi
   
   Pressure: 
   1545.20, 1555.64, 1564.07, 1570.99, 1579.94, 1587.24, 1591.69, 1599.48, 
   1646.97, 1715.88, 1788.32, 1856.66, 1930.42, 2027.27, 2139.70, 2220.38, 
   2278.71, 2345.28, 2406.04, 2471.44, 2540.74, 2648.34, 2770.60, 2845.01, 
   2941.17
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 
   0.57, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          249.683        start      
        1            56         0.00645       0.32188     
        2            57       1.828e-005     2.176e-004   
        3            58       3.453e-009     8.701e-007   
        4            59       8.682e-011     1.214e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3565.27 psi
   
   Pressure: 
   1545.55, 1556.08, 1564.58, 1571.55, 1580.58, 1588.35, 1607.70, 1667.40, 
   1734.00, 1799.66, 1869.79, 1936.58, 2009.05, 2104.45, 2215.37, 2295.03, 
   2352.68, 2418.50, 2478.60, 2543.31, 2611.94, 2718.55, 2839.78, 2913.65, 
   3009.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.51, 0.70, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          255.716        start      
        1            56         0.00573       0.47900     
        2            57       2.031e-005     3.004e-004   
        3            58       5.532e-009     1.237e-006   
        4            59       9.161e-011     2.810e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3627.30 psi
   
   Pressure: 
   1545.36, 1555.84, 1564.30, 1571.32, 1594.07, 1657.39, 1695.41, 1751.87, 
   1815.56, 1879.03, 1947.32, 2012.59, 2083.60, 2177.24, 2286.24, 2364.62, 
   2421.40, 2486.28, 2545.57, 2609.49, 2677.34, 2782.86, 2903.01, 2976.33, 
   3071.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.41, 0.68, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          226.174        start      
        1            56         0.00359       0.57771     
        2            57       1.343e-005     1.409e-004   
        3            58       1.196e-008     5.559e-007   
        4            59       1.062e-010     7.426e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3711.41 psi
   
   Pressure: 
   1545.23, 1555.71, 1569.40, 1624.68, 1701.89, 1762.81, 1799.13, 1853.76, 
   1915.78, 1977.83, 2044.78, 2108.88, 2178.71, 2270.88, 2378.27, 2455.55, 
   2511.56, 2575.63, 2634.21, 2697.42, 2764.59, 2869.13, 2988.30, 3061.11, 
   3155.73
   
   Saturation:
   0.20, 0.20, 0.32, 0.64, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          214.294        start      
        1            56         0.00263       1.39344     
        2            57       5.584e-006     2.681e-004   
        3            58       1.017e-009     8.177e-007   
        4            59       8.342e-011     2.592e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3815.85 psi
   
   Pressure: 
   1557.70, 1633.06, 1704.66, 1761.93, 1834.27, 1892.26, 1927.18, 1979.98, 
   2040.13, 2100.45, 2165.70, 2228.28, 2296.55, 2386.81, 2492.10, 2567.97, 
   2623.04, 2686.08, 2743.81, 2806.17, 2872.53, 2975.95, 3094.03, 3166.30, 
   3260.40
   
   Saturation:
   0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          35.9720        start      
        1            56         0.00121       1.49286     
        2            57       1.113e-006     9.489e-004   
        3            58       2.175e-010     2.419e-007   
        4            59       2.044e-011     1.028e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4152.72 psi
   
   Pressure: 
   1896.68, 1985.12, 2054.62, 2110.80, 2182.39, 2239.99, 2274.73, 2327.29, 
   2387.15, 2447.17, 2512.06, 2574.27, 2642.11, 2731.74, 2836.25, 2911.53, 
   2966.14, 3028.65, 3085.87, 3147.68, 3213.44, 3315.94, 3432.97, 3504.63, 
   3598.01
   
   Saturation:
   0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.45807        start      
        1            56       7.478e-004      3.43753     
        2            57       1.090e-006      0.00160     
        3            58       4.399e-010     6.019e-007   
        4            59       8.959e-012     4.151e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4133.53 psi
   
   Pressure: 
   1880.58, 1967.02, 2035.74, 2091.56, 2162.97, 2220.54, 2255.30, 2307.92, 
   2367.87, 2427.99, 2492.99, 2555.30, 2623.23, 2712.95, 2817.55, 2892.87, 
   2947.49, 3009.99, 3067.19, 3128.95, 3194.65, 3297.03, 3413.91, 3485.48, 
   3578.78
   
   Saturation:
   0.75, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.30584        start      
        1            56       7.145e-004      5.18524     
        2            57       1.335e-006      0.00328     
        3            58       8.529e-010     1.558e-006   
        4            59       7.005e-012     1.737e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4108.16 psi
   
   Pressure: 
   1869.67, 1954.20, 2021.67, 2076.60, 2147.00, 2203.85, 2238.20, 2290.25, 
   2349.59, 2409.12, 2473.53, 2535.29, 2602.65, 2691.67, 2795.50, 2870.28, 
   2924.54, 2986.66, 3043.53, 3104.97, 3170.36, 3272.32, 3388.81, 3460.20, 
   3553.36
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.12283        start      
        1            56       5.751e-004      6.08171     
        2            57       1.183e-006      0.00416     
        3            58       8.200e-010     2.315e-006   
        4            59       7.161e-012     2.462e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4090.06 psi
   
   Pressure: 
   1863.20, 1946.46, 2013.05, 2067.32, 2136.95, 2193.22, 2227.24, 2278.82, 
   2337.65, 2396.69, 2460.60, 2521.90, 2588.78, 2677.20, 2780.35, 2854.67, 
   2908.62, 2970.40, 3026.98, 3088.14, 3153.26, 3254.84, 3370.97, 3442.19, 
   3535.21
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.46958        start      
        1            56       6.427e-004      7.80004     
        2            57       1.698e-006      0.00661     
        3            58       1.465e-009     4.861e-006   
        4            59       7.865e-012     6.645e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4075.33 psi
   
   Pressure: 
   1858.52, 1940.81, 2006.69, 2060.42, 2129.41, 2185.19, 2218.93, 2270.10, 
   2328.48, 2387.10, 2450.55, 2511.44, 2577.90, 2665.77, 2768.32, 2842.23, 
   2895.90, 2957.37, 3013.69, 3074.59, 3139.46, 3240.70, 3356.50, 3427.56, 
   3520.45
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.07623        start      
        1            56       5.968e-004      8.53710     
        2            57       1.740e-006      0.00741     
        3            58       1.544e-009     6.071e-006   
        4            59       8.133e-012     8.374e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4062.66 psi
   
   Pressure: 
   1854.78, 1936.28, 2001.57, 2054.83, 2123.26, 2178.60, 2212.10, 2262.91, 
   2320.89, 2379.11, 2442.17, 2502.69, 2568.75, 2656.13, 2758.13, 2831.67, 
   2885.08, 2946.27, 3002.35, 3063.01, 3127.65, 3228.58, 3344.06, 3414.98, 
   3507.75
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.71401        start      
        1            56       3.644e-004      7.49477     
        2            57       9.380e-007      0.00501     
        3            58       6.194e-010     3.673e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4051.42 psi
   
   Pressure: 
   1851.62, 1932.44, 1997.21, 2050.07, 2118.00, 2172.95, 2206.22, 2256.70, 
   2314.31, 2372.19, 2434.87, 2495.05, 2560.76, 2647.69, 2749.19, 2822.37, 
   2875.54, 2936.48, 2992.34, 3052.78, 3117.21, 3217.84, 3333.05, 3403.82, 
   3496.48
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/28/2026 7:50:05 AM
   7/28/2026 7:51:41 AM
   ======================================================================
              Starting simulation for Rate = 400 STB/day
   
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
   
   
   
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          355.287        start      
        1            56         0.00886       0.99204     
        2            57       1.283e-005     8.198e-004   
        3            58       7.153e-009     1.367e-006   
        4            59       8.053e-011     5.518e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2962.01 psi
   
   Pressure: 
   1560.80, 1574.86, 1586.21, 1595.52, 1607.58, 1617.40, 1623.39, 1632.54, 
   1643.05, 1653.69, 1665.30, 1676.54, 1688.91, 1705.42, 1724.85, 1739.00, 
   1749.37, 1761.37, 1772.48, 1784.64, 1797.79, 1819.71, 1929.39, 2060.50, 
   2218.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.52, 0.72, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          330.596        start      
        1            56         0.00389       0.29373     
        2            57       1.164e-005     1.574e-004   
        3            58       7.744e-009     3.779e-007   
        4            59       1.272e-010     3.302e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3277.18 psi
   
   Pressure: 
   1560.97, 1575.08, 1586.46, 1595.80, 1607.90, 1617.75, 1623.76, 1632.93, 
   1643.49, 1654.16, 1665.82, 1677.10, 1689.52, 1706.09, 1725.61, 1739.82, 
   1750.24, 1762.30, 1773.49, 1793.82, 1905.25, 2082.83, 2277.29, 2391.90, 
   2534.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.32, 0.66, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          335.972        start      
        1            56         0.00834       0.60533     
        2            57       2.043e-005     4.534e-004   
        3            58       6.617e-009     1.309e-006   
        4            59       1.119e-010     3.500e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3429.01 psi
   
   Pressure: 
   1560.72, 1574.77, 1586.10, 1595.40, 1607.44, 1617.25, 1623.23, 1632.36, 
   1642.86, 1653.48, 1665.08, 1676.30, 1688.65, 1705.13, 1724.54, 1738.66, 
   1749.47, 1797.40, 1894.28, 1996.07, 2101.25, 2262.20, 2442.37, 2550.27, 
   2686.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.49, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          313.997        start      
        1            56         0.00395       0.69068     
        2            57       1.145e-005     2.221e-004   
        3            58       4.091e-009     5.185e-007   
        4            59       9.415e-011     2.297e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3626.35 psi
   
   Pressure: 
   1560.38, 1574.35, 1585.62, 1594.87, 1606.84, 1616.59, 1622.54, 1631.63, 
   1642.08, 1652.65, 1664.20, 1675.37, 1687.67, 1704.12, 1735.60, 1852.01, 
   1940.36, 2039.01, 2127.87, 2222.41, 2321.73, 2474.91, 2647.60, 2751.74, 
   2884.64
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.65, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          341.036        start      
        1            56         0.00742       0.65822     
        2            57       1.783e-005     4.111e-004   
        3            58       5.004e-009     1.167e-006   
        4            59       1.521e-010     2.619e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3882.95 psi
   
   Pressure: 
   1560.44, 1574.41, 1585.69, 1594.94, 1606.92, 1616.68, 1622.63, 1631.72, 
   1642.16, 1652.73, 1664.27, 1675.91, 1725.79, 1869.19, 2031.91, 2146.73, 
   2228.96, 2322.25, 2407.03, 2497.87, 2593.75, 2742.09, 2909.92, 3011.56, 
   3142.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          294.419        start      
        1            56         0.00248       0.72555     
        2            57       5.609e-006     9.751e-005   
        3            58       4.481e-009     1.323e-007   
        4            59       1.363e-010     1.088e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4048.72 psi
   
   Pressure: 
   1560.17, 1574.09, 1585.31, 1594.53, 1606.46, 1616.18, 1622.10, 1631.16, 
   1641.58, 1655.61, 1744.05, 1840.23, 1942.21, 2074.81, 2227.95, 2337.51, 
   2416.56, 2506.64, 2588.75, 2677.01, 2770.43, 2915.29, 3079.65, 3179.52, 
   3308.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.27, 0.63, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          342.350        start      
        1            56         0.00646       0.43640     
        2            57       2.093e-005     2.427e-004   
        3            58       1.200e-009     8.477e-007   
        4            59       1.239e-010     4.891e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4190.94 psi
   
   Pressure: 
   1560.53, 1574.52, 1585.81, 1595.08, 1607.07, 1616.85, 1622.88, 1646.40, 
   1737.25, 1827.74, 1923.13, 2013.47, 2111.16, 2239.51, 2388.53, 2495.47, 
   2572.80, 2661.03, 2741.56, 2828.23, 2920.08, 3062.68, 3224.71, 3323.36, 
   3450.90
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          310.501        start      
        1            56         0.00482       0.72051     
        2            57       5.223e-006     1.871e-004   
        3            58       3.534e-009     3.593e-007   
        4            59       9.470e-011     1.572e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4287.53 psi
   
   Pressure: 
   1560.03, 1573.91, 1585.10, 1594.30, 1607.51, 1661.69, 1713.06, 1789.03, 
   1874.33, 1959.15, 2050.28, 2137.32, 2231.97, 2356.75, 2502.00, 2606.43, 
   2682.08, 2768.51, 2847.51, 2932.66, 3023.05, 3163.59, 3323.59, 3421.20, 
   3547.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.23, 0.56, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          295.517        start      
        1            56         0.00301       0.68159     
        2            57       8.771e-006     1.155e-004   
        3            58       9.398e-009     3.045e-007   
        4            59       1.376e-010     4.156e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4433.90 psi
   
   Pressure: 
   1560.01, 1573.90, 1590.73, 1662.35, 1764.98, 1845.88, 1894.08, 1966.58, 
   2048.87, 2131.18, 2220.00, 2305.04, 2397.68, 2519.97, 2662.45, 2764.99, 
   2839.33, 2924.35, 3002.11, 3086.02, 3175.19, 3313.99, 3472.21, 3568.91, 
   3694.58
   
   Saturation:
   0.20, 0.20, 0.30, 0.63, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          310.928        start      
        1            56         0.00601       1.95515     
        2            57       8.837e-006      0.00125     
        3            58       7.086e-009     2.933e-006   
        4            59       1.257e-010     2.393e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1667.52, 1777.67, 1864.54, 1934.43, 2023.25, 2094.73, 2137.88, 2203.29, 
   2277.91, 2352.87, 2434.07, 2512.04, 2597.19, 2709.87, 2841.42, 2936.26, 
   3005.13, 3084.00, 3156.24, 3234.31, 3317.40, 3446.91, 3594.78, 3685.29, 
   3803.15
   
   Saturation:
   0.45, 0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          16.5058        start      
        1            56         0.00104       1.21858     
        2            57       1.443e-006      0.00118     
        3            58       2.389e-010     9.359e-007   
        4            59       1.127e-011     1.170e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1941.82, 2041.24, 2119.90, 2183.63, 2265.00, 2330.52, 2370.05, 2429.85, 
   2497.96, 2566.24, 2640.05, 2710.79, 2787.91, 2889.77, 3008.52, 3094.01, 
   3156.02, 3226.97, 3291.89, 3362.00, 3436.56, 3552.76, 3685.41, 3766.62, 
   3872.45
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.02327        start      
        1            56       9.212e-004      2.05330     
        2            57       1.753e-006      0.00250     
        3            58       7.586e-010     2.732e-006   
        4            59       8.599e-012     1.035e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1927.54, 2025.10, 2102.88, 2166.15, 2247.18, 2312.58, 2352.09, 2411.94, 
   2480.16, 2548.59, 2622.63, 2693.62, 2771.06, 2873.39, 2992.73, 3078.69, 
   3141.07, 3212.47, 3277.84, 3348.47, 3423.63, 3540.84, 3674.73, 3756.78, 
   3863.83
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.03878        start      
        1            56       6.230e-004      2.07866     
        2            57       1.125e-006      0.00239     
        3            58       4.253e-010     2.620e-006   
        4            59       9.155e-012     7.977e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1921.40, 2017.95, 2095.13, 2158.02, 2238.69, 2303.86, 2343.27, 2403.01, 
   2471.14, 2539.51, 2613.52, 2684.51, 2761.98, 2864.38, 2983.85, 3069.94, 
   3132.43, 3203.99, 3269.53, 3340.37, 3415.81, 3533.50, 3668.03, 3750.53, 
   3858.29
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.10735        start      
        1            56       6.925e-004      2.47017     
        2            57       1.476e-006      0.00340     
        3            58       5.843e-010     4.662e-006   
        4            59       7.302e-012     1.562e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1917.68, 2013.55, 2090.28, 2152.86, 2233.20, 2298.15, 2337.45, 2397.05, 
   2465.04, 2533.30, 2607.21, 2678.14, 2755.55, 2857.92, 2977.38, 3063.49, 
   3126.02, 3197.64, 3263.27, 3334.23, 3409.84, 3527.84, 3662.80, 3745.63, 
   3853.91
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.56299        start      
        1            56       6.526e-004      2.58301     
        2            57       1.418e-006      0.00358     
        3            58       5.200e-010     5.170e-006   
        4            59       7.331e-012     1.601e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1915.01, 2010.35, 2086.73, 2149.04, 2229.09, 2293.84, 2333.03, 2392.48, 
   2460.32, 2528.46, 2602.26, 2673.09, 2750.43, 2852.72, 2972.12, 3058.21, 
   3120.74, 3192.40, 3258.07, 3329.11, 3404.84, 3523.07, 3658.37, 3741.46, 
   3850.17
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.14100        start      
        1            56       6.715e-004      2.78134     
        2            57       1.535e-006      0.00402     
        3            58       5.602e-010     6.265e-006   
        4            59       7.361e-012     1.969e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1912.90, 2007.81, 2083.88, 2145.97, 2225.76, 2290.32, 2329.40, 2388.71, 
   2456.42, 2524.43, 2598.11, 2668.84, 2746.08, 2848.28, 2967.60, 3053.66, 
   3116.18, 3187.85, 3253.55, 3324.65, 3400.46, 3518.88, 3654.46, 3737.77, 
   3846.85
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.81134        start      
        1            56       7.426e-004      3.07001     
        2            57       1.832e-006      0.00477     
        3            58       7.161e-010     8.165e-006   
        4            59       9.122e-012     2.848e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1911.14, 2005.68, 2081.48, 2143.37, 2222.92, 2287.31, 2326.30, 2385.47, 
   2453.04, 2520.93, 2594.49, 2665.12, 2742.27, 2844.37, 2963.61, 3049.62, 
   3112.13, 3183.79, 3249.51, 3320.65, 3396.54, 3515.11, 3650.94, 3734.44, 
   3843.86
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.57434        start      
        1            56       5.764e-004      2.80334     
        2            57       1.254e-006      0.00373     
        3            58       4.099e-010     5.673e-006   
        4            59       7.555e-012     1.569e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1909.61, 2003.84, 2079.39, 2141.09, 2220.44, 2284.66, 2323.56, 2382.61, 
   2450.05, 2517.82, 2591.26, 2661.80, 2738.86, 2840.86, 2960.00, 3045.97, 
   3108.45, 3180.11, 3245.84, 3317.02, 3392.97, 3511.69, 3647.73, 3731.40, 
   3841.12
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.37472        start      
        1            56       7.129e-004      3.21777     
        2            57       1.753e-006      0.00486     
        3            58       6.850e-010     8.441e-006   
        4            59       1.019e-011     2.945e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1908.26, 2002.19, 2077.53, 2139.06, 2218.21, 2282.28, 2321.09, 2380.03, 
   2447.34, 2515.00, 2588.33, 2658.78, 2735.75, 2837.65, 2956.70, 3042.62, 
   3105.09, 3176.74, 3242.47, 3313.68, 3389.69, 3508.53, 3644.76, 3728.60, 
   3838.59
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.20362        start      
        1            56       5.826e-004      2.99155     
        2            57       1.294e-006      0.00399     
        3            58       4.474e-010     6.278e-006   
        4            59       7.747e-012     1.871e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1907.03, 2000.70, 2075.84, 2137.22, 2216.18, 2280.11, 2318.85, 2377.67, 
   2444.87, 2512.41, 2585.64, 2656.00, 2732.89, 2834.70, 2953.66, 3039.53, 
   3101.98, 3173.62, 3239.36, 3310.59, 3386.65, 3505.60, 3642.01, 3726.00, 
   3836.24
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/28/2026 7:52:47 AM
   7/28/2026 7:54:23 AM
   ======================================================================
              Starting simulation for Rate = 500 STB/day
   
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
   
   
   
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          554.909        start      
        1            56         0.00476       0.27972     
        2            57       1.108e-005     1.607e-004   
        3            58       3.606e-009     2.961e-007   
        4            59       2.477e-010     1.334e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1603.85, 1627.88, 1647.26, 1663.17, 1683.75, 1700.52, 1710.74, 1726.36, 
   1744.30, 1762.45, 1782.26, 1801.43, 1822.53, 1850.67, 1883.80, 1907.90, 
   1925.58, 1946.02, 1964.97, 1998.61, 2186.79, 2486.93, 2815.46, 3009.01, 
   3249.64
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.32, 0.66, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          452.417        start      
        1            56         0.00281       0.35699     
        2            57       5.639e-006     1.043e-004   
        3            58       2.006e-009     1.478e-007   
        4            59       2.003e-010     6.428e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1589.83, 1610.60, 1627.37, 1641.12, 1658.93, 1673.44, 1682.28, 1695.79, 
   1711.32, 1727.02, 1744.17, 1760.75, 1779.01, 1803.37, 1832.09, 1862.76, 
   1986.22, 2138.29, 2273.89, 2417.07, 2566.80, 2797.14, 3056.10, 3211.81, 
   3409.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 
   0.64, 0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          443.058        start      
        1            56         0.00598       0.36666     
        2            57       1.220e-005     2.654e-004   
        3            58       1.930e-009     5.675e-007   
        4            59       1.930e-010     8.500e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1576.75, 1594.50, 1608.82, 1620.57, 1635.79, 1648.18, 1655.73, 1667.27, 
   1680.53, 1693.95, 1708.60, 1723.00, 1768.38, 1950.20, 2157.66, 2303.73, 
   2408.22, 2526.72, 2634.36, 2749.67, 2871.35, 3059.59, 3272.49, 3401.34, 
   3566.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.43, 0.69, 0.74, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          405.498        start      
        1            56         0.00530       0.28946     
        2            57       1.324e-005     1.936e-004   
        3            58       1.101e-009     4.978e-007   
        4            59       1.252e-010     4.411e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1570.83, 1587.22, 1600.43, 1611.28, 1625.32, 1636.76, 1643.74, 1654.39, 
   1666.75, 1695.32, 1812.75, 1924.22, 2042.90, 2197.59, 2376.45, 2504.47, 
   2596.86, 2702.14, 2798.11, 2901.26, 3010.44, 3179.74, 3371.78, 3488.43, 
   3638.72
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.39, 0.68, 0.73, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          334.263        start      
        1            56         0.00249       0.30708     
        2            57       6.633e-006     7.477e-005   
        3            58       8.080e-009     1.018e-007   
        4            59       1.309e-010     1.459e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1566.48, 1581.85, 1594.26, 1604.44, 1617.62, 1628.37, 1638.13, 1718.73, 
   1817.86, 1914.99, 2018.25, 2116.40, 2222.80, 2362.78, 2525.48, 2642.33, 
   2726.89, 2823.43, 2911.59, 3006.53, 3107.21, 3263.62, 3441.43, 3549.72, 
   3689.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.64, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          353.178        start      
        1            56         0.00768       0.65162     
        2            57       1.700e-005     4.159e-004   
        3            58       2.622e-009     1.175e-006   
        4            59       1.230e-010     1.317e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1563.39, 1578.04, 1589.86, 1599.99, 1651.53, 1740.99, 1793.78, 1872.38, 
   1961.18, 2049.78, 2145.21, 2236.49, 2335.85, 2466.96, 2619.66, 2729.50, 
   2809.11, 2900.11, 2983.30, 3073.02, 3168.30, 3316.51, 3485.28, 3588.27, 
   3721.81
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          294.923        start      
        1            56         0.00284       0.58214     
        2            57       7.913e-006     1.465e-004   
        3            58       1.488e-008     3.707e-007   
        4            59       1.365e-010     8.876e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1559.87, 1579.77, 1668.86, 1748.39, 1847.70, 1926.78, 1974.21, 2045.77, 
   2127.14, 2208.65, 2296.72, 2381.11, 2473.11, 2594.64, 2736.32, 2838.34, 
   2912.33, 2996.99, 3074.45, 3158.07, 3246.97, 3385.41, 3543.27, 3639.75, 
   3765.13
   
   Saturation:
   0.20, 0.29, 0.64, 0.72, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          86.5405        start      
        1            56         0.00231       1.90545     
        2            57       1.804e-006      0.00170     
        3            58       7.974e-010     3.334e-007   
        4            59       4.044e-011     2.519e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1938.24, 2038.52, 2116.97, 2180.30, 2260.96, 2325.88, 2365.06, 2424.38, 
   2492.02, 2559.90, 2633.38, 2703.90, 2780.88, 2882.71, 3001.58, 3087.26, 
   3149.49, 3220.75, 3286.03, 3356.60, 3431.72, 3548.86, 3682.66, 3764.57, 
   3871.25
   
   Saturation:
   0.67, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          10.3382        start      
        1            56       9.618e-004      1.61640     
        2            57       1.545e-006      0.00164     
        3            58       4.905e-010     1.456e-006   
        4            59       8.481e-012     3.868e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1933.61, 2031.96, 2110.12, 2173.57, 2254.74, 2320.18, 2359.69, 2419.53, 
   2487.71, 2556.11, 2630.09, 2701.03, 2778.41, 2880.67, 2999.92, 3085.83, 
   3148.16, 3219.51, 3284.82, 3355.39, 3430.48, 3547.54, 3681.22, 3763.08, 
   3869.75
   
   Saturation:
   0.75, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.98614        start      
        1            56       7.874e-004      2.05907     
        2            57       1.489e-006      0.00252     
        3            58       6.274e-010     2.826e-006   
        4            59       7.013e-012     1.018e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1924.01, 2020.98, 2098.41, 2161.44, 2242.26, 2307.52, 2346.98, 2406.77, 
   2474.96, 2543.40, 2617.47, 2688.53, 2766.06, 2868.56, 2988.15, 3074.32, 
   3136.87, 3208.49, 3274.09, 3344.99, 3420.49, 3538.23, 3672.77, 3755.23, 
   3862.80
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.55778        start      
        1            56       6.570e-004      2.21860     
        2            57       1.295e-006      0.00285     
        3            58       4.991e-010     3.536e-006   
        4            59       7.793e-012     1.126e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1919.21, 2015.35, 2092.26, 2154.96, 2235.44, 2300.49, 2339.84, 2399.52, 
   2467.60, 2535.96, 2609.97, 2680.99, 2758.51, 2861.03, 2980.67, 3066.90, 
   3129.52, 3201.24, 3266.96, 3338.01, 3413.70, 3531.81, 3666.85, 3749.67, 
   3857.81
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.85055        start      
        1            56       5.461e-004      2.22015     
        2            57       1.042e-006      0.00272     
        3            58       3.400e-010     3.394e-006   
        4            59       7.942e-012     8.812e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1916.07, 2011.62, 2088.14, 2150.57, 2230.75, 2295.60, 2334.84, 2394.38, 
   2462.33, 2530.57, 2604.48, 2675.42, 2752.88, 2855.33, 2974.93, 3061.16, 
   3123.79, 3195.55, 3261.32, 3332.47, 3408.28, 3526.64, 3662.03, 3745.12, 
   3853.71
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.35047        start      
        1            56       5.257e-004      2.33577     
        2            57       1.029e-006      0.00291     
        3            58       3.118e-010     3.848e-006   
        4            59       8.653e-012     9.395e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1913.74, 2008.82, 2085.02, 2147.20, 2227.12, 2291.78, 2330.92, 2390.32, 
   2458.13, 2526.25, 2600.04, 2670.89, 2748.26, 2850.62, 2970.14, 3056.34, 
   3118.97, 3190.74, 3256.54, 3327.74, 3403.65, 3522.18, 3657.85, 3741.17, 
   3850.14
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.99870        start      
        1            56       5.729e-004      2.56614     
        2            57       1.210e-006      0.00343     
        3            58       3.783e-010     5.025e-006   
        4            59       8.413e-012     1.313e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1911.85, 2006.55, 2082.47, 2144.44, 2224.12, 2288.60, 2327.65, 2386.92, 
   2454.59, 2522.58, 2596.26, 2667.01, 2744.28, 2846.55, 2965.98, 3052.13, 
   3114.73, 3186.51, 3252.32, 3323.56, 3399.54, 3518.23, 3654.13, 3737.64, 
   3846.95
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.72495        start      
        1            56       6.670e-004      2.88523     
        2            57       1.555e-006      0.00426     
        3            58       5.400e-010     7.019e-006   
        4            59       7.027e-012     2.134e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1910.25, 2004.61, 2080.28, 2142.08, 2221.53, 2285.85, 2324.81, 2383.95, 
   2451.49, 2519.36, 2592.92, 2663.57, 2740.76, 2842.92, 2962.25, 3048.35, 
   3110.93, 3182.70, 3248.52, 3319.79, 3395.82, 3514.64, 3650.75, 3734.43, 
   3844.04
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.50743        start      
        1            56       5.265e-004      2.65184     
        2            57       1.090e-006      0.00339     
        3            58       3.209e-010     4.984e-006   
        4            59       7.448e-012     1.221e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1908.85, 2002.92, 2078.36, 2139.98, 2219.24, 2283.40, 2322.28, 2381.30, 
   2448.72, 2516.47, 2589.92, 2660.48, 2737.57, 2839.63, 2958.86, 3044.91, 
   3107.47, 3179.22, 3245.04, 3316.34, 3392.42, 3511.35, 3647.65, 3731.48, 
   3841.36
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.30531        start      
        1            56       6.475e-004      3.03955     
        2            57       1.509e-006      0.00438     
        3            58       5.287e-010     7.344e-006   
        4            59       9.434e-012     2.258e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1907.60, 2001.39, 2076.64, 2138.10, 2217.17, 2281.19, 2319.99, 2378.90, 
   2446.19, 2513.84, 2587.18, 2657.64, 2734.65, 2836.60, 2955.75, 3041.74, 
   3104.27, 3176.01, 3241.83, 3313.15, 3389.27, 3508.31, 3644.77, 3728.74, 
   3838.88
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.14718        start      
        1            56       5.358e-004      2.84027     
        2            57       1.134e-006      0.00364     
        3            58       3.555e-010     5.552e-006   
        4            59       6.367e-012     1.475e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1906.45, 2000.00, 2075.06, 2136.38, 2215.27, 2279.16, 2317.88, 2376.68, 
   2443.87, 2511.41, 2584.65, 2655.02, 2731.94, 2833.81, 2952.86, 3038.80, 
   3101.31, 3173.03, 3238.85, 3310.18, 3386.34, 3505.47, 3642.09, 3726.20, 
   3836.56
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.00001        start      
        1            56       6.893e-004      3.30701     
        2            57       1.670e-006      0.00489     
        3            58       6.559e-010     8.571e-006   
        4            59       6.730e-012     3.008e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1905.40, 1998.72, 2073.60, 2134.79, 2213.52, 2277.28, 2315.93, 2374.63, 
   2441.71, 2509.15, 2582.29, 2652.59, 2729.43, 2831.20, 2950.16, 3036.05, 
   3098.54, 3170.24, 3236.06, 3307.41, 3383.61, 3502.82, 3639.59, 3723.81, 
   3834.40
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.88477        start      
        1            56       5.920e-004      3.13066     
        2            57       1.327e-006      0.00421     
        3            58       4.817e-010     6.828e-006   
        4            59       7.248e-012     2.156e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1904.43, 1997.53, 2072.25, 2133.31, 2211.88, 2275.53, 2314.11, 2372.72, 
   2439.70, 2507.05, 2580.09, 2650.30, 2727.07, 2828.75, 2947.63, 3033.48, 
   3095.94, 3167.63, 3233.44, 3304.80, 3381.03, 3500.33, 3637.23, 3721.57, 
   3832.36
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/28/2026 7:55:34 AM
   7/28/2026 7:57:06 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

