using System;
using System.Collections.Generic;
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
using Testing.Properties;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для TestSelection.xaml
    /// </summary>
    public partial class TestSelection : Window
    {
        TestManagementList testManagementList;

        int userID;
        string userName;
        string userGroup;

        public TestSelection(int UID, string UName, string UGroup)
        {
            userID = UID;
            userName = UName;
            userGroup = UGroup;
            InitializeComponent();
            testManagementList = new TestManagementList(Settings.Default.list_of_tests);
            this.DataContext = testManagementList;
        }

        private void OpenTest(object sender, RoutedEventArgs e)
        {
            PassingTheTest passingTheTest = new PassingTheTest(userID, ((TestWithPath)this.testsList.SelectedItem).Path);
            passingTheTest.Show();
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
