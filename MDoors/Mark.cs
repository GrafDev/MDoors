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
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class MarkDoors: IExternalCommand
    {
        internal static Document doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            doc = uiApp.ActiveUIDocument.Document;

            try
            {
                Doors.FindMirrored(doc);
                Flags.LoadFamily(doc);
                Flags.Place(doc);//Установка семейства марки отзеркаленых дверей
                if (Start.flagsPlace)//Вывод диалоговых окон в зависимости от параметров// Если устанавливать марки можно то
                {
                    if (Start.dialogBoxShow)//Если показывать диалоговые окна можно то
                        TaskDialog.Show("Mirrored doors", "Found and mark " + Flags.counFlags + " mirrored doors");
                }
                else//Если устанавливать нельзя, то показываем диалоговое окно, даже если показывать диалоговые окна нельзя
                {
                    TaskDialog.Show("Mirrored doors", "Found  " + Doors.mirrored.Count + " mirrored doors");
                }

            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Cancelled;
            }
            return Result.Succeeded;
        }
    }// Клас маркировки отзеркаленых дверей


}
