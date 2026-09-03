Collections and Linq
====================

Collections and LINQ in C#
--------------------------
In numerical programming, we rarely work with single values.Collections allow us to group related data—such as a vector of residuals or a list of material properties.LINQ(Language Integrated Query) provides a powerful, declarative way to filter, transform, and analyze these collections without writing complex loops.

1.Core Collection Types
~~~~~~~~~~~~~~~~~~~~~~~
C# provides several specialized collections. While arrays are the standard for fixed-size mathematical data, other collections offer dynamic resizing and key-based lookups.

.. list-table:: List of Essential Collections 
   :header-rows: 1

   * - Type
     - Namespace
     - Category
     - Description
   * - Array(T[])
     - System
     - Reference type
     - Fixed-size, high-performance contiguous memory.
   * - List<T>
     - System.Collections.Generic
     - Reference type
     - Dynamically resizable array. Ideal for iterative growth.
   * - Dictionary<K, V>
     - System.Collections.Generic
     - Reference type
     - Collection of key-value pairs for fast lookups.
   * - HashSet<T>
     - System.Collections.Generic
     - Reference type
     - Unordered set of unique elements.


.. code-block:: csharp
 
   // Arrays: Best for Matrix data (Fixed size)
   double[] vector = [1.0, 0.0, 0.0];
   
   // Lists: Best for Solver History (Dynamic size)
   List<double> residuals = [];
   residuals.Add(0.125);
   residuals.Add(0.001);

   // Dictionary: Best for Material Properties
   Dictionary<string, double> materials = [];
   materials["Steel_E"] = 210e9; // Young's Modulus
 

2.Introduction to LINQ
~~~~~~~~~~~~~~~~~~~~~~
LINQ allows you to perform "query" operations on collections.It simplifies tasks like finding the maximum error in a vector or extracting specific nodes from a mesh.


.. code-block:: csharp

   double[] data = [-1.5, 0.2, 4.5, 10.1, -0.5];

   // * Filtering: Get only positive values
   var positive = data.Where(x => x > 0);

   // * Transformation: Get absolute values
   var absolute = data.Select(Abs);

   // * Aggregation: Find the maximum value
   double maxVal = data.Max();

   // * Conversion: Force evaluation into an array
   double[] result = [..positive];
 

3.Deferred Execution
~~~~~~~~~~~~~~~~~~~~
A vital concept in LINQ is that queries are not executed when they are defined.They are executed when you "materialize" them(by using foreach, .ToArray(), or.ToList()). This allows for efficient query building but can lead to multiple executions if not handled carefully.

Examples
--------

.. admonition:: Example 1 :  Filtering Convergence Data

   
   .. code-block:: csharp
   
      List<double> errors = [0.5, 0.01, 0.0002, 1.5, 0.00001]; 
      double tolerance = 0.001;
   
      // Find all errors that meet the tolerance criteria
      var convergedEntries = errors.Where(e => e < tolerance).ToArray();
   
      Console.WriteLine($"Found {convergedEntries.Length} converged steps.");
    


.. admonition:: Example 2 :  Statistical Analysis of a Vector

   
   .. code-block:: csharp
   
      double[] residuals = [0.02, 0.05, 0.01, 0.08, 0.03];
      double averageError = residuals.Average();
      double minError = residuals.Min();
      double totalEnergy = residuals.Sum(r => r * r); // Sum of squares
      Console.WriteLine($"Average: {averageError}, Energy: {totalEnergy}");
    


.. admonition:: Example 3 :  Mapping Node IDs to Coordinates

   
   .. code-block:: csharp
   
      var nodes = new Dictionary<int, double> { { 101, 0.0 }, { 102, 0.5 }, { 103, 1.0 } };
   
      if (nodes.TryGetValue(102, out double value))
      {
          Console.WriteLine($"Node 102 position: {value}");
      }
    


.. admonition:: Example 4 :  Generating Sequences

   
   .. code-block:: csharp
    
      // Generate a sequence of 100 integers for indexing
      int[] indices = [..Enumerable.Range(0, 100)];
   
      // Generate a constant initial guess vector
      double[] initialGuess = [..Enumerable.Repeat(1.0, 5)];
    

Numerical Note: LINQ vs. Loops
------------------------------
While LINQ is expressive and readable, it often involves small memory allocations. In the "hot-path" of your solver(such as inside a matrix multiplication loop), traditional for loops are preferred for maximum performance. Use LINQ for high-level data management, setup, and post-processing.
