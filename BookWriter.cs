

namespace ConsoleApp1
{
    internal class BookWriter (string ProjectFolder, string BookFolder)
    {
        string bookfolder = BookFolder;
        string projectfolder = ProjectFolder;

        public void WriteBook()
        {
            string indexfile = bookfolder + "index.rst";
            string indexmessage = """
                Welcome to Numerical Methods with SepalSolver!
                ============================================================

                Preface
                ------------

                Nearly all modern programming languages come equipped with user-friendly scientific computing toolboxes, providing developers with accessible libraries for numerical analysis, optimization, and simulation. However, C# has historically been an exception to this rule. While Microsoft recognized this gap and sought to address it by partnering with a Moscow university to develop the Open Solving Library for ODEs (OSLO), the project was ultimately stalled due to U.S. sanctions on Russia following the invasion of Ukraine in 2014.

                It was in this context that SepalSolver was conceived. SepalSolver was created to serve a very specific purpose: to provide a C#-based, high-performing, and user-friendly scientific computing tool. Unlike many general-purpose libraries, SepalSolver was designed with the dual goals of computational efficiency and ease of use, ensuring that engineers, scientists, and developers working in C# could access the same level of mathematical sophistication available in other programming ecosystems.

                The development of SepalSolver was spearheaded by Cyphercrescent, an engineering software development company that uses C# as its primary programming language. For Cyphercrescent, the need was clear: engineering software requires a mathematics library that is both powerful and intuitive. By building SepalSolver, the company not only addressed its internal requirements but also contributed a valuable tool to the broader C# community.

                This book introduces readers to the principles of numerical methods through the lens of SepalSolver. It is intended for students, researchers, and professionals who wish to combine theoretical rigor with practical implementation. By weaving together mathematical foundations, algorithmic strategies, and hands-on examples, the text demonstrates how SepalSolver can be applied to solve real-world problems across engineering, physics, finance, and data science.

                Ultimately, this work highlights the evolving synergy between mathematical theory and computational innovation. SepalSolver stands as a testament to the importance of accessible scientific computing in C#, and this book seeks to empower readers to harness its capabilities for both academic exploration and professional practice.

                Abstract
                ------------
                Numerical methods form the backbone of modern scientific computing, enabling the approximation of solutions to problems that are analytically intractable. This book presents a comprehensive exploration of numerical techniques, with a particular emphasis on SepalSolver, a versatile computational framework designed to bridge theory and practice. By integrating classical algorithms with contemporary solver strategies, SepalSolver provides a unified environment for tackling linear and nonlinear systems, optimization problems, differential equations, and large-scale simulations.
                
                The text begins with foundational principles—error analysis, convergence, and stability—before progressing to advanced topics such as iterative methods, spectral techniques, and sparse matrix computations. Each chapter demonstrates how SepalSolver can be applied to real-world problems, offering readers both theoretical insight and practical implementation guidance. Worked examples, case studies, and performance benchmarks illustrate the solver’s efficiency and adaptability across diverse domains, including engineering, physics, finance, and data science.
                
                This book is intended for students, researchers, and professionals seeking a deeper understanding of numerical methods and their computational realization. By combining rigorous mathematical exposition with hands-on solver applications, it equips readers with the tools to design, analyze, and implement robust numerical solutions. Ultimately, the integration of SepalSolver into the study of numerical methods highlights the evolving synergy between mathematical theory and computational innovation.

                
                .. list-table:: Numerical Methods
                   :header-rows: 1

                   * - Method
                     - Accuracy
                     - Speed
                   * - Euler
                     - Low
                     - Fast
                   * - Runge-Kutta
                     - High
                     - Moderate
                   * - SepalSolver
                     - High
                     - High




                .. toctree::

                """;

            using (StreamWriter writer = new(indexfile))
            {
                writer.WriteLine(indexmessage);
            }

            string[] BookChapters = Directory.GetDirectories(projectfolder);
            foreach (string BookChapter in BookChapters)
            {
                string relativePath = string.Join(' ', [.. BookChapter.Split(['_']).Skip(2)]);
                using (StreamWriter writer = new(indexfile, append: true))
                {
                    writer.WriteLine("   " + relativePath);
                }


                string chapterfile = bookfolder + relativePath + ".rst";
                string chaptermessage = $"""
                    
                    {relativePath}
                    =========================================================
                    



                    .. toctree::

                    """;
                using (StreamWriter writer = new(chapterfile))
                {
                    writer.WriteLine(chaptermessage);
                }
                string[] ChapterSections = Directory.GetFiles(BookChapter, "*.cs");
                foreach (string ChapterSection in ChapterSections)
                {
                    relativePath = Path.GetRelativePath(BookChapter, ChapterSection);
                    var sectionname = string.Join(' ', [.. relativePath.Split(['_']).Skip(2)]);
                    sectionname = sectionname.Split('.')[0];
                    using (StreamWriter writer = new(chapterfile, append: true))
                    {
                        writer.WriteLine("   " + sectionname);
                    }
                    Run(ChapterSection, sectionname, bookfolder);
                }
            }
        }
        public void Run(string inputPath, string classname, string DocFolder)
        {
            string outputPath = DocFolder + classname + ".rst";
            string csContent = File.ReadAllText(inputPath);

            // Extract BookContent block
            var bookMatch = Regex.Match(csContent, @"/// <BookContent>(.*?)///</BookContent>", RegexOptions.Singleline);
            if (!bookMatch.Success)
            {
                Console.WriteLine("No <BookContent> block found.");
                return;
            }

            string bookContent = bookMatch.Groups[1].Value;

            // Remove leading "///"
            bookContent = Regex.Replace(bookContent, @"^\s*/// ?", "", RegexOptions.Multiline);

            // Process headers
            bookContent = Regex.Replace(bookContent,
                @"<header 1>(.*?)</header 1>",
                m => $"{m.Groups[1].Value.Trim()}\n{new string('=', m.Groups[1].Value.Trim().Length)}",
                RegexOptions.Singleline);

            bookContent = Regex.Replace(bookContent,
                @"<header 2>(.*?)</header 2>",
                m => $"{m.Groups[1].Value.Trim()}\n{new string('-', m.Groups[1].Value.Trim().Length)}",
                RegexOptions.Singleline);

            bookContent = Regex.Replace(bookContent,
                @"<header 3>(.*?)</header 3>",
                m => $"{m.Groups[1].Value.Trim()}\n{new string('~', m.Groups[1].Value.Trim().Length)}",
                RegexOptions.Singleline);

            // Convert <example *> blocks
            bookContent = Regex.Replace(bookContent,
                @"<example\s+(.*?)>(.*?)</example\s+\1>",
                m =>
                {
                    string title = m.Groups[1].Value.Trim();
                    string inner = m.Groups[2].Value;

                    // Handle <code> blocks inside example
                    var codeMatch = Regex.Match(inner, @"<code>(.*?)</code>", RegexOptions.Singleline);
                    if (codeMatch.Success)
                    {
                        string code = codeMatch.Groups[1].Value;
                        code = Regex.Replace(code, @"^\s*/// ?", "", RegexOptions.Multiline);
                        return $".. admonition:: Example {title}\n\n   .. code-block:: csharp\n\n{Indent(code, 6)}";
                    }
                    else
                    {
                        inner = Regex.Replace(inner, @"^\s*/// ?", "", RegexOptions.Multiline);
                        return $".. admonition:: Example {title}\n\n   {inner.Trim()}";
                    }
                },
                RegexOptions.Singleline);

            // Convert <table> blocks
            bookContent = Regex.Replace(bookContent,
                @"<table>(.*?)</table>",
                m =>
                {
                    string[] rows = m.Groups[1].Value.Trim().Split('\n');
                    return BuildRstTable(rows);
                },
                RegexOptions.Singleline);

            // Wrap remaining code snippets in code-block
            // (simple heuristic: braces or semicolons indicate code)
            bookContent = Regex.Replace(bookContent,
                @"\{[^}]*\}",
                m => $".. code-block:: csharp\n\n{Indent(m.Value, 3)}",
                RegexOptions.Singleline);

            using (StreamWriter writer = new(outputPath, true))
            {
                writer.WriteLine(bookContent);
            }
        }

        static string BuildRstTable(string[] rows)
        {
            // Assume pipe-separated values
            string[][] cells = Array.ConvertAll(rows, r => r.Split('|'));
            int cols = cells[0].Length;
            int[] widths = new int[cols];

            // Compute column widths
            for (int c = 0; c < cols; c++)
                foreach (var row in cells)
                    widths[c] = Max(widths[c], row[c].Trim().Length);

            string line = "+" + string.Join("+", Array.ConvertAll(widths, w => new string('-', w + 2))) + "+";

            var table = new System.Text.StringBuilder();
            table.AppendLine(line);
            foreach (var row in cells)
            {
                table.Append("|");
                for (int c = 0; c < cols; c++)
                    table.Append(" " + row[c].Trim().PadRight(widths[c]) + " |");
                table.AppendLine();
                table.AppendLine(line);
            }
            return table.ToString();
        }

        static string Indent(string text, int spaces)
        {
            string pad = new string(' ', spaces);
            return Regex.Replace(text.Trim(), @"^", pad, RegexOptions.Multiline);
        }
    }
}
