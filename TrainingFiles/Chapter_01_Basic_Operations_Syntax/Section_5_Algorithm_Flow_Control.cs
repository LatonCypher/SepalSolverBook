namespace ConsoleApp1.TrainingFiles.Chapter_0_Basic_Operations_Syntax
{
    public class Section_5_Algorithm_Flow_Control
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// </BookContent>
            //Sequence
            {
                int a = 10;
                int b = 20;
                int c = a + b;
                Console.WriteLine($"The sum of {a} and {b} is {c}");
            }
            //Selection(if-else if-else)
            {
                int a = 10;
                if (a > 0)
                {
                    Console.WriteLine("a is positive");
                }
                else if (a < 0)
                {
                    Console.WriteLine("a is negative");
                }
                else
                {
                    Console.WriteLine("a is zero");
                }
            }
            //Selection(switch-case)
            {
                int a = 10;
                switch (a)
                {
                    case 0:
                        Console.WriteLine("a is zero");
                        break;
                    case 1:
                        Console.WriteLine("a is one");
                        break;
                    case 2:
                        Console.WriteLine("a is two");
                        break;
                    default:
                        Console.WriteLine("a is greater than two");
                        break;
                }
            }

            //Iteration(while)
            {
                int a = 10;
                while (a > 0)
                {
                    Console.WriteLine($"a is {a}");
                    a--;
                }
            }

            //Iteration(do-while)
            {
                int a = 10;
                do
                {
                    Console.WriteLine($"a is {a}");
                    a--;
                } while (a > 0);
            }

            //Iteration(for)
            {
                for (int a = 10; a > 0; a--)
                {
                    Console.WriteLine($"a is {a}");
                }
            }

            //Iteration(foreach)
            {
                List<int> numbers = [1, 2, 3, 4, 5];
                foreach (int number in numbers)
                {
                    Console.WriteLine($"Number is {number}");
                }
            }
        }
    }
}
