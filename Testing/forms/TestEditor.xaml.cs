using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для TestEditor.xaml
    /// </summary>
    public partial class TestEditor : Window
    {
        int userID;
        string userName;
        Test test;

        public TestEditor(int UID, string UName)
        {
            userID = UID;
            userName = UName;
            InitializeComponent();
            if (test == null)
            {
                test = new Test();
                testGrid.DataContext = test;
                /*Task task = test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json", test);
                task.Wait(1000);*/
            }
            else
            {
                TestEditor editor = new TestEditor(userID, userName);
                editor.Show();
                editor.CreateTest((object)editor, new RoutedEventArgs());
            }
        }

        private void CreateTest(object sender, RoutedEventArgs e)
        {
            if (test==null)
            {
                test = new Test();
                testGrid.DataContext = test;
                /*Task task = test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json", test);
                task.Wait(1000);*/
            }
            else
            {
                TestEditor editor = new TestEditor(userID, userName);
                editor.Show();
                editor.CreateTest((object)editor, new RoutedEventArgs());
            }
        }

        private void addQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            test.Questions.Add(new Question());
            // questionPages.Add(new QuestionPage());
            // questionPages[questionPages.Count - 1].DataContext = test.Questions[test.Questions.Count - 1];
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (test != null)
            {
                test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json", test);
                // Task task = test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json", test);
                // task.Wait(1000);
            }
        }

        private void LoadTest(object sender, RoutedEventArgs e)
        {
            if (test != null)
            {
                test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json", test);
                // Task task = test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json", test);
                // task.Wait(1000);
            }
            test = test.LoadTest(Properties.Settings.Default.defaulf_test_file_save_path + "12345.json");
        }
    }
}
