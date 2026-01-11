

using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.MultiplotLayouts;
using System.Data;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

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
            string[] Content = File.ReadAllLines(inputPath);

            using (StreamWriter writer = new(outputPath))
            {
                writer.WriteLine(classname);
                writer.WriteLine(new string('=', classname.Length));
                writer.WriteLine("");
            }

                // Extract BookContent block
                List<string> bookContent = [..Content.SkipWhile(line=> !line.Contains("/// <BookContent>")).
                                          TakeWhile(line=>!line.Contains("/// </BookContent>"))];
            if (bookContent.Count == 0)
            {
                Console.WriteLine("No <BookContent> block found.");
                return;
            }
            bookContent.RemoveAt(0); // Remove the starting tag line
            //Headers format
            TreatHeader1(bookContent);
            TreatHeader2(bookContent);
            TreatHeader3(bookContent);
            TreatCodeBlock(bookContent);
            TreatTableBlock(bookContent);
            TreatExampleBlock(bookContent);

            
            using (StreamWriter writer = new(outputPath, true))
            {
                foreach (var line in bookContent)
                    writer.WriteLine(line);
            }
        }


        static void Replace(List<string> content, int start, int length, List<string> replacement)
        {
            // Remove specified range
            content.RemoveRange(start, length);

            // Insert new items starting at index 1
            content.InsertRange(start, replacement);

        }

        static void TreatHeader1(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<header 1>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<header 1>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                string line = bookContent[startIndex];
                // Regex to capture content between <header 1> and </header 1>
                var match = Regex.Match(line, @"<header 1>(.*?)</header 1>");

                if (match.Success)
                {
                    string header1 = match.Groups[1].Value;
                    List<string> header1lines = [$"***{header1}***"];
                    Replace(bookContent, startIndex, 1, header1lines);
                }
            }
        }
        static void TreatHeader2(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<header 2>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<header 2>"))
                    {
                        startIndex = i;
                        break;
                    }
                }

                string line = bookContent[startIndex];
                // Regex to capture content between <header 2> and </header 2>
                var match = Regex.Match(line, @"<header 2>(.*?)</header 2>");

                if (match.Success)
                {
                    string header2 = match.Groups[1].Value;
                    List<string> header2lines = [$"**{header2}**"];
                    Replace(bookContent, startIndex, 1, header2lines);
                }
            }
        }
        static void TreatHeader3(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<header 3>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<header 3>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                string line = bookContent[startIndex];
                // Regex to capture content between <header 3> and </header 3>
                var match = Regex.Match(line, @"<header 3>(.*?)</header 3>");

                if (match.Success)
                {
                    string header3 = match.Groups[1].Value;
                    List<string> header3lines = [$"*{header3}*"];
                    Replace(bookContent, startIndex, 1, header3lines);
                }

            }
        }
        static void TreatCodeBlock(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<code>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<code>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                int Length = 1;
                List<string> Codelines = [".. code-block:: csharp"];
                int space = bookContent[startIndex + Length].TakeWhile(c => c == ' ').Count()+1;
                while (!bookContent[startIndex + Length].Contains("</code>"))
                {
                    string line = bookContent[startIndex + Length];
                    if(line.Length >= space)
                        Codelines.Add(line.Substring(space));
                    else
                        Codelines.Add(line);
                    Length++;
                }
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }
        static void TreatTableBlock(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<table>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<table>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                int Length = 1;
                List<string> Codelines = ["", ".. list-table::"];
                string line = bookContent[startIndex];
                var match = Regex.Match(line, @"<table>\s*(.*)");
                if (match.Success)
                {
                    string content = match.Groups[1].Value;
                    Codelines = ["", $".. list-table:: {content}"];
                }
                Codelines.Add("   :header-rows: 1");
                Codelines.Add("");
                while(!bookContent[startIndex + Length].Contains("</table>"))
                {
                    string tableline = bookContent[startIndex + Length];
                    // Remove leading "///"
                    line = tableline.TrimStart(' ', '\t', '/').Trim();

                    // Split by '|'
                    string[] columns = line.Split('|');

                    // Trim each column
                    for (int i = 0; i < columns.Length; i++)
                        columns[i] = columns[i].Trim();

                    // Print header row
                    columns[0] = "   * - " + columns[0];
                    for (int i = 1; i < columns.Length; i++)
                        columns[i] = "     - " + columns[i];

                    Codelines.AddRange(columns);
                    Length++;
                }
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }
        static void TreatExampleBlock(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<example")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<example"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                int Length = 1;
                string contentangle = Regex.Match(bookContent[startIndex], "<(.*?)>").Value;
                string[] examplen = contentangle.Substring(1, contentangle.Length-2).Split(' ');
                List<string> Codelines = [$".. Admonition:: Example {examplen[1]}"];
                while (!bookContent[startIndex + Length].Contains("</example"))
                {
                    string line = bookContent[startIndex + Length];
                    if(line.Contains("///"))
                        Codelines.Add("   " + line.TrimStart(' ', '\t', '/').Trim());
                    else
                        Codelines.Add("   " + line);
                    Length++;
                }
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }
    }
}
