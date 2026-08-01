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
        0            1          197.316        start      
        1            56         0.00259       5.69928     
        2            57       1.978e-006     6.477e-004   
        3            58       4.470e-009     9.162e-008   
        4            59       6.724e-011     2.462e-010   
   Producer BHP: 
   2261.93 psi
   
   Injector BHP: 
   2641.71 psi
   
   Pressure: 
   2297.58, 2305.22, 2314.80, 2324.72, 2332.80, 2339.44, 2345.40, 2351.51, 
   2357.78, 2364.18, 2370.29, 2378.33, 2386.44, 2392.21, 2397.85, 2406.48, 
   2414.65, 2419.27, 2423.96, 2430.29, 2442.39, 2453.21, 2458.54, 2470.66, 
   2509.92
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.70
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          185.509        start      
        1            56         0.00403       7.62324     
        2            57       3.905e-006     9.450e-004   
        3            58       1.313e-008     1.960e-007   
        4            59       4.827e-011     9.888e-010   
   Producer BHP: 
   1602.99 psi
   
   Injector BHP: 
   2023.87 psi
   
   Pressure: 
   1638.72, 1646.37, 1655.96, 1665.88, 1673.95, 1680.58, 1686.53, 1692.62, 
   1698.86, 1705.23, 1711.31, 1719.29, 1727.34, 1733.07, 1738.66, 1747.19, 
   1755.28, 1759.84, 1764.47, 1770.71, 1782.62, 1793.75, 1814.21, 1856.00, 
   1891.64
   
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
        0            1          160.093        start      
        1            56         0.00370       0.60449     
        2            57       2.988e-006     3.191e-004   
        3            58       4.082e-009     5.822e-007   
        4            59       6.341e-011     3.655e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1950.93 psi
   
   Pressure: 
   1525.68, 1531.24, 1538.30, 1545.70, 1551.78, 1556.84, 1561.44, 1566.21, 
   1571.17, 1576.29, 1581.24, 1587.83, 1594.56, 1599.42, 1604.22, 1611.66, 
   1618.81, 1622.90, 1627.11, 1632.88, 1645.42, 1704.20, 1747.46, 1785.55, 
   1818.63
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.56, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.644        start      
        1            56         0.00372       0.63279     
        2            57       1.826e-005     2.772e-004   
        3            58       3.933e-008     6.643e-007   
        4            59       9.629e-011     1.392e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2048.42 psi
   
   Pressure: 
   1525.68, 1531.24, 1538.30, 1545.69, 1551.78, 1556.84, 1561.44, 1566.21, 
   1571.17, 1576.29, 1581.24, 1587.83, 1594.56, 1599.42, 1604.22, 1611.66, 
   1618.81, 1622.91, 1627.13, 1635.34, 1720.62, 1807.67, 1848.27, 1884.37, 
   1916.16
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.29, 0.63, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          176.113        start      
        1            56         0.00691       0.16436     
        2            57       3.338e-005     2.528e-004   
        3            58       2.926e-008     1.578e-006   
        4            59       4.507e-011     9.940e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2098.18 psi
   
   Pressure: 
   1525.80, 1531.38, 1538.47, 1545.89, 1552.00, 1557.09, 1561.71, 1566.50, 
   1571.47, 1576.62, 1581.59, 1588.21, 1594.96, 1599.84, 1604.66, 1612.13, 
   1619.31, 1623.46, 1633.43, 1683.42, 1778.69, 1861.13, 1900.02, 1934.94, 
   1965.94
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.39, 0.69, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.140        start      
        1            56         0.00748       0.26349     
        2            57       2.910e-005     3.740e-004   
        3            58       3.197e-009     1.938e-006   
        4            59       3.904e-011     1.713e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2123.65 psi
   
   Pressure: 
   1525.77, 1531.35, 1538.43, 1545.84, 1551.94, 1557.02, 1561.63, 1566.42, 
   1571.38, 1576.52, 1581.49, 1588.10, 1594.84, 1599.71, 1604.53, 1611.99, 
   1619.44, 1634.42, 1671.05, 1719.41, 1809.97, 1889.24, 1926.92, 1960.97, 
   1991.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.48, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          156.433        start      
        1            56         0.00582       0.70893     
        2            57       9.113e-006     3.763e-004   
        3            58       1.017e-008     1.250e-006   
        4            59       4.195e-011     7.354e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2165.28 psi
   
   Pressure: 
   1525.63, 1531.18, 1538.22, 1545.59, 1551.66, 1556.71, 1561.30, 1566.05, 
   1571.00, 1576.11, 1581.04, 1587.62, 1594.33, 1599.17, 1603.96, 1612.24, 
   1651.98, 1687.37, 1722.40, 1768.70, 1856.03, 1932.95, 1969.70, 2003.07, 
   2033.06
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 
   0.56, 0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.690        start      
        1            56         0.00288       0.58250     
        2            57       1.030e-005     2.038e-004   
        3            58       1.262e-008     4.300e-007   
        4            59       5.446e-011     3.101e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2226.89 psi
   
   Pressure: 
   1525.65, 1531.20, 1538.24, 1545.62, 1551.69, 1556.75, 1561.34, 1566.10, 
   1571.04, 1576.16, 1581.10, 1587.68, 1594.40, 1599.25, 1605.52, 1661.11, 
   1722.77, 1756.68, 1790.49, 1835.55, 1920.80, 1996.11, 2032.19, 2065.05, 
   2094.70
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.62, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.79, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.010        start      
        1            56         0.00432       0.28683     
        2            57       2.192e-005     1.649e-004   
        3            58       3.433e-008     7.139e-007   
        4            59       4.576e-011     1.649e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2262.79 psi
   
   Pressure: 
   1525.74, 1531.31, 1538.38, 1545.78, 1551.88, 1556.95, 1561.55, 1566.33, 
   1571.29, 1576.42, 1581.38, 1587.98, 1594.75, 1603.05, 1642.16, 1705.96, 
   1765.11, 1798.01, 1831.07, 1875.27, 1959.04, 2033.17, 2068.75, 2101.23, 
   2130.61
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.65, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.387        start      
        1            56         0.00750       0.42180     
        2            57       3.597e-005     4.911e-004   
        3            58       1.199e-008     2.757e-006   
        4            59       4.916e-011     7.001e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2299.19 psi
   
   Pressure: 
   1525.70, 1531.25, 1538.31, 1545.70, 1551.78, 1556.84, 1561.44, 1566.21, 
   1571.16, 1576.28, 1581.23, 1587.91, 1605.92, 1647.77, 1688.70, 1749.89, 
   1807.24, 1839.36, 1871.75, 1915.17, 1997.57, 2070.61, 2105.73, 2137.87, 
   2167.03
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.42, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.053        start      
        1            56         0.00769       0.66570     
        2            57       2.740e-005     5.715e-004   
        3            58       6.599e-009     2.828e-006   
        4            59       3.747e-011     3.942e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2350.85 psi
   
   Pressure: 
   1525.61, 1531.14, 1538.17, 1545.53, 1551.59, 1556.63, 1561.21, 1565.96, 
   1570.90, 1576.00, 1581.22, 1609.90, 1668.31, 1709.19, 1748.49, 1807.93, 
   1863.94, 1895.42, 1927.24, 1969.96, 2051.15, 2123.22, 2157.94, 2189.76, 
   2218.71
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          147.050        start      
        1            56         0.00387       0.56893     
        2            57       3.915e-006     7.325e-005   
        3            58       7.781e-009     9.092e-008   
        4            59       3.857e-011     2.709e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2397.24 psi
   
   Pressure: 
   1525.63, 1531.17, 1538.20, 1545.57, 1551.63, 1556.68, 1561.26, 1566.02, 
   1570.96, 1577.05, 1609.44, 1666.48, 1722.73, 1762.32, 1800.68, 1859.03, 
   1914.14, 1945.17, 1976.57, 2018.78, 2099.04, 2170.37, 2204.76, 2236.34, 
   2265.12
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.25, 0.59, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.840        start      
        1            56         0.00356       0.44727     
        2            57       1.748e-005     1.707e-004   
        3            58       3.887e-008     4.758e-007   
        4            59       6.922e-011     1.434e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2434.13 psi
   
   Pressure: 
   1525.67, 1531.21, 1538.26, 1545.63, 1551.71, 1556.76, 1561.35, 1566.12, 
   1573.62, 1613.72, 1656.26, 1711.12, 1765.82, 1804.59, 1842.31, 1899.82, 
   1954.22, 1984.88, 2015.94, 2057.72, 2137.21, 2207.90, 2242.02, 2273.39, 
   2302.03
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.30, 0.64, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.481        start      
        1            56         0.00507       0.26892     
        2            57       2.429e-005     1.880e-004   
        3            58       1.063e-008     1.123e-006   
        4            59       3.220e-011     2.880e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2469.48 psi
   
   Pressure: 
   1525.74, 1531.30, 1538.36, 1545.76, 1551.84, 1556.91, 1561.55, 1571.47, 
   1613.82, 1657.73, 1698.81, 1752.34, 1806.07, 1844.28, 1881.52, 1938.39, 
   1992.23, 2022.59, 2053.38, 2094.80, 2173.64, 2243.80, 2277.69, 2308.88, 
   2337.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.37, 
   0.68, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.025        start      
        1            56         0.00692       0.34476     
        2            57       3.348e-005     3.377e-004   
        3            58       1.648e-008     2.048e-006   
        4            59       5.406e-011     8.728e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2499.61 psi
   
   Pressure: 
   1525.70, 1531.26, 1538.31, 1545.69, 1551.77, 1556.93, 1570.21, 1611.38, 
   1653.36, 1695.68, 1735.76, 1788.27, 1841.14, 1878.80, 1915.57, 1971.77, 
   2025.03, 2055.08, 2085.57, 2126.62, 2204.82, 2274.46, 2308.12, 2339.15, 
   2367.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.43, 0.69, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.825        start      
        1            56         0.00700       0.53057     
        2            57       2.598e-005     4.356e-004   
        3            58       2.226e-009     2.292e-006   
        4            59       3.534e-011     1.361e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2532.06 psi
   
   Pressure: 
   1525.62, 1531.15, 1538.18, 1545.54, 1551.93, 1573.22, 1613.05, 1653.31, 
   1693.92, 1735.21, 1774.52, 1826.17, 1878.27, 1915.45, 1951.76, 2007.33, 
   2060.02, 2089.78, 2119.99, 2160.69, 2238.26, 2307.40, 2340.85, 2371.72, 
   2400.01
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.21, 0.50, 0.70, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.252        start      
        1            56         0.00476       0.81233     
        2            57       5.438e-006     2.202e-004   
        3            58       7.385e-009     7.537e-007   
        4            59       4.923e-011     4.785e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2572.72 psi
   
   Pressure: 
   1525.48, 1530.99, 1537.98, 1546.35, 1581.64, 1625.25, 1663.65, 1702.55, 
   1742.19, 1782.67, 1821.31, 1872.18, 1923.56, 1960.25, 1996.12, 2051.06, 
   2103.19, 2132.65, 2162.58, 2202.94, 2279.89, 2348.54, 2381.79, 2412.50, 
   2440.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.24, 0.57, 0.71, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.292        start      
        1            56         0.00285       0.89328     
        2            57       1.127e-005     2.520e-004   
        3            58       2.528e-008     4.224e-007   
        4            59       5.251e-011     1.045e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2628.96 psi
   
   Pressure: 
   1525.44, 1530.94, 1540.40, 1596.08, 1648.24, 1690.26, 1727.57, 1765.66, 
   1804.66, 1844.55, 1882.70, 1932.97, 1983.77, 2020.08, 2055.59, 2110.01, 
   2161.67, 2190.88, 2220.57, 2260.62, 2337.03, 2405.24, 2438.30, 2468.86, 
   2496.95
   
   Saturation:
   0.20, 0.20, 0.28, 0.63, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          157.866        start      
        1            56         0.00428       0.59595     
        2            57       2.131e-005     1.986e-004   
        3            58       5.712e-008     1.151e-006   
        4            59       4.315e-011     3.785e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2682.29 psi
   
   Pressure: 
   1525.66, 1535.31, 1593.62, 1656.75, 1707.28, 1748.45, 1785.25, 1822.94, 
   1861.63, 1901.24, 1939.15, 1989.10, 2039.61, 2075.71, 2111.03, 2165.15, 
   2216.54, 2245.59, 2275.13, 2314.98, 2391.03, 2458.93, 2491.85, 2522.30, 
   2550.31
   
   Saturation:
   0.20, 0.33, 0.66, 0.73, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.805        start      
        1            56         0.00583       1.56713     
        2            57       2.091e-005      0.00153     
        3            58       1.747e-008     6.509e-006   
        4            59       3.356e-011     5.174e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2738.72 psi
   
   Pressure: 
   1561.20, 1607.86, 1666.85, 1726.93, 1775.57, 1815.50, 1851.34, 1888.16, 
   1926.04, 1964.89, 2002.11, 2051.23, 2100.94, 2136.51, 2171.34, 2224.77, 
   2275.55, 2304.28, 2333.53, 2373.02, 2448.44, 2515.84, 2548.56, 2578.86, 
   2606.78
   
   Saturation:
   0.40, 0.68, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   8/1/2026 8:40:02 AM
   8/1/2026 8:41:19 AM
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
        0            1          242.985        start      
        1            56         0.00287       0.22661     
        2            57       9.770e-006     1.846e-004   
        3            58       7.870e-009     3.902e-007   
        4            59       5.483e-011     1.920e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2052.06 psi
   
   Pressure: 
   1538.72, 1547.11, 1557.76, 1568.90, 1578.08, 1585.72, 1592.65, 1599.85, 
   1607.32, 1615.05, 1622.52, 1632.45, 1642.60, 1649.93, 1657.17, 1668.39, 
   1679.17, 1685.34, 1691.70, 1700.40, 1717.22, 1732.51, 1742.54, 1798.11, 
   1853.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.27, 0.63, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          242.419        start      
        1            56         0.00565       0.87203     
        2            57       5.074e-006     4.490e-004   
        3            58       5.959e-009     7.176e-007   
        4            59       8.769e-011     4.608e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2173.90 psi
   
   Pressure: 
   1538.46, 1546.79, 1557.37, 1568.44, 1577.55, 1585.13, 1592.02, 1599.16, 
   1606.59, 1614.26, 1621.67, 1631.54, 1641.62, 1648.89, 1656.08, 1667.22, 
   1677.92, 1684.04, 1690.36, 1699.00, 1717.62, 1804.52, 1869.35, 1926.22, 
   1975.54
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.56, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          239.497        start      
        1            56         0.00819       0.58940     
        2            57       1.833e-005     4.427e-004   
        3            58       1.015e-008     1.562e-006   
        4            59       8.539e-011     5.811e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2364.79 psi
   
   Pressure: 
   1538.54, 1546.89, 1557.48, 1568.57, 1577.70, 1585.30, 1592.20, 1599.36, 
   1606.79, 1614.48, 1621.91, 1631.80, 1641.89, 1649.18, 1656.38, 1667.54, 
   1678.26, 1684.39, 1691.18, 1734.98, 1881.18, 2007.54, 2066.78, 2119.72, 
   2166.55
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.54, 0.70, 0.74, 0.77, 0.79, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          263.760        start      
        1            56         0.00693       0.28735     
        2            57       2.089e-005     3.012e-004   
        3            58       3.363e-009     1.112e-006   
        4            59       7.657e-011     1.394e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2433.88 psi
   
   Pressure: 
   1538.65, 1547.01, 1557.63, 1568.75, 1577.91, 1585.53, 1592.44, 1599.62, 
   1607.07, 1614.78, 1622.23, 1632.14, 1642.26, 1649.56, 1656.78, 1667.97, 
   1679.08, 1701.03, 1756.15, 1828.62, 1964.24, 2082.88, 2139.25, 2190.17, 
   2235.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.47, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          257.097        start      
        1            56         0.00619       0.45522     
        2            57       2.202e-005     3.551e-004   
        3            58       1.853e-009     1.333e-006   
        4            59       8.387e-011     7.591e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2537.56 psi
   
   Pressure: 
   1538.51, 1546.85, 1557.43, 1568.51, 1577.63, 1585.22, 1592.12, 1599.27, 
   1606.69, 1614.37, 1621.79, 1631.67, 1641.76, 1649.03, 1656.30, 1683.12, 
   1775.80, 1827.86, 1879.39, 1947.79, 2076.96, 2190.86, 2245.32, 2294.85, 
   2339.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.40, 
   0.68, 0.73, 0.75, 0.77, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          233.822        start      
        1            56         0.00367       0.34940     
        2            57       1.380e-005     1.325e-004   
        3            58       1.396e-008     3.866e-007   
        4            59       6.907e-011     5.372e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2641.23 psi
   
   Pressure: 
   1538.54, 1546.88, 1557.47, 1568.56, 1577.68, 1585.28, 1592.18, 1599.33, 
   1606.77, 1614.45, 1621.88, 1631.77, 1641.89, 1653.89, 1712.45, 1808.05, 
   1896.58, 1945.81, 1995.27, 2061.38, 2186.64, 2297.49, 2350.68, 2399.24, 
   2443.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.65, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          220.528        start      
        1            56         0.00277       0.74320     
        2            57       8.239e-006     1.403e-004   
        3            58       7.963e-009     2.518e-007   
        4            59       8.176e-011     2.749e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2738.29 psi
   
   Pressure: 
   1538.24, 1546.51, 1557.01, 1568.01, 1577.07, 1584.60, 1591.45, 1598.55, 
   1605.92, 1613.55, 1620.93, 1634.17, 1710.58, 1773.07, 1832.79, 1922.64, 
   2007.12, 2054.52, 2102.41, 2166.67, 2288.70, 2396.99, 2449.12, 2496.90, 
   2540.32
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 0.74, 0.76, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          222.908        start      
        1            56         0.00408       0.65926     
        2            57       1.486e-006     1.013e-004   
        3            58       2.188e-009     1.503e-007   
        4            59       5.899e-011     1.480e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2839.50 psi
   
   Pressure: 
   1538.29, 1546.57, 1557.09, 1568.10, 1577.16, 1584.71, 1591.56, 1598.66, 
   1606.05, 1614.91, 1661.79, 1747.17, 1831.27, 1890.42, 1947.74, 2034.90, 
   2117.23, 2163.57, 2210.49, 2273.55, 2393.44, 2500.00, 2551.39, 2598.58, 
   2641.61
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          240.748        start      
        1            56         0.00694       0.62526     
        2            57       1.713e-005     3.460e-004   
        3            58       5.722e-009     1.229e-006   
        4            59       7.391e-011     2.905e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2918.80 psi
   
   Pressure: 
   1538.29, 1546.57, 1557.09, 1568.10, 1577.16, 1584.70, 1591.55, 1599.09, 
   1633.96, 1700.57, 1762.83, 1843.54, 1924.33, 1981.71, 2037.60, 2122.89, 
   2203.63, 2249.15, 2295.30, 2357.40, 2475.57, 2580.73, 2631.52, 2678.25, 
   2720.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 
   0.52, 0.70, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          255.359        start      
        1            56         0.00603       0.37299     
        2            57       2.237e-005     2.482e-004   
        3            58       3.186e-009     1.051e-006   
        4            59       1.019e-010     1.090e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2993.63 psi
   
   Pressure: 
   1538.46, 1546.78, 1557.34, 1568.39, 1577.50, 1585.18, 1603.49, 1665.22, 
   1728.13, 1791.50, 1851.46, 1930.01, 2009.08, 2065.41, 2120.37, 2204.38, 
   2283.99, 2328.90, 2374.47, 2435.83, 2552.67, 2656.73, 2707.04, 2753.40, 
   2795.86
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.42, 0.68, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          235.986        start      
        1            56         0.00358       0.52490     
        2            57       1.263e-005     1.455e-004   
        3            58       6.922e-009     5.083e-007   
        4            59       8.077e-011     4.168e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3071.92 psi
   
   Pressure: 
   1538.31, 1546.59, 1557.11, 1568.15, 1583.52, 1646.03, 1704.95, 1764.19, 
   1824.26, 1885.45, 1943.78, 2020.48, 2097.87, 2153.10, 2207.08, 2289.67, 
   2368.02, 2412.27, 2457.21, 2517.78, 2633.23, 2736.16, 2785.99, 2831.99, 
   2874.21
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 0.73, 0.74, 
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          214.088        start      
        1            56         0.00267       1.09377     
        2            57       5.331e-006     1.658e-004   
        3            58       9.731e-009     1.794e-007   
        4            59       1.040e-010     3.484e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3178.36 psi
   
   Pressure: 
   1537.78, 1545.95, 1559.18, 1638.72, 1716.42, 1778.95, 1834.44, 1891.08, 
   1949.09, 2008.43, 2065.19, 2139.99, 2215.61, 2269.65, 2322.54, 2403.59, 
   2480.56, 2524.08, 2568.35, 2628.07, 2742.04, 2843.79, 2893.13, 2938.76, 
   2980.73
   
   Saturation:
   0.20, 0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          236.378        start      
        1            56         0.00574       0.86440     
        2            57       1.153e-005     3.523e-004   
        3            58       1.792e-009     1.394e-006   
        4            59       8.767e-011     5.098e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3289.76 psi
   
   Pressure: 
   1541.15, 1582.98, 1674.05, 1766.28, 1840.51, 1901.21, 1955.59, 2011.36, 
   2068.66, 2127.36, 2183.56, 2257.66, 2332.61, 2386.20, 2438.65, 2519.05, 
   2595.41, 2638.60, 2682.53, 2741.82, 2854.97, 2956.03, 3005.05, 3050.43, 
   3092.21
   
   Saturation:
   0.22, 0.54, 0.71, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          58.8206        start      
        1            56         0.00136       2.36134     
        2            57       1.030e-006     9.812e-004   
        3            58       3.178e-010     2.091e-007   
        4            59       3.560e-011     8.108e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3568.30 psi
   
   Pressure: 
   1829.54, 1898.89, 1984.51, 2072.43, 2143.86, 2202.60, 2255.40, 2309.65, 
   2365.48, 2422.73, 2477.60, 2550.01, 2623.30, 2675.75, 2727.12, 2805.95, 
   2880.89, 2923.32, 2966.52, 3024.89, 3136.41, 3236.14, 3284.59, 3329.52, 
   3370.97
   
   Saturation:
   0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          9.80054        start      
        1            56       7.535e-004      2.74597     
        2            57       1.081e-006      0.00121     
        3            58       3.517e-010     5.056e-007   
        4            59       8.171e-012     2.673e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3573.51 psi
   
   Pressure: 
   1824.09, 1892.77, 1978.65, 2067.42, 2139.74, 2199.29, 2252.85, 2307.89, 
   2364.50, 2422.52, 2478.08, 2551.34, 2625.43, 2678.40, 2730.22, 2809.65, 
   2885.09, 2927.75, 2971.13, 3029.70, 3141.48, 3241.36, 3289.84, 3334.76, 
   3376.18
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.62764        start      
        1            56       5.765e-004      4.05891     
        2            57       9.503e-007      0.00203     
        3            58       5.081e-010     8.654e-007   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3548.51 psi
   
   Pressure: 
   1813.78, 1880.85, 1965.11, 2052.44, 2123.72, 2182.50, 2235.42, 2289.85, 
   2345.87, 2403.32, 2458.36, 2530.97, 2604.44, 2656.98, 2708.41, 2787.27, 
   2862.20, 2904.59, 2947.73, 3005.98, 3117.23, 3216.69, 3265.00, 3309.80, 
   3351.16
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.29632        start      
        1            56       6.662e-004      5.89834     
        2            57       1.474e-006      0.00422     
        3            58       1.119e-009     2.477e-006   
        4            59       7.870e-012     2.938e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3531.02 psi
   
   Pressure: 
   1807.96, 1873.99, 1957.10, 2043.38, 2113.86, 2172.03, 2224.44, 2278.37, 
   2333.91, 2390.88, 2445.49, 2517.54, 2590.48, 2642.65, 2693.75, 2772.11, 
   2846.60, 2888.76, 2931.68, 2989.66, 3100.43, 3199.51, 3247.66, 3292.36, 
   3333.66
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.60858        start      
        1            56       4.810e-004      6.10473     
        2            57       1.062e-006      0.00408     
        3            58       7.313e-010     2.519e-006   
        4            59       7.104e-012     2.255e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3516.92 psi
   
   Pressure: 
   1803.85, 1869.10, 1951.31, 2036.73, 2106.54, 2164.20, 2216.17, 2269.67, 
   2324.78, 2381.33, 2435.55, 2507.12, 2579.58, 2631.43, 2682.22, 2760.15, 
   2834.23, 2876.18, 2918.90, 2976.63, 3086.97, 3185.70, 3233.72, 3278.31, 
   3319.55
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.14658        start      
        1            56       4.146e-004      6.58413     
        2            57       9.874e-007      0.00441     
        3            58       6.645e-010     3.008e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3504.84 psi
   
   Pressure: 
   1800.60, 1865.21, 1946.67, 2031.35, 2100.60, 2157.80, 2209.38, 2262.49, 
   2317.22, 2373.40, 2427.27, 2498.40, 2570.42, 2621.98, 2672.49, 2750.01, 
   2823.74, 2865.49, 2908.03, 2965.54, 3075.47, 3173.89, 3221.77, 3266.27, 
   3307.46
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.83747        start      
        1            56       4.185e-004      7.36587     
        2            57       1.129e-006      0.00525     
        3            58       8.145e-010     4.072e-006   
        4            59       6.723e-012     3.697e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3494.15 psi
   
   Pressure: 
   1797.89, 1861.95, 1942.75, 2026.78, 2095.51, 2152.32, 2203.54, 2256.30, 
   2310.69, 2366.52, 2420.08, 2490.80, 2562.43, 2613.72, 2663.97, 2741.13, 
   2814.52, 2856.09, 2898.46, 2955.76, 3065.32, 3163.45, 3211.21, 3255.62, 
   3296.76
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   8/1/2026 8:42:12 AM
   8/1/2026 8:43:33 AM
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
        0            1          369.537        start      
        1            56         0.00716       0.34378     
        2            57       1.346e-005     3.555e-004   
        3            58       5.342e-009     8.090e-007   
        4            59       1.022e-010     2.523e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2267.05 psi
   
   Pressure: 
   1551.64, 1562.82, 1577.01, 1591.88, 1604.11, 1614.30, 1623.54, 1633.13, 
   1643.10, 1653.40, 1663.35, 1676.60, 1690.12, 1699.89, 1709.54, 1724.49, 
   1738.85, 1747.08, 1755.55, 1767.14, 1789.55, 1810.69, 1849.87, 1932.33, 
   2002.59
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.49, 0.72, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          300.295        start      
        1            56         0.00340       0.88955     
        2            57       8.690e-006     1.593e-004   
        3            58       1.053e-008     2.452e-007   
        4            59       1.249e-010     3.011e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2590.46 psi
   
   Pressure: 
   1551.09, 1562.15, 1576.20, 1590.90, 1603.01, 1613.09, 1622.24, 1631.73, 
   1641.60, 1651.79, 1661.65, 1674.77, 1688.16, 1697.83, 1707.39, 1722.21, 
   1736.44, 1744.59, 1753.00, 1768.92, 1937.42, 2110.72, 2191.42, 2263.13, 
   2326.30
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.28, 0.62, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          353.500        start      
        1            56         0.00694       0.31126     
        2            57       1.753e-005     2.709e-004   
        3            58       3.809e-009     7.958e-007   
        4            59       1.002e-010     1.471e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2743.76 psi
   
   Pressure: 
   1551.53, 1562.69, 1576.85, 1591.68, 1603.89, 1614.05, 1623.27, 1632.84, 
   1642.78, 1653.06, 1662.99, 1676.20, 1689.70, 1699.44, 1709.06, 1723.98, 
   1738.75, 1767.51, 1841.10, 1937.67, 2118.32, 2276.30, 2351.35, 2419.14, 
   2479.74
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.47, 0.70, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          292.959        start      
        1            56         0.00284       0.85413     
        2            57       4.171e-006     9.206e-005   
        3            58       4.626e-009     9.102e-008   
        4            59       9.351e-011     1.020e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2942.36 psi
   
   Pressure: 
   1550.89, 1561.90, 1575.89, 1590.54, 1602.59, 1612.63, 1621.74, 1631.19, 
   1641.01, 1651.17, 1660.98, 1674.04, 1687.38, 1697.02, 1709.01, 1817.34, 
   1939.91, 2007.24, 2074.37, 2163.82, 2333.06, 2482.59, 2554.25, 2619.57, 
   2678.54
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 
   0.72, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          341.728        start      
        1            56         0.00638       0.48645     
        2            57       1.883e-005     3.119e-004   
        3            58       1.986e-009     9.478e-007   
        4            59       1.150e-010     1.022e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3088.88 psi
   
   Pressure: 
   1551.22, 1562.31, 1576.38, 1591.12, 1603.25, 1613.34, 1622.51, 1632.02, 
   1641.89, 1652.10, 1661.97, 1675.24, 1708.00, 1791.75, 1873.41, 1995.33, 
   2109.52, 2173.43, 2237.90, 2324.28, 2488.17, 2633.44, 2703.28, 2767.20, 
   2825.21
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.40, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          300.544        start      
        1            56         0.00458       0.73027     
        2            57       2.808e-006     1.407e-004   
        3            58       3.237e-009     2.261e-007   
        4            59       1.335e-010     1.517e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3278.49 psi
   
   Pressure: 
   1550.89, 1561.90, 1575.89, 1590.52, 1602.57, 1612.60, 1621.71, 1631.16, 
   1640.97, 1652.52, 1712.67, 1826.26, 1938.07, 2016.69, 2092.86, 2208.67, 
   2318.06, 2379.64, 2441.99, 2525.80, 2685.14, 2826.76, 2895.06, 2957.80, 
   3015.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.23, 0.57, 0.71, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          332.365        start      
        1            56         0.00397       0.42506     
        2            57       1.176e-005     1.246e-004   
        3            58       4.038e-009     3.567e-007   
        4            59       1.033e-010     1.546e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3424.64 psi
   
   Pressure: 
   1551.16, 1562.23, 1576.29, 1591.00, 1603.12, 1613.20, 1622.40, 1639.85, 
   1724.03, 1811.57, 1893.32, 1999.74, 2106.48, 2182.37, 2256.31, 2369.19, 
   2476.07, 2536.33, 2597.43, 2679.66, 2836.15, 2975.42, 3042.70, 3104.65, 
   3161.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.34, 
   0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          342.015        start      
        1            56         0.00668       0.55184     
        2            57       1.758e-005     3.117e-004   
        3            58       2.593e-009     9.535e-007   
        4            59       1.209e-010     1.185e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3545.86 psi
   
   Pressure: 
   1550.97, 1561.99, 1575.99, 1590.65, 1603.09, 1638.91, 1718.54, 1798.87, 
   1879.74, 1961.91, 2040.10, 2142.80, 2246.37, 2320.25, 2392.41, 2502.79, 
   2607.47, 2666.57, 2726.57, 2807.42, 2961.45, 3098.73, 3165.15, 3226.45, 
   3282.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.21, 0.47, 0.70, 0.73, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          284.001        start      
        1            56         0.00311       1.23768     
        2            57       2.510e-006     1.365e-004   
        3            58       3.891e-009     9.749e-008   
        4            59       1.184e-010     1.414e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3719.59 psi
   
   Pressure: 
   1549.94, 1560.75, 1577.43, 1677.57, 1780.53, 1863.34, 1936.80, 2011.77, 
   2088.56, 2167.14, 2242.30, 2341.37, 2441.54, 2513.16, 2583.25, 2690.69, 
   2792.75, 2850.48, 2909.19, 2988.45, 3139.68, 3274.73, 3340.23, 3400.82, 
   3456.57
   
   Saturation:
   0.20, 0.20, 0.25, 0.60, 0.71, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          317.451        start      
        1            56         0.00446       1.42541     
        2            57       8.611e-006     9.501e-004   
        3            58       7.504e-009     2.420e-006   
        4            59       1.165e-010     2.102e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3925.50 psi
   
   Pressure: 
   1594.21, 1685.43, 1802.63, 1921.70, 2017.97, 2096.94, 2167.83, 2240.63, 
   2315.52, 2392.31, 2465.91, 2563.02, 2661.33, 2731.67, 2800.57, 2906.27, 
   3006.74, 3063.61, 3121.49, 3199.69, 3349.01, 3482.49, 3547.29, 3607.35, 
   3662.70
   
   Saturation:
   0.35, 0.67, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          20.2292        start      
        1            56         0.00115       1.64588     
        2            57       1.417e-006      0.00109     
        3            58       1.958e-010     6.034e-007   
        4            59       1.505e-011     1.108e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4271.46 psi
   
   Pressure: 
   1940.50, 2033.11, 2148.45, 2267.36, 2364.08, 2443.64, 2515.13, 2588.54, 
   2664.01, 2741.32, 2815.32, 2912.87, 3011.48, 3081.96, 3150.90, 3256.53, 
   3356.82, 3413.53, 3471.19, 3549.00, 3697.47, 3830.09, 3894.44, 3954.06, 
   4009.02
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.60880        start      
        1            56       9.677e-004      6.02519     
        2            57       1.711e-006      0.00303     
        3            58       1.034e-009     1.131e-006   
        4            59       5.680e-012     1.395e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4224.67 psi
   
   Pressure: 
   1918.60, 2008.02, 2120.30, 2236.62, 2331.52, 2409.77, 2480.19, 2552.59, 
   2627.10, 2703.49, 2776.67, 2873.18, 2970.81, 3040.62, 3108.95, 3213.70, 
   3313.20, 3369.50, 3426.77, 3504.11, 3651.76, 3783.74, 3847.85, 3907.29, 
   3962.18
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.27902        start      
        1            56       7.621e-004      7.83290     
        2            57       1.562e-006      0.00480     
        3            58       1.163e-009     2.160e-006   
        4            59       8.830e-012     2.711e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4194.34 psi
   
   Pressure: 
   1908.37, 1995.97, 2106.23, 2220.69, 2314.19, 2391.36, 2460.88, 2532.41, 
   2606.08, 2681.64, 2754.06, 2849.63, 2946.34, 3015.53, 3083.28, 3187.19, 
   3285.95, 3341.85, 3398.76, 3475.64, 3622.51, 3753.88, 3817.73, 3877.01, 
   3931.81
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.31314        start      
        1            56       4.969e-004      7.84292     
        2            57       9.949e-007      0.00435     
        3            58       6.395e-010     2.058e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4171.33 psi
   
   Pressure: 
   1901.75, 1988.07, 2096.85, 2209.88, 2302.28, 2378.60, 2447.39, 2518.20, 
   2591.16, 2666.02, 2737.81, 2832.56, 2928.49, 2997.15, 3064.40, 3167.58, 
   3265.70, 3321.25, 3377.83, 3454.32, 3600.49, 3731.31, 3794.94, 3854.07, 
   3908.79
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.70954        start      
        1            56       6.277e-004      10.3621     
        2            57       1.707e-006      0.00745     
        3            58       1.420e-009     4.853e-006   
        4            59       1.118e-011     5.885e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4152.19 psi
   
   Pressure: 
   1896.71, 1982.02, 2089.60, 2201.45, 2292.93, 2368.52, 2436.68, 2506.87, 
   2579.21, 2653.47, 2724.69, 2818.73, 2913.97, 2982.15, 3048.96, 3151.50, 
   3249.03, 3304.28, 3360.57, 3436.70, 3582.24, 3712.57, 3776.00, 3834.99, 
   3889.63
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.23077        start      
        1            56       5.657e-004      11.1818     
        2            57       1.691e-006      0.00803     
        3            58       1.436e-009     5.723e-006   
        4            59       6.486e-012     7.047e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4135.60 psi
   
   Pressure: 
   1892.57, 1977.03, 2083.59, 2194.43, 2285.10, 2360.06, 2427.66, 2497.30, 
   2569.10, 2642.81, 2713.53, 2806.94, 2901.56, 2969.31, 3035.71, 3137.68, 
   3234.69, 3289.66, 3345.69, 3421.49, 3566.46, 3696.34, 3759.59, 3818.45, 
   3873.01
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.90475        start      
        1            56       3.884e-004      10.0390     
        2            57       1.048e-006      0.00588     
        3            58       7.328e-010     3.810e-006   
        4            59       7.503e-012     3.346e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4120.88 psi
   
   Pressure: 
   1889.02, 1972.74, 2078.41, 2188.35, 2278.32, 2352.70, 2419.81, 2488.96, 
   2560.26, 2633.49, 2703.76, 2796.58, 2890.64, 2958.01, 3024.05, 3125.49, 
   3222.02, 3276.74, 3332.53, 3408.03, 3552.49, 3681.96, 3745.04, 3803.78, 
   3858.27
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.60483        start      
        1            56       4.089e-004      11.1911     
        2            57       1.232e-006      0.00687     
        3            58       9.826e-010     4.800e-006   
        4            59       9.626e-012     5.329e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4107.63 psi
   
   Pressure: 
   1885.89, 1968.97, 2073.84, 2182.98, 2272.31, 2346.18, 2412.84, 2481.54, 
   2552.40, 2625.18, 2695.04, 2787.34, 2880.89, 2947.90, 3013.61, 3114.56, 
   3210.66, 3265.15, 3320.72, 3395.95, 3539.93, 3669.02, 3731.95, 3790.58, 
   3845.01
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.38971        start      
        1            56       4.723e-004      12.7652     
        2            57       1.600e-006      0.00858     
        3            58       1.546e-009     6.297e-006   
        4            59       6.424e-012     9.872e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4095.58 psi
   
   Pressure: 
   1883.09, 1965.58, 2069.74, 2178.15, 2266.90, 2340.31, 2406.56, 2474.85, 
   2545.30, 2617.68, 2687.16, 2778.98, 2872.05, 2938.74, 3004.15, 3104.66, 
   3200.36, 3254.63, 3310.00, 3384.99, 3528.53, 3657.27, 3720.05, 3778.58, 
   3832.95
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.21713        start      
        1            56       3.800e-004      11.8488     
        2            57       1.165e-006      0.00686     
        3            58       1.021e-009     4.630e-006   
        4            59       1.302e-011     6.192e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4084.55 psi
   
   Pressure: 
   1880.56, 1962.52, 2066.02, 2173.76, 2261.98, 2334.97, 2400.85, 2468.76, 
   2538.84, 2610.84, 2679.97, 2771.34, 2863.99, 2930.38, 2995.51, 3095.60, 
   3190.93, 3245.01, 3300.20, 3374.95, 3518.08, 3646.51, 3709.16, 3767.59, 
   3821.91
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   8/1/2026 8:44:25 AM
   8/1/2026 8:45:41 AM
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
        0            1          417.423        start      
        1            56         0.00504       0.42588     
        2            57       1.203e-005     2.616e-004   
        3            58       2.652e-009     5.173e-007   
        4            59       1.335e-010     1.354e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2524.16 psi
   
   Pressure: 
   1564.48, 1578.44, 1596.17, 1614.73, 1630.01, 1642.73, 1654.27, 1666.25, 
   1678.69, 1691.56, 1703.99, 1720.53, 1737.43, 1749.62, 1761.67, 1780.35, 
   1798.29, 1808.56, 1819.15, 1833.63, 1861.70, 1905.07, 2010.56, 2109.26, 
   2193.79
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.33, 0.66, 0.75, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          440.872        start      
        1            56         0.00759       0.26356     
        2            57       2.296e-005     2.309e-004   
        3            58       2.393e-009     7.350e-007   
        4            59       1.557e-010     9.997e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2991.39 psi
   
   Pressure: 
   1564.49, 1578.46, 1596.19, 1614.75, 1630.03, 1642.75, 1654.30, 1666.28, 
   1678.73, 1691.59, 1704.03, 1720.58, 1737.48, 1749.68, 1761.74, 1780.43, 
   1798.38, 1808.73, 1832.31, 1957.98, 2195.83, 2401.15, 2497.88, 2584.61, 
   2661.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.38, 0.68, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          426.115        start      
        1            56         0.00754       0.63519     
        2            57       2.136e-005     3.685e-004   
        3            58       3.504e-009     1.016e-006   
        4            59       1.669e-010     1.699e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3221.23 psi
   
   Pressure: 
   1563.97, 1577.82, 1595.40, 1613.81, 1628.97, 1641.58, 1653.04, 1664.92, 
   1677.26, 1690.02, 1702.35, 1718.76, 1735.52, 1747.62, 1759.67, 1802.04, 
   1956.03, 2042.55, 2128.12, 2241.67, 2456.06, 2645.08, 2735.48, 2817.69, 
   2891.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 
   0.68, 0.73, 0.75, 0.77, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          425.177        start      
        1            56         0.00783       0.59456     
        2            57       2.263e-005     3.561e-004   
        3            58       3.156e-009     1.029e-006   
        4            59       1.061e-010     1.454e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3480.57 psi
   
   Pressure: 
   1563.92, 1577.75, 1595.32, 1613.71, 1628.85, 1641.45, 1652.89, 1664.75, 
   1677.08, 1689.82, 1702.14, 1718.68, 1758.00, 1862.46, 1964.41, 2116.52, 
   2258.95, 2338.66, 2419.05, 2526.78, 2731.14, 2912.28, 2999.36, 3079.06, 
   3151.41
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          433.422        start      
        1            56         0.00761       0.51019     
        2            57       2.244e-005     2.902e-004   
        3            58       2.545e-009     8.887e-007   
        4            59       2.189e-010     1.061e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3760.41 psi
   
   Pressure: 
   1563.92, 1577.76, 1595.32, 1613.71, 1628.85, 1641.45, 1652.89, 1664.76, 
   1677.24, 1709.33, 1816.43, 1955.96, 2093.93, 2191.33, 2285.85, 2429.68, 
   2565.59, 2642.10, 2719.58, 2823.71, 3021.67, 3197.61, 3282.47, 3360.45, 
   3431.61
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.41, 0.69, 0.73, 0.75, 0.76, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          419.825        start      
        1            56         0.00674       0.54005     
        2            57       2.068e-005     2.258e-004   
        3            58       2.524e-009     7.073e-007   
        4            59       1.123e-010     9.651e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3970.49 psi
   
   Pressure: 
   1563.77, 1577.57, 1595.09, 1613.44, 1628.54, 1641.22, 1667.52, 1769.59, 
   1874.13, 1979.26, 2078.65, 2208.76, 2339.68, 2432.93, 2523.92, 2662.96, 
   2794.70, 2869.03, 2944.46, 3046.01, 3239.39, 3411.61, 3494.88, 3571.64, 
   3641.96
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          402.319        start      
        1            56         0.00607       0.94241     
        2            57       1.781e-005     2.918e-004   
        3            58       2.615e-009     9.202e-007   
        4            59       1.758e-010     8.789e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4195.07 psi
   
   Pressure: 
   1562.97, 1576.60, 1593.98, 1630.07, 1757.25, 1863.72, 1957.42, 2052.60, 
   2149.78, 2249.04, 2343.87, 2468.70, 2594.81, 2684.90, 2773.00, 2907.93, 
   3036.02, 3108.43, 3182.03, 3281.31, 3470.66, 3639.63, 3721.52, 3797.23, 
   3866.83
   
   Saturation:
   0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          384.585        start      
        1            56         0.00495       1.37717     
        2            57       9.050e-006     9.604e-004   
        3            58       8.859e-009     2.405e-006   
        4            59       2.155e-010     2.371e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1605.85, 1717.82, 1863.93, 2012.18, 2131.98, 2230.21, 2318.36, 2408.87, 
   2501.98, 2597.45, 2688.93, 2809.65, 2931.84, 3019.26, 3104.89, 3236.22, 
   3361.03, 3431.66, 3503.54, 3600.61, 3785.89, 3951.40, 4031.70, 4106.05, 
   4174.54
   
   Saturation:
   0.33, 0.66, 0.73, 0.75, 0.75, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          18.1872        start      
        1            56       8.775e-004      1.03285     
        2            57       1.032e-006     8.522e-004   
        3            58       1.212e-010     6.342e-007   
        4            59       1.114e-011     4.842e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1973.57, 2073.45, 2198.01, 2326.53, 2431.11, 2517.17, 2594.51, 2673.96, 
   2755.65, 2839.36, 2919.51, 3025.17, 3132.03, 3208.41, 3283.15, 3397.70, 
   3506.50, 3568.02, 3630.61, 3715.09, 3876.36, 4020.46, 4090.41, 4155.23, 
   4215.01
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.60059        start      
        1            56       6.822e-004      1.51305     
        2            57       1.053e-006      0.00153     
        3            58       3.224e-010     1.484e-006   
        4            59       1.010e-011     3.303e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1958.83, 2056.96, 2180.26, 2308.06, 2412.35, 2498.37, 2575.81, 2655.45, 
   2737.44, 2821.51, 2902.06, 3008.33, 3115.86, 3192.77, 3268.07, 3383.54, 
   3493.26, 3555.35, 3618.54, 3703.90, 3866.94, 4012.72, 4083.54, 4149.25, 
   4209.95
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.51046        start      
        1            56       7.647e-004      1.86031     
        2            57       1.485e-006      0.00250     
        3            58       4.799e-010     3.361e-006   
        4            59       7.322e-012     8.461e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1953.16, 2050.41, 2172.90, 2300.08, 2404.00, 2489.80, 2567.11, 2646.67, 
   2728.63, 2812.71, 2893.32, 2999.71, 3107.40, 3184.46, 3259.94, 3375.73, 
   3485.80, 3548.13, 3611.59, 3697.37, 3861.26, 4007.90, 4079.20, 4145.42, 
   4206.66
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.52547        start      
        1            56       6.016e-004      1.77588     
        2            57       1.081e-006      0.00221     
        3            58       2.734e-010     2.889e-006   
        4            59       8.100e-012     5.389e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1949.84, 2046.53, 2168.42, 2295.10, 2398.68, 2484.26, 2561.41, 2640.84, 
   2722.71, 2806.72, 2887.30, 2993.68, 3101.40, 3178.51, 3254.06, 3370.01, 
   3480.28, 3542.73, 3606.37, 3692.41, 3856.88, 4004.13, 4075.77, 4142.37, 
   4204.04
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.87935        start      
        1            56       5.760e-004      1.82758     
        2            57       1.038e-006      0.00229     
        3            58       2.375e-010     3.130e-006   
        4            59       1.082e-011     5.426e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1947.50, 2043.75, 2165.17, 2291.44, 2394.73, 2480.09, 2557.08, 2636.38, 
   2718.13, 2802.06, 2882.58, 2988.90, 3096.61, 3173.72, 3249.31, 3365.34, 
   3475.74, 3538.29, 3602.04, 3688.29, 3853.21, 4000.93, 4072.85, 4139.77, 
   4201.78
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.40001        start      
        1            56       6.107e-004      1.95924     
        2            57       1.150e-006      0.00257     
        3            58       2.637e-010     3.795e-006   
        4            59       1.171e-011     6.901e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1945.66, 2041.56, 2162.60, 2288.50, 2391.52, 2476.70, 2553.54, 2632.70, 
   2714.34, 2798.17, 2878.62, 2984.88, 3092.55, 3169.65, 3245.25, 3361.34, 
   3471.82, 3534.45, 3598.30, 3684.70, 3850.00, 3998.12, 4070.28, 4137.47, 
   4199.79
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.06386        start      
        1            56       7.136e-004      2.18386     
        2            57       1.465e-006      0.00314     
        3            58       3.739e-010     5.179e-006   
        4            59       9.325e-012     1.106e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1944.13, 2039.74, 2160.43, 2286.01, 2388.80, 2473.80, 2550.50, 2629.54, 
   2711.07, 2794.81, 2875.18, 2981.37, 3088.98, 3166.08, 3241.67, 3357.81, 
   3468.36, 3531.04, 3594.97, 3681.51, 3847.13, 3995.60, 4067.97, 4135.40, 
   4198.00
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.75589        start      
        1            56       5.413e-004      1.96187     
        2            57       9.528e-007      0.00237     
        3            58       2.047e-010     3.391e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1942.80, 2038.15, 2158.54, 2283.83, 2386.41, 2471.25, 2547.82, 2626.74, 
   2708.17, 2791.81, 2872.11, 2978.23, 3085.80, 3162.87, 3238.46, 3354.63, 
   3465.23, 3527.96, 3591.96, 3678.63, 3844.52, 3993.31, 4065.87, 4133.52, 
   4196.36
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.53556        start      
        1            56       6.800e-004      2.25144     
        2            57       1.347e-006      0.00309     
        3            58       3.497e-010     5.045e-006   
        4            59       8.933e-012     1.120e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1941.63, 2036.74, 2156.86, 2281.89, 2384.27, 2468.96, 2545.41, 2624.23, 
   2705.55, 2789.11, 2869.34, 2975.39, 3082.91, 3159.96, 3235.55, 3351.73, 
   3462.38, 3525.15, 3589.21, 3675.99, 3842.14, 3991.22, 4063.95, 4131.79, 
   4194.86
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.33810        start      
        1            56       5.562e-004      2.08351     
        2            57       9.835e-007      0.00252     
        3            58       2.300e-010     3.690e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1940.57, 2035.47, 2155.34, 2280.13, 2382.33, 2466.88, 2543.22, 2621.93, 
   2703.17, 2786.64, 2866.81, 2972.79, 3080.25, 3157.28, 3232.87, 3349.07, 
   3459.76, 3522.56, 3586.68, 3673.56, 3839.94, 3989.28, 4062.17, 4130.19, 
   4193.47
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.13512        start      
        1            56       6.912e-004      2.38164     
        2            57       1.357e-006      0.00323     
        3            58       3.931e-010     5.303e-006   
        4            59       8.954e-012     1.334e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1939.61, 2034.31, 2153.95, 2278.52, 2380.55, 2464.97, 2541.21, 2619.82, 
   2700.97, 2784.37, 2864.48, 2970.39, 3077.81, 3154.82, 3230.40, 3346.61, 
   3457.33, 3520.17, 3584.34, 3671.31, 3837.91, 3987.49, 4060.52, 4128.71, 
   4192.18
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.98831        start      
        1            56       5.824e-004      2.22935     
        2            57       1.038e-006      0.00271     
        3            58       2.770e-010     4.055e-006   
        4            59       1.059e-011     9.282e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1938.71, 2033.24, 2152.66, 2277.03, 2378.90, 2463.21, 2539.35, 2617.87, 
   2698.94, 2782.26, 2862.31, 2968.16, 3075.54, 3152.53, 3228.10, 3344.32, 
   3455.07, 3517.95, 3582.16, 3669.21, 3836.01, 3985.82, 4058.99, 4127.33, 
   4190.97
   
   Saturation:
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 0.82, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.84, 
   0.84
   
   
   
   8/1/2026 8:46:36 AM
   8/1/2026 8:48:00 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

