using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace GroupedAssembly
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]

    class App : IExternalApplication
    {
        public static string assemblyPath = "";
        public static string settingsPath = "";


        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "Weandrevit";
            try { application.CreateRibbonTab(tabName); } catch { }

            assemblyPath = Assembly.GetExecutingAssembly().Location;
            settingsPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assemblyPath), "Settings");

            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "Сборки и группы");

            PushButton btnSuperAssembly = panel1.AddItem(new PushButtonData(
                "SuperAssembly",
                "Супер\nСборка",
                assemblyPath,
                "GroupedAssembly.CommandSuperAssembly")
                ) as PushButton;
            btnSuperAssembly.LargeImage = PngImageSource("GroupedAssembly.Icons.Settings.png");
            btnSuperAssembly.ToolTip = "Создание сборки, включающей все вложенные элементы";

            PushButton btnGroupedAssembly = panel1.AddItem(new PushButtonData(
                "GroupedAssembly",
                "Сгруппированная\nСборка",
                assemblyPath,
                "GroupedAssembly.CommandGroupedAssembly")
                ) as PushButton;
            //btn1.LargeImage = PngImageSource("GroupedAssembly.Icons.Settings.png");
            btnGroupedAssembly.ToolTip = "Создание сгруппированной сборки (элементов, одновременно входящих в сборку и в группу)";


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


        private System.Windows.Media.ImageSource PngImageSource(string embeddedPathname)
        {
            System.IO.Stream st = this.GetType().Assembly.GetManifestResourceStream(embeddedPathname);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(st, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }

    }
}
