using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using System.Runtime.CompilerServices;
using Autodesk.Revit.DB.Structure;
namespace MDoors
{
    static class Flags
    {
        private static Family family;
        //private static Type familyType;
        private static string fullPath = @"C:\Users\jakovlevgm\source\repos\MDoors\";
        private static string familyName = "ADSK_Error_Place";
       // private static Document doc = MarkDoors.doc;

        private static void LoadFamily(Document doc)// Загрузка семейства из файла.
        {
            FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(Family));
            family = a.FirstOrDefault<Element>(e => e.Name.Equals(familyName)) as Family;
            if (null == family)
            {
                string FamilyPath = fullPath + familyName + ".rfa";                
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Загрузка семейства");                    
                    doc.LoadFamily(FamilyPath, out family);
                   // familyType = family.GetType();
                    tx.Commit();
                }
            }

        }

        static internal int Clean(Document doc)
        {
            List<FamilyInstance> doors = new List<FamilyInstance>();
            List<ElementId> flag = new List<ElementId>();
            FilteredElementCollector colFlags = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors);
            colFlags.OfClass(typeof(FamilyInstance));
            foreach (FamilyInstance elem in colFlags)
            {
                doors.Add(elem);
            }
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Delete Errors");
                foreach (FamilyInstance elem in doors)
                {
                    if (elem.Symbol.FamilyName == familyName)
                    {

                        flag.Add(elem.Id);
                        doc.Delete(elem.Id);
                    }
                }
                tx.Commit();
            }
            return flag.Count;
        }


        internal static int Place(Document doc)
        {
            LoadFamily(doc);
            Clean(doc);
            int count = 0;
            ISet<ElementId> elementSet = family.GetFamilySymbolIds();
            FamilySymbol familyType = doc.GetElement(elementSet.First()) as FamilySymbol;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("вставка семейства");
                familyType.Activate();
                foreach (Door door in Doors.mirrored)
                {
                    FamilyInstance fam = doc.Create.NewFamilyInstance(door.xyz, familyType, StructuralType.NonStructural);
                    fam.get_Parameter(BuiltInParameter.DOOR_WIDTH).Set(Convert.ToDouble(door.Width));
                    fam.get_Parameter(BuiltInParameter.DOOR_HEIGHT).Set(Convert.ToDouble(door.Height));
                    count++;
                }
                tx.Commit();
            }
            return count;
        }//Устанавливает семейтсво в точки отзеркаленых дверей
        



    }
}
