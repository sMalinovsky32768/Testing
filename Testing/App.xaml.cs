using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Testing.Properties;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Default.number_of_starts++;
            Settings.Default.Save();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Settings.Default.number_of_starts == 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyTests\\";
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                Settings.Default.defaulf_test_file_save_path = path;
            }
        }
    }
}
