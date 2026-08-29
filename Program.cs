using ConsoleApp1;
{
    currenctdirectory = "C:\\Users\\lateef.a.kareem\\Documents";
    Writer.Run();
    {
        Func<double, double> J0, J1;
        J0 = (x) => BesselJ(0, x); J1 = (x) => BesselJ(1, x);
        double BottomInfiniteActingRadial_Wd(double tD, double hD)
        {
            if (tD == 0) return 0;
            Func<double, double> lapW = s =>
            {
                double s3 = s * s * s;
                Func<double, double> Intfun = x =>
                {
                    double fac = Pow(J1(x) / x, 2), am = 0, am2, x2 = x * x;
                    double fun = Coth(hD * x) / s - x / hD / (x2 * (s + x2));
                    double error = 1; int m = 0;
                    for (m = 1; m < 10000; m++)
                    {
                        am = m * pi / hD; am2 = am * am;
                        error = 2 * x / hD / ((x2 + am2) * (s + x2 + am2));
                        fun -= error;
                        if (error < 1e-9) break;
                    }
                    return fac * fun;
                };
                return 1 / (4 * s3 * hD * Integral(Intfun, 0, inf));
            };
            return NiLaplace(lapW, tD);
        }
        Indexer Td = 1..21;
        double[] zD = [0.05, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0]; string[] wi;
        for (int i = 0; i < Td.Numel; i++)
        {
            double td = Td[i]*0.1;
            wi = [..zD.Select(z => string.Format("{0,8:F4}", BottomInfiniteActingRadial_Wd(td, z)))];
            Console.WriteLine($"|  {string.Format("{0,4:F1}", td)}  | {string.Join(' ', wi)}");
        }
    }
    {
        ConsoleApp1.TrainingFiles.WaterInfluxPublication.Run();
    }
    {
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
        int Nblocks = 25, L = Nblocks - 1, M = 2 * Nblocks, Nwells = 2;

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
            double[] R_total = Zeros(M + 2 * Nwells);

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
        double Sws(double Sw) => (Sw - Sw_r) / (1 - Sw_r);
        double Swe(double Sw) => (Sw - Sw_r) / (1 - Sw_r - So_r);

        // Capillary Pressure relationships (Drainage vs Imbibition curves)
        double Pc_D(double Sw) => Pe * Pow(Sws(Sw), -0.5);
        double Pc_I(double Sw) => Pe * (Pow(Swe(Sw), -0.5) - 1);

        // Formation Volume Factors (B-factors) modeling fluid compressibility
        double Bo(double Po) => Bo0 * Exp(co * (1500 - Po));
        double Bw(double Pw) => Bw0 * Exp(cw * (2500 - Pw));

        // Pressure-dependent dynamic fluid viscosities
        double μo(double Po) => μo0 * Exp(bo * (Po - 1500));
        double μw(double Pw) => μw0 * Exp(bw * (Pw - 2500));

        // Modified Corey Brooks models evaluating relative permeability curves
        double Krw(double Sw) => krw0 * Pow(Swe(Sw), nw);
        double Kro(double So) => kro0 * Pow(1 - Swe(1 - So), no);

        // Inter-block transmissibility calculation using the
        // harmonic mean of raw cell permeabilities
        double Harmmean(double x1, double x2) => 2 / (1 / x1 + 1 / x2);

        // Unit conversion coefficients for oilfield standard parameters
        // =================================================================
        // Transmissibility conversion factor (Darcy to Field Units)
        double alpha = 1.127e-3;
        // Geometric productivity factor conversion for radial well inflows
        double alpha_well = alpha * 2 * pi;
        // Reservoir volume factor (Cubic feet to Stock Tank Barrels)
        double beta = 5.615;

        // Spatial dimensions (ft), block pore volume (STB),
        // and well radius parameters
        double dt, Dx = 200, Dy = 1000, Dz = 20, Ax = Dy * Dz,
               V = Dx * Dy * Dz / beta, rw = 0.5, re, WI, WIw, WIo;

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
                    Tr = alpha * Ax * Harmmean(K[i - 1], K[i]);

                    // Single-point upstream weighting for water phase stability
                    (Pw_up, Sw_up) = Pw_np1[i - 1] > Pw_np1[i] ?
                        (Pw_np1[i - 1], Sw_np1[i - 1]) : (Pw_np1[i], Sw_np1[i]);
                    Tw = Tr * Krw(Sw_up) / (μw(Pw_up) * Bw(Pw_up));
                    Rw[i] += Tw * (Pw_np1[i - 1] - Pw_np1[i]) * dt / Dx;

                    // Single-point upstream weighting for oil phase stability
                    (Po_up, So_up) = Po_np1[i - 1] > Po_np1[i] ?
                        (Po_np1[i - 1], So_np1[i - 1]) : (Po_np1[i], So_np1[i]);
                    To = Tr * Kro(So_up) / (μo(Po_up) * Bo(Po_up));
                    Ro[i] += To * (Po_np1[i - 1] - Po_np1[i]) * dt / Dx;
                }

                // Inter-block inter-flux flux logic (Right neighbor interaction)
                if (i < L)
                {
                    Tr = alpha * Ax * Harmmean(K[i], K[i + 1]);

                    // Single-point upstream weighting for water phase stability
                    (Pw_up, Sw_up) = Pw_np1[i + 1] > Pw_np1[i] ?
                        (Pw_np1[i + 1], Sw_np1[i + 1]) : (Pw_np1[i], Sw_np1[i]);
                    Tw = Tr * Krw(Sw_up) / (μw(Pw_up) * Bw(Pw_up));
                    Rw[i] += Tw * (Pw_np1[i + 1] - Pw_np1[i]) * dt / Dx;

                    // Single-point upstream weighting for oil phase stability
                    (Po_up, So_up) = Po_np1[i + 1] > Po_np1[i] ?
                        (Po_np1[i + 1], So_np1[i + 1]) : (Po_np1[i], So_np1[i]);
                    To = Tr * Kro(So_up) / (μo(Po_up) * Bo(Po_up));
                    Ro[i] += To * (Po_np1[i + 1] - Po_np1[i]) * dt / Dx;
                }

                // Time accumulation terms (implicit backward Euler method)
                Rw[i] -= V * Phi[i] * (Sw_np1[i] / Bw(Pw_np1[i]) - Sw_n[i] / Bw(Pw_n[i]));
                Ro[i] -= V * Phi[i] * (So_np1[i] / Bo(Po_np1[i]) - So_n[i] / Bo(Po_n[i]));
            }

            // Peaceman radius calculation for an isolated wellbore
            re = 0.14 * Hypot(Dx, Dy);
            int idx;

            // --- Well Formulation: Producer Section ---
            Rwells[0] += Qwells_np1[0];
            idx = Producer.Index;
            // Base Well Index calculation
            WI = alpha_well * K[idx] * Dz / Log(re / rw);
            // Water mobility at well
            WIw = WI * Krw(Sw_np1[idx]) / (μw(Pw_np1[idx]) * Bw(Pw_np1[idx]));
            // Oil mobility at well
            WIo = WI * Kro(So_np1[idx]) / (μo(Po_np1[idx]) * Bo(Po_np1[idx]));
            // Calculate phase flow rates using the wellbore
            // pressure and connected grid cell properties
            Producer.WaterRate = (Pwells_np1[0] - Pw_np1[Producer.Index]) * WIw;
            Producer.OilRate = (Pwells_np1[0] - Po_np1[Producer.Index]) * WIo;
            // Inject sink terms directly back into the connected grid cell
            Rw[Producer.Index] += Producer.WaterRate * dt;
            Ro[Producer.Index] += Producer.OilRate * dt;
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
            WI = alpha_well * K[idx] * Dz / Log(re / rw);
            // Injecting pure water phase
            WIw = WI * krw0 / (μw(Pw_np1[idx]) * Bw(Pw_np1[idx]));
            // Calculate injection rate using the
            // wellbore pressure and connected grid cell properties
            Injector.WaterRate = (Pwells_np1[1] - Pw_np1[idx]) * WIw;
            // Inject source terms directly back into the connected grid cell
            Rw[idx] += Injector.WaterRate * dt;
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
        double betweenab(double a, double b, double f) => a + f * (b - a);
        double interps(List<double> X, List<double> Y, double x)
        {
            int i = X.FindIndex(xi => xi > x);
            double f = (x - X[i - 1]) / (X[i] - X[i - 1]);
            return betweenab(Y[i - 1], Y[i], f);
        }
        double[] interpa(List<double> X, List<double[]> Y, double x)
        {
            int i = X.FindIndex(xi => xi > x);
            double f = (x - X[i - 1]) / (X[i] - X[i - 1]);
            return [.. Y[i - 1].Zip(Y[i], (a, b) => betweenab(a, b, f))];
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
        double delt = EndTime / 300; // Time steps for reporting intervals

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
            Axis([0, EndTime, 0, Injector.MaxPressure * 1.1]);
            Title("Producer BHP");

            Subplot(7, 4, [8, 9, 12, 13]);
            var Prate = Plot([0], [0], "r", 2);
            Axis([0, EndTime, 0, Producer.ProdRate * 1.1]);
            Title("Producer Rate");

            Subplot(7, 4, [16, 17, 20, 21]);
            var Pbsw = Plot([0], [0], "r", 2);
            Axis([0, EndTime, 0, 105]);
            Title("Producer WaterCut");

            Subplot(7, 4, [2, 3, 6, 7]);
            var Ibhp = Plot([0], [0], "b", 2);
            Axis([0, EndTime, 0, Injector.MaxPressure * 1.1]);
            Title("Injector BHP");

            Subplot(7, 4, [10, 11, 14, 15]);
            var Irate = Plot([0], [0], "b", 2);
            Axis([0, EndTime, 0, Injector.InjRate * 1.1]);
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
            var opts = SolverSet(MaxIter: 10, AbsTol: 1e-6);

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
                (Time.Last() - t) * (Time.Last() + dt - t) < 0))
                {
                    opts.Display = true;
                    printtime = Array.Find(displaytimes,
                        t => (Time.Last() - t) * (Time.Last() + dt - t) < 0);
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
                    dt = 0.5 * dt;
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
                WaterCut.Add(Producer.WaterRate * 100 / (Producer.WaterRate + Producer.OilRate));
                SweepEff.Add((Sw_n.Sum() / Nblocks - Sinit) * 100 / (1 - Sinit));
                Time.Add(Time.Last() + dt);

                // Adaptive Time-Stepping Logic:
                // scale dt up if convergence is fast, scale down if slow
                if (opts.ans.Iter < 4) dt = 1.25 * dt;
                if (opts.ans.Iter > 8) dt = 0.5 * dt;
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
                Pbhp.Xdata = Vcart(Pbhp.Xdata, i * delt);
                Pbhp.Ydata = Vcart(Pbhp.Ydata, interps(Time, ProdPwf, i * delt));

                Prate.Xdata = Pbhp.Xdata;
                Prate.Ydata = Vcart(Prate.Ydata, interps(Time, ProdRate, i * delt));

                Pbsw.Xdata = Pbhp.Xdata;
                Pbsw.Ydata = Vcart(Pbsw.Ydata, interps(Time, WaterCut, i * delt));

                Ibhp.Xdata = Pbhp.Xdata;
                Ibhp.Ydata = Vcart(Ibhp.Ydata, interps(Time, InjPwf, i * delt));

                Irate.Xdata = Pbhp.Xdata;
                Irate.Ydata = Vcart(Irate.Ydata, interps(Time, InjRate, i * delt));

                Iswp.Xdata = Pbhp.Xdata;
                Iswp.Ydata = Vcart(Iswp.Ydata, interps(Time, SweepEff, i * delt));

                // Generate spatial mapping visualization showing fluid front changes over time
                Sw_n = interpa(Time, S, i * delt);
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
    }
    {
        //int funeval = 0;
        //double SumSq(double[] x) => x.Sum(x => x * x);
        //double fun(double[] x)
        //{ 
        //    funeval++;
        //    return 1;
        //}
        //var J = IntegralN(fun, 
        //    [a =>-1, a => -Sqrt(1 - SumSq(a[..1])), a => -Sqrt(1 - SumSq(a[..1])), a => -Sqrt(1 - SumSq(a[..2]))],
        //    [a => 1, a => Sqrt(1 - SumSq(a[..1])), a => Sqrt(1 - SumSq(a[..1])), a => Sqrt(1 - SumSq(a[..2]))]);
        //Console.WriteLine($"J = {J}, {4 * pi / 3}");
    }
    //{
    //    double Area(ColVec x, ColVec y)
    //    {
    //        x = Vcart(x, x[0]); y = Vcart(y, y[0]);
    //        return Abs(x[1..].T * y[..^1] - x[..^1].T * y[1..]) / 2.0;
    //    }
    //    (ColVec xl, ColVec yl) FillPoints(double theta, double f, double a, double b, ColVec x, ColVec y)
    //    {
    //        double frac(double h, double phi) => Acos(h) - h * Sqrt(1 - h * h) - phi * pi;
    //        double h = Fzero(h => f <= 0.5 ? frac(h, f) : frac(h, 1 - f), [0, 1]), d1, d2, t;
    //        double A = -Sin(theta), B = Cos(theta), scale = Hypot(a * A, b * B), C = Sign(0.5 - f) * h * scale;
    //        List<double> xl = [], yl = []; int n = x.Numel;
    //        for (int i = 0; i < n; i++)
    //        {
    //            int j = (i + 1) % n;
    //            d1 = (A * x[i] + B * y[i] + C);
    //            d2 = (A * x[j] + B * y[j] + C);
    //            if (d1 <= 0) { xl.Add(x[i]); yl.Add(y[i]); }
    //            if (d1 * d2 < 0)
    //            {
    //                t = d1 / (d1 - d2);
    //                xl.Add(x[i] + t * (x[j] - x[i]));
    //                yl.Add(y[i] + t * (y[j] - y[i]));
    //            }
    //        }
    //        return (xl, yl);
    //    }
    //    ColVec t, x, y, xl, yl; double f = 0.2, a = 3, b = 1.5;
    //    t = Linspace(0, 2 * pi);
    //    x = a * Cos(t); y = b * Sin(t);
    //    var plt = Plot(x, y, "k", 3);
    //    HoldOn();
    //    double TankArea = Area(x, y);
    //    double theta = 0.0 * pi / 180.0;
    //    (xl, yl) = FillPoints(theta, f, a, b, x, y);
    //    var fluid = Fill(xl, yl, "b", 0.5);
    //    AxisEqual();
    //    SaveAs("TankFill.png");
    //    double calculatedFluidArea = Area(xl, yl);
    //    double expectedFluidArea = f * TankArea;
    //}
    //{
    //    string name = "plot"; int n = 0;
    //    SparseMatrix A = SparseMatrix.Bucky();
    //    Action<ColVec, ColVec> plotfun = (x, y) =>
    //    {
    //        Scatter(x, y, "ofb");
    //        SaveAs(name + n + ".png", 800, 400);
    //        n++;
    //    };
    //    A.MakeLU2(plotfun);
    //}

    //{
    //    (var X, var Y) = Meshgrid(Linspace(-2 * pi, 2 * pi, 51), Linspace(0, 4 * pi, 51));
    //    var Z = Sin(X) + Cos(Y);
    //    Contour(X, Y, Z, 10);
    //    SaveAs("Contour.png", 600, 400);
    //    Contourf(X, Y, Z, 10);
    //    SaveAs("Contourf.png", 600, 400);
    //}
    //{
    //    double D = 0.01;                  // Diffusion coefficient
    //    double growthRate = 1.0;          // Growth rate
    //    double C_ambient = 0.0;           // Ambient/Boundary concentration

    //    double[] r = Linspace(0, 5, 101); // Spatial grid [0, 5]
    //    double[] t = Linspace(0, 6, 7);   // Time output points [0, 6]
    //    (ColVec T, Matrix U) = Pdepe(
    //        m: 1,                                                                     // Geometry: 0 = Cartesian, 1 = Cylindrical, 2 = Spherical
    //        pdefun: (r, t, u, dudr) => (1.0, D * dudr, growthRate * u * (1.0 - u)),   // PDE components: (c, f, s) -> du/dt = D/r*d/dr(r*du/dr) + g*u*(1-u)
    //        icfun: r => r < 0.4 ? 1.0 : 0.0,                                          // Initial condition: u(r,0) = 1 for r < 0.4, else 0
    //        bcfun: (rl, ul, rr, ur, t) => (0, 1, ur - C_ambient, 0),                  // BCs: Symmetry at origin (du/dr = 0), Dirichlet at boundary (u(5,t) = C_ambient)
    //        x: r,                                                                     // Spatial discretization array
    //        t: t,                                                                     // Solution output times
    //        Odeset(Stats: true, AbsTol: 1e-5, OdeSolver: "ode85a"));                  // Ode options
    //    Plot(r, U, Linewidth: 2); GridOn();
    //    Title("Cylindrical Fisher-KPP Radial Wave Front (m = 1)");
    //    Xlabel("Position r"); Ylabel("Population Density u(r,t)");
    //    Legend(T.Select(time => $"t = {time:0.00}"));
    //    SaveAs("Cylindrical_FisherKPP.png");
    //    CloseFig();
    //}

    //{
    //    (var X, var Y) = Meshgrid(Linspace(-3, 3));
    //    var T1 = 3 * (1 - X).Pow(2);
    //    var T2 = Exp(-X.Pow(2) - (Y + 1).Pow(2));
    //    var T3 = 10 * (X / 5 - X.Pow(3) - Y.Pow(3));
    //    var T4 = Exp(-X.Pow(2) - Y.Pow(2));
    //    var T5 = Exp(-(X + 1).Pow(2) - Y.Pow(2)) / 3;
    //    var Z = T1.Times(T2) - T3.Times(T4) - T5;
    //    Contour(X, Y, Z, 20);
    //    SaveAs("Contour.png", 600, 400);
    //}

    //{
    //    int N = 50;
    //    ColVec x = Rand(N), y = Rand(N);
    //    DelaunayTriangulation DT = new(x, y);
    //    TriPlot(x, y, DT, "b", 2); HoldOn();
    //    Voronoi(x, y, DT, "g", 2);
    //    ColVec xh = x[DT.Hull], yh = y[DT.Hull];
    //    xh = Vcart(xh, xh[0]); yh = Vcart(yh, yh[0]);
    //    Plot(xh, yh, "r", 2);
    //    Axis([0, 1, 0, 1]);
    //    SaveAs("zDV_Diagram.png", 600, 600);
    //    List<ColVec> M = [x, y];
    //    WriteMatrix(M, "Dln.txt");
    {
        // Reservoir Simulation
        currenctdirectory = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\Morgana Data\";
        Matrix Base = ReadMatrix("Base.txt");
        var id = Base[.., 2] == 3075;

        ColVec x = Base[id, 0], y = Base[id, 1];
        Plot(x, y, "k");
        var rx = x.Range(); var ry = y.Range();
    }
    {
        // Morgana Field Development Plan
        currenctdirectory = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\Morgana Data\";
        Matrix Top = ReadMatrix("Top.txt"), Base = ReadMatrix("Base.txt");
        double Area(Matrix Data, double level)
        {
            var id = Data[.., 2] == level;
            ColVec x = Data[id, 0], y = Data[id, 1];
            x = Vcart(x, x[0]); y = Vcart(y, y[0]);
            return 0.5*Abs((x[..^1].Times(y[1..]) - y[..^1].Times(x[1..])).Sum());
        }

        // processing top
        double[] toplevels = [..Top[.., 2].Distinct()], 
            baselevels = [..Base[.., 2].Distinct()], 
            alllevels = [.. toplevels.Union(baselevels)];

        double[] allareas = [.. alllevels.Select(level => (toplevels.Contains(level)?Area(Top, level):0) - (baselevels.Contains(level)?Area(Base, level):0))];

        for (int i = 0; i < alllevels.Length; i++) Console.WriteLine($"Level = {alllevels[i]}, Area = {allareas[i]}");

        // RockVolume
        double GrossRockVolume = 0.5*(allareas[0] + 2*allareas[1..^1].Sum() + allareas[^1])*(alllevels[1]-alllevels[0]);

        // PretroPhysics Data

        (double Mean, double StdDev) ComputeStatistics(ColVec data, bool isSample = true)
        {
            // 1. Compute Mean
            double mean = data.Average();

            // 2. Compute Sum of Squares of Differences
            double sumOfSquares = (data - mean).SumSq();

            // 3. Compute Standard Deviation
            double divisor = isSample ? data.Numel - 1 : data.Numel;

            if (divisor <= 0) return (mean, 0.0); // Guard against single-element sample sets
            return (mean, Sqrt(sumOfSquares / divisor));
        }

        Matrix CapPress = ReadMatrix("CapPressure.txt"), NTGPoro = ReadMatrix("PetroPhysics.txt");
        var (NTG_P50, NTGstd) = ComputeStatistics(NTGPoro[.., 0]);
        var (Poro_P50, Porostd) = ComputeStatistics(NTGPoro[.., 1]);

        // From PVT (P = 300bars, T = 363K)
        Matrix PVT = ReadMatrix("PVT.txt");
        ColVec P_pvt = PVT[.., 0], Z_pvt = PVT[.., 1], mu_pvt = PVT[.., 2],
            cgr_pvt = PVT[.., 3], Shrinkage = PVT[.., 4], P_Z_pvt = P_pvt.Div(Z_pvt);
        double Pi = 300; // Initial reservoir pressure (psi)
        var z = 0.95; var Bgi = 0.00336*0.95*363/Pi;
        var rhog = 0.65*1.293*288*300/(363*0.95);

        ColVec cPress = CapPress[.., 0], sPress = CapPress[.., 1];
        double[] Sw = [..alllevels.Select(level => Interp1(cPress, sPress, Min(cPress.Max(), 9.8*(alllevels.Last() - level)*(1000 - rhog)*1e-5)))];
        for (int i = 0; i < alllevels.Length; i++) Console.WriteLine($"Level = {alllevels[i]}, Saturation = {Sw[i]}");
        double Swavg = Sw.Zip(allareas, (sw, area) => sw * area).Sum() / allareas.Sum();

        var NTG_P90 = NTG_P50 - 1.282*NTGstd;
        var NTG_P10 = NTG_P50 + 1.282*NTGstd;
        var Poro_P90 = Poro_P50 - 1.282*Porostd;
        var Poro_P10 = Poro_P50 + 1.282*Porostd;

        var GIIP_P50 = GrossRockVolume*NTG_P50*Poro_P50*(1-Swavg)/Bgi;
        var GIIP_P10 = GrossRockVolume*NTG_P10*Poro_P10*(1-Swavg)/Bgi;
        var GIIP_P90 = GrossRockVolume*NTG_P90*Poro_P90*(1-Swavg)/Bgi;


        // Production Data fro Merlin  
        Matrix ProductionData = ReadMatrix("Merlin Production History.txt");
        ColVec Time = ProductionData[.., 0], CumGasProd = ProductionData[.., 1], Pressure = ProductionData[.., 2];
        ColVec P_Z = Pressure.Select(p => p/Interp1(P_pvt, Z_pvt, p)).ToArray();
        // Use Regression to estimate the slope of P/Z vs CumGasProd, which is related to the initial gas in place.
        var parMerlin = Polyfit([.. CumGasProd], [.. P_Z], 1);
        var GIIP_Merlin = -parMerlin[1]/parMerlin[0];
        var RF_Merlin = CumGasProd.Last()/GIIP_Merlin;
        Figure(1000, 500);
        Scatter(CumGasProd, P_Z, "fob"); HoldOn();
        Plot([0, GIIP_Merlin], [parMerlin[1], 0], "k", 3);
        Scatter(GIIP_Merlin, 0, "fog", 20);
        Title("MerlinProdData"); Xlabel("CumGasProd"); Ylabel("P/Z");
        HoldOff(); SaveAs("MerlinProdData.png"); CloseFig();

        // Well Test Data
        Matrix TestData = ReadMatrix("Well Test.txt");
        ColVec Qipr = TestData[.., 0], Pipr = TestData[.., 1], Qipr_estimate;
        ColVec LnQ = Log(Qipr), DelP2 = Pi*Pi - Pipr.Pow(2), LnDelP2 = Log(DelP2);
        // Use Regression to estimate C and n in PressureSquared IPR
        var par = Polyfit([.. LnDelP2], [.. LnQ], 1);
        var n = par[0]; var C = Exp(par[1]);
        HoldOn();
        Scatter(Qipr, Pipr, "fob");
        Plot(Qipr_estimate = C*DelP2.Pow(n), Pipr, "r", 2);
        Xlabel("Q(m^3/day)"); Ylabel("P(bar)");
        Title($"C = {C}, and n = {n}");
        SaveAs("WellTest.png");

        // VFP data
        Matrix VFP = ReadMatrix("VFP.txt");
        ColVec Qvfp = VFP[.., 0], Pvfp20 = VFP[.., 1], Pvfp40 = VFP[.., 2], Pvfp60 = VFP[.., 3], Pvfp80 = VFP[.., 4], Pvfp100 = VFP[.., 5];
        Plot(Qvfp, VFP[.., 1..]);
        Legend(["", "IPR", "VFP_20", "VFP_40", "VFP_60", "VFP_80", "VFP_100"], LowerRight);
        

        var (nodq1, nodp1) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp100); Scatter(nodq1, nodp1, "fog");
        var (nodq2, nodp2) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp80); Scatter(nodq2, nodp2, "fog");
        var (nodq3, nodp3) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp60); Scatter(nodq3, nodp3, "fog");
        var (nodq4, nodp4) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp40); Scatter(nodq4, nodp4, "fog");
        var (nodq5, nodp5) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp20); Scatter(nodq5, nodp5, "fog");
        SaveAs("Nodal.png"); CloseFig();


        Plot(Qipr_estimate = 2*C*DelP2.Pow(n), Pipr, "r", 2);
        Xlabel("Q(m^3/day)"); Ylabel("P(bar)");
        Title($"C = {C}, and n = {n}");
        HoldOn();

        // VFP data
        Plot(Qvfp, VFP[.., 1..]);
        Legend(["", "IPR", "VFP_20", "VFP_40", "VFP_60", "VFP_80", "VFP_100"], LowerRight);


        var (nodq1e, nodp1e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp100); Scatter(nodq1e, nodp1e, "fog");
        var (nodq2e, nodp2e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp80); Scatter(nodq2e, nodp2e, "fog");
        var (nodq3e, nodp3e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp60); Scatter(nodq3e, nodp3e, "fog");
        var (nodq4e, nodp4e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp40); Scatter(nodq4e, nodp4e, "fog");
        var (nodq5e, nodp5e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp20); Scatter(nodq5e, nodp5e, "fog");
        SaveAs("Nodal2Wells.png"); CloseFig();

        // Comparing with Merlin
        double Pabn = 75, Zabn = Interp1(P_pvt, Z_pvt, Pabn), 
            Zi = Interp1(P_pvt, Z_pvt, Pi), pz_i = Pi / Zi;
        double RF = 1 - Pabn / Zabn / pz_i;

        double pwf_abandonment = Interp1(Qvfp, Pvfp20, 0.2e6);
        double pr_abandonment = Sqrt(Pow(0.2e6/C, 1/n) + pwf_abandonment*pwf_abandonment);

        // assuming tha tlike Merlin, Morgana will also undergoing volumentric depletion
        double RF2 = 1 - pr_abandonment / Interp1(P_pvt, Z_pvt, pr_abandonment) / pz_i;
        double EUR_P10 = GIIP_P10*RF, EUR_P50 = GIIP_P50*RF, EUR_P90 = GIIP_P90*RF;
        

        (ColVec Q, ColVec Pu, ColVec Pd, ColVec Thp, ColVec Cum, ColVec CumCond) 
            ProductionProfile(double EUR, double GIIP, double Nyears, double factor, int Nwells)
        {
            double Qplt = Min(1e6, factor * EUR/((Nyears)*365));
            double pr = Pi, cumprod = 0, pu, pd;
            double initcond = Interp1(P_pvt, cgr_pvt, pr)*GIIP/1e6;
            List<ColVec> PVFP = [Pvfp100, Pvfp60, Pvfp20];
            List<double> THP = [100, 60, 20];
            int pindx = 0; ColVec pwf;
            List<double> Q = [], Pu = [], Pd = [], Thp = [], Cum = [], CumCond = [];
            for (int i = 0; i <= Nyears; i++)
            {
                pwf = Linspace(pr, 0, 10);
                DelP2 = pr*pr - pwf.Pow(2);
                Qipr_estimate = Nwells * C*DelP2.Pow(n);
                pu = Interp1(Qipr_estimate, pwf, Qplt);
                pd = Interp1(Qvfp, PVFP[pindx], Qplt);
                if(pu < pd)
                {
                    if (pindx < 2)
                    {
                        pindx++;
                        pd = Interp1(Qvfp, PVFP[pindx], Qplt);
                    }
                    else
                    {
                        (Qplt, pu) = Intersection(Qipr_estimate, pwf, Qvfp, PVFP[pindx]);
                        pd = pu;
                    }
                }
                Q.Add(Qplt); Pu.Add(pu); Pd.Add(pd); Thp.Add(THP[pindx]); Cum.Add(cumprod); 
                CumCond.Add(initcond - (GIIP - cumprod)*Interp1(P_pvt, cgr_pvt, pr)/1e6);
                cumprod += Qplt*365;
                pr = Interp1(P_Z_pvt, P_pvt, pz_i*(GIIP - cumprod)/GIIP);
            }
            while(Qplt > 0.2e6)
            {
                cumprod += Qplt*365;
                pr = Interp1(P_Z_pvt, P_pvt, pz_i*(GIIP - cumprod)/GIIP); 
                pwf = Linspace(pr, 0, 10);
                DelP2 = pr*pr - pwf.Pow(2);
                Qipr_estimate = Nwells * C*DelP2.Pow(n);
                (Qplt, pu) = Intersection(Qipr_estimate, pwf, Qvfp, PVFP[pindx]);
                pd = pu;
                Q.Add(Qplt); Pu.Add(pu); Pd.Add(pd); Thp.Add(THP[pindx]); Cum.Add(cumprod);
                CumCond.Add(initcond - (GIIP - cumprod)*Interp1(P_pvt, cgr_pvt, pr)/1e6);
            }
            return (Q, Pu, Pd, Thp, Cum, CumCond);
        }

        (ColVec Q, ColVec Pu, ColVec Pd, ColVec Thp, ColVec Cum, ColVec CumCond) 
            EnforcePlateauDeclineCondition(double EUR, double GIIP, int Nyears, int Nwells, double maxD)
        {
            double f = 1;
            var (Q, Pu, Pd, Thp, Cum, CumCond) = ProductionProfile(EUR, GIIP, Nyears, f, Nwells);
            ColVec Ratio = Q[1..].Div(Q[..^1]);
            while (Q[Nyears] < Q[0] || Ratio.Any(r => r < (1 - maxD)))
            {
                f -= 0.01;
                (Q, Pu, Pd, Thp, Cum, CumCond) = ProductionProfile(EUR, GIIP, Nyears, f, Nwells);
                Ratio = Q[1..].Div(Q[..^1]);
            }
            return (Q, Pu, Pd, Thp, Cum, CumCond);
        }

        // High P90
        double  maxDeclineRate = 0.3; int Nwells = 2, NplatYears = 10; ColVec index;
        var (Q10, Pu10, Pd10, Thp10, Cum10, Cond10) = EnforcePlateauDeclineCondition(EUR_P10, GIIP_P10, NplatYears, Nwells, maxDeclineRate);
        var (Q50, Pu50, Pd50, Thp50, Cum50, Cond50) = EnforcePlateauDeclineCondition(EUR_P50, GIIP_P50, NplatYears, Nwells, maxDeclineRate);
        var (Q90, Pu90, Pd90, Thp90, Cum90, Cond90) = EnforcePlateauDeclineCondition(EUR_P90, GIIP_P90, NplatYears, Nwells, maxDeclineRate);

        for (int i = 0; i < Q10.Numel; i++)
            Console.WriteLine($"{i}, {Q10[i]/1e6:F4}, {Thp10[i]:F4}, {Cum10[i]/1e6:F4}, {Cond10[i]/1e6:F4}");
        Console.WriteLine("#################################################################################");
        for (int i = 0; i < Q50.Numel; i++)
            Console.WriteLine($"{i}, {Q50[i]/1e6:F4}, {Thp50[i]:F4}, {Cum50[i]/1e6:F4}, {Cond50[i]/1e6:F4}");
        Console.WriteLine("#################################################################################");
        for (int i = 0; i < Q90.Numel; i++)
            Console.WriteLine($"{i}, {Q90[i]/1e6:F4}, {Thp90[i]:F4}, {Cum90[i]/1e6:F4}, {Cond90[i]/1e6:F4}");
        Console.WriteLine("#################################################################################");


        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Q10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Q50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Q90, "r", 3);
        Xlabel("Time(years)"); 
        Ylabel("Production Rate(m^3/Day)");
        Title("Production Profile");
        Legend(["P10", "P50", "P90"]);
        SaveAs("Profile.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Cum10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Cum50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Cum90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Cumulative Production (m^3)");
        Title("Cumulative Production Profile");
        Legend(["P10", "P50", "P90"]);
        SaveAs("CumulativeProductionProfile.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Cond10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Cond50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Cond90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Cumulative Condensate Production (m^3)");
        Title("Cumulative Condensate Production Profile");
        Legend(["P10", "P50", "P90"]);
        SaveAs("CumulativeCondensateProductionProfile.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Pu10-Pd10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Pu50-Pd50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Pu90-Pd90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Choke Pressure Drop (bar)");
        Title("Choke Pressure Drop ");
        Legend(["P10", "P50", "P90"]);
        SaveAs("ChokePressureDrop.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Thp10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Thp50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Thp90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Tubing Head Pressure (bar)");
        Title("Tubing Head Pressure");
        Legend(["P10", "P50", "P90"]);
        SaveAs("TubingHeadPressure.png"); CloseFig();
    }


    //FormatLong();
    {
        Matrix A = new double[,]
        {
        { 0.1419,    0.6557,         0,         0,         0 },
        { 0.4218,    0.0357,    0.7431,         0,         0 },
        { 0,         0.8491,    0.3922,    0.2769,         0 },
        { 0,              0,    0.6555,    0.0462,    0.9502 },
        { 0,              0,         0,    0.0971,    0.0344 }
        };

        //A = A.T;
        Console.WriteLine($"Matrix A = {A}");
        var (U, S, V) = Svd(A, 5);
        //Console.WriteLine($"Matrix U = {U}");
        //Console.WriteLine($"Matrix S = {S}");
        //Console.WriteLine($"Matrix V = {V}");

        var A_recon = U*S*V.T;
        Console.WriteLine($"Reconstructed A = {A_recon}");
        //Console.WriteLine($"Reconstructed A = {A_recon.Full()}");
    }

    {
        SparseMatrix A = new double[,]
        {
        { 0.1419,    0.6557,         0,         0,         0 },
        { 0.4218,    0.0357,    0.7431,         0,         0 },
        { 0,         0.8491,    0.3922,    0.2769,         0 },
        { 0,              0,    0.6555,    0.0462,    0.9502 },
        { 0,              0,         0,    0.0971,    0.0344 }
        };

        //A = A.T;
        Console.WriteLine($"Matrix A = {A.Full()}");
        var (U, S, V) = Svd(A, 5);
        //Console.WriteLine($"Matrix U = {U.Full()}");
        //Console.WriteLine($"Matrix S = {S.Full()}");
        //Console.WriteLine($"Matrix V = {V.Full()}");

        var A_recon = U*S*V.T;
        Console.WriteLine($"Reconstructed A = {A_recon.Full()}");
        //Console.WriteLine($"Reconstructed A = {A_recon.Full()}");
    }

    {
        Matrix A = new double[,]
        {
        { 0.8147,    0.9134,    0.2785,    0.9649,    0.9572 },
        { 0.9058,    0.6324,    0.5469,    0.1576,    0.4854 },
        { 0.1270,    0.0975,    0.9575,    0.9706,    0.8003 }
        };

        Console.WriteLine($"Matrix A = {A}");
        var (U, S, V) = Svd(A);
        Console.WriteLine($"Matrix U = {U}");
        Console.WriteLine($"Matrix S = {S}");
        Console.WriteLine($"Matrix V = {V}");

        Console.WriteLine($"Matrix UTU = {U.T*U}");
        Console.WriteLine($"Matrix UUT = {U*U.T}");
        Console.WriteLine($"Matrix VTV = {V.T*V}");
        Console.WriteLine($"Matrix VVT = {V*V.T}");
    }
}


