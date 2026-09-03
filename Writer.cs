namespace ConsoleApp1
{
    internal class Writer
    {
        static string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;
        static string ProjectFolder, BookFolder;
        public static void SetPath()
        {
            projectRoot = projectRoot.Replace("bin", "");
            ProjectFolder = projectRoot + "TrainingFiles\\";
            BookFolder = projectRoot + "docs\\source\\";
        }
        public static void Run()
        {
            SetPath();
            var BookContent = new BookWriter(ProjectFolder, BookFolder);
            BookContent.WriteBook();
        }

        public static string[] CodeRunner(List<string> myCode)
        {
            var consolewriter = Console.Out;
            using (StreamWriter writer = new(BookFolder + "output.txt"))
            {
                Console.SetOut(writer);
                // Running the code
                DynamicRunner.RunCode(myCode);
            }
            Console.SetOut(consolewriter);
            string[] lines = File.ReadAllLines(BookFolder + "output.txt");
            if(lines.Length == 0)
                return [];
            return ["", "\n Ouput", "", ".. terminal::", "",
                      ..lines.Where(line=>!(line.Contains("Optimal solution found") ||
                        line.Contains("Solving not completed"))).Select(l => "   " + l)];
        }

    }
}
