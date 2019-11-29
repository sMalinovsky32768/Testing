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
        Test2 test;
        ObservableCollection<QuestionPage> questionPages;

        public TestEditor(int UID, string UName)
        {
            userID = UID;
            userName = UName;
            InitializeComponent();
        }

        private void CreateTest(object sender, RoutedEventArgs e)
        {
            if (test==null)
            {
                test = new Test2();
                questionPages = new ObservableCollection<QuestionPage>();
                questionsListBox.DataContext = test;
                testProperties.DataContext = test;
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
            questionPages.Add(new QuestionPage());
            questionPages[questionPages.Count].DataContext = test.Questions[test.Questions.Count];
        }

        private void questionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
