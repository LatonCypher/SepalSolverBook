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
        0            1          198.841        start      
        1            56         0.00274       5.64781     
        2            57       2.842e-006     6.471e-004   
        3            58       9.024e-009     1.145e-007   
        4            59       8.130e-011     5.766e-010   
   Producer BHP: 
   2234.32 psi
   
   Injector BHP: 
   2716.40 psi
   
   Pressure: 
   2286.19, 2296.56, 2303.46, 2314.31, 2327.02, 2336.64, 2345.92, 2353.24, 
   2361.99, 2370.41, 2377.89, 2385.46, 2391.55, 2397.10, 2402.92, 2412.65, 
   2422.36, 2429.40, 2435.49, 2444.50, 2455.61, 2463.13, 2468.52, 2483.10, 
   2541.93
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.41, 
   0.71
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          179.260        start      
        1            56         0.00286       6.03737     
        2            57       1.388e-006     6.315e-004   
        3            58       2.767e-009     1.969e-007   
        4            59       5.627e-011     2.534e-010   
   Producer BHP: 
   1580.79 psi
   
   Injector BHP: 
   2108.61 psi
   
   Pressure: 
   1632.77, 1643.16, 1650.05, 1660.91, 1673.61, 1683.21, 1692.48, 1699.77, 
   1708.49, 1716.87, 1724.31, 1731.83, 1737.88, 1743.38, 1749.15, 1758.78, 
   1768.38, 1775.34, 1781.34, 1790.23, 1801.17, 1809.07, 1832.94, 1880.67, 
   1933.61
   
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
        0            1          151.579        start      
        1            56         0.00360       0.48525     
        2            57       7.482e-006     1.553e-004   
        3            58       5.555e-009     4.210e-007   
        4            59       3.572e-011     4.806e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2039.16 psi
   
   Pressure: 
   1537.45, 1545.01, 1550.10, 1558.19, 1567.77, 1575.09, 1582.25, 1587.97, 
   1594.87, 1601.61, 1607.68, 1613.89, 1618.96, 1623.63, 1628.59, 1637.00, 
   1645.51, 1651.76, 1657.23, 1665.47, 1678.04, 1727.83, 1771.44, 1814.86, 
   1864.07
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.25, 0.61, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          156.717        start      
        1            56         0.00362       0.47568     
        2            57       1.799e-005     2.857e-004   
        3            58       3.524e-008     8.102e-007   
        4            59       5.571e-011     2.024e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2117.07 psi
   
   Pressure: 
   1537.43, 1544.98, 1550.06, 1558.15, 1567.72, 1575.04, 1582.20, 1587.90, 
   1594.81, 1601.54, 1607.61, 1613.81, 1618.88, 1623.55, 1628.51, 1636.91, 
   1645.41, 1651.66, 1657.14, 1669.75, 1752.67, 1812.70, 1853.54, 1894.70, 
   1942.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.30, 0.64, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.873        start      
        1            56         0.00526       0.21769     
        2            57       2.544e-005     2.260e-004   
        3            58       9.566e-009     1.209e-006   
        4            59       3.964e-011     9.548e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2178.50 psi
   
   Pressure: 
   1537.52, 1545.09, 1550.18, 1558.28, 1567.88, 1575.22, 1582.39, 1588.11, 
   1595.03, 1601.78, 1607.85, 1614.07, 1619.15, 1623.83, 1628.80, 1637.22, 
   1645.74, 1652.04, 1663.41, 1734.30, 1821.56, 1878.45, 1917.56, 1957.35, 
   2003.49
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.37, 0.68, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          173.976        start      
        1            56         0.00719       0.33262     
        2            57       3.095e-005     4.531e-004   
        3            58       7.030e-009     2.328e-006   
        4            59       4.185e-011     3.786e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2215.32 psi
   
   Pressure: 
   1537.48, 1545.04, 1550.12, 1558.21, 1567.80, 1575.12, 1582.29, 1588.00, 
   1594.91, 1601.65, 1607.72, 1613.93, 1618.99, 1623.66, 1628.62, 1637.03, 
   1645.71, 1664.02, 1711.42, 1780.52, 1863.49, 1918.29, 1956.22, 1995.02, 
   2040.32
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.44, 0.69, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          164.644        start      
        1            56         0.00760       0.76816     
        2            57       2.292e-005     6.877e-004   
        3            58       1.012e-008     2.921e-006   
        4            59       4.779e-011     8.712e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2269.62 psi
   
   Pressure: 
   1537.29, 1544.81, 1549.87, 1557.92, 1567.45, 1574.74, 1581.86, 1587.54, 
   1594.42, 1601.12, 1607.16, 1613.34, 1618.38, 1623.02, 1627.96, 1636.84, 
   1675.81, 1729.99, 1775.68, 1841.71, 1921.75, 1974.98, 2011.98, 2050.01, 
   2094.66
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 
   0.52, 0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.778        start      
        1            56         0.00420       0.80493     
        2            57       2.054e-006     1.575e-004   
        3            58       2.763e-009     2.909e-007   
        4            59       4.149e-011     3.594e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2338.16 psi
   
   Pressure: 
   1537.23, 1544.74, 1549.79, 1557.83, 1567.35, 1574.63, 1581.74, 1587.41, 
   1594.28, 1600.97, 1607.00, 1613.17, 1618.21, 1622.85, 1628.63, 1681.98, 
   1755.25, 1807.11, 1851.05, 1915.15, 1993.23, 2045.34, 2081.66, 2119.11, 
   2163.23
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 0.58, 
   0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.295        start      
        1            56         0.00310       0.34506     
        2            57       1.429e-005     1.632e-004   
        3            58       2.644e-008     3.599e-007   
        4            59       7.753e-011     7.946e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2382.46 psi
   
   Pressure: 
   1537.43, 1544.98, 1550.05, 1558.13, 1567.70, 1575.01, 1582.16, 1587.87, 
   1594.77, 1601.50, 1607.56, 1613.76, 1618.83, 1625.53, 1663.83, 1736.23, 
   1806.63, 1856.95, 1899.88, 1962.81, 2039.60, 2090.95, 2126.79, 2163.81, 
   2207.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.29, 0.63, 0.72, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.218        start      
        1            56         0.00503       0.25234     
        2            57       2.459e-005     1.886e-004   
        3            58       7.140e-009     1.062e-006   
        4            59       5.540e-011     5.739e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2413.17 psi
   
   Pressure: 
   1537.47, 1545.03, 1550.11, 1558.20, 1567.77, 1575.10, 1582.25, 1587.96, 
   1594.87, 1601.61, 1607.67, 1613.92, 1623.79, 1663.11, 1705.52, 1774.98, 
   1843.33, 1892.49, 1934.61, 1996.48, 2072.13, 2122.80, 2158.22, 2194.87, 
   2238.30
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          171.483        start      
        1            56         0.00740       0.41652     
        2            57       3.450e-005     4.684e-004   
        3            58       8.717e-009     2.596e-006   
        4            59       5.752e-011     5.056e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2448.18 psi
   
   Pressure: 
   1537.38, 1544.92, 1549.99, 1558.06, 1567.62, 1574.92, 1582.06, 1587.76, 
   1594.64, 1601.36, 1607.53, 1625.49, 1669.17, 1708.72, 1749.46, 1816.89, 
   1883.68, 1931.88, 1973.26, 2034.19, 2108.79, 2158.83, 2193.86, 2230.19, 
   2273.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          165.407        start      
        1            56         0.00721       0.60733     
        2            57       2.478e-005     4.904e-004   
        3            58       6.705e-009     2.375e-006   
        4            59       4.606e-011     4.326e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2493.52 psi
   
   Pressure: 
   1537.27, 1544.79, 1549.84, 1557.88, 1567.41, 1574.69, 1581.81, 1587.48, 
   1594.35, 1601.42, 1628.09, 1681.87, 1724.42, 1762.53, 1802.17, 1868.13, 
   1933.67, 1981.07, 2021.83, 2081.93, 2155.60, 2205.09, 2239.78, 2275.81, 
   2318.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          156.253        start      
        1            56         0.00481       0.75007     
        2            57       7.168e-006     2.699e-004   
        3            58       7.510e-009     8.765e-007   
        4            59       4.554e-011     5.135e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2538.84 psi
   
   Pressure: 
   1537.17, 1544.66, 1549.70, 1557.72, 1567.22, 1574.48, 1581.58, 1587.24, 
   1594.91, 1632.54, 1684.76, 1736.47, 1777.60, 1814.78, 1853.63, 1918.45, 
   1982.98, 2029.72, 2069.95, 2129.34, 2202.21, 2251.22, 2285.61, 2321.39, 
   2364.05
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.23, 0.56, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.737        start      
        1            56         0.00299       0.76917     
        2            57       5.232e-006     1.027e-004   
        3            58       7.569e-009     1.901e-007   
        4            59       3.758e-011     3.117e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2589.21 psi
   
   Pressure: 
   1537.14, 1544.62, 1549.66, 1557.67, 1567.16, 1574.41, 1581.51, 1588.49, 
   1635.70, 1693.60, 1743.85, 1794.06, 1834.31, 1870.84, 1909.10, 1973.03, 
   2036.75, 2082.94, 2122.73, 2181.53, 2253.71, 2302.31, 2336.45, 2371.99, 
   2414.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          149.128        start      
        1            56         0.00308       0.56489     
        2            57       1.375e-005     1.727e-004   
        3            58       2.072e-008     4.894e-007   
        4            59       4.981e-011     1.042e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2632.74 psi
   
   Pressure: 
   1537.24, 1544.75, 1549.80, 1557.84, 1567.35, 1574.64, 1585.12, 1629.22, 
   1688.45, 1744.34, 1793.41, 1842.78, 1882.48, 1918.58, 1956.44, 2019.75, 
   2082.89, 2128.68, 2168.15, 2226.49, 2298.16, 2346.43, 2380.36, 2415.72, 
   2458.01
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.63, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.653        start      
        1            56         0.00510       0.53149     
        2            57       2.520e-005     2.736e-004   
        3            58       6.063e-009     1.508e-006   
        4            59       4.630e-011     5.563e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2684.15 psi
   
   Pressure: 
   1537.26, 1544.77, 1549.82, 1557.85, 1567.42, 1581.74, 1642.11, 1690.87, 
   1748.00, 1802.49, 1850.70, 1899.35, 1938.54, 1974.23, 2011.68, 2074.37, 
   2136.91, 2182.29, 2221.43, 2279.32, 2350.46, 2398.41, 2432.14, 2467.32, 
   2509.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 
   0.75, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          170.263        start      
        1            56         0.00734       0.74668     
        2            57       3.445e-005     6.834e-004   
        3            58       7.878e-009     3.752e-006   
        4            59       5.701e-011     7.151e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2739.27 psi
   
   Pressure: 
   1537.10, 1544.58, 1549.61, 1557.75, 1585.07, 1648.02, 1708.46, 1755.40, 
   1811.02, 1864.42, 1911.84, 1959.78, 1998.46, 2033.71, 2070.74, 2132.76, 
   2194.69, 2239.64, 2278.44, 2335.86, 2406.47, 2454.10, 2487.63, 2522.64, 
   2564.61
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.821        start      
        1            56         0.00745       0.90694     
        2            57       2.690e-005     6.628e-004   
        3            58       2.883e-009     3.455e-006   
        4            59       4.169e-011     2.284e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2808.99 psi
   
   Pressure: 
   1536.99, 1544.44, 1549.75, 1585.05, 1667.64, 1728.96, 1787.37, 1833.17, 
   1887.74, 1940.30, 1987.07, 2034.41, 2072.64, 2107.51, 2144.16, 2205.57, 
   2266.92, 2311.48, 2349.96, 2406.94, 2477.06, 2524.38, 2557.72, 2592.56, 
   2634.38
   
   Saturation:
   0.20, 0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.517        start      
        1            56         0.00386       0.68672     
        2            57       3.726e-006     1.202e-004   
        3            58       9.052e-009     1.176e-007   
        4            59       3.907e-011     3.173e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2861.85 psi
   
   Pressure: 
   1537.14, 1546.08, 1579.10, 1648.79, 1728.74, 1788.49, 1845.91, 1891.12, 
   1945.12, 1997.22, 2043.61, 2090.60, 2128.56, 2163.19, 2199.60, 2260.62, 
   2321.60, 2365.89, 2404.15, 2460.82, 2530.57, 2577.67, 2610.86, 2645.57, 
   2687.28
   
   Saturation:
   0.20, 0.25, 0.59, 0.71, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          146.761        start      
        1            56         0.00349       1.54373     
        2            57       1.419e-005     7.473e-004   
        3            58       1.157e-008     2.974e-006   
        4            59       3.368e-011     2.253e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2917.60 psi
   
   Pressure: 
   1555.25, 1613.23, 1655.93, 1722.08, 1798.86, 1856.73, 1912.61, 1956.74, 
   2009.59, 2060.65, 2106.20, 2152.39, 2189.74, 2223.85, 2259.75, 2319.98, 
   2380.21, 2424.01, 2461.88, 2518.03, 2587.20, 2633.96, 2666.95, 2701.49, 
   2743.06
   
   Saturation:
   0.30, 0.64, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   7/28/2026 10:59:05 AM
   7/28/2026 11:01:19 AM
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
        0            1          245.375        start      
        1            56         0.00317       0.23058     
        2            57       1.251e-005     2.095e-004   
        3            58       1.558e-008     4.128e-007   
        4            59       8.167e-011     4.557e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2196.12 psi
   
   Pressure: 
   1556.37, 1567.75, 1575.40, 1587.58, 1602.01, 1613.03, 1623.81, 1632.41, 
   1642.81, 1652.96, 1662.09, 1671.44, 1679.07, 1686.10, 1693.57, 1706.23, 
   1719.03, 1728.44, 1736.68, 1749.08, 1764.56, 1775.22, 1785.91, 1851.03, 
   1933.57
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.64, 
   0.75
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          228.315        start      
        1            56         0.00350       0.57227     
        2            57       4.191e-006     1.259e-004   
        3            58       4.765e-009     1.513e-007   
        4            59       7.123e-011     2.335e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2307.10 psi
   
   Pressure: 
   1556.14, 1567.47, 1575.09, 1587.22, 1601.58, 1612.56, 1623.30, 1631.86, 
   1642.21, 1652.31, 1661.41, 1670.72, 1678.31, 1685.32, 1692.75, 1705.36, 
   1718.11, 1727.48, 1735.68, 1748.03, 1766.66, 1841.00, 1906.36, 1971.21, 
   2044.61
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          246.983        start      
        1            56         0.00691       0.75525     
        2            57       1.337e-005     4.967e-004   
        3            58       7.305e-009     1.363e-006   
        4            59       7.172e-011     5.116e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2469.48 psi
   
   Pressure: 
   1556.01, 1567.31, 1574.91, 1587.01, 1601.34, 1612.29, 1623.00, 1631.53, 
   1641.86, 1651.93, 1661.01, 1670.29, 1677.86, 1684.84, 1692.25, 1704.82, 
   1717.52, 1726.86, 1735.60, 1796.65, 1930.61, 2017.62, 2077.15, 2137.45, 
   2207.15
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.22, 0.53, 0.71, 0.75, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          261.972        start      
        1            56         0.00650       0.36044     
        2            57       2.108e-005     3.530e-004   
        3            58       2.243e-009     1.263e-006   
        4            59       8.505e-011     1.139e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2570.92 psi
   
   Pressure: 
   1556.20, 1567.54, 1575.17, 1587.31, 1601.68, 1612.67, 1623.41, 1631.98, 
   1642.34, 1652.45, 1661.55, 1670.86, 1678.46, 1685.46, 1692.90, 1705.51, 
   1718.49, 1745.18, 1816.43, 1919.98, 2044.20, 2126.19, 2182.91, 2240.93, 
   2308.68
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.43, 0.69, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          249.521        start      
        1            56         0.00458       0.47916     
        2            57       1.607e-005     2.569e-004   
        3            58       3.542e-009     8.686e-007   
        4            59       8.248e-011     2.677e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2706.08 psi
   
   Pressure: 
   1556.00, 1567.30, 1574.89, 1586.99, 1601.31, 1612.26, 1622.96, 1631.50, 
   1641.83, 1651.91, 1660.98, 1670.26, 1677.84, 1684.82, 1692.28, 1716.25, 
   1824.75, 1904.58, 1971.71, 2069.16, 2187.52, 2266.34, 2321.18, 2377.61, 
   2443.98
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 
   0.67, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          223.732        start      
        1            56         0.00269       0.43253     
        2            57       8.652e-006     1.141e-004   
        3            58       9.772e-009     1.988e-007   
        4            59       8.364e-011     2.557e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2819.73 psi
   
   Pressure: 
   1556.01, 1567.31, 1574.91, 1587.00, 1601.33, 1612.28, 1622.99, 1631.52, 
   1641.85, 1651.93, 1661.01, 1670.29, 1677.88, 1687.60, 1744.60, 1853.05, 
   1958.38, 2033.64, 2097.84, 2191.90, 2306.68, 2383.42, 2436.99, 2492.32, 
   2557.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          227.737        start      
        1            56         0.00421       0.61914     
        2            57       2.546e-006     1.407e-004   
        3            58       3.771e-009     2.570e-007   
        4            59       6.792e-011     2.053e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2889.38 psi
   
   Pressure: 
   1555.82, 1567.08, 1574.65, 1586.70, 1600.97, 1611.88, 1622.55, 1631.05, 
   1641.34, 1651.38, 1660.42, 1671.07, 1716.97, 1777.34, 1839.24, 1941.19, 
   2041.90, 2114.50, 2176.77, 2268.38, 2380.48, 2455.65, 2508.25, 2562.76, 
   2627.46
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.24, 0.57, 0.71, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          252.159        start      
        1            56         0.00688       0.63252     
        2            57       1.898e-005     4.079e-004   
        3            58       5.402e-009     1.407e-006   
        4            59       1.060e-010     3.257e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2983.80 psi
   
   Pressure: 
   1555.77, 1567.03, 1574.59, 1586.63, 1600.89, 1611.79, 1622.44, 1630.93, 
   1641.21, 1651.68, 1689.26, 1769.88, 1833.58, 1890.59, 1949.87, 2048.51, 
   2146.49, 2217.34, 2278.27, 2368.10, 2478.19, 2552.16, 2604.00, 2657.85, 
   2721.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.50, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          251.233        start      
        1            56         0.00479       0.49658     
        2            57       1.680e-005     2.390e-004   
        3            58       1.968e-009     8.625e-007   
        4            59       7.978e-011     1.202e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3088.76 psi
   
   Pressure: 
   1555.84, 1567.10, 1574.68, 1586.73, 1601.00, 1611.92, 1622.59, 1631.16, 
   1653.39, 1739.58, 1816.59, 1893.00, 1954.02, 2009.27, 2067.06, 2163.52, 
   2259.55, 2329.11, 2389.00, 2477.42, 2585.90, 2658.88, 2710.10, 2763.41, 
   2827.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.38, 0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          220.143        start      
        1            56         0.00253       0.69516     
        2            57       7.509e-006     1.219e-004   
        3            58       6.384e-009     2.349e-007   
        4            59       8.110e-011     2.500e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3189.26 psi
   
   Pressure: 
   1555.56, 1566.77, 1574.30, 1586.30, 1600.50, 1611.37, 1626.13, 1690.54, 
   1779.09, 1862.53, 1935.76, 2009.41, 2068.64, 2122.50, 2178.98, 2273.44, 
   2367.64, 2435.96, 2494.86, 2581.94, 2688.91, 2760.97, 2811.63, 2864.45, 
   2927.64
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.62, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          226.610        start      
        1            56         0.00461       1.03531     
        2            57       4.501e-006     2.386e-004   
        3            58       4.921e-009     4.918e-007   
        4            59       7.146e-011     3.170e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3300.06 psi
   
   Pressure: 
   1555.09, 1566.21, 1573.68, 1585.57, 1601.50, 1664.96, 1756.69, 1827.59, 
   1911.16, 1991.18, 2062.15, 2133.86, 2191.70, 2244.42, 2299.80, 2392.54, 
   2485.15, 2552.39, 2610.42, 2696.32, 2801.95, 2873.21, 2923.37, 2975.75, 
   3038.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.23, 0.57, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          251.585        start      
        1            56         0.00719       0.92331     
        2            57       2.204e-005     5.626e-004   
        3            58       1.453e-009     2.169e-006   
        4            59       8.785e-011     1.145e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3447.53 psi
   
   Pressure: 
   1555.18, 1566.31, 1574.09, 1620.15, 1743.64, 1835.30, 1922.54, 1990.89, 
   2072.32, 2150.73, 2220.48, 2291.10, 2348.11, 2400.12, 2454.78, 2546.38, 
   2637.90, 2704.37, 2761.77, 2846.77, 2951.37, 3021.98, 3071.72, 3123.72, 
   3186.18
   
   Saturation:
   0.20, 0.20, 0.21, 0.49, 0.70, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          257.499        start      
        1            56         0.00617       0.62082     
        2            57       2.208e-005     4.795e-004   
        3            58       1.440e-008     2.231e-006   
        4            59       7.530e-011     1.351e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3558.18 psi
   
   Pressure: 
   1556.30, 1585.37, 1650.68, 1753.18, 1871.20, 1959.73, 2044.97, 2112.14, 
   2192.43, 2269.90, 2338.91, 2408.81, 2465.28, 2516.81, 2570.98, 2661.77, 
   2752.50, 2818.42, 2875.35, 2959.69, 3063.51, 3133.63, 3183.06, 3234.77, 
   3296.94
   
   Saturation:
   0.21, 0.41, 0.69, 0.73, 0.75, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          86.8476        start      
        1            56         0.00354       7.04327     
        2            57       2.233e-006      0.00242     
        3            58       1.144e-009     4.592e-007   
        4            59       6.221e-011     3.732e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3896.69 psi
   
   Pressure: 
   1935.83, 2025.33, 2083.60, 2174.76, 2281.33, 2362.02, 2440.25, 2502.20, 
   2576.63, 2648.77, 2713.31, 2778.94, 2832.16, 2880.91, 2932.34, 3018.89, 
   3105.71, 3169.02, 3223.92, 3305.59, 3406.51, 3474.96, 3523.41, 3574.31, 
   3635.80
   
   Saturation:
   0.66, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          10.6323        start      
        1            56         0.00109       3.66658     
        2            57       1.730e-006      0.00212     
        3            58       6.670e-010     8.677e-007   
        4            59       7.410e-012     6.632e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4003.85 psi
   
   Pressure: 
   1974.25, 2068.00, 2129.93, 2227.24, 2341.15, 2427.32, 2510.68, 2576.54, 
   2655.41, 2731.58, 2799.47, 2868.26, 2923.83, 2974.54, 3027.85, 3117.21, 
   3206.49, 3271.37, 3327.41, 3410.44, 3512.69, 3581.78, 3630.52, 3681.57, 
   3743.07
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.72678        start      
        1            56       6.716e-004      5.69254     
        2            57       1.097e-006      0.00288     
        3            58       6.296e-010     1.015e-006   
        4            59       7.037e-012     1.206e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3969.80 psi
   
   Pressure: 
   1957.73, 2049.03, 2109.68, 2205.31, 2317.47, 2402.47, 2484.79, 2549.89, 
   2627.92, 2703.33, 2770.59, 2838.77, 2893.88, 2944.19, 2997.10, 3085.84, 
   3174.55, 3239.03, 3294.77, 3377.39, 3479.19, 3548.03, 3596.62, 3647.55, 
   3708.99
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.36148        start      
        1            56       7.629e-004      7.98463     
        2            57       1.652e-006      0.00584     
        3            58       1.393e-009     2.803e-006   
        4            59       6.825e-012     4.742e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3946.80 psi
   
   Pressure: 
   1948.64, 2038.40, 2098.18, 2192.56, 2303.40, 2387.46, 2468.95, 2533.43, 
   2610.75, 2685.51, 2752.23, 2819.88, 2874.59, 2924.56, 2977.12, 3065.31, 
   3153.50, 3217.63, 3273.09, 3355.34, 3456.73, 3525.33, 3573.78, 3624.61, 
   3685.97
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.57855        start      
        1            56       4.972e-004      7.90481     
        2            57       1.035e-006      0.00521     
        3            58       7.473e-010     2.666e-006   
        4            59       7.987e-012     2.899e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3928.91 psi
   
   Pressure: 
   1942.38, 2031.02, 2090.13, 2183.52, 2293.28, 2376.58, 2457.36, 2521.31, 
   2598.03, 2672.24, 2738.48, 2805.68, 2860.03, 2909.69, 2961.95, 3049.64, 
   3137.37, 3201.20, 3256.40, 3338.32, 3439.33, 3507.71, 3556.03, 3606.76, 
   3668.05
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.11650        start      
        1            56       4.256e-004      8.42468     
        2            57       9.554e-007      0.00561     
        3            58       6.710e-010     3.224e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3913.82 psi
   
   Pressure: 
   1937.51, 2025.25, 2083.79, 2176.36, 2285.20, 2367.82, 2447.99, 2511.47, 
   2587.65, 2661.37, 2727.18, 2793.97, 2848.00, 2897.38, 2949.36, 3036.61, 
   3123.93, 3187.46, 3242.44, 3324.05, 3424.72, 3492.89, 3541.09, 3591.73, 
   3652.95
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.80827        start      
        1            56       4.273e-004      9.35794     
        2            57       1.088e-006      0.00669     
        3            58       8.146e-010     4.420e-006   
        4            59       7.813e-012     4.527e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3900.62 psi
   
   Pressure: 
   1933.47, 2020.44, 2078.50, 2170.34, 2278.36, 2360.40, 2440.01, 2503.07, 
   2578.77, 2652.03, 2717.47, 2783.88, 2837.62, 2886.74, 2938.46, 3025.31, 
   3112.24, 3175.51, 3230.27, 3311.59, 3411.95, 3479.94, 3528.03, 3578.57, 
   3639.73
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/28/2026 11:07:01 AM
   7/28/2026 11:07:50 AM
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
        0            1          357.629        start      
        1            56         0.00749       0.39912     
        2            57       1.260e-005     3.771e-004   
        3            58       6.647e-009     8.448e-007   
        4            59       1.093e-010     3.206e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2461.93 psi
   
   Pressure: 
   1575.18, 1590.36, 1600.56, 1616.80, 1636.03, 1650.74, 1665.11, 1676.57, 
   1690.44, 1703.96, 1716.14, 1728.60, 1738.76, 1748.13, 1758.09, 1774.96, 
   1792.01, 1804.55, 1815.53, 1832.04, 1852.66, 1867.69, 1913.62, 2007.68, 
   2112.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.52, 0.72, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          311.930        start      
        1            56         0.00319       0.66557     
        2            57       8.770e-006     1.765e-004   
        3            58       5.822e-009     3.313e-007   
        4            59       1.008e-010     2.406e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2729.23 psi
   
   Pressure: 
   1574.59, 1589.65, 1599.77, 1615.89, 1634.97, 1649.56, 1663.83, 1675.21, 
   1688.97, 1702.40, 1714.49, 1726.87, 1736.97, 1746.28, 1756.17, 1772.93, 
   1789.89, 1802.36, 1813.29, 1837.79, 2002.82, 2122.49, 2203.75, 2285.59, 
   2379.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.30, 0.64, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          349.713        start      
        1            56         0.00643       0.39427     
        2            57       1.715e-005     3.134e-004   
        3            58       2.965e-009     8.787e-007   
        4            59       1.246e-010     1.474e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2925.90 psi
   
   Pressure: 
   1574.92, 1590.05, 1600.21, 1616.40, 1635.56, 1650.21, 1664.53, 1675.95, 
   1689.77, 1703.24, 1715.38, 1727.79, 1737.92, 1747.25, 1757.17, 1773.98, 
   1791.25, 1826.12, 1921.18, 2059.12, 2224.53, 2333.68, 2409.19, 2486.42, 
   2576.60
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.21, 0.43, 0.69, 0.74, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          305.569        start      
        1            56         0.00468       1.03650     
        2            57       3.174e-006     2.386e-004   
        3            58       2.779e-009     3.307e-007   
        4            59       1.406e-010     1.573e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3161.12 psi
   
   Pressure: 
   1573.95, 1588.88, 1598.91, 1614.89, 1633.80, 1648.26, 1662.39, 1673.66, 
   1687.29, 1700.59, 1712.57, 1724.82, 1734.81, 1744.03, 1755.19, 1856.58, 
   2002.21, 2105.14, 2192.31, 2319.52, 2474.44, 2577.89, 2650.01, 2724.40, 
   2812.14
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 0.57, 
   0.72, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          325.167        start      
        1            56         0.00407       0.36456     
        2            57       1.230e-005     1.348e-004   
        3            58       4.704e-009     3.734e-007   
        4            59       1.336e-010     1.904e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3317.10 psi
   
   Pressure: 
   1574.70, 1589.77, 1599.91, 1616.04, 1635.14, 1649.75, 1664.03, 1675.42, 
   1689.20, 1702.64, 1714.74, 1727.18, 1745.32, 1823.75, 1908.42, 2046.84, 
   2182.92, 2280.77, 2364.54, 2487.60, 2637.98, 2738.70, 2809.10, 2881.97, 
   2968.34
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.34, 0.66, 0.73, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          339.832        start      
        1            56         0.00703       0.66086     
        2            57       1.673e-005     3.730e-004   
        3            58       4.425e-009     1.032e-006   
        4            59       8.729e-011     2.258e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3471.03 psi
   
   Pressure: 
   1574.23, 1589.21, 1599.28, 1615.31, 1634.29, 1648.79, 1662.97, 1674.28, 
   1687.96, 1701.80, 1749.06, 1856.44, 1941.23, 2017.08, 2095.93, 2227.09, 
   2357.37, 2451.58, 2532.59, 2652.02, 2798.37, 2896.70, 2965.62, 3037.22, 
   3122.47
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.49, 0.70, 0.74, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          295.847        start      
        1            56         0.00372       0.98621     
        2            57       1.626e-006     1.431e-004   
        3            58       1.503e-009     1.611e-007   
        4            59       9.415e-011     8.574e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3652.03 psi
   
   Pressure: 
   1573.48, 1588.31, 1598.27, 1614.14, 1632.92, 1647.29, 1661.33, 1674.32, 
   1760.40, 1875.34, 1974.90, 2074.31, 2153.97, 2226.28, 2302.02, 2428.62, 
   2554.80, 2646.29, 2725.14, 2841.67, 2984.77, 3081.13, 3148.85, 3219.40, 
   3303.72
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.24, 
   0.58, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          311.331        start      
        1            56         0.00359       0.84942     
        2            57       1.037e-005     1.644e-004   
        3            58       4.890e-009     3.967e-007   
        4            59       1.687e-010     2.293e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3838.06 psi
   
   Pressure: 
   1573.57, 1588.41, 1598.39, 1614.27, 1633.14, 1657.22, 1774.48, 1871.14, 
   1984.13, 2091.80, 2187.01, 2283.07, 2360.46, 2430.95, 2504.94, 2628.80, 
   2752.42, 2842.14, 2919.55, 3034.08, 3174.89, 3269.84, 3336.66, 3406.41, 
   3490.01
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.32, 0.66, 0.72, 
   0.74, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          339.025        start      
        1            56         0.00716       0.98655     
        2            57       1.945e-005     4.872e-004   
        3            58       1.220e-009     1.526e-006   
        4            59       1.151e-010     7.468e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4076.50 psi
   
   Pressure: 
   1573.18, 1587.94, 1598.14, 1651.81, 1815.68, 1937.44, 2053.20, 2143.85, 
   2251.80, 2355.73, 2448.18, 2541.77, 2617.33, 2686.27, 2758.72, 2880.15, 
   3001.47, 3089.60, 3165.72, 3278.45, 3417.19, 3510.85, 3576.86, 3645.87, 
   3728.77
   
   Saturation:
   0.20, 0.20, 0.21, 0.46, 0.70, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          284.112        start      
        1            56         0.00304       1.53232     
        2            57       2.993e-006     2.863e-004   
        3            58       1.297e-009     6.092e-007   
        4            59       1.606e-010     2.172e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4272.36 psi
   
   Pressure: 
   1590.41, 1692.34, 1777.02, 1907.86, 2059.50, 2173.72, 2283.99, 2371.05, 
   2475.32, 2576.08, 2665.97, 2757.15, 2830.89, 2898.27, 2969.17, 3088.16, 
   3207.19, 3293.76, 3368.63, 3479.64, 3616.44, 3708.93, 3774.20, 3842.58, 
   3924.91
   
   Saturation:
   0.26, 0.60, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          22.9204        start      
        1            56         0.00116       0.95636     
        2            57       1.554e-006      0.00119     
        3            58       1.345e-010     1.080e-006   
        4            59       1.354e-011     4.628e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2078.93, 2192.28, 2266.68, 2383.27, 2519.47, 2622.38, 2721.84, 2800.38, 
   2894.38, 2985.14, 3066.01, 3147.93, 3214.11, 3274.49, 3337.97, 3444.36, 
   3550.68, 3627.92, 3694.66, 3793.53, 3915.29, 3997.57, 4055.61, 4116.39, 
   4189.61
   
   Saturation:
   0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.68230        start      
        1            56       8.632e-004      1.81624     
        2            57       1.488e-006      0.00219     
        3            58       5.206e-010     2.359e-006   
        4            59       6.429e-012     6.731e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2058.05, 2169.18, 2242.94, 2359.13, 2495.34, 2598.52, 2698.42, 2777.41, 
   2872.06, 2963.53, 3045.10, 3127.79, 3194.63, 3255.65, 3319.82, 3427.45, 
   3535.04, 3613.26, 3680.87, 3781.11, 3904.61, 3988.12, 4047.07, 4108.88, 
   4183.42
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.34171        start      
        1            56       8.102e-004      2.09822     
        2            57       1.638e-006      0.00306     
        3            58       6.141e-010     4.177e-006   
        4            59       6.948e-012     1.350e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2050.24, 2160.31, 2233.60, 2349.30, 2485.14, 2588.16, 2688.01, 2767.02, 
   2861.76, 2953.37, 3035.11, 3118.00, 3185.04, 3246.26, 3310.69, 3418.76, 
   3526.85, 3605.47, 3673.45, 3774.29, 3898.60, 3982.71, 4042.13, 4104.47, 
   4179.74
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.26453        start      
        1            56       5.473e-004      1.88558     
        2            57       9.657e-007      0.00236     
        3            58       2.629e-010     2.952e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2045.89, 2155.28, 2228.23, 2343.50, 2478.96, 2581.77, 2681.47, 2760.40, 
   2855.10, 2946.70, 3028.47, 3111.43, 3178.53, 3239.85, 3304.38, 3412.69, 
   3521.05, 3599.88, 3668.09, 3769.31, 3894.13, 3978.65, 4038.38, 4101.11, 
   4176.93
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.68398        start      
        1            56       7.982e-004      2.39335     
        2            57       1.837e-006      0.00403     
        3            58       6.078e-010     6.928e-006   
        4            59       9.018e-012     2.017e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2042.91, 2151.80, 2224.47, 2339.39, 2474.50, 2577.09, 2676.63, 2755.46, 
   2850.06, 2941.61, 3023.36, 3106.33, 3173.46, 3234.81, 3299.41, 3407.85, 
   3516.38, 3595.37, 3663.73, 3765.22, 3890.44, 3975.27, 4035.25, 4098.29, 
   4174.55
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.26139        start      
        1            56       5.456e-004      2.05499     
        2            57       1.029e-006      0.00277     
        3            58       2.489e-010     3.979e-006   
        4            59       7.047e-012     7.828e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2040.62, 2149.12, 2221.56, 2336.16, 2470.97, 2573.35, 2672.73, 2751.44, 
   2845.95, 2937.43, 3019.14, 3102.08, 3169.21, 3230.59, 3295.22, 3403.75, 
   3512.40, 3591.51, 3659.99, 3761.70, 3887.24, 3972.32, 4032.52, 4095.82, 
   4172.46
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.92723        start      
        1            56       6.158e-004      2.25385     
        2            57       1.258e-006      0.00331     
        3            58       3.231e-010     5.286e-006   
        4            59       9.125e-012     1.154e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2038.76, 2146.92, 2219.17, 2333.49, 2468.01, 2570.20, 2669.42, 2748.03, 
   2842.44, 2933.83, 3015.49, 3098.41, 3165.53, 3226.90, 3291.56, 3400.15, 
   3508.90, 3588.09, 3656.67, 3758.57, 3884.38, 3969.68, 4030.07, 4093.60, 
   4170.58
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.67183        start      
        1            56       7.401e-004      2.53589     
        2            57       1.677e-006      0.00416     
        3            58       4.920e-010     7.533e-006   
        4            59       7.228e-012     1.979e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2037.16, 2145.03, 2217.11, 2331.18, 2465.44, 2567.46, 2666.53, 2745.04, 
   2839.34, 2930.66, 3012.26, 3095.14, 3162.24, 3223.62, 3288.28, 3396.92, 
   3505.75, 3585.01, 3653.68, 3755.73, 3881.78, 3967.28, 4027.83, 4091.58, 
   4168.86
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.41945        start      
        1            56       5.679e-004      2.28491     
        2            57       1.111e-006      0.00318     
        3            58       2.722e-010     5.022e-006   
        4            59       7.667e-012     1.057e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2035.76, 2143.37, 2215.29, 2329.14, 2463.16, 2565.02, 2663.94, 2742.36, 
   2836.56, 2927.80, 3009.35, 3092.19, 3159.27, 3220.64, 3285.31, 3393.99, 
   3502.87, 3582.21, 3650.95, 3753.14, 3879.40, 3965.08, 4025.78, 4089.72, 
   4167.28
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.22575        start      
        1            56       7.093e-004      2.61315     
        2            57       1.559e-006      0.00412     
        3            58       4.611e-010     7.415e-006   
        4            59       1.103e-011     1.986e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2034.51, 2141.89, 2213.66, 2327.31, 2461.10, 2562.81, 2661.60, 2739.92, 
   2834.03, 2925.19, 3006.69, 3089.49, 3156.55, 3217.91, 3282.59, 3391.29, 
   3500.23, 3579.62, 3648.42, 3750.75, 3877.21, 3963.05, 4023.88, 4087.99, 
   4165.81
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/28/2026 11:08:21 AM
   7/28/2026 11:09:42 AM
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
        0            1          670.834        start      
        1            56         0.00507       0.49698     
        2            57       1.567e-006     1.324e-004   
        3            58       9.799e-010     9.151e-008   
        4            59       3.007e-010     3.069e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1663.91, 1696.99, 1719.22, 1754.60, 1796.46, 1828.46, 1859.71, 1884.61, 
   1914.72, 1944.06, 1970.47, 1997.45, 2019.45, 2039.71, 2061.21, 2097.62, 
   2134.39, 2165.66, 2314.53, 2619.98, 2983.85, 3222.70, 3387.49, 3555.60, 
   3751.19
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.24, 0.58, 0.72, 0.75, 0.77, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          579.999        start      
        1            56         0.00742       0.28347     
        2            57       1.655e-005     2.226e-004   
        3            58       1.771e-009     4.980e-007   
        4            59       1.985e-010     6.588e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1627.40, 1653.11, 1670.40, 1697.91, 1730.49, 1755.39, 1779.73, 1799.13, 
   1822.61, 1845.49, 1866.10, 1887.18, 1904.52, 1940.79, 2086.11, 2328.32, 
   2564.20, 2733.08, 2877.31, 3088.80, 3346.92, 3519.57, 3640.09, 3764.59, 
   3911.73
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.39, 0.68, 0.73, 
   0.75, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          463.171        start      
        1            56         0.00390       0.47352     
        2            57       9.358e-006     1.483e-004   
        3            58       4.129e-009     2.639e-007   
        4            59       2.152e-010     1.436e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1610.58, 1632.90, 1647.90, 1671.78, 1700.06, 1721.67, 1742.80, 1759.65, 
   1780.06, 1810.62, 1958.14, 2115.25, 2239.15, 2350.57, 2466.72, 2660.24, 
   2852.66, 2991.92, 3111.76, 3288.54, 3505.27, 3650.92, 3753.03, 3859.10, 
   3985.38
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.30, 0.65, 0.73, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          435.913        start      
        1            56         0.00489       0.39998     
        2            57       1.085e-005     2.058e-004   
        3            58       1.614e-009     4.360e-007   
        4            59       2.068e-010     7.958e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1598.30, 1618.14, 1631.47, 1652.70, 1677.83, 1697.16, 1736.78, 1865.03, 
   2019.96, 2166.26, 2294.92, 2424.42, 2528.62, 2623.42, 2722.87, 2889.25, 
   3055.20, 3175.59, 3279.40, 3432.87, 3621.40, 3748.38, 3837.62, 3930.61, 
   4041.78
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.37, 0.67, 
   0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          380.233        start      
        1            56         0.00481       0.56406     
        2            57       1.281e-005     2.403e-004   
        3            58       9.919e-010     6.523e-007   
        4            59       1.804e-010     8.682e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1586.17, 1603.56, 1615.31, 1651.52, 1840.02, 1984.64, 2121.62, 2228.69, 
   2356.06, 2478.61, 2587.60, 2697.91, 2786.98, 2868.23, 2953.64, 3096.79, 
   3239.81, 3343.70, 3433.42, 3566.28, 3729.73, 3840.01, 3917.66, 3998.76, 
   4096.01
   
   Saturation:
   0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 0.75, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          306.493        start      
        1            56         0.00355       1.29147     
        2            57       5.150e-006     3.677e-004   
        3            58       1.427e-009     9.382e-007   
        4            59       1.377e-010     4.027e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1600.00, 1713.07, 1804.58, 1946.00, 2109.99, 2233.54, 2352.85, 2447.08, 
   2559.95, 2669.05, 2766.41, 2865.17, 2945.07, 3018.07, 3094.91, 3223.87, 
   3352.87, 3446.69, 3527.80, 3648.06, 3796.18, 3896.27, 3966.82, 4040.65, 
   4129.37
   
   Saturation:
   0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          21.3426        start      
        1            56       9.742e-004      0.92442     
        2            57       1.173e-006     9.993e-004   
        3            58       9.883e-011     7.969e-007   
        4            59       1.560e-011     3.872e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2075.43, 2188.23, 2262.35, 2378.57, 2514.41, 2617.12, 2716.45, 2794.93, 
   2888.92, 2979.73, 3060.70, 3142.78, 3209.13, 3269.71, 3333.44, 3440.33, 
   3547.22, 3624.93, 3692.10, 3791.69, 3914.38, 3997.31, 4055.81, 4117.07, 
   4190.78
   
   Saturation:
   0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          7.48047        start      
        1            56       7.993e-004      1.70306     
        2            57       1.340e-006      0.00202     
        3            58       4.452e-010     2.150e-006   
        4            59       8.056e-012     5.740e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2055.64, 2166.35, 2239.86, 2355.72, 2491.61, 2594.60, 2694.37, 2773.29, 
   2867.92, 2959.42, 3041.07, 3123.88, 3190.86, 3252.05, 3316.44, 3424.49, 
   3532.57, 3611.18, 3679.16, 3780.00, 3904.28, 3988.34, 4047.68, 4109.86, 
   4184.79
   
   Saturation:
   0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.22092        start      
        1            56       7.429e-004      1.92418     
        2            57       1.461e-006      0.00277     
        3            58       5.036e-010     3.761e-006   
        4            59       8.289e-012     1.099e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2048.28, 2158.00, 2231.08, 2346.50, 2482.08, 2584.94, 2684.68, 2763.64, 
   2858.37, 2950.01, 3031.83, 3114.85, 3182.02, 3243.40, 3308.01, 3416.47, 
   3525.00, 3603.96, 3672.27, 3773.64, 3898.62, 3983.21, 4042.95, 4105.61, 
   4181.20
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.26001        start      
        1            56       5.333e-004      1.75190     
        2            57       9.418e-007      0.00226     
        3            58       2.418e-010     2.881e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2044.23, 2153.32, 2226.09, 2341.13, 2476.38, 2579.05, 2678.67, 2757.57, 
   2852.27, 2943.92, 3025.77, 3108.85, 3176.08, 3237.54, 3302.26, 3410.91, 
   3519.66, 3598.82, 3667.31, 3769.00, 3894.42, 3979.34, 4039.35, 4102.35, 
   4178.42
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.69956        start      
        1            56       7.816e-004      2.21503     
        2            57       1.805e-006      0.00385     
        3            58       5.547e-010     6.807e-006   
        4            59       5.318e-012     1.869e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2041.48, 2150.12, 2222.65, 2337.37, 2472.31, 2574.79, 2674.27, 2753.08, 
   2847.71, 2939.31, 3021.15, 3104.23, 3171.48, 3232.97, 3297.74, 3406.49, 
   3515.38, 3594.66, 3663.28, 3765.19, 3890.93, 3976.11, 4036.34, 4099.60, 
   4176.06
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.24778        start      
        1            56       5.165e-004      1.87114     
        2            57       9.563e-007      0.00255     
        3            58       2.043e-010     3.702e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2039.40, 2147.68, 2220.00, 2334.44, 2469.10, 2571.41, 2670.74, 2749.46, 
   2843.99, 2935.53, 3017.32, 3100.39, 3167.63, 3229.13, 3293.92, 3402.74, 
   3511.72, 3591.08, 3659.80, 3761.88, 3887.88, 3973.27, 4033.68, 4097.18, 
   4173.97
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.96754        start      
        1            56       6.127e-004      2.09217     
        2            57       1.259e-006      0.00319     
        3            58       2.925e-010     5.284e-006   
        4            59       9.026e-012     1.071e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2037.71, 2145.68, 2217.83, 2332.03, 2466.43, 2568.57, 2667.76, 2746.38, 
   2840.81, 2932.28, 3014.03, 3097.05, 3164.28, 3225.78, 3290.58, 3399.44, 
   3508.49, 3587.91, 3656.71, 3758.93, 3885.15, 3970.73, 4031.30, 4094.99, 
   4172.09
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.67430        start      
        1            56       7.100e-004      2.32220     
        2            57       1.582e-006      0.00386     
        3            58       4.027e-010     7.105e-006   
        4            59       7.075e-012     1.651e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2036.27, 2143.98, 2215.97, 2329.94, 2464.11, 2566.09, 2665.14, 2743.67, 
   2838.01, 2929.40, 3011.09, 3094.08, 3161.29, 3222.78, 3287.58, 3396.47, 
   3505.56, 3585.04, 3653.90, 3756.25, 3882.66, 3968.40, 4029.11, 4092.99, 
   4170.36
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.47176        start      
        1            56       5.754e-004      2.14094     
        2            57       1.137e-006      0.00312     
        3            58       2.481e-010     5.132e-006   
        4            59       6.286e-012     9.905e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2035.00, 2142.48, 2214.33, 2328.10, 2462.05, 2563.88, 2662.80, 2741.24, 
   2835.49, 2926.80, 3008.43, 3091.38, 3158.56, 3220.04, 3284.84, 3393.74, 
   3502.88, 3582.40, 3651.32, 3753.78, 3880.37, 3966.26, 4027.09, 4091.14, 
   4168.77
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.28897        start      
        1            56       7.315e-004      2.47013     
        2            57       1.636e-006      0.00411     
        3            58       4.344e-010     7.775e-006   
        4            59       6.549e-012     1.926e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2033.86, 2141.13, 2212.85, 2326.43, 2460.18, 2561.87, 2660.67, 2739.02, 
   2833.17, 2924.41, 3005.99, 3088.89, 3156.05, 3217.52, 3282.31, 3391.23, 
   3500.40, 3579.96, 3648.92, 3751.48, 3878.23, 3964.26, 4025.22, 4089.42, 
   4167.28
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.13724        start      
        1            56       6.155e-004      2.31395     
        2            57       1.246e-006      0.00344     
        3            58       2.978e-010     5.932e-006   
        4            59       8.010e-012     1.288e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2032.83, 2139.90, 2211.50, 2324.91, 2458.46, 2560.02, 2658.70, 2736.97, 
   2831.04, 2922.20, 3003.73, 3086.59, 3153.72, 3215.17, 3279.96, 3388.88, 
   3498.08, 3577.68, 3646.69, 3749.34, 3876.24, 3962.40, 4023.47, 4087.81, 
   4165.89
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.00284        start      
        1            56       5.242e-004      2.17827     
        2            57       9.674e-007      0.00292     
        3            58       2.108e-010     4.608e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2031.87, 2138.77, 2210.26, 2323.49, 2456.87, 2558.30, 2656.88, 2735.06, 
   2829.05, 2920.14, 3001.62, 3084.43, 3151.54, 3212.97, 3277.75, 3386.68, 
   3495.91, 3575.54, 3644.59, 3747.33, 3874.37, 3960.64, 4021.82, 4086.30, 
   4164.59
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.86184        start      
        1            56       6.807e-004      2.53730     
        2            57       1.428e-006      0.00391     
        3            58       3.952e-010     7.088e-006   
        4            59       7.023e-012     1.826e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2030.98, 2137.71, 2209.10, 2322.18, 2455.38, 2556.70, 2655.17, 2733.28, 
   2827.18, 2918.21, 2999.63, 3082.41, 3149.49, 3210.91, 3275.68, 3384.62, 
   3493.86, 3573.52, 3642.61, 3745.44, 3872.60, 3959.00, 4020.27, 4084.88, 
   4163.36
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          1.76229        start      
        1            56       5.967e-004      2.41542     
        2            57       1.160e-006      0.00341     
        3            58       3.010e-010     5.757e-006   
        4            59       7.407e-012     1.368e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   2030.15, 2136.72, 2208.01, 2320.94, 2453.99, 2555.19, 2653.56, 2731.59, 
   2825.42, 2916.38, 2997.76, 3080.50, 3147.55, 3208.96, 3273.73, 3382.67, 
   3491.93, 3571.62, 3640.75, 3743.65, 3870.94, 3957.44, 4018.80, 4083.54, 
   4162.20
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/28/2026 11:11:19 AM
   7/28/2026 11:13:29 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