//static (double x1, double x2) bracket_minimum(Func<double, double> f, double x = 0, double s = 0.01, double k = 2.0)
//{
//    var (a, ya) = (x, f(x));
//    var (b, yb) = (a + s, f(a + s));
//    if (yb > ya)
//        (a, b, ya, yb, s) = (b, a, yb, ya, -s);

//    while (true)
//    {
//        var (c, yc) = (b + s, f(b + s));
//        if (yc > yb)
//            return a < c ? (a, c) : (c, a);
//        (a, ya, b, yb, s) = (b, yb, c, yc, s*k);
//    }
//}




//{
//    // Super heated water table
//    // Absolute Pressure (kPa)
//    double[] P = { 10, 50, 75, 100, 150, 400 };

//    // Temperature (°C)
//    double[] T = { 100, 150, 200, 250, 300, 360, 420, 500 };

//    // Specific Volume v (m³/kg)
//    double[,] v =
//        {
//            { 17.196, 19.512, 21.825, 24.136, 26.445, 29.216, 31.986, 35.679 },
//            { 3.418, 3.889, 4.356, 4.820, 5.284, 5.839, 6.394, 7.134 },
//            { 2.270, 2.587, 2.900, 3.211, 3.520, 3.891, 4.262, 4.755 },
//            { 1.6958, 1.9364, 2.172, 2.406, 2.639, 2.917, 3.195, 3.565 },
//            { double.NaN, 1.2853, 1.4443, 1.6012, 1.7570, 1.9432, 2.129, 2.376 },
//            { double.NaN, 0.4708, 0.5342, 0.5951, 0.6548, 0.7257, 0.7960, 0.8893 }
//        };

