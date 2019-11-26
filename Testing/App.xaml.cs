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
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Settings.Default.number_of_starts == 0)
            {
                Settings.Default.defaulf_test_file_save_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyTests";
            }
        }
    }
}
