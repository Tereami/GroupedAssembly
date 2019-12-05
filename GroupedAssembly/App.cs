//Данный код опубликован под лицензией Creative Commons Attribution-NonCommercial-ShareAlike.
//Разрешено редактировать, изменять и брать данный код за основу для производных в некоммерческих целях,
//при условии указания авторства и если производные лицензируются на тех же условиях.
//Программа поставляется "как есть". Автор не несет ответственности за возможные последствия её использования.
//Зуев Александр, 2019, все права защищены.
//This code is listed under the Creative Commons Attribution-NonCommercial-ShareAlike license.
//You may redistribute, remix, tweak, and build upon this work non-commercially, 
//as long as you credit the author by linking back and license your new creations under the same terms.
//This code is provided 'as is'. Author disclaims any implied warranty. 
//Zuev Aleksandr, 2019, all rigths reserved.


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