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
   
   
   Time: 
   500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          192.251        start      
        1            56         23561.0       3320.45     
        2            57         11976.3       1648.92     
        3            58         214.106       1697.93     
        4            59         1.28640       26.7385     
        5            60       1.562e-004      0.15771     
        6            61       3.581e-007     2.028e-005   
        7            62       8.297e-008     5.968e-008   
   Producer BHP: 
   2254.72 psi
   
   Injector BHP: 
   2593.10 psi
   
   Pressure: 
   2289.66, 2297.61, 2305.43, 2312.08, 2321.02, 2329.53, 2336.34, 2343.74, 
   2350.74, 2356.63, 2364.89, 2373.91, 2383.66, 2393.14, 2399.91, 2406.81, 
   2411.52, 2415.21, 2420.42, 2427.37, 2433.22, 2438.31, 2444.15, 2458.16, 
   2497.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.69
   
   
   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
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
        0            1          166.896        start      
        1            56       1.087e-004      0.31029     
        2            57       7.723e-009     2.971e-006   
        3            58       1.321e-009     1.807e-010   
   Producer BHP: 
   1586.60 psi
   
   Injector BHP: 
   1981.85 psi
   
   Pressure: 
   1621.62, 1629.58, 1637.40, 1644.05, 1652.98, 1661.48, 1668.27, 1675.65, 
   1682.62, 1688.47, 1696.69, 1705.65, 1715.33, 1724.72, 1731.43, 1738.25, 
   1742.91, 1746.55, 1751.69, 1758.54, 1764.30, 1769.77, 1801.01, 1849.82, 
   1885.38
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.55, 0.72, 
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
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
              Rejected (Non-Convergence)
   ================================================
   ================================================
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
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.300        start      
        1            54         0.07791       0.15070     
        2            55         0.02737       0.05614     
        3            56       1.307e-005      0.01460     
        4            57       4.917e-009     6.966e-006   
        5            58       3.498e-009     4.412e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1899.90 psi
   
   Pressure: 
   1525.26, 1531.07, 1536.85, 1541.83, 1548.60, 1555.12, 1560.40, 1566.21, 
   1571.76, 1576.48, 1583.21, 1590.62, 1598.74, 1606.73, 1612.52, 1618.50, 
   1622.63, 1625.92, 1630.64, 1637.00, 1644.15, 1679.20, 1726.00, 1770.41, 
   1803.37
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 0.73, 0.76, 
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
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.977        start      
        1            54       6.312e-004      0.08557     
        2            55       1.014e-006     2.576e-005   
        3            56       1.696e-010     3.805e-008   
        4            57       9.367e-011     1.354e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1942.45 psi
   
   Pressure: 
   1525.26, 1531.07, 1536.85, 1541.83, 1548.60, 1555.11, 1560.40, 1566.20, 
   1571.75, 1576.48, 1583.20, 1590.61, 1598.73, 1606.72, 1612.51, 1618.49, 
   1622.62, 1625.91, 1630.64, 1642.14, 1687.58, 1728.19, 1772.11, 1814.26, 
   1845.91
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 0.76, 0.78, 
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
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.809        start      
        1            53         4865.91       1067.05     
        2            54         2438.14       535.612     
        3            55         16.3228       540.464     
        4            56         16.2483       0.03059     
        5            57         16.1614       0.04819     
        6            58         3.73183       11.0292     
        7            59         0.22392       2.19304     
        8            60         0.02875       0.14005     
        9            61         0.00599       0.01261     
        10           62         0.00105       0.00390     
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.809        start      
        1            56       6.074e-005      0.02280     
        2            57       1.541e-008     1.720e-006   
        3            58       5.554e-010     4.388e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1990.54 psi
   
   Pressure: 
   1525.25, 1531.05, 1536.82, 1541.80, 1548.56, 1555.08, 1560.35, 1566.16, 
   1571.70, 1576.42, 1583.14, 1590.55, 1598.65, 1606.64, 1612.42, 1618.39, 
   1622.52, 1625.88, 1641.18, 1696.33, 1741.96, 1780.36, 1822.49, 1863.21, 
   1894.01
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.46, 0.69, 0.74, 0.76, 0.78, 0.79, 
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
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.159        start      
        1            55         0.00117       0.11289     
        2            56       1.286e-006     3.919e-005   
        3            57       4.495e-010     6.793e-008   
        4            58       5.841e-011     2.140e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2020.79 psi
   
   Pressure: 
   1525.25, 1531.06, 1536.83, 1541.81, 1548.58, 1555.09, 1560.37, 1566.17, 
   1571.72, 1576.44, 1583.16, 1590.57, 1598.67, 1606.66, 1612.45, 1618.42, 
   1622.94, 1639.09, 1679.76, 1732.79, 1776.34, 1813.37, 1854.31, 1894.04, 
   1924.27
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.53, 0.72, 0.74, 0.76, 0.77, 0.79, 0.80, 
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
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.460        start      
        1            56       2.088e-004      0.09155     
        2            57       8.821e-008     5.400e-006   
        3            58       1.619e-010     2.002e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2049.57 psi
   
   Pressure: 
   1525.21, 1531.01, 1536.77, 1541.73, 1548.49, 1554.99, 1560.26, 1566.05, 
   1571.59, 1576.30, 1583.01, 1590.40, 1598.50, 1606.47, 1612.25, 1619.77, 
   1648.09, 1676.39, 1715.19, 1766.04, 1808.20, 1844.25, 1884.28, 1923.27, 
   1953.05
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 
   0.60, 0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
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
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.500        start      
        1            56         223.438       140.104     
        2            57         111.718       70.0537     
        3            58         0.00759       70.0478     
        4            59       3.497e-007      0.00294     
        5            60       2.338e-007     8.563e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2094.94 psi
   
   Pressure: 
   1525.22, 1531.01, 1536.78, 1541.74, 1548.50, 1555.00, 1560.27, 1566.07, 
   1571.60, 1576.32, 1583.02, 1590.42, 1598.52, 1606.51, 1616.04, 1665.32, 
   1700.71, 1727.78, 1765.31, 1814.77, 1855.95, 1891.26, 1930.61, 1969.00, 
   1998.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.66, 
   0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
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
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.897        start      
        1            56         0.00104       0.16700     
        2            57       1.860e-006     5.716e-005   
        3            58       1.608e-010     1.033e-007   
        4            59       7.100e-011     1.266e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2139.06 psi
   
   Pressure: 
   1525.20, 1530.98, 1536.74, 1541.70, 1548.45, 1554.95, 1560.21, 1566.00, 
   1571.53, 1576.24, 1582.93, 1590.32, 1598.47, 1616.38, 1665.88, 1716.50, 
   1750.39, 1776.60, 1813.22, 1861.61, 1902.01, 1936.74, 1975.51, 2013.42, 
   2042.57
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
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
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.890        start      
        1            56         969.521       225.653     
        2            57         471.149       110.175     
        3            58         19.4931       105.416     
        4            59         18.9454       0.28357     
        5            60         0.26914       9.95082     
        6            61         0.00349       0.13754     
        7            62       3.011e-004      0.00197     
        8            63       2.892e-005     1.407e-004   
        9            64       8.724e-006     1.967e-005   
        10           65       1.033e-006     4.000e-006   
   ================================================
              Rejected (Non-Convergence)
   ================================================
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.889        start      
        1            55         324.286       243.221     
        2            56         162.143       121.611     
        3            57         0.00112       121.609     
        4            58       2.969e-006     8.355e-004   
        5            59       6.778e-007     2.736e-006   
        6            60       2.921e-007     2.891e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2198.28 psi
   
   Pressure: 
   1525.14, 1530.92, 1536.67, 1541.62, 1548.35, 1554.83, 1560.08, 1565.86, 
   1571.38, 1576.07, 1582.76, 1590.31, 1614.72, 1683.93, 1732.64, 1781.30, 
   1814.19, 1839.78, 1875.66, 1923.18, 1962.92, 1997.14, 2035.41, 2072.91, 
   2101.82
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.21, 0.44, 0.69, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
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
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.821        start      
        1            56         1.54739       0.93470     
        2            57         1.53598       0.00641     
        3            58         1.49381       0.02371     
        4            59         0.34887       0.64398     
        5            60         0.00119       0.19689     
        6            61       2.063e-005     6.856e-004   
        7            62       5.888e-007     1.127e-005   
        8            63       2.971e-007     4.983e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2263.53 psi
   
   Pressure: 
   1525.01, 1530.76, 1536.48, 1541.40, 1548.10, 1554.54, 1559.77, 1565.51, 
   1571.00, 1575.67, 1582.91, 1620.52, 1690.67, 1757.31, 1804.14, 1851.42, 
   1883.53, 1908.59, 1943.82, 1990.55, 2029.69, 2063.45, 2101.28, 2138.40, 
   2167.09
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.54, 0.70, 0.74, 0.75, 0.76, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
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
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.609        start      
        1            55       1.037e-004      0.11246     
        2            56       5.404e-008     3.168e-006   
        3            57       2.617e-010     1.079e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2324.74 psi
   
   Pressure: 
   1525.08, 1530.84, 1536.57, 1541.51, 1548.22, 1554.69, 1559.92, 1565.68, 
   1571.19, 1577.47, 1627.69, 1691.36, 1758.98, 1823.65, 1869.50, 1915.97, 
   1947.61, 1972.33, 2007.14, 2053.33, 2092.05, 2125.48, 2162.97, 2199.82, 
   2228.32
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
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
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.366        start      
        1            54         105.212       64.6806     
        2            55         52.6026       32.3410     
        3            56         0.00378       32.3373     
        4            57       1.047e-007      0.00180     
        5            58       1.566e-009     4.864e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2361.56 psi
   
   Pressure: 
   1525.21, 1531.00, 1536.76, 1541.72, 1548.47, 1554.96, 1560.23, 1566.04, 
   1576.33, 1615.85, 1673.24, 1734.71, 1800.59, 1864.12, 1909.34, 1955.28, 
   1986.58, 2011.06, 2045.56, 2091.34, 2129.75, 2162.92, 2200.16, 2236.78, 
   2265.15
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.67, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 
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
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.112        start      
        1            56         14.8522       9.29123     
        2            57         7.42628       4.64576     
        3            58       3.500e-005      4.64591     
        4            59       4.089e-009     1.362e-005   
        5            60       9.508e-010     2.068e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2400.76 psi
   
   Pressure: 
   1525.13, 1530.90, 1536.63, 1541.58, 1548.30, 1554.77, 1560.15, 1578.23, 
   1625.99, 1665.88, 1721.08, 1780.83, 1845.22, 1907.58, 1952.08, 1997.36, 
   2028.25, 2052.44, 2086.54, 2131.85, 2169.90, 2202.78, 2239.74, 2276.13, 
   2304.36
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.45, 
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
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
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.814        start      
        1            56         0.00182       0.38693     
        2            57       1.508e-006     7.082e-005   
        3            58       1.100e-009     1.353e-007   
        4            59       9.995e-011     6.611e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2446.81 psi
   
   Pressure: 
   1525.04, 1530.79, 1536.51, 1541.43, 1548.14, 1555.30, 1585.42, 1635.43, 
   1681.98, 1720.49, 1774.29, 1832.82, 1896.06, 1957.46, 2001.34, 2046.04, 
   2076.56, 2100.48, 2134.23, 2179.11, 2216.82, 2249.44, 2286.15, 2322.32, 
   2350.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 0.56, 0.71, 
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
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
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.555        start      
        1            56         1217.92       578.866     
        2            57         870.622       165.163     
        3            58         0.13370       413.576     
        4            59         0.45168       0.34058     
        5            60         0.17433       0.13175     
        6            61       1.677e-004      0.08292     
        7            62       1.184e-006     8.025e-005   
        8            63       6.359e-007     8.650e-007   
        9            64       2.712e-007     1.732e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2490.65 psi
   
   Pressure: 
   1525.00, 1530.74, 1536.45, 1541.37, 1549.89, 1595.22, 1640.21, 1688.29, 
   1733.38, 1771.04, 1823.90, 1881.53, 1943.89, 2004.53, 2047.91, 2092.13, 
   2122.34, 2146.03, 2179.49, 2223.99, 2261.41, 2293.81, 2330.30, 2366.28, 
   2394.28
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
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
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          154.627        start      
        1            56       2.900e-004      0.15470     
        2            57       3.556e-007     1.015e-005   
        3            58       2.046e-010     1.117e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2542.13 psi
   
   Pressure: 
   1525.08, 1530.84, 1536.58, 1544.62, 1599.50, 1655.22, 1698.76, 1745.75, 
   1790.06, 1827.24, 1879.51, 1936.57, 1998.36, 2058.47, 2101.50, 2145.39, 
   2175.38, 2198.90, 2232.14, 2276.35, 2313.54, 2345.76, 2382.06, 2417.89, 
   2445.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.32, 0.65, 0.72, 0.75, 0.75, 
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
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.427        start      
        1            51       8.932e-004      0.14244     
        2            52       1.641e-006     4.277e-005   
        3            53       8.582e-010     1.078e-007   
        4            54       8.142e-011     4.201e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2578.23 psi
   
   Pressure: 
   1525.18, 1531.01, 1543.17, 1585.56, 1643.20, 1697.13, 1739.85, 1786.20, 
   1830.03, 1866.87, 1918.73, 1975.38, 2036.75, 2096.48, 2139.25, 2182.88, 
   2212.70, 2236.10, 2269.16, 2313.15, 2350.16, 2382.23, 2418.38, 2454.07, 
   2481.89
   
   Saturation:
   0.20, 0.20, 0.37, 0.68, 0.73, 0.75, 0.76, 0.76, 
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
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          167.442        start      
        1            55         1062.27       472.447     
        2            56         533.915       235.198     
        3            57         0.19882       237.230     
        4            58         0.00151       0.02874     
        5            59       2.569e-005     5.976e-004   
        6            60       2.569e-008     1.029e-005   
        7            61       2.809e-008     1.134e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2615.46 psi
   
   Pressure: 
   1525.57, 1542.32, 1591.84, 1633.74, 1689.34, 1741.91, 1783.85, 1829.47, 
   1872.68, 1909.06, 1960.32, 2016.35, 2077.08, 2136.24, 2178.62, 2221.87, 
   2251.45, 2274.67, 2307.50, 2351.19, 2387.98, 2419.88, 2455.86, 2491.41, 
   2519.14
   
   Saturation:
   0.21, 0.44, 0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
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
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.950        start      
        1            56       4.298e-004      0.54359     
        2            57       1.660e-007     5.154e-005   
        3            58       2.290e-010     4.411e-008   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2695.95 psi
   
   Pressure: 
   1604.06, 1652.18, 1699.01, 1738.23, 1790.84, 1840.92, 1881.10, 1924.94, 
   1966.57, 2001.72, 2051.34, 2105.71, 2164.75, 2222.39, 2263.77, 2306.08, 
   2335.07, 2357.87, 2390.16, 2433.23, 2469.54, 2501.09, 2536.75, 2572.06, 
   2599.66
   
   Saturation:
   0.51, 0.70, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   8/25/2026 1:02:02 PM
   8/25/2026 1:02:55 PM
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
   ⚠️ Runtime Error: time step is too small

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

