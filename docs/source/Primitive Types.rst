Primitive Types
============================


Primitive Types in C# 
-------------------------
In C#, primitive types are the basic data types provided by the language. 
The primitive (built-in) types in C# include bool, byte, sbyte, short, 
ushort, int, uint, long, ulong, char, float, double, decimal, nint, nuint, 
object, string, and dynamic

.. list-table:: List of Primitive Types 
   :header-rows: 1

   * - C# Keyword 
     -  .NET Type        
     -  Category       
     -  Description                          
   * - bool       
     -  System.Boolean   
     -  Value type     
     -  True/false values                    
   * - byte       
     -  System.Byte      
     -  Value type     
     -  8-bit unsigned integer (0–255)       
   * - sbyte      
     -  System.SByte     
     -  Value type     
     -  8-bit signed integer (−128–127)      
   * - short      
     -  System.Int16     
     -  Value type     
     -  16-bit signed integer                
   * - ushort     
     -  System.UInt16    
     -  Value type     
     -  16-bit unsigned integer              
   * - int        
     -  System.Int32     
     -  Value type     
     -  32-bit signed integer                
   * - uint       
     -  System.UInt32    
     -  Value type     
     -  32-bit unsigned integer              
   * - long       
     -  System.Int64     
     -  Value type     
     -  64-bit signed integer                
   * - ulong      
     -  System.UInt64    
     -  Value type     
     -  64-bit unsigned integer              
   * - nint       
     -  System.IntPtr    
     -  Value type     
     -  Native-sized signed integer          
   * - nuint      
     -  System.UIntPtr   
     -  Value type     
     -  Native-sized unsigned integer        
   * - char       
     -  System.Char      
     -  Value type     
     -  Single 16-bit Unicode character      
   * - float      
     -  System.Single    
     -  Value type     
     -  32-bit floating-point number         
   * - double     
     -  System.Double    
     -  Value type     
     -  64-bit floating-point number         
   * - decimal    
     -  System.Decimal   
     -  Value type     
     -  128-bit precise decimal (financial)  
   * - object     
     -  System.Object    
     -  Reference type 
     -  Base type for all objects            
   * - string     
     -  System.String    
     -  Reference type 
     -  Sequence of characters               
   * - dynamic    
     -  System.Object    
     -  Reference type 
     -  Type resolved at runtime             


.. code-block:: Integer Types

   {
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
   }
   
   

.. code-block:: Floating-Point Types

   {
       // * float: Single-precision floating-point.
       { float a = 19; float b = -5; }
       // * double: Double-precision floating-point.
       { double a = 19; double b = -5; }
   }
   
   

.. code-block:: Character Type

   {
       // * char: Represents a single 16-bit Unicode character. 
       { char a = 'c'; }
   }



.. code-block:: String Type

   {
       // * string: Represents a sequence of characters. It's immutable, meaning once created, its value cannot be changed.
       { string str = "string"; }
   }
   
   

.. code-block:: Boolean Type

   {
       // * bool: Represents a boolean value(true or false).
       { bool T = true, F = false; }
   }
   
   

.. code-block:: Decimal Type

   {
       // * decimal: High-precision decimal type, typically used for financial calculations.
       { decimal d = 0.303856569382726564575M; }
   }
   
   
   
These primitive types are the building blocks for more complex data structures and are essential for various operations in C#.
   
Inbuilt Derived Types in C# 
-------------------------
In C#, beyond the primitive types (like int, bool, char), there are several inbuilt 
derived types that the language provides out of the box. These are types that are not 
primitives themselves but are built into the framework and derive from other base 
classes (usually System.Object).

.. list-table:: List of Inbuilt Derived Types
   :header-rows: 1

   * - Type              
     -  Base Class       
     -  Category       
     -  Description                                             
   * - string            
     -  System.Object    
     -  Reference type 
     -  Represents a sequence of characters (immutable)      
   * - object            
     -  Root of all types
     -  Reference type 
     -  Base type for all classes in C#.                     
   * - dynamic           
     -  System.Object    
     -  Reference type 
     -  Type resolved at runtime.                            
   * - Array             
     -  System.Object    
     -  Reference type 
     -  Base class for all arrays (e.g., int[], string[]).   
   * - Delegate          
     -  System.Object    
     -  Reference type 
     -  Base class for all delegates (function pointers).    
   * - MulticastDelegate 
     -  Delegate         
     -  Reference type 
     -  Supports invocation of multiple methods.             
   * - Enum              
     -  System.ValueType 
     -  Value type     
     -  Base class for all enumerations.                     
   * - ValueType         
     -  System.Object    
     -  Value type     
     -  Base class for all structs.                          


