using System;
using System.Windows;
using Testing.Properties;

namespace Testing
{
    public partial class TestManagement : Window
    {
        TestManagementList testManagementList;
        public bool isCancel = false;

        public TestManagement()
        {
            InitializeComponent();
            testManagementList = new TestManagementList(Settings.Default.list_of_tests);
            DataContext = testManagementList;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            testManagementList.UpdateSettings();
        }

        private void OpenTest(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            isCancel = true;
            Close();
        }
    }
}
