Operators And Expressions
=========================

Collections and LINQ in C#
--------------------------
In numerical programming, we rarely work with single values. Collections
allow us to group related data—such as a vector of residuals or a list of
material properties. LINQ (Language Integrated Query) provides a powerful,
declarative way to filter, transform, and analyze these collections.

1. Common Collection Types
~~~~~~~~~~~~~~~~~~~~~~~~~~
C# provides several specialized collections. While arrays are the standard
for fixed-size mathematical data, other collections offer dynamic resizing
and key-based lookups.


.. list-table:: List of Essential Collections
   :header-rows: 1

   * - Type
     - Namespace
     - Category
     - Description
   * - Array (T[])
     - System
     - Reference type
     - Fixed-size, high-performance contiguous memory.
   * - List<T>
     - System.Collections.Gen
     - Reference type
     - Dynamically resizable array for iterative growth.
   * - Dictionary<K,V>
     - System.Collections.Gen
     - Reference type
     - Key-value pairs for fast lookups.
   * - HashSet<T>
     - System.Collections.Gen
     - Reference type
     - Unordered set of unique elements.




.. code-block:: csharp

   // Arrays: Best for Matrix data (Fixed size)
   double[] vector = { 1.0, 0.0, 0.0 };

   // Lists: Best for Solver History (Dynamic size)
   List<double> residuals = [];
   residuals.Add(0.125);
   residuals.Add(0.001);

   // Dictionary: Best for Material Properties mapping
   Dictionary<string, double> materials = [];
   materials["Steel_E"] = 210e9; 


2. Introduction to LINQ
~~~~~~~~~~~~~~~~~~~~~~~
LINQ allows you to perform query operations directly on collections.
It simplifies tasks like finding the maximum error in a vector or
extracting specific nodes from a mesh using a functional approach.




.. code-block:: csharp

   double[] data = { -1.5, 0.2, 4.5, 10.1, -0.5 };

   // Filtering: Get only positive values
   var positive = data.Where(x => x > 0);

   // Transformation: Get absolute values
   var absolute = data.Select(Abs);

   // Aggregation: Find the maximum value
   double maxVal = data.Max();

   // Conversion: Force evaluation into an array
   double[] result = [..positive];



3. Deferred Execution
~~~~~~~~~~~~~~~~~~~~~
A vital concept in LINQ is that queries are not executed when they are
defined. They are executed when you "materialize" them (by using
foreach, .ToArray(), or .ToList()). This is known as lazy evaluation.

Examples
--------


.. admonition:: Example 1 :  Filtering Convergence Data

   In this scenario, we have a list of error values captured from a solver
   that is struggling to reach a steady state. We want to programmatically
   extract only the "successful" steps—those where the error dropped below
   our predefined tolerance—to verify how often our algorithm performed well.
   
   .. code-block:: csharp
   
      List<double> errors =  [0.5, 0.01, 0.0002, 1.5, 0.00001];
      double tolerance = 0.001;
   
      // Find all errors that meet the tolerance criteria
      var convergedEntries = errors.Where(e => e < tolerance).ToArray();
      Console.WriteLine($"Found {convergedEntries.Length} converged steps.");
   
   
   
 Ouput
   
   .. terminal::
   
      Found 2 converged steps.


.. admonition:: Example 2 :  Statistical Analysis

   Imagine you have completed a simulation and possess a vector of residuals
   representing the difference between your guess and the true solution.
   Before proceeding, you need to calculate the average error to understand
   the general accuracy and the "Total Energy" (sum of squares) to assess
   the stability of the system.
   
   .. code-block:: csharp
   
      double[] residuals = [0.02, 0.05, 0.01, 0.08, 0.03];
      double averageError = residuals.Average();
      double totalEnergy = residuals.Sum(r => r * r); // Sum of squares
      Console.WriteLine($"Average: {averageError}, Energy: {totalEnergy}");
   
   
   
 Ouput
   
   .. terminal::
   
      Average: 0.038, Energy: 0.0103


.. admonition:: Example 3 :  Mapping Node IDs

   In Finite Element Analysis (FEA), nodes are often assigned unique
   identification numbers that aren't necessarily sequential. By using a
   Dictionary, we can map these arbitrary Node IDs to their physical
   coordinates on a 1D beam, allowing for instant lookup without
   searching through a massive list.
   
   .. code-block:: csharp
   
      var nodes = new Dictionary<int, double>
      {
          { 101, 0.0 }, 
          { 102, 0.5 }, 
          { 103, 1.0 }
      };
   
      if (nodes.ContainsKey(102))
      {
          Console.WriteLine($"Node 102 position: {nodes[102]}");
      }
   
   
   
   
 Ouput
   
   .. terminal::
   
      Node 102 position: 0.5


.. admonition:: Example 4 :  Generating Sequences

   Setup is a major part of numerical modeling. Instead of writing
   cumbersome loops to initialize an identity-like vector or an index
   map, we use LINQ generators to create a range of integers or a
   repeating set of initial guesses in a single line of code.
   
   .. code-block:: csharp
   
      // Generate a sequence of 100 integers for indexing
      int[] indices = [..Enumerable.Range(0, 100)];
   
      // Generate a constant initial guess vector
      double[] initialGuess = [..Enumerable.Repeat(1.0, 5)];
   
   

Performance Note
~~~~~~~~~~~~~~~~
While LINQ is expressive, it can introduce overhead due to allocations.
For the "hot-path" of a numerical solver (like inner loops of matrix
multiplication), traditional for-loops remain the preferred choice.
