using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling.Chapter_12_Reservoir_Simulation
{
    internal class Section_1_Rock_and_Fluid_Properties_Averaging
    {
        public static void Run()
        {
            /// <BookContent>
            /// ============================================================
            /// Averaging Rock and Fluid Properties
            /// ============================================================
            ///
            /// <header 2> Introduction </header 2>
            ///
            /// In reservoir simulation, we must "upscale" geological data into grid blocks. Because reservoirs are heterogeneous, a single grid block represents multiple geological layers. To maintain physical accuracy, we use specific averaging techniques derived from fundamental physical laws.
            ///
            /// <header 3> 1. Porosity Averaging (Arithmetic) </header 3>
            /// 
            /// **Physical Principle:** Conservation of Mass (Volume).
            /// 
            /// Porosity is a capacity property. The total pore volume in a system is the sum of the pore volumes of its constituents.
            /// 
            /// Derivation
            /// ----------
            /// 
            /// 1. Define total pore volume as the sum of individual pore volumes:
            /// 
            /// <math>
            ///     V_{p,total} = V_{p,1} + V_{p,2} + \dots + V_{p,n}
            /// </math>
            ///         
            /// 2. Substitute the definition :math:`\phi = V_p / V_b` (where :math:`V_b` is bulk volume):
            /// 
            /// <math>
            ///     \phi_{avg} V_{b,total} = \phi_1 V_{b,1} + \phi_2 V_{b,2} + \dots + \phi_n V_{b,n}
            /// </math>
            ///    
            ///         
            /// 3. Solve for :math:`\phi_{avg}`:
            /// 
            /// <math>
            ///     \phi_{avg} = \frac{\sum_{i=1}^{n} \phi_i V_{b,i}}{\sum_{i=1}^{n} V_{b,i}}
            /// </math>
            ///         
            /// **Conclusion:** Porosity is always averaged using the **Volume-Weighted Arithmetic Mean**.
            /// 
            /// 
            /// <header 3> 2. Permeability Averaging (Parallel Flow) </header 3>
            /// **Physical Principle:** Conservation of Flow (Total flow is the sum of layer flows).
            /// 
            /// This applies to horizontal flow along bedding planes.
            /// Derivation
            /// ----------
            /// 1. In a parallel system, the pressure drop (:math:`\Delta P`) and length (:math:`L`) are identical for all layers. The total flow rate (:math:`q_t`) is the sum of individual rates:
            ///     
            /// <math>
            ///     q_t = q_1 + q_2 + \dots + q_n
            /// </math>
            ///    
            ///         
            /// 2. Substitute **Darcy’s Law** :math:`q = \frac{k A \Delta P}{\mu L}`:
            /// 
            /// <math>
            ///     \frac{k_{avg} A_t \Delta P}{\mu L} = \frac{k_1 A_1 \Delta P}{\mu L} + \frac{k_2 A_2 \Delta P}{\mu L} + \dots
            /// </math>
            ///         
            /// 3. Cancel common terms (:math:`\Delta P, \mu, L`):
            /// 
            /// <math>
            ///     k_{avg} A_t = k_1 A_1 + k_2 A_2 + \dots + k_n A_n
            /// </math>
            ///    
            ///       
            ///         
            /// 4. For layers of constant width :math:`w`, then :math:`A = h \cdot w`. Dividing by :math:`w`:
            /// 
            /// <math>
            ///     k_{avg} = \frac{\sum k_i h_i}{\sum h_i}
            /// </math>
            /// 
            /// **Conclusion:** Parallel flow uses the **Arithmetic Mean**, dominated by high-permeability "thief zones."
            /// 
            /// <header 3> 3. Permeability Averaging (Series Flow) </header 3>
            /// 
            /// **Physical Principle:** Summation of Potential (Total pressure drop is the sum of layer drops).
            ///
            /// This applies to vertical flow across layers or flow between adjacent grid blocks.
            /// Derivation
            /// ----------
            ///
            /// 1. Flow rate (:math:`q`) and area (:math:`A`) are constant. Total pressure drop (:math:`\Delta P_t`) is the sum of drops across each block:
            /// 
            /// <math>
            ///     \Delta P_t = \Delta P_1 + \Delta P_2 + \dots + \Delta P_n
            /// </math>
            ///       
            /// 
            /// 2. Rearrange Darcy’s Law for :math:`\Delta P`:
            /// 
            /// <math>
            ///     \frac{q \mu L_t}{k_{avg} A} = \frac{q \mu L_1}{k_1 A} + \frac{q \mu L_2}{k_2 A} + \dots
            /// </math>
            ///       
            ///         
            /// 3. Cancel common terms (:math:`q, \mu, A`):
            ///     
            /// <math>
            ///     \frac{L_t}{k_{avg}} = \frac{L_1}{k_1} + \frac{L_2}{k_2} + \dots + \frac{L_n}{k_n}
            /// </math>
            ///         
            /// 4. Solve for :math:`k_{avg}`:
            ///     
            /// <math>
            ///     k_{avg} = \frac{\sum L_i}{\sum (L_i / k_i)}
            /// </math>
            ///    
            /// 
            /// **Conclusion:** Series flow uses the **Harmonic Mean**, dominated by the lowest permeability (bottlenecks).
            /// 
            /// <header 3> 4. Fluid Property Averaging (Saturation) </header 3>
            /// 
            /// Fluid saturations (:math:`S_w, S_o, S_g`) are fractions of the pore volume.
            /// 
            /// Derivation
            /// ----------
            /// 
            /// 1. Total water volume (:math:`V_w`) is the sum of volumes in constituent parts:
            /// 
            /// <math>
            ///     V_{w,total} = \sum (S_{wi} \cdot V_{pi})
            /// </math> 
            ///    
            /// 
            /// 2. Since :math:`V_{w,total} = S_{w,avg} \cdot V_{p,total}`:
            /// 
            /// <math>
            ///     S_{w,avg} = \frac{\sum (S_{wi} V_{pi})}{\sum V_{pi}}
            /// </math> 
            ///    
            ///         
            /// <header 3> Summary Table </header 3>
            /// <table>
            ///  Property          | Configuration   | Averaging Method                       
            ///  **Porosity**      | Any             | **Arithmetic** (Volume Weighted)       
            ///  **Permeability**  | Parallel Flow   | **Arithmetic** (Thickness Weighted)    
            ///  **Permeability**  | Series Flow     | **Harmonic** (Length Weighted)        
            ///  **Saturation**    | Any             | **Arithmetic** (Pore-Volume Weighted)  
            /// </table>
            /// </BookContent>
        }
    }
}
