using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class BookWriter (string BookFolder)
    {
        string bookfolder = BookFolder;
        public void WriteBook()
        {
            string[] BookChapters = Directory.GetDirectories(bookfolder);
            foreach (string BookChapter in BookChapters)
            {
                string[] ChapterSections = Directory.GetFiles(BookChapter, "*.cs");
                foreach (string ChapterSection in ChapterSections)
                {
                    Run(ChapterSection, BookChapter + "\\");
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
                var doc = getDoc(lines, ref start, out int indentation);
                if (doc.Length == 1 && doc[0] == "")
                    break;
                string signature = doc.Last().TrimEnd();
                string[] splitsignature = signature.Split(' ');
                string last = splitsignature.Last();

                string Name = ""; int opencount = 0;
                int ind = 0;
                for (int i = splitsignature.Length; i-->0;)
                {
                    opencount += parentIndex(splitsignature[i]);
                    if (opencount == 0)
                    { ind = i; break; }
                }
                if (last[last.Length - 1] == ')')
                {
                    // Method
                    Name = splitsignature[ind].Split('(').First();
                }
                else
                {
                    // Property
                    Name = last;

                }

                // check if method/property needs to be documented
                bool doccheck = signature.Contains("public") && !signature.Contains("operator") && !signature.Contains("override") && !signature.Contains("readonly") && !last.Contains("Exception");

                if (doccheck)
                {
                    // write for sphinx
                    string space0 = string.Concat(Enumerable.Repeat("   ", indentation - 2)),
                           space1 = string.Concat(Enumerable.Repeat("   ", indentation - 1)),
                           space2 = string.Concat(Enumerable.Repeat("   ", indentation));

                    bool paramSection = false;

                    List<string> Doc = ["\n\n"+space0 + Name, space0+string.Concat(Enumerable.Repeat("=", Name.Length))];
                    int i = -1;
                    while (i < doc.Count() - 1)
                    {
                        string line = doc[++i].Substring(3);
                        if (line.Contains("<summary"))
                        {
                            Doc.Add(space1 + "Description: ");
                            line = doc[++i].Substring(3);
                            while (!line.Contains("</summary>"))
                            {
                                if (line.Contains(".. math::"))
                                    Doc.Add("");
                                Doc.Add(space2 + line);
                                line = doc[++i].Substring(3);

                                if (line.Contains("<code"))
                                {
                                    Doc.Add("\n" + space2 + " .. code-block:: CSharp \n");
                                    line = doc[++i].Substring(3);
                                    while (!line.Contains("</code>"))
                                    {
                                        Doc.Add(space2 + line);
                                        line = doc[++i].Substring(3);
                                    }
                                    line = doc[++i].Substring(3);
                                }
                            }
                        }
                        else if (line.Contains("<note"))
                        {
                            Doc.Add("\n" + space1 + "..note::" + "\n");
                            while (!(line = doc[++i].Substring(3)).Contains("</note>"))
                            {
                                if (line.Contains(".. math::")) Doc.Add("");
                                Doc.Add(space2 + line);
                            }
                            Doc.Add("\n\n");
                        }
                        else if (line.Contains("<param") && !line.Contains("<paramref"))
                        {
                            if (!paramSection)
                            {
                                Doc.Add(space1 + "Parameters: ");
                                paramSection = true;
                            }
                            string[] linesplit = line.Split('"');
                            Doc.Add(space2 + " " + linesplit[1] + ": ");
                            string spacep = string.Concat(Enumerable.Repeat(" ", Doc.Last().IndexOf(':')));
                            line = doc[++i].Substring(3);
                            while (!line.Contains("</param>"))
                            {
                                Doc.Add(spacep + line);
                                line = doc[++i].Substring(3);
                            }
                        }
                        else if (line.Contains("<returns"))
                        {
                            Doc.Add(space1 + "Returns: ");
                            line = doc[++i].Substring(3);
                            while (!line.Contains("</returns>"))
                            {
                                Doc.Add(space2 + line);
                                line = doc[++i].Substring(3);
                            }
                        }
                        else if (line.Contains("<remarks"))
                        {
                            Doc.Add(space1 + "Remark: ");
                            line = doc[++i].Substring(3);
                            while (!line.Contains("</remarks>"))
                            {
                                Doc.Add(space2 + "| " + line);
                                line = doc[++i].Substring(3);
                            }
                        }
                        else if (line.Contains("<example"))
                        {
                            Doc.Add(space1 + "Example: ");
                            line = doc[++i].Substring(3);
                            while (!line.Contains("</example>"))
                            {
                                if (line.Contains(".. math::"))
                                    Doc.Add("");
                                Doc.Add(space2 + line);
                                line = doc[++i].Substring(3);

                                if (line.Contains("<code"))
                                {
                                    Doc.Add("\n" + space2 + " .. code-block:: CSharp \n");
                                    line = doc[++i].Substring(3);
                                    while (!line.Contains("</code>"))
                                    {
                                        Doc.Add(space2 + line);
                                        line = doc[++i].Substring(3);
                                    }
                                    line = doc[++i].Substring(3);
                                }

                                if (line.Contains("<terminal"))
                                {
                                    Doc.Add("\n" + space2 + "Output: \n");
                                    Doc.Add("\n" + space2 + " .. code-block:: Terminal \n");
                                    line = doc[++i].Substring(3);
                                    while (!line.Contains("</terminal>"))
                                    {
                                        Doc.Add(space2 + line);
                                        line = doc[++i].Substring(3);
                                    }
                                    line = doc[++i].Substring(3);
                                }

                                if (line.Contains("<figure"))
                                {
                                    Doc.Add("\n" + space2 + "Output: \n");
                                    line = doc[++i].Substring(3);
                                    while (!line.Contains("</figure>"))
                                    {
                                        Doc.Add(space1 + line);
                                        line = doc[++i].Substring(3);
                                    }
                                    Doc.Add("\n");
                                    line = doc[++i].Substring(3);
                                }
                            }
                        }
                        else if (line.Contains("<exception"))
                        {
                            line = line.Replace("<exception", "");
                            line = line.Replace("</exception>", "");
                            line = line.Replace("paramref", "");
                            line = line.Replace("name=", "");
                            line = line.Replace("<", "");
                            line = line.Replace(">", "");
                            line = line.Replace("</", "");
                            line = line.Replace("/", "");
                            line = line.Replace("\"", "");
                            line = line.Replace("xception", "xception is ");
                            Doc.Add(space0 + "| " + line);
                        }
                    }
                    Document.AddRange(Doc);
                }
            }

            int parentIndex(string str)
            {
                int ans = 0;
                if (str.Contains(')')) ans++;
                if (str.Contains('(')) ans--;
                return ans;
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
