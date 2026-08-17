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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          194.218        start      
        1            55         14981.8       2653.22     
        2            56         7692.30       1305.33     
        3            57         307.560       1393.40     
        4            58         4.71013       46.4455     
        5            59       1.927e-005      0.69984     
        6            60       1.655e-007     3.233e-006   
        7            61       1.625e-007     5.041e-010   
   Producer BHP: 
   2266.84 psi
   
   Injector BHP: 
   2642.48 psi
   
   Pressure: 
   2308.16, 2316.56, 2324.52, 2331.52, 2336.93, 2341.89, 2347.03, 2353.26, 
   2360.57, 2367.17, 2373.05, 2379.05, 2386.33, 2394.05, 2402.85, 2410.60, 
   2416.77, 2426.46, 2434.43, 2438.90, 2443.88, 2449.26, 2454.81, 2466.93, 
   2510.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 
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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          179.654        start      
        1            56         0.00159       4.15140     
        2            57       1.052e-006     3.076e-004   
        3            58       2.514e-009     1.153e-007   
        4            59       7.616e-011     1.811e-010   
   Producer BHP: 
   1611.26 psi
   
   Injector BHP: 
   2033.09 psi
   
   Pressure: 
   1652.67, 1661.08, 1669.04, 1676.04, 1681.44, 1686.40, 1691.53, 1697.73, 
   1705.01, 1711.59, 1717.43, 1723.39, 1730.62, 1738.27, 1746.99, 1754.66, 
   1760.76, 1770.33, 1778.19, 1782.60, 1787.50, 1793.07, 1816.36, 1860.86, 
   1900.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.51, 0.71, 
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
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.228        start      
        1            54         0.00391       0.11109     
        2            55       1.128e-004      0.00225     
        3            56       1.133e-008     6.377e-005   
        4            57       2.421e-009     4.850e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1928.35 psi
   
   Pressure: 
   1529.86, 1536.00, 1541.88, 1547.12, 1551.21, 1555.01, 1558.99, 1563.87, 
   1569.67, 1574.97, 1579.76, 1584.69, 1590.76, 1597.28, 1604.80, 1611.51, 
   1616.92, 1625.54, 1632.72, 1636.81, 1642.20, 1673.35, 1718.30, 1758.91, 
   1795.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.72, 0.76, 
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
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.241        start      
        1            56       1.827e-004      0.05038     
        2            57       1.814e-007     5.591e-006   
        3            58       2.073e-010     3.390e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1967.02 psi
   
   Pressure: 
   1529.88, 1536.02, 1541.91, 1547.14, 1551.24, 1555.04, 1559.02, 1563.90, 
   1569.70, 1575.01, 1579.80, 1584.74, 1590.81, 1597.33, 1604.86, 1611.57, 
   1616.99, 1625.61, 1632.81, 1638.85, 1675.09, 1718.35, 1760.53, 1799.04, 
   1834.35
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.30, 0.64, 0.73, 0.76, 0.78, 
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
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.679        start      
        1            55         1046.13       584.085     
        2            56         541.787       281.684     
        3            57         1.12995       302.007     
        4            58         0.05869       0.36268     
        5            59         0.00577       0.03616     
        6            60       3.291e-006      0.00322     
        7            61       2.227e-007     1.963e-006   
        8            62       2.374e-008     1.378e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2000.79 psi
   
   Pressure: 
   1529.85, 1535.99, 1541.87, 1547.11, 1551.19, 1554.99, 1558.97, 1563.85, 
   1569.64, 1574.95, 1579.73, 1584.66, 1590.73, 1597.24, 1604.76, 1611.46, 
   1616.87, 1625.55, 1641.50, 1676.27, 1715.33, 1756.20, 1796.57, 1833.74, 
   1868.12
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.38, 0.68, 0.73, 0.76, 0.77, 0.79, 
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
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.230        start      
        1            55         0.09901       0.21813     
        2            56         0.00467       0.05805     
        3            57       5.339e-006      0.00287     
        4            58       2.302e-009     3.287e-006   
        5            59       1.032e-009     2.015e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2062.02 psi
   
   Pressure: 
   1529.75, 1535.87, 1541.73, 1546.95, 1551.02, 1554.81, 1558.77, 1563.63, 
   1569.40, 1574.69, 1579.45, 1584.37, 1590.41, 1596.90, 1604.39, 1611.07, 
   1616.63, 1647.42, 1709.77, 1743.97, 1781.11, 1820.37, 1859.47, 1895.67, 
   1929.37
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.47, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
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
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.315        start      
        1            54         2598.21       1518.86     
        2            55         1554.13       607.993     
        3            56         5.63911       909.649     
        4            57         1.01489       0.64360     
        5            58         8.65280       5.67959     
        6            59       3.640e-004      5.08380     
        7            60       2.199e-006     2.163e-004   
        8            61       7.743e-008     1.337e-006   
        9            62       1.417e-007     1.287e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2121.40 psi
   
   Pressure: 
   1529.77, 1535.89, 1541.76, 1546.98, 1551.05, 1554.84, 1558.81, 1563.67, 
   1569.45, 1574.74, 1579.51, 1584.43, 1590.47, 1596.97, 1604.47, 1611.83, 
   1640.46, 1715.12, 1774.83, 1807.66, 1843.62, 1881.83, 1920.06, 1955.58, 
   1988.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 
   0.55, 0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
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
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.162        start      
        1            56       9.201e-004      0.32582     
        2            57       1.290e-006     3.673e-005   
        3            58       8.330e-010     4.244e-008   
        4            59       6.575e-011     3.629e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2171.69 psi
   
   Pressure: 
   1529.71, 1535.82, 1541.67, 1546.88, 1550.95, 1554.73, 1558.69, 1563.54, 
   1569.31, 1574.59, 1579.35, 1584.26, 1590.29, 1596.78, 1606.38, 1654.26, 
   1700.81, 1771.95, 1829.46, 1861.33, 1896.40, 1933.79, 1971.31, 2006.28, 
   2039.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
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
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.343        start      
        1            55       7.619e-006      0.01264     
        2            56       1.869e-009     1.412e-007   
        3            57       1.610e-009     4.418e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2230.39 psi
   
   Pressure: 
   1529.77, 1535.88, 1541.75, 1546.97, 1551.04, 1554.83, 1558.79, 1563.65, 
   1569.43, 1574.72, 1579.49, 1584.40, 1590.47, 1601.95, 1664.34, 1721.70, 
   1766.30, 1835.25, 1891.37, 1922.59, 1957.02, 1993.80, 2030.78, 2065.31, 
   2097.81
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 
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
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          175.934        start      
        1            54         1699.03       383.885     
        2            55         840.804       189.330     
        3            56         16.6157       184.418     
        4            57         16.5002       0.07088     
        5            58         11.0898       3.31559     
        6            59         0.01272       6.78836     
        7            60         0.01052       0.00133     
        8            61         0.01325       0.00167     
        9            62       1.498e-006      0.00812     
        10           63       1.894e-005     1.070e-005   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          175.949        start      
        1            56         0.52553       0.32380     
        2            57         0.26205       0.16199     
        3            58       1.285e-008      0.16111     
        4            59       2.236e-009     1.676e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2278.22 psi
   
   Pressure: 
   1529.81, 1535.93, 1541.80, 1547.02, 1551.10, 1554.89, 1558.86, 1563.73, 
   1569.51, 1574.80, 1579.58, 1584.58, 1600.68, 1657.05, 1720.90, 1776.00, 
   1819.34, 1886.76, 1941.83, 1972.53, 2006.45, 2042.72, 2079.26, 2113.43, 
   2145.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.42, 0.69, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
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
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.045        start      
        1            56       6.730e-004      0.13772     
        2            57       6.738e-007     3.037e-005   
        3            58       1.865e-010     3.713e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2319.81 psi
   
   Pressure: 
   1529.74, 1535.85, 1541.71, 1546.92, 1550.99, 1554.77, 1558.73, 1563.59, 
   1569.36, 1574.64, 1579.68, 1600.89, 1653.54, 1708.16, 1769.58, 1823.13, 
   1865.50, 1931.64, 1985.78, 2016.01, 2049.45, 2085.27, 2121.41, 2155.27, 
   2187.29
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 
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
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.351        start      
        1            55         0.24885       0.19342     
        2            56         0.24574       0.00164     
        3            57         0.26523       0.01029     
        4            58         0.00442       0.13778     
        5            59       1.107e-006      0.00234     
        6            60       5.291e-008     5.566e-007   
        7            61       1.136e-007     8.800e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2358.04 psi
   
   Pressure: 
   1529.70, 1535.80, 1541.65, 1546.85, 1550.92, 1554.70, 1558.65, 1563.50, 
   1569.26, 1575.57, 1607.40, 1649.94, 1700.61, 1753.47, 1813.37, 1865.84, 
   1907.48, 1972.61, 2025.99, 2055.83, 2088.88, 2124.31, 2160.11, 2193.70, 
   2225.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.25, 0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
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
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.273        start      
        1            56       5.804e-004      0.21091     
        2            57       1.054e-006     2.473e-005   
        3            58       5.589e-010     2.831e-008   
        4            59       1.160e-010     5.443e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2394.53 psi
   
   Pressure: 
   1529.72, 1535.83, 1541.68, 1546.89, 1550.95, 1554.73, 1558.69, 1563.55, 
   1571.79, 1612.58, 1653.47, 1694.40, 1743.59, 1795.36, 1854.23, 1905.93, 
   1947.01, 2011.34, 2064.12, 2093.65, 2126.37, 2161.49, 2197.01, 2230.37, 
   2262.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.29, 0.63, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 
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
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          161.824        start      
        1            56       3.566e-004      0.09935     
        2            57       4.514e-007     1.160e-005   
        3            58       2.239e-010     1.334e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2434.75 psi
   
   Pressure: 
   1529.77, 1535.89, 1541.75, 1546.96, 1551.04, 1554.82, 1558.81, 1567.69, 
   1616.05, 1661.38, 1700.86, 1740.77, 1789.04, 1840.03, 1898.12, 1949.20, 
   1989.82, 2053.50, 2105.77, 2135.03, 2167.48, 2202.33, 2237.60, 2270.76, 
   2302.28
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 
   0.66, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
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
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.596        start      
        1            56       4.642e-005      0.02154     
        2            57       1.368e-008     1.075e-006   
        3            58       5.252e-010     3.149e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2466.73 psi
   
   Pressure: 
   1529.81, 1535.93, 1541.80, 1547.02, 1551.09, 1554.94, 1565.21, 1607.05, 
   1656.33, 1700.12, 1738.72, 1777.94, 1825.51, 1875.87, 1933.31, 1983.85, 
   2024.08, 2087.18, 2139.00, 2168.03, 2200.23, 2234.84, 2269.89, 2302.88, 
   2334.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.41, 0.68, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
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
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.548        start      
        1            56         0.00102       0.13895     
        2            57       1.454e-006     4.043e-005   
        3            58       1.979e-010     7.393e-008   
        4            59       7.504e-011     1.765e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2492.75 psi
   
   Pressure: 
   1529.75, 1535.86, 1541.72, 1546.93, 1551.20, 1566.51, 1600.92, 1642.11, 
   1689.73, 1732.47, 1770.38, 1809.01, 1855.94, 1905.69, 1962.48, 2012.49, 
   2052.33, 2114.85, 2166.22, 2195.02, 2226.98, 2261.35, 2296.20, 2329.03, 
   2360.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.73, 
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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          155.515        start      
        1            55         62.5590       41.0024     
        2            56         59.9127       1.73439     
        3            57         13.7307       48.2690     
        4            58         0.00416       9.00288     
        5            59         0.00242       0.00434     
        6            60       6.524e-004      0.00116     
        7            61       3.316e-007     4.274e-004   
        8            62       3.631e-007     4.554e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2518.80 psi
   
   Pressure: 
   1529.68, 1535.78, 1541.62, 1547.50, 1569.79, 1602.49, 1635.82, 1675.71, 
   1722.25, 1764.24, 1801.59, 1839.71, 1886.07, 1935.27, 1991.47, 2040.98, 
   2080.45, 2142.43, 2193.39, 2221.96, 2253.70, 2287.86, 2322.52, 2355.19, 
   2386.37
   
   Saturation:
   0.20, 0.20, 0.20, 0.23, 0.55, 0.71, 0.74, 0.75, 
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
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          144.720        start      
        1            56         98.9448       1.88431     
        2            57         114.940       3.57947     
        3            58         42.7095       28.4188     
        4            59         40.8795       1.11409     
        5            60         0.12175       24.8138     
        6            61         0.01934       0.06259     
        7            62         0.00415       0.01431     
        8            63       6.773e-006      0.00253     
        9            64       1.160e-007     4.057e-006   
        10           65       1.711e-007     1.720e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2556.16 psi
   
   Pressure: 
   1529.60, 1535.68, 1543.40, 1582.01, 1617.10, 1648.65, 1681.00, 1720.00, 
   1765.73, 1807.09, 1843.95, 1881.60, 1927.42, 1976.09, 2031.71, 2080.75, 
   2119.85, 2181.29, 2231.85, 2260.21, 2291.73, 2325.67, 2360.14, 2392.67, 
   2423.74
   
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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          156.575        start      
        1            55         33.4110       18.0295     
        2            56         16.7076       9.01463     
        3            57       1.146e-004      9.01421     
        4            58       3.348e-008     1.543e-005   
        5            59       2.668e-009     8.294e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2598.64 psi
   
   Pressure: 
   1529.73, 1539.63, 1587.53, 1632.16, 1666.07, 1696.90, 1728.73, 1767.23, 
   1812.47, 1853.44, 1889.98, 1927.33, 1972.80, 2021.11, 2076.35, 2125.05, 
   2163.91, 2224.96, 2275.21, 2303.41, 2334.77, 2368.55, 2402.86, 2435.27, 
   2466.25
   
   Saturation:
   0.20, 0.32, 0.65, 0.73, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
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
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          161.957        start      
        1            55         126.133       92.1580     
        2            56         63.9632       45.5099     
        3            57         0.01036       46.6424     
        4            58       9.882e-006      0.00708     
        5            59       4.449e-008     6.301e-006   
        6            60       1.901e-009     3.084e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2655.26 psi
   
   Pressure: 
   1559.28, 1609.98, 1659.12, 1701.61, 1734.24, 1764.14, 1795.13, 1832.73, 
   1877.00, 1917.17, 1953.05, 1989.76, 2034.50, 2082.09, 2136.55, 2184.62, 
   2223.01, 2283.38, 2333.12, 2361.06, 2392.15, 2425.68, 2459.78, 2492.03, 
   2522.89
   
   Saturation:
   0.37, 0.67, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   8/17/2026 9:29:53 AM
   8/17/2026 9:31:12 AM
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
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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

