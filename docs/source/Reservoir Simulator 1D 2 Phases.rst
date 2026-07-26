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
        0            1          196.007        start      
        1            56         0.00249       5.61071     
        2            57       2.109e-006     6.203e-004   
        3            58       4.362e-009     9.044e-008   
        4            59       7.466e-011     3.290e-010   
   Producer BHP: 
   2278.05 psi
   
   Injector BHP: 
   2635.30 psi
   
   Pressure: 
   2301.08, 2305.89, 2312.19, 2320.26, 2329.56, 2340.95, 2351.78, 2361.94, 
   2371.29, 2379.27, 2386.29, 2393.77, 2401.67, 2407.18, 2412.91, 2418.94, 
   2424.39, 2430.37, 2436.46, 2442.60, 2450.15, 2457.04, 2464.80, 2482.45, 
   2520.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          179.125        start      
        1            56         0.00312       5.98009     
        2            57       1.753e-006     5.714e-004   
        3            58       2.789e-009     8.917e-008   
        4            59       6.650e-011     2.287e-010   
   Producer BHP: 
   1627.49 psi
   
   Injector BHP: 
   2052.76 psi
   
   Pressure: 
   1650.58, 1655.39, 1661.70, 1669.77, 1679.06, 1690.42, 1701.23, 1711.36, 
   1720.67, 1728.61, 1735.59, 1743.02, 1750.86, 1756.32, 1762.00, 1767.97, 
   1773.35, 1779.26, 1785.26, 1791.31, 1798.74, 1805.88, 1839.02, 1902.87, 
   1937.51
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.51, 0.71, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.663        start      
        1            56         0.00266       0.35342     
        2            57       2.107e-006     7.612e-005   
        3            58       2.741e-009     6.477e-008   
        4            59       3.729e-011     8.844e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1950.82 psi
   
   Pressure: 
   1516.63, 1520.14, 1524.80, 1530.82, 1537.85, 1546.54, 1554.92, 1562.86, 
   1570.26, 1576.65, 1582.34, 1588.48, 1595.06, 1599.71, 1604.61, 1609.84, 
   1614.62, 1619.94, 1625.43, 1631.04, 1639.44, 1682.22, 1745.18, 1803.33, 
   1835.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.865        start      
        1            56         0.00348       0.27534     
        2            57       1.776e-005     2.397e-004   
        3            58       4.271e-008     5.587e-007   
        4            59       9.868e-011     1.735e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2006.88 psi
   
   Pressure: 
   1516.65, 1520.16, 1524.82, 1530.85, 1537.88, 1546.58, 1554.96, 1562.91, 
   1570.31, 1576.71, 1582.41, 1588.55, 1595.14, 1599.79, 1604.70, 1609.93, 
   1614.71, 1620.04, 1625.55, 1634.09, 1691.29, 1746.53, 1805.54, 1860.65, 
   1891.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.30, 0.65, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.752        start      
        1            56         0.00619       0.24481     
        2            57       3.074e-005     2.741e-004   
        3            58       1.782e-008     1.616e-006   
        4            59       4.228e-011     6.426e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2046.12 psi
   
   Pressure: 
   1516.66, 1520.17, 1524.83, 1530.86, 1537.89, 1546.59, 1554.97, 1562.92, 
   1570.32, 1576.72, 1582.42, 1588.57, 1595.15, 1599.80, 1604.71, 1609.94, 
   1614.72, 1620.09, 1631.94, 1679.62, 1738.76, 1790.99, 1847.50, 1900.70, 
   1930.81
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.38, 0.67, 0.74, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.902        start      
        1            56         0.00782       0.34077     
        2            57       3.234e-005     4.742e-004   
        3            58       5.323e-009     2.486e-006   
        4            59       4.588e-011     3.597e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2085.95 psi
   
   Pressure: 
   1516.64, 1520.15, 1524.80, 1530.83, 1537.85, 1546.54, 1554.91, 1562.85, 
   1570.24, 1576.64, 1582.33, 1588.46, 1595.04, 1599.68, 1604.58, 1609.81, 
   1614.74, 1632.90, 1680.59, 1727.81, 1784.16, 1834.45, 1889.24, 1941.11, 
   1970.64
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.47, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.899        start      
        1            56         0.00595       0.47081     
        2            57       9.541e-006     2.467e-004   
        3            58       1.033e-008     9.706e-007   
        4            59       4.465e-011     5.256e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2126.12 psi
   
   Pressure: 
   1516.61, 1520.11, 1524.76, 1530.77, 1537.78, 1546.45, 1554.81, 1562.73, 
   1570.11, 1576.49, 1582.17, 1588.30, 1594.86, 1599.50, 1604.39, 1610.21, 
   1636.85, 1683.00, 1728.73, 1773.90, 1828.42, 1877.34, 1930.87, 1981.73, 
   2010.83
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 
   0.56, 0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.374        start      
        1            56         0.00301       0.41816     
        2            57       1.209e-005     1.611e-004   
        3            58       1.573e-008     3.745e-007   
        4            59       6.095e-011     4.115e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2165.93 psi
   
   Pressure: 
   1516.61, 1520.11, 1524.76, 1530.77, 1537.78, 1546.45, 1554.81, 1562.73, 
   1570.11, 1576.49, 1582.17, 1588.30, 1594.86, 1599.51, 1606.10, 1644.84, 
   1685.94, 1730.08, 1774.17, 1818.04, 1871.29, 1919.20, 1971.79, 2021.87, 
   2050.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.069        start      
        1            56         0.00397       0.22856     
        2            57       1.880e-005     1.467e-004   
        3            58       3.432e-008     6.162e-007   
        4            59       4.934e-011     1.560e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2201.67 psi
   
   Pressure: 
   1516.65, 1520.16, 1524.82, 1530.84, 1537.86, 1546.56, 1554.93, 1562.87, 
   1570.27, 1576.66, 1582.35, 1588.49, 1595.09, 1603.18, 1644.14, 1688.88, 
   1728.31, 1771.09, 1814.12, 1857.10, 1909.41, 1956.57, 2008.41, 2057.90, 
   2086.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.391        start      
        1            56         0.00549       0.33323     
        2            57       2.490e-005     2.964e-004   
        3            58       1.225e-008     1.558e-006   
        4            59       4.316e-011     5.050e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2233.06 psi
   
   Pressure: 
   1516.63, 1520.13, 1524.78, 1530.79, 1537.80, 1546.49, 1554.84, 1562.77, 
   1570.16, 1576.54, 1582.22, 1588.40, 1603.04, 1642.57, 1684.09, 1727.01, 
   1765.28, 1807.03, 1849.20, 1891.40, 1942.88, 1989.36, 2040.56, 2089.52, 
   2117.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.38, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          173.794        start      
        1            56         0.00656       0.40512     
        2            57       2.914e-005     4.002e-004   
        3            58       5.923e-009     2.115e-006   
        4            59       5.258e-011     3.521e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2278.90 psi
   
   Pressure: 
   1516.61, 1520.11, 1524.75, 1530.76, 1537.76, 1546.43, 1554.78, 1562.70, 
   1570.07, 1576.45, 1582.23, 1600.21, 1657.16, 1696.51, 1736.54, 1778.31, 
   1815.75, 1856.72, 1898.19, 1939.76, 1990.53, 2036.44, 2087.08, 2135.58, 
   2163.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.44, 0.69, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.043        start      
        1            56         0.00676       0.48470     
        2            57       2.646e-005     4.326e-004   
        3            58       3.272e-009     2.165e-006   
        4            59       3.157e-011     1.556e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2320.92 psi
   
   Pressure: 
   1516.59, 1520.08, 1524.72, 1530.72, 1537.71, 1546.36, 1554.70, 1562.60, 
   1569.97, 1576.62, 1598.40, 1651.64, 1706.93, 1744.89, 1783.95, 1824.88, 
   1861.68, 1902.01, 1942.89, 1983.92, 2034.10, 2079.51, 2129.67, 2177.78, 
   2205.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.49, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.394        start      
        1            56         0.00557       0.77863     
        2            57       9.589e-006     3.013e-004   
        3            58       9.459e-009     1.107e-006   
        4            59       4.564e-011     6.299e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2366.73 psi
   
   Pressure: 
   1516.51, 1519.99, 1524.60, 1530.57, 1537.52, 1546.14, 1554.43, 1562.30, 
   1570.44, 1605.86, 1655.01, 1706.26, 1759.61, 1796.58, 1834.83, 1875.02, 
   1911.22, 1950.94, 1991.26, 2031.77, 2081.37, 2126.32, 2176.02, 2223.76, 
   2251.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.23, 0.56, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.799        start      
        1            56         0.00297       0.85638     
        2            57       6.126e-006     1.281e-004   
        3            58       8.373e-009     2.502e-007   
        4            59       4.236e-011     3.713e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2421.12 psi
   
   Pressure: 
   1516.48, 1519.95, 1524.56, 1530.51, 1537.46, 1546.05, 1554.34, 1564.16, 
   1615.43, 1670.22, 1717.51, 1767.20, 1819.36, 1855.67, 1893.33, 1932.96, 
   1968.69, 2007.94, 2047.81, 2087.89, 2137.02, 2181.57, 2230.88, 2278.32, 
   2305.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 
   0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.431        start      
        1            56         0.00381       0.76935     
        2            57       1.860e-005     2.436e-004   
        3            58       2.852e-008     9.190e-007   
        4            59       5.420e-011     1.933e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2481.69 psi
   
   Pressure: 
   1516.50, 1519.97, 1524.58, 1530.54, 1537.49, 1546.12, 1559.18, 1622.05, 
   1685.23, 1738.06, 1784.10, 1832.84, 1884.20, 1920.03, 1957.26, 1996.46, 
   2031.83, 2070.70, 2110.21, 2149.94, 2198.67, 2242.89, 2291.88, 2339.04, 
   2366.54
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.64, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.915        start      
        1            56         0.00578       0.56866     
        2            57       2.769e-005     4.149e-004   
        3            58       1.671e-008     2.397e-006   
        4            59       6.467e-011     1.173e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2547.36 psi
   
   Pressure: 
   1516.55, 1520.04, 1524.66, 1530.64, 1537.66, 1556.85, 1628.63, 1696.33, 
   1757.42, 1809.10, 1854.38, 1902.50, 1953.31, 1988.79, 2025.69, 2064.56, 
   2099.64, 2138.21, 2177.43, 2216.88, 2265.29, 2309.23, 2357.93, 2404.85, 
   2432.25
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 0.68, 0.73, 
   0.75, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.393        start      
        1            56         0.00809       0.66284     
        2            57       3.861e-005     6.584e-004   
        3            58       1.761e-008     4.082e-006   
        4            59       3.258e-011     1.800e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2608.05 psi
   
   Pressure: 
   1516.52, 1520.00, 1524.61, 1530.74, 1554.24, 1629.40, 1699.86, 1765.16, 
   1824.75, 1875.48, 1920.05, 1967.52, 2017.71, 2052.80, 2089.32, 2127.81, 
   2162.57, 2200.81, 2239.70, 2278.85, 2326.90, 2370.56, 2418.98, 2465.66, 
   2492.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.46, 0.69, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.377        start      
        1            56         0.00628       0.65266     
        2            57       1.870e-005     4.568e-004   
        3            58       5.074e-009     2.367e-006   
        4            59       5.390e-011     8.236e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2656.03 psi
   
   Pressure: 
   1516.52, 1520.00, 1524.98, 1553.48, 1614.08, 1687.07, 1755.48, 1819.38, 
   1878.01, 1928.05, 1972.10, 2019.07, 2068.79, 2103.56, 2139.77, 2177.95, 
   2212.45, 2250.40, 2289.02, 2327.92, 2375.68, 2419.09, 2467.27, 2513.76, 
   2540.96
   
   Saturation:
   0.20, 0.20, 0.22, 0.53, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          144.797        start      
        1            56         0.00348       0.58143     
        2            57       4.612e-006     1.500e-004   
        3            58       1.077e-008     1.347e-007   
        4            59       4.346e-011     4.458e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2694.49 psi
   
   Pressure: 
   1516.55, 1520.76, 1551.55, 1603.50, 1662.02, 1733.00, 1800.16, 1863.11, 
   1921.01, 1970.52, 2014.14, 2060.69, 2109.99, 2144.49, 2180.42, 2218.33, 
   2252.59, 2290.29, 2328.67, 2367.34, 2414.84, 2458.03, 2506.00, 2552.31, 
   2579.43
   
   Saturation:
   0.20, 0.25, 0.59, 0.71, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.365        start      
        1            56         0.00286       0.61439     
        2            57       1.172e-005     2.828e-004   
        3            58       1.880e-008     1.425e-006   
        4            59       3.190e-011     1.790e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2722.49 psi
   
   Pressure: 
   1524.13, 1550.93, 1590.71, 1640.78, 1697.83, 1767.49, 1833.69, 1895.87, 
   1953.15, 2002.18, 2045.41, 2091.57, 2140.49, 2174.73, 2210.41, 2248.06, 
   2282.10, 2319.58, 2357.74, 2396.19, 2443.46, 2486.46, 2534.23, 2580.39, 
   2607.45
   
   Saturation:
   0.29, 0.63, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/26/2026 10:00:51 AM
   7/26/2026 10:02:55 AM
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
        0            1          241.351        start      
        1            56         0.00319       0.38343     
        2            57       1.012e-005     2.025e-004   
        3            58       9.119e-009     4.061e-007   
        4            59       6.706e-011     3.262e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2042.20 psi
   
   Pressure: 
   1524.98, 1530.26, 1537.25, 1546.31, 1556.86, 1569.93, 1582.51, 1594.45, 
   1605.56, 1615.17, 1623.72, 1632.95, 1642.83, 1649.82, 1657.19, 1665.04, 
   1672.22, 1680.22, 1688.47, 1696.90, 1707.42, 1717.16, 1731.84, 1815.25, 
   1869.25
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.63, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          233.786        start      
        1            56         0.00404       0.52047     
        2            57       2.093e-006     1.348e-004   
        3            58       1.400e-009     1.292e-007   
        4            59       7.459e-011     1.035e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2174.69 psi
   
   Pressure: 
   1524.93, 1530.19, 1537.17, 1546.20, 1556.73, 1569.76, 1582.32, 1594.22, 
   1605.31, 1614.89, 1623.43, 1632.63, 1642.49, 1649.46, 1656.81, 1664.64, 
   1671.80, 1679.78, 1688.01, 1696.42, 1708.86, 1772.63, 1866.97, 1953.80, 
   2001.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.24, 0.59, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          256.081        start      
        1            56         0.00633       0.46508     
        2            57       1.330e-005     3.517e-004   
        3            58       6.408e-009     1.029e-006   
        4            59       7.511e-011     3.411e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2284.36 psi
   
   Pressure: 
   1524.93, 1530.19, 1537.17, 1546.20, 1556.72, 1569.75, 1582.30, 1594.21, 
   1605.29, 1614.87, 1623.40, 1632.60, 1642.45, 1649.42, 1656.76, 1664.59, 
   1671.75, 1679.72, 1688.55, 1727.88, 1818.97, 1899.09, 1985.21, 2065.97, 
   2111.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.52, 0.71, 0.75, 0.77, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          264.253        start      
        1            56         0.00717       0.37067     
        2            57       2.272e-005     3.734e-004   
        3            58       2.491e-009     1.383e-006   
        4            59       7.780e-011     1.207e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2377.09 psi
   
   Pressure: 
   1524.94, 1530.20, 1537.18, 1546.21, 1556.74, 1569.78, 1582.33, 1594.23, 
   1605.32, 1614.90, 1623.43, 1632.64, 1642.49, 1649.46, 1656.80, 1664.63, 
   1672.00, 1698.55, 1770.22, 1840.98, 1925.34, 2000.58, 2082.54, 2160.10, 
   2204.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.46, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          259.944        start      
        1            56         0.00612       0.29640     
        2            57       2.191e-005     2.617e-004   
        3            58       3.809e-009     1.044e-006   
        4            59       6.311e-011     1.103e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2463.57 psi
   
   Pressure: 
   1524.95, 1530.21, 1537.19, 1546.22, 1556.75, 1569.79, 1582.34, 1594.25, 
   1605.34, 1614.92, 1623.46, 1632.66, 1642.52, 1649.49, 1656.91, 1675.94, 
   1737.82, 1805.62, 1872.85, 1939.51, 2020.18, 2092.63, 2172.01, 2247.51, 
   2290.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.68, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          244.534        start      
        1            56         0.00345       0.27969     
        2            57       1.210e-005     1.185e-004   
        3            58       1.383e-008     3.374e-007   
        4            59       7.526e-011     4.924e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2550.29 psi
   
   Pressure: 
   1524.94, 1530.19, 1537.17, 1546.19, 1556.71, 1569.74, 1582.29, 1594.19, 
   1605.27, 1614.85, 1623.38, 1632.58, 1642.46, 1654.20, 1715.75, 1782.81, 
   1841.87, 1905.89, 1970.30, 2034.60, 2112.85, 2183.38, 2260.91, 2334.92, 
   2377.57
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.66, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          221.080        start      
        1            56         0.00327       0.80106     
        2            57       1.725e-006     9.829e-005   
        3            58       1.568e-009     7.847e-008   
        4            59       8.048e-011     6.218e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2632.42 psi
   
   Pressure: 
   1524.70, 1529.90, 1536.81, 1545.74, 1556.16, 1569.06, 1581.48, 1593.27, 
   1604.24, 1613.72, 1622.17, 1632.99, 1697.73, 1757.61, 1818.17, 1881.10, 
   1937.42, 1998.99, 2061.27, 2123.69, 2199.92, 2268.83, 2344.83, 2417.61, 
   2459.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.24, 0.59, 0.72, 0.74, 0.76, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          257.643        start      
        1            56         0.00625       0.50847     
        2            57       1.879e-005     3.439e-004   
        3            58       3.884e-009     1.211e-006   
        4            59       8.857e-011     2.079e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2726.68 psi
   
   Pressure: 
   1524.82, 1530.05, 1536.99, 1545.97, 1556.44, 1569.40, 1581.88, 1593.71, 
   1604.74, 1614.62, 1645.54, 1725.42, 1808.19, 1865.01, 1923.44, 1984.66, 
   2039.70, 2100.01, 2161.14, 2222.48, 2297.50, 2365.40, 2440.38, 2512.32, 
   2554.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.48, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          253.648        start      
        1            56         0.00508       0.50441     
        2            57       1.778e-005     2.682e-004   
        3            58       1.855e-009     9.840e-007   
        4            59       6.461e-011     9.230e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2832.54 psi
   
   Pressure: 
   1524.80, 1530.02, 1536.96, 1545.93, 1556.38, 1569.33, 1581.80, 1593.72, 
   1617.84, 1699.90, 1772.53, 1848.26, 1927.42, 1982.38, 2039.30, 2099.12, 
   2153.02, 2212.17, 2272.22, 2332.54, 2406.41, 2473.36, 2547.41, 2618.58, 
   2660.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.38, 0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          221.031        start      
        1            56         0.00303       0.96109     
        2            57       1.027e-005     1.759e-004   
        3            58       9.118e-009     4.257e-007   
        4            59       7.557e-011     4.713e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2961.04 psi
   
   Pressure: 
   1524.54, 1529.71, 1536.57, 1545.44, 1555.79, 1568.62, 1586.86, 1678.74, 
   1772.94, 1851.65, 1920.20, 1992.77, 2069.25, 2122.61, 2178.06, 2236.46, 
   2289.17, 2347.11, 2406.01, 2465.27, 2537.97, 2603.96, 2677.08, 2747.50, 
   2788.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.63, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          222.450        start      
        1            56         0.00367       1.20423     
        2            57       1.801e-006     1.784e-004   
        3            58       1.739e-009     2.533e-007   
        4            59       8.851e-011     2.338e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3102.35 psi
   
   Pressure: 
   1524.38, 1529.52, 1536.33, 1545.15, 1557.31, 1639.16, 1745.63, 1843.73, 
   1932.82, 2008.49, 2074.93, 2145.64, 2220.43, 2272.71, 2327.15, 2384.54, 
   2436.39, 2493.44, 2551.50, 2609.96, 2681.75, 2746.99, 2819.37, 2889.18, 
   2930.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          252.743        start      
        1            56         0.00633       0.65578     
        2            57       1.694e-005     4.366e-004   
        3            58       5.019e-009     1.754e-006   
        4            59       7.552e-011     5.799e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3222.58 psi
   
   Pressure: 
   1524.67, 1529.87, 1537.14, 1575.03, 1665.89, 1775.18, 1877.49, 1973.01, 
   2060.59, 2135.35, 2201.13, 2271.26, 2345.48, 2397.40, 2451.45, 2508.44, 
   2559.93, 2616.58, 2674.23, 2732.28, 2803.57, 2868.37, 2940.29, 3009.68, 
   3050.31
   
   Saturation:
   0.20, 0.20, 0.21, 0.50, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          258.423        start      
        1            56         0.00593       0.32897     
        2            57       2.280e-005     2.824e-004   
        3            58       3.617e-008     1.693e-006   
        4            59       8.994e-011     2.582e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3302.72 psi
   
   Pressure: 
   1525.22, 1539.04, 1599.28, 1676.10, 1762.92, 1868.54, 1968.63, 2062.51, 
   2148.88, 2222.74, 2287.81, 2357.23, 2430.76, 2482.20, 2535.78, 2592.29, 
   2643.35, 2699.55, 2756.75, 2814.36, 2885.15, 2949.52, 3021.01, 3090.04, 
   3130.50
   
   Saturation:
   0.21, 0.42, 0.69, 0.73, 0.75, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          110.953        start      
        1            56         0.00433       3.39688     
        2            57       5.040e-006      0.00262     
        3            58       4.414e-009     8.680e-007   
        4            59       5.486e-011     1.090e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3448.71 psi
   
   Pressure: 
   1697.64, 1741.15, 1797.15, 1868.27, 1949.92, 2049.98, 2145.32, 2235.03, 
   2317.83, 2388.83, 2451.53, 2518.58, 2589.75, 2639.65, 2691.75, 2746.79, 
   2796.63, 2851.58, 2907.63, 2964.20, 3033.86, 3097.34, 3167.99, 3236.39, 
   3276.59
   
   Saturation:
   0.65, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          11.9989        start      
        1            56       8.591e-004      1.88988     
        2            57       1.345e-006     9.251e-004   
        3            58       3.363e-010     5.119e-007   
        4            59       1.024e-011     1.971e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3484.20 psi
   
   Pressure: 
   1711.28, 1754.77, 1811.52, 1884.04, 1967.57, 2070.01, 2167.63, 2259.44, 
   2344.09, 2416.58, 2480.52, 2548.79, 2621.14, 2671.78, 2724.55, 2780.23, 
   2830.56, 2885.97, 2942.40, 2999.27, 3069.19, 3132.81, 3203.54, 3271.94, 
   3312.11
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.22687        start      
        1            56       8.151e-004      3.70621     
        2            57       1.671e-006      0.00200     
        3            58       1.016e-009     1.073e-006   
        4            59       9.972e-012     1.166e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3461.55 psi
   
   Pressure: 
   1703.80, 1746.21, 1801.85, 1873.16, 1955.48, 2056.57, 2153.01, 2243.77, 
   2327.53, 2399.31, 2462.64, 2530.31, 2602.05, 2652.29, 2704.67, 2759.95, 
   2809.95, 2865.02, 2921.12, 2977.69, 3047.28, 3110.64, 3181.12, 3249.34, 
   3289.44
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.58535        start      
        1            56       5.441e-004      4.40391     
        2            57       1.095e-006      0.00232     
        3            58       7.237e-010     1.176e-006   
        4            59       6.202e-012     1.092e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3445.25 psi
   
   Pressure: 
   1699.76, 1741.48, 1796.35, 1866.77, 1948.15, 2048.17, 2143.65, 2233.56, 
   2316.57, 2387.74, 2450.57, 2517.72, 2588.93, 2638.83, 2690.86, 2745.80, 
   2795.50, 2850.26, 2906.07, 2962.38, 3031.67, 3094.79, 3165.05, 3233.09, 
   3273.13
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.79057        start      
        1            56       5.610e-004      5.72745     
        2            57       1.364e-006      0.00367     
        3            58       1.072e-009     2.322e-006   
        4            59       8.975e-012     2.353e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3431.95 psi
   
   Pressure: 
   1696.98, 1738.19, 1792.45, 1862.15, 1942.75, 2041.86, 2136.52, 2225.70, 
   2308.06, 2378.70, 2441.07, 2507.76, 2578.50, 2628.08, 2679.81, 2734.44, 
   2783.87, 2838.36, 2893.91, 2949.97, 3018.99, 3081.89, 3151.95, 3219.84, 
   3259.82
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.24453        start      
        1            56       4.463e-004      6.15696     
        2            57       1.131e-006      0.00384     
        3            58       8.440e-010     2.616e-006   
        4            59       7.052e-012     2.191e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3420.44 psi
   
   Pressure: 
   1694.81, 1735.61, 1789.36, 1858.44, 1938.37, 2036.68, 2130.62, 2219.14, 
   2300.92, 2371.08, 2433.04, 2499.31, 2569.64, 2618.93, 2670.37, 2724.72, 
   2773.91, 2828.14, 2883.46, 2939.29, 3008.06, 3070.77, 3140.64, 3208.38, 
   3248.30
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.88394        start      
        1            56       4.255e-004      6.84902     
        2            57       1.194e-006      0.00445     
        3            58       9.271e-010     3.377e-006   
        4            59       9.656e-012     2.917e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3410.21 psi
   
   Pressure: 
   1693.01, 1733.45, 1786.76, 1855.30, 1934.63, 2032.23, 2125.52, 2213.45, 
   2294.70, 2364.42, 2426.01, 2491.90, 2561.84, 2610.87, 2662.05, 2716.14, 
   2765.11, 2819.11, 2874.20, 2929.82, 2998.37, 3060.89, 3130.58, 3198.19, 
   3238.06
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 10:04:10 AM
   7/26/2026 10:06:17 AM
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
        0            1          356.366        start      
        1            56         0.00853       0.58818     
        2            57       1.506e-005     5.683e-004   
        3            58       6.819e-009     1.230e-006   
        4            59       8.664e-011     4.190e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2277.58 psi
   
   Pressure: 
   1533.29, 1540.32, 1549.64, 1561.70, 1575.76, 1593.17, 1609.93, 1625.83, 
   1640.64, 1653.44, 1664.83, 1677.12, 1690.27, 1699.58, 1709.39, 1719.84, 
   1729.40, 1740.05, 1751.03, 1762.24, 1776.24, 1789.80, 1853.16, 1978.80, 
   2047.05
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.51, 0.71, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          316.409        start      
        1            56         0.00303       0.38877     
        2            57       8.815e-006     1.358e-004   
        3            58       8.964e-009     2.328e-007   
        4            59       1.151e-010     2.799e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2511.31 psi
   
   Pressure: 
   1533.24, 1540.25, 1549.55, 1561.59, 1575.63, 1593.01, 1609.75, 1625.62, 
   1640.40, 1653.19, 1664.57, 1676.84, 1689.98, 1699.28, 1709.09, 1719.54, 
   1729.10, 1739.74, 1750.74, 1767.40, 1881.97, 1992.27, 2109.82, 2219.47, 
   2280.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.30, 0.65, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          353.336        start      
        1            56         0.00716       0.40647     
        2            57       1.876e-005     3.347e-004   
        3            58       3.319e-009     9.782e-007   
        4            59       1.590e-010     1.544e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2667.84 psi
   
   Pressure: 
   1533.24, 1540.25, 1549.56, 1561.60, 1575.63, 1593.01, 1609.74, 1625.61, 
   1640.39, 1653.16, 1664.53, 1676.80, 1689.93, 1699.21, 1709.00, 1719.44, 
   1729.23, 1764.02, 1859.63, 1953.88, 2066.22, 2166.41, 2275.53, 2378.79, 
   2437.61
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.46, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          295.243        start      
        1            56         0.00279       0.60049     
        2            57       5.264e-006     8.404e-005   
        3            58       4.508e-009     1.152e-007   
        4            59       9.739e-011     9.900e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2824.46 psi
   
   Pressure: 
   1533.05, 1540.02, 1549.27, 1561.23, 1575.18, 1592.46, 1609.09, 1624.87, 
   1639.56, 1652.26, 1663.57, 1675.77, 1688.83, 1698.08, 1710.71, 1786.93, 
   1868.86, 1956.70, 2044.40, 2131.67, 2237.56, 2332.85, 2437.43, 2537.08, 
   2594.36
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.61, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          333.953        start      
        1            56         0.00460       0.41110     
        2            57       1.274e-005     1.972e-004   
        3            58       1.561e-009     5.377e-007   
        4            59       1.232e-010     8.076e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2958.72 psi
   
   Pressure: 
   1533.14, 1540.13, 1549.40, 1561.40, 1575.38, 1592.70, 1609.38, 1625.19, 
   1639.92, 1652.65, 1663.99, 1676.28, 1703.43, 1782.53, 1865.39, 1950.92, 
   2027.15, 2110.27, 2194.21, 2278.22, 2380.66, 2473.15, 2575.01, 2672.43, 
   2728.74
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.37, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          345.575        start      
        1            56         0.00625       0.53494     
        2            57       1.577e-005     3.053e-004   
        3            58       3.462e-009     8.537e-007   
        4            59       1.109e-010     1.658e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3130.27 psi
   
   Pressure: 
   1533.03, 1539.99, 1549.23, 1561.19, 1575.12, 1592.38, 1608.99, 1624.75, 
   1639.42, 1652.50, 1691.74, 1798.17, 1908.38, 1983.98, 2061.71, 2143.14, 
   2216.34, 2296.55, 2377.86, 2459.44, 2559.21, 2649.50, 2749.23, 2844.90, 
   2900.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.47, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          294.087        start      
        1            56         0.00358       1.10878     
        2            57       1.389e-006     1.463e-004   
        3            58       9.168e-010     1.364e-007   
        4            59       1.126e-010     7.391e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3317.07 psi
   
   Pressure: 
   1532.52, 1539.37, 1548.47, 1560.23, 1573.95, 1590.93, 1607.29, 1625.55, 
   1719.84, 1828.41, 1921.96, 2020.18, 2123.29, 2195.06, 2269.55, 2347.94, 
   2418.66, 2496.37, 2575.34, 2654.76, 2752.14, 2840.50, 2938.35, 3032.51, 
   3087.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 
   0.59, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          327.576        start      
        1            56         0.00430       0.88715     
        2            57       1.215e-005     2.285e-004   
        3            58       1.326e-009     6.477e-007   
        4            59       1.241e-010     7.482e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3565.11 psi
   
   Pressure: 
   1532.64, 1539.52, 1548.64, 1560.45, 1574.28, 1606.16, 1746.90, 1880.99, 
   2001.72, 2103.76, 2193.13, 2288.07, 2388.32, 2458.35, 2531.19, 2607.95, 
   2677.26, 2753.48, 2831.01, 2909.05, 3004.84, 3091.83, 3188.32, 3281.32, 
   3335.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          343.603        start      
        1            56         0.00656       0.68330     
        2            57       1.611e-005     4.109e-004   
        3            58       4.466e-009     1.384e-006   
        4            59       1.444e-010     4.000e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3782.57 psi
   
   Pressure: 
   1532.78, 1539.69, 1549.20, 1593.97, 1714.95, 1860.43, 1996.45, 2123.37, 
   2239.70, 2338.95, 2426.28, 2519.37, 2617.87, 2686.77, 2758.50, 2834.13, 
   2902.46, 2977.64, 3054.15, 3131.19, 3225.82, 3311.81, 3407.27, 3499.39, 
   3553.33
   
   Saturation:
   0.20, 0.20, 0.21, 0.48, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          290.248        start      
        1            56         0.00398       0.83985     
        2            57       6.946e-006     1.811e-004   
        3            58       1.046e-008     6.503e-007   
        4            59       1.120e-010     5.233e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3913.37 psi
   
   Pressure: 
   1540.87, 1589.46, 1668.91, 1768.58, 1881.95, 2020.25, 2151.57, 2274.85, 
   2388.37, 2485.52, 2571.15, 2662.58, 2759.44, 2827.25, 2897.92, 2972.47, 
   3039.88, 3114.08, 3189.65, 3265.80, 3359.41, 3444.56, 3539.18, 3630.62, 
   3684.25
   
   Saturation:
   0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          27.1000        start      
        1            56       9.796e-004      0.54750     
        2            57       1.267e-006     6.511e-004   
        3            58       1.088e-010     6.546e-007   
        4            59       1.828e-011     3.771e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4148.34 psi
   
   Pressure: 
   1787.53, 1846.18, 1922.34, 2019.42, 2131.03, 2267.75, 2397.92, 2520.25, 
   2632.99, 2729.49, 2814.57, 2905.40, 3001.61, 3068.94, 3139.09, 3213.09, 
   3279.98, 3353.60, 3428.56, 3504.10, 3596.96, 3681.44, 3775.34, 3866.12, 
   3919.43
   
   Saturation:
   0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.24047        start      
        1            56       8.073e-004      4.28179     
        2            57       1.355e-006      0.00167     
        3            58       6.800e-010     6.926e-007   
        4            59       9.464e-012     5.982e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4109.12 psi
   
   Pressure: 
   1771.93, 1828.49, 1902.66, 1997.68, 2107.32, 2241.91, 2370.27, 2491.05, 
   2602.47, 2697.93, 2782.15, 2872.10, 2967.45, 3034.22, 3103.81, 3177.25, 
   3243.66, 3316.79, 3391.29, 3466.40, 3558.78, 3642.88, 3736.43, 3826.95, 
   3880.18
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.60370        start      
        1            56       6.007e-004      5.84105     
        2            57       1.116e-006      0.00262     
        3            58       7.217e-010     1.069e-006   
        4            59       5.115e-012     9.780e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4080.93 psi
   
   Pressure: 
   1764.89, 1820.24, 1893.05, 1986.50, 2094.48, 2227.19, 2353.88, 2473.17, 
   2583.29, 2677.69, 2761.02, 2850.09, 2944.53, 3010.70, 3079.71, 3152.56, 
   3218.48, 3291.09, 3365.11, 3439.77, 3531.66, 3615.37, 3708.57, 3798.82, 
   3851.96
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.52621        start      
        1            56       5.689e-004      7.39198     
        2            57       1.262e-006      0.00395     
        3            58       9.283e-010     1.955e-006   
        4            59       5.513e-012     1.787e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4059.15 psi
   
   Pressure: 
   1760.44, 1814.96, 1886.77, 1979.02, 2085.71, 2216.91, 2342.23, 2460.29, 
   2569.32, 2662.84, 2745.42, 2833.72, 2927.40, 2993.05, 3061.55, 3133.89, 
   3199.37, 3271.54, 3345.13, 3419.39, 3510.85, 3594.22, 3687.09, 3777.11, 
   3830.16
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.83968        start      
        1            56       4.467e-004      7.85336     
        2            57       1.039e-006      0.00408     
        3            58       7.277e-010     2.172e-006   
        4            59       5.386e-012     1.628e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4040.85 psi
   
   Pressure: 
   1757.08, 1810.95, 1881.95, 1973.21, 2078.82, 2208.74, 2332.89, 2449.89, 
   2557.98, 2650.73, 2732.65, 2820.28, 2913.27, 2978.46, 3046.51, 3118.40, 
   3183.49, 3255.25, 3328.46, 3402.37, 3493.44, 3576.49, 3669.07, 3758.87, 
   3811.84
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.35847        start      
        1            56       4.089e-004      8.58530     
        2            57       1.045e-006      0.00455     
        3            58       7.525e-010     2.666e-006   
        4            59       8.783e-012     2.014e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4024.91 psi
   
   Pressure: 
   1754.34, 1807.66, 1877.98, 1968.39, 2073.06, 2201.86, 2324.99, 2441.05, 
   2548.31, 2640.37, 2721.70, 2808.72, 2901.10, 2965.89, 3033.52, 3105.00, 
   3169.74, 3241.14, 3314.01, 3387.60, 3478.32, 3561.08, 3653.40, 3742.99, 
   3795.89
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.97453        start      
        1            56       4.023e-004      9.49409     
        2            57       1.143e-006      0.00520     
        3            58       8.887e-010     3.326e-006   
        4            59       7.295e-012     2.883e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4010.74 psi
   
   Pressure: 
   1751.99, 1804.85, 1874.56, 1964.23, 2068.07, 2195.88, 2318.09, 2433.32, 
   2539.83, 2631.26, 2712.07, 2798.54, 2890.37, 2954.78, 3022.05, 3093.16, 
   3157.58, 3228.65, 3301.20, 3374.50, 3464.89, 3547.40, 3639.47, 3728.88, 
   3781.71
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.67700        start      
        1            56       4.274e-004      10.6576     
        2            57       1.358e-006      0.00618     
        3            58       1.203e-009     4.228e-006   
        4            59       9.675e-012     4.708e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3997.97 psi
   
   Pressure: 
   1749.93, 1802.37, 1871.55, 1960.56, 2063.65, 2190.57, 2311.95, 2426.42, 
   2532.25, 2623.12, 2703.45, 2789.43, 2880.75, 2944.82, 3011.75, 3082.52, 
   3146.64, 3217.41, 3289.68, 3362.71, 3452.81, 3535.07, 3626.92, 3716.16, 
   3768.92
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.45369        start      
        1            56       3.244e-004      9.78805     
        2            57       9.337e-007      0.00480     
        3            58       7.385e-010     2.987e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3986.35 psi
   
   Pressure: 
   1748.09, 1800.16, 1868.86, 1957.26, 2059.68, 2185.79, 2306.42, 2420.19, 
   2525.40, 2615.76, 2695.64, 2781.17, 2872.02, 2935.78, 3002.40, 3072.86, 
   3136.72, 3207.21, 3279.21, 3352.00, 3441.82, 3523.87, 3615.51, 3704.59, 
   3757.29
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.28601        start      
        1            56       3.972e-004      11.3583     
        2            57       1.295e-006      0.00626     
        3            58       1.260e-009     4.092e-006   
        4            59       6.546e-012     5.642e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3975.70 psi
   
   Pressure: 
   1746.43, 1798.15, 1866.42, 1954.27, 2056.07, 2181.44, 2301.38, 2414.52, 
   2519.16, 2609.04, 2688.52, 2773.63, 2864.05, 2927.53, 2993.86, 3064.03, 
   3127.64, 3197.87, 3269.63, 3342.19, 3431.76, 3513.61, 3605.05, 3693.99, 
   3746.63
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 10:07:42 AM
   7/26/2026 10:09:50 AM
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
        0            1          434.582        start      
        1            56         0.00637       0.34719     
        2            57       1.809e-005     2.570e-004   
        3            58       4.257e-009     6.733e-007   
        4            59       1.224e-010     2.078e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2555.83 psi
   
   Pressure: 
   1541.67, 1550.47, 1562.14, 1577.24, 1594.84, 1616.64, 1637.63, 1657.54, 
   1676.07, 1692.10, 1706.37, 1721.76, 1738.24, 1749.89, 1762.18, 1775.28, 
   1787.26, 1800.60, 1814.37, 1828.42, 1846.05, 1877.19, 2035.21, 2185.75, 
   2267.87
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 0.74, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          418.495        start      
        1            56         0.00672       0.39185     
        2            57       2.028e-005     2.481e-004   
        3            58       3.949e-009     7.146e-007   
        4            59       1.170e-010     1.788e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2860.49 psi
   
   Pressure: 
   1541.58, 1550.35, 1561.99, 1577.05, 1594.61, 1616.35, 1637.29, 1657.14, 
   1675.63, 1691.62, 1705.85, 1721.20, 1737.63, 1749.26, 1761.52, 1774.59, 
   1786.53, 1799.91, 1828.09, 1947.69, 2095.22, 2225.26, 2365.79, 2498.02, 
   2572.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.37, 0.67, 0.74, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          432.147        start      
        1            56         0.00745       0.40652     
        2            57       2.170e-005     2.749e-004   
        3            58       2.369e-009     8.199e-007   
        4            59       1.150e-010     9.144e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3100.99 psi
   
   Pressure: 
   1541.52, 1550.28, 1561.90, 1576.94, 1594.46, 1616.17, 1637.07, 1656.89, 
   1675.35, 1691.30, 1705.51, 1720.83, 1737.24, 1748.84, 1761.18, 1791.43, 
   1894.61, 2007.43, 2119.22, 2230.02, 2364.05, 2484.43, 2616.29, 2741.68, 
   2813.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 
   0.68, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          415.061        start      
        1            56         0.00564       0.51506     
        2            57       1.524e-005     2.286e-004   
        3            58       2.354e-009     5.866e-007   
        4            59       1.175e-010     1.043e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3318.97 psi
   
   Pressure: 
   1541.35, 1550.07, 1561.65, 1576.62, 1594.08, 1615.69, 1636.50, 1656.24, 
   1674.62, 1690.51, 1704.66, 1720.00, 1752.88, 1851.49, 1954.93, 2061.65, 
   2156.74, 2260.43, 2365.13, 2469.90, 2597.66, 2713.02, 2840.07, 2961.58, 
   3031.83
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          375.241        start      
        1            56         0.00365       0.82450     
        2            57       9.842e-006     1.467e-004   
        3            58       5.527e-009     2.832e-007   
        4            59       1.717e-010     1.862e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3586.32 psi
   
   Pressure: 
   1540.98, 1549.62, 1561.09, 1575.93, 1593.23, 1614.65, 1635.27, 1654.84, 
   1673.09, 1696.95, 1808.98, 1939.25, 2073.64, 2166.32, 2261.93, 2362.24, 
   2452.52, 2551.53, 2651.97, 2752.84, 2876.29, 2988.12, 3111.74, 3230.46, 
   3299.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.30, 0.64, 0.72, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          357.684        start      
        1            56         0.00343       1.42607     
        2            57       6.484e-006     1.624e-004   
        3            58       5.385e-009     2.245e-007   
        4            59       1.061e-010     1.977e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3899.23 psi
   
   Pressure: 
   1540.28, 1548.77, 1560.03, 1574.61, 1591.61, 1612.67, 1639.87, 1783.31, 
   1938.46, 2067.93, 2180.65, 2299.96, 2425.74, 2513.52, 2604.80, 2700.97, 
   2787.82, 2883.33, 2980.49, 3078.29, 3198.33, 3307.35, 3428.23, 3544.71, 
   3612.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          354.637        start      
        1            56         0.00336       1.32514     
        2            57       4.841e-006     1.500e-004   
        3            58       7.995e-009     1.397e-007   
        4            59       1.449e-010     2.502e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4276.12 psi
   
   Pressure: 
   1540.28, 1548.77, 1560.04, 1578.45, 1704.20, 1887.19, 2056.86, 2214.56, 
   2358.80, 2481.75, 2589.90, 2705.18, 2827.20, 2912.58, 3001.53, 3095.36, 
   3180.18, 3273.55, 3368.63, 3464.43, 3582.15, 3689.19, 3808.07, 3922.84, 
   3990.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.26, 0.62, 0.72, 0.74, 0.75, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          367.497        start      
        1            56         0.00420       0.77893     
        2            57       1.395e-006     1.393e-004   
        3            58       1.194e-009     3.891e-007   
        4            59       1.310e-010     3.156e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4499.38 psi
   
   Pressure: 
   1548.20, 1604.54, 1703.83, 1828.28, 1969.68, 2142.08, 2305.71, 2459.29, 
   2600.68, 2721.65, 2828.27, 2942.09, 3062.67, 3147.07, 3235.02, 3327.82, 
   3411.70, 3504.05, 3598.09, 3692.86, 3809.35, 3915.31, 4033.05, 4146.83, 
   4213.58
   
   Saturation:
   0.24, 0.59, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          22.5103        start      
        1            56       9.736e-004      0.72011     
        2            57       1.256e-006     7.067e-004   
        3            58       9.242e-011     6.646e-007   
        4            59       1.201e-011     3.430e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1822.73, 1888.87, 1974.98, 2084.84, 2211.24, 2366.13, 2513.62, 2652.27, 
   2780.06, 2889.46, 2985.92, 3088.91, 3198.01, 3274.38, 3353.95, 3437.90, 
   3513.78, 3597.32, 3682.40, 3768.14, 3873.56, 3969.49, 4076.15, 4179.29, 
   4239.88
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.30335        start      
        1            56       8.574e-004      1.35629     
        2            57       1.540e-006      0.00149     
        3            58       5.312e-010     1.643e-006   
        4            59       6.511e-012     4.770e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1810.81, 1875.58, 1960.61, 2069.62, 2195.46, 2349.99, 2497.40, 2636.14, 
   2764.16, 2873.86, 2970.67, 3074.10, 3183.75, 3260.56, 3340.63, 3425.16, 
   3501.61, 3585.82, 3671.64, 3758.18, 3864.66, 3961.63, 4069.54, 4174.01, 
   4235.47
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.87963        start      
        1            56       8.817e-004      1.72309     
        2            57       1.841e-006      0.00230     
        3            58       6.993e-010     3.181e-006   
        4            59       8.648e-012     9.481e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1806.59, 1870.72, 1955.12, 2063.50, 2188.79, 2342.79, 2489.84, 2628.33, 
   2756.20, 2865.85, 2962.65, 3066.13, 3175.90, 3252.82, 3333.05, 3417.79, 
   3494.47, 3578.96, 3665.12, 3752.05, 3859.07, 3956.61, 4065.23, 4170.49, 
   4232.50
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.75826        start      
        1            56       6.650e-004      1.67184     
        2            57       1.244e-006      0.00201     
        3            58       3.711e-010     2.630e-006   
        4            59       6.240e-012     5.570e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1804.19, 1867.92, 1951.88, 2059.78, 2184.60, 2338.13, 2484.81, 2623.01, 
   2750.67, 2860.19, 2956.92, 3060.36, 3170.13, 3247.08, 3327.39, 3412.23, 
   3489.03, 3573.71, 3660.08, 3747.27, 3854.68, 3952.63, 4061.79, 4167.65, 
   4230.08
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.08152        start      
        1            56       6.434e-004      1.75720     
        2            57       1.204e-006      0.00214     
        3            58       3.346e-010     2.919e-006   
        4            59       7.855e-012     5.600e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1802.52, 1865.94, 1949.56, 2057.06, 2181.50, 2334.60, 2480.94, 2618.86, 
   2746.31, 2855.69, 2952.32, 3055.70, 3165.43, 3242.39, 3322.72, 3407.62, 
   3484.51, 3569.31, 3655.85, 3743.24, 3850.96, 3949.23, 4058.83, 4165.20, 
   4228.00
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.59114        start      
        1            56       6.930e-004      1.91416     
        2            57       1.358e-006      0.00247     
        3            58       3.888e-010     3.641e-006   
        4            59       9.973e-012     7.426e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1801.21, 1864.39, 1947.72, 2054.89, 2178.99, 2331.72, 2477.74, 2615.41, 
   2742.66, 2851.89, 2948.42, 3051.72, 3161.41, 3238.35, 3318.70, 3403.64, 
   3480.60, 3565.49, 3652.16, 3739.72, 3847.69, 3946.25, 4056.23, 4163.04, 
   4226.15
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.19974        start      
        1            56       7.874e-004      2.12323     
        2            57       1.650e-006      0.00296     
        3            58       5.187e-010     4.777e-006   
        4            59       9.755e-012     1.108e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1800.13, 1863.10, 1946.18, 2053.07, 2176.86, 2329.25, 2474.99, 2612.42, 
   2739.48, 2848.58, 2945.01, 3048.23, 3157.86, 3234.79, 3315.15, 3400.12, 
   3477.12, 3562.09, 3648.87, 3736.57, 3844.77, 3943.58, 4053.89, 4161.09, 
   4224.48
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.86766        start      
        1            56       5.929e-004      1.90916     
        2            57       1.062e-006      0.00223     
        3            58       2.841e-010     3.122e-006   
        4            59       8.619e-012     5.899e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1799.20, 1861.99, 1944.85, 2051.48, 2175.00, 2327.09, 2472.57, 2609.77, 
   2736.66, 2845.62, 2941.96, 3045.11, 3154.68, 3231.59, 3311.95, 3396.94, 
   3473.98, 3559.02, 3645.90, 3733.73, 3842.13, 3941.15, 4051.77, 4159.32, 
   4222.96
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.60413        start      
        1            56       7.216e-004      2.16921     
        2            57       1.424e-006      0.00283     
        3            58       4.491e-010     4.419e-006   
        4            59       6.598e-012     1.037e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1798.37, 1861.01, 1943.67, 2050.06, 2173.34, 2325.15, 2470.39, 2607.39, 
   2734.11, 2842.95, 2939.20, 3042.27, 3151.80, 3228.68, 3309.04, 3394.05, 
   3471.12, 3556.22, 3643.19, 3731.13, 3839.71, 3938.94, 4049.83, 4157.70, 
   4221.57
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.39702        start      
        1            56       5.881e-004      2.00733     
        2            57       1.035e-006      0.00230     
        3            58       2.954e-010     3.232e-006   
        4            59       9.386e-012     6.701e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1797.63, 1860.12, 1942.61, 2048.79, 2171.84, 2323.39, 2468.41, 2605.22, 
   2731.78, 2840.52, 2936.68, 3039.68, 3149.15, 3226.02, 3306.37, 3391.39, 
   3468.50, 3553.65, 3640.69, 3728.74, 3837.48, 3936.89, 4048.04, 4156.20, 
   4220.28
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.19283        start      
        1            56       7.346e-004      2.30101     
        2            57       1.437e-006      0.00298     
        3            58       5.015e-010     4.674e-006   
        4            59       1.108e-011     1.252e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1796.95, 1859.31, 1941.64, 2047.62, 2170.46, 2321.78, 2466.59, 2603.23, 
   2729.65, 2838.27, 2934.36, 3037.29, 3146.71, 3223.55, 3303.90, 3388.94, 
   3466.07, 3551.27, 3638.38, 3726.52, 3835.42, 3935.00, 4046.38, 4154.81, 
   4219.09
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.03874        start      
        1            56       6.166e-004      2.15182     
        2            57       1.094e-006      0.00250     
        3            58       3.543e-010     3.566e-006   
        4            59       1.215e-011     8.683e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1796.33, 1858.57, 1940.75, 2046.55, 2169.19, 2320.29, 2464.91, 2601.38, 
   2727.67, 2836.19, 2932.21, 3035.08, 3144.45, 3221.27, 3301.61, 3386.66, 
   3463.82, 3549.06, 3636.24, 3724.47, 3833.50, 3933.24, 4044.83, 4153.52, 
   4217.98
   
   Saturation:
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/26/2026 10:11:21 AM
   7/26/2026 10:13:36 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

