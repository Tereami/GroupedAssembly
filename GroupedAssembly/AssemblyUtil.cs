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


using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace GroupedAssembly
{
    public static class AssemblyUtil
    {
        public static List<ElementId> GetAllNestedIds(Document doc, List<ElementId> selectedIds)
        {
            var ids2 = new List<ElementId>();

            foreach (var id in selectedIds)
            {
                ids2.Add(id);
                var elem = doc.GetElement(id);
                if (!(elem is FamilyInstance fi)) continue;

                var nestedFamInstances = GetAllSharedNestedFamInstances(fi);
                if (nestedFamInstances.Count == 0) continue;

                var nestedFiIds = nestedFamInstances.Select(i => i.Id).ToList();
                ids2.AddRange(nestedFiIds);
            }

            return ids2.Count == 0 ? null : ids2;
        }


        private static List<FamilyInstance> GetAllSharedNestedFamInstances(FamilyInstance fi)
        {
            var nestedFams = new List<FamilyInstance>();

            var doc = fi.Document;

            var nestedIds = fi.GetSubComponentIds();

            foreach (var id in nestedIds)
            {
                if (!(doc.GetElement(id) is FamilyInstance nestedFi)) continue;

                var nestedFam = nestedFi.Symbol.Family;
                if (string.IsNullOrEmpty(nestedFam.Name)) continue;
                if (nestedFam.get_Parameter(BuiltInParameter.FAMILY_SHARED).AsInteger() != 1) continue;
                if (nestedFam.IsEditable != true) continue;

                nestedFams.Add(nestedFi);

                var nestedFams2 = GetAllSharedNestedFamInstances(nestedFi);
                nestedFams.AddRange(nestedFams2);
            }

            return nestedFams;
        }
    }
}