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
        0            1          191.996        start      
        1            56         0.00276       5.70832     
        2            57       3.532e-006     6.840e-004   
        3            58       1.070e-008     1.406e-007   
        4            59       6.529e-011     7.565e-010   
   Producer BHP: 
   2211.31 psi
   
   Injector BHP: 
   2668.05 psi
   
   Pressure: 
   2286.89, 2298.62, 2309.01, 2320.86, 2331.07, 2339.23, 2346.37, 2354.03, 
   2360.02, 2365.55, 2372.83, 2380.03, 2387.21, 2394.74, 2400.29, 2406.51, 
   2413.37, 2419.31, 2425.02, 2430.41, 2435.50, 2442.92, 2450.51, 2467.33, 
   2529.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 
   0.69
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.624        start      
        1            56         0.00321       6.09014     
        2            57       1.581e-006     5.996e-004   
        3            58       2.321e-009     2.067e-007   
        4            59       5.590e-011     3.082e-010   
   Producer BHP: 
   1548.68 psi
   
   Injector BHP: 
   2072.83 psi
   
   Pressure: 
   1624.42, 1636.17, 1646.57, 1658.42, 1668.61, 1676.76, 1683.88, 1691.51, 
   1697.48, 1702.98, 1710.21, 1717.36, 1724.48, 1731.94, 1737.44, 1743.59, 
   1750.37, 1756.23, 1761.86, 1767.17, 1772.18, 1780.01, 1816.35, 1877.10, 
   1933.72
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.53, 0.72, 
   0.76
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.716        start      
        1            56         0.00214       0.29631     
        2            57       7.097e-006     1.496e-004   
        3            58       7.369e-009     3.034e-007   
        4            59       4.308e-011     1.803e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2037.39 psi
   
   Pressure: 
   1554.60, 1563.17, 1570.85, 1579.71, 1587.43, 1593.68, 1599.21, 1605.21, 
   1609.96, 1614.40, 1620.33, 1626.27, 1632.26, 1638.64, 1643.40, 1648.81, 
   1654.85, 1660.14, 1665.31, 1670.24, 1676.50, 1729.16, 1790.55, 1845.80, 
   1898.22
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.139        start      
        1            56         0.00494       0.17156     
        2            57       2.432e-005     1.800e-004   
        3            58       1.418e-008     9.773e-007   
        4            59       5.100e-011     1.428e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2073.03 psi
   
   Pressure: 
   1554.68, 1563.26, 1570.96, 1579.82, 1587.55, 1593.81, 1599.35, 1605.36, 
   1610.11, 1614.56, 1620.49, 1626.44, 1632.45, 1638.83, 1643.59, 1649.01, 
   1655.06, 1660.36, 1665.55, 1674.70, 1714.51, 1773.51, 1831.06, 1883.51, 
   1933.87
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.35, 0.67, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.624        start      
        1            56         0.00916       0.33560     
        2            57       4.223e-005     5.297e-004   
        3            58       1.573e-008     3.089e-006   
        4            59       3.927e-011     9.186e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2108.72 psi
   
   Pressure: 
   1554.61, 1563.18, 1570.86, 1579.71, 1587.43, 1593.68, 1599.21, 1605.21, 
   1609.96, 1614.39, 1620.32, 1626.26, 1632.25, 1638.62, 1643.38, 1648.79, 
   1654.82, 1660.24, 1676.67, 1719.34, 1758.96, 1814.78, 1869.89, 1920.55, 
   1969.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.45, 0.69, 0.74, 0.76, 0.78, 0.79, 
   0.80
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.385        start      
        1            56         0.00627       0.49543     
        2            57       1.199e-005     3.286e-004   
        3            58       1.178e-008     1.288e-006   
        4            59       4.389e-011     6.596e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2151.05 psi
   
   Pressure: 
   1554.50, 1563.05, 1570.71, 1579.55, 1587.25, 1593.48, 1599.00, 1604.99, 
   1609.73, 1614.15, 1620.07, 1626.00, 1631.98, 1638.33, 1643.08, 1648.48, 
   1655.19, 1683.71, 1728.30, 1769.41, 1807.19, 1861.00, 1914.47, 1963.86, 
   2011.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.23, 0.55, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.665        start      
        1            56         0.00340       0.42285     
        2            57       1.649e-005     2.313e-004   
        3            58       3.258e-008     5.001e-007   
        4            59       1.012e-010     1.079e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2201.65 psi
   
   Pressure: 
   1554.48, 1563.03, 1570.69, 1579.52, 1587.22, 1593.46, 1598.98, 1604.96, 
   1609.70, 1614.13, 1620.04, 1625.97, 1631.95, 1638.31, 1643.07, 1650.72, 
   1697.84, 1743.38, 1785.92, 1825.38, 1862.01, 1914.44, 1966.73, 2015.18, 
   2062.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 
   0.64, 0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          168.064        start      
        1            56         0.00553       0.24616     
        2            57       2.811e-005     2.022e-004   
        3            58       7.808e-009     1.188e-006   
        4            59       5.354e-011     7.304e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2240.95 psi
   
   Pressure: 
   1554.60, 1563.16, 1570.84, 1579.69, 1587.41, 1593.66, 1599.18, 1605.18, 
   1609.93, 1614.37, 1620.29, 1626.23, 1632.22, 1638.63, 1648.28, 1694.22, 
   1745.55, 1789.08, 1830.24, 1868.65, 1904.47, 1955.90, 2007.30, 2055.05, 
   2101.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.050        start      
        1            56         0.00738       0.41762     
        2            57       3.258e-005     4.938e-004   
        3            58       6.166e-009     2.604e-006   
        4            59       4.658e-011     3.695e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2275.99 psi
   
   Pressure: 
   1554.49, 1563.03, 1570.69, 1579.53, 1587.23, 1593.46, 1598.98, 1604.96, 
   1609.69, 1614.12, 1620.03, 1625.96, 1632.06, 1651.38, 1692.47, 1738.05, 
   1787.26, 1829.40, 1869.52, 1907.09, 1942.22, 1992.78, 2043.41, 2090.56, 
   2136.90
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.21, 0.44, 0.69, 0.74, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.574        start      
        1            56         0.00712       0.61010     
        2            57       2.254e-005     4.710e-004   
        3            58       8.095e-009     2.209e-006   
        4            59       3.455e-011     4.740e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2323.76 psi
   
   Pressure: 
   1554.33, 1562.86, 1570.49, 1579.30, 1586.98, 1593.19, 1598.69, 1604.66, 
   1609.38, 1613.80, 1619.69, 1626.02, 1653.79, 1708.97, 1748.75, 1792.59, 
   1840.41, 1881.56, 1920.87, 1957.75, 1992.31, 2042.13, 2092.11, 2138.74, 
   2184.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.22, 0.52, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.315        start      
        1            56         0.00355       0.65964     
        2            57       5.049e-006     8.909e-005   
        3            58       7.166e-009     1.852e-007   
        4            59       4.365e-011     3.890e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2372.43 psi
   
   Pressure: 
   1554.25, 1562.76, 1570.38, 1579.18, 1586.84, 1593.05, 1598.54, 1604.50, 
   1609.22, 1613.63, 1620.71, 1660.75, 1712.37, 1765.34, 1803.78, 1846.55, 
   1893.40, 1933.80, 1972.48, 2008.81, 2042.91, 2092.13, 2141.57, 2187.77, 
   2233.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.25, 0.60, 0.72, 0.74, 0.75, 0.76, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.262        start      
        1            56         0.00266       0.47723     
        2            57       1.108e-005     1.702e-004   
        3            58       1.972e-008     3.038e-007   
        4            59       7.271e-011     5.122e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2414.86 psi
   
   Pressure: 
   1554.36, 1562.89, 1570.53, 1579.34, 1587.03, 1593.25, 1598.75, 1604.72, 
   1609.45, 1615.65, 1661.26, 1712.11, 1761.76, 1813.24, 1850.90, 1892.96, 
   1939.13, 1979.01, 2017.22, 2053.14, 2086.88, 2135.62, 2184.63, 2230.49, 
   2275.84
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.28, 0.63, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.82
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.447        start      
        1            56         0.00371       0.30405     
        2            57       1.737e-005     1.365e-004   
        3            58       2.795e-008     5.470e-007   
        4            59       4.208e-011     1.258e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2444.95 psi
   
   Pressure: 
   1554.50, 1563.04, 1570.70, 1579.54, 1587.24, 1593.47, 1598.99, 1604.99, 
   1613.02, 1649.12, 1699.78, 1748.85, 1797.25, 1847.76, 1884.86, 1926.37, 
   1972.00, 2011.43, 2049.25, 2084.82, 2118.26, 2166.61, 2215.26, 2260.82, 
   2305.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.33, 0.65, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.393        start      
        1            56         0.00538       0.35060     
        2            57       2.539e-005     2.603e-004   
        3            58       1.210e-008     1.443e-006   
        4            59       4.009e-011     4.455e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2476.85 psi
   
   Pressure: 
   1554.45, 1562.99, 1570.64, 1579.47, 1587.16, 1593.39, 1598.95, 1612.05, 
   1652.34, 1690.14, 1738.96, 1786.80, 1834.26, 1883.95, 1920.52, 1961.50, 
   2006.59, 2045.59, 2083.02, 2118.25, 2151.40, 2199.36, 2247.66, 2292.95, 
   2337.86
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 
   0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.343        start      
        1            56         0.00807       0.51232     
        2            57       3.904e-005     4.864e-004   
        3            58       5.153e-009     2.897e-006   
        4            59       3.598e-011     3.153e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2519.06 psi
   
   Pressure: 
   1554.30, 1562.82, 1570.45, 1579.25, 1586.92, 1593.32, 1611.74, 1663.51, 
   1703.66, 1740.10, 1787.69, 1834.60, 1881.28, 1930.24, 1966.33, 2006.81, 
   2051.39, 2089.97, 2127.03, 2161.93, 2194.80, 2242.38, 2290.35, 2335.38, 
   2380.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.46, 0.69, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.334        start      
        1            56         0.00573       0.87054     
        2            57       7.345e-006     2.501e-004   
        3            58       1.009e-008     8.973e-007   
        4            59       4.094e-011     7.256e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2566.57 psi
   
   Pressure: 
   1553.95, 1562.41, 1570.00, 1578.74, 1587.20, 1623.20, 1670.76, 1720.95, 
   1759.64, 1795.10, 1841.67, 1887.74, 1933.66, 1981.90, 2017.49, 2057.46, 
   2101.51, 2139.66, 2176.34, 2210.90, 2243.48, 2290.70, 2338.34, 2383.11, 
   2427.63
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.23, 0.57, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.953        start      
        1            56         0.00295       0.96302     
        2            57       4.389e-006     1.234e-004   
        3            58       8.029e-009     1.705e-007   
        4            59       3.444e-011     3.567e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2618.44 psi
   
   Pressure: 
   1553.83, 1562.27, 1569.84, 1580.58, 1632.40, 1685.55, 1731.34, 1779.97, 
   1817.81, 1852.65, 1898.54, 1944.02, 1989.39, 2037.09, 2072.32, 2111.89, 
   2155.53, 2193.34, 2229.71, 2264.01, 2296.35, 2343.26, 2390.63, 2435.17, 
   2479.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.25, 0.60, 0.72, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.472        start      
        1            56         0.00308       0.96996     
        2            57       1.367e-005     2.360e-004   
        3            58       3.118e-008     5.558e-007   
        4            59       6.014e-011     1.564e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2683.29 psi
   
   Pressure: 
   1553.80, 1562.25, 1573.29, 1641.55, 1707.28, 1758.58, 1803.17, 1850.84, 
   1888.08, 1922.46, 1967.81, 2012.79, 2057.71, 2104.95, 2139.86, 2179.08, 
   2222.36, 2259.88, 2295.97, 2330.03, 2362.16, 2408.78, 2455.89, 2500.22, 
   2544.41
   
   Saturation:
   0.20, 0.20, 0.29, 0.63, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.549        start      
        1            56         0.00488       0.67249     
        2            57       2.352e-005     3.028e-004   
        3            58       9.188e-009     1.808e-006   
        4            59       3.343e-011     3.948e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2744.58 psi
   
   Pressure: 
   1554.34, 1570.65, 1635.16, 1710.52, 1774.21, 1824.60, 1868.62, 1915.80, 
   1952.73, 1986.86, 2031.93, 2076.65, 2121.30, 2168.29, 2203.01, 2242.03, 
   2285.08, 2322.41, 2358.32, 2392.21, 2424.19, 2470.60, 2517.51, 2561.68, 
   2605.74
   
   Saturation:
   0.20, 0.35, 0.67, 0.73, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          161.821        start      
        1            56         0.00618       3.30995     
        2            57       1.731e-005      0.00230     
        3            58       7.644e-009     7.078e-006   
        4            59       4.375e-011     3.164e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2845.04 psi
   
   Pressure: 
   1635.58, 1705.48, 1767.36, 1837.07, 1896.81, 1944.54, 1986.43, 2031.51, 
   2066.93, 2099.76, 2143.25, 2186.51, 2229.82, 2275.50, 2309.32, 2347.41, 
   2389.53, 2426.12, 2461.40, 2494.74, 2526.28, 2572.15, 2618.60, 2662.43, 
   2706.25
   
   Saturation:
   0.42, 0.68, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/26/2026 9:35:01 AM
   7/26/2026 9:37:09 AM
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
        0            1          240.865        start      
        1            56         0.00353       0.34392     
        2            57       1.355e-005     2.452e-004   
        3            58       1.540e-008     5.131e-007   
        4            59       9.142e-011     6.229e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2167.17 psi
   
   Pressure: 
   1582.05, 1594.92, 1606.47, 1619.78, 1631.38, 1640.77, 1649.08, 1658.09, 
   1665.23, 1671.90, 1680.81, 1689.74, 1698.75, 1708.33, 1715.48, 1723.61, 
   1732.68, 1740.64, 1748.40, 1755.81, 1762.92, 1773.42, 1788.33, 1870.14, 
   1958.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.64, 
   0.74
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          229.287        start      
        1            56         0.00308       0.44520     
        2            57       8.656e-006     1.547e-004   
        3            58       8.895e-009     2.756e-007   
        4            59       7.761e-011     2.049e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2304.72 psi
   
   Pressure: 
   1581.86, 1594.71, 1606.23, 1619.50, 1631.08, 1640.45, 1648.74, 1657.73, 
   1664.86, 1671.51, 1680.40, 1689.31, 1698.30, 1707.86, 1714.99, 1723.11, 
   1732.16, 1740.10, 1747.85, 1755.25, 1764.51, 1843.37, 1935.34, 2017.87, 
   2096.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          235.358        start      
        1            56         0.00450       0.45892     
        2            57       2.530e-006     1.541e-004   
        3            58       4.386e-009     2.834e-007   
        4            59       6.549e-011     2.294e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2384.02 psi
   
   Pressure: 
   1581.83, 1594.67, 1606.18, 1619.45, 1631.01, 1640.38, 1648.67, 1657.65, 
   1664.77, 1671.42, 1680.30, 1689.20, 1698.18, 1707.73, 1714.86, 1722.96, 
   1732.00, 1739.93, 1748.93, 1793.69, 1854.79, 1940.36, 2024.36, 2101.27, 
   2175.42
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.24, 0.57, 0.72, 0.75, 0.77, 0.79, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          242.825        start      
        1            56         0.00603       0.56200     
        2            57       9.682e-006     2.882e-004   
        3            58       7.707e-009     7.891e-007   
        4            59       8.885e-011     3.832e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2474.18 psi
   
   Pressure: 
   1581.67, 1594.49, 1605.97, 1619.22, 1630.76, 1640.11, 1648.38, 1657.34, 
   1664.45, 1671.08, 1679.94, 1688.82, 1697.78, 1707.30, 1714.42, 1722.50, 
   1732.46, 1774.51, 1841.39, 1902.94, 1959.47, 2039.96, 2119.92, 2193.78, 
   2265.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.23, 0.55, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          252.970        start      
        1            56         0.00726       0.54781     
        2            57       1.885e-005     3.952e-004   
        3            58       6.319e-009     1.375e-006   
        4            59       9.116e-011     3.334e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2575.94 psi
   
   Pressure: 
   1581.62, 1594.43, 1605.91, 1619.15, 1630.68, 1640.02, 1648.28, 1657.24, 
   1664.34, 1670.97, 1679.82, 1688.70, 1697.65, 1707.16, 1714.67, 1750.61, 
   1829.10, 1895.52, 1957.96, 2016.09, 2070.18, 2147.76, 2225.21, 2297.10, 
   2367.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.51, 
   0.71, 0.74, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          262.153        start      
        1            56         0.00665       0.44458     
        2            57       2.203e-005     3.763e-004   
        3            58       1.787e-009     1.386e-006   
        4            59       7.435e-011     7.969e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2660.84 psi
   
   Pressure: 
   1581.66, 1594.47, 1605.95, 1619.19, 1630.73, 1640.07, 1648.34, 1657.30, 
   1664.40, 1671.04, 1679.89, 1688.77, 1697.89, 1725.67, 1787.41, 1855.71, 
   1929.38, 1992.45, 2052.49, 2108.69, 2161.25, 2236.87, 2312.60, 2383.12, 
   2452.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.21, 0.44, 0.69, 0.74, 
   0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          247.022        start      
        1            56         0.00500       0.42604     
        2            57       1.905e-005     2.119e-004   
        3            58       3.926e-009     8.171e-007   
        4            59       8.869e-011     2.471e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2769.43 psi
   
   Pressure: 
   1581.55, 1594.35, 1605.81, 1619.04, 1630.56, 1639.89, 1648.15, 1657.11, 
   1664.20, 1670.83, 1679.73, 1697.37, 1773.42, 1854.76, 1913.35, 1978.23, 
   2049.14, 2110.21, 2168.60, 2223.39, 2274.77, 2348.86, 2423.22, 2492.64, 
   2561.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          223.198        start      
        1            56         0.00233       0.60485     
        2            57       6.522e-006     1.072e-004   
        3            58       7.265e-009     1.557e-007   
        4            59       7.337e-011     1.635e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2866.26 psi
   
   Pressure: 
   1581.21, 1593.95, 1605.37, 1618.54, 1630.02, 1639.31, 1647.54, 1656.46, 
   1663.53, 1672.45, 1739.81, 1815.88, 1890.07, 1966.95, 2023.20, 2086.01, 
   2154.96, 2214.51, 2271.58, 2325.23, 2375.63, 2448.46, 2521.70, 2590.23, 
   2658.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.28, 0.63, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          240.443        start      
        1            56         0.00551       0.56238     
        2            57       1.111e-005     2.532e-004   
        3            58       5.304e-009     7.636e-007   
        4            59       7.300e-011     2.509e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2931.56 psi
   
   Pressure: 
   1581.28, 1594.03, 1605.46, 1618.63, 1630.11, 1639.41, 1647.64, 1657.29, 
   1692.39, 1749.89, 1823.98, 1896.19, 1967.65, 2042.38, 2097.32, 2158.87, 
   2226.56, 2285.10, 2341.27, 2394.13, 2443.85, 2515.77, 2588.19, 2656.08, 
   2723.39
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 
   0.53, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          252.646        start      
        1            56         0.00718       0.54125     
        2            57       2.691e-005     3.670e-004   
        3            58       2.756e-009     1.543e-006   
        4            59       7.085e-011     1.390e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3021.53 psi
   
   Pressure: 
   1581.24, 1593.98, 1605.40, 1618.57, 1630.05, 1639.55, 1664.78, 1742.30, 
   1802.43, 1856.95, 1928.10, 1998.24, 2068.00, 2141.18, 2195.11, 2255.60, 
   2322.21, 2379.86, 2435.24, 2487.39, 2536.50, 2607.61, 2679.29, 2746.59, 
   2813.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.44, 0.69, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          259.123        start      
        1            56         0.00470       0.54246     
        2            57       1.583e-005     2.518e-004   
        3            58       1.933e-009     9.133e-007   
        4            59       4.997e-011     7.362e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3129.02 psi
   
   Pressure: 
   1581.15, 1593.88, 1605.29, 1618.53, 1642.96, 1723.23, 1793.59, 1867.83, 
   1925.31, 1978.09, 2047.49, 2116.17, 2184.62, 2256.54, 2309.60, 2369.18, 
   2434.83, 2491.70, 2546.36, 2597.87, 2646.43, 2716.80, 2787.83, 2854.58, 
   2921.01
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.37, 0.68, 0.73, 0.74, 
   0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          215.812        start      
        1            56         0.00251       1.20037     
        2            57       6.634e-006     1.549e-004   
        3            58       1.004e-008     2.407e-007   
        4            59       7.883e-011     4.078e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3257.35 psi
   
   Pressure: 
   1579.86, 1592.40, 1607.63, 1705.47, 1803.28, 1879.52, 1945.79, 2016.60, 
   2071.93, 2123.01, 2190.42, 2257.30, 2324.09, 2394.37, 2446.31, 2504.70, 
   2569.14, 2625.01, 2678.79, 2729.55, 2777.45, 2846.99, 2917.28, 2983.45, 
   3049.45
   
   Saturation:
   0.20, 0.20, 0.28, 0.62, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          230.345        start      
        1            56         0.00502       1.40859     
        2            57       7.005e-006     3.952e-004   
        3            58       7.191e-009     9.583e-007   
        4            59       8.220e-011     5.945e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3391.46 psi
   
   Pressure: 
   1587.74, 1656.33, 1753.68, 1862.59, 1955.24, 2028.88, 2093.33, 2162.54, 
   2216.79, 2266.99, 2333.36, 2399.28, 2465.16, 2534.55, 2585.85, 2643.56, 
   2707.29, 2762.57, 2815.81, 2866.07, 2913.55, 2982.51, 3052.27, 3118.01, 
   3183.66
   
   Saturation:
   0.23, 0.55, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          58.5316        start      
        1            56         0.00221       7.63868     
        2            57       1.169e-006      0.00169     
        3            58       3.084e-010     2.686e-007   
        4            59       4.397e-011     1.478e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3927.97 psi
   
   Pressure: 
   2164.18, 2265.75, 2354.11, 2454.27, 2540.41, 2609.37, 2669.95, 2735.17, 
   2786.46, 2834.04, 2897.10, 2959.90, 3022.81, 3089.22, 3138.45, 3193.96, 
   3255.41, 3308.84, 3360.44, 3409.27, 3455.52, 3522.90, 3591.26, 3655.87, 
   3720.62
   
   Saturation:
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          9.94957        start      
        1            56       8.444e-004      4.45763     
        2            57       1.063e-006      0.00193     
        3            58       3.803e-010     5.243e-007   
        4            59       1.062e-011     3.681e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4005.67 psi
   
   Pressure: 
   2187.92, 2293.51, 2386.53, 2492.39, 2583.55, 2656.48, 2720.43, 2789.14, 
   2843.03, 2892.88, 2958.74, 3024.11, 3089.38, 3158.06, 3208.80, 3265.83, 
   3328.75, 3383.30, 3435.80, 3485.35, 3532.13, 3600.06, 3668.79, 3733.60, 
   3798.38
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.54582        start      
        1            56       9.056e-004      8.40187     
        2            57       1.636e-006      0.00529     
        3            58       1.165e-009     1.678e-006   
        4            59       9.062e-012     3.181e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3966.18 psi
   
   Pressure: 
   2164.67, 2267.60, 2358.76, 2462.83, 2552.63, 2624.60, 2687.77, 2755.71, 
   2809.03, 2858.39, 2923.65, 2988.45, 3053.19, 3121.35, 3171.72, 3228.37, 
   3290.90, 3345.13, 3397.36, 3446.67, 3493.25, 3560.95, 3629.48, 3694.15, 
   3758.86
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.23410        start      
        1            56       6.963e-004      9.21445     
        2            57       1.339e-006      0.00651     
        3            58       1.046e-009     2.574e-006   
        4            59       6.568e-012     4.252e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3940.32 psi
   
   Pressure: 
   2151.54, 2252.78, 2342.64, 2445.38, 2534.14, 2605.34, 2667.88, 2735.18, 
   2788.03, 2836.98, 2901.72, 2966.04, 3030.32, 3098.01, 3148.07, 3204.38, 
   3266.56, 3320.51, 3372.49, 3421.58, 3467.99, 3535.46, 3603.80, 3668.33, 
   3732.97
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.52235        start      
        1            56       4.877e-004      9.08226     
        2            57       9.272e-007      0.00609     
        3            58       6.451e-010     2.715e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3920.71 psi
   
   Pressure: 
   2142.48, 2242.48, 2331.34, 2433.03, 2520.95, 2591.52, 2653.53, 2720.29, 
   2772.74, 2821.33, 2885.63, 2949.52, 3013.40, 3080.70, 3130.47, 3186.48, 
   3248.35, 3302.05, 3353.80, 3402.70, 3448.94, 3516.21, 3584.38, 3648.79, 
   3713.35
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.05190        start      
        1            56       6.213e-004      11.8003     
        2            57       1.620e-006      0.01063     
        3            58       1.506e-009     6.768e-006   
        4            59       8.190e-012     1.130e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3904.48 psi
   
   Pressure: 
   2135.45, 2234.44, 2322.48, 2423.28, 2510.48, 2580.50, 2642.06, 2708.34, 
   2760.43, 2808.71, 2872.60, 2936.12, 2999.64, 3066.57, 3116.08, 3171.82, 
   3233.41, 3286.88, 3338.43, 3387.15, 3433.24, 3500.31, 3568.32, 3632.61, 
   3697.10
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.73872        start      
        1            56       4.058e-004      10.4125     
        2            57       9.465e-007      0.00758     
        3            58       6.682e-010     4.537e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3890.41 psi
   
   Pressure: 
   2129.61, 2227.75, 2315.08, 2415.11, 2501.67, 2571.20, 2632.35, 2698.20, 
   2749.97, 2797.96, 2861.49, 2924.66, 2987.85, 3054.45, 3103.73, 3159.22, 
   3220.54, 3273.80, 3325.16, 3373.72, 3419.67, 3486.56, 3554.42, 3618.60, 
   3683.03
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 9:38:21 AM
   7/26/2026 9:40:10 AM
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
        0            1          348.181        start      
        1            56         0.00851       0.63383     
        2            57       1.313e-005     5.488e-004   
        3            58       8.133e-009     1.145e-006   
        4            59       1.064e-010     4.708e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2443.69 psi
   
   Pressure: 
   1609.33, 1626.49, 1641.87, 1659.61, 1675.06, 1687.57, 1698.64, 1710.65, 
   1720.16, 1729.04, 1740.90, 1752.79, 1764.79, 1777.53, 1787.05, 1797.87, 
   1809.95, 1820.54, 1830.86, 1840.72, 1850.18, 1865.06, 1934.64, 2054.12, 
   2165.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.53, 0.72, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          335.785        start      
        1            56         0.00428       0.22529     
        2            57       1.285e-005     1.385e-004   
        3            58       4.987e-009     3.840e-007   
        4            59       1.275e-010     2.273e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2644.61 psi
   
   Pressure: 
   1609.42, 1626.59, 1641.99, 1659.73, 1675.20, 1687.73, 1698.81, 1710.83, 
   1720.35, 1729.25, 1741.13, 1753.04, 1765.05, 1777.82, 1787.36, 1798.20, 
   1810.30, 1820.91, 1831.30, 1849.09, 1929.32, 2047.21, 2161.96, 2266.44, 
   2366.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.34, 0.67, 0.74, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          326.093        start      
        1            56         0.00628       0.62383     
        2            57       9.099e-006     2.815e-004   
        3            58       6.005e-009     6.152e-007   
        4            59       1.470e-010     2.565e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2796.59 psi
   
   Pressure: 
   1608.84, 1625.92, 1641.23, 1658.88, 1674.26, 1686.71, 1697.73, 1709.68, 
   1719.15, 1727.99, 1739.79, 1751.62, 1763.55, 1776.24, 1785.71, 1796.48, 
   1809.66, 1864.96, 1954.07, 2036.00, 2111.24, 2218.37, 2324.80, 2423.12, 
   2518.82
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.54, 0.71, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          333.636        start      
        1            56         0.00466       0.34468     
        2            57       1.472e-005     1.517e-004   
        3            58       5.275e-009     4.544e-007   
        4            59       1.132e-010     2.055e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2976.45 psi
   
   Pressure: 
   1608.96, 1626.05, 1641.38, 1659.05, 1674.45, 1686.93, 1697.96, 1709.93, 
   1719.42, 1728.28, 1740.10, 1751.97, 1763.93, 1776.71, 1794.89, 1886.85, 
   1989.33, 2076.11, 2158.12, 2234.63, 2305.96, 2408.34, 2510.64, 2605.69, 
   2698.86
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          334.721        start      
        1            56         0.00700       0.69687     
        2            57       1.533e-005     3.693e-004   
        3            58       4.999e-009     9.806e-007   
        4            59       1.214e-010     2.442e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3135.88 psi
   
   Pressure: 
   1608.31, 1625.30, 1640.53, 1658.09, 1673.39, 1685.78, 1696.74, 1708.63, 
   1718.04, 1726.83, 1738.57, 1750.97, 1802.05, 1912.20, 1991.41, 2078.65, 
   2173.75, 2255.57, 2333.74, 2407.06, 2475.79, 2574.87, 2674.26, 2767.02, 
   2858.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.51, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          295.263        start      
        1            56         0.00233       0.72331     
        2            57       4.564e-006     7.893e-005   
        3            58       4.041e-009     9.539e-008   
        4            59       1.430e-010     8.250e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3313.90 psi
   
   Pressure: 
   1607.86, 1624.78, 1639.95, 1657.44, 1672.69, 1685.04, 1695.96, 1707.81, 
   1717.21, 1728.72, 1816.78, 1917.90, 2016.44, 2118.53, 2193.21, 2276.61, 
   2368.17, 2447.25, 2523.05, 2594.32, 2661.29, 2758.07, 2855.41, 2946.51, 
   3036.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.27, 0.62, 0.72, 0.74, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          327.542        start      
        1            56         0.00421       0.47780     
        2            57       1.216e-005     1.623e-004   
        3            58       2.194e-009     4.524e-007   
        4            59       1.304e-010     9.209e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3439.29 psi
   
   Pressure: 
   1608.35, 1625.35, 1640.58, 1658.15, 1673.47, 1685.86, 1696.90, 1719.92, 
   1799.93, 1875.31, 1972.45, 2067.56, 2161.87, 2260.59, 2333.22, 2414.61, 
   2504.15, 2581.60, 2655.93, 2725.89, 2791.73, 2886.99, 2982.94, 3072.92, 
   3162.20
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 
   0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          314.218        start      
        1            56         0.00692       0.94808     
        2            57       1.249e-005     3.586e-004   
        3            58       5.537e-009     9.012e-007   
        4            59       1.261e-010     3.081e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3606.53 psi
   
   Pressure: 
   1607.02, 1623.81, 1638.85, 1656.20, 1672.29, 1733.35, 1827.99, 1927.70, 
   2004.48, 2074.80, 2167.16, 2258.50, 2349.53, 2445.18, 2515.76, 2595.01, 
   2682.37, 2758.05, 2830.80, 2899.38, 2964.03, 3057.73, 3152.31, 3241.19, 
   3329.63
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.22, 0.53, 0.70, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          284.195        start      
        1            56         0.00265       1.36803     
        2            57       3.568e-006     1.291e-004   
        3            58       4.649e-009     1.240e-007   
        4            59       1.193e-010     1.622e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3821.62 psi
   
   Pressure: 
   1605.50, 1622.07, 1641.06, 1764.72, 1894.20, 1995.05, 2082.67, 2176.31, 
   2249.47, 2317.03, 2406.22, 2494.72, 2583.12, 2676.17, 2744.95, 2822.30, 
   2907.69, 2981.75, 3053.05, 3120.35, 3183.90, 3276.16, 3369.45, 3457.30, 
   3544.95
   
   Saturation:
   0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          307.950        start      
        1            56         0.00529       2.67934     
        2            57       7.708e-006      0.00125     
        3            58       3.829e-009     2.297e-006   
        4            59       1.029e-010     1.238e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4091.59 psi
   
   Pressure: 
   1701.22, 1837.20, 1959.78, 2097.56, 2215.50, 2309.68, 2392.33, 2481.27, 
   2551.15, 2615.95, 2701.79, 2787.22, 2872.75, 2962.97, 3029.79, 3105.08, 
   3188.34, 3260.68, 3330.45, 3396.41, 3458.81, 3549.60, 3641.57, 3728.38, 
   3815.22
   
   Saturation:
   0.36, 0.67, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          20.4192        start      
        1            56         0.00119       1.34086     
        2            57       1.502e-006      0.00149     
        3            58       2.078e-010     1.184e-006   
        4            59       1.532e-011     8.489e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2336.56, 2463.71, 2575.10, 2701.52, 2810.18, 2897.00, 2973.08, 3054.79, 
   3118.84, 3178.07, 3256.31, 3333.96, 3411.49, 3493.06, 3553.32, 3621.05, 
   3695.79, 3760.58, 3822.94, 3881.80, 3937.37, 4018.08, 4099.73, 4176.72, 
   4253.69
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.50876        start      
        1            56       7.941e-004      1.98955     
        2            57       1.213e-006      0.00227     
        3            58       4.022e-010     2.225e-006   
        4            59       9.423e-012     5.316e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2311.15, 2436.55, 2547.49, 2674.03, 2783.16, 2870.59, 2947.31, 3029.80, 
   3094.54, 3154.47, 3233.69, 3312.35, 3390.95, 3473.69, 3534.84, 3603.62, 
   3679.54, 3745.39, 3808.81, 3868.69, 3925.27, 4007.49, 4090.74, 4169.29, 
   4247.91
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.25339        start      
        1            56       7.965e-004      2.26997     
        2            57       1.503e-006      0.00332     
        3            58       5.204e-010     4.430e-006   
        4            59       8.769e-012     1.265e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2301.20, 2425.66, 2536.10, 2662.35, 2771.40, 2858.88, 2935.70, 3018.37, 
   3083.29, 3143.41, 3222.94, 3301.95, 3380.92, 3464.08, 3525.58, 3594.77, 
   3671.18, 3737.48, 3801.36, 3861.71, 3918.75, 4001.70, 4085.73, 4165.10, 
   4244.61
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.23693        start      
        1            56       5.681e-004      2.03315     
        2            57       9.662e-007      0.00266     
        3            58       2.493e-010     3.342e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2295.57, 2419.40, 2529.45, 2655.37, 2764.25, 2851.65, 2928.45, 3011.13, 
   3076.09, 3136.28, 3215.92, 3295.08, 3374.22, 3457.60, 3519.28, 3588.69, 
   3665.37, 3731.94, 3796.10, 3856.73, 3914.07, 3997.50, 4082.07, 4161.99, 
   4242.14
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.64891        start      
        1            56       8.232e-004      2.53272     
        2            57       1.836e-006      0.00445     
        3            58       5.688e-010     7.751e-006   
        4            59       1.006e-011     2.222e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2291.71, 2415.06, 2524.79, 2650.42, 2759.10, 2846.40, 2923.13, 3005.77, 
   3070.72, 3130.92, 3210.61, 3289.83, 3369.07, 3452.57, 3514.35, 3583.91, 
   3660.78, 3727.52, 3791.88, 3852.72, 3910.29, 3994.08, 4079.07, 4159.44, 
   4240.10
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.17971        start      
        1            56       5.376e-004      2.10919     
        2            57       9.586e-007      0.00289     
        3            58       2.065e-010     4.110e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2288.76, 2411.73, 2521.17, 2646.54, 2755.04, 2842.22, 2918.87, 3001.45, 
   3066.37, 3126.57, 3206.26, 3285.52, 3364.80, 3448.38, 3510.23, 3579.89, 
   3656.90, 3723.78, 3788.29, 3849.30, 3907.04, 3991.14, 4076.47, 4157.22, 
   4238.32
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.84746        start      
        1            56       6.050e-004      2.29161     
        2            57       1.167e-006      0.00341     
        3            58       2.645e-010     5.398e-006   
        4            59       7.906e-012     1.108e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2286.35, 2409.00, 2518.20, 2643.32, 2751.64, 2838.71, 2915.29, 2997.79, 
   3062.68, 3122.85, 3202.54, 3281.80, 3361.12, 3444.74, 3506.65, 3576.39, 
   3653.50, 3720.49, 3785.13, 3846.28, 3904.18, 3988.53, 4074.17, 4155.24, 
   4236.72
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.59259        start      
        1            56       7.243e-004      2.55976     
        2            57       1.547e-006      0.00425     
        3            58       3.998e-010     7.609e-006   
        4            59       6.947e-012     1.888e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2284.30, 2406.67, 2515.64, 2640.55, 2748.71, 2835.66, 2912.16, 2994.59, 
   3059.44, 3119.58, 3199.25, 3278.52, 3357.85, 3441.51, 3503.46, 3573.26, 
   3650.46, 3717.54, 3782.29, 3843.56, 3901.59, 3986.18, 4072.08, 4153.45, 
   4235.27
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.38789        start      
        1            56       5.828e-004      2.34013     
        2            57       1.101e-006      0.00339     
        3            58       2.422e-010     5.419e-006   
        4            59       8.929e-012     1.108e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2282.50, 2404.62, 2513.40, 2638.10, 2746.11, 2832.96, 2909.37, 2991.74, 
   3056.54, 3116.65, 3196.30, 3275.56, 3354.90, 3438.58, 3500.57, 3570.42, 
   3647.69, 3714.86, 3779.71, 3841.08, 3899.23, 3984.02, 4070.17, 4151.80, 
   4233.94
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.19645        start      
        1            56       7.288e-004      2.66776     
        2            57       1.546e-006      0.00439     
        3            58       4.119e-010     7.992e-006   
        4            59       1.059e-011     2.081e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2280.89, 2402.78, 2511.38, 2635.89, 2743.76, 2830.51, 2906.85, 2989.15, 
   3053.90, 3113.99, 3193.61, 3272.86, 3352.20, 3435.91, 3497.92, 3567.82, 
   3645.16, 3712.40, 3777.33, 3838.79, 3897.06, 3982.03, 4068.40, 4150.28, 
   4232.71
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 9:41:18 AM
   7/26/2026 9:43:00 AM
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
        0            1          838.692        start      
        1            56         0.00682       0.31206     
        2            57       8.377e-006     1.861e-004   
        3            58       9.070e-010     2.323e-007   
        4            59       5.003e-010     2.727e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1760.29, 1801.13, 1837.72, 1879.88, 1916.62, 1946.34, 1972.63, 2001.12, 
   2023.67, 2044.73, 2072.82, 2100.97, 2129.33, 2159.46, 2181.94, 2207.47, 
   2236.35, 2303.41, 2514.65, 2710.18, 2888.94, 3142.78, 3394.49, 3626.57, 
   3851.86
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.42, 0.69, 0.73, 0.76, 0.77, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          643.340        start      
        1            56         0.00777       0.44311     
        2            57       1.273e-005     2.857e-004   
        3            58       2.227e-009     4.836e-007   
        4            59       2.938e-010     8.843e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1701.88, 1733.56, 1761.94, 1794.65, 1823.15, 1846.21, 1866.62, 1888.73, 
   1906.23, 1922.58, 1944.39, 1966.66, 2032.46, 2236.58, 2384.10, 2546.19, 
   2722.70, 2874.46, 3019.40, 3155.33, 3282.70, 3466.24, 3650.28, 3821.88, 
   3990.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.44, 0.69, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          545.896        start      
        1            56         0.00599       0.29862     
        2            57       1.191e-005     1.873e-004   
        3            58       1.522e-009     3.783e-007   
        4            59       1.835e-010     5.087e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1673.06, 1700.22, 1724.55, 1752.60, 1777.05, 1796.84, 1814.35, 1833.55, 
   1871.56, 1993.28, 2152.21, 2306.35, 2458.58, 2617.55, 2734.35, 2865.11, 
   3008.88, 3133.17, 3252.41, 3364.59, 3470.10, 3622.64, 3776.16, 3919.94, 
   4062.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.41, 0.68, 0.73, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          406.732        start      
        1            56         0.00285       0.69298     
        2            57       4.724e-006     1.079e-004   
        3            58       2.463e-009     1.251e-007   
        4            59       1.842e-010     6.799e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1650.43, 1674.03, 1695.18, 1719.59, 1747.01, 1879.15, 2011.06, 2149.75, 
   2256.86, 2355.12, 2484.30, 2612.15, 2739.65, 2873.66, 2972.59, 3083.73, 
   3206.27, 3312.44, 3414.54, 3510.80, 3601.54, 3733.07, 3865.79, 3990.47, 
   4114.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.26, 0.63, 0.72, 0.74, 
   0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          353.317        start      
        1            56         0.00295       0.83147     
        2            57       5.459e-006     1.563e-004   
        3            58       7.452e-009     2.065e-007   
        4            59       1.858e-010     3.297e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1631.00, 1658.09, 1798.10, 1982.26, 2137.35, 2259.73, 2366.59, 2481.12, 
   2570.79, 2653.70, 2763.23, 2872.00, 2980.68, 3095.10, 3179.69, 3274.83, 
   3379.85, 3470.94, 3558.63, 3641.38, 3719.50, 3832.90, 3947.50, 4055.38, 
   4162.88
   
   Saturation:
   0.20, 0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          64.7185        start      
        1            56         0.00195       1.29946     
        2            57       1.484e-006      0.00162     
        3            58       5.482e-010     3.573e-007   
        4            59       3.520e-011     3.446e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2339.73, 2467.22, 2577.96, 2703.29, 2810.89, 2896.86, 2972.25, 3053.29, 
   3116.90, 3175.79, 3253.70, 3331.13, 3408.55, 3490.13, 3550.48, 3618.39, 
   3693.42, 3758.54, 3821.28, 3880.53, 3936.52, 4017.90, 4100.25, 4177.89, 
   4255.42
   
   Saturation:
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          9.88780        start      
        1            56       8.563e-004      1.76816     
        2            57       1.186e-006      0.00182     
        3            58       3.390e-010     1.561e-006   
        4            59       9.348e-012     2.961e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2315.28, 2440.82, 2551.62, 2677.87, 2786.71, 2873.86, 2950.35, 3032.61, 
   3097.18, 3156.97, 3236.05, 3314.61, 3393.15, 3475.86, 3537.02, 3605.84, 
   3681.84, 3747.79, 3811.31, 3871.31, 3928.01, 4010.42, 4093.84, 4172.52, 
   4251.16
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.85866        start      
        1            56       7.768e-004      2.05604     
        2            57       1.375e-006      0.00284     
        3            58       4.605e-010     3.509e-006   
        4            59       8.729e-012     9.354e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2301.73, 2426.13, 2536.47, 2662.56, 2771.47, 2858.84, 2935.59, 3018.20, 
   3083.09, 3143.22, 3222.78, 3301.87, 3380.94, 3464.26, 3525.89, 3595.27, 
   3671.90, 3738.42, 3802.52, 3863.09, 3920.35, 4003.62, 4087.97, 4167.58, 
   4247.23
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.56762        start      
        1            56       7.274e-004      2.11434     
        2            57       1.426e-006      0.00331     
        3            58       4.308e-010     4.846e-006   
        4            59       8.270e-012     1.242e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2295.16, 2418.88, 2528.84, 2654.65, 2763.45, 2850.80, 2927.58, 3010.26, 
   3075.25, 3135.49, 3215.22, 3294.51, 3373.81, 3457.38, 3519.22, 3588.85, 
   3665.78, 3732.59, 3796.99, 3857.85, 3915.42, 3999.19, 4084.07, 4164.25, 
   4244.53
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.88488        start      
        1            56       6.307e-004      2.02780     
        2            57       1.211e-006      0.00311     
        3            58       3.002e-010     4.663e-006   
        4            59       8.590e-012     1.006e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2291.03, 2414.28, 2523.91, 2649.46, 2758.10, 2845.38, 2922.13, 3004.80, 
   3069.80, 3130.07, 3209.87, 3289.24, 3368.65, 3452.35, 3514.31, 3584.08, 
   3661.19, 3728.17, 3792.76, 3853.83, 3911.61, 3995.72, 4080.99, 4161.59, 
   4242.35
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.40804        start      
        1            56       6.332e-004      2.07832     
        2            57       1.259e-006      0.00331     
        3            58       2.866e-010     5.340e-006   
        4            59       1.095e-011     1.098e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2288.05, 2410.92, 2520.30, 2645.60, 2754.08, 2841.27, 2917.95, 3000.58, 
   3065.56, 3125.83, 3205.65, 3285.06, 3364.51, 3448.29, 3510.31, 3580.18, 
   3657.42, 3724.52, 3789.25, 3850.46, 3908.41, 3992.78, 4078.38, 4159.31, 
   4240.48
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.03472        start      
        1            56       6.883e-004      2.21479     
        2            57       1.453e-006      0.00376     
        3            58       3.299e-010     6.664e-006   
        4            59       8.576e-012     1.449e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2285.70, 2408.27, 2517.41, 2642.50, 2750.82, 2837.91, 2914.52, 2997.09, 
   3062.04, 3122.29, 3202.11, 3281.52, 3361.00, 3444.82, 3506.89, 3576.82, 
   3654.15, 3721.35, 3786.19, 3847.52, 3905.60, 3990.20, 4076.06, 4157.30, 
   4238.82
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.74348        start      
        1            56       8.001e-004      2.43972     
        2            57       1.838e-006      0.00453     
        3            58       4.533e-010     8.970e-006   
        4            59       7.545e-012     2.234e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2283.74, 2406.04, 2514.98, 2639.87, 2748.05, 2835.03, 2911.57, 2994.08, 
   3058.99, 3119.21, 3199.01, 3278.42, 3357.91, 3441.75, 3503.85, 3573.83, 
   3651.24, 3718.51, 3783.44, 3844.88, 3903.07, 3987.88, 4073.97, 4155.47, 
   4237.31
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.52949        start      
        1            56       6.426e-004      2.22939     
        2            57       1.300e-006      0.00360     
        3            58       2.671e-010     6.353e-006   
        4            59       6.652e-012     1.283e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2282.05, 2404.12, 2512.88, 2637.58, 2745.61, 2832.50, 2908.97, 2991.41, 
   3056.28, 3116.47, 3196.24, 3275.64, 3355.13, 3438.99, 3501.11, 3571.13, 
   3648.59, 3715.93, 3780.94, 3842.47, 3900.77, 3985.75, 4072.06, 4153.80, 
   4235.92
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.34641        start      
        1            56       5.285e-004      2.06075     
        2            57       9.538e-007      0.00293     
        3            58       1.696e-010     4.659e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2280.55, 2402.40, 2510.99, 2635.52, 2743.42, 2830.22, 2906.61, 2988.98, 
   3053.81, 3113.97, 3193.71, 3273.09, 3352.58, 3436.45, 3498.59, 3568.64, 
   3646.16, 3713.55, 3778.63, 3840.24, 3898.64, 3983.78, 4070.29, 4152.24, 
   4234.64
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.19482        start      
        1            56       6.924e-004      2.40003     
        2            57       1.438e-006      0.00397     
        3            58       3.180e-010     7.375e-006   
        4            59       6.496e-012     1.642e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2279.19, 2400.85, 2509.28, 2633.64, 2741.42, 2828.13, 2904.46, 2986.76, 
   3051.54, 3111.68, 3191.39, 3270.75, 3350.23, 3434.10, 3496.26, 3566.34, 
   3643.90, 3711.34, 3776.49, 3838.17, 3896.65, 3981.94, 4068.63, 4150.80, 
   4233.44
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.02105        start      
        1            56       5.653e-004      2.21824     
        2            57       1.041e-006      0.00321     
        3            58       2.041e-010     5.331e-006   
        4            59       7.947e-012     1.018e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2277.94, 2399.42, 2507.71, 2631.92, 2739.57, 2826.20, 2902.46, 2984.70, 
   3049.44, 3109.54, 3189.22, 3268.56, 3348.03, 3431.91, 3494.08, 3564.18, 
   3641.78, 3709.28, 3774.48, 3836.23, 3894.79, 3980.23, 4067.08, 4149.44, 
   4232.31
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.92092        start      
        1            56       7.754e-004      2.63395     
        2            57       1.675e-006      0.00454     
        3            58       4.289e-010     8.931e-006   
        4            59       8.676e-012     2.334e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2276.78, 2398.09, 2506.24, 2630.31, 2737.85, 2824.39, 2900.59, 2982.77, 
   3047.47, 3107.54, 3187.19, 3266.51, 3345.98, 3429.85, 3492.03, 3562.16, 
   3639.80, 3707.33, 3772.59, 3834.41, 3893.04, 3978.61, 4065.63, 4148.16, 
   4231.25
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.78303        start      
        1            56       6.498e-004      2.46223     
        2            57       1.263e-006      0.00377     
        3            58       2.930e-010     6.723e-006   
        4            59       7.469e-012     1.550e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2275.69, 2396.85, 2504.87, 2628.80, 2736.24, 2822.70, 2898.84, 2980.96, 
   3045.62, 3105.66, 3185.28, 3264.58, 3344.04, 3427.92, 3490.10, 3560.25, 
   3637.92, 3705.50, 3770.81, 3832.69, 3891.39, 3977.08, 4064.25, 4146.96, 
   4230.26
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.69038        start      
        1            56       5.733e-004      2.34787     
        2            57       1.034e-006      0.00331     
        3            58       2.259e-010     5.498e-006   
        4            59       9.504e-012     1.174e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2274.67, 2395.68, 2503.58, 2627.38, 2734.71, 2821.11, 2897.18, 2979.25, 
   3043.87, 3103.88, 3183.47, 3262.76, 3342.20, 3426.08, 3488.27, 3558.44, 
   3636.15, 3703.76, 3769.12, 3831.06, 3889.83, 3975.64, 4062.95, 4145.82, 
   4229.31
   
   Saturation:
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 9:44:26 AM
   7/26/2026 9:46:12 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