//    // Enthalpy h (kJ/kg)
//    double[,] h =
//        {
//            { 2687.5, 2783.0, 2879.5, 2977.3, 3076.5, 3197.6, 3320.9, 3489.1 },
//            { 2682.5, 2780.1, 2877.7, 2976.0, 3075.5, 3196.8, 3320.4, 3488.7 },
//            { 2679.4, 2778.2, 2876.5, 2975.2, 3074.9, 3196.4, 3320.0, 3488.4 },
//            { 2672.2, 2776.4, 2875.3, 2974.3, 3074.3, 3195.9, 3319.6, 3488.1 },
//            { double.NaN, 2772.6, 2872.9, 2972.7, 3073.1, 3195.0, 3318.9, 3487.6 },
//            { double.NaN, 2752.8, 2860.5, 2964.2, 3066.8, 3190.3, 3315.3, 3484.9 }
//        };

//    // Entropy s (kJ/kg·K)
//    double[,] s =
//        {
//            { 8.4479, 8.6882, 8.9038, 9.1002, 9.2813, 9.4821, 9.6682, 9.8978 },
//            { 7.6947, 7.9401, 8.1580, 8.3556, 8.5373, 8.7385, 8.9249, 9.1546 },
//            { 7.5009, 7.7496, 7.9690, 8.1673, 8.3493, 8.5508, 8.7374, 8.9672 },
//            { 7.3614, 7.6134, 7.8343, 8.0333, 8.2158, 8.4175, 8.6042, 8.8342 },
//            { double.NaN, 7.4193, 7.6433, 7.8438, 8.0720, 8.2293, 8.4163, 8.6466 },
//            { double.NaN, 6.9299, 7.1706, 7.3789, 7.5662, 7.7712, 7.9598, 8.1913 }
//        };
//    Console.WriteLine("==============================================================");
//    Console.WriteLine($"Enthalpy at P = {39}, T = {102} is {Interp2(P, T, h, 39, 102)}");
//    Console.WriteLine($"specific volume at P = {39}, T = {102} is {Interp2(P, T, v, 39, 102)}");
//    Console.WriteLine($"entropy at P = {39}, T = {102} is {Interp2(P, T, s, 39, 102)}");
//}

