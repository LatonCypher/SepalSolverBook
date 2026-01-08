namespace ConsoleApp1.TrainingFiles.Chapter_0_Basic_Operations_Syntax
{
    public class Section_3_Operators
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// </BookContent>
            // There are six broad catergories of operators
            {
                //1.  Arithmetic Operators

                // + Addition
                // - Subtraction
                // * Multiplication
                // / Division
                // % Modulus

                // Examples
                int a = 25, b = 17;
                Console.WriteLine($"sum = {a + b}");
                Console.WriteLine($"diff = {a - b}");
                Console.WriteLine($"mult = {a * b}");
                Console.WriteLine($"div = {a / b}");
                Console.WriteLine($"mod = {a % b}");
            }

            {
                //2.  Assignment Operators

                // = Assign
                // += Add and assign
                // -= Subtract and assign
                // *= Multiply and assign
                // /= Divide and assign
                // %= Divide and assign


                // Examples
                int a = 25, b = 17;
                a += b;
                Console.WriteLine($"a = {a}");
                Console.WriteLine("==================");
                a -= b;
                Console.WriteLine($"a = {a}");
                Console.WriteLine("==================");
                a *= b;
                Console.WriteLine($"a = {a}");
                Console.WriteLine("==================");
                a /= b;
                Console.WriteLine($"a = {a}");
                Console.WriteLine("==================");
                a %= b;
                Console.WriteLine($"a = {a}");
                Console.WriteLine("==================");
            }

            {
                //3.  Comparison Operators

                //== Equal to
                //!= Not equal to
                //> Greater than
                //<Less than
                //>= Greater than or equal to
                //<= Less than or equal to

                // Examples
                int a = 25, b = 17;
                Console.WriteLine($"Test1 = {a == b}");
                Console.WriteLine("==================");
                Console.WriteLine($"Test2 = {a != b}");
                Console.WriteLine("==================");
                Console.WriteLine($"Test3 = {a < b}");
                Console.WriteLine("==================");
                Console.WriteLine($"Test4 = {a > b}");
                Console.WriteLine("==================");
                Console.WriteLine($"Test5 = {a <= b}");
                Console.WriteLine("==================");
                Console.WriteLine($"Test6 = {a >= b}");
                Console.WriteLine("==================");
            }

            {
                //4.  Logical Operators

                // && Logical AND
                // || Logical OR
                // ! Logical NOT

                // Example (Check if 3 is greater than 4 and less than 5)
                var test1 = 3 > 4 && 3 < 5;

                // Example (Check if 3 is greater than 4 or less than 5)
                var test2 = 3 > 4 || 3 < 5;

                // Example (Check if 3 not less than 4)
                var test3 = !(3 < 4 );
                var test4 = 3 >= 4;

            }

            {
                //5.  Bitwise Operators

                // & AND
                // | OR
                // ^ XOR
                // ~NOR
                // << Left shift
                // >> Right shift

                // Example (Check if 3 is greater than 4 and less than 5)
                var test1 = 3 > 4 & 3 < 5;

                // Example (Check if 3 is greater than 4 or less than 5)
                var test2 = 3 > 4 | 3 < 5;

                // Example (Check if one of 3 is greater than 4 or is less than 5)
                var test3 = 3 > 4 ^ 3 < 5;

                var a = 5;
                a = a >> 2; // Bit shift to the left by 2 positions
                Console.WriteLine($"a = {a}");
                a = a << 2; // Bit shift to the right by 2 positions
                Console.WriteLine($"a = {a}");
            }

            {
                //6.  Other Operators

                // Ternary Operator: condition? true_value : false_value
                // Null-coalescing Operator: ??
                // Type Check: is, as
                // Increment/Decrement: ++, --

                // Example (Print Positive if x is greater than 0, and negative if its less)
                int x = 5;
                Console.WriteLine(x > 0 ? "Positive" : "Negative");
                int y = -5;
                Console.WriteLine(y > 0 ? "Positive" : "Negative");

                Console.WriteLine(x++);
                Console.WriteLine(++x);


                Console.WriteLine(--y);
                Console.WriteLine(y--);
            }
        }
    }
}
