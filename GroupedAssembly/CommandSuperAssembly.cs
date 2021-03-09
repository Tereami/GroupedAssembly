//Данный код опубликован под лицензией Creative Commons Attribution-ShareAlike.
//Разрешено редактировать, изменять и брать данный код за основу для производных в коммерческих и некоммерческих целях,
//при условии указания авторства и если производные лицензируются на тех же условиях.
//Программа поставляется "как есть". Автор не несет ответственности за возможные последствия её использования.
//Зуев Александр, 2021, все права защищены.
//This code is listed under the Creative Commons Attribution-ShareAlike license.
//You may redistribute, remix, tweak, and build upon this work commercially and non-commercially, 
//as long as you credit the author by linking back and license your new creations under the same terms.
//This code is provided 'as is'. Author disclaims any implied warranty. 
//Zuev Aleksandr, 2021, all rigths reserved.

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
            UIApplication uiApp = commandData.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            Autodesk.Revit.UI.Selection.Selection sel = commandData.Application.ActiveUIDocument.Selection;
            List<ElementId> selids = sel.GetElementIds().ToList();

            if (selids.Count == 0)
            {
                message = "Не выбраны элементы";
                return Result.Failed;
            }

            bool createAssemblyByGroup = false;
            string defaultName = "";
            if(selids.Count == 1)
            {
                Group existGroup = doc.GetElement(selids[0]) as Group;
                if(existGroup != null)
                {
                    createAssemblyByGroup = true;
                    defaultName = existGroup.Name;
                }
            }


            FormEnterName form = new FormEnterName(createAssemblyByGroup, defaultName);
            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK) 
                return Result.Cancelled;

            string name = form.NameText;
            bool groupedElements = form.GroupedElements;
            bool untouchBeamsEnds = form.UntouchBeamsEnds;
            bool untouchBeamsPlane = form.UntouchBeamsPlane;

            List<ElementId> allIds = new List<ElementId>();

            Group group = null;
            AssemblyInstance ai = null;

            allIds = AssemblyUtil.GetAllNestedIds(doc, selids);

            List<ElementId> finalSelIds = new List<ElementId>();
            using (Transaction t = new Transaction(doc))
            {
                if (untouchBeamsEnds || untouchBeamsPlane)
                {
                    List<FamilyInstance> beams = new List<FamilyInstance>();
                    foreach (var ei in selids)
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
                            try
                            {
                                if (untouchBeamsEnds)
                                {
                                    StructuralFramingUtils.DisallowJoinAtEnd(fin, 1);
                                    StructuralFramingUtils.DisallowJoinAtEnd(fin, 0);
                                }
                                if (untouchBeamsPlane)
                                {
                                    double oldElev = fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION)
                                            .AsDouble();
                                    fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(1);
                                    fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(oldElev);
                                }
                            }
                            catch
                            {
                            }

                        t.Commit();
                    }
                }


                allIds = AssemblyUtil.GetAllNestedIds(doc, selids);


                //проверяю, могут ли элементы использоваться в сборке
                List<ElementId> idsForAssembly = new List<ElementId>();
                List<ElementId> idsNotForAssembly = new List<ElementId>();
                string messageAssemblyNotAllowed = "";

                foreach (ElementId id in allIds)
                {
                    Element elem = doc.GetElement(id);
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

                if (idsNotForAssembly.Count > 0 && idsForAssembly.Count > 0)
                {
                    TaskDialog.Show("Внимание", "Не были включены в сборку элементы с id: " + messageAssemblyNotAllowed);
                }
                if(idsForAssembly.Count == 0)
                {
                    message = "Нет элементов, доступных для включения в сборку.";
                    foreach(ElementId id in idsNotForAssembly)
                    {
                        elements.Insert(doc.GetElement(id));
                    }
                    return Result.Failed;
                }
                
                Element mainElem = doc.GetElement(idsForAssembly.First());

                t.Start("Создание сборки");
                try
                {
                    ai = AssemblyInstance.Create(doc, idsForAssembly, mainElem.Category.Id);
                }
                catch (Exception ex)
                {
                    message += "Не удалось создать сборку: " + ex.Message;
                    return Result.Failed;
                }
                t.Commit();
                t.Start("Задание имени сборки");
                try
                {
                    ai.AssemblyTypeName = name;
                }
                catch
                {
                    message += "\nНе удалось задать имя сборки. Установлено имя: " + ai.AssemblyTypeName;
                }
                t.Commit();
                t.Start("Создание группы");

                if (groupedElements)
                {
                    group = doc.Create.NewGroup(allIds);
                    
                    finalSelIds.Add(group.Id);

                    try
                    {
                        var gtype = group.GroupType;
                        gtype.Name = name;
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
                t.Commit();
            }


            sel.SetElementIds(finalSelIds);

            return Result.Succeeded;
        }
    }
}