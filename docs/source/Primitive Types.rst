<header 2> 
Primitive Types in C# 
</header 2>
In C#, primitive types are the basic data types provided by the language. 
The primitive (built-in) types in C# include bool, byte, sbyte, short, 
ushort, int, uint, long, ulong, char, float, double, decimal, nint, nuint, 
object, string, and dynamic

+-------------------------+
| List of Primitive Types |
+-------------------------+
| C# Keyword              |
+-------------------------+
| bool                    |
+-------------------------+
| byte                    |
+-------------------------+
| sbyte                   |
+-------------------------+
| short                   |
+-------------------------+
| ushort                  |
+-------------------------+
| int                     |
+-------------------------+
| uint                    |
+-------------------------+
| long                    |
+-------------------------+
| ulong                   |
+-------------------------+
| nint                    |
+-------------------------+
| nuint                   |
+-------------------------+
| char                    |
+-------------------------+
| float                   |
+-------------------------+
| double                  |
+-------------------------+
| decimal                 |
+-------------------------+
| object                  |
+-------------------------+
| string                  |
+-------------------------+
| dynamic                 |
+-------------------------+


<code> Integer Types
            .. code-block:: csharp

   {
                   // * byte: 8-bit unsigned integer.
                   { byte a = 19; }
                // * sbyte: 8-bit signed integer.
                .. code-block:: csharp

   { sbyte a = 19; sbyte b = -5; }
                // * short: 16-bit signed integer.
                .. code-block:: csharp

   { short a = 19; short b = -5; }
                // * ushort: 16-bit unsigned integer.
                .. code-block:: csharp

   { ushort a = 19; }
                // * int: 32-bit signed integer.
                .. code-block:: csharp

   { int a = 19; int b = -5; }
                // * uint: 32-bit unsigned integer.
                .. code-block:: csharp

   { uint a = 19; }
                // * long: 64-bit signed integer.
                .. code-block:: csharp

   { long a = 19; long b = -5; }
                // * ulong: 64-bit unsigned integer.
                .. code-block:: csharp

   { ulong a = 19; }
            }
</code>


<code> Floating-Point Types
            .. code-block:: csharp

   {
                   // * float: Single-precision floating-point.
                   { float a = 19; float b = -5; }
                // * double: Double-precision floating-point.
                .. code-block:: csharp

   { double a = 19; double b = -5; }
            }
</code>


<code> Character Type
            .. code-block:: csharp

   {
                   // * char: Represents a single Unicode character.
                   { char a = 'c'; }
            }
</code>


<code> String Type
            .. code-block:: csharp

   {
                   // * string: Represents a sequence of characters.
                   // It's immutable, meaning once created, its value cannot be changed.
                   { string str = "string"; }
            }
</code>


<code> Boolean Type
            .. code-block:: csharp

   {
                   // * bool: Represents a boolean value(true or false).
                   { bool T = true, F = false; }
            }
</code>


<code> Decimal Type
            .. code-block:: csharp

   {
                   // * decimal: High-precision decimal type, typically used for financial calculations.
                   { decimal d = 0.303856569382726564575M; }
            }
</code>

These primitive types are the building blocks for more complex data structures
and are essential for various operations in C#.

<header 2> 
Inbuilt Derived Types in C# 
</header 2>
In C#, beyond the primitive types (like int, bool, char), there are several inbuilt 
derived types that the language provides out of the box. These are types that are not 
primitives themselves but are built into the framework and derive from other base 
classes (usually System.Object).

+-------------------------------+
| List of Inbuilt Derived Types |
+-------------------------------+
| Type                          |
+-------------------------------+
| string                        |
+-------------------------------+
| object                        |
+-------------------------------+
| dynamic                       |
+-------------------------------+
| Array                         |
+-------------------------------+
| Delegate                      |
+-------------------------------+
| MulticastDelegate             |
+-------------------------------+
| Enum                          |
+-------------------------------+
| ValueType                     |
+-------------------------------+
| Nullable <T>                  |
+-------------------------------+


            