
Basic Operations and Syntax
###########################


To build a powerful numerical engine like SepalSolver, we must first master the "grammar" of C#. C# is a strongly-typed, object-oriented language designed for clarity and performance. Its syntax provides a structured way to express mathematical logic, ensuring that the computer interprets our engineering formulas exactly as intended.


**1. The Structure of a C# Program**

Every instruction in C# lives inside a Statement, and statements are grouped into Blocks defined by curly braces { }. For our solver, this structure helps us separate different mathematical concerns—like keeping the matrix inversion logic separate from the file-saving logic. 


**2. Semicolons and Whitespace** 

In C#, every statement must end with a semicolon `;`. This acts as a "terminator," telling the compiler that one instruction is finished and the next is beginning. Unlike some languages, C# ignores extra whitespace,  allowing us to format complex multi-line equations in a way that is readable to humans without confusing the machine.

**3. Comments for Documentation**

Writing code is only half the battle; explaining it is the other. C# provides two main ways to leave notes for yourself and other engineers:

- Single-line (//): Best for quick notes about a specific line. 
- Multi-line (/\* ... \*/): Best for describing complex algorithms. 
- XML Documentation (///): Used to generate tooltips for your library. 


.. code-block:: csharp
 
   // This is a single-line comment double pi = 3.14159; 
   // Every statement ends with a semicolon

   /* 
    * This is a multi-line comment 
    * used to explain the following block 
    */
   {
       double radius = 10.0;
       double area = pi * radius * radius;
   }
 


**Examples**

*Basic Variable Assignment :*
In this example, we see how to declare a variable and assign it a value. In SepalSolver, we use descriptive names to ensure that anyone reading  the code understands that tol represents a tolerance and maxIter  represents an iteration limit.

.. code-block:: csharp
 
   double tol = 1e-6; int maxIter = 100;
   Console.WriteLine($"Tolerance: {tol}");
   Console.WriteLine($"Max Iterations: {maxIter}");
 

*Code Blocks and Scope :* 
Variables created inside curly braces are part of a "Scope." This is  essential for solvers because it allows us to create temporary variables  (like a local residual) that disappear once the calculation is done,  keeping the computer's memory clean. 

.. code-block:: csharp
 
   double globalValue = 10.0;
   {
       double temporaryValue = 5.0;
       double sum = globalValue + temporaryValue;
   } 
   // temporaryValue and sum are no longer accessible here
 

*Key Syntax Rules for Engineers*

1. Case Sensitivity: Matrix and matrix are different things in C#. 
2. Strong Typing: You cannot put a string (text) into a double (decimal number) without explicit conversion.
3. Entry Point: Every C# application starts executing at the Main method. 

The rest of sytax, structure,  variable and operators are addressed in the following sections. 

To build a powerful numerical engine like SepalSolver, we must first master the "grammar" of C#. C# is a strongly-typed, object-oriented language designed for clarity and performance. Its syntax provides a structured way to express mathematical logic, ensuring that the computer interprets our engineering formulas exactly as intended.


**1. The Structure of a C# Program**

Every instruction in C# lives inside a Statement, and statements are grouped into Blocks defined by curly braces { }. For our solver, this structure helps us separate different mathematical concerns—like keeping the matrix inversion logic separate from the file-saving logic. 


**2. Semicolons and Whitespace** 

In C#, every statement must end with a semicolon `;`. This acts as a "terminator," telling the compiler that one instruction is finished and the next is beginning. Unlike some languages, C# ignores extra whitespace,  allowing us to format complex multi-line equations in a way that is readable to humans without confusing the machine.

**3. Comments for Documentation**

Writing code is only half the battle; explaining it is the other. C# provides two main ways to leave notes for yourself and other engineers:

- Single-line (//): Best for quick notes about a specific line. 
- Multi-line (/\* ... \*/): Best for describing complex algorithms. 
- XML Documentation (///): Used to generate tooltips for your library. 


.. code-block:: csharp
 
   // This is a single-line comment double pi = 3.14159; 
   // Every statement ends with a semicolon

   /* 
    * This is a multi-line comment 
    * used to explain the following block 
    */
   {
       double radius = 10.0;
       double area = pi * radius * radius;
   }
 


**Examples**

*Basic Variable Assignment :*
In this example, we see how to declare a variable and assign it a value. In SepalSolver, we use descriptive names to ensure that anyone reading  the code understands that tol represents a tolerance and maxIter  represents an iteration limit.

.. code-block:: csharp
 
   double tol = 1e-6; int maxIter = 100;
   Console.WriteLine($"Tolerance: {tol}");
   Console.WriteLine($"Max Iterations: {maxIter}");
 

*Code Blocks and Scope :* 
Variables created inside curly braces are part of a "Scope." This is  essential for solvers because it allows us to create temporary variables  (like a local residual) that disappear once the calculation is done,  keeping the computer's memory clean. 

.. code-block:: csharp
 
   double globalValue = 10.0;
   {
       double temporaryValue = 5.0;
       double sum = globalValue + temporaryValue;
   } 
   // temporaryValue and sum are no longer accessible here
 

*Key Syntax Rules for Engineers*

1. Case Sensitivity: Matrix and matrix are different things in C#. 
2. Strong Typing: You cannot put a string (text) into a double (decimal number) without explicit conversion.
3. Entry Point: Every C# application starts executing at the Main method. 

The rest of sytax, structure,  variable and operators are addressed in the following sections. 




.. toctree::

   Primitive Types
   Variables
   Operators And Expressions
   Collections and Linq
   Algorithm Flow Control
   Exercise
