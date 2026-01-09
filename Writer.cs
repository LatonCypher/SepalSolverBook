namespace ConsoleApp1
{
    internal class Writer
    {
        public static void Run()
        {
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;
            projectRoot = projectRoot.Replace("bin", "");
            var ProjectFolder = projectRoot + "TrainingFiles\\";
            var BookFolder = projectRoot + "docs\\source\\";
            var ImageFolder = BookFolder + "images\\";
            var BookContent = new BookWriter(ProjectFolder, BookFolder);
            BookContent.WriteBook();
        }
    }
}
