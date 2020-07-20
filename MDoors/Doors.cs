﻿using System;
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
        static internal List<Door> mirrored=new List<Door>();


        private static Document doc = MarkDoors.doc;
        static internal int FindMirrored()
        {
            List<Door> allDoors = new List<Door>();
            List<Door> mirroredDoors = new List<Door>();
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
                    mirroredDoors.Add(elem);
                }
            }
            mirrored = mirroredDoors;
            return mirrored.Count;
        }
   /*     static internal void ShowCount()
        {
            //string outDoors = "Все двери-" + allDoors.Count.ToString();
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Delete Errors");
                string outDoors = "Отзеркаленные двери-" + Doors.mirrored.Count.ToString();
                TaskDialog.Show("Revit  ", outDoors);
                tx.Commit();
            }
        } // Выводит количество  дверей*/

    }

    internal class Door // Класс дверей
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