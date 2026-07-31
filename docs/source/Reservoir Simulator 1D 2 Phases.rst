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
        0            1          198.173        start      
        1            56         0.00310       5.58908     
        2            57       4.343e-006     5.916e-004   
        3            58       1.665e-008     2.274e-007   
        4            59       6.957e-011     1.229e-009   
   Producer BHP: 
   2268.55 psi
   
   Injector BHP: 
   2739.08 psi
   
   Pressure: 
   2300.49, 2307.17, 2314.78, 2324.11, 2331.66, 2339.54, 2348.07, 2354.70, 
   2362.84, 2370.45, 2377.66, 2391.58, 2404.77, 2411.69, 2419.74, 2428.48, 
   2435.39, 2441.34, 2447.08, 2453.30, 2458.50, 2463.01, 2468.22, 2485.15, 
   2547.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.43, 
   0.71
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.833        start      
        1            56         0.00285       5.97456     
        2            57       2.988e-006     6.183e-004   
        3            58       9.099e-009     3.724e-007   
        4            59       5.600e-011     8.376e-010   
   Producer BHP: 
   1621.28 psi
   
   Injector BHP: 
   2140.92 psi
   
   Pressure: 
   1653.29, 1659.98, 1667.59, 1676.92, 1684.47, 1692.33, 1700.84, 1707.45, 
   1715.56, 1723.13, 1730.30, 1744.14, 1757.23, 1764.09, 1772.07, 1780.72, 
   1787.55, 1793.44, 1799.10, 1805.23, 1810.36, 1815.19, 1841.63, 1892.83, 
   1948.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.54, 0.72, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.413        start      
        1            56         0.00317       0.25313     
        2            57       9.075e-006     1.116e-004   
        3            58       6.676e-009     3.238e-007   
        4            59       3.547e-011     2.146e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2016.54 psi
   
   Pressure: 
   1523.10, 1527.99, 1533.62, 1540.60, 1546.31, 1552.35, 1558.95, 1564.16, 
   1570.61, 1576.73, 1582.59, 1594.08, 1605.09, 1610.94, 1617.84, 1625.44, 
   1631.52, 1636.83, 1642.01, 1647.71, 1653.91, 1683.73, 1725.61, 1772.19, 
   1824.47
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.296        start      
        1            56         0.00422       0.20838     
        2            57       2.082e-005     1.810e-004   
        3            58       2.912e-008     7.992e-007   
        4            59       4.834e-011     1.942e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2053.68 psi
   
   Pressure: 
   1523.10, 1527.99, 1533.62, 1540.60, 1546.32, 1552.35, 1558.96, 1564.16, 
   1570.62, 1576.73, 1582.60, 1594.08, 1605.10, 1610.94, 1617.84, 1625.44, 
   1631.52, 1636.83, 1642.03, 1651.68, 1691.74, 1727.81, 1767.08, 1811.31, 
   1861.62
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.33, 0.66, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          173.066        start      
        1            56         0.00647       0.23969     
        2            57       3.025e-005     2.935e-004   
        3            58       2.088e-008     1.692e-006   
        4            59       4.508e-011     8.663e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2094.55 psi
   
   Pressure: 
   1523.10, 1527.98, 1533.61, 1540.59, 1546.30, 1552.33, 1558.93, 1564.13, 
   1570.59, 1576.70, 1582.56, 1594.04, 1605.05, 1610.89, 1617.79, 1625.38, 
   1631.46, 1636.83, 1649.30, 1698.31, 1738.99, 1773.11, 1810.72, 1853.46, 
   1902.51
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.40, 0.68, 0.74, 0.76, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.276        start      
        1            56         0.00811       0.40575     
        2            57       3.168e-005     5.030e-004   
        3            58       3.839e-009     2.595e-006   
        4            59       4.700e-011     1.635e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2134.51 psi
   
   Pressure: 
   1523.06, 1527.93, 1533.55, 1540.51, 1546.22, 1552.24, 1558.83, 1564.02, 
   1570.46, 1576.56, 1582.41, 1593.87, 1604.86, 1610.68, 1617.57, 1625.15, 
   1631.47, 1651.94, 1696.88, 1744.57, 1783.34, 1816.17, 1852.65, 1894.35, 
   1942.50
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.49, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.305        start      
        1            56         0.00506       0.61703     
        2            57       4.182e-006     2.191e-004   
        3            58       8.787e-009     6.128e-007   
        4            59       4.919e-011     6.009e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2181.91 psi
   
   Pressure: 
   1522.99, 1527.85, 1533.45, 1540.39, 1546.08, 1552.08, 1558.65, 1563.83, 
   1570.25, 1576.33, 1582.17, 1593.59, 1604.55, 1610.36, 1617.23, 1625.94, 
   1662.02, 1707.84, 1750.85, 1796.50, 1834.00, 1865.92, 1901.56, 1942.46, 
   1989.93
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 
   0.57, 0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.346        start      
        1            56         0.00317       0.49135     
        2            57       1.465e-005     2.514e-004   
        3            58       3.032e-008     5.126e-007   
        4            59       7.034e-011     1.270e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2245.21 psi
   
   Pressure: 
   1523.01, 1527.87, 1533.47, 1540.42, 1546.11, 1552.12, 1558.69, 1563.87, 
   1570.30, 1576.39, 1582.23, 1593.66, 1604.63, 1610.45, 1620.37, 1681.24, 
   1733.44, 1777.27, 1818.75, 1863.13, 1899.76, 1931.03, 1966.05, 2006.34, 
   2053.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.64, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.241        start      
        1            56         0.00403       0.32485     
        2            57       1.898e-005     1.845e-004   
        3            58       2.986e-008     7.515e-007   
        4            59       4.580e-011     1.549e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2293.40 psi
   
   Pressure: 
   1523.06, 1527.93, 1533.54, 1540.50, 1546.20, 1552.22, 1558.81, 1564.00, 
   1570.44, 1576.53, 1582.39, 1593.84, 1604.87, 1615.25, 1672.98, 1737.44, 
   1787.46, 1829.95, 1870.43, 1913.90, 1949.87, 1980.64, 2015.17, 2054.98, 
   2101.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.824        start      
        1            56         0.00656       0.61377     
        2            57       2.950e-005     5.968e-004   
        3            58       5.557e-009     2.903e-006   
        4            59       3.424e-011     2.207e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2339.41 psi
   
   Pressure: 
   1522.96, 1527.82, 1533.41, 1540.34, 1546.02, 1552.01, 1558.57, 1563.73, 
   1570.15, 1576.21, 1582.04, 1593.56, 1620.20, 1669.97, 1728.30, 1790.14, 
   1838.59, 1880.01, 1919.61, 1962.25, 1997.62, 2027.93, 2062.00, 2101.39, 
   2147.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.40, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.679        start      
        1            56         0.00775       0.94524     
        2            57       3.102e-005     8.679e-004   
        3            58       4.822e-009     4.088e-006   
        4            59       5.342e-011     4.704e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2426.52 psi
   
   Pressure: 
   1522.85, 1527.68, 1533.24, 1540.14, 1545.79, 1551.75, 1558.27, 1563.41, 
   1569.79, 1575.83, 1581.80, 1622.71, 1717.85, 1766.88, 1822.86, 1882.94, 
   1930.21, 1970.76, 2009.62, 2051.54, 2086.36, 2116.26, 2149.92, 2188.92, 
   2234.71
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.47, 0.70, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          162.677        start      
        1            56         0.00621       0.57241     
        2            57       1.772e-005     3.751e-004   
        3            58       7.531e-009     1.700e-006   
        4            59       4.833e-011     4.592e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2498.31 psi
   
   Pressure: 
   1522.97, 1527.82, 1533.41, 1540.34, 1546.01, 1552.00, 1558.56, 1563.73, 
   1570.14, 1576.67, 1605.25, 1704.72, 1796.78, 1844.32, 1899.16, 1958.31, 
   2004.93, 2044.98, 2083.39, 2124.86, 2159.33, 2188.94, 2222.32, 2261.02, 
   2306.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.53, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          152.285        start      
        1            56         0.00411       0.65826     
        2            57       3.162e-006     1.688e-004   
        3            58       5.650e-009     4.529e-007   
        4            59       4.388e-011     3.971e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2537.42 psi
   
   Pressure: 
   1522.92, 1527.77, 1533.34, 1540.26, 1545.93, 1551.90, 1558.45, 1563.61, 
   1571.02, 1607.58, 1658.04, 1753.40, 1842.56, 1888.97, 1942.78, 2000.98, 
   2046.93, 2086.45, 2124.39, 2165.39, 2199.50, 2228.84, 2261.95, 2300.39, 
   2345.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.24, 0.57, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.648        start      
        1            56         0.00283       0.65024     
        2            57       1.086e-005     1.707e-004   
        3            58       1.662e-008     3.493e-007   
        4            59       4.705e-011     4.641e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2583.65 psi
   
   Pressure: 
   1522.92, 1527.76, 1533.33, 1540.25, 1545.91, 1551.89, 1558.44, 1565.41, 
   1613.17, 1665.64, 1714.21, 1806.90, 1894.19, 1939.80, 1992.83, 2050.27, 
   2095.66, 2134.73, 2172.27, 2212.87, 2246.68, 2275.77, 2308.64, 2346.85, 
   2391.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 
   0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.223        start      
        1            56         0.00396       0.39349     
        2            57       1.829e-005     1.808e-004   
        3            58       1.393e-008     8.715e-007   
        4            59       5.338e-011     1.103e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2622.91 psi
   
   Pressure: 
   1523.01, 1527.87, 1533.46, 1540.40, 1546.09, 1552.11, 1563.43, 1606.41, 
   1661.68, 1712.37, 1759.84, 1851.04, 1937.23, 1982.33, 2034.85, 2091.78, 
   2136.79, 2175.55, 2212.80, 2253.11, 2286.69, 2315.60, 2348.29, 2386.31, 
   2431.25
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 
   0.73, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.587        start      
        1            56         0.00621       0.45081     
        2            57       3.171e-005     3.285e-004   
        3            58       1.902e-008     2.052e-006   
        4            59       5.386e-011     9.485e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2665.98 psi
   
   Pressure: 
   1522.98, 1527.84, 1533.42, 1540.36, 1546.09, 1560.10, 1616.34, 1660.44, 
   1713.72, 1763.10, 1809.67, 1899.46, 1984.48, 2029.04, 2080.96, 2137.31, 
   2181.88, 2220.28, 2257.21, 2297.19, 2330.53, 2359.25, 2391.74, 2429.58, 
   2474.35
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.67, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.192        start      
        1            56         0.00712       0.47232     
        2            57       3.167e-005     4.791e-004   
        3            58       1.132e-008     2.760e-006   
        4            59       3.568e-011     9.459e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2707.39 psi
   
   Pressure: 
   1522.97, 1527.82, 1533.40, 1540.54, 1559.53, 1611.63, 1667.49, 1710.19, 
   1762.24, 1810.76, 1856.68, 1945.38, 2029.46, 2073.57, 2125.00, 2180.85, 
   2225.05, 2263.15, 2299.80, 2339.50, 2372.62, 2401.17, 2433.49, 2471.16, 
   2515.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.46, 0.70, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.119        start      
        1            56         0.00652       0.82550     
        2            57       1.843e-005     5.107e-004   
        3            58       2.875e-009     2.481e-006   
        4            59       5.328e-011     3.917e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2750.07 psi
   
   Pressure: 
   1522.82, 1527.63, 1533.59, 1567.16, 1616.30, 1666.61, 1720.45, 1762.06, 
   1813.02, 1860.67, 1905.85, 1993.29, 2076.27, 2119.83, 2170.67, 2225.93, 
   2269.68, 2307.42, 2343.75, 2383.14, 2416.01, 2444.38, 2476.52, 2514.02, 
   2558.49
   
   Saturation:
   0.20, 0.20, 0.22, 0.53, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.518        start      
        1            56         0.00390       0.70461     
        2            57       2.112e-006     1.260e-004   
        3            58       4.889e-009     3.615e-007   
        4            59       3.554e-011     7.193e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2795.75 psi
   
   Pressure: 
   1522.87, 1528.55, 1562.98, 1622.90, 1670.47, 1719.49, 1772.36, 1813.40, 
   1863.78, 1910.95, 1955.73, 2042.43, 2124.75, 2167.98, 2218.46, 2273.34, 
   2316.80, 2354.30, 2390.41, 2429.57, 2462.27, 2490.49, 2522.50, 2559.86, 
   2604.21
   
   Saturation:
   0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.748        start      
        1            56         0.00261       0.84686     
        2            57       1.010e-005     2.717e-004   
        3            58       8.501e-009     1.190e-006   
        4            59       4.717e-011     7.451e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2836.80 psi
   
   Pressure: 
   1531.35, 1568.35, 1616.36, 1674.02, 1720.27, 1768.30, 1820.29, 1860.76, 
   1910.52, 1957.15, 2001.46, 2087.31, 2168.87, 2211.72, 2261.78, 2316.22, 
   2359.35, 2396.59, 2432.46, 2471.38, 2503.89, 2531.97, 2563.83, 2601.06, 
   2645.29
   
   Saturation:
   0.28, 0.63, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/31/2026 9:48:00 AM
   7/31/2026 9:49:34 AM
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
        0            1          250.260        start      
        1            56         0.00374       0.20955     
        2            57       1.671e-005     2.323e-004   
        3            58       2.884e-008     4.706e-007   
        4            59       1.097e-010     1.022e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2194.76 psi
   
   Pressure: 
   1534.70, 1542.04, 1550.50, 1560.99, 1569.58, 1578.64, 1588.57, 1596.39, 
   1606.09, 1615.27, 1624.09, 1641.34, 1657.90, 1666.68, 1677.05, 1688.46, 
   1697.60, 1705.58, 1713.37, 1721.93, 1729.20, 1735.59, 1746.66, 1819.20, 
   1906.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.65, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          231.199        start      
        1            56         0.00298       0.29943     
        2            57       5.558e-006     7.725e-005   
        3            58       5.173e-009     1.377e-007   
        4            59       6.182e-011     1.395e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2273.87 psi
   
   Pressure: 
   1534.65, 1541.98, 1550.42, 1560.90, 1569.47, 1578.52, 1588.43, 1596.24, 
   1605.93, 1615.10, 1623.90, 1641.12, 1657.65, 1666.41, 1676.77, 1688.16, 
   1697.29, 1705.25, 1713.03, 1721.58, 1730.73, 1775.55, 1838.35, 1907.92, 
   1985.90
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.73, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          243.338        start      
        1            56         0.00552       0.53401     
        2            57       7.255e-006     2.704e-004   
        3            58       6.170e-009     6.439e-007   
        4            59       9.953e-011     3.262e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2358.70 psi
   
   Pressure: 
   1534.55, 1541.86, 1550.27, 1560.71, 1569.26, 1578.28, 1588.16, 1595.94, 
   1605.59, 1614.73, 1623.51, 1640.67, 1657.14, 1665.88, 1676.20, 1687.55, 
   1696.64, 1704.58, 1713.13, 1759.66, 1822.25, 1874.54, 1931.84, 1996.67, 
   2070.80
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.23, 0.55, 0.72, 0.75, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          257.729        start      
        1            56         0.00753       0.44588     
        2            57       2.281e-005     4.022e-004   
        3            58       5.414e-009     1.475e-006   
        4            59       7.419e-011     2.921e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2449.82 psi
   
   Pressure: 
   1534.56, 1541.87, 1550.29, 1560.73, 1569.29, 1578.31, 1588.19, 1595.97, 
   1605.63, 1614.77, 1623.55, 1640.72, 1657.19, 1665.93, 1676.25, 1687.60, 
   1697.03, 1727.00, 1794.50, 1865.93, 1923.95, 1973.08, 2027.65, 2090.00, 
   2162.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.48, 0.70, 0.74, 0.76, 0.77, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          256.896        start      
        1            56         0.00767       0.55305     
        2            57       2.750e-005     4.925e-004   
        3            58       2.936e-009     1.914e-006   
        4            59       7.236e-011     1.887e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2558.77 psi
   
   Pressure: 
   1534.48, 1541.77, 1550.16, 1560.58, 1569.11, 1578.11, 1587.96, 1595.72, 
   1605.36, 1614.47, 1623.22, 1640.35, 1656.78, 1665.49, 1675.93, 1708.08, 
   1786.79, 1853.91, 1917.04, 1984.33, 2039.74, 2086.99, 2139.81, 2200.50, 
   2271.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.43, 
   0.69, 0.73, 0.75, 0.76, 0.78, 0.78, 0.79, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          243.218        start      
        1            56         0.00349       0.40041     
        2            57       1.212e-005     1.474e-004   
        3            58       1.178e-008     4.110e-007   
        4            59       7.102e-011     4.877e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2686.91 psi
   
   Pressure: 
   1534.50, 1541.80, 1550.20, 1560.63, 1569.16, 1578.17, 1588.03, 1595.80, 
   1605.45, 1614.57, 1623.34, 1640.48, 1656.99, 1672.02, 1758.45, 1854.98, 
   1929.82, 1993.38, 2053.91, 2118.90, 2172.68, 2218.69, 2270.29, 2329.82, 
   2399.37
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          215.217        start      
        1            56         0.00296       1.35808     
        2            57       4.313e-006     1.585e-004   
        3            58       4.507e-009     1.964e-007   
        4            59       1.040e-010     2.126e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2822.77 psi
   
   Pressure: 
   1533.88, 1541.05, 1549.29, 1559.53, 1567.91, 1576.75, 1586.44, 1594.07, 
   1603.54, 1612.50, 1621.11, 1642.19, 1758.65, 1833.11, 1917.63, 2007.90, 
   2078.84, 2139.66, 2197.91, 2260.75, 2312.95, 2357.78, 2408.26, 2466.72, 
   2535.38
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          248.890        start      
        1            56         0.00612       0.60726     
        2            57       1.487e-005     3.462e-004   
        3            58       5.491e-009     1.132e-006   
        4            59       7.263e-011     3.116e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2990.44 psi
   
   Pressure: 
   1534.35, 1541.61, 1549.98, 1560.35, 1568.84, 1577.81, 1587.62, 1595.35, 
   1604.95, 1614.58, 1654.78, 1803.82, 1941.58, 2012.68, 2094.65, 2183.03, 
   2252.68, 2312.51, 2369.88, 2431.82, 2483.30, 2527.53, 2577.39, 2635.21, 
   2703.24
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.52, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          253.128        start      
        1            56         0.00563       0.46124     
        2            57       2.013e-005     2.900e-004   
        3            58       1.726e-009     1.114e-006   
        4            59       5.428e-011     6.764e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3080.64 psi
   
   Pressure: 
   1534.41, 1541.69, 1550.06, 1560.45, 1568.96, 1577.94, 1587.77, 1595.60, 
   1618.94, 1697.44, 1771.81, 1912.71, 2044.91, 2113.84, 2193.83, 2280.39, 
   2348.74, 2407.53, 2463.97, 2524.98, 2575.75, 2619.43, 2668.72, 2725.99, 
   2793.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.40, 0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          234.686        start      
        1            56         0.00319       0.50830     
        2            57       1.076e-005     1.387e-004   
        3            58       8.275e-009     3.934e-007   
        4            59       7.134e-011     4.307e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3175.75 psi
   
   Pressure: 
   1534.35, 1541.61, 1549.97, 1560.35, 1568.84, 1577.82, 1593.55, 1657.28, 
   1739.99, 1815.76, 1886.65, 2022.82, 2151.45, 2218.76, 2297.12, 2382.06, 
   2449.22, 2507.05, 2562.64, 2622.79, 2672.90, 2716.06, 2764.86, 2821.64, 
   2888.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.65, 
   0.72, 0.74, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          216.286        start      
        1            56         0.00319       0.84776     
        2            57       2.680e-006     9.356e-005   
        3            58       4.306e-009     6.689e-008   
        4            59       7.473e-011     1.248e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3270.62 psi
   
   Pressure: 
   1534.07, 1541.27, 1549.56, 1559.85, 1569.97, 1630.24, 1715.01, 1779.37, 
   1857.50, 1930.16, 1998.81, 2131.37, 2256.97, 2322.85, 2399.68, 2483.10, 
   2549.12, 2606.05, 2660.82, 2720.16, 2769.66, 2812.34, 2860.67, 2917.00, 
   2983.74
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 0.71, 0.74, 
   0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.277        start      
        1            56         0.00661       0.81708     
        2            57       1.711e-005     4.831e-004   
        3            58       1.954e-009     1.789e-006   
        4            59       1.209e-010     2.442e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3361.92 psi
   
   Pressure: 
   1534.06, 1541.26, 1549.97, 1594.54, 1668.14, 1743.41, 1823.91, 1886.08, 
   1962.22, 2033.39, 2100.87, 2231.42, 2355.30, 2420.33, 2496.23, 2578.70, 
   2644.00, 2700.34, 2754.57, 2813.34, 2862.41, 2904.75, 2952.73, 3008.72, 
   3075.14
   
   Saturation:
   0.20, 0.20, 0.21, 0.51, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          250.928        start      
        1            56         0.00589       0.49585     
        2            57       2.307e-005     3.171e-004   
        3            58       3.037e-008     1.818e-006   
        4            59       7.507e-011     2.216e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3460.65 psi
   
   Pressure: 
   1534.66, 1552.49, 1624.49, 1712.95, 1783.38, 1856.22, 1934.88, 1996.00, 
   2071.05, 2141.32, 2208.04, 2337.20, 2459.81, 2524.20, 2599.39, 2681.10, 
   2745.82, 2801.66, 2855.44, 2913.74, 2962.45, 3004.49, 3052.16, 3107.84, 
   3173.98
   
   Saturation:
   0.20, 0.40, 0.68, 0.73, 0.75, 0.75, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          134.970        start      
        1            56         0.00834       6.51400     
        2            57       7.610e-006      0.00456     
        3            58       1.010e-008     1.696e-006   
        4            59       3.727e-011     3.494e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3648.94 psi
   
   Pressure: 
   1753.50, 1812.60, 1878.83, 1959.32, 2024.38, 2092.30, 2166.06, 2223.67, 
   2294.66, 2361.37, 2424.93, 2548.36, 2665.89, 2727.77, 2800.25, 2879.27, 
   2942.00, 2996.28, 3048.70, 3105.69, 3153.43, 3194.75, 3241.77, 3296.84, 
   3362.49
   
   Saturation:
   0.63, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          13.5196        start      
        1            56       7.463e-004      1.83212     
        2            57       9.435e-007     8.316e-004   
        3            58       1.750e-010     3.677e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3730.84 psi
   
   Pressure: 
   1794.32, 1854.86, 1923.45, 2007.34, 2075.24, 2146.12, 2223.01, 2282.95, 
   2356.68, 2425.79, 2491.47, 2618.67, 2739.47, 2802.92, 2877.02, 2957.57, 
   3021.38, 3076.45, 3129.49, 3187.03, 3235.11, 3276.64, 3323.77, 3378.89, 
   3444.49
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.41034        start      
        1            56       5.826e-004      3.51317     
        2            57       9.194e-007      0.00152     
        3            58       4.199e-010     6.239e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3703.81 psi
   
   Pressure: 
   1783.16, 1842.15, 1909.33, 1991.80, 2058.69, 2128.62, 2204.57, 2263.83, 
   2336.78, 2405.20, 2470.26, 2596.34, 2716.13, 2779.08, 2852.63, 2932.61, 
   2996.00, 3050.73, 3103.48, 3160.72, 3208.58, 3249.95, 3296.93, 3351.92, 
   3417.42
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.69659        start      
        1            56       6.004e-004      5.06479     
        2            57       1.193e-006      0.00288     
        3            58       7.786e-010     1.429e-006   
        4            59       7.397e-012     1.488e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3685.16 psi
   
   Pressure: 
   1777.27, 1835.28, 1901.50, 1982.91, 2049.03, 2118.21, 2193.39, 2252.10, 
   2324.39, 2392.23, 2456.76, 2581.87, 2700.79, 2763.29, 2836.36, 2915.85, 
   2978.87, 3033.30, 3085.78, 3142.75, 3190.41, 3231.63, 3278.47, 3333.34, 
   3398.75
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.79800        start      
        1            56       5.673e-004      6.37505     
        2            57       1.330e-006      0.00422     
        3            58       1.010e-009     2.556e-006   
        4            59       6.760e-012     2.800e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3670.32 psi
   
   Pressure: 
   1773.29, 1830.58, 1896.05, 1976.62, 2042.10, 2110.65, 2185.19, 2243.41, 
   2315.13, 2382.47, 2446.54, 2570.80, 2688.93, 2751.05, 2823.68, 2902.73, 
   2965.41, 3019.58, 3071.81, 3128.55, 3176.03, 3217.11, 3263.82, 3318.57, 
   3383.90
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.30925        start      
        1            56       4.864e-004      6.92861     
        2            57       1.216e-006      0.00460     
        3            58       9.175e-010     3.056e-006   
        4            59       7.748e-012     3.038e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3657.66 psi
   
   Pressure: 
   1770.22, 1826.93, 1891.78, 1971.63, 2036.56, 2104.56, 2178.53, 2236.32, 
   2307.54, 2374.42, 2438.08, 2561.57, 2679.00, 2740.76, 2813.00, 2891.65, 
   2954.02, 3007.94, 3059.95, 3116.47, 3163.78, 3204.73, 3251.33, 3305.97, 
   3371.21
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.92967        start      
        1            56       4.547e-004      7.64960     
        2            57       1.255e-006      0.00523     
        3            58       9.819e-010     3.861e-006   
        4            59       7.268e-012     3.893e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3646.47 psi
   
   Pressure: 
   1767.68, 1823.90, 1888.21, 1967.43, 2031.87, 2099.39, 2172.84, 2230.25, 
   2301.01, 2367.48, 2430.76, 2553.54, 2670.33, 2731.77, 2803.65, 2881.92, 
   2944.02, 2997.71, 3049.52, 3105.83, 3152.99, 3193.82, 3240.31, 3294.84, 
   3360.02
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/31/2026 9:50:32 AM
   7/31/2026 9:52:10 AM
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
        0            1          344.596        start      
        1            56         0.00692       0.41543     
        2            57       8.595e-006     3.148e-004   
        3            58       5.742e-009     6.543e-007   
        4            59       9.039e-011     2.706e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2461.11 psi
   
   Pressure: 
   1546.24, 1556.03, 1567.30, 1581.28, 1592.72, 1604.80, 1618.03, 1628.45, 
   1641.38, 1653.61, 1665.36, 1688.34, 1710.39, 1722.09, 1735.90, 1751.10, 
   1763.27, 1773.90, 1784.27, 1795.67, 1805.35, 1814.52, 1865.77, 1966.61, 
   2077.29
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.54, 0.72, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          326.022        start      
        1            56         0.00367       0.27648     
        2            57       1.081e-005     1.342e-004   
        3            58       5.737e-009     3.220e-007   
        4            59       8.767e-011     2.374e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2605.75 psi
   
   Pressure: 
   1546.19, 1555.97, 1567.22, 1581.18, 1592.62, 1604.68, 1617.89, 1628.30, 
   1641.22, 1653.44, 1665.18, 1688.14, 1710.18, 1721.86, 1735.67, 1750.86, 
   1763.02, 1773.65, 1784.04, 1802.82, 1883.52, 1955.61, 2033.91, 2121.97, 
   2222.11
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.32, 0.66, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          345.058        start      
        1            56         0.00756       0.48810     
        2            57       1.910e-005     3.632e-004   
        3            58       5.070e-009     1.057e-006   
        4            59       1.420e-010     2.408e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2764.64 psi
   
   Pressure: 
   1546.07, 1555.81, 1567.03, 1580.95, 1592.35, 1604.38, 1617.55, 1627.92, 
   1640.79, 1652.98, 1664.67, 1687.55, 1709.50, 1721.14, 1734.89, 1750.02, 
   1762.55, 1801.86, 1891.86, 1986.99, 2064.24, 2129.66, 2202.31, 2285.33, 
   2381.23
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.48, 0.70, 0.74, 0.76, 0.77, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          301.517        start      
        1            56         0.00273       0.75018     
        2            57       6.994e-006     1.378e-004   
        3            58       5.954e-009     2.022e-007   
        4            59       1.121e-010     1.994e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2980.67 psi
   
   Pressure: 
   1545.66, 1555.32, 1566.44, 1580.24, 1591.54, 1603.47, 1616.53, 1626.82, 
   1639.59, 1651.68, 1663.28, 1686.00, 1707.79, 1719.37, 1738.25, 1858.25, 
   1962.07, 2049.13, 2131.50, 2219.63, 2292.37, 2354.50, 2424.09, 2504.20, 
   2597.58
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.64, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          332.138        start      
        1            56         0.00554       0.70919     
        2            57       1.459e-005     3.686e-004   
        3            58       2.472e-009     9.235e-007   
        4            59       1.263e-010     1.565e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3166.10 psi
   
   Pressure: 
   1545.69, 1555.35, 1566.47, 1580.27, 1591.57, 1603.50, 1616.56, 1626.84, 
   1639.61, 1651.69, 1663.28, 1686.14, 1734.64, 1833.76, 1949.92, 2072.90, 
   2169.20, 2251.52, 2330.22, 2414.95, 2485.22, 2545.47, 2613.20, 2691.50, 
   2783.29
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.38, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          336.866        start      
        1            56         0.00643       0.64992     
        2            57       1.404e-005     3.373e-004   
        3            58       4.473e-009     8.974e-007   
        4            59       9.402e-011     2.303e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3478.81 psi
   
   Pressure: 
   1545.70, 1555.36, 1566.48, 1580.29, 1591.59, 1603.51, 1616.57, 1626.85, 
   1639.62, 1652.31, 1702.66, 1901.10, 2084.39, 2178.93, 2287.87, 2405.30, 
   2497.84, 2577.33, 2653.54, 2735.82, 2804.22, 2862.99, 2929.23, 3006.06, 
   3096.48
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.51, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          288.322        start      
        1            56         0.00294       0.91068     
        2            57       2.931e-006     8.621e-005   
        3            58       3.343e-009     6.822e-008   
        4            59       1.096e-010     7.592e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3640.76 psi
   
   Pressure: 
   1545.31, 1554.89, 1565.92, 1579.61, 1590.81, 1602.64, 1615.60, 1628.23, 
   1716.65, 1820.82, 1917.04, 2100.48, 2273.15, 2363.35, 2468.22, 2581.83, 
   2671.63, 2748.94, 2823.23, 2903.61, 2970.57, 3028.22, 3093.37, 3169.15, 
   3258.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          321.767        start      
        1            56         0.00449       0.67572     
        2            57       1.389e-005     1.833e-004   
        3            58       2.627e-009     5.556e-007   
        4            59       7.699e-011     1.433e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3805.33 psi
   
   Pressure: 
   1545.51, 1555.13, 1566.20, 1579.95, 1591.25, 1614.30, 1724.57, 1812.25, 
   1917.91, 2015.76, 2107.95, 2285.63, 2453.80, 2541.92, 2644.63, 2756.07, 
   2844.23, 2920.21, 2993.29, 3072.42, 3138.42, 3195.30, 3259.68, 3334.68, 
   3423.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 0.66, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          334.913        start      
        1            56         0.00688       0.82045     
        2            57       1.631e-005     4.488e-004   
        3            58       1.410e-009     1.370e-006   
        4            59       1.140e-010     1.339e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3967.21 psi
   
   Pressure: 
   1545.27, 1554.84, 1566.25, 1618.90, 1716.86, 1817.07, 1924.13, 2006.78, 
   2107.95, 2202.50, 2292.13, 2465.51, 2629.99, 2716.33, 2817.10, 2926.58, 
   3013.26, 3088.04, 3160.02, 3238.04, 3303.18, 3359.39, 3423.08, 3497.41, 
   3585.61
   
   Saturation:
   0.20, 0.20, 0.21, 0.49, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          285.397        start      
        1            56         0.00456       1.19740     
        2            57       4.525e-006     2.219e-004   
        3            58       9.201e-009     5.464e-007   
        4            59       8.728e-011     6.120e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4132.16 psi
   
   Pressure: 
   1553.90, 1619.47, 1714.95, 1829.32, 1920.92, 2015.98, 2118.84, 2198.88, 
   2297.27, 2389.48, 2477.11, 2646.87, 2808.13, 2892.86, 2991.85, 3099.51, 
   3184.83, 3258.48, 3329.45, 3406.44, 3470.78, 3526.37, 3589.44, 3663.16, 
   3750.82
   
   Saturation:
   0.25, 0.60, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          32.5167        start      
        1            56       9.236e-004      0.40787     
        2            57       1.144e-006     6.554e-004   
        3            58       1.392e-010     7.009e-007   
        4            59       2.818e-011     6.599e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4471.28 psi
   
   Pressure: 
   1899.47, 1980.77, 2072.46, 2184.26, 2274.62, 2368.82, 2470.93, 2550.49, 
   2648.30, 2739.96, 2827.05, 2995.69, 3155.81, 3239.90, 3338.12, 3444.89, 
   3529.46, 3602.46, 3672.77, 3749.05, 3812.79, 3867.86, 3930.36, 4003.45, 
   4090.44
   
   Saturation:
   0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.69666        start      
        1            56       9.745e-004      5.19296     
        2            57       1.655e-006      0.00227     
        3            58       8.414e-010     8.629e-007   
        4            59       8.787e-012     8.950e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4431.10 psi
   
   Pressure: 
   1877.94, 1956.62, 2046.17, 2156.03, 2245.12, 2338.22, 2439.30, 2518.15, 
   2615.17, 2706.16, 2792.65, 2960.22, 3119.38, 3203.00, 3300.68, 3406.89, 
   3491.05, 3563.70, 3633.71, 3709.66, 3773.17, 3828.04, 3890.37, 3963.31, 
   4050.21
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.71391        start      
        1            56       6.572e-004      6.68502     
        2            57       1.193e-006      0.00319     
        3            58       7.525e-010     1.232e-006   
        4            59       8.281e-012     1.269e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4398.74 psi
   
   Pressure: 
   1867.61, 1944.57, 2032.41, 2140.42, 2228.13, 2319.91, 2419.64, 2497.51, 
   2593.39, 2683.36, 2768.94, 2934.84, 3092.50, 3175.37, 3272.22, 3377.60, 
   3461.13, 3533.28, 3602.84, 3678.36, 3741.53, 3796.17, 3858.27, 3931.02, 
   4017.80
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.50740        start      
        1            56       5.662e-004      8.16166     
        2            57       1.195e-006      0.00442     
        3            58       8.361e-010     2.058e-006   
        4            59       7.060e-012     2.013e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4374.56 psi
   
   Pressure: 
   1861.25, 1937.03, 2023.64, 2130.25, 2216.91, 2307.64, 2406.29, 2483.36, 
   2578.30, 2667.43, 2752.25, 2916.73, 3073.11, 3155.34, 3251.49, 3356.14, 
   3439.12, 3510.83, 3580.00, 3655.13, 3718.02, 3772.44, 3834.35, 3906.92, 
   3993.57
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.87656        start      
        1            56       4.700e-004      8.73748     
        2            57       1.059e-006      0.00470     
        3            58       7.269e-010     2.396e-006   
        4            59       6.602e-012     2.051e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4354.51 psi
   
   Pressure: 
   1856.51, 1931.38, 2017.00, 2122.47, 2208.24, 2298.09, 2395.82, 2472.20, 
   2566.32, 2654.72, 2738.87, 2902.11, 3057.36, 3139.02, 3234.54, 3338.55, 
   3421.05, 3492.37, 3561.19, 3635.97, 3698.60, 3752.83, 3814.55, 3886.96, 
   3973.50
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.38173        start      
        1            56       4.248e-004      9.49455     
        2            57       1.052e-006      0.00518     
        3            58       7.416e-010     2.905e-006   
        4            59       8.385e-012     2.489e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4337.17 psi
   
   Pressure: 
   1852.66, 1926.77, 2011.56, 2116.05, 2201.05, 2290.14, 2387.06, 2462.83, 
   2556.23, 2643.98, 2727.53, 2889.67, 3043.91, 3125.06, 3220.01, 3323.43, 
   3405.50, 3476.47, 3544.97, 3619.44, 3681.83, 3735.88, 3797.43, 3869.70, 
   3956.13
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.99023        start      
        1            56       4.145e-004      10.4604     
        2            57       1.144e-006      0.00588     
        3            58       8.703e-010     3.604e-006   
        4            59       8.402e-012     3.466e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4321.81 psi
   
   Pressure: 
   1849.38, 1922.83, 2006.90, 2110.54, 2194.86, 2283.26, 2379.47, 2454.70, 
   2547.45, 2634.61, 2717.63, 2878.76, 3032.08, 3112.77, 3207.22, 3310.11, 
   3391.78, 3462.42, 3530.64, 3604.82, 3667.00, 3720.88, 3782.29, 3854.41, 
   3940.74
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.68843        start      
        1            56       4.381e-004      11.7155     
        2            57       1.354e-006      0.00696     
        3            58       1.175e-009     4.575e-006   
        4            59       1.122e-011     5.580e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4307.99 psi
   
   Pressure: 
   1846.51, 1919.38, 2002.81, 2105.68, 2189.40, 2277.18, 2372.74, 2447.48, 
   2539.65, 2626.27, 2708.80, 2869.02, 3021.51, 3101.78, 3195.75, 3298.16, 
   3379.47, 3449.82, 3517.77, 3591.69, 3653.67, 3707.40, 3768.66, 3840.67, 
   3926.90
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.46268        start      
        1            56       3.321e-004      10.7450     
        2            57       9.297e-007      0.00539     
        3            58       7.157e-010     3.242e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4295.43 psi
   
   Pressure: 
   1843.95, 1916.30, 1999.15, 2101.32, 2184.50, 2271.73, 2366.69, 2440.98, 
   2532.62, 2618.76, 2700.84, 2860.22, 3011.94, 3091.82, 3185.37, 3287.33, 
   3368.30, 3438.38, 3506.09, 3579.77, 3641.57, 3695.16, 3756.29, 3828.18, 
   3914.32
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.29352        start      
        1            56       4.054e-004      12.4574     
        2            57       1.288e-006      0.00703     
        3            58       1.225e-009     4.448e-006   
        4            59       7.371e-012     6.538e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4283.93 psi
   
   Pressure: 
   1841.64, 1913.52, 1995.84, 2097.38, 2180.06, 2266.77, 2361.20, 2435.08, 
   2526.22, 2611.91, 2693.58, 2852.19, 3003.21, 3082.73, 3175.88, 3277.44, 
   3358.10, 3427.93, 3495.41, 3568.86, 3630.49, 3683.96, 3744.97, 3816.74, 
   3902.80
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/31/2026 9:53:13 AM
   7/31/2026 9:54:39 AM
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
        0            1          449.816        start      
        1            56         0.00697       0.21617     
        2            57       1.900e-005     2.078e-004   
        3            58       2.189e-009     5.959e-007   
        4            59       1.162e-010     9.930e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2746.14 psi
   
   Pressure: 
   1557.93, 1570.19, 1584.30, 1601.81, 1616.15, 1631.28, 1647.85, 1660.90, 
   1677.10, 1692.43, 1707.14, 1735.94, 1763.56, 1778.21, 1795.52, 1814.57, 
   1829.81, 1843.13, 1856.12, 1870.41, 1882.63, 1906.17, 2012.84, 2133.28, 
   2266.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 0.69, 0.75, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          433.622        start      
        1            56         0.00718       0.37213     
        2            57       2.066e-005     2.606e-004   
        3            58       3.254e-009     7.522e-007   
        4            59       1.360e-010     1.240e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2982.14 psi
   
   Pressure: 
   1557.72, 1569.93, 1583.99, 1601.43, 1615.72, 1630.79, 1647.30, 1660.30, 
   1676.43, 1691.70, 1706.36, 1735.05, 1762.57, 1777.17, 1794.42, 1813.39, 
   1828.58, 1841.96, 1871.72, 1994.79, 2096.27, 2181.24, 2274.84, 2381.13, 
   2503.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.39, 0.68, 0.74, 0.76, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          427.451        start      
        1            56         0.00943       0.72629     
        2            57       2.692e-005     5.042e-004   
        3            58       5.179e-009     1.457e-006   
        4            59       1.440e-010     2.642e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3256.77 psi
   
   Pressure: 
   1557.31, 1569.44, 1583.39, 1600.71, 1614.89, 1629.85, 1646.24, 1659.14, 
   1675.16, 1690.31, 1704.86, 1733.32, 1760.63, 1775.11, 1792.42, 1843.15, 
   1974.03, 2085.54, 2190.36, 2302.07, 2394.05, 2472.50, 2560.20, 2660.99, 
   2778.24
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.42, 
   0.68, 0.73, 0.75, 0.77, 0.78, 0.78, 0.79, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          412.398        start      
        1            56         0.00681       0.86133     
        2            57       1.727e-005     4.190e-004   
        3            58       3.453e-009     9.839e-007   
        4            59       2.036e-010     1.991e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3575.39 psi
   
   Pressure: 
   1556.96, 1569.01, 1582.88, 1600.09, 1614.19, 1629.06, 1645.34, 1658.16, 
   1674.08, 1689.14, 1703.60, 1732.07, 1790.36, 1913.74, 2058.62, 2211.94, 
   2331.97, 2434.56, 2532.64, 2638.25, 2725.84, 2800.94, 2885.38, 2983.01, 
   3097.47
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.38, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          398.347        start      
        1            56         0.00414       0.65832     
        2            57       1.113e-005     1.537e-004   
        3            58       4.882e-009     3.427e-007   
        4            59       1.480e-010     1.944e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4018.34 psi
   
   Pressure: 
   1557.06, 1569.13, 1583.02, 1600.26, 1614.38, 1629.27, 1645.59, 1658.44, 
   1674.44, 1699.69, 1821.07, 2063.87, 2288.63, 2405.05, 2539.56, 2684.72, 
   2799.18, 2897.54, 2991.90, 3093.79, 3178.54, 3251.39, 3333.55, 3428.91, 
   3541.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.32, 0.66, 0.73, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          373.071        start      
        1            56         0.00324       0.82849     
        2            57       8.139e-006     1.294e-004   
        3            58       4.868e-009     2.195e-007   
        4            59       1.338e-010     1.709e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4263.89 psi
   
   Pressure: 
   1556.72, 1568.71, 1582.52, 1599.65, 1613.68, 1628.51, 1651.66, 1753.64, 
   1890.63, 2015.93, 2133.02, 2357.76, 2569.97, 2681.01, 2810.26, 2950.39, 
   3061.19, 3156.62, 3248.37, 3347.67, 3430.43, 3501.73, 3582.37, 3676.24, 
   3787.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.64, 
   0.72, 0.74, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          360.240        start      
        1            56         0.00323       0.71462     
        2            57       4.295e-006     1.147e-004   
        3            58       5.366e-009     1.268e-007   
        4            59       1.389e-010     1.657e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1556.20, 1568.08, 1581.77, 1603.28, 1703.23, 1830.35, 1965.18, 2068.68, 
   2195.06, 2312.98, 2424.63, 2640.40, 2844.95, 2952.26, 3077.44, 3213.36, 
   3320.93, 3413.67, 3502.90, 3599.56, 3680.20, 3749.73, 3828.47, 3920.28, 
   4029.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          358.932        start      
        1            56         0.00582       0.62685     
        2            57       8.291e-006     2.800e-004   
        3            58       2.229e-009     9.001e-007   
        4            59       1.607e-010     4.721e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1555.80, 1611.97, 1722.65, 1855.28, 1961.28, 2071.15, 2189.91, 2282.28, 
   2395.75, 2502.05, 2603.00, 2798.50, 2984.11, 3081.58, 3195.41, 3319.11, 
   3417.08, 3501.62, 3583.02, 3671.27, 3744.97, 3808.59, 3880.73, 3964.98, 
   4065.09
   
   Saturation:
   0.22, 0.54, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          32.6701        start      
        1            56       9.384e-004      0.53126     
        2            57       1.078e-006     6.563e-004   
        3            58       9.876e-011     6.419e-007   
        4            59       1.823e-011     5.032e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1903.48, 1985.59, 2078.19, 2191.10, 2282.35, 2377.48, 2480.60, 2560.94, 
   2659.72, 2752.30, 2840.26, 3010.64, 3172.43, 3257.41, 3356.66, 3464.56, 
   3550.03, 3623.80, 3694.87, 3771.95, 3836.36, 3892.01, 3955.16, 4029.01, 
   4116.89
   
   Saturation:
   0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.85312        start      
        1            56       7.365e-004      1.32722     
        2            57       1.146e-006      0.00129     
        3            58       3.406e-010     1.215e-006   
        4            59       1.320e-011     2.772e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1885.85, 1966.19, 2057.65, 2169.87, 2260.88, 2356.01, 2459.30, 2539.90, 
   2639.11, 2732.18, 2820.68, 2992.20, 3155.16, 3240.80, 3340.88, 3449.74, 
   3536.01, 3610.52, 3682.32, 3760.26, 3825.43, 3881.76, 3945.76, 4020.67, 
   4109.94
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.87408        start      
        1            56       6.028e-004      1.54878     
        2            57       1.014e-006      0.00166     
        3            58       3.179e-010     1.785e-006   
        4            59       1.060e-011     4.120e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1879.66, 1959.15, 2049.91, 2161.51, 2252.16, 2347.02, 2450.13, 2530.64, 
   2629.81, 2722.90, 2811.46, 2983.17, 3146.39, 3232.21, 3332.53, 3441.70, 
   3528.26, 3603.04, 3675.15, 3753.45, 3818.97, 3875.65, 3940.08, 4015.57, 
   4105.62
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.69938        start      
        1            56       6.457e-004      1.81346     
        2            57       1.227e-006      0.00226     
        3            58       3.860e-010     2.913e-006   
        4            59       7.630e-012     6.968e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1876.32, 1955.28, 2045.53, 2156.64, 2246.97, 2341.56, 2444.43, 2524.80, 
   2623.84, 2716.84, 2805.35, 2977.03, 3140.28, 3226.13, 3326.55, 3435.86, 
   3522.55, 3597.48, 3669.77, 3748.30, 3814.04, 3870.95, 3935.68, 4011.59, 
   4102.24
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.00178        start      
        1            56       5.883e-004      1.86791     
        2            57       1.097e-006      0.00228     
        3            58       3.070e-010     3.011e-006   
        4            59       8.392e-012     6.296e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1874.05, 1952.62, 2042.49, 2153.19, 2243.24, 2337.58, 2440.22, 2520.44, 
   2619.32, 2712.20, 2800.63, 2972.21, 3135.40, 3221.25, 3321.69, 3431.07, 
   3517.85, 3592.88, 3665.29, 3743.98, 3809.90, 3866.98, 3931.96, 4008.22, 
   4099.35
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.50300        start      
        1            56       6.082e-004      2.00855     
        2            57       1.174e-006      0.00255     
        3            58       3.257e-010     3.593e-006   
        4            59       8.557e-012     7.710e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1872.32, 1950.58, 2040.13, 2150.49, 2240.28, 2334.40, 2436.82, 2516.89, 
   2615.62, 2708.37, 2796.71, 2968.15, 3131.26, 3217.08, 3317.53, 3426.95, 
   3513.78, 3588.88, 3661.38, 3740.21, 3806.27, 3863.50, 3928.69, 4005.24, 
   4096.79
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.15186        start      
        1            56       6.976e-004      2.24032     
        2            57       1.458e-006      0.00310     
        3            58       4.411e-010     4.849e-006   
        4            59       8.578e-012     1.181e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1870.91, 1948.90, 2038.17, 2148.23, 2237.81, 2331.71, 2433.93, 2513.86, 
   2612.44, 2705.07, 2793.31, 2964.61, 3127.62, 3213.41, 3313.85, 3423.29, 
   3510.16, 3585.31, 3657.89, 3736.84, 3803.02, 3860.38, 3925.75, 4002.56, 
   4094.49
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.83446        start      
        1            56       8.105e-004      2.50903     
        2            57       1.835e-006      0.00379     
        3            58       6.264e-010     6.523e-006   
        4            59       9.551e-012     1.873e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1869.70, 1947.46, 2036.50, 2146.28, 2235.65, 2329.37, 2431.39, 2511.20, 
   2609.63, 2702.14, 2790.29, 2961.44, 3124.35, 3210.11, 3310.53, 3419.98, 
   3506.88, 3582.08, 3654.73, 3733.77, 3800.06, 3857.53, 3923.07, 4000.11, 
   4092.39
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.60062        start      
        1            56       6.473e-004      2.30926     
        2            57       1.297e-006      0.00303     
        3            58       3.867e-010     4.655e-006   
        4            59       7.075e-012     1.121e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1868.64, 1946.20, 2035.01, 2144.55, 2233.74, 2327.28, 2429.12, 2508.81, 
   2607.10, 2699.50, 2787.57, 2958.58, 3121.38, 3207.11, 3307.51, 3416.96, 
   3503.89, 3579.13, 3651.83, 3730.96, 3797.35, 3854.92, 3920.61, 3997.87, 
   4090.46
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.40073        start      
        1            56       5.279e-004      2.14104     
        2            57       9.467e-007      0.00247     
        3            58       2.528e-010     3.419e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1867.69, 1945.06, 2033.67, 2142.99, 2232.00, 2325.38, 2427.06, 2506.62, 
   2604.79, 2697.09, 2785.07, 2955.94, 3118.65, 3204.34, 3304.72, 3414.18, 
   3501.12, 3576.40, 3649.15, 3728.36, 3794.84, 3852.51, 3918.33, 3995.79, 
   4088.67
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.19563        start      
        1            56       6.551e-004      2.45306     
        2            57       1.309e-006      0.00319     
        3            58       4.272e-010     4.953e-006   
        4            59       8.458e-012     1.326e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1866.82, 1944.02, 2032.45, 2141.56, 2230.42, 2323.63, 2425.16, 2504.62, 
   2602.67, 2694.87, 2782.77, 2953.52, 3116.13, 3201.78, 3302.14, 3411.60, 
   3498.56, 3573.87, 3646.67, 3725.95, 3792.51, 3850.27, 3916.22, 3993.86, 
   4087.00
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.04736        start      
        1            56       5.517e-004      2.30018     
        2            57       1.004e-006      0.00269     
        3            58       3.020e-010     3.808e-006   
        4            59       9.838e-012     9.191e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1866.02, 1943.06, 2031.33, 2140.24, 2228.95, 2322.02, 2423.41, 2502.76, 
   2600.70, 2692.80, 2780.63, 2951.25, 3113.78, 3199.40, 3299.74, 3409.19, 
   3496.17, 3571.51, 3644.35, 3723.70, 3790.33, 3848.18, 3914.25, 3992.05, 
   4085.45
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/31/2026 9:55:45 AM
   7/31/2026 9:57:14 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

