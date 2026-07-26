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
        0            1          195.309        start      
        1            56         0.00223       4.54184     
        2            57       2.423e-006     3.947e-004   
        3            58       6.762e-009     1.919e-007   
        4            59       7.733e-011     7.546e-010   
   Producer BHP: 
   2252.87 psi
   
   Injector BHP: 
   2734.46 psi
   
   Pressure: 
   2279.65, 2285.90, 2293.35, 2301.63, 2308.99, 2314.83, 2322.16, 2331.51, 
   2340.25, 2357.71, 2374.29, 2381.70, 2389.06, 2395.29, 2400.78, 2406.47, 
   2411.43, 2426.30, 2445.29, 2453.72, 2458.65, 2464.13, 2475.04, 2508.48, 
   2569.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.43, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.731        start      
        1            56         0.00287       6.06186     
        2            57       2.659e-006     6.437e-004   
        3            58       5.711e-009     4.806e-007   
        4            59       7.073e-011     8.260e-010   
   Producer BHP: 
   1588.96 psi
   
   Injector BHP: 
   2178.76 psi
   
   Pressure: 
   1615.80, 1622.06, 1629.52, 1637.80, 1645.15, 1650.97, 1658.29, 1667.61, 
   1676.30, 1693.67, 1710.16, 1717.51, 1724.81, 1730.99, 1736.42, 1742.05, 
   1746.95, 1761.63, 1780.35, 1788.66, 1793.50, 1799.44, 1859.38, 1958.46, 
   2013.74
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 0.56, 0.73, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.798        start      
        1            56         0.00209       0.20335     
        2            57       7.117e-006     1.056e-004   
        3            58       7.682e-009     2.178e-007   
        4            59       5.559e-011     1.865e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2102.00 psi
   
   Pressure: 
   1519.37, 1523.95, 1529.46, 1535.66, 1541.23, 1545.70, 1551.39, 1558.73, 
   1565.67, 1579.71, 1593.22, 1599.33, 1605.48, 1610.76, 1615.46, 1620.40, 
   1624.77, 1638.05, 1655.23, 1662.98, 1669.23, 1707.74, 1795.46, 1885.49, 
   1936.89
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.28, 0.62, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.768        start      
        1            56         0.00579       0.26034     
        2            57       2.570e-005     3.243e-004   
        3            58       1.791e-008     1.674e-006   
        4            59       4.480e-011     6.672e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2135.65 psi
   
   Pressure: 
   1519.37, 1523.94, 1529.45, 1535.65, 1541.21, 1545.69, 1551.37, 1558.71, 
   1565.65, 1579.68, 1593.18, 1599.29, 1605.43, 1610.71, 1615.41, 1620.35, 
   1624.71, 1637.99, 1655.25, 1670.84, 1709.72, 1753.44, 1835.54, 1921.11, 
   1970.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.920        start      
        1            56         0.00871       1.08223     
        2            57       3.153e-005      0.00133     
        3            58       7.999e-009     5.281e-006   
        4            59       5.097e-011     1.124e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2213.13 psi
   
   Pressure: 
   1519.22, 1523.75, 1529.23, 1535.37, 1540.89, 1545.33, 1550.97, 1558.25, 
   1565.13, 1579.05, 1592.45, 1598.50, 1604.59, 1609.83, 1614.49, 1619.38, 
   1623.71, 1637.19, 1692.92, 1759.78, 1797.87, 1839.02, 1917.42, 1999.96, 
   2048.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.46, 0.70, 0.74, 0.76, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.808        start      
        1            56         0.00700       1.24788     
        2            57       1.389e-005     7.430e-004   
        3            58       1.144e-008     2.610e-006   
        4            59       4.089e-011     1.147e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2352.02 psi
   
   Pressure: 
   1519.17, 1523.69, 1529.15, 1535.27, 1540.78, 1545.21, 1550.83, 1558.09, 
   1564.96, 1578.84, 1592.20, 1598.24, 1604.32, 1609.54, 1614.18, 1619.07, 
   1623.79, 1695.64, 1843.75, 1907.56, 1943.88, 1983.48, 2059.46, 2139.90, 
   2187.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.55, 0.71, 0.74, 0.76, 0.77, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.453        start      
        1            56         0.00299       0.29675     
        2            57       8.423e-006     1.080e-004   
        3            58       6.375e-009     2.861e-007   
        4            59       4.824e-011     2.228e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2409.54 psi
   
   Pressure: 
   1519.35, 1523.91, 1529.42, 1535.60, 1541.16, 1545.63, 1551.31, 1558.63, 
   1565.56, 1579.58, 1593.06, 1599.16, 1605.29, 1610.56, 1615.26, 1621.53, 
   1652.38, 1766.70, 1908.48, 1970.19, 2005.60, 2044.35, 2118.92, 2198.06, 
   2244.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 
   0.61, 0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          156.021        start      
        1            56         0.00385       0.26157     
        2            57       1.926e-005     1.642e-004   
        3            58       4.416e-008     5.421e-007   
        4            59       7.049e-011     1.755e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2436.14 psi
   
   Pressure: 
   1519.35, 1523.92, 1529.42, 1535.61, 1541.16, 1545.63, 1551.31, 1558.63, 
   1565.56, 1579.58, 1593.06, 1599.16, 1605.29, 1610.58, 1618.17, 1657.98, 
   1695.30, 1804.32, 1941.21, 2001.25, 2035.86, 2073.84, 2147.14, 2225.18, 
   2271.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.65, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.266        start      
        1            56         0.00658       0.26999     
        2            57       3.191e-005     3.043e-004   
        3            58       1.906e-008     1.791e-006   
        4            59       4.311e-011     7.796e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2465.15 psi
   
   Pressure: 
   1519.35, 1523.91, 1529.42, 1535.60, 1541.16, 1545.62, 1551.30, 1558.62, 
   1565.55, 1579.56, 1593.04, 1599.13, 1605.33, 1617.87, 1658.04, 1700.05, 
   1735.81, 1841.52, 1975.07, 2033.86, 2067.84, 2105.21, 2177.49, 2254.61, 
   2300.24
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.156        start      
        1            56         0.00793       0.51517     
        2            57       3.251e-005     5.526e-004   
        3            58       4.719e-009     2.883e-006   
        4            59       4.960e-011     2.840e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2502.08 psi
   
   Pressure: 
   1519.29, 1523.83, 1529.32, 1535.48, 1541.02, 1545.47, 1551.12, 1558.42, 
   1565.32, 1579.28, 1592.71, 1599.03, 1622.15, 1667.80, 1707.32, 1747.60, 
   1782.29, 1885.47, 2016.25, 2073.97, 2107.40, 2144.23, 2215.60, 2291.92, 
   2337.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.48, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.278        start      
        1            56         0.00527       0.75006     
        2            57       6.072e-006     2.517e-004   
        3            58       1.117e-008     7.517e-007   
        4            59       4.190e-011     8.006e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2549.29 psi
   
   Pressure: 
   1519.22, 1523.74, 1529.21, 1535.34, 1540.86, 1545.29, 1550.93, 1558.19, 
   1565.07, 1578.98, 1594.09, 1629.45, 1682.40, 1726.34, 1764.39, 1803.54, 
   1837.45, 1938.63, 2067.14, 2123.96, 2156.91, 2193.26, 2263.85, 2339.47, 
   2384.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.23, 0.57, 0.71, 0.74, 0.75, 0.76, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.471        start      
        1            56         0.00287       1.43463     
        2            57       9.071e-006     2.553e-004   
        3            58       9.600e-009     5.677e-007   
        4            59       5.721e-011     6.357e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2637.49 psi
   
   Pressure: 
   1519.01, 1523.49, 1528.89, 1534.96, 1540.41, 1544.80, 1550.37, 1557.56, 
   1564.37, 1582.38, 1681.55, 1733.29, 1783.68, 1825.89, 1862.76, 1900.89, 
   1934.02, 2033.12, 2159.24, 2215.08, 2247.54, 2283.40, 2353.15, 2428.05, 
   2472.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          155.169        start      
        1            56         0.00457       0.97636     
        2            57       2.416e-005     2.993e-004   
        3            58       5.649e-008     1.117e-006   
        4            59       5.091e-011     3.027e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2745.03 psi
   
   Pressure: 
   1519.13, 1523.64, 1529.08, 1535.19, 1540.68, 1545.09, 1550.70, 1557.95, 
   1569.63, 1684.17, 1798.71, 1848.74, 1897.97, 1939.47, 1975.85, 2013.52, 
   2046.29, 2144.40, 2269.28, 2324.61, 2356.78, 2392.33, 2461.54, 2535.92, 
   2580.30
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.33, 0.65, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          175.223        start      
        1            56         0.00590       0.38255     
        2            57       2.695e-005     3.792e-004   
        3            58       1.289e-008     2.069e-006   
        4            59       5.787e-011     7.935e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2799.67 psi
   
   Pressure: 
   1519.31, 1523.85, 1529.34, 1535.50, 1541.04, 1545.49, 1551.21, 1568.67, 
   1628.50, 1747.89, 1859.08, 1908.23, 1956.85, 1997.97, 2034.05, 2071.46, 
   2104.01, 2201.50, 2325.61, 2380.60, 2412.57, 2447.92, 2516.76, 2590.77, 
   2634.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.387        start      
        1            56         0.00803       0.50282     
        2            57       3.689e-005     5.313e-004   
        3            58       9.419e-009     3.175e-006   
        4            59       3.540e-011     7.701e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2843.67 psi
   
   Pressure: 
   1519.27, 1523.80, 1529.28, 1535.42, 1540.95, 1545.54, 1565.80, 1629.28, 
   1687.59, 1802.61, 1911.00, 1959.19, 2007.01, 2047.54, 2083.15, 2120.10, 
   2152.30, 2248.77, 2371.68, 2426.18, 2457.89, 2492.98, 2561.36, 2634.97, 
   2678.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.47, 0.69, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.800        start      
        1            56         0.00577       0.47680     
        2            57       1.369e-005     2.761e-004   
        3            58       5.301e-009     1.293e-006   
        4            59       4.541e-011     1.715e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2877.40 psi
   
   Pressure: 
   1519.27, 1523.81, 1529.28, 1535.43, 1541.49, 1564.51, 1613.63, 1675.21, 
   1731.81, 1844.34, 1950.93, 1998.45, 2045.70, 2085.77, 2121.02, 2157.62, 
   2189.53, 2285.20, 2407.14, 2461.24, 2492.73, 2527.60, 2595.61, 2668.88, 
   2712.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.22, 0.54, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.562        start      
        1            56         0.00327       0.67267     
        2            57       3.935e-006     1.068e-004   
        3            58       9.089e-009     1.112e-007   
        4            59       4.019e-011     3.293e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2909.07 psi
   
   Pressure: 
   1519.20, 1523.72, 1529.18, 1536.61, 1573.34, 1611.68, 1658.88, 1718.47, 
   1773.80, 1884.22, 1989.11, 2035.97, 2082.60, 2122.19, 2157.05, 2193.26, 
   2224.86, 2319.65, 2440.55, 2494.22, 2525.49, 2560.14, 2627.77, 2700.72, 
   2744.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.941        start      
        1            56         0.00323       0.65832     
        2            57       1.502e-005     2.141e-004   
        3            58       4.875e-008     5.044e-007   
        4            59       6.122e-011     2.125e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2950.30 psi
   
   Pressure: 
   1519.20, 1523.73, 1531.79, 1579.33, 1626.90, 1663.89, 1709.89, 1768.33, 
   1822.85, 1931.85, 2035.55, 2081.91, 2128.09, 2167.32, 2201.88, 2237.79, 
   2269.14, 2363.23, 2483.29, 2536.61, 2567.69, 2602.14, 2669.45, 2742.10, 
   2785.69
   
   Saturation:
   0.20, 0.20, 0.30, 0.63, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.436        start      
        1            56         0.00550       0.38522     
        2            57       2.845e-005     2.172e-004   
        3            58       4.971e-008     1.848e-006   
        4            59       5.491e-011     2.707e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2990.56 psi
   
   Pressure: 
   1519.42, 1528.85, 1575.54, 1628.45, 1674.56, 1710.82, 1756.21, 1814.05, 
   1868.11, 1976.32, 2079.34, 2125.42, 2171.33, 2210.34, 2244.71, 2280.44, 
   2311.62, 2405.24, 2524.72, 2577.79, 2608.73, 2643.04, 2710.09, 2782.50, 
   2825.98
   
   Saturation:
   0.20, 0.37, 0.67, 0.73, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.538        start      
        1            56         0.00611       1.39124     
        2            57       2.158e-005      0.00147     
        3            58       1.939e-008     6.562e-006   
        4            59       5.263e-011     5.752e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3034.19 psi
   
   Pressure: 
   1555.11, 1593.64, 1639.49, 1689.79, 1734.19, 1769.35, 1813.56, 1870.05, 
   1922.98, 2029.08, 2130.24, 2175.56, 2220.75, 2259.20, 2293.10, 2328.37, 
   2359.20, 2451.83, 2570.15, 2622.76, 2653.46, 2687.53, 2754.21, 2826.30, 
   2869.63
   
   Saturation:
   0.44, 0.69, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/27/2026 12:01:04 AM
   7/27/2026 12:02:58 AM
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
        0            1          253.428        start      
        1            56         0.00277       0.32878     
        2            57       8.595e-006     2.024e-004   
        3            58       6.858e-009     4.178e-007   
        4            59       8.274e-011     4.801e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2260.62 psi
   
   Pressure: 
   1529.06, 1535.92, 1544.19, 1553.49, 1561.85, 1568.56, 1577.09, 1588.10, 
   1598.52, 1619.59, 1639.86, 1649.03, 1658.25, 1666.17, 1673.22, 1680.64, 
   1687.19, 1707.13, 1732.90, 1744.52, 1751.40, 1759.17, 1783.13, 1926.83, 
   2013.06
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.66, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          231.172        start      
        1            56         0.00296       0.30357     
        2            57       8.788e-006     1.224e-004   
        3            58       9.294e-009     2.257e-007   
        4            59       6.075e-011     2.035e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2401.58 psi
   
   Pressure: 
   1529.06, 1535.92, 1544.20, 1553.49, 1561.85, 1568.56, 1577.09, 1588.10, 
   1598.52, 1619.58, 1639.85, 1649.02, 1658.24, 1666.16, 1673.21, 1680.62, 
   1687.18, 1707.11, 1732.88, 1744.50, 1753.72, 1811.55, 1942.97, 2077.42, 
   2154.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 0.73, 0.77, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          228.306        start      
        1            56         0.00393       0.80802     
        2            57       2.281e-006     2.017e-004   
        3            58       1.747e-009     1.677e-007   
        4            59       8.150e-011     1.054e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2500.06 psi
   
   Pressure: 
   1528.87, 1535.68, 1543.90, 1553.13, 1561.42, 1568.09, 1576.57, 1587.50, 
   1597.84, 1618.76, 1638.89, 1647.99, 1657.14, 1665.01, 1672.01, 1679.37, 
   1685.88, 1705.67, 1736.15, 1812.84, 1871.69, 1934.93, 2054.54, 2179.87, 
   2252.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.25, 0.59, 0.72, 0.75, 0.77, 0.79, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          240.247        start      
        1            56         0.00697       1.38807     
        2            57       1.163e-005     6.876e-004   
        3            58       6.905e-009     1.657e-006   
        4            59       7.987e-011     5.741e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2769.67 psi
   
   Pressure: 
   1528.63, 1535.39, 1543.54, 1552.69, 1560.92, 1567.53, 1575.93, 1586.77, 
   1597.03, 1617.76, 1637.72, 1646.74, 1655.81, 1663.61, 1670.54, 1677.84, 
   1684.82, 1788.87, 2010.13, 2105.38, 2159.58, 2218.67, 2332.05, 2452.11, 
   2522.52
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.54, 0.71, 0.74, 0.76, 0.77, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          263.230        start      
        1            56         0.00678       0.29786     
        2            57       2.054e-005     3.373e-004   
        3            58       1.851e-009     1.216e-006   
        4            59       8.294e-011     8.135e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2874.42 psi
   
   Pressure: 
   1529.03, 1535.87, 1544.13, 1553.41, 1561.75, 1568.45, 1576.97, 1587.95, 
   1598.35, 1619.37, 1639.60, 1648.74, 1657.94, 1665.85, 1673.08, 1697.52, 
   1754.37, 1921.33, 2129.60, 2220.62, 2272.95, 2330.30, 2440.77, 2558.19, 
   2627.36
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.46, 
   0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          254.988        start      
        1            56         0.00575       0.29813     
        2            57       2.111e-005     2.343e-004   
        3            58       3.408e-009     9.431e-007   
        4            59       6.596e-011     9.788e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2943.94 psi
   
   Pressure: 
   1529.00, 1535.84, 1544.09, 1553.35, 1561.69, 1568.38, 1576.89, 1587.86, 
   1598.25, 1619.25, 1639.46, 1648.59, 1657.86, 1675.79, 1736.16, 1799.15, 
   1852.72, 2010.97, 2210.79, 2298.73, 2349.55, 2405.41, 2513.46, 2628.73, 
   2696.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          235.941        start      
        1            56         0.00380       0.49292     
        2            57       1.411e-005     1.656e-004   
        3            58       1.446e-008     4.753e-007   
        4            59       9.645e-011     6.219e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3036.67 psi
   
   Pressure: 
   1528.87, 1535.68, 1543.90, 1553.12, 1561.42, 1568.08, 1576.55, 1587.48, 
   1597.82, 1618.73, 1638.91, 1654.05, 1729.75, 1797.24, 1855.27, 1914.71, 
   1966.03, 2118.91, 2312.79, 2398.42, 2448.04, 2502.72, 2608.80, 2722.33, 
   2789.77
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.32, 0.65, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          215.746        start      
        1            56         0.00288       1.72106     
        2            57       4.244e-006     1.936e-004   
        3            58       4.317e-009     2.349e-007   
        4            59       5.711e-011     2.400e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3188.44 psi
   
   Pressure: 
   1528.17, 1534.81, 1542.82, 1551.82, 1559.91, 1566.42, 1574.68, 1585.34, 
   1595.44, 1621.17, 1764.09, 1840.98, 1915.79, 1978.48, 2033.25, 2089.91, 
   2139.17, 2286.60, 2474.26, 2557.41, 2605.76, 2659.21, 2763.24, 2875.02, 
   2941.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.26, 0.61, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          230.822        start      
        1            56         0.00613       0.92704     
        2            57       1.066e-005     3.493e-004   
        3            58       7.141e-009     1.034e-006   
        4            59       7.705e-011     4.591e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3399.26 psi
   
   Pressure: 
   1528.60, 1535.34, 1543.47, 1552.61, 1560.82, 1567.42, 1575.81, 1587.56, 
   1643.27, 1824.41, 1992.23, 2066.00, 2138.80, 2200.31, 2254.26, 2310.17, 
   2358.84, 2504.56, 2690.08, 2772.29, 2820.10, 2872.96, 2975.90, 3086.59, 
   3152.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 
   0.55, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          253.656        start      
        1            56         0.00720       0.53625     
        2            57       2.627e-005     3.992e-004   
        3            58       1.444e-009     1.699e-006   
        4            59       9.362e-011     7.431e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3504.06 psi
   
   Pressure: 
   1528.80, 1535.59, 1543.78, 1552.97, 1561.24, 1568.03, 1594.96, 1689.94, 
   1777.28, 1949.34, 2111.32, 2183.32, 2254.74, 2315.25, 2368.42, 2423.58, 
   2471.64, 2615.62, 2799.02, 2880.33, 2927.65, 2980.00, 3082.04, 3191.89, 
   3257.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.45, 0.69, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          247.388        start      
        1            56         0.00436       0.38625     
        2            57       1.553e-005     1.765e-004   
        3            58       7.145e-009     7.661e-007   
        4            59       6.472e-011     2.262e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3578.18 psi
   
   Pressure: 
   1528.87, 1535.68, 1543.89, 1553.15, 1569.38, 1625.87, 1698.33, 1789.16, 
   1873.05, 2040.07, 2198.42, 2269.06, 2339.29, 2398.88, 2451.30, 2505.73, 
   2553.19, 2695.48, 2876.84, 2957.31, 3004.16, 3056.05, 3157.29, 3266.42, 
   3331.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          217.448        start      
        1            56         0.00259       0.81212     
        2            57       7.163e-006     1.607e-004   
        3            58       1.654e-008     2.138e-007   
        4            59       5.652e-011     5.694e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3658.68 psi
   
   Pressure: 
   1528.58, 1535.32, 1546.33, 1614.30, 1685.36, 1740.54, 1809.12, 1896.20, 
   1977.42, 2139.79, 2294.23, 2363.29, 2432.07, 2490.52, 2541.99, 2595.51, 
   2642.23, 2782.45, 2961.38, 3040.86, 3087.19, 3138.57, 3238.95, 3347.32, 
   3412.38
   
   Saturation:
   0.20, 0.20, 0.28, 0.62, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          226.113        start      
        1            56         0.00426       0.74283     
        2            57       3.122e-006     1.652e-004   
        3            58       4.196e-009     5.378e-007   
        4            59       4.813e-011     1.923e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3742.75 psi
   
   Pressure: 
   1532.44, 1572.75, 1643.56, 1720.78, 1788.49, 1841.93, 1908.95, 1994.44, 
   2074.44, 2234.63, 2387.20, 2455.49, 2523.54, 2581.40, 2632.38, 2685.41, 
   2731.71, 2870.76, 3048.24, 3127.11, 3173.11, 3224.14, 3323.91, 3431.73, 
   3496.52
   
   Saturation:
   0.23, 0.57, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          52.9094        start      
        1            56         0.00160       1.62908     
        2            57       1.674e-006      0.00106     
        3            58       5.765e-010     3.515e-007   
        4            59       3.443e-011     1.372e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3946.67 psi
   
   Pressure: 
   1751.44, 1808.76, 1876.09, 1950.42, 2016.26, 2068.48, 2134.17, 2218.07, 
   2296.67, 2454.16, 2604.24, 2671.45, 2738.47, 2795.47, 2845.72, 2898.01, 
   2943.71, 3080.98, 3256.32, 3334.29, 3379.80, 3430.33, 3529.24, 3636.25, 
   3700.65
   
   Saturation:
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          9.77235        start      
        1            56       7.116e-004      2.41072     
        2            57       1.056e-006     9.943e-004   
        3            58       3.271e-010     4.437e-007   
        4            59       1.096e-011     2.252e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3939.36 psi
   
   Pressure: 
   1743.03, 1799.14, 1865.88, 1939.95, 2005.79, 2058.14, 2124.04, 2208.28, 
   2287.21, 2445.36, 2596.06, 2663.52, 2730.76, 2787.93, 2838.31, 2890.71, 
   2936.47, 3073.89, 3249.32, 3327.30, 3372.80, 3423.29, 3522.10, 3628.99, 
   3693.33
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.65254        start      
        1            56       5.655e-004      3.85308     
        2            57       9.790e-007      0.00170     
        3            58       5.167e-010     7.339e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3915.35 psi
   
   Pressure: 
   1735.55, 1790.40, 1855.91, 1928.80, 1993.72, 2045.40, 2110.54, 2193.86, 
   2271.99, 2428.61, 2577.93, 2644.81, 2711.50, 2768.23, 2818.24, 2870.27, 
   2915.74, 3052.32, 3226.78, 3304.36, 3349.65, 3399.94, 3498.43, 3605.06, 
   3669.30
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.31621        start      
        1            56       6.625e-004      5.85449     
        2            57       1.530e-006      0.00358     
        3            58       1.186e-009     1.979e-006   
        4            59       5.411e-012     2.508e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3897.47 psi
   
   Pressure: 
   1731.24, 1785.26, 1849.90, 1921.90, 1986.10, 2037.25, 2101.77, 2184.32, 
   2261.78, 2417.11, 2565.26, 2631.64, 2697.85, 2754.19, 2803.87, 2855.59, 
   2900.79, 3036.65, 3210.23, 3287.45, 3332.56, 3382.67, 3480.86, 3587.25, 
   3651.39
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.62158        start      
        1            56       4.808e-004      6.20317     
        2            57       1.103e-006      0.00354     
        3            58       7.996e-010     2.019e-006   
        4            59       6.151e-012     1.945e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3882.59 psi
   
   Pressure: 
   1728.17, 1781.55, 1845.49, 1916.78, 1980.38, 2031.08, 2095.05, 2176.94, 
   2253.80, 2408.00, 2555.10, 2621.04, 2686.83, 2742.82, 2792.21, 2843.64, 
   2888.61, 3023.79, 3196.59, 3273.49, 3318.42, 3368.37, 3466.29, 3572.45, 
   3636.50
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.15402        start      
        1            56       4.153e-004      6.78634     
        2            57       1.022e-006      0.00388     
        3            58       7.402e-010     2.412e-006   
        4            59       6.189e-012     2.139e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3869.62 psi
   
   Pressure: 
   1725.73, 1778.59, 1841.95, 1912.61, 1975.69, 2025.99, 2089.49, 2170.79, 
   2247.12, 2400.29, 2546.46, 2611.99, 2677.39, 2733.07, 2782.19, 2833.36, 
   2878.11, 3012.69, 3184.75, 3261.36, 3306.14, 3355.93, 3453.60, 3559.55, 
   3623.52
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.79586        start      
        1            56       3.939e-004      7.53881     
        2            57       1.075e-006      0.00445     
        3            58       8.168e-010     3.057e-006   
        4            59       5.106e-012     2.821e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3858.03 psi
   
   Pressure: 
   1723.68, 1776.10, 1838.94, 1909.07, 1971.68, 2021.62, 2084.69, 2165.45, 
   2241.31, 2393.54, 2538.85, 2604.01, 2669.05, 2724.44, 2773.33, 2824.25, 
   2868.80, 3002.82, 3174.21, 3250.54, 3295.18, 3344.83, 3442.27, 3548.02, 
   3611.91
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/27/2026 12:04:03 AM
   7/27/2026 12:05:43 AM
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
        0            1          338.926        start      
        1            56         0.00493       0.72528     
        2            57       3.860e-006     3.488e-004   
        3            58       2.474e-009     4.011e-007   
        4            59       1.351e-010     1.401e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2593.49 psi
   
   Pressure: 
   1538.64, 1547.76, 1558.76, 1571.12, 1582.23, 1591.15, 1602.50, 1617.14, 
   1630.99, 1658.99, 1685.93, 1698.11, 1710.36, 1720.89, 1730.26, 1740.11, 
   1748.81, 1775.29, 1809.52, 1824.94, 1834.07, 1845.33, 1960.38, 2154.88, 
   2263.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.56, 0.73, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          341.469        start      
        1            56         0.00511       0.32292     
        2            57       1.394e-005     2.346e-004   
        3            58       2.676e-009     6.189e-007   
        4            59       1.118e-010     1.492e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2768.45 psi
   
   Pressure: 
   1538.75, 1547.89, 1558.92, 1571.31, 1582.45, 1591.40, 1602.78, 1617.46, 
   1631.35, 1659.42, 1686.44, 1698.65, 1710.94, 1721.50, 1730.90, 1740.78, 
   1749.51, 1776.07, 1810.57, 1840.73, 1919.06, 2006.41, 2170.07, 2340.41, 
   2438.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.36, 0.67, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          322.274        start      
        1            56         0.00752       1.51451     
        2            57       1.098e-005     6.790e-004   
        3            58       5.014e-009     1.279e-006   
        4            59       1.158e-010     3.437e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3184.03 psi
   
   Pressure: 
   1538.05, 1547.02, 1557.86, 1570.02, 1580.96, 1589.74, 1600.91, 1615.31, 
   1628.94, 1656.49, 1682.99, 1694.97, 1707.03, 1717.38, 1726.59, 1736.28, 
   1745.50, 1880.30, 2174.16, 2300.63, 2372.59, 2451.08, 2601.73, 2761.29, 
   2854.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.54, 0.71, 0.74, 0.76, 0.77, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          307.554        start      
        1            56         0.00310       0.39686     
        2            57       9.239e-006     1.095e-004   
        3            58       8.629e-009     2.130e-007   
        4            59       1.106e-010     2.535e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3362.80 psi
   
   Pressure: 
   1538.58, 1547.68, 1558.67, 1571.00, 1582.09, 1591.00, 1602.33, 1616.94, 
   1630.77, 1658.72, 1685.63, 1697.79, 1710.03, 1720.57, 1734.81, 1813.79, 
   1888.31, 2105.56, 2377.98, 2497.40, 2566.20, 2641.69, 2787.35, 2942.40, 
   3033.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.64, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          345.823        start      
        1            56         0.00730       0.56253     
        2            57       1.922e-005     3.799e-004   
        3            58       4.371e-009     1.109e-006   
        4            59       1.618e-010     2.343e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3490.77 psi
   
   Pressure: 
   1538.45, 1547.52, 1558.47, 1570.76, 1581.81, 1590.68, 1601.97, 1616.52, 
   1630.29, 1658.13, 1684.91, 1697.36, 1739.05, 1830.38, 1909.20, 1989.47, 
   2058.56, 2263.92, 2524.07, 2638.84, 2705.30, 2778.50, 2920.35, 3072.03, 
   3162.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.46, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          285.924        start      
        1            56         0.00334       1.94469     
        2            57       2.332e-006     2.248e-004   
        3            58       1.730e-009     1.626e-007   
        4            59       1.106e-010     8.662e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3730.11 psi
   
   Pressure: 
   1537.17, 1545.93, 1556.51, 1568.38, 1579.06, 1587.64, 1598.55, 1612.62, 
   1625.94, 1658.92, 1841.88, 1943.51, 2042.34, 2125.18, 2197.59, 2272.52, 
   2337.71, 2532.90, 2781.46, 2891.64, 2955.74, 3026.64, 3164.71, 3313.13, 
   3401.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.25, 0.60, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          340.200        start      
        1            56         0.00452       0.57965     
        2            57       1.269e-005     2.099e-004   
        3            58       1.442e-009     5.931e-007   
        4            59       1.350e-010     8.155e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4071.53 psi
   
   Pressure: 
   1538.30, 1547.33, 1558.23, 1570.47, 1581.47, 1590.31, 1601.62, 1630.63, 
   1749.13, 1986.84, 2207.59, 2305.02, 2401.34, 2482.75, 2554.18, 2628.21, 
   2692.65, 2885.56, 3131.10, 3239.90, 3303.17, 3373.14, 3509.40, 3655.97, 
   3743.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 
   0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          337.706        start      
        1            56         0.00653       0.50654     
        2            57       1.519e-005     2.884e-004   
        3            58       2.878e-009     8.638e-007   
        4            59       1.330e-010     1.328e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4226.96 psi
   
   Pressure: 
   1538.34, 1547.38, 1558.29, 1570.54, 1582.09, 1619.32, 1717.63, 1840.62, 
   1953.38, 2177.27, 2389.09, 2483.48, 2577.26, 2656.78, 2726.71, 2799.30, 
   2862.57, 3052.20, 3293.81, 3400.98, 3463.37, 3532.43, 3667.13, 3812.27, 
   3899.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          288.386        start      
        1            56         0.00278       0.92696     
        2            57       3.509e-006     1.284e-004   
        3            58       6.827e-009     1.099e-007   
        4            59       1.121e-010     2.030e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4357.77 psi
   
   Pressure: 
   1537.85, 1546.78, 1560.42, 1645.68, 1740.09, 1813.32, 1904.27, 2019.74, 
   2127.42, 2342.64, 2547.34, 2638.87, 2730.04, 2807.52, 2875.76, 2946.72, 
   3008.67, 3194.62, 3431.89, 3537.29, 3598.74, 3666.89, 3800.05, 3943.83, 
   4030.16
   
   Saturation:
   0.20, 0.20, 0.26, 0.60, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          330.992        start      
        1            56         0.00720       1.15750     
        2            57       1.734e-005      0.00150     
        3            58       1.974e-008     4.981e-006   
        4            59       8.900e-011     5.540e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1581.14, 1657.28, 1748.65, 1848.66, 1936.77, 2006.50, 2094.10, 2205.95, 
   2310.70, 2520.54, 2720.48, 2809.99, 2899.23, 2975.11, 3041.98, 3111.55, 
   3172.32, 3354.78, 3587.68, 3691.17, 3751.53, 3818.48, 3949.40, 4090.86, 
   4175.89
   
   Saturation:
   0.38, 0.68, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          19.8443        start      
        1            56         0.00100       0.75835     
        2            57       1.458e-006     8.351e-004   
        3            58       1.283e-010     8.324e-007   
        4            59       1.694e-011     6.529e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1804.50, 1874.17, 1956.67, 2048.03, 2129.10, 2193.48, 2274.47, 2377.93, 
   2474.84, 2668.94, 2853.83, 2936.58, 3019.05, 3089.16, 3150.92, 3215.16, 
   3271.26, 3439.72, 3654.76, 3750.32, 3806.08, 3867.96, 3989.03, 4119.99, 
   4198.79
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.56876        start      
        1            56       6.982e-004      1.24828     
        2            57       1.209e-006      0.00131     
        3            58       3.953e-010     1.360e-006   
        4            59       1.032e-011     3.522e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1793.70, 1862.01, 1943.52, 2034.16, 2114.85, 2179.07, 2259.98, 2363.46, 
   2460.48, 2654.93, 2840.28, 2923.29, 3006.06, 3076.46, 3138.52, 3203.10, 
   3259.52, 3429.03, 3645.52, 3741.79, 3797.99, 3860.40, 3982.61, 4114.92, 
   4194.63
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.36559        start      
        1            56       7.178e-004      1.59719     
        2            57       1.482e-006      0.00206     
        3            58       5.629e-010     2.711e-006   
        4            59       6.911e-012     8.248e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1789.42, 1857.04, 1937.93, 2028.03, 2108.37, 2172.37, 2253.09, 2356.37, 
   2453.27, 2647.57, 2832.87, 2915.89, 2998.71, 3069.18, 3131.33, 3196.02, 
   3252.58, 3422.54, 3639.70, 3736.31, 3792.75, 3855.45, 3978.33, 4111.47, 
   4191.76
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.35067        start      
        1            56       5.227e-004      1.54398     
        2            57       9.668e-007      0.00177     
        3            58       2.924e-010     2.184e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1786.96, 1854.12, 1934.57, 2024.26, 2104.29, 2168.09, 2248.60, 2351.66, 
   2448.40, 2642.44, 2827.56, 2910.53, 2993.33, 3063.81, 3125.98, 3190.73, 
   3247.35, 3417.57, 3635.15, 3731.99, 3788.59, 3851.51, 3974.88, 4108.66, 
   4189.41
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.75423        start      
        1            56       7.685e-004      2.01951     
        2            57       1.829e-006      0.00309     
        3            58       6.795e-010     5.171e-006   
        4            59       5.828e-012     1.527e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1785.24, 1852.06, 1932.17, 2021.52, 2101.29, 2164.91, 2245.22, 2348.06, 
   2444.63, 2638.39, 2823.30, 2906.20, 2988.95, 3059.41, 3121.59, 3186.36, 
   3243.02, 3413.42, 3631.30, 3728.31, 3785.04, 3848.13, 3971.91, 4106.23, 
   4187.37
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.27327        start      
        1            56       5.059e-004      1.74113     
        2            57       9.702e-007      0.00210     
        3            58       2.650e-010     2.887e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1783.91, 1850.46, 1930.27, 2019.34, 2098.88, 2162.34, 2242.47, 2345.11, 
   2441.51, 2634.99, 2819.68, 2902.50, 2985.20, 3055.63, 3117.80, 3182.58, 
   3239.26, 3409.80, 3627.91, 3725.06, 3781.90, 3845.13, 3969.27, 4104.06, 
   4185.54
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.93018        start      
        1            56       5.717e-004      1.93407     
        2            57       1.182e-006      0.00252     
        3            58       3.473e-010     3.848e-006   
        4            59       9.157e-012     8.412e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1782.81, 1849.13, 1928.70, 2017.51, 2096.84, 2160.15, 2240.12, 2342.57, 
   2438.82, 2632.03, 2816.50, 2899.25, 2981.89, 3052.29, 3114.44, 3179.22, 
   3235.92, 3406.56, 3624.87, 3722.14, 3779.07, 3842.43, 3966.88, 4102.09, 
   4183.88
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.66579        start      
        1            56       6.850e-004      2.19173     
        2            57       1.562e-006      0.00318     
        3            58       5.264e-010     5.455e-006   
        4            59       8.915e-012     1.435e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1781.87, 1847.99, 1927.33, 2015.91, 2095.06, 2158.24, 2238.05, 2340.33, 
   2436.43, 2629.38, 2813.65, 2896.32, 2978.91, 3049.27, 3111.41, 3176.19, 
   3232.90, 3403.62, 3622.10, 3719.48, 3776.49, 3839.97, 3964.70, 4100.29, 
   4182.35
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.45289        start      
        1            56       5.501e-004      2.02204     
        2            57       1.115e-006      0.00257     
        3            58       3.308e-010     3.938e-006   
        4            59       8.189e-012     8.746e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1781.04, 1846.98, 1926.12, 2014.50, 2093.48, 2156.53, 2236.20, 2338.31, 
   2434.28, 2626.99, 2811.06, 2893.66, 2976.19, 3046.52, 3108.64, 3173.41, 
   3230.13, 3400.92, 3619.56, 3717.03, 3774.11, 3837.70, 3962.69, 4098.62, 
   4180.94
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.25348        start      
        1            56       6.858e-004      2.32193     
        2            57       1.556e-006      0.00333     
        3            58       5.582e-010     5.784e-006   
        4            59       1.184e-011     1.640e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1780.30, 1846.07, 1925.03, 2013.22, 2092.04, 2154.98, 2234.52, 2336.47, 
   2432.31, 2624.80, 2808.68, 2891.22, 2973.69, 3043.98, 3106.09, 3170.85, 
   3227.58, 3398.44, 3617.20, 3714.76, 3771.91, 3835.59, 3960.82, 4097.08, 
   4179.63
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/27/2026 12:06:53 AM
   7/27/2026 12:08:34 AM
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
        0            1          624.151        start      
        1            56         0.00792       1.43326     
        2            57       3.662e-006     4.259e-004   
        3            58       1.037e-009     2.467e-007   
        4            59       3.296e-010     3.789e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1578.52, 1597.05, 1619.39, 1644.46, 1666.97, 1685.04, 1707.98, 1737.53, 
   1765.45, 1821.80, 1875.90, 1900.30, 1924.79, 1945.76, 1964.38, 1983.90, 
   2001.09, 2059.06, 2448.09, 2711.05, 2859.46, 3020.62, 3328.99, 3654.56, 
   3844.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.23, 0.56, 0.71, 0.75, 0.76, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          472.905        start      
        1            56         0.00437       0.47938     
        2            57       1.170e-006     1.043e-004   
        3            58       8.914e-010     7.956e-008   
        4            59       2.164e-010     3.668e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1561.05, 1575.46, 1592.84, 1612.36, 1629.91, 1644.00, 1661.92, 1685.01, 
   1706.87, 1751.05, 1793.54, 1812.74, 1832.07, 1851.49, 1946.96, 2081.63, 
   2195.72, 2531.52, 2954.81, 3140.97, 3248.50, 3366.70, 3595.21, 3838.80, 
   3982.71
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.71, 
   0.74, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          482.463        start      
        1            56         0.00816       0.92385     
        2            57       1.357e-005     5.658e-004   
        3            58       2.978e-009     9.324e-007   
        4            59       1.944e-010     1.849e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1553.08, 1565.59, 1580.70, 1597.66, 1612.91, 1625.15, 1640.71, 1660.77, 
   1679.74, 1718.73, 1827.44, 1973.26, 2116.00, 2234.93, 2338.56, 2445.58, 
   2538.52, 2816.41, 3169.71, 3326.05, 3416.82, 3517.03, 3711.75, 3920.48, 
   4044.62
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.44, 0.69, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          410.881        start      
        1            56         0.00579       0.36331     
        2            57       1.230e-005     2.896e-004   
        3            58       1.226e-009     6.458e-007   
        4            59       1.664e-010     5.667e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1544.55, 1555.06, 1567.74, 1581.98, 1594.78, 1605.06, 1618.31, 1661.99, 
   1801.37, 2076.41, 2332.36, 2445.47, 2557.37, 2652.01, 2735.09, 2821.24, 
   2896.29, 3121.10, 3407.38, 3534.26, 3608.04, 3689.65, 3848.57, 4019.45, 
   4121.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          324.501        start      
        1            56         0.00315       0.41365     
        2            57       8.923e-006     1.250e-004   
        3            58       9.746e-009     2.296e-007   
        4            59       1.244e-010     3.279e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1541.69, 1551.53, 1563.40, 1576.74, 1593.55, 1668.68, 1774.26, 1906.26, 
   2027.94, 2270.01, 2499.42, 2601.76, 2703.53, 2789.91, 2865.92, 2944.90, 
   3013.80, 3220.50, 3484.03, 3600.97, 3669.08, 3744.50, 3891.66, 4050.22, 
   4145.11
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 0.74, 
   0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          345.747        start      
        1            56         0.00806       0.50823     
        2            57       2.227e-005     5.073e-004   
        3            58       1.306e-008     2.019e-006   
        4            59       1.112e-010     1.155e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1539.33, 1548.86, 1587.47, 1697.38, 1793.60, 1868.71, 1962.40, 2081.59, 
   2192.90, 2415.55, 2627.42, 2722.20, 2816.62, 2896.88, 2967.59, 3041.13, 
   3105.36, 3298.17, 3544.22, 3653.50, 3717.21, 3787.84, 3925.83, 4074.75, 
   4164.05
   
   Saturation:
   0.20, 0.21, 0.47, 0.69, 0.73, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          236.591        start      
        1            56         0.00448       2.30048     
        2            57       2.418e-006      0.00131     
        3            58       4.055e-009     8.346e-007   
        4            59       9.718e-011     1.154e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1728.37, 1799.83, 1883.51, 1975.78, 2057.56, 2122.56, 2204.49, 2309.39, 
   2407.94, 2605.83, 2794.85, 2879.66, 2964.37, 3036.55, 3100.28, 3166.71, 
   3224.83, 3399.71, 3623.27, 3722.73, 3780.81, 3845.30, 3971.51, 4107.98, 
   4189.97
   
   Saturation:
   0.59, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          13.5834        start      
        1            56       8.436e-004      0.89427     
        2            57       1.264e-006     8.871e-004   
        3            58       2.240e-010     8.393e-007   
        4            59       1.256e-011     1.158e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1799.29, 1868.23, 1950.14, 2040.99, 2121.74, 2185.93, 2266.76, 2370.10, 
   2466.97, 2661.13, 2846.20, 2929.09, 3011.76, 3082.07, 3144.07, 3208.60, 
   3264.99, 3434.42, 3650.82, 3747.04, 3803.21, 3865.55, 3987.58, 4119.57, 
   4198.93
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.75006        start      
        1            56       7.212e-004      1.30425     
        2            57       1.355e-006      0.00154     
        3            58       4.737e-010     1.792e-006   
        4            59       8.129e-012     5.074e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1791.28, 1859.14, 1940.22, 2030.46, 2110.86, 2174.88, 2255.62, 2358.92, 
   2455.84, 2650.19, 2835.55, 2918.61, 3001.48, 3072.01, 3134.22, 3198.98, 
   3255.61, 3425.80, 3643.26, 3739.99, 3796.48, 3859.23, 3982.13, 4115.17, 
   4195.26
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.03798        start      
        1            56       5.700e-004      1.39581     
        2            57       1.064e-006      0.00165     
        3            58       3.410e-010     2.021e-006   
        4            59       7.658e-012     4.799e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1787.78, 1855.07, 1935.63, 2025.40, 2105.49, 2169.33, 2249.89, 2353.02, 
   2449.82, 2644.02, 2829.31, 2912.38, 2995.28, 3065.85, 3128.12, 3192.97, 
   3249.69, 3420.21, 3638.16, 3735.16, 3791.83, 3854.82, 3978.25, 4111.98, 
   4192.55
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.16178        start      
        1            56       6.853e-004      1.70469     
        2            57       1.511e-006      0.00247     
        3            58       5.130e-010     3.808e-006   
        4            59       7.799e-012     1.003e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1785.69, 1852.58, 1932.76, 2022.18, 2102.01, 2165.68, 2246.05, 2348.98, 
   2445.64, 2639.61, 2824.73, 2907.74, 2990.62, 3061.19, 3123.47, 3188.35, 
   3245.11, 3415.83, 3634.08, 3731.25, 3788.05, 3851.21, 3975.05, 4109.31, 
   4190.28
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.59693        start      
        1            56       6.570e-004      1.79306     
        2            57       1.449e-006      0.00262     
        3            58       4.482e-010     4.207e-006   
        4            59       5.103e-012     9.986e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1784.20, 1850.80, 1930.68, 2019.81, 2099.41, 2162.91, 2243.12, 2345.85, 
   2442.36, 2636.07, 2821.00, 2903.94, 2986.77, 3057.31, 3119.59, 3184.48, 
   3241.27, 3412.10, 3630.57, 3727.87, 3784.78, 3848.07, 3972.25, 4106.97, 
   4188.27
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.20501        start      
        1            56       7.183e-004      1.97256     
        2            57       1.677e-006      0.00307     
        3            58       5.307e-010     5.395e-006   
        4            59       9.008e-012     1.339e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1783.03, 1849.40, 1929.02, 2017.90, 2097.29, 2160.66, 2240.70, 2343.26, 
   2439.62, 2633.07, 2817.79, 2900.66, 2983.43, 3053.94, 3116.21, 3181.10, 
   3237.89, 3408.81, 3627.45, 3724.86, 3781.85, 3845.26, 3969.74, 4104.86, 
   4186.47
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.92339        start      
        1            56       5.531e-004      1.79747     
        2            57       1.120e-006      0.00239     
        3            58       2.928e-010     3.693e-006   
        4            59       9.610e-012     7.175e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1782.07, 1848.23, 1927.63, 2016.28, 2095.49, 2158.73, 2238.62, 2341.01, 
   2437.23, 2630.43, 2814.95, 2897.75, 2980.46, 3050.93, 3113.17, 3178.06, 
   3234.86, 3405.84, 3624.62, 3722.12, 3779.19, 3842.71, 3967.45, 4102.93, 
   4184.82
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.66273        start      
        1            56       6.658e-004      2.04449     
        2            57       1.488e-006      0.00302     
        3            58       4.445e-010     5.258e-006   
        4            59       6.471e-012     1.224e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1781.23, 1847.22, 1926.42, 2014.86, 2093.91, 2157.03, 2236.78, 2339.00, 
   2435.09, 2628.06, 2812.38, 2895.10, 2977.75, 3048.19, 3110.41, 3175.28, 
   3232.09, 3403.12, 3622.03, 3719.61, 3776.74, 3840.36, 3965.34, 4101.16, 
   4183.30
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.45951        start      
        1            56       5.408e-004      1.89795     
        2            57       1.077e-006      0.00246     
        3            58       2.847e-010     3.849e-006   
        4            59       9.322e-012     7.650e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1780.49, 1846.31, 1925.34, 2013.60, 2092.49, 2155.49, 2235.12, 2337.19, 
   2433.15, 2625.89, 2810.02, 2892.68, 2975.27, 3045.67, 3107.86, 3172.72, 
   3229.53, 3400.61, 3619.62, 3717.28, 3774.47, 3838.18, 3963.39, 4099.51, 
   4181.88
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.26773        start      
        1            56       6.817e-004      2.19378     
        2            57       1.526e-006      0.00323     
        3            58       4.886e-010     5.739e-006   
        4            59       4.873e-012     1.463e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1779.82, 1845.49, 1924.36, 2012.44, 2091.20, 2154.09, 2233.60, 2335.53, 
   2431.37, 2623.89, 2807.85, 2890.43, 2972.96, 3043.33, 3105.50, 3170.35, 
   3227.16, 3398.27, 3617.39, 3715.11, 3772.36, 3836.15, 3961.56, 4097.98, 
   4180.57
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.11603        start      
        1            56       5.733e-004      2.06126     
        2            57       1.165e-006      0.00271     
        3            58       3.419e-010     4.406e-006   
        4            59       7.791e-012     1.002e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1779.20, 1844.74, 1923.45, 2011.38, 2090.00, 2152.80, 2232.19, 2333.99, 
   2429.72, 2622.03, 2805.81, 2888.33, 2970.81, 3041.14, 3103.29, 3168.13, 
   3224.93, 3396.08, 3615.29, 3713.08, 3770.38, 3834.24, 3959.86, 4096.54, 
   4179.33
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.98159        start      
        1            56       7.557e-004      2.42006     
        2            57       1.760e-006      0.00370     
        3            58       6.542e-010     6.921e-006   
        4            59       1.068e-011     2.108e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1778.63, 1844.05, 1922.62, 2010.40, 2088.89, 2151.60, 2230.88, 2332.55, 
   2428.17, 2620.29, 2803.91, 2886.37, 2968.79, 3039.09, 3101.22, 3166.04, 
   3222.85, 3394.03, 3613.32, 3711.17, 3768.52, 3832.45, 3958.25, 4095.19, 
   4178.18
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.83371        start      
        1            56       6.257e-004      2.25993     
        2            57       1.312e-006      0.00306     
        3            58       4.491e-010     5.175e-006   
        4            59       5.998e-012     1.414e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1778.10, 1843.40, 1921.84, 2009.47, 2087.85, 2150.47, 2229.66, 2331.20, 
   2426.72, 2618.66, 2802.12, 2884.52, 2966.90, 3037.15, 3099.26, 3164.08, 
   3220.88, 3392.09, 3611.47, 3709.37, 3766.77, 3830.77, 3956.74, 4093.92, 
   4177.08
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/27/2026 12:09:52 AM
   7/27/2026 12:11:32 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