//{
//    // T: Temperature (°C)
//    double[] T = { 85, 90, 95, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190,
//        200, 210, 220, 230, 240, 250, 260, 270, 280, 290, 300, 320, 340, 360 };

//    // P: Pressure (bar)
//    double[] P = { 0.5783, 0.7013, 0.8455, 1.013, 1.433, 1.985, 2.701, 3.613, 4.758,
//        6.178, 7.916, 10.02, 12.54, 15.54, 19.06, 23.18, 27.95, 33.44, 39.73, 46.88,
//        54.98, 64.11, 74.36, 85.81, 112.7, 145.9, 186.5 };

//    // vf: Spec. Vol. Sat. Liquid (m³/kg) - Adjusted for X1000 scale
//    double[] v_f = { 0.001033, 0.001036, 0.001039, 0.001044, 0.001052, 0.001060,
//        0.001069, 0.001080, 0.001091, 0.001102, 0.001114, 0.001127, 0.001141,
//        0.001156, 0.001172, 0.001190, 0.001209, 0.001229, 0.001251, 0.001275,
//        0.001302, 0.001332, 0.001365, 0.001403, 0.001499, 0.001638, 0.001893 };

//    // vg: Spec. Vol. Sat. Vapor (m³/kg)
//    double[] v_g = { 2.828, 2.361, 1.982, 1.673, 1.21, 0.892, 0.669, 0.509, 0.393,
//        0.307, 0.243, 0.194, 0.157, 0.127, 0.104, 0.086, 0.072, 0.06, 0.05, 0.042,
//        0.036, 0.03, 0.026, 0.022, 0.015, 0.011, 0.007 };

