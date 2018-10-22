using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace GroupedAssembly
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    internal class App : IExternalApplication
    {
        public static string assemblyPath = "";
        public static string settingsPath = "";


        public Result OnStartup(UIControlledApplication application)
        {
            var tabName = "Weandrevit";
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch
            {
            }

            assemblyPath = Assembly.GetExecutingAssembly().Location;
            settingsPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assemblyPath), "Settings");

            var panel1 = application.CreateRibbonPanel(tabName, "Сборки и группы");

            var btnSuperAssembly = panel1.AddItem(new PushButtonData(
                    "SuperAssembly",
                    "Супер\nСборка",
                    assemblyPath,
                    "GroupedAssembly.CommandSuperAssembly")
            ) as PushButton;
            btnSuperAssembly.Image = PngImageSource("GroupedAssembly.Icons.SuperAssemblySmall.png");
            btnSuperAssembly.LargeImage = PngImageSource("GroupedAssembly.Icons.SuperAssemblyBig.png");
            btnSuperAssembly.ToolTip = "Создание сгруппированной сборки, с включением всех вложенных элементов";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


        private System.Windows.Media.ImageSource PngImageSource(string embeddedPathname)
        {
            var st = GetType().Assembly.GetManifestResourceStream(embeddedPathname);
            var decoder = new PngBitmapDecoder(st, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }
    }
}