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
using MDoors.Properties;
using Autodesk.Revit.Creation;
using System.Windows.Media;

namespace MDoors
{

    public class Start : IExternalApplication
    {
        internal string verRevit;
        internal static string fullPath;
        internal static string familyName = "ADSK_Error_Place";
        internal static bool dialogBoxShow = true;
        internal static bool flagsPlace = true;

        public Result OnStartup(UIControlledApplication application)
        {
            UIControlledApplication app = application;
            verRevit = app.ControlledApplication.VersionNumber;
            fullPath = $@"C:\ProgramData\Autodesk\Revit\Addins\{verRevit}\MDoors\";
            string panelName = "Mirrored Doors";
            string imagePaintSmall = "MDoors.icon_PaintError16.png";
            string imagePaintLarge = "MDoors.icon_PaintError32.png";
            string imageCleantSmall = "MDoors.icon_CleanError16.png";
            string imageCleanLarge = "MDoors.icon_CleanError32.png";
            string imageRibbonSmall = "MDoors.iconMain_Door16.png";
            string imageRibbonLarge =  "MDoors.iconMain_Door32.png";
            string imageParametersSmall = "MDoors.iconParameters16.png";
            string imageParametersLarge = "MDoors.iconParameters32.png";

            string classMarkName = "MDoors.MarkDoors";
            string classCleanName = "MDoors.CleanDoors";
            string classParametersName = "MDoors.ParametersDoors";


            string thisAssembyPath = Assembly.GetExecutingAssembly().Location;

            RibbonPanel ribbonPanel = application.CreateRibbonPanel(panelName);
            ribbonPanel.Enabled = true;
            ribbonPanel.Visible = true;
            
            PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup", "Mark \n mirrored doors");
            group1Data.Image = GetEmbeddedImage(imageRibbonSmall);
            group1Data.LargeImage = GetEmbeddedImage(imageRibbonLarge);
            PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;

            PushButtonData buttonMarkData = new PushButtonData("Name1", "Mark", thisAssembyPath, classMarkName);
            PushButton pushMarkButton = group1.AddPushButton(buttonMarkData) as PushButton;
            pushMarkButton.Image = GetEmbeddedImage(imagePaintSmall);
            pushMarkButton.LargeImage=GetEmbeddedImage(imagePaintLarge);
            pushMarkButton.ClassName = classMarkName;
            
            PushButtonData buttonCleanData = new PushButtonData("Name2", "Clean", thisAssembyPath, classCleanName);
            PushButton pushCleanButton = group1.AddPushButton(buttonCleanData) as PushButton;
            pushCleanButton.Image = GetEmbeddedImage(imageCleantSmall);
            pushCleanButton.LargeImage = GetEmbeddedImage(imageCleanLarge);
            pushCleanButton.ClassName = classCleanName;

            PushButtonData buttonParametersData = new PushButtonData("Name3", "Parameters", thisAssembyPath, classParametersName);
            PushButton pushParametersButton = group1.AddPushButton(buttonParametersData) as PushButton;
            pushParametersButton.Image = GetEmbeddedImage(imageParametersSmall);
            pushParametersButton.LargeImage = GetEmbeddedImage(imageParametersLarge);
            pushParametersButton.ClassName = classParametersName;

            return Result.Succeeded;

        }

        static BitmapSource GetEmbeddedImage(string name)
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                Stream s = a.GetManifestResourceStream(name);
                return BitmapFrame.Create(s);
            }
            catch
            {
                return null;
            }
        }       

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;

        }
        // Чтение иконки из сборки
    }
}
