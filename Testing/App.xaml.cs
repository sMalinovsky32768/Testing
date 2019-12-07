using System;
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
            if (Settings.Default.list_of_tests == null) 
                Settings.Default.list_of_tests = new System.Collections.Specialized.NameValueCollection();
            if (Settings.Default.number_of_starts == 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyTests\\";
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                Settings.Default.defaulf_test_file_save_path = path;
                Settings.Default.Save();
            }
            Settings.Default.list_of_tests.Add("Test1", "C:\\Users\\sMali\\Documents\\MyTests\\Тест1.json");
        }
    }
}
