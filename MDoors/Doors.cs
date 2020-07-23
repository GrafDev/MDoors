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

namespace MDoors
{

    internal static class Doors
    {
        static internal List<Door> mirrored=new List<Door>(); //Список отзеркаленых дверей документа

        static internal int FindMirrored(Document doc)
        {
            List<Door> allDoors = new List<Door>();
            List<Door> mirroredDoors = new List<Door>();

            FilteredElementCollector collectorDoors = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors);
            collectorDoors.OfClass(typeof(FamilyInstance));

            foreach (FamilyInstance elem in collectorDoors) //Перебор и заполнение списка всех установленных  дверей докумена
            {
                allDoors.Add(new Door(elem));
            }
            foreach (Door elem in allDoors)
            {
                if (elem.Mirrored)
                {
                    mirroredDoors.Add(elem);//Перебор и заполнение списка отзеркаленных дверей документа
                    {
                    }
                }
                mirrored = mirroredDoors;
            }
            return mirrored.Count;

        }//Поиска отзеркаленых дверей документа
     }//Класс дверей  проекта

    internal class Door // Класс дверь
    {
        internal double Width;
        internal double Height;
        internal bool Mirrored; //Отражена ли зеркально дверь
        internal XYZ xyz; //Координаты двери
        internal Door(FamilyInstance familyInstance)
        {
            this.xyz = (familyInstance.Location as LocationPoint).Point;
            this.Mirrored = familyInstance.Mirrored;
            this.Width = familyInstance.get_Parameter(BuiltInParameter.DOOR_WIDTH).AsDouble();
            this.Height = familyInstance.get_Parameter(BuiltInParameter.DOOR_HEIGHT).AsDouble();
        }
    }


}
