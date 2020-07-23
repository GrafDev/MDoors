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
    public class CleanDoors : IExternalCommand
    {
        internal static Document doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            doc = uiApp.ActiveUIDocument.Document;
            try
            {
                Flags.LoadFamily(doc); // Load family mark
                if (Start.dialogBoxShow)//if allowed show dialog box is  then
                {
                    TaskDialog.Show("Mirrored doors", "Removed " + Flags.Clean(doc) + " mirrored door markers");//Show dialog box. Remove marks  from a document and show how much removed
                }
                else//if not allowed show dialog box is then
                {
                    Flags.Clean(doc);//Remove marks
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

    } // Defines methods and properties for clearing mirrored door marks from a document


}
