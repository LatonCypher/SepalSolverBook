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
        0            1          196.448        start      
        1            56         0.00247       5.65665     
        2            57       1.763e-006     6.503e-004   
        3            58       3.136e-009     9.058e-008   
        4            59       7.000e-011     2.060e-010   
   Producer BHP: 
   2262.09 psi
   
   Injector BHP: 
   2714.98 psi
   
   Pressure: 
   2288.87, 2295.93, 2304.10, 2316.73, 2327.93, 2333.63, 2339.74, 2347.02, 
   2355.39, 2363.26, 2369.63, 2380.46, 2391.64, 2400.69, 2410.23, 2419.96, 
   2429.61, 2435.84, 2440.37, 2445.48, 2452.68, 2458.99, 2464.69, 2479.39, 
   2534.88
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          185.876        start      
        1            56         0.00414       7.56653     
        2            57       4.652e-006     8.839e-004   
        3            58       1.669e-008     2.200e-007   
        4            59       5.762e-011     1.308e-009   
   Producer BHP: 
   1607.47 psi
   
   Injector BHP: 
   2109.22 psi
   
   Pressure: 
   1634.31, 1641.38, 1649.55, 1662.19, 1673.38, 1679.06, 1685.16, 1692.42, 
   1700.76, 1708.59, 1714.92, 1725.69, 1736.78, 1745.76, 1755.21, 1764.85, 
   1774.40, 1780.55, 1785.02, 1790.06, 1797.15, 1803.63, 1824.86, 1878.24, 
   1928.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.49, 0.71, 
   0.77
   
   
   
   ================================================
         Rejected (Minimum Pressure Violated) 
   ================================================
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.239        start      
        1            56         0.00325       0.34852     
        2            57       9.953e-007     1.106e-004   
        3            58       2.266e-009     1.904e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2013.61 psi
   
   Pressure: 
   1519.34, 1524.50, 1530.54, 1539.97, 1548.42, 1552.77, 1557.50, 1563.19, 
   1569.81, 1576.11, 1581.27, 1590.17, 1599.46, 1607.09, 1615.23, 1623.64, 
   1632.09, 1637.61, 1641.68, 1646.35, 1654.03, 1690.94, 1737.49, 1786.16, 
   1832.85
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.881        start      
        1            56         0.00324       0.32230     
        2            57       1.600e-005     2.337e-004   
        3            58       3.298e-008     5.011e-007   
        4            59       8.885e-011     9.622e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2066.56 psi
   
   Pressure: 
   1519.35, 1524.51, 1530.55, 1539.98, 1548.44, 1552.78, 1557.51, 1563.21, 
   1569.83, 1576.13, 1581.29, 1590.19, 1599.49, 1607.12, 1615.26, 1623.68, 
   1632.14, 1637.66, 1641.74, 1648.42, 1700.60, 1751.18, 1794.84, 1840.96, 
   1885.82
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.29, 0.63, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.590        start      
        1            56         0.00422       0.14060     
        2            57       1.989e-005     1.346e-004   
        3            58       2.638e-008     6.402e-007   
        4            59       5.216e-011     1.379e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2100.13 psi
   
   Pressure: 
   1519.38, 1524.55, 1530.60, 1540.05, 1548.51, 1552.87, 1557.61, 1563.31, 
   1569.94, 1576.25, 1581.42, 1590.33, 1599.64, 1607.28, 1615.44, 1623.87, 
   1632.34, 1637.90, 1645.43, 1684.87, 1741.47, 1789.37, 1831.13, 1875.68, 
   1919.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.35, 0.67, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.742        start      
        1            56         0.00649       0.26851     
        2            57       2.930e-005     3.287e-004   
        3            58       1.384e-008     1.760e-006   
        4            59       4.283e-011     5.652e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2127.48 psi
   
   Pressure: 
   1519.36, 1524.52, 1530.56, 1539.99, 1548.45, 1552.79, 1557.52, 1563.22, 
   1569.84, 1576.14, 1581.30, 1590.20, 1599.50, 1607.12, 1615.26, 1623.68, 
   1632.25, 1646.04, 1680.80, 1720.02, 1773.87, 1819.98, 1860.44, 1903.86, 
   1946.77
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.41, 0.68, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.744        start      
        1            56         0.00864       0.78498     
        2            57       3.023e-005     7.885e-004   
        3            58       1.149e-008     3.655e-006   
        4            59       5.659e-011     1.037e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2180.61 psi
   
   Pressure: 
   1519.25, 1524.38, 1530.38, 1539.75, 1548.16, 1552.48, 1557.18, 1562.84, 
   1569.42, 1575.68, 1580.82, 1589.66, 1598.90, 1606.48, 1614.57, 1623.36, 
   1659.61, 1707.42, 1741.58, 1779.03, 1830.95, 1875.71, 1915.14, 1957.67, 
   1999.93
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 
   0.51, 0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.515        start      
        1            56         0.00425       0.86475     
        2            57       2.903e-006     1.570e-004   
        3            58       3.234e-009     2.014e-007   
        4            59       4.684e-011     2.127e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2255.50 psi
   
   Pressure: 
   1519.21, 1524.33, 1530.32, 1539.68, 1548.06, 1552.38, 1557.07, 1562.72, 
   1569.28, 1575.54, 1580.66, 1589.49, 1598.71, 1606.28, 1615.88, 1670.39, 
   1743.14, 1789.04, 1821.82, 1858.17, 1908.81, 1952.59, 1991.27, 2033.13, 
   2074.87
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 0.59, 
   0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.79, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.812        start      
        1            56         0.00347       0.54789     
        2            57       1.647e-005     2.479e-004   
        3            58       3.344e-008     6.469e-007   
        4            59       6.139e-011     1.700e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2324.21 psi
   
   Pressure: 
   1519.28, 1524.41, 1530.42, 1539.81, 1548.22, 1552.55, 1557.26, 1562.92, 
   1569.51, 1575.79, 1580.93, 1589.78, 1599.06, 1610.50, 1676.00, 1748.13, 
   1817.93, 1862.37, 1894.34, 1929.97, 1979.72, 2022.81, 2060.93, 2102.28, 
   2143.62
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.65, 0.72, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.940        start      
        1            56         0.00556       0.48428     
        2            57       2.642e-005     3.809e-004   
        3            58       7.812e-009     1.977e-006   
        4            59       4.374e-011     3.663e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2381.28 psi
   
   Pressure: 
   1519.29, 1524.43, 1530.44, 1539.83, 1548.25, 1552.58, 1557.29, 1562.96, 
   1569.55, 1575.83, 1580.97, 1589.88, 1609.00, 1673.84, 1742.98, 1812.18, 
   1879.94, 1923.34, 1954.66, 1989.69, 2038.67, 2081.16, 2118.81, 2159.71, 
   2200.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.37, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.076        start      
        1            56         0.00719       0.58983     
        2            57       3.237e-005     6.096e-004   
        3            58       3.871e-009     3.164e-006   
        4            59       3.332e-011     2.693e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2448.90 psi
   
   Pressure: 
   1519.26, 1524.39, 1530.39, 1539.77, 1548.17, 1552.49, 1557.19, 1562.85, 
   1569.43, 1575.69, 1580.93, 1607.03, 1687.56, 1751.92, 1818.42, 1885.67, 
   1951.91, 1994.44, 2025.22, 2059.70, 2107.99, 2149.94, 2187.17, 2227.68, 
   2268.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.44, 0.69, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.940        start      
        1            56         0.00721       0.47940     
        2            57       2.680e-005     4.225e-004   
        3            58       5.501e-009     2.208e-006   
        4            59       3.906e-011     2.636e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2505.79 psi
   
   Pressure: 
   1519.29, 1524.42, 1530.43, 1539.82, 1548.24, 1552.56, 1557.27, 1562.93, 
   1569.52, 1576.17, 1598.43, 1675.76, 1753.82, 1816.04, 1880.99, 1946.98, 
   2012.15, 2054.07, 2084.44, 2118.51, 2166.26, 2207.78, 2244.66, 2284.86, 
   2325.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.089        start      
        1            56         0.00425       0.73929     
        2            57       1.667e-006     1.262e-004   
        3            58       3.113e-009     2.451e-007   
        4            59       3.078e-011     3.272e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2547.13 psi
   
   Pressure: 
   1519.20, 1524.31, 1530.29, 1539.64, 1548.02, 1552.33, 1557.01, 1562.65, 
   1570.26, 1609.77, 1654.22, 1728.32, 1803.67, 1864.29, 1927.90, 1992.69, 
   2056.79, 2098.08, 2128.03, 2161.68, 2208.89, 2249.99, 2286.54, 2326.44, 
   2366.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.24, 0.58, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.037        start      
        1            56         0.00288       0.67303     
        2            57       1.113e-005     1.601e-004   
        3            58       1.775e-008     3.512e-007   
        4            59       3.994e-011     5.616e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2593.38 psi
   
   Pressure: 
   1519.21, 1524.33, 1530.31, 1539.66, 1548.05, 1552.36, 1557.05, 1564.78, 
   1613.37, 1667.30, 1710.11, 1782.11, 1855.94, 1915.57, 1978.28, 2042.25, 
   2105.60, 2146.43, 2176.07, 2209.40, 2256.19, 2296.96, 2333.24, 2372.90, 
   2412.98
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 
   0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          162.793        start      
        1            56         0.00507       0.36268     
        2            57       2.633e-005     1.850e-004   
        3            58       2.696e-008     1.064e-006   
        4            59       3.793e-011     1.556e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2637.13 psi
   
   Pressure: 
   1519.31, 1524.45, 1530.46, 1539.86, 1548.28, 1552.63, 1561.54, 1609.31, 
   1666.08, 1718.27, 1760.13, 1831.00, 1903.92, 1962.93, 2025.06, 2088.47, 
   2151.30, 2191.81, 2221.23, 2254.32, 2300.80, 2341.31, 2377.38, 2416.84, 
   2456.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 
   0.72, 0.75, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.655        start      
        1            56         0.00663       0.29505     
        2            57       3.220e-005     2.980e-004   
        3            58       2.102e-008     1.860e-006   
        4            59       3.925e-011     1.065e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2667.22 psi
   
   Pressure: 
   1519.33, 1524.47, 1530.49, 1539.89, 1548.47, 1560.28, 1600.99, 1649.34, 
   1704.13, 1755.14, 1796.27, 1866.17, 1938.24, 1996.62, 2058.15, 2120.98, 
   2183.27, 2223.45, 2252.65, 2285.50, 2331.67, 2371.93, 2407.81, 2447.08, 
   2486.87
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.21, 0.42, 0.69, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.108        start      
        1            56         0.00738       0.87289     
        2            57       2.696e-005     7.484e-004   
        3            58       2.552e-009     3.672e-006   
        4            59       4.492e-011     3.127e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2702.85 psi
   
   Pressure: 
   1519.13, 1524.22, 1530.17, 1539.92, 1573.69, 1610.99, 1650.53, 1696.96, 
   1750.07, 1799.86, 1840.14, 1908.76, 1979.62, 2037.12, 2097.77, 2159.77, 
   2221.30, 2261.03, 2289.91, 2322.45, 2368.22, 2408.18, 2443.82, 2482.89, 
   2522.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.227        start      
        1            56         0.00497       1.29031     
        2            57       4.749e-006     2.909e-004   
        3            58       7.456e-009     9.873e-007   
        4            59       4.653e-011     7.444e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2773.46 psi
   
   Pressure: 
   1518.98, 1524.03, 1530.83, 1586.46, 1658.65, 1694.59, 1732.74, 1777.94, 
   1829.86, 1878.69, 1918.28, 1985.81, 2055.63, 2112.33, 2172.20, 2233.44, 
   2294.26, 2333.55, 2362.15, 2394.39, 2439.78, 2479.44, 2514.85, 2553.71, 
   2593.19
   
   Saturation:
   0.20, 0.20, 0.24, 0.57, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.445        start      
        1            56         0.00329       0.63541     
        2            57       1.542e-005     2.608e-004   
        3            58       5.375e-008     4.956e-007   
        4            59       5.458e-011     2.332e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2836.19 psi
   
   Pressure: 
   1519.23, 1526.68, 1574.30, 1655.22, 1725.51, 1760.85, 1798.65, 1843.57, 
   1895.26, 1943.91, 1983.38, 2050.69, 2120.30, 2176.82, 2236.48, 2297.51, 
   2358.12, 2397.26, 2425.75, 2457.87, 2503.08, 2542.59, 2577.86, 2616.59, 
   2655.96
   
   Saturation:
   0.20, 0.29, 0.64, 0.72, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.014        start      
        1            56         0.00434       0.90485     
        2            57       1.672e-005     7.799e-004   
        3            58       2.351e-008     3.775e-006   
        4            59       4.056e-011     4.666e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2877.76 psi
   
   Pressure: 
   1536.08, 1578.86, 1629.77, 1707.42, 1775.71, 1810.29, 1847.45, 1891.72, 
   1942.73, 1990.81, 2029.85, 2096.48, 2165.42, 2221.43, 2280.59, 2341.13, 
   2401.27, 2440.14, 2468.43, 2500.35, 2545.30, 2584.61, 2619.72, 2658.30, 
   2697.56
   
   Saturation:
   0.35, 0.67, 0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/31/2026 4:07:52 AM
   7/31/2026 4:08:48 AM
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
        0            1          241.658        start      
        1            56         0.00296       0.29991     
        2            57       8.583e-006     1.823e-004   
        3            58       6.104e-009     3.833e-007   
        4            59       7.854e-011     1.508e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2168.39 psi
   
   Pressure: 
   1529.07, 1536.83, 1545.90, 1560.09, 1572.80, 1579.34, 1586.44, 1595.00, 
   1604.95, 1614.43, 1622.19, 1635.57, 1649.54, 1661.01, 1673.25, 1685.90, 
   1698.61, 1706.92, 1713.04, 1720.05, 1730.07, 1738.98, 1749.48, 1818.88, 
   1897.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.63, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          233.603        start      
        1            56         0.00495       0.51094     
        2            57       2.130e-006     1.851e-004   
        3            58       4.422e-009     3.078e-007   
        4            59       5.583e-011     2.776e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2268.92 psi
   
   Pressure: 
   1529.00, 1536.73, 1545.78, 1559.92, 1572.60, 1579.12, 1586.20, 1594.74, 
   1604.66, 1614.11, 1621.85, 1635.18, 1649.12, 1660.55, 1672.75, 1685.37, 
   1698.04, 1706.31, 1712.42, 1719.41, 1730.81, 1785.76, 1855.57, 1928.26, 
   1997.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.58, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          254.270        start      
        1            56         0.00706       0.38913     
        2            57       1.721e-005     3.385e-004   
        3            58       5.613e-009     1.150e-006   
        4            59       5.908e-011     2.695e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2372.85 psi
   
   Pressure: 
   1529.02, 1536.76, 1545.81, 1559.96, 1572.65, 1579.17, 1586.26, 1594.80, 
   1604.72, 1614.18, 1621.92, 1635.26, 1649.20, 1660.64, 1672.84, 1685.46, 
   1698.14, 1706.42, 1712.84, 1743.99, 1831.00, 1904.48, 1968.19, 2035.82, 
   2101.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.51, 0.71, 0.75, 0.77, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          255.217        start      
        1            56         0.00580       0.29286     
        2            57       1.986e-005     2.567e-004   
        3            58       2.448e-009     9.523e-007   
        4            59       6.230e-011     7.395e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2439.81 psi
   
   Pressure: 
   1529.03, 1536.77, 1545.82, 1559.97, 1572.66, 1579.18, 1586.27, 1594.81, 
   1604.74, 1614.19, 1621.94, 1635.28, 1649.22, 1660.66, 1672.87, 1685.49, 
   1698.32, 1718.46, 1770.82, 1829.61, 1910.25, 1979.27, 2039.81, 2104.77, 
   2168.95
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.40, 0.68, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          248.391        start      
        1            56         0.00488       0.50880     
        2            57       1.789e-005     2.712e-004   
        3            58       5.547e-009     9.423e-007   
        4            59       1.073e-010     3.869e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2577.78 psi
   
   Pressure: 
   1528.90, 1536.60, 1545.61, 1559.70, 1572.33, 1578.82, 1585.88, 1594.38, 
   1604.26, 1613.68, 1621.39, 1634.67, 1648.56, 1659.95, 1672.16, 1695.89, 
   1803.56, 1874.25, 1924.38, 1979.65, 2056.44, 2122.70, 2181.14, 2244.26, 
   2307.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 
   0.67, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          228.030        start      
        1            56         0.00299       0.69493     
        2            57       1.008e-005     1.783e-004   
        3            58       1.145e-008     3.553e-007   
        4            59       8.679e-011     4.937e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2730.99 psi
   
   Pressure: 
   1528.78, 1536.45, 1545.42, 1559.45, 1572.02, 1578.49, 1585.52, 1593.98, 
   1603.83, 1613.20, 1620.88, 1634.12, 1647.97, 1664.51, 1761.83, 1869.67, 
   1973.93, 2040.30, 2088.04, 2141.24, 2215.53, 2279.89, 2336.85, 2398.62, 
   2460.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.64, 0.72, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          222.466        start      
        1            56         0.00399       1.17047     
        2            57       1.708e-006     1.981e-004   
        3            58       1.798e-009     2.511e-007   
        4            59       7.127e-011     1.788e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2866.38 psi
   
   Pressure: 
   1528.52, 1536.13, 1545.02, 1558.92, 1571.37, 1577.78, 1584.74, 1593.13, 
   1602.88, 1612.17, 1619.78, 1635.03, 1723.30, 1821.00, 1921.31, 2022.34, 
   2121.67, 2185.42, 2231.53, 2283.19, 2355.55, 2418.41, 2474.20, 2534.93, 
   2595.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.24, 0.58, 0.72, 0.74, 0.76, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          253.329        start      
        1            56         0.00688       0.50361     
        2            57       2.078e-005     3.603e-004   
        3            58       5.416e-009     1.351e-006   
        4            59       1.133e-010     2.775e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3002.35 psi
   
   Pressure: 
   1528.86, 1536.55, 1545.55, 1559.61, 1572.21, 1578.69, 1585.74, 1594.22, 
   1604.08, 1613.91, 1645.04, 1760.99, 1877.88, 1970.97, 2068.11, 2166.77, 
   2264.17, 2326.81, 2372.19, 2423.08, 2494.42, 2556.45, 2611.54, 2671.59, 
   2732.08
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          260.452        start      
        1            56         0.00551       0.44389     
        2            57       1.892e-005     2.892e-004   
        3            58       1.633e-009     1.083e-006   
        4            59       5.714e-011     5.593e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3095.69 psi
   
   Pressure: 
   1528.87, 1536.56, 1545.55, 1559.61, 1572.22, 1578.70, 1585.74, 1594.33, 
   1618.59, 1700.13, 1765.74, 1875.33, 1987.22, 2077.38, 2172.05, 2268.50, 
   2363.94, 2425.42, 2470.01, 2520.11, 2590.40, 2651.60, 2706.03, 2765.49, 
   2825.53
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.40, 0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          238.885        start      
        1            56         0.00408       0.49184     
        2            57       1.562e-005     1.436e-004   
        3            58       1.667e-008     4.935e-007   
        4            59       7.587e-011     6.749e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3196.83 psi
   
   Pressure: 
   1528.82, 1536.50, 1545.48, 1559.52, 1572.10, 1578.59, 1590.84, 1661.76, 
   1746.70, 1824.69, 1887.19, 1992.98, 2101.80, 2189.85, 2282.53, 2377.12, 
   2470.85, 2531.29, 2575.18, 2624.55, 2693.91, 2754.37, 2808.22, 2867.13, 
   2926.77
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 
   0.72, 0.75, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          218.973        start      
        1            56         0.00252       0.60634     
        2            57       5.595e-006     1.017e-004   
        3            58       5.273e-009     1.789e-007   
        4            59       6.879e-011     1.811e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3264.75 psi
   
   Pressure: 
   1528.73, 1536.38, 1545.33, 1559.33, 1575.58, 1622.23, 1682.91, 1753.64, 
   1834.17, 1909.40, 1970.18, 2073.55, 2180.20, 2266.67, 2357.83, 2450.96, 
   2543.34, 2602.96, 2646.30, 2695.09, 2763.70, 2823.57, 2876.95, 2935.44, 
   2994.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.27, 0.61, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          229.359        start      
        1            56         0.00567       1.34593     
        2            57       8.758e-006     4.153e-004   
        3            58       4.130e-009     1.215e-006   
        4            59       7.527e-011     2.043e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3390.71 psi
   
   Pressure: 
   1528.21, 1535.72, 1545.45, 1620.11, 1727.80, 1781.39, 1838.22, 1905.55, 
   1982.89, 2055.62, 2114.60, 2215.20, 2319.24, 2403.73, 2492.95, 2584.24, 
   2674.90, 2733.48, 2776.12, 2824.21, 2891.91, 2951.07, 3003.90, 3061.90, 
   3120.86
   
   Saturation:
   0.20, 0.20, 0.23, 0.55, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          256.358        start      
        1            56         0.00681       0.58883     
        2            57       2.107e-005     5.021e-004   
        3            58       1.467e-008     2.409e-006   
        4            59       7.828e-011     1.785e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3515.41 psi
   
   Pressure: 
   1529.84, 1559.74, 1637.84, 1756.76, 1860.61, 1913.01, 1969.17, 2035.99, 
   2112.89, 2185.32, 2244.07, 2344.30, 2447.94, 2532.10, 2620.95, 2711.82, 
   2802.05, 2860.34, 2902.75, 2950.58, 3017.90, 3076.74, 3129.28, 3186.99, 
   3245.69
   
   Saturation:
   0.21, 0.49, 0.70, 0.73, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          81.9451        start      
        1            56         0.00242       2.73376     
        2            57       2.483e-006      0.00159     
        3            58       1.369e-009     4.668e-007   
        4            59       4.313e-011     3.061e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3710.89 psi
   
   Pressure: 
   1742.11, 1806.19, 1878.89, 1990.68, 2089.48, 2139.69, 2193.73, 2258.21, 
   2332.56, 2402.72, 2459.73, 2557.11, 2657.96, 2739.97, 2826.68, 2915.48, 
   3003.79, 3060.92, 3102.55, 3149.58, 3215.91, 3273.97, 3325.91, 3383.08, 
   3441.38
   
   Saturation:
   0.67, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          11.0087        start      
        1            56         0.00103       2.58723     
        2            57       1.809e-006      0.00134     
        3            58       6.072e-010     7.341e-007   
        4            59       8.491e-012     4.575e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3730.40 psi
   
   Pressure: 
   1744.54, 1808.21, 1881.48, 1994.75, 2095.15, 2146.25, 2201.28, 2266.91, 
   2342.56, 2413.89, 2471.80, 2570.61, 2672.82, 2755.83, 2843.48, 2933.13, 
   3022.17, 3079.70, 3121.57, 3168.80, 3235.33, 3293.50, 3345.49, 3402.65, 
   3460.91
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.85742        start      
        1            56       6.685e-004      3.98352     
        2            57       1.234e-006      0.00192     
        3            58       7.028e-010     8.670e-007   
        4            59       6.873e-012     8.372e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3705.73 psi
   
   Pressure: 
   1736.28, 1798.43, 1870.32, 1981.73, 2080.67, 2131.10, 2185.46, 2250.36, 
   2325.22, 2395.84, 2453.21, 2551.15, 2652.49, 2734.84, 2821.83, 2910.84, 
   2999.29, 3056.45, 3098.09, 3145.07, 3211.28, 3269.21, 3321.02, 3378.04, 
   3436.21
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.41629        start      
        1            56       4.853e-004      4.76837     
        2            57       9.167e-007      0.00239     
        3            58       5.716e-010     1.100e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3688.03 psi
   
   Pressure: 
   1731.74, 1792.92, 1863.84, 1973.86, 2071.69, 2121.59, 2175.42, 2239.71, 
   2313.91, 2383.94, 2440.84, 2538.03, 2638.64, 2720.41, 2806.82, 2895.28, 
   2983.20, 3040.05, 3081.47, 3128.23, 3194.16, 3251.88, 3303.53, 3360.41, 
   3418.49
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.64893        start      
        1            56       4.989e-004      6.15776     
        2            57       1.148e-006      0.00376     
        3            58       8.410e-010     2.207e-006   
        4            59       7.396e-012     2.057e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3673.59 psi
   
   Pressure: 
   1728.58, 1789.04, 1859.18, 1968.09, 2064.99, 2114.44, 2167.81, 2231.58, 
   2305.20, 2374.71, 2431.21, 2527.73, 2627.68, 2708.94, 2794.84, 2882.80, 
   2970.25, 3026.81, 3068.03, 3114.59, 3180.27, 3237.79, 3289.28, 3346.04, 
   3404.04
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.18701        start      
        1            56       4.326e-004      6.72440     
        2            57       1.067e-006      0.00416     
        3            58       7.753e-010     2.695e-006   
        4            59       9.122e-012     2.308e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3661.12 psi
   
   Pressure: 
   1726.11, 1785.97, 1855.47, 1963.42, 2059.51, 2108.57, 2161.54, 2224.84, 
   2297.94, 2366.99, 2423.12, 2519.04, 2618.39, 2699.19, 2784.62, 2872.12, 
   2959.14, 3015.44, 3056.48, 3102.86, 3168.30, 3225.64, 3276.99, 3333.63, 
   3391.55
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.82331        start      
        1            56       4.072e-004      7.44063     
        2            57       1.112e-006      0.00476     
        3            58       8.359e-010     3.436e-006   
        4            59       8.036e-012     3.000e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3650.03 psi
   
   Pressure: 
   1724.04, 1783.39, 1852.33, 1959.44, 2054.82, 2103.52, 2156.13, 2219.02, 
   2291.65, 2360.26, 2416.06, 2511.43, 2610.24, 2690.61, 2775.61, 2862.69, 
   2949.31, 3005.37, 3046.25, 3092.45, 3157.68, 3214.85, 3266.08, 3322.60, 
   3380.45
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/31/2026 4:09:23 AM
   7/31/2026 4:10:20 AM
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
        0            1          369.679        start      
        1            56         0.00740       0.34720     
        2            57       1.445e-005     3.788e-004   
        3            58       4.019e-009     8.862e-007   
        4            59       1.045e-010     1.934e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2425.47 psi
   
   Pressure: 
   1538.79, 1549.14, 1561.25, 1580.17, 1597.13, 1605.85, 1615.34, 1626.75, 
   1640.03, 1652.67, 1663.02, 1680.86, 1699.50, 1714.80, 1731.12, 1747.99, 
   1764.93, 1776.00, 1784.16, 1793.50, 1806.86, 1819.18, 1859.82, 1965.05, 
   2064.23
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.48, 0.71, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          306.869        start      
        1            56         0.00285       0.45454     
        2            57       7.839e-006     1.171e-004   
        3            58       8.298e-009     1.761e-007   
        4            59       1.154e-010     2.057e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2630.37 psi
   
   Pressure: 
   1538.62, 1548.93, 1560.98, 1579.82, 1596.70, 1605.38, 1614.83, 1626.20, 
   1639.41, 1652.00, 1662.31, 1680.09, 1698.65, 1713.89, 1730.15, 1746.97, 
   1763.86, 1774.89, 1783.03, 1796.07, 1900.37, 2001.34, 2088.33, 2180.11, 
   2269.38
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.28, 0.63, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          340.485        start      
        1            56         0.00571       0.32316     
        2            57       1.617e-005     2.303e-004   
        3            58       2.224e-009     6.706e-007   
        4            59       1.203e-010     1.020e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2751.81 psi
   
   Pressure: 
   1538.70, 1549.02, 1561.09, 1579.96, 1596.87, 1605.56, 1615.02, 1626.40, 
   1639.64, 1652.24, 1662.57, 1680.35, 1698.94, 1714.19, 1730.46, 1747.29, 
   1764.37, 1790.78, 1860.71, 1939.05, 2046.45, 2138.37, 2218.99, 2305.50, 
   2390.98
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.40, 0.68, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          301.736        start      
        1            56         0.00466       1.12260     
        2            57       2.513e-006     2.320e-004   
        3            58       2.585e-009     2.847e-007   
        4            59       8.277e-011     1.589e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2996.78 psi
   
   Pressure: 
   1538.09, 1548.25, 1560.13, 1578.70, 1595.34, 1603.90, 1613.21, 1624.41, 
   1637.44, 1649.84, 1660.00, 1677.51, 1695.80, 1710.81, 1729.31, 1833.35, 
   1977.77, 2068.80, 2133.80, 2205.89, 2306.35, 2393.26, 2470.08, 2553.26, 
   2636.29
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 
   0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.79, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          327.571        start      
        1            56         0.00455       0.66447     
        2            57       1.283e-005     2.527e-004   
        3            58       2.661e-009     6.504e-007   
        4            59       1.346e-010     1.529e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3248.63 psi
   
   Pressure: 
   1538.31, 1548.53, 1560.48, 1579.15, 1595.89, 1604.50, 1613.86, 1625.13, 
   1638.24, 1650.72, 1660.94, 1678.63, 1713.39, 1841.92, 1979.52, 2117.01, 
   2251.54, 2337.68, 2399.85, 2469.37, 2566.59, 2650.95, 2725.71, 2806.97, 
   2888.50
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.35, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          341.266        start      
        1            56         0.00699       0.54483     
        2            57       1.836e-005     3.321e-004   
        3            58       4.844e-009     1.005e-006   
        4            59       9.880e-011     2.208e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3495.37 psi
   
   Pressure: 
   1538.40, 1548.64, 1560.61, 1579.33, 1596.10, 1604.72, 1614.10, 1625.39, 
   1638.52, 1651.49, 1690.31, 1844.71, 2000.31, 2124.14, 2253.29, 2384.42, 
   2513.86, 2597.09, 2657.38, 2725.01, 2819.79, 2902.20, 2975.40, 3055.19, 
   3135.59
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.48, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          293.169        start      
        1            56         0.00293       0.91932     
        2            57       2.887e-006     9.638e-005   
        3            58       3.144e-009     8.180e-008   
        4            59       1.169e-010     8.466e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3659.68 psi
   
   Pressure: 
   1537.98, 1548.11, 1559.95, 1578.46, 1595.05, 1603.58, 1612.86, 1626.83, 
   1716.66, 1823.76, 1908.58, 2051.13, 2197.21, 2315.18, 2439.23, 2565.77, 
   2691.11, 2771.91, 2830.59, 2896.57, 2989.25, 3070.02, 3141.94, 3220.58, 
   3300.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 
   0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          340.553        start      
        1            56         0.00517       0.39891     
        2            57       1.629e-005     1.674e-004   
        3            58       1.361e-009     5.735e-007   
        4            59       1.022e-010     6.129e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3814.59 psi
   
   Pressure: 
   1538.44, 1548.68, 1560.66, 1579.39, 1596.32, 1615.71, 1696.75, 1793.24, 
   1902.37, 2003.81, 2085.56, 2224.36, 2367.39, 2483.23, 2605.25, 2729.84, 
   2853.33, 2932.98, 2990.85, 3055.97, 3147.49, 3227.31, 3298.43, 3376.32, 
   3455.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.38, 0.68, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          315.270        start      
        1            56         0.00661       1.37201     
        2            57       1.140e-005     4.915e-004   
        3            58       2.347e-009     1.249e-006   
        4            59       1.032e-010     9.989e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3998.86 psi
   
   Pressure: 
   1537.37, 1547.33, 1559.87, 1648.56, 1791.56, 1862.72, 1938.14, 2027.45, 
   2130.03, 2226.50, 2304.73, 2438.16, 2576.15, 2688.23, 2806.59, 2927.68, 
   3047.95, 3125.67, 3182.25, 3246.05, 3335.89, 3414.41, 3484.52, 3561.51, 
   3639.79
   
   Saturation:
   0.20, 0.20, 0.22, 0.53, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          302.590        start      
        1            56         0.00283       0.97795     
        2            57       6.202e-006     3.635e-004   
        3            58       9.980e-009     1.268e-006   
        4            59       9.100e-011     1.987e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4209.59 psi
   
   Pressure: 
   1556.85, 1638.47, 1739.71, 1893.72, 2028.89, 2097.31, 2170.76, 2258.24, 
   2359.02, 2454.00, 2531.11, 2662.72, 2798.90, 2909.53, 3026.40, 3146.00, 
   3264.82, 3341.61, 3397.54, 3460.63, 3549.51, 3627.24, 3696.71, 3773.07, 
   3850.82
   
   Saturation:
   0.30, 0.65, 0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          24.2736        start      
        1            56         0.00112       0.92157     
        2            57       1.443e-006     8.445e-004   
        3            58       1.037e-010     6.597e-007   
        4            59       1.130e-011     4.145e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4477.02 psi
   
   Pressure: 
   1832.68, 1918.50, 2016.82, 2168.46, 2302.66, 2370.87, 2444.26, 2531.74, 
   2632.52, 2727.49, 2804.56, 2936.03, 3071.96, 3182.33, 3298.83, 3417.97, 
   3536.26, 3612.68, 3668.29, 3731.00, 3819.31, 3896.52, 3965.51, 4041.35, 
   4118.64
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.96671        start      
        1            56       7.428e-004      4.72698     
        2            57       1.174e-006      0.00173     
        3            58       5.825e-010     6.158e-007   
        4            59       9.842e-012     5.240e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4433.34 psi
   
   Pressure: 
   1815.26, 1898.15, 1993.96, 2142.37, 2274.13, 2341.26, 2413.61, 2499.95, 
   2599.52, 2693.43, 2769.69, 2899.87, 3034.54, 3143.94, 3259.48, 3377.70, 
   3495.12, 3571.01, 3626.27, 3688.63, 3776.48, 3853.35, 3922.08, 3997.72, 
   4074.89
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.45726        start      
        1            56       5.671e-004      6.38722     
        2            57       1.007e-006      0.00280     
        3            58       6.265e-010     1.037e-006   
        4            59       6.986e-012     9.240e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4402.68 psi
   
   Pressure: 
   1807.30, 1888.48, 1982.57, 2128.55, 2258.33, 2324.53, 2395.94, 2481.23, 
   2579.64, 2672.53, 2747.99, 2876.88, 3010.28, 3118.70, 3233.27, 3350.54, 
   3467.08, 3542.44, 3597.34, 3659.33, 3746.73, 3823.24, 3891.71, 3967.14, 
   4044.19
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.39093        start      
        1            56       5.283e-004      7.99698     
        2            57       1.120e-006      0.00414     
        3            58       7.831e-010     1.907e-006   
        4            59       7.455e-012     1.687e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4379.07 psi
   
   Pressure: 
   1802.23, 1882.21, 1975.04, 2119.16, 2247.39, 2312.85, 2383.49, 2467.91, 
   2565.36, 2657.39, 2732.18, 2859.96, 2992.28, 3099.86, 3213.58, 3330.03, 
   3445.81, 3520.70, 3575.28, 3636.95, 3723.94, 3800.14, 3868.38, 3943.61, 
   4020.55
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.72293        start      
        1            56       4.153e-004      8.46127     
        2            57       9.265e-007      0.00426     
        3            58       6.159e-010     2.122e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4359.26 psi
   
   Pressure: 
   1798.39, 1877.43, 1969.22, 2111.82, 2238.76, 2303.58, 2373.57, 2457.24, 
   2553.86, 2645.12, 2719.32, 2846.14, 2977.50, 3084.33, 3197.30, 3313.02, 
   3428.11, 3502.58, 3556.89, 3618.26, 3704.88, 3780.81, 3848.83, 3923.88, 
   4000.71
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.25216        start      
        1            56       3.797e-004      9.21977     
        2            57       9.327e-007      0.00472     
        3            58       6.376e-010     2.595e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4342.02 psi
   
   Pressure: 
   1795.24, 1873.49, 1964.40, 2105.69, 2231.51, 2295.78, 2365.20, 2448.20, 
   2544.08, 2634.67, 2708.35, 2834.29, 2964.79, 3070.96, 3183.25, 3298.31, 
   3412.78, 3486.88, 3540.92, 3602.04, 3688.33, 3763.99, 3831.82, 3906.71, 
   3983.44
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.93347        start      
        1            56       3.994e-004      10.3673     
        2            57       1.112e-006      0.00568     
        3            58       8.485e-010     3.472e-006   
        4            59       5.420e-012     3.147e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4326.68 psi
   
   Pressure: 
   1792.54, 1870.11, 1960.26, 2100.40, 2225.22, 2288.99, 2357.90, 2440.31, 
   2535.53, 2625.51, 2698.71, 2823.88, 2953.60, 3059.15, 3170.83, 3285.30, 
   3399.21, 3472.96, 3526.77, 3587.65, 3673.63, 3749.06, 3816.72, 3891.46, 
   3968.08
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.63972        start      
        1            56       4.245e-004      11.6270     
        2            57       1.322e-006      0.00674     
        3            58       1.152e-009     4.419e-006   
        4            59       7.653e-012     5.136e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4312.87 psi
   
   Pressure: 
   1790.17, 1867.13, 1956.60, 2095.71, 2219.64, 2282.98, 2351.42, 2433.30, 
   2527.91, 2617.34, 2690.11, 2814.56, 2943.57, 3048.57, 3159.69, 3273.61, 
   3387.01, 3460.45, 3514.05, 3574.70, 3660.41, 3735.62, 3803.11, 3877.71, 
   3954.25
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.41934        start      
        1            56       4.877e-004      13.2699     
        2            57       1.706e-006      0.00841     
        3            58       1.785e-009     5.725e-006   
        4            59       8.982e-012     9.433e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4300.30 psi
   
   Pressure: 
   1788.06, 1864.47, 1953.33, 2091.50, 2214.62, 2277.56, 2345.58, 2426.97, 
   2521.02, 2609.96, 2682.33, 2806.13, 2934.48, 3038.98, 3149.59, 3263.00, 
   3375.92, 3449.07, 3502.48, 3562.93, 3648.38, 3723.40, 3790.73, 3865.21, 
   3941.65
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.24303        start      
        1            56       3.904e-004      12.3207     
        2            57       1.238e-006      0.00673     
        3            58       1.181e-009     4.213e-006   
        4            59       7.927e-012     5.932e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4288.76 psi
   
   Pressure: 
   1786.14, 1862.06, 1950.35, 2087.68, 2210.06, 2272.62, 2340.26, 2421.20, 
   2514.75, 2603.22, 2675.23, 2798.42, 2926.18, 3030.20, 3140.34, 3253.29, 
   3365.78, 3438.66, 3491.88, 3552.15, 3637.35, 3712.19, 3779.38, 3853.74, 
   3930.11
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/31/2026 4:11:14 AM
   7/31/2026 4:12:17 AM
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
        0            1          414.833        start      
        1            56         0.00552       0.32999     
        2            57       1.641e-005     2.061e-004   
        3            58       5.700e-009     5.179e-007   
        4            59       1.487e-010     2.725e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2719.30 psi
   
   Pressure: 
   1548.49, 1561.42, 1576.56, 1600.21, 1621.41, 1632.31, 1644.16, 1658.43, 
   1675.03, 1690.83, 1703.77, 1726.08, 1749.38, 1768.50, 1788.91, 1810.01, 
   1831.20, 1845.05, 1855.25, 1866.94, 1883.70, 1909.64, 2022.72, 2148.77, 
   2268.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 0.74, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          422.842        start      
        1            56         0.00460       0.24156     
        2            57       1.291e-005     1.232e-004   
        3            58       7.326e-009     3.040e-007   
        4            59       1.529e-010     2.138e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2997.42 psi
   
   Pressure: 
   1548.46, 1561.38, 1576.50, 1600.13, 1621.32, 1632.21, 1644.05, 1658.31, 
   1674.90, 1690.69, 1703.62, 1725.91, 1749.20, 1768.31, 1788.71, 1809.80, 
   1830.98, 1844.87, 1862.99, 1962.44, 2103.77, 2223.14, 2327.12, 2437.92, 
   2546.65
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.34, 0.67, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          408.270        start      
        1            56         0.00585       0.78441     
        2            57       1.679e-005     2.875e-004   
        3            58       5.454e-009     7.168e-007   
        4            59       1.679e-010     2.564e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3286.57 psi
   
   Pressure: 
   1547.87, 1560.64, 1575.57, 1598.91, 1619.84, 1630.60, 1642.30, 1656.39, 
   1672.77, 1688.38, 1701.16, 1723.19, 1746.22, 1765.11, 1785.35, 1823.00, 
   2001.16, 2118.41, 2201.52, 2293.15, 2420.46, 2530.34, 2627.28, 2732.02, 
   2836.30
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 
   0.67, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          404.387        start      
        1            56         0.00547       0.86654     
        2            57       1.484e-005     2.868e-004   
        3            58       3.780e-009     6.803e-007   
        4            59       1.184e-010     1.899e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3676.83 psi
   
   Pressure: 
   1547.70, 1560.42, 1575.30, 1598.56, 1619.41, 1630.13, 1641.78, 1655.82, 
   1672.14, 1687.69, 1700.42, 1722.45, 1764.16, 1923.66, 2095.12, 2266.35, 
   2433.86, 2541.10, 2618.51, 2705.08, 2826.16, 2931.23, 3024.38, 3125.64, 
   3227.26
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          387.884        start      
        1            56         0.00441       0.68524     
        2            57       1.267e-005     1.688e-004   
        3            58       5.452e-009     3.904e-007   
        4            59       1.162e-010     2.211e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4034.04 psi
   
   Pressure: 
   1547.81, 1560.56, 1575.47, 1598.78, 1619.67, 1630.41, 1642.10, 1656.16, 
   1672.55, 1697.79, 1802.87, 1991.88, 2182.11, 2334.29, 2493.42, 2655.19, 
   2815.02, 2917.86, 2992.40, 3076.07, 3193.40, 3295.49, 3386.22, 3485.23, 
   3585.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.32, 0.65, 0.73, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          377.938        start      
        1            56         0.00397       0.86183     
        2            57       1.170e-005     1.368e-004   
        3            58       1.084e-008     2.728e-007   
        4            59       2.131e-010     3.189e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4296.95 psi
   
   Pressure: 
   1547.52, 1560.19, 1575.01, 1598.17, 1618.93, 1629.63, 1647.31, 1761.20, 
   1901.75, 2030.54, 2133.66, 2308.09, 2487.45, 2632.56, 2785.32, 2941.22, 
   3095.73, 3195.38, 3267.78, 3349.24, 3463.72, 3563.55, 3652.49, 3749.86, 
   3848.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.65, 
   0.72, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          359.395        start      
        1            56         0.00327       0.98541     
        2            57       7.560e-006     1.766e-004   
        3            58       6.183e-009     3.486e-007   
        4            59       1.618e-010     3.477e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1546.49, 1558.88, 1573.39, 1605.34, 1764.21, 1854.55, 1949.49, 2061.36, 
   2189.51, 2309.77, 2407.17, 2573.12, 2744.57, 2883.71, 3030.53, 3180.63, 
   3329.60, 3425.78, 3495.74, 3574.56, 3685.43, 3782.23, 3868.59, 3963.28, 
   4059.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.29, 0.63, 0.72, 0.74, 0.75, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          329.358        start      
        1            56         0.00468       0.81667     
        2            57       1.832e-006     2.138e-004   
        3            58       2.962e-009     4.825e-007   
        4            59       1.612e-010     6.835e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1549.68, 1624.16, 1738.60, 1912.27, 2064.35, 2141.22, 2223.68, 2321.84, 
   2434.86, 2541.34, 2627.76, 2775.22, 2927.73, 3051.60, 3182.39, 3316.17, 
   3449.02, 3534.84, 3597.29, 3667.69, 3766.81, 3853.43, 3930.79, 4015.74, 
   4102.18
   
   Saturation:
   0.24, 0.59, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          26.5513        start      
        1            56       9.187e-004      0.61633     
        2            57       1.099e-006     6.383e-004   
        3            58       7.398e-011     5.803e-007   
        4            59       1.571e-011     3.011e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1835.73, 1922.20, 2021.19, 2173.83, 2308.88, 2377.53, 2451.38, 2539.41, 
   2640.85, 2736.46, 2814.07, 2946.49, 3083.45, 3194.68, 3312.13, 3432.28, 
   3551.61, 3628.70, 3684.82, 3748.11, 3837.26, 3915.21, 3984.88, 4061.48, 
   4139.54
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.31940        start      
        1            56       6.243e-004      1.18746     
        2            57       9.268e-007      0.00103     
        3            58       2.742e-010     8.993e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1821.97, 1906.60, 2004.43, 2155.96, 2290.49, 2359.04, 2432.93, 2521.12, 
   2622.85, 2718.83, 2796.79, 2929.90, 3067.66, 3179.60, 3297.85, 3418.88, 
   3539.13, 3616.87, 3673.48, 3737.38, 3827.44, 3906.25, 3976.74, 4054.32, 
   4133.48
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.66151        start      
        1            56       8.508e-004      1.78737     
        2            57       1.807e-006      0.00244     
        3            58       7.369e-010     3.327e-006   
        4            59       7.712e-012     1.066e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1817.12, 1900.89, 1998.00, 2148.66, 2282.62, 2350.96, 2424.69, 2512.77, 
   2614.42, 2710.38, 2788.36, 2921.57, 3059.49, 3171.60, 3290.09, 3411.40, 
   3531.98, 3609.96, 3666.79, 3730.96, 3821.45, 3900.69, 3971.61, 4049.75, 
   4129.57
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.53583        start      
        1            56       5.964e-004      1.69340     
        2            57       1.107e-006      0.00203     
        3            58       3.385e-010     2.552e-006   
        4            59       6.812e-012     5.479e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1814.46, 1897.68, 1994.28, 2144.28, 2277.75, 2345.89, 2419.45, 2507.35, 
   2608.85, 2704.71, 2782.64, 2915.80, 3053.71, 3165.86, 3284.43, 3405.86, 
   3526.61, 3604.72, 3661.67, 3726.01, 3816.80, 3896.33, 3967.57, 4046.11, 
   4126.43
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.87415        start      
        1            56       5.528e-004      1.75612     
        2            57       1.012e-006      0.00209     
        3            58       2.760e-010     2.721e-006   
        4            59       9.846e-012     5.121e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1812.64, 1895.46, 1991.66, 2141.12, 2274.17, 2342.12, 2415.51, 2503.25, 
   2604.59, 2700.32, 2778.17, 2911.24, 3049.09, 3161.22, 3279.81, 3401.29, 
   3522.14, 3600.34, 3657.37, 3721.85, 3812.85, 3892.63, 3964.11, 4042.99, 
   4123.74
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.44305        start      
        1            56       6.010e-004      1.92334     
        2            57       1.166e-006      0.00245     
        3            58       3.235e-010     3.498e-006   
        4            59       6.009e-012     6.994e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1811.24, 1893.74, 1989.61, 2138.60, 2271.29, 2339.08, 2412.32, 2499.90, 
   2601.07, 2696.67, 2774.44, 2907.39, 3045.16, 3157.25, 3275.83, 3397.34, 
   3518.25, 3596.52, 3653.62, 3718.19, 3809.38, 3889.36, 3961.06, 4040.23, 
   4121.34
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.05204        start      
        1            56       6.632e-004      2.11531     
        2            57       1.362e-006      0.00288     
        3            58       3.974e-010     4.464e-006   
        4            59       6.049e-012     9.935e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1810.10, 1892.32, 1987.91, 2136.50, 2268.86, 2336.50, 2409.59, 2497.02, 
   2598.03, 2693.51, 2771.19, 2904.02, 3041.70, 3153.75, 3272.31, 3393.83, 
   3514.78, 3593.10, 3650.26, 3714.92, 3806.27, 3886.42, 3958.31, 4037.74, 
   4119.18
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.79015        start      
        1            56       8.077e-004      2.41023     
        2            57       1.840e-006      0.00367     
        3            58       6.219e-010     6.433e-006   
        4            59       5.017e-012     1.759e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1809.11, 1891.10, 1986.44, 2134.66, 2266.73, 2334.24, 2407.19, 2494.47, 
   2595.34, 2690.70, 2768.29, 2901.01, 3038.60, 3150.60, 3269.13, 3390.66, 
   3511.64, 3590.00, 3647.20, 3711.94, 3803.43, 3883.74, 3955.80, 4035.47, 
   4117.20
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.56093        start      
        1            56       6.465e-004      2.22024     
        2            57       1.303e-006      0.00294     
        3            58       3.860e-010     4.606e-006   
        4            59       9.274e-012     1.064e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1808.25, 1890.03, 1985.13, 2133.03, 2264.83, 2332.21, 2405.04, 2492.19, 
   2592.91, 2688.16, 2765.67, 2898.28, 3035.78, 3147.72, 3266.23, 3387.75, 
   3508.76, 3587.16, 3644.40, 3709.21, 3800.83, 3881.27, 3953.49, 4033.37, 
   4115.38
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.32876        start      
        1            56       7.879e-004      2.52800     
        2            57       1.751e-006      0.00373     
        3            58       6.195e-010     6.499e-006   
        4            59       8.450e-012     1.888e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1807.47, 1889.06, 1983.96, 2131.55, 2263.10, 2330.36, 2403.08, 2490.10, 
   2590.69, 2685.83, 2763.27, 2895.77, 3033.18, 3145.08, 3263.55, 3385.07, 
   3506.11, 3584.53, 3641.81, 3706.69, 3798.42, 3878.99, 3951.35, 4031.43, 
   4113.69
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.16317        start      
        1            56       6.564e-004      2.36166     
        2            57       1.319e-006      0.00311     
        3            58       4.260e-010     4.917e-006   
        4            59       7.410e-012     1.272e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1806.76, 1888.17, 1982.88, 2130.20, 2261.52, 2328.67, 2401.27, 2488.18, 
   2588.65, 2683.68, 2761.05, 2893.45, 3030.78, 3142.63, 3261.08, 3382.59, 
   3503.64, 3582.10, 3639.41, 3704.34, 3796.18, 3876.87, 3949.37, 4029.63, 
   4112.12
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.01733        start      
        1            56       5.534e-004      2.21544     
        2            57       1.013e-006      0.00262     
        3            58       3.018e-010     3.785e-006   
        4            59       8.195e-012     8.872e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1806.11, 1887.36, 1981.89, 2128.95, 2260.05, 2327.09, 2399.60, 2486.39, 
   2586.75, 2681.69, 2758.99, 2891.29, 3028.54, 3140.34, 3258.77, 3380.27, 
   3501.34, 3579.82, 3637.17, 3702.15, 3794.09, 3874.89, 3947.51, 4027.94, 
   4110.65
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   7/31/2026 4:12:56 AM
   7/31/2026 4:13:53 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

