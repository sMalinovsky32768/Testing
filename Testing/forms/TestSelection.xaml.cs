using System.Windows;
using Testing.Properties;

namespace Testing
{
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
            DataContext = testManagementList;
        }

        private void OpenTest(object sender, RoutedEventArgs e)
        {
            if (testsList.SelectedItem == null)
            {
                e.Handled = false;
                return;
            }
            PassingTheTest passingTheTest = new PassingTheTest(userID, ((TestWithPath)testsList.SelectedItem).Path);
            passingTheTest.Show();
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
