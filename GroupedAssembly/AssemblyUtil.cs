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
        public static List<ElementId> GetAllNestedIds(Document doc, List<ElementId> selectedIds, string name)
        {

            List<ElementId> ids2 = new List<ElementId>();

            foreach (ElementId id in selectedIds)
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

            

            return ids2;
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
