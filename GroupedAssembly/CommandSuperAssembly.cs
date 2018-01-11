using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;

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
            bool groupedElements = form.GroupedElements;
            bool untouchBeams = form.UntouchBeams;
            
            List<ElementId> selids = sel.GetElementIds().ToList();
            List<ElementId> allIds = new List<ElementId>();

            Group group = null;
            AssemblyInstance ai = null;

            allIds = AssemblyUtil.GetAllNestedIds(doc, selids, name);

            List<ElementId> finalSelIds = new List<ElementId>();
            using (Transaction t = new Transaction(doc))
            {
                if (untouchBeams)
                {
                    List<FamilyInstance> beams = new List<FamilyInstance>();
                    foreach (ElementId ei in selids)
                    {
                        Element elem = doc.GetElement(ei);
                        FamilyInstance fin = elem as FamilyInstance;
                        if (fin == null) continue;
                        if (fin.Category.Id.IntegerValue != (int)BuiltInCategory.OST_StructuralFraming) continue;
                        if (fin.StructuralType != StructuralType.Beam 
                            && fin.StructuralType != StructuralType.Brace) continue;

                        beams.Add(fin);
                    }

                    if (beams.Count > 0)
                    {
                        t.Start("Открепление балок");

                        foreach (FamilyInstance fin in beams)
                        {
                            try
                            {
                                StructuralFramingUtils.DisallowJoinAtEnd(fin, 1);
                                StructuralFramingUtils.DisallowJoinAtEnd(fin, 0);

                                double oldElev = fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).AsDouble();
                                fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(1);
                                fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(oldElev);
                            }
                            catch { }
                        }

                        t.Commit();
                    }
                }


                allIds = AssemblyUtil.GetAllNestedIds(doc, selids, name);
                Element mainElem = doc.GetElement(selids.First());

                //проверяю, могут ли элементы использоваться в сборке
                List<ElementId> idsForAssembly = new List<ElementId>();
                List<ElementId> idsNotForAssembly = new List<ElementId>();
                string messageAssemblyNotAllowed = "";

                foreach(ElementId id in allIds)
                {
                    Element elem = doc.GetElement(id);
                    bool check = AllowedForAssembly.Check(elem);
                    if (check == true)
                    {
                        idsForAssembly.Add(id);
                    }
                    else
                    {
                        idsNotForAssembly.Add(id);
                        messageAssemblyNotAllowed += id.IntegerValue.ToString() + "; ";
                    }
                }

                if (idsNotForAssembly.Count > 0)
                {
                    TaskDialog.Show("Внимание", "Некоторые элементы не были включены в сборку. ID: " + messageAssemblyNotAllowed);
                }

                try
                {
                    t.Start("Создание сборки");
                    ai = AssemblyInstance.Create(doc, idsForAssembly, mainElem.Category.Id);
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

                if (groupedElements)
                {
                    t.Start("Создание группы");
                    group = doc.Create.NewGroup(allIds);
                    t.Commit();
                    finalSelIds.Add(group.Id);

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
                else
                {
                    finalSelIds.Add(ai.Id);
                }
            }

            
            sel.SetElementIds(finalSelIds);

            return Result.Succeeded;
        }
    }
}
