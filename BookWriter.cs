

using ScottPlot;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class BookWriter(string ProjectFolder, string BookFolder)
    {
        string bookfolder = BookFolder;
        string projectfolder = ProjectFolder;

        public void WriteBook()
        {
            string indexfile = bookfolder + "index.rst";
            string indexmessage = """
                Welcome to Numerical Methods with SepalSolver!
                #################################################

                Preface
                **********

                Nearly all modern programming languages come equipped with user-friendly scientific computing toolboxes, providing developers with accessible libraries for numerical analysis, optimization, and simulation. However, C# has historically been an exception to this rule. While Microsoft recognized this gap and sought to address it by partnering with a Moscow university to develop the Open Solving Library for ODEs (OSLO), the project was ultimately stalled due to U.S. sanctions on Russia following the invasion of Ukraine in 2014.

                It was in this context that SepalSolver was conceived. SepalSolver was created to serve a very specific purpose: to provide a C#-based, high-performing, and user-friendly scientific computing tool. Unlike many general-purpose libraries, SepalSolver was designed with the dual goals of computational efficiency and ease of use, ensuring that engineers, scientists, and developers working in C# could access the same level of mathematical sophistication available in other programming ecosystems.

                The development of SepalSolver was spearheaded by Cyphercrescent, an engineering software development company that uses C# as its primary programming language. For Cyphercrescent, the need was clear: engineering software requires a mathematics library that is both powerful and intuitive. By building SepalSolver, the company not only addressed its internal requirements but also contributed a valuable tool to the broader C# community.

                This book introduces readers to the principles of numerical methods through the lens of SepalSolver. It is intended for students, researchers, and professionals who wish to combine theoretical rigor with practical implementation. By weaving together mathematical foundations, algorithmic strategies, and hands-on examples, the text demonstrates how SepalSolver can be applied to solve real-world problems across engineering, physics, finance, and data science.

                Ultimately, this work highlights the evolving synergy between mathematical theory and computational innovation. SepalSolver stands as a testament to the importance of accessible scientific computing in C#, and this book seeks to empower readers to harness its capabilities for both academic exploration and professional practice.

                Abstract
                ********

                Numerical methods form the backbone of modern scientific computing, enabling the approximation of solutions to problems that are analytically intractable. This book presents a comprehensive exploration of numerical techniques, with a particular emphasis on SepalSolver, a versatile computational framework designed to bridge theory and practice. By integrating classical algorithms with contemporary solver strategies, SepalSolver provides a unified environment for tackling linear and nonlinear systems, optimization problems, differential equations, and large-scale simulations.

                Here are some simulation performed with the sepal solver. 

                1. **Six Linked Bar Mechanism**
                ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    A six-linked bar mechanism is a type of kinematic chain used in mechanical engineering to achieve complex motion paths and force transmission. It consists of six rigid bars (links) connected by joints, typically revolute (hinge) or prismatic (sliding), forming a closed-loop system. These mechanisms are extensions of four-bar and five-bar linkages, offering greater flexibility and control over motion.
                    In essence, a six-linked bar mechanism is a versatile extension of classical linkage theory, enabling engineers to design systems with greater motion complexity and adaptability.
                
                .. figure:: images/Six_Link.gif
                    :align: center
                    :alt: Six_Link.gif


                2. **Ship Roll at Sea**
                ~~~~~~~~~~~~~~~~~~~~~~~
                    **Ship roll characteristics** describe the side-to-side tilting motion of a vessel around its longitudinal axis (running bow to stern). This is one of the six fundamental ship motions (heave, sway, surge, yaw, pitch, and roll) and is particularly important because it directly affects stability, comfort, and safety at sea.  

                    In essence, roll is the most critical ship motion to manage because it directly ties to **stability and survivability** at sea. Engineers and naval architects devote significant effort to predicting and controlling roll through both design and operational strategies.   
                
                .. figure:: images/Ship_Roll.gif
                    :align: center
                    :alt: Ship_Roll.gif

                3. **Chaos of Double Compund Pendulum**
                ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    A double compound pendulum is a classic example of a chaotic system in physics. It consists of two pendulums attached end-to-end, where the motion of the second pendulum depends on the first. Despite its simple construction, the system exhibits highly complex and unpredictable behavior.

                .. figure:: images/Chaos.gif
                    :align: center
                    :alt: Chaos.gif

                4. **Three Double Pendulums with Different Masses**
                ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    Simulating three double pendulums with different masses is a powerful way to explore how mass distribution influences chaotic dynamics. Even though the governing equations are deterministic, the outcomes vary dramatically depending on the parameters.
                
                
                .. figure:: images/Three_Double_Pendulums.gif
                    :align: center
                    :alt: Three_Double_Pendulums.gif


                5. **Park Transformation**
                ~~~~~~~~~~~~~~~~~~~~~~~~~~
                    In electrical engineering, Park transformation (also called the direct-quadrature-zero (d-q-0) transformation) is a mathematical technique used to simplify the analysis and control of three-phase AC circuits, especially in rotating machines like motors and generators.

                .. figure:: images/Parktransform.gif
                    :align: center
                    :alt: Parktransform.gif

                

                Structure of the Book
                *********************

                The text begins with foundational principles—Basic Operations and Syntax—before progressing to core mathematical structures such as Polynomials, Interpolation, and Special Functions. These form the building blocks for the more complex computational engines within the library.

                The middle chapters delve into the heart of numerical computing: Linear Algebra, Integration, and the solution of Ordinary Differential Equations (ODEs). Each section demonstrates how SepalSolver can be applied to real-world problems, offering readers both theoretical insight and practical implementation guidance.

                Finally, the book explores high-level applications in Numerical Optimization and Partial Differential Equations (PDEs). Worked examples, case studies, and performance benchmarks illustrate the solver’s efficiency and adaptability across diverse domains, including engineering, physics, and data science.
                
                This book is intended for students, researchers, and professionals seeking a deeper understanding of numerical methods and their computational realization. By combining rigorous mathematical exposition with hands-on solver applications, it equips readers with the tools to design, analyze, and implement robust numerical solutions. Ultimately, the integration of SepalSolver into the study of numerical methods highlights the evolving synergy between mathematical theory and computational innovation.


                Getting Started
                ===============
                This video explains how to get started with a console project and install SepalSolver nuget packages

                .. youtube:: v3I3McaUMfY

                   :width: 960
                   :height: 540





                Content
                ===========

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
                    {new string('#', relativePath.Length)}

                    """;
                using (StreamWriter writer = new(chapterfile))
                {
                    writer.WriteLine(chaptermessage);
                }
                string[] ChapterSections = Directory.GetFiles(BookChapter, "*.cs");
                relativePath = Path.GetRelativePath(BookChapter, ChapterSections[0]);
                var sectionname = string.Join(' ', [.. relativePath.Split(['_']).Skip(2)]);
                string outputPath = bookfolder + sectionname + ".rst";
                string[] Content = File.ReadAllLines(ChapterSections[0]);
                List<string> bookContent = [..Content.SkipWhile(line=> !line.Contains("/// <BookContent>")).
                                              TakeWhile(line=>!line.Contains("/// </BookContent>"))];
                if (bookContent.Count > 0)
                    bookContent = processBookContent(bookContent[1..]);
                using (StreamWriter writer = new(chapterfile, true))
                {
                    foreach (var line in bookContent)
                        writer.WriteLine(line);

                    string chapterchildren = $"""
                    



                    .. toctree::

                    """;
                    writer.WriteLine(chapterchildren);
                }

                ChapterSections = ChapterSections[1..];

                foreach (string ChapterSection in ChapterSections)
                {
                    relativePath = Path.GetRelativePath(BookChapter, ChapterSection);
                    sectionname = string.Join(' ', [.. relativePath.Split(['_']).Skip(2)]);
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
            Console.WriteLine(classname);
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
            if (bookContent.Count == 0) return;

            //Headers format
            bookContent = processBookContent(bookContent[1..]);

            using (StreamWriter writer = new(outputPath, true))
            {
                foreach (var line in bookContent)
                    writer.WriteLine(line);
            }
        }

        List<string> processBookContent(List<string> bookContent)
        {
            TreatHeader1(bookContent);
            TreatHeader2(bookContent);
            TreatHeader3(bookContent);
            TreatFigure(bookContent);
            TreatMathTag(bookContent);
            TreatNoteTag(bookContent);
            TreatCodeBlock(bookContent);
            TreatTableBlock(bookContent);
            TreatExampleBlock(bookContent);
            TreatDocSlashed(bookContent);
            return bookContent;
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
                var match = Regex.Match(line, @"<header 1>(.*?)</header");

                if (match.Success)
                {
                    string header1 = match.Groups[1].Value;
                    header1 = header1.TrimStart().Trim();
                    List<string> header1lines = [header1, new string('=', header1.Length)];
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
                var match = Regex.Match(line, @"<header 2>(.*?)</header");

                if (match.Success)
                {
                    string header2 = match.Groups[1].Value;
                    header2 = header2.TrimStart().Trim();
                    List<string> header2lines = [header2, new string('-', header2.Length)];
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
                var match = Regex.Match(line, @"<header 3>(.*?)</header");

                if (match.Success)
                {
                    string header3 = match.Groups[1].Value;
                    header3 = header3.TrimStart().Trim();
                    List<string> header3lines = [header3, new string('~', header3.Length)];
                    Replace(bookContent, startIndex, 1, header3lines);
                }

            }
        }
        static void TreatFigure(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<figure>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<figure>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                string line = bookContent[startIndex];
                var match = Regex.Match(line, @"<figure>(.*?)</figure");

                if (match.Success)
                {
                    string figurename = match.Groups[1].Value;
                    figurename = figurename.TrimStart().Trim();
                    List<string> figurelines = [
                        $".. figure:: images/{figurename}",
                        $"    :align: center",
                        $"    :alt: {figurename}"
                    ];
                    Replace(bookContent, startIndex, 1, figurelines);

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
                List<string> Codelines = ["", ".. code-block:: csharp"];
                string line = bookContent[startIndex + Length];
                if (line.Contains("///"))
                {
                    int start = line.LastIndexOf('/') + 3;
                    while (!bookContent[startIndex + Length].Contains("</code>"))
                    {
                        line = bookContent[startIndex + Length];
                        if (line.Length >= start)
                            Codelines.Add(line.Substring(start));
                        else
                            Codelines.Add("");
                        Length++;
                    }
                }
                else
                {
                    List<string> Imagelines = [];
                    List<string> Outputlines = [];
                    bool loadoutputfile = false;
                    string terminalfilename = "";
                    int space = bookContent[startIndex + Length].TakeWhile(c => c == ' ').Count()+1;
                    while (!bookContent[startIndex + Length].Contains("</code>"))
                    {
                        line = bookContent[startIndex + Length];
                        if (line.Length >= space)
                            Codelines.Add(line.Substring(space));
                        else
                            Codelines.Add(line);
                        Length++;

                        if (line.Contains("SaveAs"))
                            Imagelines.AddRange(GetImageReference(line));
                        if (line.Contains("AnimationMaker"))
                            Imagelines.AddRange(GetAnimationReference(line));
                    }
                    Codelines.AddRange(Writer.CodeRunner([.. Codelines.Skip(2)]));
                    Codelines.AddRange(Imagelines);
                }
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }
        static string[] GetImageReference(string line)
        {
            // Regex to capture content inside parentheses
            // Regex to capture everything between the first '(' and the last ')'
            var match = Regex.Match(line, @"\(([^)]*)\)");
            string firstArg = "";
            if (match.Success)
            {
                string allArgs = match.Groups[1].Value; // "a, m0, m1"
                string[] args = allArgs.Split(',');

                if (args.Length >= 2)
                    firstArg = args[0].Trim().Trim('"', '\'');
            }
            return ["",
                    ".. figure:: images/" +  firstArg,
                    "   :align: center",
                    "   :alt: " + firstArg,
                    ""];
        }
        static string[] GetAnimationReference(string line)
        {
            // Regex to capture content inside parentheses
            // Regex to capture everything between the first '(' and the last ')'
            var match = Regex.Match(line, @"\(([^)]*)\)");
            string secondArg = "";
            if (match.Success)
            {
                string allArgs = match.Groups[1].Value; // "a, 15, m0, m1"
                string[] args = allArgs.Split(',');

                if (args.Length >= 2)
                    secondArg = args[1].Trim().Trim('"', '\'');
            }
            return ["",
                    ".. figure:: images/" +  secondArg,
                    "   :align: center",
                    "   :alt: " + secondArg,
                    ""];
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
                while (!bookContent[startIndex + Length].Contains("</table>"))
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
                string line = bookContent[startIndex];
                string contentangle = Regex.Match(line, "<(.*?)>").Value;
                string[] examplen = contentangle.Substring(1, contentangle.Length-2).Split(' ');
                List<string> Codelines = ["", $".. Admonition:: Example {examplen[1]} : {line.Substring(line.IndexOf(">")+1)}", ""];

                while (!bookContent[startIndex + Length].Contains("</example"))
                {
                    line = bookContent[startIndex + Length];
                    if (line.Contains("///"))
                        Codelines.Add("   " + line.TrimStart(' ', '\t', '/'));
                    else
                        Codelines.Add("   " + line);
                    Length++;
                }
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }
        static void TreatDocSlashed(List<string> bookContent)
        {
            for (int i = 0; i < bookContent.Count; i++)
            {
                if (bookContent[i].Contains("///"))
                    bookContent[i] = bookContent[i].TrimStart(' ', '\t', '/');
            }
        }
        static void TreatMathTag(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<math>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<math>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                int Length = 1;
                List<string> Codelines = ["", ".. math::", ""];
                string line = bookContent[startIndex + Length];
                int space = line.TakeWhile(c => c == ' ').Count()+1;
                while (!bookContent[startIndex + Length].Contains("</math>"))
                {
                    line = bookContent[startIndex + Length];
                    if (line.Contains("///"))
                        Codelines.Add("   " + line.TrimStart(' ', '\t', '/'));
                    Length++;
                }
                Codelines.Add("");
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }
        static void TreatNoteTag(List<string> bookContent)
        {
            while (bookContent.Any(line => line.Contains("<note>")))
            {
                int startIndex = -1;
                // replace code blocks with rst format
                for (int i = 0; i < bookContent.Count; i++)
                {
                    if (bookContent[i].Contains("<note>"))
                    {
                        startIndex = i;
                        break;
                    }
                }
                int Length = 1;
                List<string> Codelines = ["", ".. note::", ""];
                string line = bookContent[startIndex + Length];
                int space = line.TakeWhile(c => c == ' ').Count()+1;
                while (!bookContent[startIndex + Length].Contains("</note>"))
                {
                    line = bookContent[startIndex + Length];
                    if (line.Contains("///"))
                        Codelines.Add("   " + line.TrimStart(' ', '\t', '/'));
                    Length++;
                }
                Codelines.Add("");
                Replace(bookContent, startIndex, Length + 1, Codelines);
            }
        }

    }
}
