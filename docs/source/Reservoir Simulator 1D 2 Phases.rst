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
        0            1          193.596        start      
        1            56         0.00217       5.62686     
        2            57       1.415e-006     6.552e-004   
        3            58       4.138e-009     1.638e-007   
        4            59       7.610e-011     1.956e-010   
   Producer BHP: 
   2272.01 psi
   
   Injector BHP: 
   2649.84 psi
   
   Pressure: 
   2300.66, 2307.18, 2313.15, 2319.21, 2325.92, 2332.44, 2339.65, 2348.00, 
   2359.93, 2370.21, 2376.02, 2382.88, 2390.24, 2398.13, 2405.53, 2411.78, 
   2422.36, 2432.32, 2446.30, 2460.05, 2465.86, 2471.15, 2476.18, 2485.73, 
   2520.52
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.37, 
   0.69
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          183.375        start      
        1            56         0.00435       7.51832     
        2            57       7.024e-006     9.138e-004   
        3            58       3.118e-008     3.596e-007   
        4            59       7.227e-011     2.233e-009   
   Producer BHP: 
   1622.42 psi
   
   Injector BHP: 
   2038.18 psi
   
   Pressure: 
   1651.13, 1657.66, 1663.63, 1669.70, 1676.40, 1682.91, 1690.10, 1698.43, 
   1710.31, 1720.55, 1726.32, 1733.15, 1740.46, 1748.28, 1755.61, 1761.80, 
   1772.27, 1782.10, 1795.91, 1809.47, 1815.19, 1820.56, 1837.62, 1876.34, 
   1908.42
   
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
        0            1          162.116        start      
        1            56         0.00389       0.28508     
        2            57       4.694e-006     1.908e-004   
        3            58       4.643e-009     5.222e-007   
        4            59       5.216e-011     2.650e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1921.67 psi
   
   Pressure: 
   1520.71, 1525.48, 1529.89, 1534.42, 1539.48, 1544.45, 1550.02, 1556.56, 
   1566.00, 1574.23, 1578.93, 1584.56, 1590.68, 1597.31, 1603.62, 1609.01, 
   1618.28, 1627.12, 1639.70, 1652.24, 1658.20, 1685.36, 1726.47, 1762.07, 
   1791.81
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.55, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.587        start      
        1            56         0.00298       0.41095     
        2            57       9.548e-006     1.672e-004   
        3            58       6.054e-009     4.276e-007   
        4            59       4.564e-011     3.062e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   1967.78 psi
   
   Pressure: 
   1520.69, 1525.45, 1529.86, 1534.38, 1539.44, 1544.40, 1549.96, 1556.49, 
   1565.92, 1574.14, 1578.84, 1584.46, 1590.57, 1597.19, 1603.49, 1608.88, 
   1618.14, 1626.97, 1639.55, 1655.71, 1694.63, 1737.10, 1775.72, 1809.39, 
   1837.93
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.26, 0.61, 0.72, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          159.062        start      
        1            56         0.00445       0.63026     
        2            57       2.171e-005     3.947e-004   
        3            58       1.806e-008     1.593e-006   
        4            59       3.716e-011     1.850e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2063.86 psi
   
   Pressure: 
   1520.63, 1525.38, 1529.77, 1534.28, 1539.33, 1544.28, 1549.83, 1556.33, 
   1565.74, 1573.94, 1578.62, 1584.23, 1590.32, 1596.93, 1603.21, 1608.59, 
   1617.82, 1626.65, 1647.69, 1751.27, 1796.93, 1837.03, 1873.88, 1906.30, 
   1934.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.32, 0.65, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.865        start      
        1            56         0.00700       0.44494     
        2            57       3.348e-005     5.232e-004   
        3            58       6.162e-009     2.793e-006   
        4            59       4.768e-011     3.741e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2161.81 psi
   
   Pressure: 
   1520.67, 1525.43, 1529.83, 1534.35, 1539.40, 1544.37, 1549.92, 1556.44, 
   1565.86, 1574.07, 1578.76, 1584.38, 1590.48, 1597.10, 1603.39, 1608.78, 
   1618.13, 1640.14, 1749.33, 1855.43, 1898.93, 1937.54, 1973.26, 2004.84, 
   2032.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.41, 0.69, 0.73, 0.76, 0.77, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          174.694        start      
        1            56         0.00758       0.62335     
        2            57       2.883e-005     7.349e-004   
        3            58       4.254e-009     3.405e-006   
        4            59       4.348e-011     3.445e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2227.44 psi
   
   Pressure: 
   1520.63, 1525.37, 1529.77, 1534.27, 1539.31, 1544.26, 1549.80, 1556.31, 
   1565.70, 1573.90, 1578.57, 1584.18, 1590.27, 1596.87, 1603.14, 1608.70, 
   1642.33, 1719.04, 1824.29, 1925.57, 1967.57, 2005.10, 2039.97, 2070.90, 
   2097.69
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 
   0.48, 0.70, 0.74, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          158.585        start      
        1            56         0.00607       0.49162     
        2            57       1.337e-005     3.139e-004   
        3            58       9.536e-009     1.321e-006   
        4            59       4.886e-011     5.521e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2284.74 psi
   
   Pressure: 
   1520.65, 1525.40, 1529.80, 1534.30, 1539.35, 1544.31, 1549.85, 1556.36, 
   1565.77, 1573.97, 1578.65, 1584.26, 1590.36, 1596.96, 1603.85, 1632.37, 
   1712.85, 1786.39, 1887.73, 1986.08, 2027.08, 2063.84, 2098.08, 2128.54, 
   2155.02
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.55, 
   0.71, 0.74, 0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          148.719        start      
        1            56         0.00328       0.58368     
        2            57       4.487e-006     8.979e-005   
        3            58       7.675e-009     1.399e-007   
        4            59       4.392e-011     3.221e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2325.53 psi
   
   Pressure: 
   1520.61, 1525.35, 1529.74, 1534.24, 1539.28, 1544.22, 1549.76, 1556.26, 
   1565.64, 1573.83, 1578.51, 1584.11, 1590.19, 1598.25, 1640.56, 1686.98, 
   1763.83, 1834.79, 1933.40, 2029.50, 2069.70, 2105.81, 2139.53, 2169.60, 
   2195.83
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 0.72, 
   0.74, 0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          150.797        start      
        1            56         0.00323       0.48354     
        2            57       1.502e-005     1.951e-004   
        3            58       2.976e-008     4.808e-007   
        4            59       6.680e-011     1.222e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2372.78 psi
   
   Pressure: 
   1520.63, 1525.38, 1529.77, 1534.27, 1539.31, 1544.26, 1549.80, 1556.31, 
   1565.70, 1573.90, 1578.57, 1584.19, 1593.23, 1645.23, 1699.30, 1743.86, 
   1818.36, 1887.68, 1984.38, 2078.84, 2118.41, 2154.03, 2187.33, 2217.08, 
   2243.10
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.30, 0.64, 0.72, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          166.264        start      
        1            56         0.00526       0.31386     
        2            57       2.615e-005     2.245e-004   
        3            58       6.377e-009     1.265e-006   
        4            59       3.616e-011     5.803e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2415.79 psi
   
   Pressure: 
   1520.68, 1525.44, 1529.83, 1534.35, 1539.40, 1544.36, 1549.91, 1556.43, 
   1565.84, 1574.06, 1578.77, 1590.11, 1641.95, 1698.60, 1750.63, 1794.01, 
   1866.98, 1935.13, 2030.39, 2123.55, 2162.63, 2197.84, 2230.80, 2260.29, 
   2286.13
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.36, 0.67, 0.73, 0.75, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          172.762        start      
        1            56         0.00686       0.30286     
        2            57       3.260e-005     3.239e-004   
        3            58       1.050e-008     1.878e-006   
        4            59       4.642e-011     5.030e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2451.46 psi
   
   Pressure: 
   1520.68, 1525.44, 1529.84, 1534.35, 1539.40, 1544.36, 1549.91, 1556.43, 
   1565.84, 1574.23, 1587.66, 1636.34, 1688.15, 1742.72, 1793.37, 1835.89, 
   1907.62, 1974.76, 2068.73, 2160.73, 2199.37, 2234.21, 2266.87, 2296.13, 
   2321.82
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.43, 0.69, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          160.220        start      
        1            56         0.00757       0.92924     
        2            57       2.380e-005     6.704e-004   
        3            58       1.034e-008     2.994e-006   
        4            59       5.332e-011     8.749e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2493.29 psi
   
   Pressure: 
   1520.48, 1525.19, 1529.54, 1534.01, 1539.01, 1543.92, 1549.42, 1555.87, 
   1565.81, 1603.73, 1644.14, 1691.21, 1740.91, 1793.76, 1843.12, 1884.71, 
   1955.06, 2021.03, 2113.52, 2204.20, 2242.33, 2276.78, 2309.11, 2338.13, 
   2363.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.22, 0.52, 0.70, 0.74, 0.75, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          145.052        start      
        1            56         0.00366       1.15313     
        2            57       4.794e-006     1.313e-004   
        3            58       9.798e-009     1.620e-007   
        4            59       4.251e-011     4.117e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2567.62 psi
   
   Pressure: 
   1520.39, 1525.08, 1529.42, 1533.87, 1538.85, 1543.74, 1549.21, 1556.97, 
   1620.34, 1690.70, 1729.63, 1775.10, 1823.54, 1875.29, 1923.76, 1964.68, 
   2033.98, 2099.05, 2190.35, 2279.95, 2317.67, 2351.78, 2383.83, 2412.64, 
   2438.04
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 
   0.60, 0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          151.537        start      
        1            56         0.00308       0.56071     
        2            57       1.395e-005     1.980e-004   
        3            58       3.322e-008     4.198e-007   
        4            59       6.023e-011     1.279e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2624.12 psi
   
   Pressure: 
   1520.58, 1525.32, 1529.69, 1534.18, 1539.21, 1544.15, 1552.29, 1603.82, 
   1684.58, 1752.83, 1790.96, 1835.84, 1883.81, 1935.15, 1983.28, 2023.94, 
   2092.81, 2157.48, 2248.23, 2337.30, 2374.80, 2408.72, 2440.60, 2469.26, 
   2494.56
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 0.64, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          162.530        start      
        1            56         0.00449       0.35494     
        2            57       2.190e-005     1.694e-004   
        3            58       1.644e-008     9.761e-007   
        4            59       5.555e-011     1.160e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2660.87 psi
   
   Pressure: 
   1520.65, 1525.40, 1529.79, 1534.29, 1539.36, 1548.63, 1595.16, 1650.85, 
   1728.92, 1795.65, 1833.15, 1877.46, 1924.92, 1975.76, 2023.47, 2063.79, 
   2132.12, 2196.32, 2286.43, 2374.90, 2412.17, 2445.88, 2477.59, 2506.12, 
   2531.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.35, 0.66, 0.73, 
   0.75, 0.75, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          169.071        start      
        1            56         0.00674       0.36508     
        2            57       3.348e-005     3.619e-004   
        3            58       3.062e-008     2.355e-006   
        4            59       4.305e-011     1.907e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2692.28 psi
   
   Pressure: 
   1520.65, 1525.39, 1529.78, 1534.35, 1547.61, 1590.21, 1637.48, 1691.31, 
   1767.59, 1833.17, 1870.14, 1913.89, 1960.83, 2011.15, 2058.41, 2098.38, 
   2166.14, 2229.83, 2319.29, 2407.14, 2444.16, 2477.68, 2509.23, 2537.63, 
   2562.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.21, 0.42, 0.68, 0.73, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          163.569        start      
        1            56         0.00764       0.49382     
        2            57       3.014e-005     4.706e-004   
        3            58       1.333e-008     2.871e-006   
        4            59       4.489e-011     1.386e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2725.60 psi
   
   Pressure: 
   1520.60, 1525.33, 1529.98, 1548.95, 1592.75, 1634.68, 1680.38, 1732.97, 
   1807.91, 1872.51, 1908.98, 1952.22, 1998.64, 2048.45, 2095.26, 2134.86, 
   2202.06, 2265.24, 2354.03, 2441.27, 2478.05, 2511.38, 2542.76, 2571.04, 
   2596.09
   
   Saturation:
   0.20, 0.20, 0.22, 0.50, 0.70, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          142.840        start      
        1            56         0.00335       0.59999     
        2            57       9.532e-006     2.206e-004   
        3            58       1.558e-008     3.995e-007   
        4            59       4.429e-011     6.121e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2760.64 psi
   
   Pressure: 
   1520.56, 1526.46, 1557.39, 1596.36, 1638.71, 1679.33, 1724.02, 1775.71, 
   1849.56, 1913.31, 1949.35, 1992.11, 2038.04, 2087.36, 2133.73, 2172.99, 
   2239.62, 2302.32, 2390.45, 2477.09, 2513.65, 2546.78, 2578.01, 2606.17, 
   2631.15
   
   Saturation:
   0.20, 0.26, 0.61, 0.71, 0.74, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          153.826        start      
        1            56         0.00319       0.84289     
        2            57       1.257e-005     4.398e-004   
        3            58       2.148e-008     2.157e-006   
        4            59       3.848e-011     2.985e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2797.67 psi
   
   Pressure: 
   1531.70, 1569.58, 1606.93, 1644.35, 1685.43, 1725.14, 1769.04, 1819.96, 
   1892.82, 1955.79, 1991.42, 2033.72, 2079.19, 2128.05, 2174.01, 2212.93, 
   2279.04, 2341.26, 2428.78, 2514.86, 2551.20, 2584.16, 2615.24, 2643.29, 
   2668.19
   
   Saturation:
   0.31, 0.65, 0.73, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   7/26/2026 7:05:44 AM
   7/26/2026 7:07:08 AM
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
        0            1          243.567        start      
        1            56         0.00333       0.26412     
        2            57       4.900e-006     7.751e-005   
        3            58       4.970e-009     1.442e-007   
        4            59       8.353e-011     2.212e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2043.73 psi
   
   Pressure: 
   1531.12, 1538.29, 1544.92, 1551.72, 1559.34, 1566.81, 1575.18, 1585.00, 
   1599.19, 1611.57, 1618.64, 1627.10, 1636.30, 1646.27, 1655.74, 1663.86, 
   1677.79, 1691.07, 1709.98, 1728.84, 1736.91, 1744.37, 1753.17, 1799.02, 
   1849.00
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 0.60, 
   0.74
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          245.960        start      
        1            56         0.00582       0.41343     
        2            57       7.355e-006     2.653e-004   
        3            58       7.223e-009     6.469e-007   
        4            59       7.275e-011     3.736e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2131.55 psi
   
   Pressure: 
   1531.06, 1538.21, 1544.83, 1551.62, 1559.22, 1566.68, 1575.03, 1584.83, 
   1598.99, 1611.34, 1618.39, 1626.84, 1636.01, 1645.96, 1655.41, 1663.51, 
   1677.41, 1690.66, 1709.53, 1728.34, 1737.20, 1777.55, 1839.25, 1892.46, 
   1936.82
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.23, 0.55, 0.72, 0.76, 
   0.79
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          263.978        start      
        1            56         0.00753       0.88910     
        2            57       1.833e-005     7.487e-004   
        3            58       6.635e-009     1.951e-006   
        4            59       9.558e-011     6.128e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2248.23 psi
   
   Pressure: 
   1530.87, 1537.98, 1544.55, 1551.30, 1558.85, 1566.26, 1574.56, 1584.29, 
   1598.36, 1610.63, 1617.63, 1626.02, 1635.13, 1645.01, 1654.40, 1662.43, 
   1676.24, 1689.39, 1708.70, 1774.87, 1844.62, 1906.14, 1962.34, 2011.58, 
   2053.57
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.21, 0.47, 0.70, 0.74, 0.77, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          261.986        start      
        1            56         0.00625       0.50049     
        2            57       2.203e-005     4.134e-004   
        3            58       3.096e-009     1.498e-006   
        4            59       7.842e-011     2.169e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2489.86 psi
   
   Pressure: 
   1530.96, 1538.09, 1544.68, 1551.45, 1559.02, 1566.46, 1574.78, 1584.54, 
   1598.66, 1610.96, 1617.99, 1626.41, 1635.55, 1645.46, 1654.88, 1662.95, 
   1676.94, 1708.85, 1872.53, 2031.38, 2096.44, 2154.16, 2207.55, 2254.73, 
   2295.38
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.40, 0.68, 0.74, 0.76, 0.77, 0.78, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          234.238        start      
        1            56         0.00332       0.54576     
        2            57       1.215e-005     1.641e-004   
        3            58       1.848e-008     3.634e-007   
        4            59       7.841e-011     6.462e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2645.69 psi
   
   Pressure: 
   1530.89, 1538.00, 1544.58, 1551.33, 1558.88, 1566.30, 1574.61, 1584.35, 
   1598.43, 1610.71, 1617.72, 1626.13, 1635.25, 1645.15, 1654.57, 1667.34, 
   1780.68, 1893.46, 2047.65, 2196.68, 2258.65, 2314.10, 2365.69, 2411.53, 
   2451.32
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 
   0.65, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          224.305        start      
        1            56         0.00331       0.68292     
        2            57       1.885e-006     1.025e-004   
        3            58       1.631e-009     9.036e-008   
        4            59       9.362e-011     6.887e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2732.70 psi
   
   Pressure: 
   1530.81, 1537.90, 1544.46, 1551.19, 1558.72, 1566.12, 1574.40, 1584.11, 
   1598.15, 1610.39, 1617.38, 1625.76, 1634.85, 1646.63, 1708.60, 1778.11, 
   1893.02, 1999.06, 2146.38, 2289.93, 2349.96, 2403.90, 2454.27, 2499.19, 
   2538.39
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.25, 0.59, 0.72, 
   0.74, 0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          247.490        start      
        1            56         0.00685       0.63704     
        2            57       1.759e-005     4.091e-004   
        3            58       6.045e-009     1.369e-006   
        4            59       9.224e-011     3.587e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2831.42 psi
   
   Pressure: 
   1530.82, 1537.91, 1544.47, 1551.20, 1558.74, 1566.13, 1574.41, 1584.12, 
   1598.16, 1610.40, 1617.39, 1626.24, 1666.68, 1752.82, 1831.88, 1897.43, 
   2007.40, 2109.96, 2253.18, 2393.16, 2451.86, 2504.72, 2554.19, 2598.43, 
   2637.18
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.22, 0.51, 0.70, 0.74, 0.75, 
   0.76, 0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          259.685        start      
        1            56         0.00605       0.32914     
        2            57       2.203e-005     2.462e-004   
        3            58       1.668e-009     9.981e-007   
        4            59       1.091e-010     6.515e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2922.40 psi
   
   Pressure: 
   1530.97, 1538.09, 1544.69, 1551.45, 1559.02, 1566.45, 1574.77, 1584.53, 
   1598.63, 1611.13, 1629.95, 1703.03, 1780.69, 1862.40, 1938.22, 2001.83, 
   2109.12, 2209.52, 2350.00, 2487.50, 2545.24, 2597.31, 2646.11, 2689.83, 
   2728.23
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.21, 0.42, 0.69, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.79, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          244.104        start      
        1            56         0.00493       0.74677     
        2            57       1.796e-005     3.066e-004   
        3            58       2.906e-009     1.093e-006   
        4            59       9.496e-011     2.202e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3036.53 psi
   
   Pressure: 
   1530.67, 1537.73, 1544.26, 1550.96, 1558.46, 1565.82, 1574.06, 1583.77, 
   1610.69, 1714.50, 1774.31, 1843.64, 1917.16, 1995.51, 2068.76, 2130.52, 
   2235.00, 2333.01, 2470.42, 2605.17, 2661.85, 2713.07, 2761.18, 2804.38, 
   2842.44
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.35, 0.67, 0.73, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          222.279        start      
        1            56         0.00252       0.73416     
        2            57       7.701e-006     1.325e-004   
        3            58       1.007e-008     2.109e-007   
        4            59       8.470e-011     3.067e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3174.69 psi
   
   Pressure: 
   1530.67, 1537.72, 1544.25, 1550.94, 1558.44, 1565.80, 1577.20, 1652.35, 
   1772.97, 1874.77, 1931.63, 1998.53, 2070.03, 2146.54, 2218.28, 2278.88, 
   2381.53, 2477.95, 2613.26, 2746.06, 2801.98, 2852.57, 2900.15, 2942.93, 
   2980.71
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.28, 0.63, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          234.227        start      
        1            56         0.00493       0.62501     
        2            57       7.392e-006     2.150e-004   
        3            58       4.367e-009     6.258e-007   
        4            59       6.205e-011     2.071e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3252.35 psi
   
   Pressure: 
   1530.72, 1537.79, 1544.32, 1551.03, 1559.36, 1599.44, 1671.36, 1752.84, 
   1867.70, 1966.20, 2021.65, 2087.24, 2157.55, 2232.91, 2303.68, 2363.51, 
   2464.95, 2560.28, 2694.16, 2825.62, 2881.02, 2931.16, 2978.36, 3020.84, 
   3058.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.23, 0.55, 0.71, 0.74, 
   0.75, 0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          251.757        start      
        1            56         0.00730       0.49873     
        2            57       2.478e-005     4.023e-004   
        3            58       9.744e-009     1.894e-006   
        4            59       8.560e-011     7.518e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3328.27 psi
   
   Pressure: 
   1530.79, 1537.87, 1544.70, 1569.69, 1635.35, 1698.20, 1766.61, 1845.28, 
   1957.33, 2053.89, 2108.40, 2173.00, 2242.35, 2316.75, 2386.66, 2445.81, 
   2546.15, 2640.49, 2773.04, 2903.27, 2958.18, 3007.92, 3054.78, 3097.00, 
   3134.41
   
   Saturation:
   0.20, 0.20, 0.21, 0.48, 0.70, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          262.410        start      
        1            56         0.00667       0.45369     
        2            57       2.411e-005     4.552e-004   
        3            58       2.263e-008     2.340e-006   
        4            59       1.052e-010     2.138e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3402.22 psi
   
   Pressure: 
   1531.38, 1552.54, 1609.56, 1667.06, 1729.72, 1790.04, 1856.55, 1933.56, 
   2043.61, 2138.63, 2192.35, 2256.09, 2324.56, 2398.07, 2467.19, 2525.70, 
   2625.00, 2718.42, 2849.73, 2978.82, 3033.28, 3082.65, 3129.19, 3171.17, 
   3208.42
   
   Saturation:
   0.21, 0.44, 0.69, 0.73, 0.75, 0.75, 0.76, 0.76, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          106.932        start      
        1            56         0.00387       3.93688     
        2            57       3.657e-006      0.00245     
        3            58       2.701e-009     6.559e-007   
        4            59       3.550e-011     7.113e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3593.65 psi
   
   Pressure: 
   1746.07, 1804.58, 1857.06, 1910.09, 1968.66, 2025.52, 2088.55, 2161.84, 
   2266.89, 2357.83, 2409.36, 2470.64, 2536.63, 2607.63, 2674.53, 2731.30, 
   2827.86, 2918.91, 3047.18, 3173.57, 3227.02, 3275.60, 3321.50, 3363.03, 
   3399.99
   
   Saturation:
   0.65, 0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 0.77, 
   0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          11.9077        start      
        1            56       8.455e-004      2.07147     
        2            57       1.231e-006      0.00102     
        3            58       3.078e-010     5.032e-007   
        4            59       1.101e-011     1.986e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3638.68 psi
   
   Pressure: 
   1762.63, 1821.59, 1875.23, 1929.67, 1989.97, 2048.55, 2113.49, 2188.93, 
   2296.93, 2390.29, 2443.11, 2505.82, 2573.21, 2645.59, 2713.66, 2771.30, 
   2869.15, 2961.23, 3090.71, 3218.04, 3271.79, 3320.55, 3366.56, 3408.11, 
   3445.05
   
   Saturation:
   0.74, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          6.11831        start      
        1            56       7.903e-004      3.92691     
        2            57       1.537e-006      0.00222     
        3            58       8.777e-010     1.119e-006   
        4            59       7.070e-012     1.191e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3613.88 psi
   
   Pressure: 
   1753.29, 1810.81, 1863.42, 1916.95, 1976.35, 2034.14, 2098.29, 2172.87, 
   2279.72, 2372.14, 2424.46, 2486.60, 2553.41, 2625.21, 2692.75, 2749.98, 
   2847.16, 2938.65, 3067.37, 3194.02, 3247.52, 3296.07, 3341.92, 3383.35, 
   3420.23
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.48283        start      
        1            56       5.113e-004      4.56838     
        2            57       9.776e-007      0.00244     
        3            58       5.934e-010     1.207e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3596.38 psi
   
   Pressure: 
   1748.30, 1804.91, 1856.80, 1909.65, 1968.36, 2025.54, 2089.05, 2162.93, 
   2268.83, 2360.46, 2412.35, 2474.01, 2540.33, 2611.62, 2678.71, 2735.57, 
   2832.16, 2923.15, 3051.20, 3177.24, 3230.50, 3278.87, 3324.56, 3365.89, 
   3402.72
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.69497        start      
        1            56       5.176e-004      5.88227     
        2            57       1.204e-006      0.00373     
        3            58       8.711e-010     2.307e-006   
        4            59       5.993e-012     2.316e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3582.17 psi
   
   Pressure: 
   1744.87, 1800.80, 1852.12, 1904.43, 1962.58, 2019.24, 2082.19, 2155.47, 
   2260.54, 2351.48, 2403.00, 2464.23, 2530.11, 2600.94, 2667.63, 2724.16, 
   2820.24, 2910.76, 3038.20, 3163.68, 3216.73, 3264.93, 3310.49, 3351.73, 
   3388.49
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.22443        start      
        1            56       4.459e-004      6.42011     
        2            57       1.111e-006      0.00406     
        3            58       7.989e-010     2.750e-006   
        4            59       7.411e-012     2.499e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3569.90 psi
   
   Pressure: 
   1742.20, 1797.57, 1848.41, 1900.26, 1957.92, 2014.12, 2076.60, 2149.34, 
   2253.67, 2343.99, 2395.17, 2456.01, 2521.50, 2591.92, 2658.23, 2714.47, 
   2810.06, 2900.16, 3027.05, 3152.02, 3204.88, 3252.92, 3298.36, 3339.50, 
   3376.22
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.86641        start      
        1            56       4.246e-004      7.13156     
        2            57       1.176e-006      0.00465     
        3            58       8.846e-010     3.500e-006   
        4            59       1.004e-011     3.282e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3558.99 psi
   
   Pressure: 
   1739.96, 1794.86, 1845.29, 1896.74, 1953.96, 2009.76, 2071.80, 2144.05, 
   2247.71, 2337.47, 2388.34, 2448.83, 2513.95, 2584.00, 2649.97, 2705.94, 
   2801.09, 2890.80, 3017.17, 3141.68, 3194.36, 3242.25, 3287.57, 3328.63, 
   3365.30
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 7:07:56 AM
   7/26/2026 7:09:19 AM
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
        0            1          365.091        start      
        1            56         0.00770       0.29708     
        2            57       1.681e-005     3.557e-004   
        3            58       2.404e-009     9.221e-007   
        4            59       1.190e-010     9.828e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2258.04 psi
   
   Pressure: 
   1541.52, 1551.09, 1559.94, 1569.02, 1579.18, 1589.15, 1600.32, 1613.42, 
   1632.35, 1648.87, 1658.29, 1669.59, 1681.85, 1695.15, 1707.78, 1718.61, 
   1737.19, 1754.90, 1780.12, 1805.26, 1816.03, 1826.23, 1858.75, 1935.22, 
   1998.47
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.21, 0.46, 0.70, 
   0.77
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          301.560        start      
        1            56         0.00288       0.54897     
        2            57       4.219e-006     1.292e-004   
        3            58       2.957e-009     1.501e-007   
        4            59       1.102e-010     1.255e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2432.89 psi
   
   Pressure: 
   1541.29, 1550.81, 1559.60, 1568.63, 1578.74, 1588.65, 1599.76, 1612.79, 
   1631.61, 1648.04, 1657.41, 1668.64, 1680.84, 1694.06, 1706.63, 1717.40, 
   1735.89, 1753.50, 1778.61, 1810.23, 1887.92, 1972.74, 2049.65, 2116.66, 
   2173.45
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.26, 0.61, 0.73, 0.76, 0.78, 
   0.80
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          348.341        start      
        1            56         0.00615       0.57233     
        2            57       1.757e-005     3.668e-004   
        3            58       3.542e-009     1.029e-006   
        4            59       1.194e-010     2.036e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2816.49 psi
   
   Pressure: 
   1541.20, 1550.69, 1559.47, 1568.48, 1578.56, 1588.45, 1599.53, 1612.53, 
   1631.31, 1647.70, 1657.05, 1668.26, 1680.43, 1693.63, 1706.17, 1716.91, 
   1735.52, 1776.99, 1994.89, 2206.28, 2292.82, 2369.59, 2440.60, 2503.35, 
   2557.43
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.39, 0.68, 0.73, 0.76, 0.77, 0.78, 0.80, 
   0.81
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          325.095        start      
        1            56         0.00626       0.59027     
        2            57       1.077e-005     2.955e-004   
        3            58       5.303e-009     7.106e-007   
        4            59       1.217e-010     2.482e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3059.22 psi
   
   Pressure: 
   1541.17, 1550.65, 1559.42, 1568.41, 1578.48, 1588.37, 1599.43, 1612.41, 
   1631.17, 1647.53, 1656.87, 1668.06, 1680.21, 1693.38, 1706.83, 1760.30, 
   1921.00, 2067.51, 2269.16, 2464.70, 2546.19, 2619.22, 2687.25, 2747.77, 
   2800.40
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.22, 0.53, 
   0.71, 0.74, 0.76, 0.77, 0.78, 0.79, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          295.695        start      
        1            56         0.00263       0.73100     
        2            57       6.486e-006     1.109e-004   
        3            58       5.034e-009     1.748e-007   
        4            59       9.269e-011     1.522e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3229.96 psi
   
   Pressure: 
   1540.92, 1550.35, 1559.06, 1568.00, 1578.01, 1587.84, 1598.84, 1611.75, 
   1630.40, 1646.67, 1655.96, 1667.11, 1683.88, 1784.95, 1892.50, 1980.99, 
   2128.80, 2266.31, 2458.07, 2645.35, 2723.83, 2794.47, 2860.56, 2919.61, 
   2971.31
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.28, 0.63, 0.72, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.79, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          345.265        start      
        1            56         0.00579       0.37442     
        2            57       1.770e-005     2.110e-004   
        3            58       2.585e-009     6.770e-007   
        4            59       1.035e-010     1.009e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3390.46 psi
   
   Pressure: 
   1541.21, 1550.70, 1559.48, 1568.48, 1578.56, 1588.45, 1599.53, 1612.52, 
   1631.30, 1647.89, 1671.52, 1768.81, 1872.25, 1980.99, 2081.86, 2166.46, 
   2309.11, 2442.58, 2629.30, 2812.04, 2888.77, 2957.96, 3022.82, 3080.92, 
   3131.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.41, 0.68, 0.73, 0.75, 0.76, 0.77, 
   0.77, 0.78, 0.78, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          296.195        start      
        1            56         0.00484       1.42710     
        2            57       3.437e-006     2.678e-004   
        3            58       2.814e-009     3.742e-007   
        4            59       1.421e-010     1.716e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3597.77 psi
   
   Pressure: 
   1540.12, 1549.36, 1557.90, 1566.66, 1576.47, 1586.10, 1596.88, 1611.22, 
   1723.36, 1862.39, 1939.22, 2028.92, 2124.48, 2226.58, 2322.26, 2403.06, 
   2539.97, 2668.58, 2849.10, 3026.31, 3100.96, 3168.48, 3231.99, 3289.08, 
   3339.50
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.23, 
   0.57, 0.71, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 
   0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          309.793        start      
        1            56         0.00308       0.60336     
        2            57       8.914e-006     1.090e-004   
        3            58       7.743e-009     2.531e-007   
        4            59       1.433e-010     2.581e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3795.83 psi
   
   Pressure: 
   1540.89, 1550.31, 1559.01, 1567.94, 1577.96, 1593.61, 1683.91, 1794.69, 
   1949.55, 2081.76, 2156.02, 2243.71, 2337.61, 2438.20, 2532.58, 2612.35, 
   2747.55, 2874.56, 3052.87, 3227.92, 3301.68, 3368.42, 3431.23, 3487.76, 
   3537.76
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.65, 0.73, 
   0.75, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          339.447        start      
        1            56         0.00719       0.52792     
        2            57       2.182e-005     3.478e-004   
        3            58       7.230e-009     1.369e-006   
        4            59       1.559e-010     4.471e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3924.91 psi
   
   Pressure: 
   1540.93, 1550.36, 1559.32, 1588.67, 1676.03, 1759.75, 1850.76, 1955.36, 
   2104.27, 2232.54, 2304.95, 2390.75, 2482.83, 2581.62, 2674.43, 2752.96, 
   2886.15, 3011.39, 3187.32, 3360.16, 3433.04, 3499.06, 3561.25, 3617.29, 
   3666.97
   
   Saturation:
   0.20, 0.20, 0.21, 0.46, 0.69, 0.73, 0.75, 0.76, 
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          290.759        start      
        1            56         0.00362       1.19370     
        2            57       9.078e-006     2.615e-004   
        3            58       2.091e-009     1.137e-006   
        4            59       1.100e-010     4.975e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4054.87 psi
   
   Pressure: 
   1552.93, 1623.03, 1697.28, 1771.50, 1852.86, 1931.45, 2018.29, 2118.99, 
   2263.06, 2387.56, 2458.00, 2541.64, 2631.56, 2728.17, 2819.07, 2896.08, 
   3026.85, 3149.96, 3323.12, 3493.44, 3565.36, 3630.59, 3692.13, 3747.69, 
   3797.06
   
   Saturation:
   0.27, 0.62, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          27.6118        start      
        1            56         0.00100       0.51545     
        2            57       1.430e-006     7.264e-004   
        3            58       7.786e-011     8.905e-007   
        4            59       2.148e-011     3.618e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4353.58 psi
   
   Pressure: 
   1857.30, 1936.72, 2008.63, 2081.46, 2161.99, 2240.14, 2326.69, 2427.17, 
   2570.93, 2695.14, 2765.39, 2848.77, 2938.36, 3034.56, 3125.01, 3201.59, 
   3331.56, 3453.85, 3625.77, 3794.81, 3866.16, 3930.87, 3991.93, 4047.06, 
   4096.08
   
   Saturation:
   0.73, 0.75, 0.75, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.26433        start      
        1            56       8.508e-004      4.63135     
        2            57       1.391e-006      0.00195     
        3            58       6.763e-010     7.478e-007   
        4            59       9.169e-012     6.979e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4311.39 psi
   
   Pressure: 
   1837.97, 1914.69, 1984.81, 2056.13, 2135.24, 2212.20, 2297.58, 2396.83, 
   2538.99, 2661.90, 2731.47, 2814.08, 2902.89, 2998.30, 3088.05, 3164.07, 
   3293.15, 3414.66, 3585.55, 3753.66, 3824.66, 3889.09, 3949.92, 4004.90, 
   4053.84
   
   Saturation:
   0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.56856        start      
        1            56       6.067e-004      6.13949     
        2            57       1.095e-006      0.00288     
        3            58       6.663e-010     1.154e-006   
        4            59       8.139e-012     1.098e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4281.11 psi
   
   Pressure: 
   1829.25, 1904.36, 1973.20, 2043.34, 2121.24, 2197.11, 2281.37, 2379.39, 
   2519.88, 2641.43, 2710.26, 2792.04, 2880.00, 2974.54, 3063.51, 3138.92, 
   3267.01, 3387.65, 3557.43, 3724.52, 3795.14, 3859.27, 3919.86, 3974.67, 
   4023.53
   
   Saturation:
   0.76, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.40130        start      
        1            56       5.215e-004      7.55301     
        2            57       1.102e-006      0.00395     
        3            58       7.379e-010     1.917e-006   
        4            59       6.851e-012     1.709e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4257.90 psi
   
   Pressure: 
   1823.76, 1897.75, 1965.66, 2034.90, 2111.86, 2186.87, 2270.22, 2367.23, 
   2506.34, 2626.74, 2694.95, 2776.02, 2863.25, 2957.04, 3045.35, 3120.22, 
   3247.45, 3367.34, 3536.13, 3702.33, 3772.61, 3836.47, 3896.85, 3951.51, 
   4000.29
   
   Saturation:
   0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.80432        start      
        1            56       4.417e-004      8.15080     
        2            57       1.006e-006      0.00424     
        3            58       6.674e-010     2.250e-006   
        4            59       6.544e-012     1.798e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4238.41 psi
   
   Pressure: 
   1819.61, 1892.73, 1959.88, 2028.38, 2104.55, 2178.82, 2261.40, 2357.54, 
   2495.45, 2614.85, 2682.51, 2762.96, 2849.55, 2942.68, 3030.40, 3104.79, 
   3231.26, 3350.47, 3518.38, 3683.78, 3753.75, 3817.37, 3877.55, 3932.08, 
   3980.78
   
   Saturation:
   0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.32948        start      
        1            56       4.050e-004      8.90514     
        2            57       1.020e-006      0.00470     
        3            58       7.010e-010     2.735e-006   
        4            59       8.480e-012     2.220e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4221.43 psi
   
   Pressure: 
   1816.22, 1888.60, 1955.11, 2022.97, 2098.47, 2172.10, 2253.99, 2349.36, 
   2486.21, 2604.72, 2671.89, 2751.79, 2837.81, 2930.34, 3017.52, 3091.49, 
   3217.26, 3335.87, 3502.98, 3667.65, 3737.35, 3800.74, 3860.75, 3915.15, 
   3963.79
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.95014        start      
        1            56       3.990e-004      9.84653     
        2            57       1.121e-006      0.00535     
        3            58       8.402e-010     3.389e-006   
        4            59       8.761e-012     3.154e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4206.33 psi
   
   Pressure: 
   1813.32, 1885.06, 1951.00, 2018.31, 2093.21, 2166.27, 2247.55, 2342.24, 
   2478.13, 2595.84, 2662.58, 2741.97, 2827.47, 2919.47, 3006.16, 3079.73, 
   3204.89, 3322.94, 3489.32, 3653.33, 3722.77, 3785.97, 3845.82, 3900.10, 
   3948.67
   
   Saturation:
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.65587        start      
        1            56       4.245e-004      11.0537     
        2            57       1.338e-006      0.00633     
        3            58       1.151e-009     4.288e-006   
        4            59       7.068e-012     5.101e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4192.71 psi
   
   Pressure: 
   1810.77, 1881.95, 1947.39, 2014.19, 2088.55, 2161.11, 2241.83, 2335.90, 
   2470.93, 2587.91, 2654.25, 2733.18, 2818.20, 2909.71, 2995.97, 3069.19, 
   3193.76, 3311.31, 3477.02, 3640.43, 3709.64, 3772.65, 3832.35, 3886.53, 
   3935.03
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.43482        start      
        1            56       3.226e-004      10.1525     
        2            57       9.225e-007      0.00491     
        3            58       7.081e-010     3.036e-006   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4180.31 psi
   
   Pressure: 
   1808.48, 1879.16, 1944.15, 2010.50, 2084.37, 2156.46, 2236.69, 2330.19, 
   2464.42, 2580.75, 2646.72, 2725.23, 2809.82, 2900.88, 2986.73, 3059.62, 
   3183.67, 3300.74, 3465.85, 3628.70, 3697.69, 3760.53, 3820.09, 3874.17, 
   3922.62
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.26876        start      
        1            56       3.952e-004      11.7821     
        2            57       1.282e-006      0.00640     
        3            58       1.220e-009     4.146e-006   
        4            59       1.220e-011     6.041e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4168.94 psi
   
   Pressure: 
   1806.42, 1876.64, 1941.21, 2007.16, 2080.58, 2152.24, 2232.01, 2324.99, 
   2458.50, 2574.21, 2639.84, 2717.97, 2802.16, 2892.80, 2978.28, 3050.87, 
   3174.43, 3291.08, 3455.61, 3617.95, 3686.75, 3749.43, 3808.86, 3862.85, 
   3911.24
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 7:10:07 AM
   7/26/2026 7:11:26 AM
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
        0            1          408.776        start      
        1            56         0.00457       0.28820     
        2            57       1.381e-005     1.527e-004   
        3            58       1.021e-008     3.354e-007   
        4            59       1.213e-010     3.490e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2506.60 psi
   
   Pressure: 
   1551.90, 1563.86, 1574.92, 1586.26, 1598.96, 1611.43, 1625.39, 1641.77, 
   1665.43, 1686.08, 1697.86, 1711.98, 1727.31, 1743.94, 1759.75, 1773.28, 
   1796.52, 1818.67, 1850.22, 1881.67, 1895.17, 1915.07, 2014.02, 2106.30, 
   2182.33
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.31, 0.65, 0.74, 
   0.78
   
   
   
   
   
   Time: 
   1000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          391.419        start      
        1            56         0.00489       1.12722     
        2            57       1.249e-005     3.363e-004   
        3            58       5.490e-009     6.489e-007   
        4            59       1.587e-010     2.879e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   2895.78 psi
   
   Pressure: 
   1551.05, 1562.81, 1573.69, 1584.85, 1597.34, 1609.61, 1623.34, 1639.46, 
   1662.75, 1683.06, 1694.66, 1708.56, 1723.66, 1740.03, 1755.59, 1768.93, 
   1791.82, 1813.70, 1863.45, 2118.96, 2232.21, 2331.53, 2422.80, 2503.13, 
   2571.96
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.31, 0.65, 0.73, 0.76, 0.77, 0.79, 
   0.81
   
   
   
   
   
   Time: 
   1500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          383.593        start      
        1            56         0.00386       0.90740     
        2            57       1.074e-005     1.561e-004   
        3            58       9.302e-009     2.820e-007   
        4            59       1.638e-010     2.575e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3394.85 psi
   
   Pressure: 
   1551.09, 1562.86, 1573.75, 1584.92, 1597.42, 1609.69, 1623.44, 1639.56, 
   1662.87, 1683.20, 1694.81, 1708.73, 1723.83, 1740.22, 1755.83, 1776.00, 
   1961.59, 2148.45, 2403.63, 2650.15, 2752.65, 2844.41, 2929.80, 3005.72, 
   3071.67
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.30, 
   0.64, 0.73, 0.75, 0.76, 0.77, 0.78, 0.79, 0.80, 
   0.82
   
   
   
   
   
   Time: 
   2000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          366.718        start      
        1            56         0.00330       0.94119     
        2            57       7.053e-006     1.263e-004   
        3            58       5.583e-009     1.807e-007   
        4            59       1.381e-010     1.487e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3652.83 psi
   
   Pressure: 
   1550.95, 1562.69, 1573.55, 1584.68, 1597.15, 1609.38, 1623.09, 1639.16, 
   1662.39, 1682.66, 1694.23, 1708.11, 1728.44, 1852.48, 1986.51, 2096.73, 
   2280.80, 2452.01, 2690.77, 2923.94, 3021.67, 3109.65, 3191.96, 3265.54, 
   3329.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.20, 0.20, 0.20, 0.28, 0.62, 0.72, 0.75, 
   0.76, 0.77, 0.77, 0.78, 0.79, 0.80, 0.80, 0.81, 
   0.82
   
   
   
   
   
   Time: 
   2500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          364.283        start      
        1            56         0.00345       0.81518     
        2            57       5.328e-006     1.216e-004   
        3            58       4.058e-009     1.540e-007   
        4            59       1.905e-010     1.223e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   3893.43 psi
   
   Pressure: 
   1550.99, 1562.74, 1573.60, 1584.74, 1597.21, 1609.45, 1623.16, 1639.24, 
   1662.50, 1688.61, 1771.92, 1892.01, 2017.66, 2150.57, 2274.28, 2378.29, 
   2553.94, 2718.48, 2948.89, 3174.58, 3269.44, 3355.07, 3435.41, 3507.47, 
   3570.88
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 
   0.20, 0.26, 0.61, 0.72, 0.74, 0.75, 0.76, 0.77, 
   0.78, 0.78, 0.79, 0.79, 0.80, 0.80, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          361.400        start      
        1            56         0.00342       1.13305     
        2            57       4.387e-006     1.044e-004   
        3            58       4.653e-009     1.010e-007   
        4            59       1.134e-010     1.100e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4254.34 psi
   
   Pressure: 
   1550.48, 1562.10, 1572.85, 1583.88, 1596.22, 1608.35, 1625.41, 1741.27, 
   1940.55, 2108.46, 2202.16, 2312.37, 2430.15, 2556.19, 2674.39, 2774.27, 
   2943.50, 3102.48, 3325.65, 3544.71, 3637.00, 3720.51, 3799.08, 3869.76, 
   3932.25
   
   Saturation:
   0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.26, 0.61, 
   0.72, 0.74, 0.75, 0.76, 0.77, 0.77, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.81, 0.81, 0.82, 
   0.83
   
   
   
   
   
   Time: 
   3500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          366.939        start      
        1            56         0.00501       0.93818     
        2            57       2.274e-006     1.507e-004   
        3            58       2.602e-009     2.043e-007   
        4            59       1.509e-010     1.668e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4470.78 psi
   
   Pressure: 
   1550.62, 1562.27, 1573.05, 1585.96, 1664.32, 1770.25, 1884.70, 2015.57, 
   2201.43, 2361.36, 2451.60, 2558.48, 2673.19, 2796.26, 2911.89, 3009.74, 
   3175.71, 3331.80, 3551.08, 3766.50, 3857.35, 3939.65, 4017.17, 4087.04, 
   4148.97
   
   Saturation:
   0.20, 0.20, 0.20, 0.24, 0.58, 0.71, 0.74, 0.75, 
   0.76, 0.76, 0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 
   0.83
   
   
   
   ================================================
         Rejected (Maximum Pressure Violated) 
   ================================================
   
   
   Time: 
   4000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          348.279        start      
        1            56         0.00554       0.92958     
        2            57       2.351e-006     2.300e-004   
        3            58       3.888e-009     5.558e-007   
        4            59       1.358e-010     6.525e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1556.02, 1628.72, 1717.03, 1805.20, 1901.71, 1994.88, 2097.77, 2217.03, 
   2387.60, 2534.95, 2618.30, 2717.22, 2823.54, 2937.73, 3045.11, 3136.05, 
   3290.41, 3435.66, 3639.84, 3840.55, 3925.23, 4001.99, 4074.35, 4139.63, 
   4197.58
   
   Saturation:
   0.24, 0.59, 0.72, 0.74, 0.75, 0.76, 0.76, 0.77, 
   0.77, 0.77, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   4500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          25.6171        start      
        1            56         0.00131       0.80645     
        2            57       1.943e-006      0.00102     
        3            58       1.062e-010     1.153e-006   
        4            59       1.979e-011     4.111e-011   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1874.04, 1957.34, 2032.82, 2109.31, 2193.89, 2276.00, 2366.94, 2472.54, 
   2623.65, 2754.23, 2828.10, 2915.78, 3010.00, 3111.20, 3206.36, 3286.95, 
   3423.77, 3552.54, 3733.64, 3911.75, 3986.95, 4055.15, 4119.51, 4177.63, 
   4229.32
   
   Saturation:
   0.73, 0.75, 0.76, 0.76, 0.76, 0.77, 0.77, 0.77, 
   0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 
   0.83
   
   
   
   
   
   Time: 
   5000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          8.18028        start      
        1            56       8.830e-004      1.38716     
        2            57       1.648e-006      0.00170     
        3            58       5.554e-010     2.045e-006   
        4            59       1.256e-011     5.626e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1859.22, 1940.83, 2015.46, 2091.39, 2175.64, 2257.62, 2348.60, 2454.37, 
   2605.90, 2736.96, 2811.15, 2899.28, 2994.05, 3095.88, 3191.71, 3272.91, 
   3410.83, 3540.72, 3723.47, 3903.29, 3979.26, 4048.23, 4113.35, 4172.23, 
   4224.66
   
   Saturation:
   0.76, 0.76, 0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   5500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          5.71427        start      
        1            56       8.499e-004      1.65307     
        2            57       1.851e-006      0.00244     
        3            58       6.563e-010     3.665e-006   
        4            59       1.367e-011     1.069e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1854.09, 1934.90, 2009.00, 2084.50, 2168.39, 2250.11, 2340.88, 2446.50, 
   2597.90, 2728.91, 2803.12, 2891.30, 2986.18, 3088.18, 3184.19, 3265.60, 
   3403.92, 3534.24, 3717.68, 3898.26, 3974.60, 4043.94, 4109.47, 4168.77, 
   4221.64
   
   Saturation:
   0.77, 0.77, 0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          4.60373        start      
        1            56       6.151e-004      1.54702     
        2            57       1.188e-006      0.00201     
        3            58       3.139e-010     2.815e-006   
        4            59       7.780e-012     5.624e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1851.26, 1931.56, 2005.28, 2080.46, 2164.04, 2245.51, 2336.07, 2441.48, 
   2592.66, 2723.53, 2797.68, 2885.84, 2980.72, 3082.75, 3178.85, 3260.34, 
   3398.87, 3529.43, 3713.29, 3894.37, 3970.95, 4040.56, 4106.39, 4166.00, 
   4219.21
   
   Saturation:
   0.77, 0.77, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.81, 0.81, 0.81, 0.81, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   6500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.94838        start      
        1            56       5.855e-004      1.59834     
        2            57       1.127e-006      0.00208     
        3            58       2.686e-010     3.019e-006   
        4            59       1.107e-011     5.427e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1849.31, 1929.24, 2002.66, 2077.57, 2160.90, 2242.15, 2332.50, 2437.71, 
   2588.65, 2719.36, 2793.44, 2881.54, 2976.39, 3078.42, 3174.54, 3256.08, 
   3394.74, 3525.47, 3709.63, 3891.07, 3967.85, 4037.67, 4103.74, 4163.62, 
   4217.11
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.46680        start      
        1            56       6.212e-004      1.72007     
        2            57       1.245e-006      0.00234     
        3            58       3.006e-010     3.655e-006   
        4            59       1.314e-011     6.910e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1847.80, 1927.44, 2000.61, 2075.30, 2158.39, 2239.45, 2329.61, 2434.63, 
   2585.35, 2715.89, 2789.89, 2877.92, 2972.73, 3074.74, 3170.86, 3252.43, 
   3391.17, 3522.03, 3706.43, 3888.18, 3965.11, 4035.12, 4101.40, 4161.50, 
   4215.25
   
   Saturation:
   0.78, 0.78, 0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 
   0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 
   0.81, 0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   7500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          3.13229        start      
        1            56       7.287e-004      1.92405     
        2            57       1.592e-006      0.00288     
        3            58       4.292e-010     5.004e-006   
        4            59       1.055e-011     1.107e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1846.56, 1925.94, 1998.91, 2073.39, 2156.29, 2237.17, 2327.16, 2432.00, 
   2582.50, 2712.88, 2786.81, 2874.78, 2969.53, 3071.51, 3167.62, 3249.21, 
   3388.02, 3518.98, 3703.58, 3885.58, 3962.66, 4032.82, 4099.29, 4159.59, 
   4213.56
   
   Saturation:
   0.78, 0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.81628        start      
        1            56       5.505e-004      1.72846     
        2            57       1.027e-006      0.00217     
        3            58       2.327e-010     3.261e-006   
        4            59       9.555e-012     5.787e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1845.50, 1924.65, 1997.43, 2071.74, 2154.46, 2235.18, 2325.01, 2429.69, 
   2579.98, 2710.21, 2784.07, 2871.97, 2966.67, 3068.61, 3164.72, 3246.31, 
   3385.17, 3516.21, 3700.99, 3883.22, 3960.42, 4030.73, 4097.36, 4157.85, 
   4212.02
   
   Saturation:
   0.78, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 
   0.81, 0.81, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   8500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.55667        start      
        1            56       6.671e-004      1.95857     
        2            57       1.369e-006      0.00272     
        3            58       3.666e-010     4.574e-006   
        4            59       9.993e-012     1.015e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1844.56, 1923.52, 1996.13, 2070.27, 2152.83, 2233.41, 2323.09, 2427.62, 
   2577.72, 2707.81, 2781.60, 2869.43, 2964.08, 3065.98, 3162.07, 3243.67, 
   3382.57, 3513.69, 3698.62, 3881.05, 3958.37, 4028.80, 4095.59, 4156.24, 
   4210.60
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.35997        start      
        1            56       5.462e-004      1.81490     
        2            57       1.001e-006      0.00222     
        3            58       2.428e-010     3.353e-006   
        4            59       1.057e-011     6.553e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1843.72, 1922.49, 1994.95, 2068.95, 2151.36, 2231.80, 2321.34, 2425.74, 
   2575.66, 2705.61, 2779.33, 2867.10, 2961.70, 3063.57, 3159.64, 3241.25, 
   3380.18, 3511.36, 3696.43, 3879.05, 3956.47, 4027.03, 4093.95, 4154.76, 
   4209.29
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.81, 0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   9500.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.20524        start      
        1            56       7.170e-004      2.12029     
        2            57       1.502e-006      0.00301     
        3            58       4.593e-010     5.235e-006   
        4            59       7.917e-012     1.371e-009   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1842.95, 1921.56, 1993.88, 2067.75, 2150.01, 2230.33, 2319.75, 2424.01, 
   2573.76, 2703.59, 2777.25, 2864.96, 2959.50, 3061.34, 3157.39, 3239.00, 
   3377.97, 3509.21, 3694.40, 3877.20, 3954.71, 4025.37, 4092.43, 4153.38, 
   4208.07
   
   Saturation:
   0.79, 0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   
   
   Time: 
   10000.00 days
    Iteration    Func-count       f(x)      Norm of Step
        0            1          2.01823        start      
        1            56       5.783e-004      1.95347     
        2            57       1.072e-006      0.00241     
        3            58       2.969e-010     3.740e-006   
        4            59       9.557e-012     8.664e-010   
   Producer BHP: 
   1500.00 psi
   
   Injector BHP: 
   4500.00 psi
   
   Pressure: 
   1842.24, 1920.71, 1992.89, 2066.64, 2148.77, 2228.97, 2318.27, 2422.41, 
   2572.01, 2701.71, 2775.31, 2862.97, 2957.46, 3059.26, 3155.30, 3236.91, 
   3375.90, 3507.20, 3692.51, 3875.46, 3953.07, 4023.83, 4091.00, 4152.09, 
   4206.93
   
   Saturation:
   0.79, 0.79, 0.79, 0.80, 0.80, 0.80, 0.80, 0.80, 
   0.80, 0.80, 0.81, 0.81, 0.81, 0.81, 0.81, 0.81, 
   0.82, 0.82, 0.82, 0.82, 0.83, 0.83, 0.83, 0.83, 
   0.84
   
   
   
   7/26/2026 7:12:23 AM
   7/26/2026 7:13:44 AM

.. figure:: images/$"1D_WaterFlooding_{rate}.gif
   :align: center
   :alt: $"1D_WaterFlooding_{rate}.gif

