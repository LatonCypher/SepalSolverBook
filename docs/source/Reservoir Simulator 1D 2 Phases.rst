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
        0            1          195.816        start      
        1            56         0.00221       5.65178     
        2            57       1.106e-006     6.846e-004   
        3            58       2.163e-009     1.509e-007   
        4            59       8.015e-011     1.224e-010   
   Producer BHP: 
   2264.29 psi
   
   Injector BHP: 
   2646.08 psi
   
   Pressure: 
   2301.98, 2309.67, 2318.77, 2328.46, 2335.12, 2340.22, 2345.88, 2353.41, 
   2360.45, 2365.81, 2372.67, 2380.33, 2386.92, 2392.79, 2398.47, 2404.36, 
   2413.69, 2423.27, 2429.49, 2442.49, 2456.02, 2462.53, 2467.46, 2478.64, 
   2526.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          184.013        start      
        1            56         0.00277       6.04615     
        2            57       1.946e-006     6.408e-004   
        3            58       4.866e-009     1.018e-007   
        4            59       6.699e-011     2.991e-010   
   Producer BHP: 
   1611.34 psi
   
   Injector BHP: 
   2033.39 psi
   
   Pressure: 
   1649.12, 1656.82, 1665.92, 1675.61, 1682.26, 1687.36, 1693.01, 1700.52, 
   1707.53, 1712.87, 1719.68, 1727.30, 1733.84, 1739.67, 1745.30, 1751.13, 
   1760.35, 1769.82, 1775.96, 1788.78, 1802.11, 1808.75, 1825.71, 1869.71, 
   1913.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.47, 0.70, 
   0.76
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          155.581        start      
        1            56         0.00233       0.31703     
        2            57       9.639e-007     1.093e-004   
        3            58       1.794e-009     1.511e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1934.22 psi
   
   Pressure: 
   1527.21, 1532.82, 1539.52, 1546.74, 1551.77, 1555.66, 1560.03, 1565.90, 
   1571.46, 1575.74, 1581.29, 1587.56, 1593.03, 1597.96, 1602.79, 1607.87, 
   1616.02, 1624.51, 1630.10, 1641.95, 1656.21, 1693.12, 1733.32, 1773.64, 
   1814.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.57, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.282        start      
        1            56         0.00306       0.68400     
        2            57       1.306e-005     3.294e-004   
        3            58       1.869e-008     7.562e-007   
        4            59       5.234e-011     1.220e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2031.39 psi
   
   Pressure: 
   1527.14, 1532.73, 1539.41, 1546.62, 1551.63, 1555.51, 1559.86, 1565.72, 
   1571.26, 1575.54, 1581.07, 1587.33, 1592.78, 1597.70, 1602.52, 1607.59, 
   1615.72, 1624.19, 1629.77, 1646.13, 1744.61, 1796.70, 1834.38, 1872.48, 
   1911.29
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.28, 0.64, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.044        start      
        1            56         0.00435       0.35104     
        2            57       2.273e-005     2.528e-004   
        3            58       5.939e-008     8.537e-007   
        4            59       7.376e-011     2.663e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2120.55 psi
   
   Pressure: 
   1527.22, 1532.82, 1539.53, 1546.75, 1551.77, 1555.67, 1560.04, 1565.91, 
   1571.47, 1575.75, 1581.30, 1587.58, 1593.04, 1597.98, 1602.81, 1607.89, 
   1616.05, 1624.57, 1634.44, 1734.37, 1840.53, 1889.87, 1925.94, 1962.70, 
   2000.48
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.34, 0.66, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          173.373        start      
        1            56         0.00627       0.35188     
        2            57       2.640e-005     4.587e-004   
        3            58       9.657e-009     2.205e-006   
        4            59       4.545e-011     5.295e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2161.51 psi
   
   Pressure: 
   1527.22, 1532.82, 1539.52, 1546.74, 1551.77, 1555.66, 1560.03, 1565.90, 
   1571.45, 1575.74, 1581.28, 1587.55, 1593.02, 1597.95, 1602.78, 1607.86, 
   1616.10, 1636.69, 1684.79, 1784.98, 1886.05, 1933.58, 1968.54, 2004.37, 
   2041.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.40, 0.68, 0.74, 0.76, 0.77, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.177        start      
        1            56         0.00811       0.59307     
        2            57       3.249e-005     7.109e-004   
        3            58       3.259e-009     3.521e-006   
        4            59       3.416e-011     1.826e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2223.54 psi
   
   Pressure: 
   1527.14, 1532.73, 1539.41, 1546.61, 1551.62, 1555.50, 1559.85, 1565.70, 
   1571.24, 1575.51, 1581.04, 1587.29, 1592.74, 1597.66, 1602.47, 1607.71, 
   1637.61, 1711.44, 1758.30, 1853.94, 1951.58, 1997.78, 2031.88, 2066.99, 
   2103.51
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 
   0.48, 0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.535        start      
        1            56         0.00579       0.45995     
        2            57       1.151e-005     2.731e-004   
        3            58       8.435e-009     1.121e-006   
        4            59       5.427e-011     4.375e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2274.92 psi
   
   Pressure: 
   1527.17, 1532.76, 1539.45, 1546.66, 1551.67, 1555.55, 1559.91, 1565.77, 
   1571.32, 1575.59, 1581.12, 1587.38, 1592.83, 1597.76, 1603.07, 1630.37, 
   1701.11, 1772.09, 1817.19, 1910.10, 2005.47, 2050.74, 2084.23, 2118.80, 
   2154.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 0.55, 
   0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.096        start      
        1            56         0.00319       0.43155     
        2            57       4.870e-006     6.961e-005   
        3            58       8.252e-009     1.202e-007   
        4            59       4.737e-011     2.776e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2306.93 psi
   
   Pressure: 
   1527.16, 1532.76, 1539.44, 1546.65, 1551.66, 1555.54, 1559.90, 1565.76, 
   1571.30, 1575.57, 1581.11, 1587.37, 1592.82, 1598.91, 1631.12, 1674.91, 
   1742.60, 1811.10, 1855.01, 1945.91, 2039.51, 2084.02, 2117.02, 2151.15, 
   2186.93
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 
   0.74, 0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.498        start      
        1            56         0.00323       0.33096     
        2            57       1.498e-005     1.641e-004   
        3            58       3.082e-008     4.284e-007   
        4            59       5.041e-011     1.231e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2341.69 psi
   
   Pressure: 
   1527.19, 1532.79, 1539.48, 1546.69, 1551.71, 1555.59, 1559.95, 1565.82, 
   1571.37, 1575.64, 1581.18, 1587.46, 1595.73, 1635.14, 1676.71, 1718.77, 
   1784.43, 1851.32, 1894.38, 1983.74, 2075.93, 2119.84, 2152.43, 2186.21, 
   2221.71
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.30, 0.64, 0.72, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.536        start      
        1            56         0.00551       0.36008     
        2            57       2.736e-005     2.648e-004   
        3            58       1.055e-008     1.499e-006   
        4            59       3.722e-011     3.940e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2378.30 psi
   
   Pressure: 
   1527.18, 1532.77, 1539.46, 1546.67, 1551.68, 1555.56, 1559.92, 1565.78, 
   1571.32, 1575.60, 1581.17, 1593.96, 1639.94, 1682.01, 1721.92, 1762.78, 
   1826.99, 1892.62, 1934.98, 2023.02, 2113.98, 2157.35, 2189.60, 2223.07, 
   2258.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.37, 0.67, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.950        start      
        1            56         0.00798       0.43567     
        2            57       3.785e-005     4.807e-004   
        3            58       8.180e-009     2.807e-006   
        4            59       3.615e-011     5.231e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2422.77 psi
   
   Pressure: 
   1527.15, 1532.74, 1539.42, 1546.61, 1551.62, 1555.50, 1559.85, 1565.70, 
   1571.24, 1575.61, 1593.44, 1647.82, 1694.18, 1734.72, 1773.54, 1813.55, 
   1876.61, 1941.21, 1982.96, 2069.86, 2159.74, 2202.65, 2234.58, 2267.78, 
   2302.83
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.45, 0.69, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          162.121        start      
        1            56         0.00638       0.44540     
        2            57       1.872e-005     3.085e-004   
        3            58       6.759e-009     1.481e-006   
        4            59       4.279e-011     2.978e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2458.87 psi
   
   Pressure: 
   1527.14, 1532.72, 1539.40, 1546.59, 1551.60, 1555.47, 1559.82, 1565.67, 
   1571.65, 1592.21, 1640.26, 1693.05, 1737.82, 1777.39, 1815.47, 1854.84, 
   1917.01, 1980.76, 2022.01, 2107.95, 2196.92, 2239.44, 2271.11, 2304.08, 
   2338.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.53, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.891        start      
        1            56         0.00367       0.66044     
        2            57       2.603e-006     9.048e-005   
        3            58       3.551e-009     1.139e-007   
        4            59       2.929e-011     1.242e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2493.71 psi
   
   Pressure: 
   1527.03, 1532.60, 1539.25, 1546.41, 1551.40, 1555.26, 1559.59, 1566.55, 
   1602.08, 1638.92, 1685.08, 1736.13, 1779.78, 1818.56, 1855.98, 1894.73, 
   1956.03, 2018.96, 2059.72, 2144.72, 2232.80, 2274.93, 2306.35, 2339.10, 
   2373.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.59, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.645        start      
        1            56         0.00355       0.55495     
        2            57       1.753e-005     1.862e-004   
        3            58       4.618e-008     4.363e-007   
        4            59       9.295e-011     1.431e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2537.34 psi
   
   Pressure: 
   1527.07, 1532.64, 1539.30, 1546.48, 1551.47, 1555.34, 1561.86, 1607.37, 
   1655.06, 1690.62, 1735.60, 1785.67, 1828.65, 1866.91, 1903.88, 1942.22, 
   2002.90, 2065.22, 2105.62, 2189.90, 2277.29, 2319.12, 2350.33, 2382.89, 
   2417.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.63, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.336        start      
        1            56         0.00633       0.26583     
        2            57       3.327e-005     2.154e-004   
        3            58       2.708e-008     1.484e-006   
        4            59       3.680e-011     9.151e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2570.56 psi
   
   Pressure: 
   1527.20, 1532.80, 1539.49, 1546.70, 1551.76, 1560.61, 1597.87, 1648.20, 
   1694.37, 1729.19, 1773.52, 1823.02, 1865.57, 1903.51, 1940.19, 1978.24, 
   2038.49, 2100.40, 2140.53, 2224.29, 2311.17, 2352.77, 2383.82, 2416.24, 
   2450.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
   0.74, 0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.826        start      
        1            56         0.00759       0.46577     
        2            57       3.341e-005     4.824e-004   
        3            58       8.936e-009     2.805e-006   
        4            59       3.689e-011     7.364e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2597.20 psi
   
   Pressure: 
   1527.11, 1532.68, 1539.35, 1546.77, 1564.55, 1598.10, 1634.97, 1683.45, 
   1728.41, 1762.55, 1806.16, 1854.94, 1896.93, 1934.42, 1970.69, 2008.35, 
   2068.02, 2129.37, 2169.17, 2252.27, 2338.54, 2379.87, 2410.75, 2443.01, 
   2477.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.47, 0.70, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.591        start      
        1            56         0.00604       0.89195     
        2            57       1.518e-005     4.731e-004   
        3            58       3.616e-009     2.104e-006   
        4            59       2.794e-011     1.462e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2638.56 psi
   
   Pressure: 
   1526.89, 1532.42, 1539.60, 1575.53, 1618.64, 1651.08, 1686.64, 1733.77, 
   1777.73, 1811.22, 1854.09, 1902.11, 1943.50, 1980.49, 2016.30, 2053.53, 
   2112.55, 2173.27, 2212.69, 2295.08, 2380.68, 2421.72, 2452.41, 2484.52, 
   2518.71
   
   Saturation:
   0.20, 0.20, 0.22, 0.54, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.611        start      
        1            56         0.00352       0.90258     
        2            57       3.821e-006     1.471e-004   
        3            58       1.009e-008     1.150e-007   
        4            59       3.199e-011     2.889e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2692.32 psi
   
   Pressure: 
   1526.89, 1533.60, 1577.39, 1639.29, 1680.92, 1712.48, 1747.36, 1793.74, 
   1837.11, 1870.20, 1912.61, 1960.15, 2001.14, 2037.79, 2073.30, 2110.22, 
   2168.77, 2229.04, 2268.18, 2350.03, 2435.09, 2475.91, 2506.44, 2538.41, 
   2572.49
   
   Saturation:
   0.20, 0.25, 0.59, 0.71, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.659        start      
        1            56         0.00379       1.18376     
        2            57       1.592e-005     6.873e-004   
        3            58       2.036e-008     3.153e-006   
        4            59       5.234e-011     3.357e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2745.77 psi
   
   Pressure: 
   1542.20, 1586.24, 1643.10, 1702.49, 1742.92, 1773.80, 1808.06, 1853.71, 
   1896.48, 1929.15, 1971.06, 2018.07, 2058.63, 2094.91, 2130.09, 2166.68, 
   2224.74, 2284.53, 2323.39, 2404.67, 2489.20, 2529.78, 2560.16, 2591.99, 
   2625.97
   
   Saturation:
   0.31, 0.64, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   7/31/2026 4:32:14 AM
   7/31/2026 4:33:57 AM
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
        0            1          243.296        start      
        1            56         0.00318       0.28114     
        2            57       5.663e-006     9.751e-005   
        3            58       4.399e-009     1.990e-007   
        4            59       6.858e-011     1.899e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2051.72 psi
   
   Pressure: 
   1540.93, 1549.37, 1559.45, 1570.32, 1577.88, 1583.74, 1590.31, 1599.15, 
   1607.51, 1613.95, 1622.30, 1631.74, 1639.96, 1647.39, 1654.66, 1662.30, 
   1674.57, 1687.34, 1695.75, 1713.58, 1732.41, 1741.61, 1750.31, 1803.59, 
   1871.59
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          235.967        start      
        1            56         0.00548       0.57470     
        2            57       3.638e-006     2.595e-004   
        3            58       7.021e-009     4.508e-007   
        4            59       6.211e-011     4.470e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2149.79 psi
   
   Pressure: 
   1540.79, 1549.19, 1559.24, 1570.07, 1577.60, 1583.44, 1589.98, 1598.79, 
   1607.12, 1613.54, 1621.86, 1631.26, 1639.45, 1646.85, 1654.09, 1661.71, 
   1673.92, 1686.64, 1695.02, 1712.78, 1733.94, 1788.78, 1849.09, 1909.33, 
   1969.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.57, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          263.318        start      
        1            56         0.00735       0.90167     
        2            57       1.722e-005     7.197e-004   
        3            58       6.069e-009     1.940e-006   
        4            59       8.380e-011     5.275e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2352.00 psi
   
   Pressure: 
   1540.60, 1548.97, 1558.97, 1569.75, 1577.24, 1583.05, 1589.57, 1598.33, 
   1606.62, 1613.01, 1621.28, 1630.64, 1638.79, 1646.15, 1653.35, 1660.92, 
   1673.08, 1685.73, 1694.40, 1765.83, 1928.73, 2004.23, 2059.12, 2114.88, 
   2171.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.50, 0.71, 0.75, 0.77, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          260.029        start      
        1            56         0.00560       0.38108     
        2            57       1.742e-005     3.517e-004   
        3            58       1.916e-009     1.152e-006   
        4            59       6.529e-011     6.109e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2490.00 psi
   
   Pressure: 
   1540.80, 1549.20, 1559.25, 1570.08, 1577.61, 1583.44, 1589.99, 1598.79, 
   1607.12, 1613.54, 1621.86, 1631.26, 1639.45, 1646.85, 1654.09, 1661.70, 
   1674.03, 1703.99, 1776.32, 1926.43, 2077.71, 2148.81, 2201.08, 2254.64, 
   2310.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.39, 0.68, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          236.745        start      
        1            56         0.00349       0.45393     
        2            57       1.299e-005     1.592e-004   
        3            58       2.040e-008     3.772e-007   
        4            59       7.294e-011     6.987e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2634.67 psi
   
   Pressure: 
   1540.68, 1549.06, 1559.09, 1569.88, 1577.39, 1583.21, 1589.74, 1598.52, 
   1606.83, 1613.24, 1621.53, 1630.92, 1639.09, 1646.47, 1653.71, 1665.97, 
   1766.89, 1875.88, 1944.60, 2085.50, 2229.71, 2298.04, 2348.54, 2400.58, 
   2454.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 
   0.65, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          225.000        start      
        1            56         0.00317       0.50698     
        2            57       2.208e-006     7.442e-005   
        3            58       2.394e-009     6.548e-008   
        4            59       7.122e-011     5.621e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2706.22 psi
   
   Pressure: 
   1540.64, 1549.01, 1559.03, 1569.81, 1577.31, 1583.12, 1589.64, 1598.41, 
   1606.71, 1613.11, 1621.39, 1630.76, 1638.92, 1647.82, 1695.27, 1760.90, 
   1862.19, 1964.65, 2030.32, 2166.21, 2306.09, 2372.62, 2421.94, 2472.96, 
   2526.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 
   0.74, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          251.741        start      
        1            56         0.00624       0.54929     
        2            57       1.533e-005     3.437e-004   
        3            58       5.497e-009     1.096e-006   
        4            59       8.003e-011     2.918e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2779.09 psi
   
   Pressure: 
   1540.60, 1548.96, 1558.96, 1569.72, 1577.21, 1583.02, 1589.53, 1598.29, 
   1606.57, 1612.96, 1621.22, 1631.16, 1667.05, 1731.15, 1791.92, 1853.76, 
   1950.68, 2049.61, 2113.38, 2245.85, 2382.60, 2447.79, 2496.23, 2546.48, 
   2599.37
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.22, 0.51, 0.71, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          257.829        start      
        1            56         0.00715       0.46220     
        2            57       2.595e-005     3.675e-004   
        3            58       1.613e-009     1.508e-006   
        4            59       7.098e-011     6.298e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2878.86 psi
   
   Pressure: 
   1540.63, 1549.00, 1559.00, 1569.78, 1577.27, 1583.08, 1589.60, 1598.36, 
   1606.65, 1613.17, 1638.15, 1719.71, 1789.15, 1849.83, 1907.92, 1967.76, 
   2062.08, 2158.67, 2221.09, 2350.99, 2485.32, 2549.45, 2597.18, 2646.80, 
   2699.20
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.44, 0.69, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.846        start      
        1            56         0.00434       0.35077     
        2            57       1.544e-005     1.659e-004   
        3            58       2.908e-009     6.219e-007   
        4            59       5.611e-011     1.417e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2957.65 psi
   
   Pressure: 
   1540.69, 1549.06, 1559.08, 1569.87, 1577.38, 1583.19, 1589.72, 1598.54, 
   1614.56, 1668.70, 1739.58, 1817.41, 1883.70, 1942.42, 1998.99, 2057.52, 
   2149.97, 2244.80, 2306.18, 2434.05, 2566.46, 2629.76, 2676.93, 2726.05, 
   2778.05
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          219.336        start      
        1            56         0.00287       0.70696     
        2            57       9.607e-006     1.220e-004   
        3            58       1.412e-008     2.194e-007   
        4            59       1.105e-010     3.638e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3046.73 psi
   
   Pressure: 
   1540.36, 1548.67, 1558.60, 1569.30, 1576.75, 1582.52, 1591.67, 1658.18, 
   1729.45, 1782.52, 1849.63, 1924.32, 1988.41, 2045.49, 2100.63, 2157.83, 
   2248.35, 2341.35, 2401.63, 2527.41, 2657.85, 2720.30, 2766.91, 2815.55, 
   2867.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.63, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          219.636        start      
        1            56         0.00316       0.55159     
        2            57       3.469e-006     6.853e-005   
        3            58       5.486e-009     7.486e-008   
        4            59       1.070e-010     1.466e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3117.78 psi
   
   Pressure: 
   1540.48, 1548.81, 1558.77, 1569.50, 1578.59, 1617.73, 1674.01, 1747.57, 
   1815.46, 1866.84, 1932.39, 2005.65, 2068.67, 2124.91, 2179.30, 2235.77, 
   2325.21, 2417.16, 2476.79, 2601.29, 2730.49, 2792.39, 2838.63, 2886.93, 
   2938.29
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.039        start      
        1            56         0.00613       0.89720     
        2            57       1.399e-005     4.424e-004   
        3            58       1.551e-009     1.487e-006   
        4            59       8.375e-011     9.321e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3195.82 psi
   
   Pressure: 
   1540.12, 1548.37, 1558.86, 1607.53, 1672.08, 1720.59, 1773.73, 1844.12, 
   1909.77, 1959.78, 2023.80, 2095.51, 2157.32, 2212.54, 2266.03, 2321.63, 
   2409.76, 2500.45, 2559.32, 2682.37, 2810.19, 2871.49, 2917.33, 2965.29, 
   3016.38
   
   Saturation:
   0.20, 0.20, 0.22, 0.52, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          253.423        start      
        1            56         0.00667       0.61875     
        2            57       2.604e-005     4.358e-004   
        3            58       2.500e-008     2.369e-006   
        4            59       9.613e-011     2.190e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3312.01 psi
   
   Pressure: 
   1541.04, 1564.15, 1650.49, 1741.87, 1803.57, 1850.52, 1902.48, 1971.61, 
   2036.29, 2085.65, 2148.90, 2219.81, 2280.94, 2335.59, 2388.54, 2443.58, 
   2530.87, 2620.71, 2679.06, 2801.03, 2927.80, 2988.63, 3034.15, 3081.81, 
   3132.65
   
   Saturation:
   0.21, 0.43, 0.68, 0.73, 0.75, 0.76, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          81.3048        start      
        1            56         0.00300       4.55857     
        2            57       2.594e-006      0.00207     
        3            58       1.347e-009     4.918e-007   
        4            59       4.182e-011     3.825e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3584.74 psi
   
   Pressure: 
   1831.18, 1899.60, 1979.10, 2063.02, 2120.56, 2164.67, 2213.74, 2279.24, 
   2340.71, 2387.75, 2448.20, 2516.10, 2574.78, 2627.38, 2678.44, 2731.66, 
   2816.27, 2903.55, 2960.38, 3079.49, 3203.62, 3263.35, 3308.15, 3355.20, 
   3405.58
   
   Saturation:
   0.67, 0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          10.2676        start      
        1            56       9.461e-004      2.95851     
        2            57       1.485e-006      0.00156     
        3            58       5.359e-010     7.034e-007   
        4            59       1.139e-011     4.503e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3630.83 psi
   
   Pressure: 
   1844.07, 1913.52, 1995.28, 2082.17, 2141.89, 2187.71, 2238.64, 2306.55, 
   2370.18, 2418.79, 2481.12, 2551.00, 2611.27, 2665.15, 2717.35, 2771.62, 
   2857.69, 2946.27, 3003.80, 3124.11, 3249.20, 3309.24, 3354.21, 3401.33, 
   3451.70
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.73076        start      
        1            56       6.433e-004      4.29374     
        2            57       1.093e-006      0.00233     
        3            58       6.140e-010     1.014e-006   
        4            59       9.072e-012     9.517e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3603.96 psi
   
   Pressure: 
   1832.51, 1900.20, 1980.30, 2065.72, 2124.56, 2169.77, 2220.07, 2287.19, 
   2350.13, 2398.24, 2459.97, 2529.22, 2588.96, 2642.40, 2694.19, 2748.07, 
   2833.55, 2921.57, 2978.76, 3098.43, 3222.92, 3282.72, 3327.52, 3374.52, 
   3424.81
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.39068        start      
        1            56       7.455e-004      6.19326     
        2            57       1.694e-006      0.00478     
        3            58       1.374e-009     2.854e-006   
        4            59       6.753e-012     3.938e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3585.55 psi
   
   Pressure: 
   1826.14, 1892.74, 1971.72, 2056.06, 2114.24, 2158.96, 2208.76, 2275.25, 
   2337.63, 2385.33, 2446.56, 2515.27, 2574.56, 2627.63, 2679.08, 2732.61, 
   2817.58, 2905.10, 2962.00, 3081.10, 3205.05, 3264.63, 3309.29, 3356.17, 
   3406.39
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.61523        start      
        1            56       4.891e-004      6.23761     
        2            57       1.075e-006      0.00426     
        3            58       7.438e-010     2.632e-006   
        4            59       7.603e-012     2.474e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3570.86 psi
   
   Pressure: 
   1821.70, 1887.50, 1965.60, 2049.08, 2106.70, 2151.02, 2200.40, 2266.34, 
   2328.23, 2375.57, 2436.36, 2504.60, 2563.50, 2616.23, 2667.37, 2720.59, 
   2805.10, 2892.18, 2948.81, 3067.40, 3190.87, 3250.24, 3294.77, 3341.54, 
   3391.68
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.15495        start      
        1            56       4.209e-004      6.71299     
        2            57       9.992e-007      0.00458     
        3            58       6.728e-010     3.128e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3558.30 psi
   
   Pressure: 
   1818.23, 1883.36, 1960.73, 2043.48, 2100.62, 2144.59, 2193.59, 2259.05, 
   2320.50, 2367.52, 2427.92, 2495.72, 2554.27, 2606.69, 2657.55, 2710.50, 
   2794.59, 2881.26, 2937.65, 3055.76, 3178.78, 3237.96, 3282.37, 3329.04, 
   3379.12
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.84644        start      
        1            56       4.250e-004      7.50240     
        2            57       1.144e-006      0.00544     
        3            58       8.245e-010     4.229e-006   
        4            59       8.722e-012     3.945e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3547.22 psi
   
   Pressure: 
   1815.32, 1879.90, 1956.63, 2038.74, 2095.45, 2139.11, 2187.77, 2252.79, 
   2313.85, 2360.58, 2420.61, 2488.02, 2546.25, 2598.39, 2648.99, 2701.68, 
   2785.39, 2871.69, 2927.85, 3045.52, 3168.14, 3227.14, 3271.44, 3318.01, 
   3368.03
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/31/2026 4:34:46 AM
   7/31/2026 4:36:03 AM
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
        0            1          366.190        start      
        1            56         0.00757       0.28999     
        2            57       1.673e-005     3.376e-004   
        3            58       3.580e-009     8.833e-007   
        4            59       1.113e-010     1.518e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2268.08 psi
   
   Pressure: 
   1554.63, 1565.88, 1579.35, 1593.85, 1603.94, 1611.75, 1620.52, 1632.32, 
   1643.47, 1652.08, 1663.21, 1675.81, 1686.78, 1696.69, 1706.38, 1716.58, 
   1732.94, 1749.98, 1761.19, 1784.97, 1810.08, 1822.71, 1855.10, 1941.96, 
   2027.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.47, 0.71, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          304.716        start      
        1            56         0.00290       0.96161     
        2            57       5.982e-006     1.925e-004   
        3            58       3.907e-009     2.680e-007   
        4            59       1.518e-010     1.822e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2556.06 psi
   
   Pressure: 
   1553.93, 1565.05, 1578.34, 1592.66, 1602.62, 1610.34, 1619.00, 1630.65, 
   1641.67, 1650.16, 1661.17, 1673.62, 1684.46, 1694.25, 1703.84, 1713.92, 
   1730.10, 1746.96, 1758.06, 1789.87, 1984.84, 2088.50, 2163.39, 2239.07, 
   2316.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.28, 0.63, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          346.332        start      
        1            56         0.00553       0.41866     
        2            57       1.394e-005     3.093e-004   
        3            58       1.740e-009     7.808e-007   
        4            59       9.553e-011     1.032e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2817.72 psi
   
   Pressure: 
   1554.36, 1565.56, 1578.96, 1593.39, 1603.42, 1611.20, 1619.93, 1631.66, 
   1642.76, 1651.32, 1662.39, 1674.93, 1685.84, 1695.70, 1705.34, 1715.49, 
   1731.90, 1771.00, 1867.48, 2067.43, 2268.82, 2363.46, 2433.02, 2504.30, 
   2578.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.39, 0.68, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          324.690        start      
        1            56         0.00597       0.56131     
        2            57       9.521e-006     2.625e-004   
        3            58       4.760e-009     6.156e-007   
        4            59       1.247e-010     2.037e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3040.51 psi
   
   Pressure: 
   1554.18, 1565.34, 1578.69, 1593.07, 1603.07, 1610.81, 1619.51, 1631.20, 
   1642.26, 1650.78, 1661.82, 1674.30, 1685.17, 1694.99, 1705.36, 1756.97, 
   1898.27, 2039.70, 2129.49, 2314.34, 2503.98, 2593.96, 2660.54, 2729.26, 
   2801.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.54, 
   0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          300.739        start      
        1            56         0.00262       0.50716     
        2            57       7.000e-006     1.030e-004   
        3            58       5.683e-009     1.656e-007   
        4            59       1.294e-010     1.668e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3172.54 psi
   
   Pressure: 
   1554.08, 1565.22, 1578.54, 1592.89, 1602.87, 1610.60, 1619.29, 1630.96, 
   1642.00, 1650.52, 1661.54, 1674.03, 1689.47, 1767.15, 1850.03, 1933.73, 
   2064.30, 2197.27, 2282.85, 2460.39, 2643.50, 2730.73, 2795.50, 2862.63, 
   2933.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.29, 0.64, 0.72, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          343.853        start      
        1            56         0.00693       0.50700     
        2            57       2.110e-005     3.167e-004   
        3            58       2.490e-009     1.031e-006   
        4            59       1.311e-010     1.119e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3332.03 psi
   
   Pressure: 
   1554.06, 1565.19, 1578.51, 1592.85, 1602.82, 1610.55, 1619.23, 1630.89, 
   1641.92, 1650.56, 1681.92, 1790.47, 1882.92, 1963.64, 2040.91, 2120.49, 
   2245.90, 2374.31, 2457.29, 2629.97, 2808.52, 2893.77, 2957.22, 3023.18, 
   3092.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          304.896        start      
        1            56         0.00451       0.80458     
        2            57       3.689e-006     1.750e-004   
        3            58       3.185e-009     2.838e-007   
        4            59       1.206e-010     1.387e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3466.71 psi
   
   Pressure: 
   1553.61, 1564.64, 1577.84, 1592.06, 1601.95, 1609.61, 1618.21, 1631.30, 
   1695.79, 1769.16, 1860.86, 1962.18, 2048.79, 2125.72, 2199.95, 2276.85, 
   2398.46, 2523.32, 2604.21, 2772.88, 2947.68, 3031.31, 3093.68, 3158.71, 
   3227.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 
   0.57, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          328.903        start      
        1            56         0.00473       0.42174     
        2            57       1.563e-005     1.290e-004   
        3            58       4.511e-009     4.412e-007   
        4            59       1.282e-010     1.806e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3623.37 psi
   
   Pressure: 
   1554.05, 1565.18, 1578.49, 1592.83, 1602.85, 1617.74, 1691.58, 1791.92, 
   1883.78, 1952.99, 2041.03, 2139.29, 2223.73, 2299.00, 2371.77, 2447.27, 
   2566.78, 2689.57, 2769.18, 2935.31, 3107.62, 3190.14, 3251.76, 3316.09, 
   3384.47
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 0.73, 
   0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          334.925        start      
        1            56         0.00650       0.89725     
        2            57       1.369e-005     4.214e-004   
        3            58       1.056e-009     1.165e-006   
        4            59       1.045e-010     6.063e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3747.59 psi
   
   Pressure: 
   1553.30, 1564.27, 1578.00, 1636.68, 1722.62, 1787.17, 1857.84, 1951.41, 
   2038.67, 2105.12, 2190.19, 2285.47, 2367.58, 2440.95, 2512.01, 2585.87, 
   2702.96, 2823.44, 2901.65, 3065.10, 3234.89, 3316.32, 3377.21, 3440.92, 
   3508.80
   
   Saturation:
   0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          283.275        start      
        1            56         0.00274       1.26499     
        2            57       5.081e-006     2.704e-004   
        3            58       2.981e-009     8.321e-007   
        4            59       1.092e-010     7.407e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3945.10 psi
   
   Pressure: 
   1569.69, 1650.32, 1763.03, 1880.49, 1960.34, 2021.31, 2088.93, 2179.05, 
   2263.47, 2327.98, 2410.73, 2503.59, 2583.72, 2655.42, 2724.95, 2797.30, 
   2912.14, 3030.43, 3107.31, 3268.18, 3435.51, 3515.87, 3576.05, 3639.12, 
   3706.50
   
   Saturation:
   0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          22.2288        start      
        1            56         0.00108       0.80791     
        2            57       1.449e-006     9.656e-004   
        3            58       1.017e-010     9.282e-007   
        4            59       2.398e-011     4.610e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4344.86 psi
   
   Pressure: 
   1967.36, 2060.92, 2170.55, 2286.73, 2366.44, 2427.52, 2495.36, 2585.77, 
   2670.44, 2735.09, 2817.97, 2910.86, 2990.95, 3062.54, 3131.88, 3203.97, 
   3318.26, 3435.88, 3512.27, 3671.97, 3837.96, 3917.65, 3977.30, 4039.82, 
   4106.64
   
   Saturation:
   0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.64145        start      
        1            56       6.712e-004      5.06473     
        2            57       9.375e-007      0.00199     
        3            58       4.500e-010     6.083e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4298.29 psi
   
   Pressure: 
   1943.63, 2033.90, 2140.66, 2254.43, 2332.79, 2392.97, 2459.90, 2549.20, 
   2632.93, 2696.90, 2778.97, 2871.01, 2950.41, 3021.41, 3090.22, 3161.79, 
   3275.31, 3392.18, 3468.11, 3626.95, 3792.15, 3871.50, 3930.94, 3993.29, 
   4060.02
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.34290        start      
        1            56       8.158e-004      8.13490     
        2            57       1.686e-006      0.00523     
        3            58       1.304e-009     2.353e-006   
        4            59       6.327e-012     3.324e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4266.34 psi
   
   Pressure: 
   1932.45, 2020.79, 2125.55, 2237.44, 2314.62, 2373.95, 2440.01, 2528.20, 
   2610.94, 2674.20, 2755.40, 2846.51, 2925.14, 2995.51, 3063.72, 3134.71, 
   3247.36, 3363.40, 3438.84, 3596.74, 3761.06, 3840.04, 3899.26, 3961.43, 
   4028.05
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.35588        start      
        1            56       5.214e-004      8.07426     
        2            57       1.047e-006      0.00463     
        3            58       6.836e-010     2.203e-006   
        4            59       1.043e-011     1.980e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4242.40 psi
   
   Pressure: 
   1925.32, 2012.35, 2115.67, 2226.13, 2302.38, 2361.04, 2426.39, 2513.68, 
   2595.60, 2658.27, 2738.74, 2829.07, 2907.05, 2976.86, 3044.57, 3115.05, 
   3226.96, 3342.28, 3417.28, 3574.35, 3737.90, 3816.55, 3875.56, 3937.57, 
   4004.08
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.69173        start      
        1            56       6.185e-004      10.4724     
        2            57       1.663e-006      0.00754     
        3            58       1.361e-009     4.888e-006   
        4            59       1.103e-011     5.940e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4222.55 psi
   
   Pressure: 
   1919.93, 2005.92, 2108.08, 2217.37, 2292.85, 2350.95, 2415.69, 2502.19, 
   2583.41, 2645.56, 2725.39, 2815.03, 2892.44, 2961.76, 3029.02, 3099.06, 
   3210.30, 3324.97, 3399.59, 3555.90, 3718.76, 3797.12, 3855.94, 3917.79, 
   3984.21
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.27970        start      
        1            56       3.958e-004      9.25979     
        2            57       9.503e-007      0.00530     
        3            58       6.062e-010     3.120e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4205.36 psi
   
   Pressure: 
   1915.50, 2000.63, 2101.81, 2210.09, 2284.90, 2342.50, 2406.71, 2492.52, 
   2573.12, 2634.81, 2714.07, 2803.09, 2879.99, 2948.87, 3015.72, 3085.36, 
   3196.00, 3310.09, 3384.35, 3539.99, 3702.22, 3780.31, 3838.96, 3900.68, 
   3967.00
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.89841        start      
        1            56       3.869e-004      10.1529     
        2            57       1.037e-006      0.00596     
        3            58       7.136e-010     3.857e-006   
        4            59       6.246e-012     3.382e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4190.13 psi
   
   Pressure: 
   1911.71, 1996.09, 2096.41, 2203.81, 2278.03, 2335.19, 2398.93, 2484.12, 
   2564.16, 2625.43, 2704.18, 2792.65, 2869.08, 2937.57, 3004.05, 3073.32, 
   3183.42, 3296.97, 3370.92, 3525.94, 3687.59, 3765.44, 3823.94, 3885.52, 
   3951.76
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.66037        start      
        1            56       4.391e-004      11.5803     
        2            57       1.349e-006      0.00746     
        3            58       1.100e-009     5.326e-006   
        4            59       1.253e-011     6.253e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4176.43 psi
   
   Pressure: 
   1908.38, 1992.10, 2091.66, 2198.26, 2271.96, 2328.72, 2392.02, 2476.66, 
   2556.19, 2617.09, 2695.37, 2783.33, 2859.34, 2927.47, 2993.61, 3062.55, 
   3172.14, 3285.21, 3358.86, 3513.32, 3674.44, 3752.07, 3810.42, 3871.89, 
   3938.05
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.38736        start      
        1            56       4.719e-004      12.9152     
        2            57       1.595e-006      0.00872     
        3            58       1.523e-009     6.410e-006   
        4            59       7.531e-012     1.002e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4163.97 psi
   
   Pressure: 
   1905.40, 1988.52, 2087.39, 2193.28, 2266.49, 2322.90, 2385.81, 2469.94, 
   2549.00, 2609.55, 2687.41, 2774.90, 2850.53, 2918.32, 2984.16, 3052.79, 
   3161.92, 3274.54, 3347.92, 3501.86, 3662.49, 3739.91, 3798.13, 3859.49, 
   3925.57
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.21574        start      
        1            56       3.798e-004      11.9894     
        2            57       1.163e-006      0.00698     
        3            58       1.007e-009     4.720e-006   
        4            59       8.416e-012     6.292e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4152.57 psi
   
   Pressure: 
   1902.71, 1985.29, 2083.53, 2188.76, 2261.54, 2317.61, 2380.17, 2463.83, 
   2542.46, 2602.70, 2680.17, 2767.23, 2842.50, 2909.99, 2975.54, 3043.89, 
   3152.59, 3264.81, 3337.93, 3491.39, 3651.57, 3728.80, 3786.90, 3848.15, 
   3914.17
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/31/2026 4:36:51 AM
   7/31/2026 4:38:06 AM
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
        0            1          409.062        start      
        1            56         0.00525       0.32667     
        2            57       1.563e-005     1.950e-004   
        3            58       7.377e-009     4.637e-007   
        4            59       1.694e-010     3.025e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2518.00 psi
   
   Pressure: 
   1568.25, 1582.32, 1599.14, 1617.26, 1629.86, 1639.63, 1650.59, 1665.32, 
   1679.26, 1690.01, 1703.93, 1719.67, 1733.38, 1745.76, 1757.88, 1770.63, 
   1791.08, 1812.38, 1826.39, 1856.13, 1887.61, 1913.23, 2010.06, 2114.63, 
   2218.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.66, 0.74, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          405.353        start      
        1            56         0.00468       0.70303     
        2            57       1.376e-005     1.888e-004   
        3            58       1.075e-008     4.064e-007   
        4            59       1.758e-010     3.322e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3040.86 psi
   
   Pressure: 
   1567.59, 1581.51, 1598.17, 1616.12, 1628.60, 1638.27, 1649.13, 1663.73, 
   1677.55, 1688.20, 1701.99, 1717.60, 1731.19, 1743.48, 1755.50, 1768.15, 
   1788.45, 1809.65, 1833.03, 2080.92, 2344.81, 2467.23, 2556.65, 2647.78, 
   2741.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.32, 0.66, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          387.759        start      
        1            56         0.00406       0.76411     
        2            57       1.179e-005     1.543e-004   
        3            58       1.026e-008     2.985e-007   
        4            59       1.532e-010     2.895e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3378.92 psi
   
   Pressure: 
   1567.36, 1581.24, 1597.84, 1615.72, 1628.16, 1637.80, 1648.62, 1663.17, 
   1676.94, 1687.55, 1701.30, 1716.86, 1730.40, 1742.64, 1754.64, 1774.07, 
   1940.06, 2120.81, 2234.68, 2468.06, 2706.87, 2820.04, 2903.69, 2989.94, 
   3079.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 
   0.65, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          371.994        start      
        1            56         0.00320       0.66633     
        2            57       7.915e-006     1.164e-004   
        3            58       5.693e-009     1.773e-007   
        4            59       1.493e-010     1.515e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3583.78 psi
   
   Pressure: 
   1567.41, 1581.29, 1597.90, 1615.79, 1628.23, 1637.88, 1648.70, 1663.26, 
   1677.02, 1687.64, 1701.38, 1716.96, 1735.73, 1831.66, 1935.05, 2039.40, 
   2202.14, 2367.85, 2474.50, 2695.74, 2923.92, 3032.62, 3113.34, 3197.02, 
   3285.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          367.527        start      
        1            56         0.00327       0.85385     
        2            57       7.995e-006     1.110e-004   
        3            58       7.454e-009     1.547e-007   
        4            59       1.967e-010     1.575e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3834.23 psi
   
   Pressure: 
   1566.99, 1580.79, 1597.30, 1615.08, 1627.45, 1637.03, 1647.79, 1662.25, 
   1675.95, 1690.31, 1796.35, 1929.96, 2042.38, 2141.19, 2236.04, 2333.94, 
   2488.43, 2646.77, 2749.21, 2962.53, 3183.31, 3288.83, 3367.42, 3449.24, 
   3535.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.28, 0.63, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          359.212        start      
        1            56         0.00358       1.05723     
        2            57       5.735e-006     1.010e-004   
        3            58       6.786e-009     1.133e-007   
        4            59       1.820e-010     1.360e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4048.90 psi
   
   Pressure: 
   1566.53, 1580.23, 1596.62, 1614.27, 1626.55, 1636.07, 1649.87, 1753.35, 
   1871.19, 1958.84, 2069.58, 2192.81, 2298.56, 2392.74, 2483.76, 2578.19, 
   2727.67, 2881.28, 2980.88, 3188.76, 3404.37, 3507.63, 3584.73, 3665.21, 
   3750.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          363.334        start      
        1            56         0.00307       0.94885     
        2            57       6.481e-006     1.238e-004   
        3            58       5.748e-009     1.923e-007   
        4            59       1.530e-010     2.111e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4248.14 psi
   
   Pressure: 
   1566.56, 1580.26, 1596.67, 1620.21, 1713.98, 1796.30, 1885.69, 2003.55, 
   2113.13, 2196.44, 2302.97, 2422.20, 2524.90, 2616.64, 2705.46, 2797.75, 
   2944.04, 3094.53, 3192.22, 3396.33, 3608.31, 3709.96, 3785.97, 3865.47, 
   3950.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.27, 0.62, 0.72, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          350.315        start      
        1            56         0.00373       1.04305     
        2            57       3.703e-006     2.365e-004   
        3            58       2.418e-009     6.510e-007   
        4            59       1.836e-010     1.291e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1580.84, 1674.84, 1814.54, 1960.00, 2058.79, 2134.19, 2217.79, 2329.15, 
   2433.45, 2513.11, 2615.28, 2729.87, 2828.72, 2917.13, 3002.82, 3091.94, 
   3233.31, 3378.83, 3473.36, 3670.97, 3876.33, 3974.86, 4048.58, 4125.77, 
   4208.16
   
   Saturation:
   0.26, 0.60, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          19.3958        start      
        1            56         0.00115       1.13692     
        2            57       1.574e-006      0.00115     
        3            58       2.124e-010     1.006e-006   
        4            59       1.604e-011     8.840e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1989.72, 2088.04, 2203.44, 2325.83, 2409.85, 2474.24, 2545.78, 2641.14, 
   2730.46, 2798.67, 2886.13, 2984.18, 3068.72, 3144.31, 3217.54, 3293.69, 
   3414.46, 3538.77, 3619.52, 3788.43, 3964.05, 4048.38, 4111.52, 4177.70, 
   4248.46
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.68889        start      
        1            56       7.535e-004      1.57886     
        2            57       1.230e-006      0.00174     
        3            58       3.943e-010     1.814e-006   
        4            59       1.086e-011     4.308e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1973.47, 2069.90, 2184.02, 2305.70, 2389.53, 2453.93, 2525.59, 2621.21, 
   2710.88, 2779.43, 2867.38, 2966.05, 3051.18, 3127.34, 3201.17, 3277.98, 
   3399.88, 3525.42, 3607.02, 3777.79, 3955.47, 4040.84, 4104.82, 4171.95, 
   4243.82
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.50296        start      
        1            56       7.839e-004      1.85679     
        2            57       1.573e-006      0.00264     
        3            58       5.072e-010     3.715e-006   
        4            59       1.117e-011     9.610e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1967.42, 2062.96, 2176.29, 2297.37, 2380.91, 2445.15, 2516.69, 2612.22, 
   2701.86, 2770.42, 2858.44, 2957.22, 3042.49, 3118.81, 3192.83, 3269.88, 
   3392.19, 3518.22, 3600.18, 3771.78, 3950.43, 4036.32, 4100.73, 4168.38, 
   4240.90
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.52986        start      
        1            56       6.108e-004      1.74859     
        2            57       1.136e-006      0.00230     
        3            58       2.810e-010     3.149e-006   
        4            59       8.518e-012     5.967e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1963.96, 2058.92, 2171.69, 2292.29, 2375.56, 2439.63, 2511.03, 2606.40, 
   2695.93, 2764.43, 2852.42, 2951.19, 3036.49, 3112.87, 3186.97, 3264.12, 
   3386.66, 3512.96, 3595.14, 3767.27, 3946.56, 4032.81, 4097.53, 4165.57, 
   4238.58
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.88409        start      
        1            56       5.795e-004      1.78099     
        2            57       1.080e-006      0.00235     
        3            58       2.368e-010     3.366e-006   
        4            59       5.814e-012     5.785e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1961.53, 2056.06, 2168.40, 2288.59, 2371.63, 2435.55, 2506.79, 2602.00, 
   2691.40, 2759.83, 2847.75, 2946.48, 3031.76, 3108.15, 3182.29, 3259.50, 
   3382.18, 3508.67, 3591.00, 3763.53, 3943.32, 4029.85, 4094.82, 4163.17, 
   4236.59
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.41590        start      
        1            56       6.171e-004      1.90378     
        2            57       1.206e-006      0.00264     
        3            58       2.649e-010     4.104e-006   
        4            59       1.015e-011     7.434e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1959.64, 2053.82, 2165.79, 2285.64, 2368.46, 2432.24, 2503.35, 2598.39, 
   2687.67, 2756.02, 2843.86, 2942.54, 3027.79, 3104.17, 3178.32, 3255.57, 
   3378.35, 3504.98, 3587.44, 3760.28, 3940.48, 4027.25, 4092.44, 4161.05, 
   4234.83
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.08895        start      
        1            56       7.253e-004      2.12080     
        2            57       1.554e-006      0.00324     
        3            58       3.808e-010     5.652e-006   
        4            59       9.802e-012     1.200e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1958.06, 2051.96, 2163.61, 2283.15, 2365.79, 2429.43, 2500.41, 2595.31, 
   2684.46, 2752.74, 2840.50, 2939.11, 3024.32, 3100.69, 3174.84, 3252.12, 
   3374.97, 3501.72, 3584.27, 3757.38, 3937.94, 4024.92, 4090.29, 4159.15, 
   4233.24
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.77901        start      
        1            56       5.488e-004      1.89905     
        2            57       1.005e-006      0.00243     
        3            58       2.054e-010     3.674e-006   
        4            59       6.858e-012     6.223e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1956.70, 2050.34, 2161.71, 2280.98, 2363.45, 2426.98, 2497.84, 2592.59, 
   2681.63, 2749.83, 2837.52, 2936.06, 3021.23, 3097.58, 3171.73, 3249.03, 
   3371.94, 3498.78, 3581.41, 3754.76, 3935.63, 4022.80, 4088.34, 4157.41, 
   4231.79
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.52374        start      
        1            56       6.658e-004      2.14665     
        2            57       1.346e-006      0.00305     
        3            58       3.268e-010     5.176e-006   
        4            59       7.592e-012     1.101e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1955.50, 2048.91, 2160.03, 2279.05, 2361.36, 2424.78, 2495.53, 2590.15, 
   2679.08, 2747.21, 2834.82, 2933.30, 3018.43, 3094.76, 3168.90, 3246.21, 
   3369.17, 3496.09, 3578.81, 3752.37, 3933.52, 4020.86, 4086.56, 4155.82, 
   4230.46
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.33001        start      
        1            56       5.456e-004      1.98602     
        2            57       9.843e-007      0.00249     
        3            58       2.145e-010     3.787e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1954.42, 2047.62, 2158.51, 2277.30, 2359.47, 2422.78, 2493.43, 2587.93, 
   2676.76, 2744.83, 2832.37, 2930.78, 3015.87, 3092.18, 3166.31, 3243.63, 
   3366.63, 3493.62, 3576.41, 3750.16, 3931.57, 4019.06, 4084.90, 4154.35, 
   4229.23
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.16957        start      
        1            56       7.102e-004      2.30937     
        2            57       1.461e-006      0.00334     
        3            58       4.043e-010     5.853e-006   
        4            59       1.049e-011     1.458e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1953.43, 2046.44, 2157.12, 2275.70, 2357.73, 2420.96, 2491.51, 2585.89, 
   2674.63, 2742.63, 2830.10, 2928.45, 3013.51, 3089.79, 3163.92, 3241.25, 
   3364.28, 3491.34, 3574.18, 3748.11, 3929.76, 4017.40, 4083.37, 4152.98, 
   4228.09
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.98616        start      
        1            56       5.735e-004      2.12542     
        2            57       1.044e-006      0.00268     
        3            58       2.598e-010     4.177e-006   
        4            59       1.063e-011     9.153e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1952.52, 2045.36, 2155.84, 2274.23, 2356.13, 2419.27, 2489.73, 2584.01, 
   2672.65, 2740.60, 2828.00, 2926.29, 3011.31, 3087.58, 3161.69, 3239.03, 
   3362.10, 3489.21, 3572.11, 3746.21, 3928.08, 4015.85, 4081.94, 4151.70, 
   4227.02
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/31/2026 4:38:51 AM
   7/31/2026 4:40:07 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

