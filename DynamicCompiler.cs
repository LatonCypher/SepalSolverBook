
public static class DynamicRunner
{
    public static void RunCode(List<string> codeLines)
    {
        // 1. Join lines into a single script
        string fullCode = string.Join(Environment.NewLine, codeLines);

        // 2. Setup the environment (References and Imports)
        var options = ScriptOptions.Default
            // ADD REFERENCES (The DLLs)
            .AddReferences(
                typeof(object).Assembly,                      // mscorlib
                typeof(Enumerable).Assembly,                  // System.Core
                typeof(Regex).Assembly,                       // System.Text.RegularExpressions
                typeof(Process).Assembly,                     // System.Diagnostics
                typeof(SepalSolver.Math).Assembly,            // Your SepalSolver DLL
                typeof(Chart).Assembly                        // Your SepalSolver PlotLob DLL
            )
            // ADD IMPORTS (The 'using' statements)
            .AddImports(
                "System",
                "System.Collections.Generic",
                "System.Linq",
                "System.Threading.Tasks",
                "System.Text.RegularExpressions",
                "System.Diagnostics",
                "SepalSolver",
                "SepalSolver.PlotLib",
                // STATIC IMPORTS (The 'using static' equivalent)
                "SepalSolver.Math",
                "SepalSolver.PlotLib.Chart",
                "SepalSolver.PlotLib.Interpreter",
                "SepalSolver.PlotLib.Location"
            );

        try
        {
            // 3. Execute Synchronously 
            // We use GetAwaiter().GetResult() to block until finished without async/await
            CSharpScript.RunAsync(fullCode, options).GetAwaiter().GetResult();
        }
        catch (CompilationErrorException e)
        {
            Console.WriteLine("❌ Syntax Error in Documentation:");
            foreach (var diag in e.Diagnostics)
            {
                Console.WriteLine($"Line {diag.Location.GetLineSpan().StartLinePosition.Line + 1}: {diag.GetMessage()}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Runtime Error: {ex.Message}");
        }
    }
}

