using ScottPlot;

namespace ConsoleApp1
{
    internal class Writer
    {
        static string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;
        static string ProjectFolder, BookFolder, ImageFolder;
        public static void SetPath()
        {
            projectRoot = projectRoot.Replace("bin", "");
            ProjectFolder = projectRoot + "TrainingFiles\\";
            BookFolder = projectRoot + "docs\\source\\";
            ImageFolder = BookFolder + "images\\";
            folderpath = ImageFolder;
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
            return ["", "Ouput", "", ".. terminal::", "",
                      ..lines.Select(l => "   " + l)];
        }

    }
}