//    // uf: Int. Energy Sat. Liquid (kJ/kg)
//    double[] u_f = { 355.8, 376.8, 397.9, 418.9, 461.1, 503.5, 546.0, 588.7, 631.7,
//        674.9, 718.3, 762.1, 806.2, 850.6, 895.5, 940.8, 986.7, 1033, 1080, 1128,
//        1177, 1227, 1279, 1332, 1445, 1570, 1725 };

//    // ug: Int. Energy Sat. Vapor (kJ/kg)
//    double[] u_g = { 2488, 2494, 2501, 2507, 2518, 2529, 2540, 2550, 2559, 2568,
//        2576, 2584, 2589, 2596, 2600, 2603, 2603, 2603, 2603, 2600, 2592, 2587,
//        2573, 2560, 2531, 2462, 2351 };

//    // hf: Enthalpy Sat. Liquid (kJ/kg)
//    double[] enthah_flpyLiq = { 355.9, 376.9, 398.0, 419.0, 461.3, 503.7, 546.3, 589.1,
//        632.2, 675.5, 719.2, 763.2, 807.6, 852.4, 897.8, 943.6, 990.1, 1037.3, 1085.3,
//        1134.4, 1184.5, 1236.0, 1289.0, 1344.0, 1461.5, 1594.1, 1760.5 };

