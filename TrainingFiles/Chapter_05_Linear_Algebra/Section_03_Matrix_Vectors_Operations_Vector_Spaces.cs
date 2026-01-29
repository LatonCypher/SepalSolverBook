using ScottPlot.DataViews;
using ScottPlot.PlotStyles;
using SepalSolver;
using System.Xml.Linq;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algerba
{
    internal class Section_03_Matrix_Vectors_Operations_Vector_Spaces
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> Arithemetic Operations on Vectors and Matrices </header>
            /// The SepalSolver comes with overloaded operators to allow easy arithmetic operations on Vectors and matrices. 
            /// Vector and Matrix addition and subtraction when conformable can easily be achieved by using the "+" and "-" signs respectively. 
            /// Addition and subtraction of column and row vectors invokes their broadcast in the direction of each other (I mean along their first singleton).  
            /// for matrices, conformity requires that they have exactly the same dimensions. Vectors can be added to matrices. The dimension of the vector has to match that of the matrix. ie. for a ColVec-Matrix Operation, the number of rows must be equal and the ColVec is broadasted to have the same number of columns as the Matrix. 
            /// RowVec are treated the same way. The number of columns in the RowVec must be equal to the number of columns in the Matrix, and the RowVec is broadcasted to have the same number of rows as the Matrix. 
            /// This rules is also apply when carrying out termwise operations between ColVec and RowVec, ColVec and Matrix, and RowVec and Matrix.
            /// Aside the rules, the standard conformity rules applies. 
            /// This table provides the list of operators in Matlab and the corresponding operators in SepalSolver
            /// 
            /// <table>
            /// **Operation**            | **MATLAB Syntax** |  **SepalSolver Syntax**
            ///    Addition              |      ``A+B``      |       ``A + B``
            ///    Subtraction           |      ``A-B``      |       ``A - B``
            ///    Matrix Multiplication |      ``A*B``      |       ``A * B``
            ///    Matrix Left Division  |      ``A\B``      |    ``Mldivide(A, B)``
            ///    Matrix Right Division |      ``A/B``      |    ``Mrdivide(A, B)``
            ///    Element-wise Mult.    |      ``A.*B``     |      ``A.Times(B)``
            ///    Element-wise Div.     |      ``A./B``     |      ``A.Div(B)``
            ///    Element-wise Power    |      ``A.^B``     |      ``A.Pow(B)``
            /// </table>
            /// 
            /// <header 3> Examples </header>
            /// <code>
            {
                // Declarations
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };

                Matrix B = new double[,] { { 9, 8, 7 },
                                           { 6, 5, 4 },
                                           { 3, 2, 1 } };

                ColVec U = new double[] { 1, 2, 3 };
                RowVec P = new double[] { 4, 5, 6 };


                // Matrix-Matrix Addition
                Matrix C = A + B;
                Console.WriteLine($"A + B = \n{C}");

                // Matrix-Matrix Subtraction
                Matrix D = A - B;
                Console.WriteLine($"A - B = \n{D}");

                // Matrix-matrix Multiplication
                Matrix E = A * B; // 
                Console.WriteLine($"E = \n{E}");
                Matrix F = B * A;
                Console.WriteLine($"F = \n{F}");

                // Matrix-Matrix Division
                Matrix G = Mldivide(A, B); // A\B
                Console.WriteLine($"G = \n{G}");
                Matrix H = Mrdivide(A, B); // A/B
                Console.WriteLine($"H = \n{H}");


                Matrix I = A.Div(B);
                Console.WriteLine($"I = \n{I}");

                // Power (A.^B)
                Matrix J = A.Pow(B);
                Console.WriteLine($"J = \n{J}");

                // Power (B.^A)
                Matrix K = B.Pow(A);
                Console.WriteLine($"B.^A = B.Pow(A) = \n{K}");

            }
            /// </code>
            /// 
            /// </BookContent>
            // 





            


            // Matrix transpose
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = A.T;
                Console.WriteLine($"A^T = \n{B}");
            }

            // Matrix inverse
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = A.Inverse();
                Console.WriteLine($"A^-1 = \n{B}");
            }

            // Matrix determinant
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                double det = A.Det();
                Console.WriteLine($"det(A) = {det}");
            }

            // Matrix Rref
            {
                Matrix A = new double[,] { { 8,    1,    6,    1,  16 },
                                           { 3,    5,    6,    1,  15 },
                                           { 4,    7,    2,    1,  14 } };
                (Matrix R, Indexer P, Matrix N) = A.Rref();
                Console.WriteLine("\n A = \n" + A
                                + "\n R = \n" + R
                                + "\n P = \n" + P
                                + "\n N = \n" + N);
            }
        }
    }
}
