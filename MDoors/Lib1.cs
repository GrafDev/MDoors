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
using VCRevitRibbonUtil;
using Autodesk.Revit.DB.Structure;

namespace MDoors
{
    internal class Door // Класс дверей

    {
        internal Parameter Width;//Ширина двери  
        internal Parameter Height;// Высота двери
        internal bool Mirrored; //Отражена ли зеркально дверь
        internal XYZ xyz; //Координаты двери
        internal Door(FamilyInstance familyInstance)
        {
            this.xyz = (familyInstance.Location as LocationPoint).Point;
            this.Mirrored = familyInstance.Mirrored;
            this.Width = familyInstance.Symbol.LookupParameter(BuiltInParameter.DOOR_WIDTH.ToString());
            this.Height = familyInstance.Symbol.LookupParameter(BuiltInParameter.DOOR_HEIGHT.ToString());

        }

    }

    internal class Doors
    {
        
        internal List<Door> mirrored = new List<Door>();
        internal List<Door> unMirrored = new List<Door>();
        private Document doc;
        internal Doors(Document doc)
        {
            List<Door> allDoors = new List<Door>();
            this.doc = doc;
            FilteredElementCollector collectorDoors = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors);
            collectorDoors.OfClass(typeof(FamilyInstance));
            foreach (FamilyInstance elem in collectorDoors)
            {
                allDoors.Add(new Door(elem));
            }
            foreach (Door elem in allDoors)
            {

                if (elem.Mirrored)
                {
                    mirrored.Add(elem);
                }
                else
                {
                    unMirrored.Add(elem);
                }
            }

        }

        internal void ShowCount()
        {
            //string outDoors = "Все двери-" + allDoors.Count.ToString();
            using (Transaction tx = new Transaction(this.doc))
            {
                tx.Start("Delete Errors");
                string outDoors = "Отзеркаленные двери-" + mirrored.Count.ToString() + "\n" + "Не отзеркаленные двери-" + unMirrored.Count.ToString();
                TaskDialog.Show("Revit  ", outDoors);
                tx.Commit();
            }
        } // Выводит количество  дверей*/

    }
    class FlagOfError
    {
        internal Family family;

        //string fullPath = @"C:\Users\gregy\source\repos\DoorsMirrored\Properties\";
        string fullPath = @"C:\Users\jakovlevgm\source\repos\MDoors\";
        string familyName = "ADSK_Error_Place";


        internal FlagOfError(Document doc)
        {
            this.LoadErrorFamily(doc);
        }


        private void LoadErrorFamily(Document doc)// Загрузка семейства из файла.
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
                    tx.Commit();
                }
            }

        }

        internal void PlaceErrorFamily(Document doc, List<Door> doors)
        {
            ISet<ElementId> elementSet = family.GetFamilySymbolIds();
            FamilySymbol familyType = doc.GetElement(elementSet.First()) as FamilySymbol;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("вставка семейства");
                familyType.Activate();
                foreach (Door elem in doors)
                {
                    XYZ point = elem.xyz;
                    FamilyInstance fam = doc.Create.NewFamilyInstance(point, familyType, StructuralType.NonStructural);
                }
                tx.Commit();
            }
        }//Устанавливает семейтсво в точки отзеркаленых дверей
    }


}
