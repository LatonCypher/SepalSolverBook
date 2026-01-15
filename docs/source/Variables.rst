Variables
=========

Variables in C#
---------------
A variable is a symbolic name given to a memory location. In C#, variables 
are categorized into two main types based on how they store data in memory: 
Value Types and Reference Types.

1. Value Types
~~~~~~~~~~~~~~
Value types store the actual data directly in the location where the variable 
is declared (usually on the stack). When you copy a value type, a complete 
independent copy of the data is made.

.. code-block:: csharp
 { 
     int original = 100; int copy = original; 
     // A new copy is created copy = 200; 
     // Changing the copy does NOT affect the original 
     Console.WriteLine("Original: {original}, Copy: {copy}"); 
 }

2. Reference Types
~~~~~~~~~~~~~~~~~~
Reference types do not store the actual data. Instead, they store a **reference** 
(a pointer) to the memory address where the data is kept (usually on the heap). 
When you copy a reference type, you are only copying the pointer, not the data itself. 


.. code-block:: csharp
 {
     int[] originalArray = { 1, 2, 3 }; int[] aliasArray = originalArray; 
     // Both variables point to the same memory aliasArray[0] = 99; 
     // Changing aliasArray affects originalArray 
     Console.WriteLine("Original[0]: {originalArray[0]}, Alias[0]: {aliasArray[0]}");     
 } 


.. list-table:: Variable Categories 
   :header-rows: 1

   * - Category
     - Memory
     - Typical Types
     - Copy Behavior
   * - Value Types
     - Stack
     - int, double, bool, struct
     - Copies the actual value
   * - Reference Types
     - Heap
     - string, class, Array, object
     - Copies the reference (pointer)



Variable Declaration and Initialization
---------------------------------------
C# is a strongly-typed language. You must declare the type of a variable 
before you use it. You can also use the var keyword for implicit typing, 
where the compiler determines the type based on the value assigned. 

Examples
~~~~~~~~


.. Admonition:: Example 1 :  Explicit vs Implicit Typing 

   
   .. code-block:: csharp
   
      double precision = 0.0001; //Explicit
      var tolerance = 1e-6; // Implicit (compiler sees it is a double) 
      Console.WriteLine($"Precision: {precision}, Tolerance: {tolerance}");
   
   
   Ouput
   
   .. terminal::
   
      Precision: 0.0001, Tolerance: 1E-06


.. Admonition:: Example 2 :  Constant Variables 

   
   .. code-block:: csharp
    
      const double Gravity = 9.81; 
      // Gravity = 10; 
      // This would cause a compilation error 
      Console.WriteLine($"Acceleration due to gravity: {Gravity} m/s²"); 
    
   
   Ouput
   
   .. terminal::
   
      Acceleration due to gravity: 9.81 m/s²


.. Admonition:: Example 3 :  Scope and Braces

   
   .. code-block:: csharp
    
      int outer = 10; 
      { 
          int inner = 20; 
          Console.WriteLine($"Sum: {outer + inner}"); 
      } 
      // Console.WriteLine(inner); 
      // Error: 'inner' is out of scope 
    
   
   Ouput
   
   .. terminal::
   
      Sum: 30


.. Admonition:: Example 4 :  Nullable Reference Types 

   
   .. code-block:: csharp
    
      string? name = null; 
      // The '?' allows the reference to be null 
      name = "Sepal Solver"; 
      Console.WriteLine($"Project: {name}"); 
    
   
   Ouput
   
   .. terminal::
   
      Project: Sepal Solver

