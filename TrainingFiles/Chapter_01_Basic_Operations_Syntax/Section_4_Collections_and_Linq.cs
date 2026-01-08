namespace ConsoleApp1.TrainingFiles.Chapter_0_Basic_Operations_Syntax
{
    public class Section_4_Collections_and_Linq
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// </BookContent>
            //using Math
            ColVec x = Linspace(0, 10);
            ColVec y = Sin(x);
            Plot(x, y);
            hold = true;
            ColVec z = BesselJ(0, x);
            Plot(x, z);

            Xlabel("x-axis");
            Ylabel("y-axis");
            Title("Sin and BesselJ");
            Legend(["Sin", "BesselJ"]);

            SaveAs("PlotExample.png");

            //// Example of using a Array
            //int[] ArrayOfIntegers = [1, 2, 3, 4]; //Array of integers
            //double[] ArrayOfDoubles = [4, 5, 6]; //Array of doubles

            //// Example of using a List
            //List<int> ListOfIntegers = [3, 7, 1, 2]; //List of Integers
            //List<int> ListOfDoubles = [6, 3, 4, 5]; //List of doubles

            //// Example of using a arrays
            //int[] arr = [5, 3, 8, 1, 2];
            //Console.WriteLine("Array elements: " + string.Join(", ", arr));

            //// Example of using LINQ to filter data
            //int[] numbers = [1, 2, 3, 4, 5];
            //var evenNumbers = numbers.Where(n => n % 2 == 0).ToArray();
            //Console.WriteLine("Even numbers: " + string.Join(", ", evenNumbers));

            //// Example of using LINQ to Transform data
            //int[] x = [1, 2, 3, 4, 5];
            //int[] xsquared = x.Select(n => n*n).ToArray();
            //Console.WriteLine("Squared numbers: " + string.Join(", ", xsquared));
        }
    }
}
