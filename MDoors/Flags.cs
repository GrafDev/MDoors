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
        private static Family family;      //Член марки отзеркаленой двери
        internal static string fullPath = Start.fullPath;//Взятие пути к семейству
        internal static string familyName = Start.familyName;//Взятие имени семейства
        internal static int counFlags = 0;
        
        static internal int Clean(Document doc)
        {
            List<FamilyInstance> doors = new List<FamilyInstance>(); //Список для установленых отзеркаленых дверей
            List<ElementId> flag = new List<ElementId>();//Список для ID марок отзеркаленых дверей
            FilteredElementCollector colFlags = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors);///Получение из документа
            colFlags.OfClass(typeof(FamilyInstance));                                                                   ///колекции всех дверей, так как марка отзеркаленых дверей
                                                                                                                        /// имеет кактегорию "Двери"
            foreach (FamilyInstance elem in colFlags)
            {
                doors.Add(elem);//Получение всех установленых дверей документа
            }
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Clean");
                foreach (FamilyInstance elem in doors)
                {
                    if (elem.Symbol.FamilyName == familyName)//Выявление элементов у которых совпадает имя семейства с именем семейства марки отзеркаленных дверей
                    {

                        flag.Add(elem.Id);// Добавление ID в список ID всех отзеркаленых дверей 
                        doc.Delete(elem.Id);// Удаление элемента из документа по его ID
                    }
                }
                tx.Commit();
            }
            return flag.Count;//Возвращение количества найденых отзеркаленых дверей(имено найденых) по этому стоит изменить затем, правильнее будет указывать именно количество удаленных
        }// метод очистки документа от марок отзеркаленых дверей


        internal static void Place(Document doc)
        {
            Clean(doc);
            ISet<ElementId> elementSet = family.GetFamilySymbolIds();
            FamilySymbol familyType = doc.GetElement(elementSet.First()) as FamilySymbol;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("insert famaly");
                familyType.Activate();
                foreach (Door door in Doors.mirrored)
                {
                    if (Start.flagsPlace)
                    {
                        FamilyInstance fam = doc.Create.NewFamilyInstance(door.xyz, familyType, StructuralType.NonStructural);
                        fam.get_Parameter(BuiltInParameter.DOOR_WIDTH).Set(Convert.ToDouble(door.Width));
                        fam.get_Parameter(BuiltInParameter.DOOR_HEIGHT).Set(Convert.ToDouble(door.Height));
                        counFlags++;
                    }                    
                }
                tx.Commit();
            }
        }//Устанавливает семейство марки отзеркаленых дверей в точки отзеркаленых дверей
        internal static void LoadFamily(Document doc)
        {
            FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(Family));
            family = a.FirstOrDefault<Element>(e => e.Name.Equals(familyName)) as Family;

            if (null == family)
            {
                string FamilyPath = fullPath + familyName + ".rfa";//Сблорка полного пути к семейству  марки отзеркаленых дверей
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("load family");
                    doc.LoadFamily(FamilyPath, out family);
                    tx.Commit();
                }
            }

        }// Загрузка семейства марки отзеркаленых дверей из файла.



    }//Класс с методами и свойствами марок отзеркаленых дверей
}
