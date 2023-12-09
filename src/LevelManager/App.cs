using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LevelManager
{
    public class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {

            var panel = application.CreateRibbonPanel("Levels");
            var loadCommandType = typeof(Command);

            var levelsButtonData = new PushButtonData(loadCommandType.FullName,
                "Manager",
                loadCommandType.Assembly.Location,
                loadCommandType.FullName)
            {
                // Set the LargeImage of levelsButton using an embedded resource
                Image = new BitmapImage(new Uri("pack://application:,,,/LevelManager;component/Resources/logo_16.png", UriKind.RelativeOrAbsolute)),
                LargeImage = new BitmapImage(new Uri("pack://application:,,,/LevelManager;component/Resources/logo_32.png", UriKind.RelativeOrAbsolute))
            };

            panel.AddItem(levelsButtonData);

            return Result.Succeeded;
        }
    }
}
