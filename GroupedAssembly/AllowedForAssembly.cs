using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace GroupedAssembly
{
    public static class AllowedForAssembly
    {
        public static bool Check(Element elem)            
        {
            Category cat = elem.Category;

            if (elem is AnalyticalModel) return false;
            if (elem is Truss) return false;
            if (elem is BeamSystem) return false;
            if (elem is Group) return false;
            if (elem is ImportInstance) return false;
            if (elem is CurveElement) return false;
            if (elem is SpatialElement) return false;
            if (elem is LoadBase) return false;
            if (elem is Autodesk.Revit.DB.Architecture.Railing) return false;


            return true;
        }
    }
}
