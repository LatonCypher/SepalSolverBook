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
            string filePath = bookfolder + "\\index.rst";
            string indexmessage = """
                    Welcome to Numerical Methods with SepalSolver!
                ============================================================

                ** Preface **

                The study of numerical methods has long been a cornerstone of applied mathematics, engineering, and computational science. As problems in these fields grow in complexity, the need for efficient, reliable, and adaptable computational tools becomes ever more pressing. This book was written to address that need, offering readers both a rigorous introduction to numerical techniques and a practical guide to their implementation using SepalSolver.
                
                SepalSolver was chosen as the central framework for this text because of its ability to unify diverse numerical approaches under a single, coherent environment. By integrating classical algorithms with modern solver strategies, it provides a platform that is both accessible to beginners and powerful enough for advanced research. Throughout the book, readers will encounter worked examples, case studies, and performance analyses that demonstrate how SepalSolver can be applied to real-world problems across disciplines.
                
                This book is intended for a wide audience: undergraduate and graduate students seeking a solid foundation in numerical methods, researchers exploring computational techniques for specialized applications, and professionals who require practical tools for solving complex problems. Each chapter is designed to balance theory with practice, ensuring that readers not only understand the mathematical principles but also gain the skills to implement them effectively.
                
                The organization of the book reflects a progression from fundamental concepts—such as error analysis, stability, and convergence—to advanced topics including iterative solvers, spectral methods, and large-scale simulations. Readers are encouraged to engage actively with the examples and exercises, using SepalSolver as a hands-on companion to deepen their understanding.

                Ultimately, this book aims to highlight the evolving synergy between mathematical theory and computational innovation. By combining rigorous exposition with practical application, it seeks to empower readers to design, analyze, and implement robust numerical solutions that meet the challenges of modern science and engineering.

                SepalSolver is built with a focus on performance, accuracy, and ease of use. It provides a well-documented API and is designed to be easily integrated into your C# projects. Whether you're working on scientific research, engineering simulations, or data analysis, SepalSolver can significantly enhance your productivity and accelerate your mathematical computations.

                This document is designed t demonstract how to use the SepalSolver to solve commnom science and engineering problems


                **Abstract**

                Numerical methods form the backbone of modern scientific computing, enabling the approximation of solutions to problems that are analytically intractable. This book presents a comprehensive exploration of numerical techniques, with a particular emphasis on SepalSolver, a versatile computational framework designed to bridge theory and practice. By integrating classical algorithms with contemporary solver strategies, SepalSolver provides a unified environment for tackling linear and nonlinear systems, optimization problems, differential equations, and large-scale simulations.
                
                The text begins with foundational principles—error analysis, convergence, and stability—before progressing to advanced topics such as iterative methods, spectral techniques, and sparse matrix computations. Each chapter demonstrates how SepalSolver can be applied to real-world problems, offering readers both theoretical insight and practical implementation guidance. Worked examples, case studies, and performance benchmarks illustrate the solver’s efficiency and adaptability across diverse domains, including engineering, physics, finance, and data science.
                
                This book is intended for students, researchers, and professionals seeking a deeper understanding of numerical methods and their computational realization. By combining rigorous mathematical exposition with hands-on solver applications, it equips readers with the tools to design, analyze, and implement robust numerical solutions. Ultimately, the integration of SepalSolver into the study of numerical methods highlights the evolving synergy between mathematical theory and computational innovation.

                

                Contents
                --------

                .. toctree::

                """;

            using (StreamWriter writer = new(filePath))
            {
                // Write to file
                writer.WriteLine(indexmessage);
            }

            string[] BookChapters = Directory.GetDirectories(projectfolder);
            foreach (string BookChapter in BookChapters)
            {
                string relativePath = Path.GetRelativePath(projectfolder, BookChapter);
                using (StreamWriter writer = new(filePath, append: true))
                {
                    // Write to file
                    writer.WriteLine("   " + string.Join(' ', [.. BookChapter.Split(['_']).Skip(2)]));
                }

                string[] ChapterSections = Directory.GetFiles(BookChapter, "*.cs");
                foreach (string ChapterSection in ChapterSections)
                {
                    relativePath = Path.GetRelativePath(BookChapter, ChapterSection);
                    Run(ChapterSection, bookfolder);
                }
            }
        }
        public static void Run(string Classname, string DocFolder)
        {
            string[] namesplit = Classname.Split(['.', '\\']);
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


            using (StreamWriter writer = new(DocFolder + Classname + ".rst"))
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
