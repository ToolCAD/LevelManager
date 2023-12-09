using LevelManager.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LevelManager.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            List<LevelModel> levelDataList = new List<LevelModel>
            {
                new LevelModel("EG",new Elevation(125.548787),BasePointType.Project),
                new LevelModel("EG1",new Elevation(165.548787),BasePointType.Project),
                new LevelModel("EK",new Elevation(15.548787),BasePointType.Shared),
                new LevelModel("HB",new Elevation(145.548787),BasePointType.Project),
            };

            // Create an instance of your MainWindow from the class library
            //var mainForm = new UI.MainForm(levelDataList);

                       

            //// Show the MainWindow
            //mainForm.Show();

            base.OnStartup(e);
        }
    }
}
