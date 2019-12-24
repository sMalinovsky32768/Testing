using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Testing.Properties;
using static Testing.Test;

namespace Testing
{
    public partial class TestEditor : Window
    {
        int userID;
        string userName;
        string fileName;
        Test test;

        public TestEditor()
        {
            InitializeComponent();
        }

        public TestEditor(int UID, string UName)
        {
            userID = UID;
            userName = UName;
            InitializeComponent();
        }

        private void CreateTest(object sender, RoutedEventArgs e)
        {
            if (test == null)
            {
                test = new Test();
                testGrid.DataContext = test;
            }
            else
            {
                TestEditor editor = new TestEditor(userID, userName);
                editor.Show();
                editor.CreateTest(editor, new RoutedEventArgs());
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (test != null)
            {
                string path = Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json";
                if (System.Windows.MessageBox.Show("Сохранить тест?", "Спаси и сохрани",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    test.SaveTest(path);
                }
                try
                {
                    Settings.Default.list_of_tests.Remove(fileName);
                }
                catch { }
                Settings.Default.list_of_tests.Add(path, test.TestName);
                Settings.Default.Save();
            }
        }

        private void LoadTestButton(object sender, RoutedEventArgs e)
        {
            if (test != null)
            {
                if (System.Windows.MessageBox.Show("Сохранить тест?", "Спаси и сохрани",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json");
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON|*.json";
            openFileDialog.InitialDirectory = Properties.Settings.Default.defaulf_test_file_save_path;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            test = new Test();
            fileName = openFileDialog.FileName;
            test = LoadTest(openFileDialog.FileName);
            testGrid.DataContext = test;
        }

        private void OpenManager(object sender, RoutedEventArgs e)
        {
            TestManagement testManagement = new TestManagement();
            testManagement.ShowDialog();
            if (testManagement.isCancel)
            {
                return;
            }
            else
            {
                string path = ((TestWithPath)testManagement.testsList.SelectedItem).Path;
                test = new Test();
                test = LoadTest(path);
                testGrid.DataContext = test;
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }
    }
}
