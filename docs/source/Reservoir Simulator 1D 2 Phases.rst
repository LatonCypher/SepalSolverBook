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
        0            1          195.575        start      
        1            56         0.00289       5.66523     
        2            57       5.004e-006     7.177e-004   
        3            58       1.720e-008     4.443e-007   
        4            59       6.958e-011     2.000e-009   
   Producer BHP: 
   2227.48 psi
   
   Injector BHP: 
   2786.39 psi
   
   Pressure: 
   2266.24, 2273.29, 2279.31, 2287.38, 2294.92, 2301.83, 2316.60, 2331.70, 
   2340.61, 2352.55, 2363.22, 2368.40, 2377.29, 2386.66, 2391.40, 2399.68, 
   2409.48, 2418.02, 2425.53, 2431.02, 2473.12, 2514.53, 2520.27, 2555.41, 
   2669.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          181.437        start      
        1            56         0.00287       6.09068     
        2            57       1.648e-006     7.355e-004   
        3            58       3.493e-009     2.372e-007   
        4            59       6.386e-011     2.293e-010   
   Producer BHP: 
   1566.27 psi
   
   Injector BHP: 
   2218.75 psi
   
   Pressure: 
   1605.12, 1612.18, 1618.20, 1626.27, 1633.81, 1640.70, 1655.43, 1670.49, 
   1679.37, 1691.25, 1701.86, 1707.01, 1715.83, 1725.13, 1729.82, 1738.02, 
   1747.71, 1756.15, 1763.56, 1768.98, 1810.45, 1853.71, 1878.77, 1998.65, 
   2101.38
   
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
        0            1          152.207        start      
        1            56         0.00356       2.25724     
        2            57       1.766e-006     7.547e-004   
        3            58       7.508e-010     4.921e-007   
        4            59       5.460e-011     1.288e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2330.54 psi
   
   Pressure: 
   1527.50, 1532.56, 1536.93, 1542.86, 1548.46, 1553.65, 1564.88, 1576.51, 
   1583.46, 1592.88, 1601.40, 1605.59, 1612.87, 1620.64, 1624.63, 1631.68, 
   1640.14, 1647.61, 1654.26, 1659.19, 1704.89, 1965.09, 2011.04, 2118.57, 
   2213.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.126        start      
        1            56         0.00233       1.14848     
        2            57       9.026e-006     2.201e-004   
        3            58       2.257e-008     2.778e-007   
        4            59       5.235e-011     6.724e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2664.21 psi
   
   Pressure: 
   1527.73, 1532.83, 1537.24, 1543.22, 1548.87, 1554.11, 1565.44, 1577.16, 
   1584.18, 1593.68, 1602.28, 1606.51, 1613.86, 1621.71, 1625.73, 1632.86, 
   1641.40, 1648.94, 1655.67, 1663.10, 1981.56, 2310.70, 2353.95, 2456.15, 
   2546.99
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.30, 0.65, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.758        start      
        1            56         0.00441       0.14369     
        2            57       1.393e-005     2.424e-004   
        3            58       8.336e-009     9.037e-007   
        4            59       4.769e-011     3.339e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2698.91 psi
   
   Pressure: 
   1528.08, 1533.24, 1537.71, 1543.76, 1549.48, 1554.78, 1566.24, 1578.11, 
   1585.20, 1594.81, 1603.51, 1607.79, 1615.22, 1623.15, 1627.22, 1634.42, 
   1643.04, 1650.73, 1665.92, 1708.69, 2038.62, 2351.90, 2393.58, 2492.88, 
   2581.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.38, 0.67, 0.74, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.006        start      
        1            56         0.00585       0.44321     
        2            57       1.657e-005     4.878e-004   
        3            58       5.080e-009     1.823e-006   
        4            59       5.450e-011     3.977e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2731.29 psi
   
   Pressure: 
   1527.96, 1533.10, 1537.54, 1543.57, 1549.26, 1554.53, 1565.94, 1577.76, 
   1584.82, 1594.39, 1603.05, 1607.30, 1614.70, 1622.59, 1626.64, 1633.81, 
   1642.82, 1674.48, 1733.29, 1775.41, 2088.58, 2389.66, 2430.04, 2526.89, 
   2614.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.50, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.260        start      
        1            56         0.00247       0.61093     
        2            57       3.875e-006     9.471e-005   
        3            58       4.235e-009     1.649e-007   
        4            59       4.116e-011     2.127e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2789.42 psi
   
   Pressure: 
   1527.86, 1532.98, 1537.40, 1543.41, 1549.08, 1554.33, 1565.70, 1577.48, 
   1584.51, 1594.05, 1602.68, 1606.92, 1614.29, 1622.16, 1626.20, 1634.94, 
   1694.97, 1760.42, 1816.45, 1856.70, 2159.26, 2451.89, 2491.31, 2586.31, 
   2672.24
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.60, 0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.327        start      
        1            56         0.00268       0.25732     
        2            57       1.129e-005     1.294e-004   
        3            58       2.728e-008     2.781e-007   
        4            59       5.139e-011     8.212e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2835.98 psi
   
   Pressure: 
   1527.99, 1533.13, 1537.58, 1543.61, 1549.31, 1554.59, 1566.01, 1577.84, 
   1584.91, 1594.49, 1603.16, 1607.42, 1614.82, 1622.76, 1629.39, 1688.38, 
   1761.94, 1824.59, 1878.80, 1918.01, 2214.15, 2501.32, 2540.11, 2633.81, 
   2718.81
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.65, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.344        start      
        1            56         0.00669       0.36712     
        2            57       2.924e-005     4.775e-004   
        3            58       1.128e-008     2.401e-006   
        4            59       5.264e-011     5.956e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2857.58 psi
   
   Pressure: 
   1527.98, 1533.12, 1537.56, 1543.59, 1549.28, 1554.56, 1565.97, 1577.79, 
   1584.86, 1594.43, 1603.09, 1607.34, 1614.82, 1634.16, 1668.86, 1729.77, 
   1800.22, 1860.93, 1913.78, 1952.15, 2242.83, 2525.33, 2563.57, 2656.17, 
   2740.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.759        start      
        1            56         0.00795       0.59830     
        2            57       3.205e-005     6.415e-004   
        3            58       3.513e-009     3.266e-006   
        4            59       3.952e-011     1.998e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2907.32 psi
   
   Pressure: 
   1527.89, 1533.01, 1537.44, 1543.44, 1549.12, 1554.38, 1565.75, 1577.53, 
   1584.57, 1594.11, 1602.74, 1607.14, 1635.04, 1703.74, 1737.79, 1796.22, 
   1864.60, 1923.83, 1975.56, 2013.20, 2298.99, 2577.27, 2615.01, 2706.61, 
   2790.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.48, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.147        start      
        1            56         0.00550       0.43131     
        2            57       1.112e-005     2.314e-004   
        3            58       1.205e-008     9.211e-007   
        4            59       6.042e-011     5.979e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2946.27 psi
   
   Pressure: 
   1527.94, 1533.07, 1537.51, 1543.52, 1549.21, 1554.47, 1565.87, 1577.67, 
   1584.72, 1594.28, 1603.94, 1626.66, 1690.88, 1757.08, 1790.04, 1847.11, 
   1914.21, 1972.46, 2023.43, 2060.55, 2342.80, 2617.94, 2655.30, 2746.12, 
   2829.15
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.23, 0.55, 0.71, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.333        start      
        1            56         0.00294       0.91170     
        2            57       1.059e-005     2.289e-004   
        3            58       1.208e-008     5.014e-007   
        4            59       4.548e-011     5.610e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2996.30 psi
   
   Pressure: 
   1527.72, 1532.81, 1537.21, 1543.18, 1548.82, 1554.05, 1565.36, 1577.07, 
   1584.07, 1596.54, 1661.04, 1697.56, 1758.79, 1822.54, 1854.56, 1910.29, 
   1976.00, 2033.16, 2083.25, 2119.80, 2398.06, 2669.74, 2706.69, 2796.72, 
   2879.20
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.037        start      
        1            56         0.00453       0.68156     
        2            57       2.326e-005     2.634e-004   
        3            58       4.248e-008     1.072e-006   
        4            59       5.883e-011     2.407e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3063.53 psi
   
   Pressure: 
   1527.81, 1532.92, 1537.33, 1543.32, 1548.98, 1554.22, 1565.56, 1577.35, 
   1589.51, 1668.40, 1742.28, 1777.48, 1837.14, 1899.69, 1931.23, 1986.22, 
   2051.15, 2107.67, 2157.23, 2193.40, 2469.05, 2738.35, 2775.01, 2864.42, 
   2946.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.33, 0.65, 0.73, 0.74, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.252        start      
        1            56         0.00685       0.72722     
        2            57       3.088e-005     6.951e-004   
        3            58       5.230e-009     3.475e-006   
        4            59       3.478e-011     3.419e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3117.83 psi
   
   Pressure: 
   1527.78, 1532.88, 1537.29, 1543.27, 1548.92, 1554.15, 1565.61, 1595.72, 
   1656.48, 1737.66, 1808.82, 1843.10, 1901.59, 1963.14, 1994.24, 2048.53, 
   2112.71, 2168.61, 2217.67, 2253.50, 2526.73, 2793.86, 2830.26, 2919.13, 
   3000.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.879        start      
        1            56         0.00720       0.94110     
        2            57       2.885e-005     8.179e-004   
        3            58       2.012e-009     3.907e-006   
        4            59       4.524e-011     1.107e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3199.31 psi
   
   Pressure: 
   1527.67, 1532.75, 1537.14, 1543.09, 1548.72, 1554.11, 1593.91, 1696.07, 
   1755.25, 1833.39, 1902.69, 1936.23, 1993.69, 2054.27, 2084.93, 2138.50, 
   2201.88, 2257.14, 2305.65, 2341.12, 2611.78, 2876.63, 2912.75, 3001.05, 
   3082.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.47, 0.70, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          155.565        start      
        1            56         0.00629       0.61069     
        2            57       1.579e-005     3.225e-004   
        3            58       8.317e-009     1.556e-006   
        4            59       5.310e-011     3.984e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3266.72 psi
   
   Pressure: 
   1527.82, 1532.92, 1537.34, 1543.33, 1549.55, 1577.09, 1676.24, 1775.19, 
   1832.83, 1909.58, 1977.97, 2011.15, 2068.06, 2128.12, 2158.53, 2211.70, 
   2274.60, 2329.46, 2377.64, 2412.86, 2681.77, 2945.01, 2980.94, 3068.81, 
   3149.75
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.23, 0.55, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.110        start      
        1            56         0.00294       0.65781     
        2            57       8.767e-006     1.722e-004   
        3            58       1.244e-008     3.450e-007   
        4            59       4.526e-011     4.227e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3301.90 psi
   
   Pressure: 
   1527.78, 1532.88, 1537.29, 1544.94, 1585.67, 1631.04, 1726.27, 1822.34, 
   1878.79, 1954.25, 2021.67, 2054.43, 2110.69, 2170.11, 2200.22, 2252.88, 
   2315.23, 2369.62, 2417.42, 2452.38, 2719.45, 2981.06, 3016.79, 3104.27, 
   3184.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.393        start      
        1            56         0.00362       0.51080     
        2            57       1.749e-005     1.919e-004   
        3            58       6.470e-008     5.564e-007   
        4            59       3.832e-011     2.634e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3339.26 psi
   
   Pressure: 
   1527.85, 1532.97, 1540.06, 1588.74, 1637.47, 1681.28, 1774.15, 1868.61, 
   1924.33, 1998.96, 2065.72, 2098.19, 2153.99, 2212.94, 2242.83, 2295.12, 
   2357.04, 2411.08, 2458.57, 2493.33, 2758.91, 3019.18, 3054.75, 3141.89, 
   3222.32
   
   Saturation:
   0.20, 0.20, 0.31, 0.65, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.247        start      
        1            56         0.00531       0.34361     
        2            57       2.583e-005     2.521e-004   
        3            58       4.314e-008     1.823e-006   
        4            59       4.074e-011     2.612e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3366.87 psi
   
   Pressure: 
   1528.13, 1539.10, 1576.77, 1628.21, 1675.41, 1718.31, 1809.77, 1903.17, 
   1958.37, 2032.39, 2098.66, 2130.91, 2186.36, 2244.95, 2274.67, 2326.66, 
   2388.25, 2442.01, 2489.27, 2523.85, 2788.24, 3047.42, 3082.85, 3169.71, 
   3249.94
   
   Saturation:
   0.20, 0.38, 0.67, 0.73, 0.75, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.618        start      
        1            56         0.00587       1.85525     
        2            57       1.808e-005      0.00171     
        3            58       1.266e-008     6.339e-006   
        4            59       5.036e-011     4.343e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3416.95 psi
   
   Pressure: 
   1576.65, 1619.77, 1656.52, 1705.09, 1750.22, 1791.52, 1879.97, 1970.61, 
   2024.32, 2096.48, 2161.22, 2192.77, 2247.10, 2304.61, 2333.81, 2384.98, 
   2445.67, 2498.70, 2545.39, 2579.60, 2841.46, 3098.51, 3133.70, 3220.10, 
   3300.04
   
   Saturation:
   0.43, 0.69, 0.73, 0.75, 0.76, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/27/2026 10:17:59 AM
   7/27/2026 10:19:22 AM
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
        0            1          243.941        start      
        1            56         0.00214       0.41907     
        2            57       5.797e-006     1.666e-004   
        3            58       7.689e-009     1.928e-007   
        4            59       5.926e-011     1.871e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2373.10 psi
   
   Pressure: 
   1542.09, 1549.83, 1556.52, 1565.59, 1574.17, 1582.12, 1599.31, 1617.11, 
   1627.75, 1642.16, 1655.21, 1661.62, 1672.77, 1684.67, 1690.77, 1701.57, 
   1714.52, 1725.95, 1736.13, 1743.68, 1802.35, 1860.95, 1872.16, 2036.93, 
   2197.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.64, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          228.765        start      
        1            56         0.00624       3.26925     
        2            57       2.803e-006      0.00118     
        3            58       1.139e-009     7.184e-007   
        4            59       7.464e-011     1.805e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2728.68 psi
   
   Pressure: 
   1540.88, 1548.40, 1554.90, 1563.71, 1572.04, 1579.76, 1596.45, 1613.73, 
   1624.07, 1638.06, 1650.73, 1656.96, 1667.78, 1679.33, 1685.26, 1695.74, 
   1708.30, 1719.40, 1729.28, 1736.60, 1803.70, 2183.99, 2252.38, 2412.16, 
   2552.84
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.24, 0.59, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          258.063        start      
        1            56         0.00675       0.25557     
        2            57       1.386e-005     3.457e-004   
        3            58       8.217e-009     1.021e-006   
        4            59       7.945e-011     4.325e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3286.26 psi
   
   Pressure: 
   1542.15, 1549.90, 1556.60, 1565.69, 1574.27, 1582.23, 1599.44, 1617.26, 
   1627.91, 1642.34, 1655.40, 1661.82, 1672.97, 1684.88, 1690.99, 1701.80, 
   1714.75, 1726.18, 1737.08, 1771.66, 2282.37, 2763.26, 2826.71, 2976.96, 
   3110.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.52, 0.71, 0.75, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          256.765        start      
        1            56         0.00851       0.58021     
        2            57       2.333e-005     6.303e-004   
        3            58       8.180e-009     2.104e-006   
        4            59       8.169e-011     5.832e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3339.97 psi
   
   Pressure: 
   1541.92, 1549.63, 1556.29, 1565.33, 1573.87, 1581.78, 1598.89, 1616.60, 
   1627.19, 1641.54, 1654.52, 1660.90, 1671.98, 1683.82, 1689.89, 1700.63, 
   1714.04, 1759.35, 1847.63, 1910.76, 2379.46, 2829.51, 2889.83, 3034.39, 
   3164.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.49, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          265.054        start      
        1            56         0.00763       0.48229     
        2            57       2.447e-005     5.008e-004   
        3            58       3.161e-009     1.829e-006   
        4            59       7.006e-011     2.070e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3458.39 psi
   
   Pressure: 
   1541.91, 1549.62, 1556.28, 1565.31, 1573.85, 1581.76, 1598.86, 1616.58, 
   1627.17, 1641.51, 1654.49, 1660.87, 1671.95, 1683.79, 1690.00, 1724.97, 
   1837.37, 1933.26, 2015.73, 2075.18, 2522.90, 2956.14, 3014.55, 3155.42, 
   3283.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.46, 
   0.70, 0.74, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          257.604        start      
        1            56         0.00580       0.38282     
        2            57       1.884e-005     3.439e-004   
        3            58       2.753e-009     1.189e-006   
        4            59       5.808e-011     6.823e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3529.30 psi
   
   Pressure: 
   1541.93, 1549.64, 1556.30, 1565.33, 1573.87, 1581.78, 1598.89, 1616.61, 
   1627.20, 1641.55, 1654.53, 1660.91, 1672.10, 1699.43, 1751.61, 1842.94, 
   1948.48, 2039.37, 2118.48, 2175.89, 2610.55, 3032.70, 3089.83, 3228.13, 
   3353.98
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          235.416        start      
        1            56         0.00332       0.51245     
        2            57       1.218e-005     1.443e-004   
        3            58       2.306e-008     2.883e-007   
        4            59       6.550e-011     6.280e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3643.22 psi
   
   Pressure: 
   1541.77, 1549.45, 1556.09, 1565.09, 1573.60, 1581.48, 1598.53, 1616.19, 
   1626.74, 1641.04, 1654.02, 1664.16, 1755.37, 1856.71, 1906.76, 1993.04, 
   2094.24, 2181.99, 2258.69, 2314.53, 2738.55, 3151.46, 3207.51, 3343.63, 
   3467.98
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.31, 0.65, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          215.617        start      
        1            56         0.00286       1.10573     
        2            57       5.253e-006     1.392e-004   
        3            58       4.909e-009     2.062e-007   
        4            59       9.093e-011     2.000e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3728.29 psi
   
   Pressure: 
   1541.27, 1548.85, 1555.41, 1564.30, 1572.70, 1580.49, 1597.33, 1614.77, 
   1625.20, 1643.00, 1736.26, 1790.77, 1882.03, 1977.03, 2024.76, 2107.81, 
   2205.76, 2290.99, 2365.69, 2420.20, 2835.27, 3240.47, 3295.61, 3429.95, 
   3553.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.26, 0.61, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          231.931        start      
        1            56         0.00590       0.94921     
        2            57       9.718e-006     3.435e-004   
        3            58       8.013e-009     9.212e-007   
        4            59       5.822e-011     5.210e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3866.55 psi
   
   Pressure: 
   1541.37, 1548.97, 1555.54, 1564.45, 1572.87, 1580.67, 1597.55, 1616.70, 
   1673.60, 1797.28, 1905.11, 1956.79, 2044.73, 2137.13, 2183.79, 2265.21, 
   2361.42, 2445.23, 2518.76, 2572.46, 2981.80, 3381.81, 3436.31, 3569.30, 
   3691.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 
   0.55, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          260.687        start      
        1            56         0.00649       0.97538     
        2            57       1.964e-005     5.942e-004   
        3            58       2.442e-009     1.955e-006   
        4            59       1.003e-010     2.024e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4028.83 psi
   
   Pressure: 
   1541.28, 1548.86, 1555.42, 1564.31, 1572.71, 1580.67, 1633.35, 1786.03, 
   1874.48, 1991.16, 2094.56, 2144.60, 2230.30, 2320.65, 2366.37, 2446.27, 
   2540.79, 2623.20, 2695.57, 2748.48, 3152.15, 3547.05, 3600.93, 3732.62, 
   3853.84
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.45, 0.70, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          247.896        start      
        1            56         0.00513       0.44949     
        2            57       1.965e-005     2.186e-004   
        3            58       5.972e-009     9.676e-007   
        4            59       6.400e-011     1.757e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4160.93 psi
   
   Pressure: 
   1541.73, 1549.39, 1556.02, 1565.06, 1582.49, 1649.65, 1795.86, 1941.99, 
   2027.47, 2141.44, 2243.08, 2292.41, 2377.05, 2466.37, 2511.60, 2590.67, 
   2684.23, 2765.82, 2837.49, 2889.89, 3289.86, 3681.32, 3734.76, 3865.52, 
   3986.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.37, 0.67, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          223.710        start      
        1            56         0.00268       0.68236     
        2            57       8.970e-006     1.552e-004   
        3            58       2.128e-008     2.339e-007   
        4            59       8.093e-011     6.878e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4238.36 psi
   
   Pressure: 
   1541.48, 1549.11, 1558.63, 1629.02, 1701.85, 1767.25, 1905.71, 2046.46, 
   2129.47, 2240.61, 2340.03, 2388.39, 2471.49, 2559.29, 2603.81, 2681.70, 
   2773.95, 2854.47, 2925.25, 2977.06, 3372.87, 3760.68, 3813.69, 3943.58, 
   4063.52
   
   Saturation:
   0.20, 0.20, 0.29, 0.64, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          226.843        start      
        1            56         0.00419       0.83321     
        2            57       3.379e-006     2.033e-004   
        3            58       4.041e-009     5.413e-007   
        4            59       5.018e-011     2.034e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4301.80 psi
   
   Pressure: 
   1546.69, 1591.63, 1648.71, 1723.67, 1792.84, 1855.93, 1990.66, 2128.45, 
   2209.96, 2319.33, 2417.31, 2465.02, 2547.09, 2633.88, 2677.91, 2754.99, 
   2846.35, 2926.12, 2996.28, 3047.66, 3440.48, 3825.62, 3878.30, 4007.53, 
   4127.00
   
   Saturation:
   0.23, 0.57, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          57.2004        start      
        1            56         0.00177       0.83204     
        2            57       1.734e-006      0.00132     
        3            58       7.162e-010     3.501e-007   
        4            59       3.571e-011     3.840e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1852.38, 1915.18, 1967.92, 2038.07, 2103.41, 2163.25, 2291.34, 2422.54, 
   2500.22, 2604.49, 2697.95, 2743.47, 2821.78, 2904.59, 2946.61, 3020.19, 
   3107.40, 3183.57, 3250.57, 3299.64, 3675.03, 4043.27, 4093.66, 4217.34, 
   4331.79
   
   Saturation:
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          10.2351        start      
        1            56       8.405e-004      1.15812     
        2            57       1.427e-006      0.00133     
        3            58       3.532e-010     1.379e-006   
        4            59       1.013e-011     2.657e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1842.05, 1903.49, 1955.77, 2025.75, 2091.15, 2151.16, 2279.73, 2411.52, 
   2489.57, 2594.37, 2688.30, 2734.04, 2812.74, 2895.96, 2938.18, 3012.10, 
   3099.71, 3176.22, 3243.53, 3292.83, 3669.96, 4039.98, 4090.64, 4215.06, 
   4330.29
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.82383        start      
        1            56       7.458e-004      1.53414     
        2            57       1.558e-006      0.00208     
        3            58       6.037e-010     2.721e-006   
        4            59       3.762e-012     9.076e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1834.47, 1895.11, 1946.97, 2016.57, 2081.73, 2141.60, 2269.99, 2401.71, 
   2479.77, 2584.63, 2678.67, 2724.49, 2803.34, 2886.76, 2929.09, 3003.25, 
   3091.16, 3167.97, 3235.57, 3285.10, 3664.16, 4036.24, 4087.21, 4212.50, 
   4328.64
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.35376        start      
        1            56       6.038e-004      1.63008     
        2            57       1.302e-006      0.00228     
        3            58       4.911e-010     3.214e-006   
        4            59       7.088e-012     1.006e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1830.62, 1890.78, 1942.33, 2011.61, 2076.56, 2136.26, 2264.39, 2395.90, 
   2473.88, 2578.67, 2672.68, 2718.50, 2797.38, 2880.86, 2923.23, 2997.49, 
   3085.55, 3162.51, 3230.26, 3279.92, 3660.15, 4033.53, 4084.71, 4210.59, 
   4327.38
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.65290        start      
        1            56       4.910e-004      1.60738     
        2            57       1.018e-006      0.00215     
        3            58       3.283e-010     3.031e-006   
        4            59       4.128e-012     7.564e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1828.16, 1887.98, 1939.30, 2008.32, 2073.07, 2132.61, 2260.47, 2391.75, 
   2469.63, 2574.30, 2668.24, 2714.03, 2792.90, 2876.38, 2918.77, 2993.07, 
   3081.21, 3158.27, 3226.12, 3275.88, 3656.94, 4031.30, 4082.64, 4209.00, 
   4326.32
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.17118        start      
        1            56       4.678e-004      1.67043     
        2            57       9.918e-007      0.00229     
        3            58       2.944e-010     3.438e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1826.38, 1885.93, 1937.05, 2005.86, 2070.42, 2129.83, 2257.42, 2388.48, 
   2466.24, 2570.79, 2664.63, 2710.39, 2789.22, 2872.68, 2915.08, 2989.40, 
   3077.59, 3154.71, 3222.64, 3272.47, 3654.20, 4029.36, 4080.84, 4207.60, 
   4325.38
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.82986        start      
        1            56       5.038e-004      1.81284     
        2            57       1.145e-006      0.00269     
        3            58       3.413e-010     4.467e-006   
        4            59       8.485e-012     1.034e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1824.97, 1884.30, 1935.26, 2003.87, 2068.28, 2127.55, 2254.90, 2385.73, 
   2463.38, 2567.80, 2661.55, 2707.28, 2786.06, 2869.50, 2911.89, 2986.22, 
   3074.44, 3151.61, 3219.60, 3269.48, 3651.77, 4027.62, 4079.22, 4206.33, 
   4324.52
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/27/2026 10:20:09 AM
   7/27/2026 10:21:33 AM
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
        0            1          446.046        start      
        1            56         0.00290       0.17583     
        2            57       5.468e-006     9.470e-005   
        3            58       6.586e-010     1.706e-007   
        4            59       2.140e-010     3.866e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1571.70, 1584.89, 1596.29, 1611.74, 1626.34, 1639.86, 1669.11, 1699.38, 
   1717.48, 1741.98, 1764.14, 1775.02, 1793.93, 1814.12, 1824.47, 1842.76, 
   1864.68, 1884.02, 1901.37, 1929.55, 2789.44, 3614.08, 3722.48, 3978.69, 
   4206.39
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.38, 0.68, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          415.260        start      
        1            56         0.00901       0.53181     
        2            57       1.962e-005     5.511e-004   
        3            58       4.137e-009     1.267e-006   
        4            59       1.350e-010     2.364e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1566.57, 1578.80, 1589.39, 1603.73, 1617.29, 1629.85, 1657.01, 1685.13, 
   1701.93, 1724.69, 1745.29, 1755.40, 1772.98, 1791.74, 1801.36, 1818.68, 
   1880.56, 2037.67, 2173.15, 2270.13, 2996.97, 3698.16, 3792.47, 4019.18, 
   4223.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 
   0.44, 0.69, 0.74, 0.75, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          373.524        start      
        1            56         0.00687       0.37708     
        2            57       1.927e-005     3.354e-004   
        3            58       2.937e-009     9.454e-007   
        4            59       7.924e-011     8.056e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1562.03, 1573.43, 1583.29, 1596.66, 1609.29, 1621.00, 1646.31, 1672.52, 
   1688.19, 1709.41, 1728.62, 1738.05, 1754.54, 1790.18, 1866.91, 2002.23, 
   2158.51, 2293.10, 2410.30, 2495.42, 3140.09, 3766.17, 3850.89, 4055.88, 
   4242.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 
   0.75, 0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          318.732        start      
        1            56         0.00470       0.46917     
        2            57       2.705e-006     1.425e-004   
        3            58       4.030e-009     2.124e-007   
        4            59       1.287e-010     1.714e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1557.85, 1568.48, 1577.68, 1590.14, 1601.92, 1612.84, 1636.44, 1660.88, 
   1675.49, 1695.29, 1715.94, 1769.08, 1901.89, 2038.47, 2106.52, 2224.45, 
   2363.20, 2483.73, 2589.26, 2666.20, 3251.26, 3821.36, 3898.77, 4086.86, 
   4258.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.24, 0.57, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          310.672        start      
        1            56         0.00416       0.45363     
        2            57       1.246e-005     1.934e-004   
        3            58       4.585e-009     5.189e-007   
        4            59       1.272e-010     2.176e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1553.29, 1563.08, 1571.55, 1583.03, 1593.88, 1603.94, 1625.68, 1648.30, 
   1674.05, 1828.82, 1969.96, 2037.21, 2151.24, 2270.83, 2331.16, 2436.39, 
   2560.69, 2668.95, 2763.93, 2833.31, 3362.09, 3878.54, 3948.83, 4120.11, 
   4277.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.66, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          309.366        start      
        1            56         0.00650       0.67189     
        2            57       1.753e-005     5.219e-004   
        3            58       1.852e-009     1.483e-006   
        4            59       1.229e-010     1.369e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1549.06, 1558.07, 1565.87, 1576.44, 1586.43, 1595.82, 1650.33, 1831.56, 
   1936.88, 2075.72, 2198.70, 2258.21, 2360.13, 2467.58, 2521.98, 2617.06, 
   2729.57, 2827.69, 2913.88, 2976.92, 3458.18, 3928.96, 3993.14, 4149.87, 
   4293.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.42, 0.69, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          289.390        start      
        1            56         0.00641       0.35690     
        2            57       1.859e-005     3.470e-004   
        3            58       1.714e-009     1.202e-006   
        4            59       9.077e-011     1.047e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1546.75, 1555.34, 1562.77, 1573.04, 1602.42, 1679.29, 1842.02, 2005.15, 
   2100.72, 2228.29, 2342.14, 2397.44, 2492.37, 2592.60, 2643.39, 2732.23, 
   2837.41, 2929.19, 3009.86, 3068.89, 3519.84, 3961.35, 4021.61, 4169.00, 
   4304.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.45, 0.69, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          280.243        start      
        1            56         0.00513       0.26727     
        2            57       1.565e-005     2.364e-004   
        3            58       9.690e-009     9.775e-007   
        4            59       7.697e-011     5.567e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1545.28, 1553.74, 1573.81, 1658.72, 1737.20, 1807.96, 1958.18, 2111.14, 
   2201.42, 2322.36, 2430.58, 2483.23, 2573.74, 2669.39, 2717.90, 2802.80, 
   2903.38, 2991.19, 3068.42, 3124.97, 3557.22, 3980.76, 4038.63, 4180.33, 
   4311.02
   
   Saturation:
   0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          258.495        start      
        1            56         0.00515       1.19957     
        2            57       1.146e-005      0.00122     
        3            58       1.007e-008     3.433e-006   
        4            59       9.568e-011     2.956e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1588.22, 1655.83, 1714.44, 1791.70, 1863.37, 1928.92, 2069.20, 2212.90, 
   2298.02, 2412.35, 2514.89, 2564.86, 2650.90, 2741.97, 2788.20, 2869.23, 
   2965.34, 3049.33, 3123.27, 3177.46, 3592.19, 3998.99, 4054.63, 4191.05, 
   4317.05
   
   Saturation:
   0.37, 0.67, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          26.1029        start      
        1            56       7.371e-004      0.39581     
        2            57       9.744e-007     5.981e-004   
        3            58       5.834e-011     6.677e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1850.90, 1913.00, 1965.43, 2035.36, 2100.57, 2160.34, 2288.34, 2419.47, 
   2497.13, 2601.40, 2694.86, 2740.38, 2818.72, 2901.58, 2943.62, 3017.27, 
   3104.59, 3180.88, 3248.02, 3297.22, 3673.82, 4043.40, 4093.97, 4218.08, 
   4332.83
   
   Saturation:
   0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.06641        start      
        1            56       6.315e-004      1.11180     
        2            57       1.011e-006      0.00116     
        3            58       2.642e-010     1.145e-006   
        4            59       9.329e-012     2.321e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1837.40, 1898.25, 1950.15, 2019.71, 2084.79, 2144.55, 2272.68, 2404.10, 
   2481.99, 2586.62, 2680.47, 2726.20, 2804.93, 2888.23, 2930.52, 3004.62, 
   3092.50, 3169.30, 3236.92, 3286.48, 3665.97, 4038.47, 4089.48, 4214.74, 
   4330.66
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.20397        start      
        1            56       7.775e-004      1.56994     
        2            57       1.796e-006      0.00245     
        3            58       7.398e-010     3.696e-006   
        4            59       6.618e-012     1.336e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1831.57, 1891.78, 1943.33, 2012.56, 2077.43, 2137.06, 2265.02, 2396.35, 
   2474.24, 2578.91, 2672.83, 2718.61, 2797.45, 2880.90, 2923.28, 2997.56, 
   3085.68, 3162.72, 3230.56, 3280.30, 3661.25, 4035.32, 4086.56, 4212.50, 
   4329.16
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.05664        start      
        1            56       7.249e-004      1.71532     
        2            57       1.839e-006      0.00298     
        3            58       7.577e-010     5.209e-006   
        4            59       6.737e-012     1.856e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1828.41, 1888.22, 1939.51, 2008.48, 2073.16, 2132.64, 2260.37, 2391.54, 
   2469.35, 2573.96, 2667.85, 2713.63, 2792.49, 2875.99, 2918.41, 2992.77, 
   3081.01, 3158.17, 3226.14, 3275.99, 3657.85, 4032.92, 4084.34, 4210.76, 
   4327.96
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.44994        start      
        1            56       6.218e-004      1.70305     
        2            57       1.545e-006      0.00290     
        3            58       5.435e-010     5.178e-006   
        4            59       5.999e-012     1.509e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1826.34, 1885.86, 1936.94, 2005.69, 2070.20, 2129.55, 2257.05, 2388.02, 
   2465.74, 2570.25, 2664.09, 2709.85, 2788.70, 2872.20, 2914.63, 2989.03, 
   3077.34, 3154.58, 3222.63, 3272.55, 3655.06, 4030.91, 4082.45, 4209.28, 
   4326.92
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.05184        start      
        1            56       6.349e-004      1.80339     
        2            57       1.654e-006      0.00324     
        3            58       5.549e-010     6.306e-006   
        4            59       6.828e-012     1.751e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1824.81, 1884.10, 1935.02, 2003.57, 2067.93, 2127.16, 2254.43, 2385.21, 
   2462.83, 2567.24, 2661.00, 2706.73, 2785.55, 2869.04, 2911.47, 2985.89, 
   3074.24, 3151.53, 3219.65, 3269.63, 3652.65, 4029.13, 4080.79, 4207.95, 
   4326.00
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.77917        start      
        1            56       4.721e-004      1.60810     
        2            57       1.056e-006      0.00244     
        3            58       2.753e-010     4.157e-006   
        4            59       8.628e-012     8.378e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1823.59, 1882.69, 1933.46, 2001.85, 2066.07, 2125.19, 2252.25, 2382.84, 
   2460.37, 2564.66, 2658.34, 2704.04, 2782.83, 2866.29, 2908.72, 2983.15, 
   3071.52, 3148.85, 3217.01, 3267.03, 3650.50, 4027.53, 4079.28, 4206.74, 
   4325.15
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.52611        start      
        1            56       5.498e-004      1.79318     
        2            57       1.354e-006      0.00302     
        3            58       3.765e-010     5.834e-006   
        4            59       6.555e-012     1.309e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1822.57, 1881.50, 1932.15, 2000.39, 2064.48, 2123.50, 2250.36, 2380.77, 
   2458.20, 2562.39, 2655.99, 2701.66, 2780.40, 2863.84, 2906.26, 2980.68, 
   3069.08, 3146.43, 3214.63, 3264.69, 3648.53, 4026.05, 4077.89, 4205.63, 
   4324.36
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.33990        start      
        1            56       4.421e-004      1.65057     
        2            57       9.663e-007      0.00244     
        3            58       2.278e-010     4.229e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1821.68, 1880.47, 1931.01, 1999.11, 2063.09, 2122.01, 2248.67, 2378.91, 
   2456.26, 2560.34, 2653.86, 2699.50, 2778.20, 2861.60, 2904.01, 2978.43, 
   3066.84, 3144.21, 3212.44, 3262.53, 3646.72, 4024.68, 4076.60, 4204.60, 
   4323.63
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.18827        start      
        1            56       5.701e-004      1.91681     
        2            57       1.433e-006      0.00329     
        3            58       4.025e-010     6.692e-006   
        4            59       5.278e-012     1.543e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1820.89, 1879.55, 1929.99, 1997.97, 2061.83, 2120.66, 2247.15, 2377.23, 
   2454.49, 2558.47, 2651.91, 2697.51, 2776.17, 2859.54, 2901.94, 2976.35, 
   3064.76, 3142.16, 3210.41, 3260.53, 3645.02, 4023.40, 4075.39, 4203.63, 
   4322.95
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.01665        start      
        1            56       4.587e-004      1.76775     
        2            57       1.020e-006      0.00264     
        3            58       2.474e-010     4.808e-006   
        4            59       7.981e-012     9.171e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1820.18, 1878.72, 1929.06, 1996.92, 2060.69, 2119.43, 2245.75, 2375.67, 
   2452.85, 2556.73, 2650.10, 2695.67, 2774.28, 2857.62, 2900.01, 2974.42, 
   3062.83, 3140.24, 3208.51, 3258.66, 3643.44, 4022.19, 4074.26, 4202.72, 
   4322.31
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/27/2026 10:22:30 AM
   7/27/2026 10:23:52 AM
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
        0            1          446.046        start      
        1            56         0.00290       0.17583     
        2            57       5.468e-006     9.470e-005   
        3            58       6.586e-010     1.706e-007   
        4            59       2.140e-010     3.866e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1571.70, 1584.89, 1596.29, 1611.74, 1626.34, 1639.86, 1669.11, 1699.38, 
   1717.48, 1741.98, 1764.14, 1775.02, 1793.93, 1814.12, 1824.47, 1842.76, 
   1864.68, 1884.02, 1901.37, 1929.55, 2789.44, 3614.08, 3722.48, 3978.69, 
   4206.39
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.38, 0.68, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          415.260        start      
        1            56         0.00901       0.53181     
        2            57       1.962e-005     5.511e-004   
        3            58       4.137e-009     1.267e-006   
        4            59       1.350e-010     2.364e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1566.57, 1578.80, 1589.39, 1603.73, 1617.29, 1629.85, 1657.01, 1685.13, 
   1701.93, 1724.69, 1745.29, 1755.40, 1772.98, 1791.74, 1801.36, 1818.68, 
   1880.56, 2037.67, 2173.15, 2270.13, 2996.97, 3698.16, 3792.47, 4019.18, 
   4223.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 
   0.44, 0.69, 0.74, 0.75, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          373.524        start      
        1            56         0.00687       0.37708     
        2            57       1.927e-005     3.354e-004   
        3            58       2.937e-009     9.454e-007   
        4            59       7.924e-011     8.056e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1562.03, 1573.43, 1583.29, 1596.66, 1609.29, 1621.00, 1646.31, 1672.52, 
   1688.19, 1709.41, 1728.62, 1738.05, 1754.54, 1790.18, 1866.91, 2002.23, 
   2158.51, 2293.10, 2410.30, 2495.42, 3140.09, 3766.17, 3850.89, 4055.88, 
   4242.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 
   0.75, 0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          318.732        start      
        1            56         0.00470       0.46917     
        2            57       2.705e-006     1.425e-004   
        3            58       4.030e-009     2.124e-007   
        4            59       1.287e-010     1.714e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1557.85, 1568.48, 1577.68, 1590.14, 1601.92, 1612.84, 1636.44, 1660.88, 
   1675.49, 1695.29, 1715.94, 1769.08, 1901.89, 2038.47, 2106.52, 2224.45, 
   2363.20, 2483.73, 2589.26, 2666.20, 3251.26, 3821.36, 3898.77, 4086.86, 
   4258.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.24, 0.57, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          310.672        start      
        1            56         0.00416       0.45363     
        2            57       1.246e-005     1.934e-004   
        3            58       4.585e-009     5.189e-007   
        4            59       1.272e-010     2.176e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1553.29, 1563.08, 1571.55, 1583.03, 1593.88, 1603.94, 1625.68, 1648.30, 
   1674.05, 1828.82, 1969.96, 2037.21, 2151.24, 2270.83, 2331.16, 2436.39, 
   2560.69, 2668.95, 2763.93, 2833.31, 3362.09, 3878.54, 3948.83, 4120.11, 
   4277.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.66, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          309.366        start      
        1            56         0.00650       0.67189     
        2            57       1.753e-005     5.219e-004   
        3            58       1.852e-009     1.483e-006   
        4            59       1.229e-010     1.369e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1549.06, 1558.07, 1565.87, 1576.44, 1586.43, 1595.82, 1650.33, 1831.56, 
   1936.88, 2075.72, 2198.70, 2258.21, 2360.13, 2467.58, 2521.98, 2617.06, 
   2729.57, 2827.69, 2913.88, 2976.92, 3458.18, 3928.96, 3993.14, 4149.87, 
   4293.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.42, 0.69, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          289.390        start      
        1            56         0.00641       0.35690     
        2            57       1.859e-005     3.470e-004   
        3            58       1.714e-009     1.202e-006   
        4            59       9.077e-011     1.047e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1546.75, 1555.34, 1562.77, 1573.04, 1602.42, 1679.29, 1842.02, 2005.15, 
   2100.72, 2228.29, 2342.14, 2397.44, 2492.37, 2592.60, 2643.39, 2732.23, 
   2837.41, 2929.19, 3009.86, 3068.89, 3519.84, 3961.35, 4021.61, 4169.00, 
   4304.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.45, 0.69, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          280.243        start      
        1            56         0.00513       0.26727     
        2            57       1.565e-005     2.364e-004   
        3            58       9.690e-009     9.775e-007   
        4            59       7.697e-011     5.567e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1545.28, 1553.74, 1573.81, 1658.72, 1737.20, 1807.96, 1958.18, 2111.14, 
   2201.42, 2322.36, 2430.58, 2483.23, 2573.74, 2669.39, 2717.90, 2802.80, 
   2903.38, 2991.19, 3068.42, 3124.97, 3557.22, 3980.76, 4038.63, 4180.33, 
   4311.02
   
   Saturation:
   0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          258.495        start      
        1            56         0.00515       1.19957     
        2            57       1.146e-005      0.00122     
        3            58       1.007e-008     3.433e-006   
        4            59       9.568e-011     2.956e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1588.22, 1655.83, 1714.44, 1791.70, 1863.37, 1928.92, 2069.20, 2212.90, 
   2298.02, 2412.35, 2514.89, 2564.86, 2650.90, 2741.97, 2788.20, 2869.23, 
   2965.34, 3049.33, 3123.27, 3177.46, 3592.19, 3998.99, 4054.63, 4191.05, 
   4317.05
   
   Saturation:
   0.37, 0.67, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          26.1029        start      
        1            56       7.371e-004      0.39581     
        2            57       9.744e-007     5.981e-004   
        3            58       5.834e-011     6.677e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1850.90, 1913.00, 1965.43, 2035.36, 2100.57, 2160.34, 2288.34, 2419.47, 
   2497.13, 2601.40, 2694.86, 2740.38, 2818.72, 2901.58, 2943.62, 3017.27, 
   3104.59, 3180.88, 3248.02, 3297.22, 3673.82, 4043.40, 4093.97, 4218.08, 
   4332.83
   
   Saturation:
   0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.06641        start      
        1            56       6.315e-004      1.11180     
        2            57       1.011e-006      0.00116     
        3            58       2.642e-010     1.145e-006   
        4            59       9.329e-012     2.321e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1837.40, 1898.25, 1950.15, 2019.71, 2084.79, 2144.55, 2272.68, 2404.10, 
   2481.99, 2586.62, 2680.47, 2726.20, 2804.93, 2888.23, 2930.52, 3004.62, 
   3092.50, 3169.30, 3236.92, 3286.48, 3665.97, 4038.47, 4089.48, 4214.74, 
   4330.66
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.20397        start      
        1            56       7.775e-004      1.56994     
        2            57       1.796e-006      0.00245     
        3            58       7.398e-010     3.696e-006   
        4            59       6.618e-012     1.336e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1831.57, 1891.78, 1943.33, 2012.56, 2077.43, 2137.06, 2265.02, 2396.35, 
   2474.24, 2578.91, 2672.83, 2718.61, 2797.45, 2880.90, 2923.28, 2997.56, 
   3085.68, 3162.72, 3230.56, 3280.30, 3661.25, 4035.32, 4086.56, 4212.50, 
   4329.16
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.05664        start      
        1            56       7.249e-004      1.71532     
        2            57       1.839e-006      0.00298     
        3            58       7.577e-010     5.209e-006   
        4            59       6.737e-012     1.856e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1828.41, 1888.22, 1939.51, 2008.48, 2073.16, 2132.64, 2260.37, 2391.54, 
   2469.35, 2573.96, 2667.85, 2713.63, 2792.49, 2875.99, 2918.41, 2992.77, 
   3081.01, 3158.17, 3226.14, 3275.99, 3657.85, 4032.92, 4084.34, 4210.76, 
   4327.96
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.44994        start      
        1            56       6.218e-004      1.70305     
        2            57       1.545e-006      0.00290     
        3            58       5.435e-010     5.178e-006   
        4            59       5.999e-012     1.509e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1826.34, 1885.86, 1936.94, 2005.69, 2070.20, 2129.55, 2257.05, 2388.02, 
   2465.74, 2570.25, 2664.09, 2709.85, 2788.70, 2872.20, 2914.63, 2989.03, 
   3077.34, 3154.58, 3222.63, 3272.55, 3655.06, 4030.91, 4082.45, 4209.28, 
   4326.92
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.05184        start      
        1            56       6.349e-004      1.80339     
        2            57       1.654e-006      0.00324     
        3            58       5.549e-010     6.306e-006   
        4            59       6.828e-012     1.751e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1824.81, 1884.10, 1935.02, 2003.57, 2067.93, 2127.16, 2254.43, 2385.21, 
   2462.83, 2567.24, 2661.00, 2706.73, 2785.55, 2869.04, 2911.47, 2985.89, 
   3074.24, 3151.53, 3219.65, 3269.63, 3652.65, 4029.13, 4080.79, 4207.95, 
   4326.00
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.77917        start      
        1            56       4.721e-004      1.60810     
        2            57       1.056e-006      0.00244     
        3            58       2.753e-010     4.157e-006   
        4            59       8.628e-012     8.378e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1823.59, 1882.69, 1933.46, 2001.85, 2066.07, 2125.19, 2252.25, 2382.84, 
   2460.37, 2564.66, 2658.34, 2704.04, 2782.83, 2866.29, 2908.72, 2983.15, 
   3071.52, 3148.85, 3217.01, 3267.03, 3650.50, 4027.53, 4079.28, 4206.74, 
   4325.15
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.52611        start      
        1            56       5.498e-004      1.79318     
        2            57       1.354e-006      0.00302     
        3            58       3.765e-010     5.834e-006   
        4            59       6.555e-012     1.309e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1822.57, 1881.50, 1932.15, 2000.39, 2064.48, 2123.50, 2250.36, 2380.77, 
   2458.20, 2562.39, 2655.99, 2701.66, 2780.40, 2863.84, 2906.26, 2980.68, 
   3069.08, 3146.43, 3214.63, 3264.69, 3648.53, 4026.05, 4077.89, 4205.63, 
   4324.36
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.33990        start      
        1            56       4.421e-004      1.65057     
        2            57       9.663e-007      0.00244     
        3            58       2.278e-010     4.229e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1821.68, 1880.47, 1931.01, 1999.11, 2063.09, 2122.01, 2248.67, 2378.91, 
   2456.26, 2560.34, 2653.86, 2699.50, 2778.20, 2861.60, 2904.01, 2978.43, 
   3066.84, 3144.21, 3212.44, 3262.53, 3646.72, 4024.68, 4076.60, 4204.60, 
   4323.63
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.18827        start      
        1            56       5.701e-004      1.91681     
        2            57       1.433e-006      0.00329     
        3            58       4.025e-010     6.692e-006   
        4            59       5.278e-012     1.543e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1820.89, 1879.55, 1929.99, 1997.97, 2061.83, 2120.66, 2247.15, 2377.23, 
   2454.49, 2558.47, 2651.91, 2697.51, 2776.17, 2859.54, 2901.94, 2976.35, 
   3064.76, 3142.16, 3210.41, 3260.53, 3645.02, 4023.40, 4075.39, 4203.63, 
   4322.95
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.01665        start      
        1            56       4.587e-004      1.76775     
        2            57       1.020e-006      0.00264     
        3            58       2.474e-010     4.808e-006   
        4            59       7.981e-012     9.171e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1820.18, 1878.72, 1929.06, 1996.92, 2060.69, 2119.43, 2245.75, 2375.67, 
   2452.85, 2556.73, 2650.10, 2695.67, 2774.28, 2857.62, 2900.01, 2974.42, 
   3062.83, 3140.24, 3208.51, 3258.66, 3643.44, 4022.19, 4074.26, 4202.72, 
   4322.31
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/27/2026 10:24:47 AM
   7/27/2026 10:26:09 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

