using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace GroupedAssembly
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]

    class CommandSuperAssembly : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            Selection sel = commandData.Application.ActiveUIDocument.Selection;

            if (sel.GetElementIds().Count == 0)
            {
                message = "Не выбраны элементы";
                return Result.Failed;
            }

            FormEnterName form = new FormEnterName();
            form.LabelText = "Укажите имя сборки:";
            form.ShowDialog();
            if (form.DialogResult != System.Windows.Forms.DialogResult.OK) return Result.Cancelled;
            string name = form.NameText;

            List<ElementId> selids = sel.GetElementIds().ToList();
            List<ElementId> allIds = new List<ElementId>();

            Group group = null;
            AssemblyInstance ai = null;

            allIds = AssemblyUtil.GetAllNestedIds(doc, selids, name);

            using (Transaction t = new Transaction(doc))
            {
                
                Element mainElem = doc.GetElement(selids.First());
                try
                {
                    t.Start("Создание сборки");
                    ai = AssemblyInstance.Create(doc, allIds, mainElem.Category.Id);
                    t.Commit();
                }
                catch (Exception ex)
                {
                    message += "Не удалось создать сборку: " + ex.Message;
                    return Result.Failed;
                }

                try
                {

                    t.Start("Именование сборки");
                    ai.AssemblyTypeName = name;
                    t.Commit();
                }
                catch
                {
                    message += "\nНе удалось задать имя сборки. Установлено имя: " + ai.AssemblyTypeName;
                }
                
                t.Start("Создание группы");
                group = doc.Create.NewGroup(allIds);

                t.Commit();

                try
                {
                    t.Start("Именование группы");
                    GroupType gtype = group.GroupType;
                    gtype.Name = name;
                    t.Commit();
                }
                catch
                {
                    message += "\nНе удалось задать имя группы. Установлено имя: " + group.GroupType.Name;
                }
            }

            List<ElementId> groupId = new List<ElementId> { group.Id };
            sel.SetElementIds(groupId);

            return Result.Succeeded;
        }
    }
}
