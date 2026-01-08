using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class BookWriter (string ProjectFolder, string BookFolder)
    {
        string bookfolder = BookFolder;
        string projectfolder = ProjectFolder;

        public void WriteBook()
        {
            string indexfile = bookfolder + "\\index.rst";
            string indexmessage = """
                    Welcome to Numerical Methods with SepalSolver!
                ============================================================

                Preface
                ------------

                Nearly all modern programming languages come equipped with user-friendly scientific computing toolboxes, providing developers with accessible libraries for numerical analysis, optimization, and simulation. However, C# has historically been an exception to this rule. While Microsoft recognized this gap and sought to address it by partnering with a Moscow university to develop the OpenSolver for Ordinary Differential Equations (OSLO), the project was ultimately stalled due to U.S. sanctions on Russia following the invasion of Ukraine in 2014.

                It was in this context that SepalSolver was conceived. SepalSolver was created to serve a very specific purpose: to provide a C#-based, high-performing, and user-friendly scientific computing tool. Unlike many general-purpose libraries, SepalSolver was designed with the dual goals of computational efficiency and ease of use, ensuring that engineers, scientists, and developers working in C# could access the same level of mathematical sophistication available in other programming ecosystems.

                The development of SepalSolver was spearheaded by Cyphercrescent, an engineering software development company that uses C# as its primary programming language. For Cyphercrescent, the need was clear: engineering software requires a mathematics library that is both powerful and intuitive. By building SepalSolver, the company not only addressed its internal requirements but also contributed a valuable tool to the broader C# community.

                This book introduces readers to the principles of numerical methods through the lens of SepalSolver. It is intended for students, researchers, and professionals who wish to combine theoretical rigor with practical implementation. By weaving together mathematical foundations, algorithmic strategies, and hands-on examples, the text demonstrates how SepalSolver can be applied to solve real-world problems across engineering, physics, finance, and data science.

                Ultimately, this work highlights the evolving synergy between mathematical theory and computational innovation. SepalSolver stands as a testament to the importance of accessible scientific computing in C#, and this book seeks to empower readers to harness its capabilities for both academic exploration and professional practice.

                Abstract
                ------------
                Numerical methods form the backbone of modern scientific computing, enabling the approximation of solutions to problems that are analytically intractable. This book presents a comprehensive exploration of numerical techniques, with a particular emphasis on SepalSolver, a versatile computational framework designed to bridge theory and practice. By integrating classical algorithms with contemporary solver strategies, SepalSolver provides a unified environment for tackling linear and nonlinear systems, optimization problems, differential equations, and large-scale simulations.
                
                The text begins with foundational principles—error analysis, convergence, and stability—before progressing to advanced topics such as iterative methods, spectral techniques, and sparse matrix computations. Each chapter demonstrates how SepalSolver can be applied to real-world problems, offering readers both theoretical insight and practical implementation guidance. Worked examples, case studies, and performance benchmarks illustrate the solver’s efficiency and adaptability across diverse domains, including engineering, physics, finance, and data science.
                
                This book is intended for students, researchers, and professionals seeking a deeper understanding of numerical methods and their computational realization. By combining rigorous mathematical exposition with hands-on solver applications, it equips readers with the tools to design, analyze, and implement robust numerical solutions. Ultimately, the integration of SepalSolver into the study of numerical methods highlights the evolving synergy between mathematical theory and computational innovation.

                

                Contents
                ----------

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


                string chapterfile = bookfolder + "\\" + relativePath + ".rst";
                string chaptermessage = $"""
                    
                    {relativePath}
                    ----------
                    
                    .. toctree::

                    """;
                using (StreamWriter writer = new(chapterfile))
                {
                    writer.WriteLine(chaptermessage);
                }
                string[] ChapterSections = Directory.GetFiles(BookChapter, "*.cs");
                foreach (string ChapterSection in ChapterSections)
                {
                    relativePath = string.Join(' ', [.. ChapterSection.Split(['_']).Skip(2)]);
                    using (StreamWriter writer = new(chapterfile, append: true))
                    {
                        writer.WriteLine("   " + relativePath);
                    }
                    Run(ChapterSection, bookfolder);
                }
            }
        }
        public static void Run(string Classname, string DocFolder)
        {
            string name = string.Join(' ', Classname.Split(['.', '\\'])[^2].Split('_').Skip(2));
            string[] lines = [..File.ReadAllLines(Classname)
                                .SkipWhile(line => !line.Contains("<BookContent>"))   // skip until opening tag
                                .Skip(1)                                              // skip the opening tag itself
                                .TakeWhile(line => !line.Contains("</BookContent>"))  // take until closing tag
                                    ];
            List<string> Document = [];
            int start = 0;
            while (start < lines.Length)
            {
                //var doc = getDoc(lines, ref start, out int indentation);
                start++;
            }


            using (StreamWriter writer = new(DocFolder + name + ".rst"))
            {
                foreach (string line in Document)
                    writer.WriteLine(line);
            }
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
