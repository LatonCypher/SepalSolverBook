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
            var BookContent = new BookWriter(ProjectFolder, BookFolder);
            BookContent.WriteBook();
        }
    }
}
