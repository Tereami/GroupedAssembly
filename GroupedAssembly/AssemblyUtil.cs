using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace GroupedAssembly
{
    public static class AssemblyUtil
    {
        public static AssemblyInstance CreateAssembly(Document doc, Transaction t, List<ElementId> ids, string name)
        {
            try
            {
                t.Start("Создание сборки");
            }
            catch
            {
                t.Commit();
                t.Start("Создание сборки");
            }

            List<ElementId> ids2 = new List<ElementId>();

            foreach (ElementId id in ids)
            {
                ids2.Add(id);
                Element elem = doc.GetElement(id);
                FamilyInstance fi = elem as FamilyInstance;
                if (fi == null) continue;

                List<FamilyInstance> nestedFamInstances = GetAllSharedNestedFamInstances(fi);
                if (nestedFamInstances.Count == 0) continue;

                List<ElementId> nestedFiIds = nestedFamInstances.Select(i => i.Id).ToList();
                ids2.AddRange(nestedFiIds);
            }

            if (ids2.Count == 0) return null;

            Element mainElem = doc.GetElement(ids2.First());

            AssemblyInstance ai = AssemblyInstance.Create(doc, ids2, mainElem.Category.Id);

            t.Commit();

            t.Start("Именование сборки");
            ai.AssemblyTypeName = name;
            t.Commit();

            return ai;
        }

        public static Group CreateGroup(Document doc, Transaction t, List<Element> elems, string name)
        {
            try
            {
                t.Start("Создание группы");
            }
            catch
            {
                t.Commit();
                t.Start("Создание группы");
            }


            List<ElementId> ids2 = elems.Select(i => i.Id).ToList();

            Group group = doc.Create.NewGroup(ids2);

            t.Commit();

            t.Start("Cоздание группы для изделия2");
            GroupType gtype = group.GroupType;
            gtype.Name = name;
            t.Commit();

            return group;
        }



        public static List<FamilyInstance> GetAllSharedNestedFamInstances (FamilyInstance fi)
        {
            List<FamilyInstance> nestedFams = new List<FamilyInstance>();

            Document doc = fi.Document;

            ICollection<ElementId> nestedIds = fi.GetSubComponentIds();

            foreach(ElementId id in nestedIds)
            {
                FamilyInstance nestedFi = doc.GetElement(id) as FamilyInstance;
                if (nestedFi == null) continue;

                Family nestedFam = nestedFi.Symbol.Family;
                if (string.IsNullOrEmpty(nestedFam.Name)) continue;
                if (nestedFam.get_Parameter(BuiltInParameter.FAMILY_SHARED).AsInteger() != 1) continue;
                if (nestedFam.IsEditable != true) continue;

                nestedFams.Add(nestedFi);

                List<FamilyInstance> nestedFams2 = GetAllSharedNestedFamInstances(nestedFi);
                nestedFams.AddRange(nestedFams2);
            }

            return nestedFams;
        }
    }
}
