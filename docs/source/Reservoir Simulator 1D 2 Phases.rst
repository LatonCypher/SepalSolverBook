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
        0            1          200.156        start      
        1            56         0.00269       5.60999     
        2            57       2.545e-006     6.024e-004   
        3            58       7.519e-009     1.203e-007   
        4            59       5.583e-011     5.953e-010   
   Producer BHP: 
   2257.36 psi
   
   Injector BHP: 
   2657.49 psi
   
   Pressure: 
   2285.81, 2293.13, 2301.14, 2307.76, 2313.63, 2320.42, 2326.14, 2334.76, 
   2364.68, 2391.05, 2397.66, 2403.45, 2409.77, 2415.85, 2421.12, 2427.63, 
   2434.71, 2441.27, 2447.89, 2455.03, 2460.93, 2465.69, 2470.50, 2489.04, 
   2547.50
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.71
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          177.713        start      
        1            56         0.00290       6.00480     
        2            57       1.640e-006     6.098e-004   
        3            58       4.567e-009     2.017e-007   
        4            59       5.855e-011     3.847e-010   
   Producer BHP: 
   1607.61 psi
   
   Injector BHP: 
   2060.33 psi
   
   Pressure: 
   1636.12, 1643.46, 1651.47, 1658.08, 1663.95, 1670.73, 1676.44, 1685.03, 
   1714.84, 1741.09, 1747.66, 1753.41, 1759.69, 1765.72, 1770.95, 1777.39, 
   1784.39, 1790.88, 1797.41, 1804.45, 1810.26, 1815.28, 1836.81, 1897.42, 
   1949.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.52, 0.72, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.227        start      
        1            56         0.00332       0.27437     
        2            57       1.045e-005     1.573e-004   
        3            58       5.550e-009     4.870e-007   
        4            59       3.204e-011     2.037e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1950.40 psi
   
   Pressure: 
   1520.57, 1525.92, 1531.84, 1536.79, 1541.23, 1546.42, 1550.85, 1557.60, 
   1581.35, 1602.55, 1607.91, 1612.68, 1617.96, 1623.11, 1627.62, 1633.27, 
   1639.50, 1645.36, 1651.35, 1657.90, 1664.81, 1697.01, 1735.92, 1791.04, 
   1839.96
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.864        start      
        1            56         0.00443       0.26435     
        2            57       2.235e-005     2.131e-004   
        3            58       2.548e-008     9.572e-007   
        4            59       4.725e-011     1.981e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1991.45 psi
   
   Pressure: 
   1520.57, 1525.92, 1531.84, 1536.78, 1541.22, 1546.41, 1550.84, 1557.59, 
   1581.32, 1602.52, 1607.88, 1612.65, 1617.93, 1623.07, 1627.59, 1633.23, 
   1639.46, 1645.31, 1651.32, 1662.36, 1707.25, 1745.23, 1781.69, 1833.94, 
   1881.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.33, 0.65, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          173.271        start      
        1            56         0.00831       0.33869     
        2            57       3.985e-005     4.904e-004   
        3            58       1.975e-008     2.856e-006   
        4            59       6.327e-011     1.141e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2042.25 psi
   
   Pressure: 
   1520.56, 1525.90, 1531.82, 1536.76, 1541.19, 1546.38, 1550.80, 1557.55, 
   1581.27, 1602.44, 1607.80, 1612.57, 1617.84, 1622.98, 1627.49, 1633.13, 
   1639.35, 1645.29, 1661.94, 1718.57, 1764.66, 1800.62, 1835.50, 1885.94, 
   1931.83
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.43, 0.69, 0.74, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.622        start      
        1            56         0.00735       0.48851     
        2            57       2.260e-005     4.910e-004   
        3            58       7.764e-009     2.240e-006   
        4            59       4.629e-011     4.855e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2089.41 psi
   
   Pressure: 
   1520.52, 1525.86, 1531.76, 1536.69, 1541.12, 1546.29, 1550.71, 1557.45, 
   1581.12, 1602.25, 1607.60, 1612.36, 1617.62, 1622.75, 1627.25, 1632.88, 
   1639.46, 1666.03, 1717.85, 1772.44, 1816.32, 1850.96, 1884.77, 1933.95, 
   1979.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.52, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.516        start      
        1            56         0.00440       0.57481     
        2            57       2.893e-006     1.780e-004   
        3            58       5.921e-009     4.566e-007   
        4            59       4.797e-011     4.097e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2135.58 psi
   
   Pressure: 
   1520.49, 1525.82, 1531.71, 1536.63, 1541.05, 1546.22, 1550.63, 1557.35, 
   1580.98, 1602.08, 1607.42, 1612.17, 1617.43, 1622.54, 1627.04, 1633.59, 
   1671.12, 1721.58, 1770.97, 1823.32, 1865.77, 1899.50, 1932.53, 1980.77, 
   2025.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 
   0.57, 0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.000        start      
        1            56         0.00324       0.37163     
        2            57       1.549e-005     1.980e-004   
        3            58       3.355e-008     3.999e-007   
        4            59       9.664e-011     9.591e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2182.93 psi
   
   Pressure: 
   1520.53, 1525.86, 1531.77, 1536.70, 1541.12, 1546.30, 1550.72, 1557.45, 
   1581.13, 1602.26, 1607.62, 1612.37, 1617.64, 1622.77, 1629.28, 1673.51, 
   1727.07, 1775.35, 1823.11, 1874.07, 1915.56, 1948.63, 1981.09, 2028.62, 
   2072.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.64, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.222        start      
        1            56         0.00461       0.23268     
        2            57       2.165e-005     1.706e-004   
        3            58       7.178e-009     9.122e-007   
        4            59       3.953e-011     6.061e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2214.64 psi
   
   Pressure: 
   1520.57, 1525.91, 1531.82, 1536.76, 1541.19, 1546.38, 1550.80, 1557.55, 
   1581.26, 1602.43, 1607.79, 1612.55, 1617.85, 1627.65, 1665.62, 1713.76, 
   1765.06, 1811.88, 1858.53, 1908.46, 1949.22, 1981.76, 2013.76, 2060.73, 
   2104.28
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.242        start      
        1            56         0.00646       0.31024     
        2            57       3.057e-005     3.155e-004   
        3            58       1.286e-008     1.794e-006   
        4            59       4.608e-011     5.810e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2248.92 psi
   
   Pressure: 
   1520.55, 1525.88, 1531.79, 1536.72, 1541.15, 1546.33, 1550.75, 1557.48, 
   1581.16, 1602.31, 1607.66, 1612.48, 1626.18, 1670.34, 1708.54, 1754.78, 
   1804.54, 1850.28, 1896.02, 1945.07, 1985.18, 2017.27, 2048.87, 2095.35, 
   2138.57
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.41, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.858        start      
        1            56         0.00696       0.37002     
        2            57       2.918e-005     3.688e-004   
        3            58       2.150e-009     1.964e-006   
        4            59       3.511e-011     8.888e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2284.56 psi
   
   Pressure: 
   1520.53, 1525.86, 1531.76, 1536.68, 1541.11, 1546.28, 1550.69, 1557.42, 
   1581.08, 1602.20, 1607.74, 1624.83, 1670.56, 1713.88, 1750.71, 1795.74, 
   1844.43, 1889.34, 1934.35, 1982.67, 2022.25, 2053.95, 2085.22, 2131.28, 
   2174.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.48, 0.70, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          161.812        start      
        1            56         0.00604       0.62571     
        2            57       1.728e-005     3.615e-004   
        3            58       1.651e-008     1.477e-006   
        4            59       5.295e-011     1.093e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2320.30 psi
   
   Pressure: 
   1520.44, 1525.75, 1531.62, 1536.53, 1540.93, 1546.09, 1550.48, 1557.18, 
   1580.73, 1603.62, 1629.35, 1670.47, 1714.57, 1756.32, 1792.20, 1836.26, 
   1884.04, 1928.21, 1972.55, 2020.21, 2059.30, 2090.64, 2121.60, 2167.29, 
   2209.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.53, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          140.030        start      
        1            56         0.00362       2.63643     
        2            57       4.164e-006     3.269e-004   
        3            58       5.756e-009     3.969e-007   
        4            59       5.387e-011     5.585e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2444.21 psi
   
   Pressure: 
   1519.79, 1524.93, 1530.62, 1535.37, 1539.63, 1544.62, 1548.87, 1555.36, 
   1582.84, 1725.92, 1770.85, 1809.43, 1851.11, 1890.96, 1925.43, 1967.95, 
   2014.21, 2057.13, 2100.33, 2146.89, 2185.17, 2215.96, 2246.46, 2291.60, 
   2333.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.25, 0.60, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.258        start      
        1            56         0.00269       2.21038     
        2            57       9.536e-006     2.501e-004   
        3            58       2.210e-008     3.743e-007   
        4            59       5.094e-011     8.376e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2622.84 psi
   
   Pressure: 
   1519.92, 1525.09, 1530.81, 1535.59, 1539.88, 1544.90, 1549.18, 1558.08, 
   1735.47, 1912.34, 1955.82, 1993.50, 2034.49, 2073.80, 2107.87, 2149.94, 
   2195.71, 2238.20, 2280.98, 2327.09, 2365.02, 2395.54, 2425.78, 2470.58, 
   2512.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 
   0.63, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          156.631        start      
        1            56         0.00392       0.40500     
        2            57       1.934e-005     1.739e-004   
        3            58       5.532e-008     5.884e-007   
        4            59       4.871e-011     2.136e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2698.27 psi
   
   Pressure: 
   1520.50, 1525.82, 1531.71, 1536.63, 1541.04, 1546.23, 1553.64, 1608.88, 
   1812.00, 1986.88, 2030.27, 2068.08, 2109.29, 2148.84, 2183.10, 2225.37, 
   2271.32, 2313.93, 2356.77, 2402.92, 2440.83, 2471.30, 2501.48, 2546.15, 
   2588.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.65, 
   0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.596        start      
        1            56         0.00550       0.25212     
        2            57       2.551e-005     2.533e-004   
        3            58       2.421e-008     1.529e-006   
        4            59       4.367e-011     1.133e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2722.11 psi
   
   Pressure: 
   1520.55, 1525.88, 1531.78, 1536.71, 1541.18, 1552.81, 1590.51, 1648.13, 
   1844.31, 2015.45, 2058.13, 2095.46, 2136.24, 2175.42, 2209.38, 2251.31, 
   2296.92, 2339.22, 2381.77, 2427.62, 2465.30, 2495.61, 2525.64, 2570.13, 
   2611.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.954        start      
        1            56         0.00657       0.29926     
        2            57       2.997e-005     3.403e-004   
        3            58       2.262e-008     2.080e-006   
        4            59       3.672e-011     1.448e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2749.42 psi
   
   Pressure: 
   1520.53, 1525.86, 1531.75, 1536.79, 1549.99, 1594.81, 1632.29, 1687.94, 
   1879.51, 2047.67, 2089.73, 2126.59, 2166.90, 2205.67, 2239.31, 2280.87, 
   2326.09, 2368.05, 2410.29, 2455.82, 2493.26, 2523.40, 2553.28, 2597.58, 
   2639.28
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.44, 0.69, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          161.332        start      
        1            56         0.00707       0.52044     
        2            57       2.440e-005     4.636e-004   
        3            58       6.971e-009     2.569e-006   
        4            59       4.547e-011     8.685e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2776.62 psi
   
   Pressure: 
   1520.45, 1525.75, 1531.99, 1553.85, 1592.16, 1635.79, 1672.03, 1726.32, 
   1914.27, 2079.78, 2121.24, 2157.64, 2197.49, 2235.85, 2269.15, 2310.31, 
   2355.14, 2396.76, 2438.68, 2483.89, 2521.09, 2551.05, 2580.78, 2624.90, 
   2666.49
   
   Saturation:
   0.20, 0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.434        start      
        1            56         0.00407       0.75617     
        2            57       1.657e-006     1.283e-004   
        3            58       4.092e-009     3.005e-007   
        4            59       3.519e-011     7.467e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2813.61 psi
   
   Pressure: 
   1520.36, 1526.49, 1563.24, 1605.64, 1642.64, 1684.86, 1720.25, 1773.52, 
   1958.42, 2121.56, 2162.48, 2198.43, 2237.84, 2275.79, 2308.76, 2349.54, 
   2393.97, 2435.25, 2476.85, 2521.75, 2558.71, 2588.50, 2618.08, 2662.03, 
   2703.49
   
   Saturation:
   0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.450        start      
        1            56         0.00272       0.87147     
        2            57       9.754e-006     2.487e-004   
        3            58       1.056e-009     1.049e-006   
        4            59       3.818e-011     2.260e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2854.23 psi
   
   Pressure: 
   1527.51, 1566.07, 1616.46, 1657.33, 1693.28, 1734.65, 1769.47, 1822.01, 
   2004.70, 2166.06, 2206.57, 2242.18, 2281.24, 2318.87, 2351.58, 2392.05, 
   2436.16, 2477.16, 2518.50, 2563.13, 2599.89, 2629.53, 2658.98, 2702.77, 
   2744.13
   
   Saturation:
   0.28, 0.62, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/26/2026 8:16:50 AM
   7/26/2026 8:18:35 AM
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
        0            1          245.245        start      
        1            56         0.00318       0.28065     
        2            57       1.260e-005     2.323e-004   
        3            58       1.741e-008     4.446e-007   
        4            59       1.043e-010     4.546e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2091.61 psi
   
   Pressure: 
   1530.89, 1538.93, 1547.82, 1555.25, 1561.92, 1569.72, 1576.37, 1586.52, 
   1622.18, 1654.03, 1662.09, 1669.25, 1677.19, 1684.91, 1691.70, 1700.19, 
   1709.54, 1718.34, 1727.33, 1737.17, 1745.39, 1752.14, 1761.68, 1844.12, 
   1926.03
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.64, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          227.593        start      
        1            56         0.00312       0.32587     
        2            57       6.446e-006     1.008e-004   
        3            58       4.009e-009     2.037e-007   
        4            59       7.545e-011     1.299e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2174.71 psi
   
   Pressure: 
   1530.85, 1538.88, 1547.76, 1555.18, 1561.84, 1569.63, 1576.27, 1586.41, 
   1622.02, 1653.82, 1661.87, 1669.02, 1676.95, 1684.66, 1691.43, 1699.91, 
   1709.25, 1718.04, 1727.02, 1736.84, 1747.07, 1795.45, 1853.79, 1936.15, 
   2009.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          236.671        start      
        1            56         0.00571       0.65596     
        2            57       6.346e-006     2.911e-004   
        3            58       6.982e-009     6.489e-007   
        4            59       7.525e-011     3.899e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2273.35 psi
   
   Pressure: 
   1530.73, 1538.72, 1547.57, 1554.95, 1561.58, 1569.34, 1575.96, 1586.05, 
   1621.51, 1653.17, 1661.18, 1668.30, 1676.19, 1683.87, 1690.61, 1699.05, 
   1708.34, 1717.09, 1727.11, 1782.82, 1853.72, 1908.78, 1961.91, 2038.45, 
   2107.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.23, 0.56, 0.71, 0.75, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          255.546        start      
        1            56         0.00696       0.54211     
        2            57       1.685e-005     4.047e-004   
        3            58       6.280e-009     1.301e-006   
        4            59       8.027e-011     3.585e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2381.80 psi
   
   Pressure: 
   1530.75, 1538.75, 1547.60, 1554.99, 1561.62, 1569.38, 1576.00, 1586.09, 
   1621.57, 1653.24, 1661.26, 1668.38, 1676.27, 1683.95, 1690.70, 1699.14, 
   1708.93, 1747.90, 1825.65, 1907.38, 1973.04, 2024.86, 2075.42, 2148.95, 
   2216.32
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.51, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          256.701        start      
        1            56         0.00691       0.39017     
        2            57       2.529e-005     3.426e-004   
        3            58       1.813e-009     1.387e-006   
        4            59       9.496e-011     6.267e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2483.66 psi
   
   Pressure: 
   1530.78, 1538.79, 1547.64, 1555.04, 1561.68, 1569.44, 1576.07, 1586.17, 
   1621.68, 1653.38, 1661.41, 1668.54, 1676.44, 1684.12, 1690.98, 1714.26, 
   1794.93, 1868.89, 1941.56, 2018.83, 2081.64, 2131.59, 2180.56, 2252.17, 
   2318.24
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.42, 
   0.68, 0.74, 0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          248.326        start      
        1            56         0.00403       0.27279     
        2            57       1.413e-005     1.401e-004   
        3            58       4.821e-009     4.956e-007   
        4            59       8.485e-011     2.751e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2569.77 psi
   
   Pressure: 
   1530.81, 1538.82, 1547.68, 1555.08, 1561.73, 1569.50, 1576.13, 1586.25, 
   1621.79, 1653.52, 1661.55, 1668.69, 1676.63, 1690.80, 1747.90, 1820.06, 
   1896.89, 1966.99, 2036.81, 2111.50, 2172.47, 2221.15, 2269.01, 2339.26, 
   2404.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 0.67, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          219.695        start      
        1            56         0.00250       0.57432     
        2            57       5.355e-006     9.218e-005   
        3            58       4.987e-009     1.405e-007   
        4            59       7.134e-011     1.107e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2651.50 psi
   
   Pressure: 
   1530.63, 1538.59, 1547.40, 1554.76, 1561.36, 1569.09, 1575.68, 1585.73, 
   1621.06, 1652.61, 1660.60, 1669.71, 1727.12, 1793.29, 1849.17, 1917.19, 
   1990.59, 2058.21, 2125.91, 2198.58, 2258.06, 2305.68, 2352.64, 2421.77, 
   2486.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.75, 0.76, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.667        start      
        1            56         0.00587       0.65184     
        2            57       1.387e-005     3.094e-004   
        3            58       9.788e-009     9.321e-007   
        4            59       8.611e-011     5.575e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2724.97 psi
   
   Pressure: 
   1530.57, 1538.52, 1547.31, 1554.65, 1561.24, 1568.95, 1575.52, 1585.55, 
   1620.79, 1654.56, 1691.30, 1752.97, 1818.97, 1881.42, 1935.07, 2000.96, 
   2072.39, 2138.43, 2204.71, 2275.97, 2334.39, 2381.26, 2427.55, 2495.86, 
   2559.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.52, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          258.431        start      
        1            56         0.00627       1.75217     
        2            57       1.652e-005     8.717e-004   
        3            58       2.621e-009     2.287e-006   
        4            59       7.590e-011     3.138e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3016.07 psi
   
   Pressure: 
   1529.83, 1537.59, 1546.16, 1553.32, 1559.75, 1567.27, 1573.69, 1583.60, 
   1676.45, 1945.69, 2012.23, 2069.47, 2131.51, 2190.91, 2242.32, 2305.74, 
   2374.71, 2438.68, 2503.06, 2572.42, 2629.43, 2675.28, 2720.69, 2787.91, 
   2850.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.42, 0.69, 0.73, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          228.152        start      
        1            56         0.00297       0.58992     
        2            57       1.049e-005     1.306e-004   
        3            58       1.824e-008     2.590e-007   
        4            59       9.149e-011     5.472e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3284.87 psi
   
   Pressure: 
   1530.57, 1538.51, 1547.29, 1554.63, 1561.22, 1568.94, 1579.02, 1659.79, 
   1963.37, 2224.20, 2288.87, 2345.20, 2406.58, 2465.47, 2516.49, 2579.43, 
   2647.86, 2711.32, 2775.13, 2843.88, 2900.36, 2945.78, 2990.76, 3057.38, 
   3119.96
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.64, 
   0.72, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          224.906        start      
        1            56         0.00362       0.60216     
        2            57       1.491e-006     1.081e-004   
        3            58       2.347e-009     1.787e-007   
        4            59       6.149e-011     1.821e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3341.29 psi
   
   Pressure: 
   1530.53, 1538.46, 1547.23, 1554.56, 1562.19, 1610.33, 1667.34, 1751.55, 
   2039.78, 2291.99, 2354.98, 2410.14, 2470.45, 2528.41, 2578.70, 2640.81, 
   2708.40, 2771.11, 2834.24, 2902.28, 2958.24, 3003.27, 3047.91, 3114.11, 
   3176.42
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 
   0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          249.885        start      
        1            56         0.00694       0.50810     
        2            57       2.138e-005     4.096e-004   
        3            58       6.091e-009     1.759e-006   
        4            59       5.944e-011     5.326e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3403.22 psi
   
   Pressure: 
   1530.57, 1538.52, 1547.66, 1575.99, 1633.45, 1698.88, 1753.14, 1834.40, 
   2115.43, 2362.75, 2424.69, 2479.05, 2538.55, 2595.82, 2645.53, 2706.97, 
   2773.85, 2835.96, 2898.50, 2965.95, 3021.45, 3066.15, 3110.50, 3176.33, 
   3238.39
   
   Saturation:
   0.20, 0.20, 0.21, 0.49, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          255.295        start      
        1            56         0.00503       0.49570     
        2            57       1.839e-005     2.865e-004   
        3            58       2.741e-008     1.594e-006   
        4            59       8.562e-011     2.218e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3486.92 psi
   
   Pressure: 
   1530.85, 1548.93, 1624.80, 1687.54, 1742.35, 1805.12, 1857.81, 1937.17, 
   2212.64, 2455.64, 2516.59, 2570.14, 2628.82, 2685.33, 2734.42, 2795.14, 
   2861.27, 2922.72, 2984.64, 3051.46, 3106.48, 3150.84, 3194.88, 3260.34, 
   3322.14
   
   Saturation:
   0.20, 0.39, 0.68, 0.73, 0.75, 0.75, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          112.277        start      
        1            56         0.00505       4.56533     
        2            57       5.363e-006      0.00306     
        3            58       5.015e-009     9.749e-007   
        4            59       4.685e-011     1.351e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3668.45 psi
   
   Pressure: 
   1737.63, 1803.24, 1873.42, 1930.92, 1981.84, 2040.71, 2090.42, 2165.61, 
   2427.56, 2659.42, 2717.73, 2769.11, 2825.57, 2880.08, 2927.57, 2986.44, 
   3050.71, 3110.59, 3171.08, 3236.52, 3290.52, 3334.18, 3377.64, 3442.42, 
   3503.78
   
   Saturation:
   0.64, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          11.7870        start      
        1            56       8.585e-004      2.19406     
        2            57       1.292e-006      0.00108     
        3            58       3.633e-010     5.107e-007   
        4            59       1.051e-011     2.387e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3732.21 psi
   
   Pressure: 
   1761.02, 1827.43, 1899.55, 1959.00, 2011.76, 2072.80, 2124.33, 2202.17, 
   2472.88, 2712.01, 2772.03, 2824.80, 2882.64, 2938.36, 2986.79, 3046.67, 
   3111.92, 3172.56, 3233.69, 3299.67, 3354.02, 3397.86, 3441.43, 3506.26, 
   3567.58
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.12986        start      
        1            56       7.741e-004      4.40934     
        2            57       1.499e-006      0.00227     
        3            58       8.908e-010     1.074e-006   
        4            59       8.471e-012     1.219e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3705.50 psi
   
   Pressure: 
   1751.74, 1816.40, 1887.06, 1945.49, 1997.46, 2057.69, 2108.59, 2185.55, 
   2453.42, 2690.21, 2749.68, 2801.99, 2859.36, 2914.65, 2962.71, 3022.19, 
   3087.02, 3147.29, 3208.08, 3273.73, 3327.84, 3371.51, 3414.94, 3479.61, 
   3540.86
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.51894        start      
        1            56       5.210e-004      5.18317     
        2            57       9.999e-007      0.00257     
        3            58       6.453e-010     1.187e-006   
        4            59       8.922e-012     1.165e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3686.34 psi
   
   Pressure: 
   1746.67, 1810.24, 1879.88, 1937.56, 1988.93, 2048.50, 2098.90, 2175.13, 
   2440.62, 2675.43, 2734.41, 2786.32, 2843.28, 2898.19, 2945.94, 3005.06, 
   3069.51, 3129.46, 3189.95, 3255.30, 3309.18, 3352.70, 3396.00, 3460.53, 
   3521.69
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.74462        start      
        1            56       5.484e-004      6.73135     
        2            57       1.289e-006      0.00407     
        3            58       1.016e-009     2.300e-006   
        4            59       8.175e-012     2.616e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3670.82 psi
   
   Pressure: 
   1743.17, 1805.94, 1874.80, 1931.88, 1982.74, 2041.78, 2091.73, 2167.34, 
   2430.77, 2663.83, 2722.40, 2773.96, 2830.54, 2885.11, 2932.59, 2991.38, 
   3055.49, 3115.15, 3175.36, 3240.44, 3294.12, 3337.50, 3380.68, 3445.07, 
   3506.16
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.26854        start      
        1            56       4.759e-004      7.36229     
        2            57       1.196e-006      0.00451     
        3            58       9.469e-010     2.769e-006   
        4            59       6.789e-012     2.861e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3657.46 psi
   
   Pressure: 
   1740.45, 1802.58, 1870.78, 1927.35, 1977.78, 2036.34, 2085.92, 2160.97, 
   2422.56, 2654.05, 2712.25, 2763.48, 2819.74, 2874.00, 2921.22, 2979.71, 
   3043.52, 3102.91, 3162.87, 3227.70, 3281.20, 3324.44, 3367.51, 3431.77, 
   3492.78
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.89558        start      
        1            56       4.475e-004      8.15981     
        2            57       1.241e-006      0.00519     
        3            58       1.024e-009     3.518e-006   
        4            59       7.418e-012     3.660e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3645.61 psi
   
   Pressure: 
   1738.20, 1799.78, 1867.41, 1923.54, 1973.59, 2031.73, 2080.96, 2155.52, 
   2415.43, 2645.51, 2703.35, 2754.30, 2810.25, 2864.23, 2911.22, 2969.43, 
   3032.95, 3092.10, 3151.83, 3216.43, 3269.75, 3312.87, 3355.84, 3419.99, 
   3480.93
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 8:19:34 AM
   7/26/2026 8:21:14 AM
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
        0            1          354.866        start      
        1            56         0.00751       0.35144     
        2            57       1.297e-005     3.401e-004   
        3            58       5.690e-009     8.188e-007   
        4            59       1.013e-010     2.466e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2322.12 psi
   
   Pressure: 
   1541.22, 1551.95, 1563.81, 1573.72, 1582.62, 1593.03, 1601.91, 1615.45, 
   1663.03, 1705.50, 1716.26, 1725.81, 1736.39, 1746.69, 1755.74, 1767.06, 
   1779.53, 1791.26, 1803.25, 1816.36, 1827.31, 1836.86, 1878.41, 1997.80, 
   2101.42
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.52, 0.72, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          320.892        start      
        1            56         0.00382       0.35199     
        2            57       1.149e-005     1.591e-004   
        3            58       5.466e-009     3.812e-007   
        4            59       1.386e-010     2.476e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2481.01 psi
   
   Pressure: 
   1541.10, 1551.80, 1563.63, 1573.51, 1582.38, 1592.76, 1601.61, 1615.12, 
   1662.56, 1704.93, 1715.65, 1725.18, 1735.74, 1746.01, 1755.04, 1766.34, 
   1778.78, 1790.49, 1802.49, 1823.95, 1914.14, 1990.00, 2062.67, 2166.71, 
   2260.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.32, 0.65, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          342.692        start      
        1            56         0.00711       0.59387     
        2            57       1.452e-005     3.723e-004   
        3            58       5.101e-009     9.446e-007   
        4            59       1.248e-010     2.609e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2673.56 psi
   
   Pressure: 
   1540.98, 1551.64, 1563.43, 1573.28, 1582.12, 1592.46, 1601.28, 1614.73, 
   1662.00, 1704.20, 1714.89, 1724.38, 1734.89, 1745.12, 1754.10, 1765.34, 
   1778.35, 1829.45, 1933.07, 2041.87, 2129.26, 2198.24, 2265.54, 2363.42, 
   2453.11
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.51, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          297.826        start      
        1            56         0.00274       0.56344     
        2            57       7.327e-006     1.067e-004   
        3            58       7.294e-009     1.550e-007   
        4            59       1.526e-010     1.685e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2858.45 psi
   
   Pressure: 
   1540.84, 1551.47, 1563.22, 1573.04, 1581.85, 1592.16, 1600.95, 1614.37, 
   1661.51, 1703.60, 1714.26, 1723.73, 1734.22, 1744.44, 1756.83, 1844.24, 
   1951.01, 2047.07, 2142.06, 2243.37, 2325.88, 2391.62, 2456.18, 2550.72, 
   2638.15
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.63, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          341.994        start      
        1            56         0.00553       0.38032     
        2            57       1.632e-005     2.168e-004   
        3            58       1.621e-009     6.655e-007   
        4            59       9.345e-011     7.332e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2990.63 psi
   
   Pressure: 
   1540.98, 1551.64, 1563.43, 1573.27, 1582.11, 1592.45, 1601.27, 1614.73, 
   1662.00, 1704.20, 1714.89, 1724.47, 1749.69, 1838.10, 1914.34, 2006.52, 
   2105.65, 2196.73, 2287.79, 2385.40, 2465.22, 2529.06, 2591.95, 2684.42, 
   2770.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.40, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          332.712        start      
        1            56         0.00610       0.67481     
        2            57       1.266e-005     2.915e-004   
        3            58       7.115e-009     7.203e-007   
        4            59       1.182e-010     3.527e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3127.37 psi
   
   Pressure: 
   1540.69, 1551.27, 1562.97, 1572.74, 1581.51, 1591.78, 1600.53, 1613.87, 
   1660.78, 1705.28, 1752.08, 1834.25, 1922.09, 2005.18, 2076.54, 2164.18, 
   2259.18, 2347.01, 2435.16, 2529.92, 2607.63, 2669.95, 2731.52, 2822.38, 
   2907.30
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          279.575        start      
        1            56         0.00342       3.05966     
        2            57       2.463e-006     3.305e-004   
        3            58       1.625e-009     2.459e-007   
        4            59       1.336e-010     7.035e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3657.87 psi
   
   Pressure: 
   1538.24, 1548.18, 1559.18, 1568.37, 1576.61, 1586.26, 1594.50, 1610.00, 
   1924.49, 2265.95, 2349.87, 2422.63, 2501.91, 2578.08, 2644.22, 2726.04, 
   2815.25, 2898.22, 2981.91, 3072.31, 3146.79, 3206.84, 3266.47, 3354.97, 
   3438.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.61, 0.72, 0.74, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          324.730        start      
        1            56         0.00381       0.38501     
        2            57       1.107e-005     1.276e-004   
        3            58       2.065e-009     3.830e-007   
        4            59       1.453e-010     1.238e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3919.71 psi
   
   Pressure: 
   1540.85, 1551.47, 1563.22, 1573.03, 1581.87, 1600.42, 1674.58, 1789.69, 
   2180.17, 2519.91, 2604.55, 2678.51, 2759.25, 2836.79, 2904.00, 2986.94, 
   3077.15, 3160.80, 3244.96, 3335.63, 3410.15, 3470.10, 3529.50, 3617.53, 
   3700.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          338.383        start      
        1            56         0.00688       0.51309     
        2            57       1.932e-005     3.534e-004   
        3            58       4.834e-009     1.272e-006   
        4            59       1.354e-010     2.987e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4023.33 psi
   
   Pressure: 
   1540.67, 1551.24, 1563.25, 1595.93, 1672.41, 1759.64, 1831.89, 1939.99, 
   2313.56, 2642.11, 2724.38, 2796.54, 2875.53, 2951.53, 3017.50, 3099.02, 
   3187.76, 3270.16, 3353.13, 3442.61, 3516.23, 3575.52, 3634.36, 3721.69, 
   3804.03
   
   Saturation:
   0.20, 0.20, 0.21, 0.46, 0.69, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          298.156        start      
        1            56         0.00372       0.93162     
        2            57       1.650e-006     1.687e-004   
        3            58       1.834e-009     4.263e-007   
        4            59       1.049e-010     9.131e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4163.11 psi
   
   Pressure: 
   1546.92, 1611.91, 1712.34, 1793.56, 1864.89, 1946.90, 2015.90, 2119.97, 
   2481.55, 2800.76, 2880.87, 2951.30, 3028.52, 3102.93, 3167.61, 3247.63, 
   3334.84, 3415.90, 3497.64, 3585.87, 3658.56, 3717.19, 3775.44, 3862.07, 
   3943.94
   
   Saturation:
   0.24, 0.58, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          26.7039        start      
        1            56         0.00104       0.38433     
        2            57       2.005e-006     7.630e-004   
        3            58       1.419e-010     1.350e-006   
        4            59       2.811e-011     6.952e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4472.88 psi
   
   Pressure: 
   1854.37, 1943.76, 2040.28, 2119.62, 2189.92, 2271.15, 2339.65, 2443.07, 
   2802.45, 3119.70, 3199.31, 3269.28, 3345.99, 3419.86, 3484.06, 3563.45, 
   3649.95, 3730.33, 3811.36, 3898.82, 3970.87, 4028.99, 4086.75, 4172.68, 
   4253.98
   
   Saturation:
   0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.28911        start      
        1            56       8.387e-004      5.19363     
        2            57       1.372e-006      0.00203     
        3            58       6.928e-010     7.385e-007   
        4            59       1.072e-011     7.277e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4432.54 psi
   
   Pressure: 
   1835.98, 1922.23, 2016.41, 2094.27, 2163.48, 2243.66, 2311.41, 2413.80, 
   2770.01, 3084.73, 3163.75, 3233.24, 3309.44, 3382.86, 3446.67, 3525.62, 
   3611.66, 3691.64, 3772.29, 3859.37, 3931.13, 3989.04, 4046.63, 4132.38, 
   4213.60
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.61426        start      
        1            56       6.244e-004      6.98483     
        2            57       1.136e-006      0.00306     
        3            58       7.326e-010     1.149e-006   
        4            59       8.234e-012     1.203e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4399.32 psi
   
   Pressure: 
   1827.07, 1911.39, 2003.78, 2080.31, 2148.45, 2227.49, 2294.34, 2395.46, 
   2747.52, 3058.79, 3136.98, 3205.79, 3281.28, 3354.05, 3417.34, 3495.67, 
   3581.08, 3660.52, 3740.67, 3827.26, 3898.66, 3956.33, 4013.71, 4099.24, 
   4180.35
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.43247        start      
        1            56       5.452e-004      8.61485     
        2            57       1.159e-006      0.00429     
        3            58       8.437e-010     1.869e-006   
        4            59       9.927e-012     1.909e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4373.95 psi
   
   Pressure: 
   1821.45, 1904.47, 1995.56, 2071.09, 2138.40, 2216.54, 2282.66, 2382.74, 
   2731.37, 3039.75, 3117.25, 3185.48, 3260.36, 3332.58, 3395.41, 3473.21, 
   3558.07, 3637.04, 3716.75, 3802.91, 3873.99, 3931.43, 3988.63, 4073.96, 
   4154.96
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.81744        start      
        1            56       4.581e-004      9.27715     
        2            57       1.040e-006      0.00463     
        3            58       7.544e-010     2.171e-006   
        4            59       6.732e-012     1.961e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4352.76 psi
   
   Pressure: 
   1817.24, 1899.24, 1989.29, 2064.01, 2130.63, 2208.00, 2273.51, 2372.70, 
   2718.35, 3024.22, 3101.11, 3168.82, 3243.17, 3314.90, 3377.32, 3454.65, 
   3539.02, 3617.57, 3696.88, 3782.65, 3853.44, 3910.68, 3967.72, 4052.85, 
   4133.75
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.33164        start      
        1            56       4.162e-004      10.1150     
        2            57       1.037e-006      0.00516     
        3            58       7.769e-010     2.631e-006   
        4            59       9.872e-012     2.341e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4334.37 psi
   
   Pressure: 
   1813.81, 1894.98, 1984.15, 2058.17, 2124.19, 2200.90, 2265.87, 2364.28, 
   2707.29, 3010.93, 3087.28, 3154.54, 3228.40, 3299.69, 3361.75, 3438.65, 
   3522.58, 3600.74, 3679.70, 3765.11, 3835.64, 3892.70, 3949.58, 4034.54, 
   4115.34
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.94640        start      
        1            56       4.068e-004      11.1656     
        2            57       1.124e-006      0.00589     
        3            58       9.101e-010     3.261e-006   
        4            59       8.848e-012     3.252e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4318.05 psi
   
   Pressure: 
   1810.90, 1891.34, 1979.75, 2053.16, 2118.66, 2194.78, 2259.27, 2356.97, 
   2697.63, 2999.25, 3075.12, 3141.96, 3215.39, 3286.27, 3348.00, 3424.51, 
   3508.04, 3585.85, 3664.49, 3749.58, 3819.86, 3876.75, 3933.49, 4018.30, 
   4099.01
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.64936        start      
        1            56       4.303e-004      12.5196     
        2            57       1.328e-006      0.00701     
        3            58       1.221e-009     4.125e-006   
        4            59       9.309e-012     5.187e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4303.36 psi
   
   Pressure: 
   1808.35, 1888.15, 1975.89, 2048.75, 2113.78, 2189.38, 2253.43, 2350.50, 
   2689.01, 2988.81, 3064.23, 3130.69, 3203.73, 3274.25, 3335.67, 3411.83, 
   3494.99, 3572.48, 3650.82, 3735.61, 3805.68, 3862.41, 3919.02, 4003.68, 
   4084.31
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.42723        start      
        1            56       4.925e-004      14.2872     
        2            57       1.705e-006      0.00876     
        3            58       1.867e-009     5.292e-006   
        4            59       1.455e-011     9.401e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4290.01 psi
   
   Pressure: 
   1806.08, 1885.31, 1972.44, 2044.81, 2109.42, 2184.53, 2248.20, 2344.68, 
   2681.24, 2979.37, 3054.38, 3120.50, 3193.17, 3263.35, 3324.50, 3400.33, 
   3483.15, 3560.35, 3638.41, 3722.93, 3792.79, 3849.38, 3905.87, 3990.39, 
   4070.94
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.24995        start      
        1            56       3.936e-004      13.2651     
        2            57       1.235e-006      0.00703     
        3            58       1.231e-009     3.917e-006   
        4            59       1.349e-011     5.875e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4277.78 psi
   
   Pressure: 
   1804.02, 1882.74, 1969.32, 2041.25, 2105.46, 2180.14, 2243.44, 2339.40, 
   2674.16, 2970.75, 3045.39, 3111.19, 3183.53, 3253.40, 3314.29, 3389.82, 
   3472.32, 3549.25, 3627.06, 3711.32, 3781.00, 3837.46, 3893.84, 3978.23, 
   4058.70
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 8:22:23 AM
   7/26/2026 8:24:05 AM
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
        0            1          435.422        start      
        1            56         0.00745       0.24793     
        2            57       2.210e-005     2.316e-004   
        3            58       2.885e-009     6.976e-007   
        4            59       1.144e-010     1.247e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2574.59 psi
   
   Pressure: 
   1551.58, 1565.01, 1579.86, 1592.26, 1603.40, 1616.42, 1627.54, 1644.48, 
   1704.03, 1757.19, 1770.65, 1782.61, 1795.85, 1808.74, 1820.07, 1834.24, 
   1849.85, 1864.54, 1879.55, 1895.96, 1909.76, 1933.58, 2031.31, 2174.03, 
   2298.89
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.37, 0.68, 0.75, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          435.034        start      
        1            56         0.00932       0.51141     
        2            57       2.758e-005     4.152e-004   
        3            58       5.045e-009     1.257e-006   
        4            59       1.329e-010     2.265e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2850.09 psi
   
   Pressure: 
   1551.34, 1564.69, 1579.47, 1591.81, 1602.89, 1615.85, 1626.90, 1643.76, 
   1703.00, 1755.89, 1769.28, 1781.18, 1794.36, 1807.18, 1818.45, 1832.54, 
   1848.06, 1862.85, 1902.25, 2044.07, 2158.96, 2248.45, 2335.19, 2460.58, 
   2574.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.42, 0.69, 0.74, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          427.367        start      
        1            56         0.00845       0.53210     
        2            57       2.508e-005     3.590e-004   
        3            58       4.081e-009     1.089e-006   
        4            59       1.373e-010     1.724e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3133.14 psi
   
   Pressure: 
   1551.20, 1564.52, 1579.26, 1591.57, 1602.62, 1615.54, 1626.56, 1643.38, 
   1702.46, 1755.20, 1768.56, 1780.42, 1793.56, 1806.35, 1817.73, 1854.54, 
   1988.76, 2111.74, 2232.46, 2360.80, 2465.09, 2548.04, 2629.37, 2748.28, 
   2858.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.68, 0.74, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          425.589        start      
        1            56         0.00679       0.47745     
        2            57       1.966e-005     2.505e-004   
        3            58       2.770e-009     7.306e-007   
        4            59       1.669e-010     1.147e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3358.87 psi
   
   Pressure: 
   1551.15, 1564.46, 1579.17, 1591.47, 1602.51, 1615.42, 1626.43, 1643.22, 
   1702.24, 1754.93, 1768.27, 1780.22, 1810.64, 1920.94, 2016.13, 2131.16, 
   2254.83, 2368.44, 2482.00, 2603.74, 2703.29, 2782.92, 2861.35, 2976.69, 
   3083.98
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          390.601        start      
        1            56         0.00601       1.37256     
        2            57       1.302e-005     5.207e-004   
        3            58       2.780e-009     9.303e-007   
        4            59       1.574e-010     1.994e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3596.95 psi
   
   Pressure: 
   1550.00, 1563.00, 1577.38, 1589.40, 1600.18, 1612.79, 1623.55, 1639.95, 
   1697.78, 1793.59, 1903.03, 2002.46, 2108.91, 2210.17, 2297.45, 2404.89, 
   2521.54, 2629.57, 2738.13, 2854.98, 2950.91, 3027.97, 3104.18, 3216.83, 
   3322.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.35, 0.66, 0.73, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          363.389        start      
        1            56         0.00313       1.10067     
        2            57       6.102e-006     1.013e-004   
        3            58       6.638e-009     1.235e-007   
        4            59       1.459e-010     1.333e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4427.66 psi
   
   Pressure: 
   1550.21, 1563.27, 1577.71, 1589.78, 1600.61, 1613.29, 1627.68, 1751.76, 
   2252.61, 2681.46, 2787.65, 2880.06, 2980.76, 3077.39, 3161.10, 3264.43, 
   3376.81, 3481.06, 3585.97, 3699.02, 3791.96, 3866.74, 3940.85, 4050.67, 
   4153.93
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 
   0.72, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          363.634        start      
        1            56         0.00434       0.47564     
        2            57       1.824e-006     1.211e-004   
        3            58       2.353e-009     1.639e-007   
        4            59       1.382e-010     1.125e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1549.04, 1561.79, 1575.90, 1589.56, 1654.86, 1762.56, 1851.12, 1982.94, 
   2437.04, 2835.57, 2935.24, 3022.58, 3118.10, 3209.94, 3289.62, 3388.04, 
   3495.12, 3594.48, 3694.49, 3802.28, 3890.92, 3962.27, 4033.01, 4137.94, 
   4236.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          388.438        start      
        1            56         0.00603       0.47793     
        2            57       1.205e-005     4.222e-004   
        3            58       8.188e-009     1.403e-006   
        4            59       1.939e-010     1.012e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1547.79, 1593.03, 1708.71, 1802.41, 1884.42, 1978.51, 2057.56, 2176.67, 
   2590.13, 2954.79, 3046.23, 3126.55, 3214.56, 3299.29, 3372.88, 3463.88, 
   3562.97, 3655.03, 3747.77, 3847.82, 3930.18, 3996.56, 4062.46, 4160.39, 
   4252.86
   
   Saturation:
   0.21, 0.48, 0.70, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          29.5212        start      
        1            56       8.720e-004      0.48666     
        2            57       1.068e-006     5.957e-004   
        3            58       9.257e-011     6.176e-007   
        4            59       1.780e-011     4.079e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1858.39, 1948.69, 2046.09, 2126.13, 2197.02, 2278.93, 2348.01, 2452.32, 
   2814.94, 3135.11, 3215.45, 3286.07, 3363.48, 3438.04, 3502.82, 3582.96, 
   3670.26, 3751.41, 3833.20, 3921.50, 3994.23, 4052.90, 4111.21, 4197.95, 
   4280.01
   
   Saturation:
   0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.64778        start      
        1            56       7.116e-004      1.13118     
        2            57       1.169e-006      0.00123     
        3            58       3.249e-010     1.300e-006   
        4            59       8.618e-012     2.764e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1843.11, 1931.17, 2027.34, 2106.84, 2177.52, 2259.43, 2328.65, 2433.30, 
   2797.47, 3119.28, 3200.10, 3271.19, 3349.16, 3424.31, 3489.64, 3570.50, 
   3658.64, 3740.60, 3823.27, 3912.55, 3986.15, 4045.56, 4104.65, 4192.65, 
   4276.00
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.79835        start      
        1            56       6.003e-004      1.31223     
        2            57       1.057e-006      0.00153     
        3            58       3.004e-010     1.826e-006   
        4            59       1.013e-011     4.031e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1837.84, 1924.94, 2020.40, 2099.48, 2169.90, 2251.61, 2320.73, 2425.31, 
   2789.44, 3111.42, 3192.32, 3263.52, 3341.65, 3416.98, 3482.51, 3563.64, 
   3652.12, 3734.43, 3817.49, 3907.25, 3981.27, 4041.06, 4100.58, 4189.29, 
   4273.43
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.67600        start      
        1            56       6.641e-004      1.54218     
        2            57       1.317e-006      0.00207     
        3            58       3.852e-010     2.942e-006   
        4            59       7.936e-012     6.882e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1835.03, 1921.57, 2016.54, 2095.31, 2165.51, 2247.01, 2316.00, 2420.43, 
   2784.20, 3106.02, 3186.90, 3258.12, 3336.30, 3411.72, 3477.34, 3558.62, 
   3647.28, 3729.80, 3813.12, 3903.18, 3977.50, 4037.57, 4097.39, 4186.64, 
   4271.37
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.99961        start      
        1            56       6.169e-004      1.59285     
        2            57       1.198e-006      0.00211     
        3            58       3.167e-010     3.041e-006   
        4            59       9.912e-012     6.317e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1833.17, 1919.31, 2013.91, 2092.42, 2162.42, 2243.74, 2312.59, 2416.86, 
   2780.21, 3101.76, 3182.61, 3253.81, 3332.01, 3407.45, 3473.13, 3554.50, 
   3643.29, 3725.97, 3809.47, 3899.77, 3974.31, 4034.60, 4094.68, 4184.37, 
   4269.60
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.51224        start      
        1            56       6.484e-004      1.71735     
        2            57       1.301e-006      0.00236     
        3            58       3.466e-010     3.641e-006   
        4            59       1.070e-011     7.744e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1831.77, 1917.59, 2011.89, 2090.18, 2160.01, 2241.16, 2309.89, 2414.00, 
   2776.92, 3098.18, 3178.97, 3250.15, 3328.34, 3403.81, 3469.52, 3550.95, 
   3639.84, 3722.63, 3806.28, 3896.78, 3971.52, 4031.99, 4092.29, 4182.36, 
   4268.03
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.12815        start      
        1            56       7.281e-004      1.89528     
        2            57       1.551e-006      0.00279     
        3            58       4.476e-010     4.680e-006   
        4            59       7.176e-012     1.098e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1830.63, 1916.18, 2010.23, 2088.32, 2158.01, 2239.00, 2307.62, 2411.58, 
   2774.08, 3095.05, 3175.79, 3246.94, 3325.12, 3400.59, 3466.32, 3547.80, 
   3636.77, 3719.66, 3803.44, 3894.11, 3969.01, 4029.65, 4090.14, 4180.55, 
   4266.61
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.81378        start      
        1            56       5.501e-004      1.70566     
        2            57       1.001e-006      0.00211     
        3            58       2.430e-010     3.061e-006   
        4            59       6.445e-012     5.734e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1829.66, 1914.99, 2008.80, 2086.73, 2156.28, 2237.13, 2305.65, 2409.47, 
   2771.57, 3092.26, 3172.95, 3244.06, 3322.22, 3397.69, 3463.44, 3544.96, 
   3633.99, 3716.97, 3800.86, 3891.68, 3966.73, 4027.52, 4088.17, 4178.90, 
   4265.32
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.59856        start      
        1            56       6.959e-004      1.96634     
        2            57       1.425e-006      0.00277     
        3            58       4.178e-010     4.593e-006   
        4            59       6.587e-012     1.082e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1828.81, 1913.94, 2007.55, 2085.32, 2154.75, 2235.47, 2303.89, 2407.59, 
   2769.31, 3089.74, 3170.36, 3241.45, 3319.58, 3395.05, 3460.81, 3542.36, 
   3631.45, 3714.51, 3798.50, 3889.45, 3964.64, 4025.56, 4086.37, 4177.38, 
   4264.12
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.36060        start      
        1            56       5.473e-004      1.79520     
        2            57       9.777e-007      0.00218     
        3            58       2.537e-010     3.171e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1828.05, 1912.99, 2006.43, 2084.06, 2153.37, 2233.97, 2302.30, 2405.89, 
   2767.25, 3087.42, 3168.00, 3239.04, 3317.16, 3392.62, 3458.39, 3539.97, 
   3629.11, 3712.23, 3796.31, 3887.39, 3962.70, 4023.74, 4084.71, 4175.97, 
   4263.01
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.19910        start      
        1            56       7.135e-004      2.09180     
        2            57       1.450e-006      0.00293     
        3            58       4.699e-010     4.900e-006   
        4            59       1.133e-011     1.295e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1827.36, 1912.14, 2005.41, 2082.91, 2152.11, 2232.61, 2300.86, 2404.33, 
   2765.36, 3085.28, 3165.81, 3236.83, 3314.92, 3390.37, 3456.15, 3537.76, 
   3626.94, 3710.13, 3794.29, 3885.48, 3960.91, 4022.06, 4083.16, 4174.66, 
   4261.98
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.04931        start      
        1            56       6.014e-004      1.95972     
        2            57       1.109e-006      0.00246     
        3            58       3.318e-010     3.756e-006   
        4            59       8.071e-012     8.976e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1826.73, 1911.36, 2004.47, 2081.85, 2150.96, 2231.35, 2299.52, 2402.89, 
   2763.61, 3083.29, 3163.77, 3234.76, 3312.83, 3388.28, 3454.06, 3535.69, 
   3624.92, 3708.16, 3792.40, 3883.69, 3959.23, 4020.49, 4081.71, 4173.44, 
   4261.02
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/26/2026 8:25:14 AM
   7/26/2026 8:26:51 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