//    // hg: Enthalpy Sat. Vapor (kJ/kg)
//    double[] h_g = { 2652, 2660, 2668, 2676, 2691, 2706, 2720, 2734, 2746, 2758, 2769,
//        2778, 2786, 2793, 2798, 2802, 2804, 2804, 2802, 2797, 2790, 2780, 2766, 2749,
//        2700, 2622, 2481 };

//    // sf: Entropy Sat. Liquid (kJ/kg*K)
//    double[] s_f = { 1.134, 1.193, 1.250, 1.307, 1.418, 1.528, 1.634, 1.739, 1.842, 1.943,
//        2.042, 2.140, 2.236, 2.331, 2.425, 2.518, 2.610, 2.702, 2.793, 2.884, 2.975, 3.067,
//        3.159, 3.253, 3.448, 3.659, 3.915 };

//    // sg: Entropy Sat. Vapor (kJ/kg*K)
//    double[] s_g = { 7.544, 7.479, 7.416, 7.355, 7.239, 7.130, 7.027, 6.930, 6.838, 6.750,
//        6.666, 6.586, 6.508, 6.432, 6.358, 6.286, 6.215, 6.144, 6.073, 6.002, 5.930, 5.857,
//        5.782, 5.704, 5.536, 5.336, 5.053 };

//    // Lets compute all themophysical parameters at temp T = 257C
//    Console.WriteLine($"Pressure at T = 257 = {Interp1(T, P, 257)}");
//    Console.WriteLine($"Gas Entrophy at T = 257 = {Interp1(T, s_g, 257)}");
//    Console.WriteLine($"Liquid Enthropy at T = 257 = {Interp1(T, s_f, 257)}");
//    Console.WriteLine($"Gas Specific Volume at T = 257 = {Interp1(T, v_g, 257)}");
//}