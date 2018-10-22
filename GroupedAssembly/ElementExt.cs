using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace GroupedAssembly
{
    public static class ElementExt
    {
        public static bool CanAssembling(this Element e)
        {
            return AssemblyInstance.AreElementsValidForAssembly(e.Document, new List<ElementId> {e.Id},
                ElementId.InvalidElementId);
        }
    }
}