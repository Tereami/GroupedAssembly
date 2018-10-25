using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

namespace GroupedAssembly
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    internal class CommandSuperAssembly : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var doc = commandData.Application.ActiveUIDocument.Document;

            var sel = commandData.Application.ActiveUIDocument.Selection;

            if (sel.GetElementIds().Count == 0)
            {
                message = "Не выбраны элементы";
                return Result.Failed;
            }

            var form = new FormEnterName();
            form.LabelText = "Укажите имя сборки:";
            form.ShowDialog();
            if (form.DialogResult != System.Windows.Forms.DialogResult.OK) return Result.Cancelled;

            var name = form.NameText;
            var groupedElements = form.GroupedElements;
            var untouchBeams = form.UntouchBeams;

            var selids = sel.GetElementIds().ToList();
            var allIds = new List<ElementId>();

            Group group = null;
            AssemblyInstance ai = null;

            allIds = AssemblyUtil.GetAllNestedIds(doc, selids);

            var finalSelIds = new List<ElementId>();
            using (var t = new Transaction(doc))
            {
                if (untouchBeams)
                {
                    var beams = new List<FamilyInstance>();
                    foreach (var ei in selids)
                    {
                        var elem = doc.GetElement(ei);
                        var fin = elem as FamilyInstance;
                        if (fin == null) continue;
                        if (fin.Category.Id.IntegerValue != (int) BuiltInCategory.OST_StructuralFraming) continue;
                        if (fin.StructuralType != StructuralType.Beam
                            && fin.StructuralType != StructuralType.Brace) continue;

                        beams.Add(fin);
                    }

                    if (beams.Count > 0)
                    {
                        t.Start("Открепление балок");

                        foreach (var fin in beams)
                            try
                            {
                                StructuralFramingUtils.DisallowJoinAtEnd(fin, 1);
                                StructuralFramingUtils.DisallowJoinAtEnd(fin, 0);

                                var oldElev = fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION)
                                        .AsDouble();
                                fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(1);
                                fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(oldElev);
                            }
                            catch
                            {
                            }

                        t.Commit();
                    }
                }


                allIds = AssemblyUtil.GetAllNestedIds(doc, selids);
                var mainElem = doc.GetElement(selids.First());

                //проверяю, могут ли элементы использоваться в сборке
                var idsForAssembly = new List<ElementId>();
                var idsNotForAssembly = new List<ElementId>();
                var messageAssemblyNotAllowed = "";

                foreach (var id in allIds)
                {
                    var elem = doc.GetElement(id);
                    if (elem.CanAssembling())
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
                    TaskDialog.Show("Внимание",
                            "Некоторые элементы не были включены в сборку. ID: " + messageAssemblyNotAllowed);

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
                        var gtype = group.GroupType;
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