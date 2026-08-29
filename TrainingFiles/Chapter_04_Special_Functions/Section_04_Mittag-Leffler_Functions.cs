using ScottPlot;
using ScottPlot.ArrowShapes;
using ScottPlot.TickGenerators.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1.TrainingFiles.Chapter_04_Special_Functions
{
    internal class Section_4_MetagLefla_Functions
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// If the Gamma function is the generalization of the factorial, the Mittag-Leffler function is the generalization of the exponential function. It is the "crown jewel" of fractional calculus.
            /// 
            /// <header 2> Definition and Origin </header>
            ///
            /// The Mittag-Leffler function :math:`E_{\alpha, \beta}(z)` is defined by the following power series
            /// for :math:`\alpha > 0`:
            ///
            /// <math>
            ///    E_{\alpha, \beta}(z) = \sum_{k=0}^{\infty} \frac{z^k}{\Gamma(\alpha k + \beta)}
            /// </math>
            /// 
            /// * When :math:`\alpha = 1, \beta = 1`, it becomes the standard exponential: :math:`E_{1,1}(z) = e^z`.
            /// 
            /// * When :math:`\alpha = 2, \beta = 1`, it describes hyperbolic cosines: :math:`E_{2,1}(z^2) = \cosh(z)`.
            ///
            ///
            ///
            /// <header 2> Physical Significance: Fractional Calculus </header>
            ///
            /// While the standard exponential function describes "normal" relaxation
            /// (like a cooling cup of coffee), the Mittag-Leffler function describes
            /// "anomalous" relaxation.
            ///
            /// * Viscoelasticity: Used to model materials that are halfway between a liquid and a solid (like polymers or human tissue).
            /// * Fractional Diffusion: Describes how particles move in crowded environments (like proteins moving inside a biological cell).
            ///
            /// <header 2> 3. Implementation in SepalSolver </header>
            ///
            /// Unlike other scientific computing tools like matlab, in sepalsolver, the Mittag-Leffler function is
            /// not part of the sepcial function library. And it is exposed in the SepalSolver.Math class. 
            ///
            /// <code></code>
            {
                //Plotting E_{ a, 1}  (z) for varying alpha
                ColVec z = Linspace(0, 4);
                double[] alp = [0.5, 0.8, 1.0, 1.2];
                Matrix Y = alp.Select(a => Arrayfun(x => MettagLeffler(a, 1, x), z)).ToList();
                Plot(z, Y);
                Legend(alp.Select(a=>"E_{" + a +",1}(x)"));
                Title("Mittag-Leffler Function E_{\alpha, 1}(z)");
            }
            /// </code>
            /// <header 2> Key Properties </header>
            ///
            /// * Interpolation: It interpolates between a pure exponential and a power-law function.
            /// * Laplace Transform: The Laplace transform of :math:E_{\alpha}( -at^\alpha ) is :math:`\frac{s^{\alpha-1}}{s^\alpha + a}`, which is vital for solving fractional differential equations.
            ///
            ///
            /// </BookContent>
        }
    }
}
