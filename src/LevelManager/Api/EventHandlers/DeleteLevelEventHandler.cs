using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LevelManager.Api;
using LevelManager.Domain;

namespace LevelManagerApp.Windows
{
    public class DeleteLevelEventHandler : IExternalEventHandler
    {
        
        public LevelModel Input { get; set; }
        private LevelApiController LevelApiController { get; set; }

        public bool Success { get; set; } = false;
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            LevelApiController = new LevelApiController(doc);

            LevelApiController.Delete(Input.Name);
            Success = true;
        }

        public string GetName()
        {
            return "DeleteLevelHandler";
        }
    }
}
