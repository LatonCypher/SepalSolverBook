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
        0            1          194.931        start      
        1            56         0.00250       4.63764     
        2            57       2.819e-006     3.984e-004   
        3            58       8.765e-009     2.111e-007   
        4            59       7.783e-011     8.875e-010   
   Producer BHP: 
   2211.69 psi
   
   Injector BHP: 
   2656.90 psi
   
   Pressure: 
   2258.22, 2269.53, 2278.60, 2286.27, 2295.45, 2307.91, 2320.39, 2333.44, 
   2346.81, 2354.17, 2359.69, 2367.01, 2375.03, 2381.96, 2391.48, 2400.70, 
   2407.96, 2415.34, 2425.33, 2434.54, 2439.03, 2444.71, 2452.04, 2480.99, 
   2547.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.45, 
   0.71
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.517        start      
        1            56         0.00267       6.22112     
        2            57       5.081e-006     7.579e-004   
        3            58       8.647e-009     7.208e-007   
        4            59       5.712e-011     9.949e-010   
   Producer BHP: 
   1535.36 psi
   
   Injector BHP: 
   2060.17 psi
   
   Pressure: 
   1581.99, 1593.33, 1602.40, 1610.07, 1619.25, 1631.69, 1644.13, 1657.14, 
   1670.46, 1677.79, 1683.27, 1690.55, 1698.51, 1705.38, 1714.81, 1723.94, 
   1731.12, 1738.41, 1748.26, 1757.35, 1761.76, 1768.32, 1813.53, 1890.75, 
   1950.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.73, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.433        start      
        1            56         0.00394       0.16089     
        2            57       2.211e-005     2.208e-004   
        3            58       6.131e-008     5.423e-007   
        4            59       1.517e-010     2.156e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2021.80 psi
   
   Pressure: 
   1533.68, 1541.96, 1548.67, 1554.42, 1561.37, 1570.91, 1580.58, 1590.80, 
   1601.40, 1607.31, 1611.79, 1617.82, 1624.51, 1630.37, 1638.51, 1646.51, 
   1652.90, 1659.49, 1668.54, 1677.02, 1683.65, 1728.14, 1786.33, 1856.47, 
   1911.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.31, 0.66, 0.74, 0.77, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.017        start      
        1            56         0.00669       0.33634     
        2            57       2.787e-005     4.894e-004   
        3            58       1.590e-008     2.349e-006   
        4            59       2.620e-011     8.435e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2052.71 psi
   
   Pressure: 
   1533.63, 1541.90, 1548.60, 1554.33, 1561.28, 1570.80, 1580.45, 1590.66, 
   1601.24, 1607.14, 1611.62, 1617.64, 1624.32, 1630.16, 1638.28, 1646.27, 
   1652.65, 1659.22, 1668.34, 1687.95, 1723.46, 1768.28, 1822.76, 1889.46, 
   1942.58
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.39, 0.68, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.324        start      
        1            56         0.00898       0.71786     
        2            57       3.277e-005     8.735e-004   
        3            58       8.716e-009     4.107e-006   
        4            59       3.910e-011     7.469e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2124.47 psi
   
   Pressure: 
   1533.51, 1541.75, 1548.43, 1554.14, 1561.06, 1570.55, 1580.17, 1590.33, 
   1600.88, 1606.75, 1611.21, 1617.21, 1623.86, 1629.68, 1637.78, 1645.73, 
   1652.08, 1658.93, 1695.44, 1768.82, 1803.53, 1845.98, 1898.15, 1962.61, 
   2014.36
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.50, 0.70, 0.74, 0.76, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.238        start      
        1            56         0.00433       0.58890     
        2            57       2.733e-006     9.970e-005   
        3            58       4.401e-009     7.411e-008   
        4            59       3.968e-011     1.371e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2192.58 psi
   
   Pressure: 
   1533.52, 1541.76, 1548.43, 1554.15, 1561.07, 1570.56, 1580.17, 1590.34, 
   1600.89, 1606.77, 1611.23, 1617.23, 1623.88, 1629.71, 1637.80, 1645.77, 
   1653.14, 1696.04, 1774.00, 1843.86, 1877.01, 1918.03, 1968.72, 2031.68, 
   2082.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.24, 0.59, 0.72, 0.75, 0.76, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.712        start      
        1            56         0.00274       0.48865     
        2            57       9.735e-006     1.522e-004   
        3            58       9.840e-009     3.663e-007   
        4            59       3.233e-011     3.987e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2236.67 psi
   
   Pressure: 
   1533.53, 1541.78, 1548.45, 1554.17, 1561.09, 1570.58, 1580.20, 1590.38, 
   1600.92, 1606.81, 1611.27, 1617.27, 1623.93, 1629.75, 1637.86, 1648.57, 
   1695.33, 1751.50, 1825.68, 1892.86, 1925.00, 1965.02, 2014.64, 2076.48, 
   2126.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 
   0.62, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.624        start      
        1            56         0.00408       0.42959     
        2            57       1.927e-005     2.424e-004   
        3            58       2.290e-008     9.990e-007   
        4            59       4.475e-011     1.667e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2295.32 psi
   
   Pressure: 
   1533.54, 1541.79, 1548.46, 1554.18, 1561.10, 1570.60, 1580.22, 1590.39, 
   1600.94, 1606.83, 1611.29, 1617.29, 1623.95, 1629.80, 1643.57, 1710.00, 
   1764.53, 1818.26, 1889.98, 1955.35, 1986.75, 2025.99, 2074.75, 2135.71, 
   2185.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.524        start      
        1            56         0.00631       0.34635     
        2            57       3.203e-005     3.099e-004   
        3            58       7.286e-009     1.810e-006   
        4            59       4.287e-011     3.234e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2350.81 psi
   
   Pressure: 
   1533.58, 1541.83, 1548.51, 1554.24, 1561.17, 1570.67, 1580.30, 1590.48, 
   1601.04, 1606.93, 1611.40, 1617.40, 1624.15, 1637.82, 1707.64, 1775.42, 
   1827.65, 1879.84, 1949.87, 2013.90, 2044.73, 2083.35, 2131.44, 2191.68, 
   2240.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.866        start      
        1            56         0.00868       0.62219     
        2            57       3.574e-005     6.597e-004   
        3            58       6.083e-009     3.557e-006   
        4            59       5.130e-011     3.881e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2395.28 psi
   
   Pressure: 
   1533.45, 1541.68, 1548.34, 1554.04, 1560.94, 1570.41, 1580.00, 1590.15, 
   1600.66, 1606.53, 1610.98, 1617.23, 1644.02, 1694.58, 1763.00, 1828.02, 
   1878.65, 1929.60, 1998.19, 2061.05, 2091.38, 2129.44, 2176.93, 2236.54, 
   2285.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.779        start      
        1            56         0.00451       0.67083     
        2            57       1.801e-006     1.223e-004   
        3            58       4.311e-009     2.622e-007   
        4            59       4.325e-011     4.139e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2445.81 psi
   
   Pressure: 
   1533.41, 1541.62, 1548.27, 1553.96, 1560.86, 1570.31, 1579.89, 1590.02, 
   1600.53, 1606.39, 1611.52, 1649.14, 1706.70, 1755.47, 1821.44, 1884.77, 
   1934.31, 1984.36, 2051.85, 2113.79, 2143.72, 2181.33, 2228.32, 2287.42, 
   2335.82
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 0.76, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.375        start      
        1            56         0.00292       0.43033     
        2            57       1.188e-005     1.324e-004   
        3            58       1.450e-008     3.305e-007   
        4            59       5.531e-011     4.827e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2479.84 psi
   
   Pressure: 
   1533.51, 1541.74, 1548.41, 1554.12, 1561.04, 1570.52, 1580.13, 1590.29, 
   1600.84, 1608.99, 1641.98, 1693.61, 1749.04, 1796.41, 1860.99, 1923.27, 
   1972.10, 2021.52, 2088.23, 2149.50, 2179.13, 2216.42, 2263.02, 2321.71, 
   2369.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.28, 0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.589        start      
        1            56         0.00497       0.55062     
        2            57       2.282e-005     3.520e-004   
        3            58       8.551e-009     1.737e-006   
        4            59       4.906e-011     3.479e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2524.05 psi
   
   Pressure: 
   1533.44, 1541.65, 1548.31, 1554.00, 1560.90, 1570.36, 1579.94, 1590.13, 
   1610.00, 1659.47, 1697.66, 1747.25, 1801.10, 1847.40, 1910.76, 1972.03, 
   2020.14, 2068.92, 2134.82, 2195.40, 2224.73, 2261.66, 2307.90, 2366.19, 
   2414.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.132        start      
        1            56         0.00770       0.81381     
        2            57       3.773e-005     7.076e-004   
        3            58       3.170e-009     3.887e-006   
        4            59       3.372e-011     1.927e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2602.11 psi
   
   Pressure: 
   1533.29, 1541.47, 1548.09, 1553.76, 1560.63, 1570.05, 1579.76, 1608.36, 
   1699.40, 1749.07, 1785.73, 1833.94, 1886.58, 1931.98, 1994.25, 2054.57, 
   2102.01, 2150.15, 2215.25, 2275.16, 2304.18, 2340.78, 2386.64, 2444.54, 
   2492.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.43, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.393        start      
        1            56         0.00768       1.20766     
        2            57       2.251e-005     7.084e-004   
        3            58       1.212e-008     3.227e-006   
        4            59       3.799e-011     1.097e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2684.24 psi
   
   Pressure: 
   1533.07, 1541.19, 1547.77, 1553.40, 1560.22, 1570.29, 1617.68, 1705.34, 
   1793.46, 1841.26, 1876.87, 1924.00, 1975.62, 2020.22, 2081.50, 2140.93, 
   2187.73, 2235.27, 2299.62, 2358.87, 2387.61, 2423.90, 2469.40, 2526.93, 
   2574.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.53, 0.70, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          144.745        start      
        1            56         0.00330       1.18562     
        2            57       8.936e-006     1.970e-004   
        3            58       1.533e-008     3.854e-007   
        4            59       4.156e-011     5.973e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2763.42 psi
   
   Pressure: 
   1533.06, 1541.18, 1547.75, 1553.39, 1562.08, 1629.01, 1711.16, 1795.46, 
   1880.88, 1927.66, 1962.64, 2009.08, 2060.03, 2104.08, 2164.65, 2223.45, 
   2269.76, 2316.85, 2380.62, 2439.38, 2467.89, 2503.91, 2549.13, 2606.34, 
   2653.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.658        start      
        1            56         0.00501       0.54789     
        2            57       2.635e-005     2.236e-004   
        3            58       4.426e-008     1.235e-006   
        4            59       5.247e-011     2.719e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2823.75 psi
   
   Pressure: 
   1533.40, 1541.61, 1548.28, 1558.51, 1616.32, 1697.64, 1777.46, 1860.24, 
   1944.70, 1991.12, 2025.90, 2072.11, 2122.83, 2166.69, 2227.01, 2285.56, 
   2331.68, 2378.56, 2442.05, 2500.55, 2528.94, 2564.81, 2609.84, 2666.84, 
   2713.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.550        start      
        1            56         0.00727       0.49635     
        2            57       3.579e-005     5.299e-004   
        3            58       2.489e-008     3.333e-006   
        4            59       4.428e-011     2.098e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2863.37 psi
   
   Pressure: 
   1533.42, 1541.77, 1560.17, 1609.48, 1668.21, 1746.71, 1824.74, 1906.12, 
   1989.43, 2035.33, 2069.77, 2115.56, 2165.86, 2209.37, 2269.24, 2327.38, 
   2373.19, 2419.78, 2482.88, 2541.05, 2569.28, 2604.98, 2649.82, 2706.61, 
   2753.55
   
   Saturation:
   0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          162.184        start      
        1            56         0.00762       1.04510     
        2            57       2.639e-005     7.810e-004   
        3            58       2.939e-009     4.100e-006   
        4            59       5.333e-011     7.283e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2913.47 psi
   
   Pressure: 
   1535.01, 1571.54, 1628.99, 1676.71, 1733.13, 1809.32, 1885.49, 1965.20, 
   2046.99, 2092.13, 2126.03, 2171.18, 2220.81, 2263.78, 2322.95, 2380.46, 
   2425.81, 2471.96, 2534.53, 2592.23, 2620.27, 2655.74, 2700.34, 2756.89, 
   2803.67
   
   Saturation:
   0.22, 0.52, 0.70, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          127.571        start      
        1            56         0.00399       4.20549     
        2            57       3.960e-006      0.00112     
        3            58       6.588e-009     1.628e-006   
        4            59       4.843e-011     2.556e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3052.02 psi
   
   Pressure: 
   1693.79, 1760.37, 1812.76, 1856.69, 1909.23, 1980.64, 2052.42, 2127.82, 
   2205.47, 2248.48, 2280.88, 2324.17, 2371.89, 2413.32, 2470.53, 2526.29, 
   2570.38, 2615.39, 2676.57, 2733.14, 2760.69, 2795.66, 2839.74, 2895.78, 
   2942.28
   
   Saturation:
   0.58, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/26/2026 12:02:48 PM
   7/26/2026 12:04:08 PM
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
        0            1          261.717        start      
        1            56         0.00334       0.20821     
        2            57       1.136e-005     1.751e-004   
        3            58       1.085e-008     4.504e-007   
        4            59       7.415e-011     6.814e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2169.87 psi
   
   Pressure: 
   1550.55, 1562.99, 1573.06, 1581.68, 1592.12, 1606.44, 1620.95, 1636.30, 
   1652.21, 1661.08, 1667.81, 1676.86, 1686.91, 1695.70, 1707.92, 1719.93, 
   1729.52, 1739.41, 1753.00, 1765.70, 1771.97, 1780.07, 1797.71, 1912.24, 
   2004.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.67, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.624        start      
        1            56         0.00350       0.19093     
        2            57       1.397e-005     1.562e-004   
        3            58       2.587e-008     3.115e-007   
        4            59       7.733e-011     7.571e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2281.95 psi
   
   Pressure: 
   1550.54, 1562.97, 1573.04, 1581.66, 1592.10, 1606.41, 1620.92, 1636.26, 
   1652.17, 1661.04, 1667.76, 1676.81, 1686.86, 1695.64, 1707.85, 1719.87, 
   1729.45, 1739.34, 1752.93, 1765.65, 1775.43, 1842.51, 1929.69, 2034.47, 
   2116.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.31, 0.66, 0.74, 0.77, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          223.478        start      
        1            56         0.00313       0.74156     
        2            57       6.586e-006     1.527e-004   
        3            58       6.582e-009     2.484e-007   
        4            59       7.717e-011     2.592e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2385.92 psi
   
   Pressure: 
   1550.17, 1562.52, 1572.51, 1581.07, 1591.43, 1605.64, 1620.04, 1635.27, 
   1651.06, 1659.87, 1666.54, 1675.53, 1685.50, 1694.22, 1706.35, 1718.28, 
   1727.79, 1737.62, 1754.96, 1846.83, 1900.39, 1965.38, 2044.84, 2142.62, 
   2220.87
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.26, 0.61, 0.72, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          226.680        start      
        1            56         0.00431       0.69160     
        2            57       1.301e-006     1.317e-004   
        3            58       2.126e-009     1.628e-007   
        4            59       6.036e-011     1.736e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2535.43 psi
   
   Pressure: 
   1550.17, 1562.52, 1572.51, 1581.06, 1591.43, 1605.64, 1620.03, 1635.26, 
   1651.05, 1659.85, 1666.52, 1675.51, 1685.47, 1694.19, 1706.31, 1718.24, 
   1729.13, 1792.56, 1909.30, 2013.79, 2063.35, 2124.67, 2200.42, 2294.50, 
   2370.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.24, 0.59, 0.72, 0.75, 0.76, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          255.392        start      
        1            56         0.00726       0.68276     
        2            57       2.078e-005     5.327e-004   
        3            58       5.490e-009     1.758e-006   
        4            59       7.782e-011     3.853e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2634.98 psi
   
   Pressure: 
   1550.15, 1562.49, 1572.48, 1581.02, 1591.38, 1605.58, 1619.96, 1635.18, 
   1650.95, 1659.74, 1666.41, 1675.38, 1685.33, 1694.03, 1706.56, 1751.26, 
   1834.12, 1916.06, 2024.86, 2123.76, 2171.19, 2230.37, 2303.84, 2395.58, 
   2470.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.48, 
   0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          253.474        start      
        1            56         0.00552       0.40783     
        2            57       2.100e-005     2.477e-004   
        3            58       3.629e-009     9.742e-007   
        4            59       8.130e-011     2.100e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2772.25 psi
   
   Pressure: 
   1550.27, 1562.63, 1572.65, 1581.21, 1591.59, 1605.83, 1620.25, 1635.51, 
   1651.32, 1660.14, 1666.83, 1675.83, 1685.91, 1705.48, 1810.09, 1911.60, 
   1989.77, 2067.83, 2172.55, 2268.28, 2314.38, 2372.10, 2443.97, 2534.01, 
   2607.42
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          242.022        start      
        1            56         0.00442       0.48493     
        2            57       1.696e-005     1.871e-004   
        3            58       1.218e-008     6.497e-007   
        4            59       7.067e-011     6.022e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2881.68 psi
   
   Pressure: 
   1550.15, 1562.49, 1572.48, 1581.02, 1591.38, 1605.58, 1619.97, 1635.19, 
   1650.97, 1659.77, 1666.47, 1682.40, 1766.33, 1841.22, 1941.77, 2037.83, 
   2112.78, 2188.34, 2290.11, 2383.41, 2428.46, 2485.03, 2555.63, 2644.33, 
   2716.92
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          221.676        start      
        1            56         0.00260       0.51606     
        2            57       6.597e-006     9.360e-005   
        3            58       5.693e-009     1.679e-007   
        4            59       5.450e-011     1.537e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2963.63 psi
   
   Pressure: 
   1550.11, 1562.43, 1572.41, 1580.95, 1591.29, 1605.48, 1619.85, 1635.05, 
   1650.83, 1662.52, 1711.04, 1788.36, 1871.27, 1942.09, 2038.61, 2131.68, 
   2204.64, 2278.48, 2378.14, 2469.67, 2513.93, 2569.63, 2639.27, 2726.96, 
   2798.92
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.27, 0.61, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          228.140        start      
        1            56         0.00493       1.44002     
        2            57       4.933e-006     3.519e-004   
        3            58       4.712e-009     6.319e-007   
        4            59       8.339e-011     3.498e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3075.30 psi
   
   Pressure: 
   1549.15, 1561.23, 1571.02, 1579.39, 1589.53, 1603.44, 1617.54, 1634.34, 
   1724.50, 1799.50, 1854.64, 1926.77, 2005.43, 2073.21, 2166.18, 2256.24, 
   2327.08, 2399.00, 2496.30, 2585.85, 2629.26, 2684.03, 2752.66, 2839.33, 
   2910.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 
   0.56, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          242.536        start      
        1            56         0.00778       1.21619     
        2            57       2.072e-005     6.665e-004   
        3            58       7.887e-009     2.238e-006   
        4            59       8.645e-011     6.474e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3260.12 psi
   
   Pressure: 
   1549.31, 1561.44, 1571.25, 1579.65, 1589.83, 1604.54, 1667.99, 1798.95, 
   1930.55, 2001.89, 2055.01, 2125.30, 2202.30, 2268.82, 2360.21, 2448.86, 
   2518.66, 2589.58, 2685.57, 2773.98, 2816.86, 2871.00, 2938.92, 3024.80, 
   3095.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.51, 0.70, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          254.943        start      
        1            56         0.00792       0.76914     
        2            57       2.965e-005     5.288e-004   
        3            58       1.501e-009     2.314e-006   
        4            59       8.288e-011     1.069e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3426.46 psi
   
   Pressure: 
   1549.73, 1561.96, 1571.85, 1580.54, 1614.55, 1737.63, 1858.85, 1983.78, 
   2110.79, 2180.46, 2232.61, 2301.84, 2377.80, 2443.47, 2533.76, 2621.38, 
   2690.39, 2760.54, 2855.52, 2943.02, 2985.48, 3039.12, 3106.46, 3191.69, 
   3262.05
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.46, 0.69, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          256.097        start      
        1            56         0.00594       0.55086     
        2            57       2.230e-005     3.488e-004   
        3            58       1.254e-008     1.631e-006   
        4            59       7.640e-011     7.923e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3531.18 psi
   
   Pressure: 
   1549.93, 1562.33, 1586.01, 1659.61, 1747.59, 1864.97, 1981.53, 2103.03, 
   2227.36, 2295.84, 2347.20, 2415.50, 2490.50, 2555.39, 2644.66, 2731.34, 
   2799.63, 2869.09, 2963.17, 3049.89, 3091.99, 3145.22, 3212.09, 3296.80, 
   3366.84
   
   Saturation:
   0.20, 0.20, 0.40, 0.68, 0.73, 0.75, 0.75, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          231.433        start      
        1            56         0.00382       1.64237     
        2            57       9.680e-006     8.142e-004   
        3            58       9.585e-009     2.543e-006   
        4            59       8.650e-011     2.451e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3658.70 psi
   
   Pressure: 
   1581.78, 1681.76, 1765.18, 1834.38, 1916.62, 2027.93, 2139.42, 2256.22, 
   2376.18, 2442.46, 2492.28, 2558.69, 2631.75, 2695.06, 2782.30, 2867.16, 
   2934.13, 3002.36, 3094.91, 3180.35, 3221.90, 3274.52, 3340.74, 3424.81, 
   3494.44
   
   Saturation:
   0.33, 0.66, 0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          29.2095        start      
        1            56         0.00110       1.31767     
        2            57       1.071e-006     9.494e-004   
        3            58       1.599e-010     2.657e-007   
        4            59       2.146e-011     1.142e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4009.19 psi
   
   Pressure: 
   1936.49, 2039.76, 2121.42, 2190.10, 2272.19, 2383.55, 2495.18, 2612.12, 
   2732.19, 2798.49, 2848.30, 2914.63, 2987.55, 3050.68, 3137.62, 3222.11, 
   3288.74, 3356.58, 3448.55, 3533.40, 3574.65, 3626.88, 3692.59, 3776.02, 
   3845.16
   
   Saturation:
   0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.88269        start      
        1            56       9.367e-004      4.86406     
        2            57       1.606e-006      0.00241     
        3            58       7.661e-010     9.156e-007   
        4            59       8.207e-012     9.843e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3985.15 psi
   
   Pressure: 
   1917.97, 2018.90, 2099.52, 2167.71, 2249.46, 2360.53, 2472.00, 2588.85, 
   2708.88, 2775.18, 2824.99, 2891.32, 2964.23, 3027.36, 3114.27, 3198.72, 
   3265.31, 3333.08, 3424.96, 3509.71, 3550.90, 3603.06, 3668.68, 3752.01, 
   3821.10
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.08081        start      
        1            56       6.442e-004      6.17242     
        2            57       1.156e-006      0.00322     
        3            58       7.215e-010     1.225e-006   
        4            59       6.833e-012     1.496e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3956.12 psi
   
   Pressure: 
   1906.39, 2005.19, 2084.38, 2151.53, 2232.16, 2341.83, 2451.99, 2567.56, 
   2686.35, 2752.00, 2801.36, 2867.12, 2939.43, 3002.07, 3088.34, 3172.22, 
   3238.38, 3305.76, 3397.14, 3481.48, 3522.50, 3574.46, 3639.89, 3723.04, 
   3792.05
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.94132        start      
        1            56       5.116e-004      7.25590     
        2            57       1.002e-006      0.00419     
        3            58       6.903e-010     1.847e-006   
        4            59       6.795e-012     2.043e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3935.15 psi
   
   Pressure: 
   1899.44, 1996.80, 2074.97, 2141.34, 2221.09, 2329.64, 2438.75, 2553.27, 
   2671.04, 2736.16, 2785.13, 2850.40, 2922.20, 2984.42, 3070.15, 3153.52, 
   3219.31, 3286.34, 3377.29, 3461.26, 3502.12, 3553.92, 3619.16, 3702.15, 
   3771.07
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.37936        start      
        1            56       6.177e-004      9.51739     
        2            57       1.585e-006      0.00721     
        3            58       1.416e-009     4.319e-006   
        4            59       7.740e-012     6.337e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3918.06 psi
   
   Pressure: 
   1894.38, 1990.63, 2067.99, 2133.71, 2212.73, 2320.34, 2428.55, 2542.16, 
   2659.03, 2723.68, 2772.31, 2837.15, 2908.50, 2970.34, 3055.57, 3138.50, 
   3203.96, 3270.67, 3361.22, 3444.86, 3485.58, 3537.21, 3602.30, 3685.12, 
   3753.97
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.92959        start      
        1            56       5.257e-004      10.1878     
        2            57       1.452e-006      0.00771     
        3            58       1.267e-009     5.164e-006   
        4            59       7.806e-012     6.684e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3903.33 psi
   
   Pressure: 
   1890.33, 1985.66, 2062.33, 2127.49, 2205.87, 2312.65, 2420.06, 2532.86, 
   2648.94, 2713.16, 2761.48, 2825.94, 2896.87, 2958.38, 3043.16, 3125.67, 
   3190.82, 3257.25, 3347.44, 3430.77, 3471.35, 3522.84, 3587.77, 3670.45, 
   3739.22
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.64310        start      
        1            56       5.364e-004      11.3918     
        2            57       1.681e-006      0.00926     
        3            58       1.600e-009     6.997e-006   
        4            59       6.724e-012     1.016e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3890.26 psi
   
   Pressure: 
   1886.89, 1981.44, 2057.50, 2122.17, 2199.99, 2306.03, 2412.72, 2524.79, 
   2640.14, 2703.98, 2752.02, 2816.12, 2886.68, 2947.87, 3032.24, 3114.38, 
   3179.25, 3245.41, 3335.26, 3418.31, 3458.76, 3510.12, 3574.91, 3657.45, 
   3726.15
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 12:04:53 PM
   7/26/2026 12:06:15 PM
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
        0            1          324.056        start      
        1            56         0.00360       0.48100     
        2            57       1.120e-006     1.487e-004   
        3            58       1.376e-009     1.379e-007   
        4            59       1.217e-010     7.816e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2450.67 psi
   
   Pressure: 
   1567.30, 1583.86, 1597.27, 1608.75, 1622.65, 1641.71, 1661.03, 1681.46, 
   1702.64, 1714.45, 1723.40, 1735.45, 1748.82, 1760.51, 1776.77, 1792.76, 
   1805.51, 1818.68, 1836.75, 1853.64, 1861.98, 1874.40, 1962.11, 2113.80, 
   2230.63
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.73, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          344.652        start      
        1            56         0.00595       0.40817     
        2            57       1.535e-005     3.266e-004   
        3            58       2.135e-009     8.301e-007   
        4            59       8.830e-011     1.102e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2602.72 psi
   
   Pressure: 
   1567.28, 1583.84, 1597.24, 1608.72, 1622.61, 1641.67, 1660.98, 1681.40, 
   1702.57, 1714.37, 1723.32, 1735.36, 1748.73, 1760.41, 1776.66, 1792.64, 
   1805.39, 1818.54, 1836.75, 1874.65, 1946.15, 2035.64, 2144.21, 2277.01, 
   2382.77
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.38, 0.68, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          303.437        start      
        1            56         0.00462       0.78684     
        2            57       1.853e-006     1.636e-004   
        3            58       2.550e-009     2.042e-007   
        4            59       8.690e-011     1.514e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2876.90 psi
   
   Pressure: 
   1566.80, 1583.23, 1596.54, 1607.93, 1621.72, 1640.64, 1659.81, 1680.08, 
   1701.09, 1712.81, 1721.69, 1733.65, 1746.91, 1758.51, 1774.65, 1790.52, 
   1804.90, 1888.35, 2043.70, 2182.67, 2248.59, 2330.15, 2430.92, 2556.10, 
   2657.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.23, 0.58, 0.72, 0.75, 0.76, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          315.266        start      
        1            56         0.00338       0.64482     
        2            57       9.322e-006     1.697e-004   
        3            58       4.581e-009     3.640e-007   
        4            59       1.420e-010     2.198e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3080.03 psi
   
   Pressure: 
   1566.67, 1583.07, 1596.36, 1607.72, 1621.49, 1640.38, 1659.52, 1679.76, 
   1700.75, 1712.46, 1721.33, 1733.28, 1746.54, 1758.16, 1784.07, 1915.72, 
   2024.29, 2131.13, 2273.64, 2403.50, 2465.90, 2543.86, 2640.76, 2761.92, 
   2860.47
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.65, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          337.733        start      
        1            56         0.00810       0.68041     
        2            57       2.159e-005     4.697e-004   
        3            58       5.025e-009     1.409e-006   
        4            59       1.376e-010     2.860e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3276.58 psi
   
   Pressure: 
   1566.69, 1583.09, 1596.38, 1607.74, 1621.51, 1640.39, 1659.52, 1679.74, 
   1700.71, 1712.40, 1721.26, 1733.55, 1781.52, 1882.50, 2018.85, 2148.28, 
   2248.98, 2350.30, 2486.65, 2611.57, 2671.85, 2747.49, 2841.86, 2960.33, 
   3057.20
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.48, 0.69, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          295.268        start      
        1            56         0.00267       0.59121     
        2            57       4.271e-006     7.956e-005   
        3            58       3.892e-009     9.950e-008   
        4            59       1.068e-010     9.208e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3443.98 psi
   
   Pressure: 
   1566.64, 1583.03, 1596.30, 1607.66, 1621.42, 1640.29, 1659.40, 1679.62, 
   1700.60, 1715.65, 1778.75, 1881.67, 1991.93, 2086.08, 2214.37, 2338.04, 
   2434.99, 2533.11, 2665.53, 2787.14, 2845.96, 2919.97, 3012.52, 3129.08, 
   3224.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          333.958        start      
        1            56         0.00618       1.04342     
        2            57       1.820e-005     4.126e-004   
        3            58       3.817e-009     1.178e-006   
        4            59       9.469e-011     2.456e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3672.36 psi
   
   Pressure: 
   1565.79, 1581.97, 1595.07, 1606.28, 1619.87, 1638.49, 1657.56, 1704.97, 
   1884.38, 1982.82, 2055.36, 2150.67, 2254.73, 2344.45, 2467.54, 2586.77, 
   2680.57, 2775.78, 2904.58, 3023.12, 3080.59, 3153.09, 3243.97, 3358.77, 
   3453.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          296.067        start      
        1            56         0.00473       1.45522     
        2            57       3.123e-006     2.672e-004   
        3            58       2.805e-009     3.740e-007   
        4            59       1.166e-010     1.833e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3975.89 psi
   
   Pressure: 
   1565.00, 1580.99, 1593.93, 1605.01, 1620.45, 1734.60, 1897.00, 2063.33, 
   2231.64, 2323.77, 2392.68, 2484.17, 2584.56, 2671.40, 2790.84, 2906.83, 
   2998.23, 3091.20, 3217.15, 3333.23, 3389.59, 3460.83, 3550.30, 3663.57, 
   3757.11
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          333.794        start      
        1            56         0.00515       0.65119     
        2            57       1.589e-005     2.508e-004   
        3            58       6.605e-009     9.382e-007   
        4            59       1.174e-010     3.131e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4189.62 psi
   
   Pressure: 
   1566.27, 1582.66, 1610.19, 1707.33, 1824.34, 1980.23, 2134.87, 2296.00, 
   2460.80, 2551.55, 2619.61, 2710.11, 2809.47, 2895.45, 3013.71, 3128.56, 
   3219.05, 3311.10, 3435.79, 3550.71, 3606.53, 3677.10, 3765.76, 3878.11, 
   3971.02
   
   Saturation:
   0.20, 0.20, 0.37, 0.67, 0.73, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          299.277        start      
        1            56         0.00695       3.73279     
        2            57       8.096e-006      0.00109     
        3            58       2.340e-009     2.483e-006   
        4            59       1.130e-010     7.740e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4452.78 psi
   
   Pressure: 
   1776.60, 1907.01, 2009.67, 2095.71, 2198.65, 2338.69, 2479.57, 2627.68, 
   2780.30, 2864.89, 2928.66, 3013.91, 3107.94, 3189.62, 3302.45, 3412.46, 
   3499.48, 3588.35, 3709.16, 3820.88, 3875.33, 3944.44, 4031.57, 4142.40, 
   4234.41
   
   Saturation:
   0.52, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          14.7297        start      
        1            56         0.00117       1.31173     
        2            57       1.908e-006      0.00160     
        3            58       3.993e-010     1.659e-006   
        4            59       1.137e-011     2.555e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2012.08, 2134.76, 2232.37, 2314.74, 2413.34, 2547.19, 2681.41, 2822.05, 
   2966.45, 3046.18, 3106.07, 3185.81, 3273.45, 3349.32, 3453.76, 3555.25, 
   3635.26, 3716.70, 3827.10, 3928.93, 3978.43, 4041.09, 4119.93, 4220.02, 
   4303.01
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.72255        start      
        1            56       8.237e-004      1.68876     
        2            57       1.564e-006      0.00225     
        3            58       5.486e-010     2.810e-006   
        4            59       6.865e-012     7.988e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1998.16, 2119.09, 2215.94, 2298.01, 2396.50, 2530.43, 2664.93, 2805.99, 
   2950.95, 3031.05, 3091.27, 3171.49, 3259.71, 3336.12, 3441.36, 3543.67, 
   3624.38, 3706.57, 3818.04, 3920.92, 3970.96, 4034.35, 4114.17, 4215.60, 
   4299.78
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.91809        start      
        1            56       6.144e-004      1.67932     
        2            57       1.140e-006      0.00221     
        3            58       3.432e-010     2.863e-006   
        4            59       6.538e-012     6.727e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1992.41, 2112.40, 2208.73, 2290.50, 2388.74, 2522.45, 2656.82, 2797.85, 
   2942.85, 3023.03, 3083.32, 3163.69, 3252.10, 3328.71, 3434.27, 3536.94, 
   3617.96, 3700.51, 3812.52, 3915.94, 3966.27, 4030.07, 4110.45, 4212.69, 
   4297.63
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.06021        start      
        1            56       7.455e-004      1.98477     
        2            57       1.660e-006      0.00322     
        3            58       5.269e-010     5.332e-006   
        4            59       5.718e-012     1.446e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1989.05, 2108.41, 2204.35, 2285.85, 2383.84, 2517.29, 2651.47, 2792.35, 
   2937.28, 3017.44, 3077.74, 3158.16, 3246.65, 3323.36, 3429.08, 3531.94, 
   3613.15, 3695.93, 3808.29, 3912.08, 3962.61, 4026.71, 4107.52, 4210.38, 
   4295.90
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.49872        start      
        1            56       7.146e-004      2.03517     
        2            57       1.595e-006      0.00332     
        3            58       4.599e-010     5.733e-006   
        4            59       5.171e-012     1.408e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1986.68, 2105.58, 2201.19, 2282.47, 2380.24, 2513.43, 2647.41, 2788.12, 
   2932.92, 3013.04, 3073.33, 3153.75, 3242.27, 3319.03, 3424.85, 3527.85, 
   3609.19, 3692.13, 3804.76, 3908.83, 3959.53, 4023.87, 4105.02, 4208.39, 
   4294.41
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.11179        start      
        1            56       7.812e-004      2.19978     
        2            57       1.848e-006      0.00383     
        3            58       5.486e-010     7.219e-006   
        4            59       6.609e-012     1.883e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1984.85, 2103.35, 2198.70, 2279.78, 2377.35, 2510.31, 2644.09, 2784.63, 
   2929.29, 3009.35, 3069.61, 3150.02, 3238.56, 3315.34, 3421.23, 3524.32, 
   3605.76, 3688.83, 3801.68, 3905.99, 3956.82, 4021.36, 4102.82, 4206.63, 
   4293.08
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.78399        start      
        1            56       5.724e-004      1.94128     
        2            57       1.139e-006      0.00279     
        3            58       2.670e-010     4.506e-006   
        4            59       7.572e-012     8.760e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1983.33, 2101.51, 2196.62, 2277.53, 2374.91, 2507.65, 2641.24, 2781.62, 
   2926.14, 3006.14, 3066.37, 3146.76, 3235.29, 3312.09, 3418.03, 3521.19, 
   3602.71, 3685.89, 3798.92, 3903.44, 3954.38, 4019.11, 4100.83, 4205.04, 
   4291.88
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.53778        start      
        1            56       6.928e-004      2.18839     
        2            57       1.524e-006      0.00352     
        3            58       4.132e-010     6.404e-006   
        4            59       6.847e-012     1.512e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1982.02, 2099.92, 2194.83, 2275.58, 2372.79, 2505.33, 2638.74, 2778.96, 
   2923.34, 3003.29, 3063.49, 3143.85, 3232.37, 3309.18, 3415.14, 3518.37, 
   3599.96, 3683.23, 3796.42, 3901.12, 3952.17, 4017.06, 4099.02, 4203.59, 
   4290.77
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.33867        start      
        1            56       5.607e-004      2.01206     
        2            57       1.093e-006      0.00283     
        3            58       2.589e-010     4.611e-006   
        4            59       1.096e-011     9.227e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1980.87, 2098.51, 2193.23, 2273.84, 2370.90, 2503.25, 2636.50, 2776.56, 
   2920.82, 3000.71, 3060.87, 3141.21, 3229.72, 3306.52, 3412.52, 3515.79, 
   3597.44, 3680.80, 3794.13, 3898.99, 3950.14, 4015.17, 4097.35, 4202.25, 
   4289.76
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.15162        start      
        1            56       7.044e-004      2.30397     
        2            57       1.538e-006      0.00367     
        3            58       4.436e-010     6.798e-006   
        4            59       5.856e-012     1.741e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1979.84, 2097.25, 2191.80, 2272.28, 2369.20, 2501.37, 2634.46, 2774.38, 
   2918.51, 2998.34, 3058.48, 3138.79, 3227.28, 3304.09, 3410.10, 3513.42, 
   3595.11, 3678.55, 3792.01, 3897.02, 3948.25, 4013.42, 4095.80, 4201.01, 
   4288.81
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/26/2026 12:07:07 PM
   7/26/2026 12:08:25 PM
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
        0            1          774.147        start      
        1            56         0.00727       0.45966     
        2            57       6.641e-006     2.509e-004   
        3            58       1.044e-009     2.540e-007   
        4            59       3.456e-010     3.499e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1651.36, 1688.59, 1718.72, 1744.50, 1775.72, 1818.50, 1861.82, 1907.59, 
   1955.01, 1981.43, 2001.44, 2028.33, 2058.15, 2084.19, 2120.37, 2155.90, 
   2185.03, 2295.49, 2644.25, 2956.58, 3104.28, 3286.65, 3511.71, 3790.82, 
   4015.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.48, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          522.295        start      
        1            56         0.00304       0.36774     
        2            57       5.526e-006     1.044e-004   
        3            58       1.382e-009     1.462e-007   
        4            59       3.690e-010     5.175e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1614.96, 1643.25, 1666.14, 1685.74, 1709.47, 1742.01, 1774.96, 1809.81, 
   1845.92, 1866.05, 1881.31, 1901.87, 1937.22, 2097.87, 2335.74, 2560.27, 
   2734.47, 2909.42, 3144.69, 3360.11, 3464.01, 3594.33, 3756.78, 3960.53, 
   4126.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.31, 0.64, 0.72, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          430.694        start      
        1            56         0.00373       0.73342     
        2            57       5.694e-006     1.476e-004   
        3            58       3.033e-009     1.790e-007   
        4            59       2.290e-010     1.046e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1598.96, 1623.30, 1643.01, 1659.87, 1680.30, 1708.30, 1736.67, 1766.68, 
   1806.57, 1935.44, 2049.37, 2196.78, 2356.54, 2493.83, 2681.70, 2863.38, 
   3006.13, 3150.88, 3346.52, 3526.37, 3613.46, 3723.16, 3860.43, 4033.45, 
   4175.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.26, 0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          410.659        start      
        1            56         0.00428       0.47186     
        2            57       8.837e-006     2.234e-004   
        3            58       1.176e-009     4.378e-007   
        4            59       1.768e-010     6.537e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1581.66, 1601.75, 1618.01, 1631.93, 1648.88, 1696.13, 1898.18, 2110.65, 
   2324.07, 2440.37, 2527.15, 2642.15, 2768.20, 2877.14, 3026.87, 3172.15, 
   3286.55, 3402.83, 3560.24, 3705.20, 3775.51, 3864.27, 3975.60, 4116.32, 
   4232.24
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.37, 0.67, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          351.759        start      
        1            56         0.00333       0.36515     
        2            57       8.529e-006     1.335e-004   
        3            58       1.579e-009     3.783e-007   
        4            59       1.245e-010     1.267e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1573.94, 1592.17, 1616.77, 1722.05, 1853.24, 2027.79, 2200.82, 2381.07, 
   2565.44, 2666.98, 2743.15, 2844.47, 2955.76, 3052.09, 3184.65, 3313.43, 
   3414.94, 3518.21, 3658.14, 3787.12, 3849.74, 3928.91, 4028.35, 4154.26, 
   4258.22
   
   Saturation:
   0.20, 0.20, 0.32, 0.66, 0.73, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          290.821        start      
        1            56         0.00651       2.92957     
        2            57       6.255e-006     9.947e-004   
        3            58       2.948e-009     1.980e-006   
        4            59       1.154e-010     8.547e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1809.29, 1940.97, 2044.60, 2131.51, 2235.58, 2377.20, 2519.70, 2669.54, 
   2823.94, 2909.50, 2973.99, 3060.17, 3155.19, 3237.70, 3351.62, 3462.61, 
   3550.33, 3639.82, 3761.33, 3873.56, 3928.17, 3997.34, 4084.38, 4194.84, 
   4286.24
   
   Saturation:
   0.54, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          14.3054        start      
        1            56         0.00109       1.24845     
        2            57       1.736e-006      0.00149     
        3            58       3.494e-010     1.525e-006   
        4            59       1.305e-011     2.179e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2009.05, 2131.11, 2228.29, 2310.35, 2408.65, 2542.17, 2676.15, 2816.62, 
   2960.94, 3040.69, 3100.62, 3180.49, 3268.33, 3344.44, 3449.27, 3551.22, 
   3631.65, 3713.57, 3824.68, 3927.22, 3977.08, 4040.21, 4119.66, 4220.51, 
   4304.02
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.60495        start      
        1            56       7.852e-004      1.56952     
        2            57       1.479e-006      0.00212     
        3            58       4.944e-010     2.684e-006   
        4            59       9.222e-012     7.217e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1995.82, 2116.24, 2212.74, 2294.55, 2392.78, 2526.43, 2660.72, 2801.63, 
   2946.53, 3026.65, 3086.90, 3167.25, 3255.64, 3332.26, 3437.84, 3540.55, 
   3621.62, 3704.22, 3816.31, 3919.79, 3970.13, 4033.92, 4114.24, 4216.29, 
   4300.88
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.84918        start      
        1            56       5.760e-004      1.51844     
        2            57       1.056e-006      0.00203     
        3            58       2.924e-010     2.670e-006   
        4            59       7.217e-012     5.820e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1990.45, 2110.01, 2206.04, 2287.59, 2385.62, 2519.09, 2653.30, 2794.22, 
   2939.19, 3019.38, 3079.73, 3160.21, 3248.79, 3325.59, 3431.46, 3534.48, 
   3615.81, 3698.73, 3811.27, 3915.21, 3965.80, 4029.95, 4110.76, 4213.51, 
   4298.77
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.02039        start      
        1            56       6.964e-004      1.77026     
        2            57       1.530e-006      0.00293     
        3            58       4.313e-010     4.938e-006   
        4            59       5.087e-012     1.184e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1987.37, 2106.38, 2202.06, 2283.38, 2381.19, 2514.45, 2648.51, 2789.31, 
   2934.22, 3014.41, 3074.77, 3155.29, 3243.95, 3320.82, 3426.83, 3530.01, 
   3611.51, 3694.61, 3807.44, 3911.68, 3962.44, 4026.84, 4108.01, 4211.29, 
   4297.05
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.53850        start      
        1            56       7.074e-004      1.84860     
        2            57       1.602e-006      0.00318     
        3            58       4.189e-010     5.751e-006   
        4            59       8.174e-012     1.304e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1985.25, 2103.83, 2199.23, 2280.36, 2377.98, 2511.02, 2644.91, 2785.57, 
   2930.37, 3010.53, 3070.87, 3151.40, 3240.08, 3316.99, 3423.07, 3526.36, 
   3607.95, 3691.18, 3804.22, 3908.69, 3959.59, 4024.18, 4105.65, 4209.37, 
   4295.57
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.12303        start      
        1            56       7.515e-004      1.97146     
        2            57       1.769e-006      0.00355     
        3            58       4.525e-010     6.909e-006   
        4            59       1.091e-011     1.581e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1983.61, 2101.85, 2197.02, 2277.97, 2375.42, 2508.26, 2641.97, 2782.49, 
   2927.17, 3007.27, 3067.59, 3148.10, 3236.78, 3313.71, 3419.84, 3523.19, 
   3604.86, 3688.19, 3801.40, 3906.06, 3957.06, 4021.83, 4103.55, 4207.66, 
   4294.24
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.85255        start      
        1            56       5.821e-004      1.78004     
        2            57       1.183e-006      0.00273     
        3            58       2.448e-010     4.668e-006   
        4            59       6.690e-012     8.280e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1982.27, 2100.22, 2195.18, 2275.99, 2373.27, 2505.92, 2639.47, 2779.83, 
   2924.39, 3004.44, 3064.73, 3145.21, 3233.88, 3310.81, 3416.96, 3520.37, 
   3602.09, 3685.51, 3798.86, 3903.69, 3954.79, 4019.71, 4101.65, 4206.10, 
   4293.03
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.61014        start      
        1            56       7.108e-004      2.01745     
        2            57       1.601e-006      0.00346     
        3            58       3.804e-010     6.710e-006   
        4            59       8.517e-012     1.432e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1981.11, 2098.82, 2193.60, 2274.26, 2371.40, 2503.87, 2637.26, 2777.48, 
   2921.91, 3001.90, 3062.16, 3142.61, 3231.26, 3308.19, 3414.36, 3517.80, 
   3599.57, 3683.06, 3796.54, 3901.51, 3952.70, 4017.76, 4099.91, 4204.67, 
   4291.91
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.37706        start      
        1            56       5.583e-004      1.83834     
        2            57       1.090e-006      0.00270     
        3            58       2.198e-010     4.589e-006   
        4            59       9.664e-012     7.995e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1980.09, 2097.57, 2192.19, 2272.72, 2369.73, 2502.03, 2635.26, 2775.34, 
   2919.65, 2999.59, 3059.81, 3140.24, 3228.86, 3305.79, 3411.97, 3515.44, 
   3597.25, 3680.80, 3794.39, 3899.50, 3950.77, 4015.95, 4098.29, 4203.34, 
   4290.88
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.23471        start      
        1            56       7.415e-004      2.15753     
        2            57       1.672e-006      0.00370     
        3            58       4.238e-010     7.373e-006   
        4            59       5.817e-012     1.718e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1979.18, 2096.45, 2190.91, 2271.33, 2368.21, 2500.35, 2633.44, 2773.39, 
   2917.58, 2997.46, 3057.65, 3138.04, 3226.65, 3303.57, 3409.75, 3513.25, 
   3595.10, 3678.70, 3792.40, 3897.62, 3948.97, 4014.27, 4096.78, 4202.11, 
   4289.91
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.05201        start      
        1            56       6.016e-004      1.99414     
        2            57       1.198e-006      0.00298     
        3            58       2.690e-010     5.288e-006   
        4            59       6.120e-012     1.059e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1978.34, 2095.42, 2189.75, 2270.06, 2366.81, 2498.80, 2631.76, 2771.58, 
   2915.65, 2995.48, 3055.64, 3136.00, 3224.58, 3301.49, 3407.68, 3511.20, 
   3593.08, 3676.74, 3790.53, 3895.87, 3947.28, 4012.69, 4095.37, 4200.95, 
   4289.01
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.92413        start      
        1            56       7.988e-004      2.34226     
        2            57       1.824e-006      0.00407     
        3            58       5.273e-010     8.360e-006   
        4            59       5.490e-012     2.252e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1977.56, 2094.47, 2188.66, 2268.87, 2365.51, 2497.36, 2630.19, 2769.89, 
   2913.85, 2993.63, 3053.75, 3134.09, 3222.65, 3299.54, 3405.74, 3509.28, 
   3591.19, 3674.90, 3788.78, 3894.22, 3945.69, 4011.21, 4094.04, 4199.86, 
   4288.16
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.81657        start      
        1            56       6.968e-004      2.22590     
        2            57       1.466e-006      0.00353     
        3            58       3.955e-010     6.723e-006   
        4            59       9.968e-012     1.657e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1976.84, 2093.59, 2187.65, 2267.76, 2364.30, 2496.01, 2628.71, 2768.30, 
   2912.16, 2991.89, 3051.98, 3132.29, 3220.82, 3297.71, 3403.91, 3507.47, 
   3589.41, 3673.16, 3787.12, 3892.66, 3944.20, 4009.81, 4092.79, 4198.83, 
   4287.36
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.68837        start      
        1            56       5.867e-004      2.08761     
        2            57       1.115e-006      0.00296     
        3            58       2.786e-010     5.107e-006   
        4            59       7.186e-012     1.136e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1976.16, 2092.75, 2186.70, 2266.72, 2363.15, 2494.74, 2627.33, 2766.80, 
   2910.56, 2990.25, 3050.31, 3130.58, 3219.10, 3295.98, 3402.17, 3505.75, 
   3587.72, 3671.51, 3785.56, 3891.19, 3942.79, 4008.48, 4091.60, 4197.86, 
   4286.61
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/26/2026 12:09:32 PM
   7/26/2026 12:11:01 PM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

