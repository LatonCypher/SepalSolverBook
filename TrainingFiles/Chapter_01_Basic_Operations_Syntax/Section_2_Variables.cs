namespace ConsoleApp1.TrainingFiles.Chapter_0_Basic_Operations_Syntax
{
    public class Section_2_Variables
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// </BookContent>

            // Variables in C# are used to store data that can be manipulated during the execution of a program. 
            {
                //Declaration: You declare a variable by specifying its type followed by its name. For example
                int age;
                //Console.WriteLine(age);
            }

            {
                // Initialization: You can initialize a variable at the time of declaration or later in the code. For example
                int age = 25;
                Console.WriteLine(age);
            }


            // * Scope: The scope of a variable is the region of the code where the variable is accessible.
            //   Variables can be local(within a method), instance(within a class), or
            //   static (shared across all instances of a class).

            // * Lifetime: The lifetime of a variable refers to the duration for which the variable exists
            //   in memory.Local variables exist only during the execution of the method, while instance
            //   variables exist as long as the object exists.


            // Constants
            // * If you want a variable to be immutable (unchangeable), you can declare it as a constant using the const
            {
                const int MAX_AGE = 100;
                // MAX_AGE = 5;
            }

            // Type Inference
            // * C# supports type inference using the var keyword, allowing the compiler to determine the type of the variable based on the assigned value:
            {
                var name = "John"; // Compiler infers 'name' as a string
                var age = 5.2;
                
                Console.WriteLine(name.GetType());
            }

            // Nullable Types
            // * C# allows variables to be nullable, meaning they can hold a value or be null. This is useful for reference types and value types:
            {
                int? nullableInt = null;
            }
        }
    }
}
