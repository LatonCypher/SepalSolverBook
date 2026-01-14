namespace ConsoleApp1
{
    internal class Writer
    {
        static string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;
        static string ProjectFolder, BookFolder, ImageFolder, TextFolder;
        static StreamWriter writer;
        public static void SetPath()
        {
            projectRoot = projectRoot.Replace("bin", "");
            ProjectFolder = projectRoot + "TrainingFiles\\";
            BookFolder = projectRoot + "docs\\source\\";
            ImageFolder = BookFolder + "images\\";
            TextFolder = BookFolder + "texts\\";
            folderpath = ImageFolder;
        }
        public static void SetOut(string filename)
        {
            writer?.Close();
            string fullpath = TextFolder + filename;
            if (File.Exists(fullpath)) File.Delete(fullpath);
            writer = new(fullpath, true)
            { AutoFlush = true };

            // Redirect the standard output
            Console.SetOut(writer);
        }

        public static string[] Load(string filename)
        {
            writer?.Close();
            string filepath = TextFolder + filename;
            string[] lines = File.ReadAllLines(filepath);
            string[] completelines = ["", ".. terminal::", "", ..lines.Select(l => "   " + l)];
            return completelines;
        }
        public static void Run()
        { 
            var BookContent = new BookWriter(ProjectFolder, BookFolder);
            BookContent.WriteBook();
        }
    }
}
