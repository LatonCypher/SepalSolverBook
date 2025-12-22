namespace ConsoleApp1.
          TrainingFiles.
          Chapter_0_Basic_Operations_Syntax
{
    public class Section_1_Primitive_Types
    {
        public static void Run()
        {
            // In C#, primitive types are the basic data types provided by the language. Here is a list of all the primitive types:

            {

                // Integral Types
                
                // * byte: 8-bit unsigned integer.
                { byte a = 19; }
                // * sbyte: 8-bit signed integer.
                { sbyte a = 19; sbyte b = -5; }
                // * short: 16-bit signed integer.
                { short a = 19; short b = -5; }
                // * ushort: 16-bit unsigned integer.
                { ushort a = 19; }
                // * int: 32-bit signed integer.
                { int a = 19; int b = -5; }
                // * uint: 32-bit unsigned integer.
                { uint a = 19; }
                // * long: 64-bit signed integer.
                { long a = 19; long b = -5; }
                // * ulong: 64-bit unsigned integer.
                { ulong a = 19; }

                // Floating-Point Types
                // * float: Single-precision floating-point.
                { float a = 19; float b = -5; }
                // * double: Double-precision floating-point.
                { double a = 19; double b = -5; }

                // Character Type
                // * char: Represents a single Unicode character.
                { char a = 'c'; }

                // String Type
                // * string: Represents a sequence of characters. It's immutable, meaning once created, its value cannot be changed.
                { string str = "string"; }


                // Boolean Type
                // * bool: Represents a boolean value(true or false).
                { bool T = true, F = false; }


                // Decimal Type
                // * decimal: High-precision decimal type, typically used for financial calculations.
                { decimal d = 0.303856569382726564575M; }


                // These primitive types are the building blocks for more complex data structures and are essential for various operations in C#.
            }


        }
    }
}
