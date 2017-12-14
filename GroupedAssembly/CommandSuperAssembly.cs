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

            using (Transaction t = new Transaction(doc))
            {
                if (untouchBeams == true)
                {
                    List<FamilyInstance> beams = new List<FamilyInstance>();
                    foreach (ElementId ei in selids)
                    {
                        Element elem = doc.GetElement(ei);
                        FamilyInstance fin = elem as FamilyInstance;
                        if (fin == null) continue;
                        if(fin.Category.Id.IntegerValue != (int)BuiltInCategory.OST_StructuralFraming)
                        if (fin.StructuralType != StructuralType.Beam) continue;
                        if (fin.StructuralType != StructuralType.Brace) continue;

                        beams.Add(fin);
                    }

                    t.Start("Открепление балок");

                    foreach (FamilyInstance fin in beams)
                    {
                        try
                        {
                            StructuralFramingUtils.DisallowJoinAtEnd(fin, 1);
                            StructuralFramingUtils.DisallowJoinAtEnd(fin, 0);

                            double oldElev = fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).AsDouble();
                            fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(0);
                            fin.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).Set(oldElev);
                        }
                        catch { }
                    }

                    t.Commit();
                }


                t.Start("Создание сборки");

                allIds = AssemblyUtil.GetAllNestedIds(doc, selids, name);
                Element mainElem = doc.GetElement(selids.First());
                AssemblyInstance ai = AssemblyInstance.Create(doc, allIds, mainElem.Category.Id);

                t.Commit();

                t.Start("Именование сборки");
                ai.AssemblyTypeName = name;
                t.Commit();

                if (groupedElements == true)
                {
                    t.Start("Создание группы");
                    group = doc.Create.NewGroup(allIds);

                    t.Commit();

                    t.Start("Именование группы");
                    GroupType gtype = group.GroupType;
                    gtype.Name = name;
                    t.Commit();
                }
            }

            List<ElementId> groupId = new List<ElementId> { group.Id };
            sel.SetElementIds(groupId);

            return Result.Succeeded;
        }
    }
}
