using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LevelManager.Api;
using LevelManager.Api.EventHandlers;
using LevelManager.Domain;
using LevelManager.UI;
using LevelManagerApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelManager
{
    [Transaction(TransactionMode.Manual)]
    internal class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get the Revit application and document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            var levelController = new LevelApiController(doc);
            var levelDataList = levelController.GetAll();

            var createLevelEventHandler = new CreateLevelEventHandler();
            var deleteLevelEventHandler = new DeleteLevelEventHandler();
            var createLevelEvent = ExternalEvent.Create(createLevelEventHandler);
            var deleteLevelEvent = ExternalEvent.Create(deleteLevelEventHandler);


            // Create an instance of MainWindow from the class library
            MainForm mainForm = new MainForm(levelDataList, createLevelEventHandler, deleteLevelEventHandler, createLevelEvent, deleteLevelEvent);
            mainForm.Show();

            return Result.Succeeded;
        }
    }
}
