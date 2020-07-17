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

namespace MDoors
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class MarkDoors : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            Doors doors;
            FlagOfError flag;
            try
            {
                doors = new Doors(doc);
                flag = new FlagOfError(doc);
                flag.PlaceErrorFamily(doc,doors.mirrored);
                //error = new FlagOfError(doc);
                //error.PlaceErrorFamily(doc, doors.mirrored);
                doors.ShowCount();
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
    }

}
