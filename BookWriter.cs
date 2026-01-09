using ScottPlot;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal class Packet
    {
        public string info;
        public List<string> content;
    }
    internal class BookWriter (string ProjectFolder, string BookFolder)
    {
        string bookfolder = BookFolder;
        string projectfolder = ProjectFolder;
        string pattern = "<.*?>";

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
                    =====================================================
                    



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
        public void Run(string Classpath, string classname, string DocFolder)
        {
            string[] lines = [..File.ReadAllLines(Classpath)
                                .SkipWhile(line => !line.Contains("<BookContent>"))   // skip until opening tag
                                .Skip(1)                                              // skip the opening tag itself
                                .TakeWhile(line => !line.Contains("</BookContent>"))  // take until closing tag
                                    ];
            List<string> Document = [];
            int start = 0;
            while (start < lines.Length)
            {
                var packet = PacketExtraction(lines, ref start);
                switch (packet.info)
                {
                    case "Header 1":
                        Document.Add(packet.content[0]);
                        Document.Add("=========================");
                        break;
                    case "Header 2":
                        Document.Add(packet.content[0]);
                        Document.Add("-------------------------");
                        break;
                    case "Header 3":
                        Document.Add(packet.content[0]);
                        Document.Add("~~~~~~~~~~~~~~~~~~~~~~~~~");
                        break;
                    case "Table":
                        Document.Add(".. list-table::");
                        Document.Add("   :header-rows: 1");
                        Document.Add("");
                        foreach (var line in packet.content)
                        {
                            Document.Add("   * - " + string.Join("\n     - ", line.Split('|')));
                        }
                        Document.Add("");
                        break;
                    case "Code":
                        Document.Add(".. code-block:: csharp");
                        Document.Add("");
                        foreach (var line in packet.content)
                        {
                            Document.Add("   " + line);
                        }
                        Document.Add("");
                        break;
                    case "Text":
                        Document.AddRange(packet.content);
                        break;
                }
            }


            using (StreamWriter writer = new(DocFolder + classname + ".rst"))
            {
                writer.WriteLine($"""
                    {classname}
                    ============================


                    """);
                foreach (string line in Document)
                    writer.WriteLine(line);
            }
        }

        Packet PacketExtraction(string[] lines, ref int start)
        {
            Packet packet = new() { info = "text", content = [] };
            while (start < lines.Length)
            {
                string line = lines[start];
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    
                    string anglebrackets = match.Groups[0].Value;
                    string result = anglebrackets.Substring(1, anglebrackets.Length - 2);
                    if(result.Contains('/'))
                    {
                        start++;
                        return packet;
                    }
                    if (packet.content.Count > 0) return packet;
                    packet.info = result;
                }
                else
                {
                    line = line.TrimStart();
                    if (line.Contains("///")) 
                        line = line.Substring(3);
                    packet.content.Add(line); 
                }
                start++;
            }
            return packet;
        }

        static string[] getDoc(string[] lines, ref int start, out int indentation)
        {
            List<string> result = [];

            // get doc start
            string line = lines[start].TrimStart();
            while (true)
            {
                if (line.Length >= 3 && line.Substring(0, 3) == "///")
                    break;
                else if (start < lines.Length - 1)
                    line = lines[++start].TrimStart();
                else break;
            }

            // get doc
            indentation = 0;
            if (line != "")
            {
                indentation = lines[start].IndexOf(line[0]) / 4;
                while (true)
                {
                    if (line.Length >= 3 && line.Substring(0, 3) == "///")
                    {
                        result.Add(line);
                        if (start < lines.Length - 1)
                            line = lines[++start].TrimStart();
                    }
                    else
                        break;

                }

                // get signature
                string signature = "";
                while (true && line != "}")
                {
                    string[] linesplit = line.Split(' ');
                    bool completed = false;
                    foreach (var split in linesplit)
                    {
                        if (completed = split == "{" || split == "=>" || split == ":")
                            break;
                        else
                            signature += split + " ";
                    }
                    if (completed)
                        break;
                    else if (start < lines.Length - 1)
                        line = lines[++start].TrimStart();

                }
                result.Add(signature);
            }

            return [.. result];
        }

    }
}
