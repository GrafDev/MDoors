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
        internal string verRevit; //Версия Revit
        internal static string fullPath; // Путь к расположению семейства
        internal static string familyName = "ADSK_Error_Place";// Имя семейства
        internal static bool dialogBoxShow = true; //Показзывать ли диалоговые окна плагина
        internal static bool flagsPlace = true; // Устанавливать ли марки отзеркаленных дверей

        public Result OnStartup(UIControlledApplication application)
        {
            UIControlledApplication app = application;
            verRevit = app.ControlledApplication.VersionNumber;

            fullPath = $@"C:\ProgramData\Autodesk\Revit\Addins\{verRevit}\MDoors\";// Установка пути к семейству
            string panelName = "Mirrored Doors";//Имя панели плагина
            string imagePaintSmall = "MDoors.icon_PaintError16.png";/// Иконки комманд
            string imagePaintLarge = "MDoors.icon_PaintError32.png";
            string imageCleantSmall = "MDoors.icon_CleanError16.png";
            string imageCleanLarge = "MDoors.icon_CleanError32.png";
            string imageRibbonSmall = "MDoors.iconMain_Door16.png";
            string imageRibbonLarge =  "MDoors.iconMain_Door32.png";
            string imageParSmall = "MDoors.iconParameters16.pngg";
            string imageParLarge = "MDoors.iconParameters32.png";///

            string classMarkName = "MDoors.MarkDoors";// Имя Класса для маркировки
            string classCleanName = "MDoors.CleanDoors";//Имя класса для очистки
            string classParName = "MDoors.ParametersDoors";// Имя класса для параметров


            string thisAssembyPath = Assembly.GetExecutingAssembly().Location;
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(panelName);//Установка панели
            ribbonPanel.Enabled = true;
            ribbonPanel.Visible = true;
            
            PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup", "Mark \n mirrored doors");// Установка группы комманд
            group1Data.Image = GetEmbeddedImage(imageRibbonSmall);
            group1Data.LargeImage = GetEmbeddedImage(imageRibbonLarge);
            PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;

            PushButtonData buttonMarkData = new PushButtonData("Name1", "Mark", thisAssembyPath, classMarkName);//Команда маркировки
            PushButton pushMarkButton = group1.AddPushButton(buttonMarkData) as PushButton;
            pushMarkButton.Image = GetEmbeddedImage(imagePaintSmall);
            pushMarkButton.LargeImage=GetEmbeddedImage(imagePaintLarge);
            pushMarkButton.ClassName = classMarkName;
            
            PushButtonData buttonCleanData = new PushButtonData("Name2", "Clean", thisAssembyPath, classCleanName);//Команда очистки
            PushButton pushCleanButton = group1.AddPushButton(buttonCleanData) as PushButton;
            pushCleanButton.Image = GetEmbeddedImage(imageCleantSmall);
            pushCleanButton.LargeImage = GetEmbeddedImage(imageCleanLarge);
            pushCleanButton.ClassName = classCleanName;

            group1.AddSeparator();

            PushButtonData buttonParData = new PushButtonData("Name3", "Parameters", thisAssembyPath, classParName);//Команда параметров
            PushButton pushParButton = group1.AddPushButton(buttonParData) as PushButton;
            pushParButton.Image = GetEmbeddedImage(imageParSmall);
            pushParButton.LargeImage = GetEmbeddedImage(imageParLarge);
            pushParButton.ClassName = classParName;

            return Result.Succeeded;

        }

        static BitmapSource GetEmbeddedImage(string name)// Получение иконок из сборки
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
