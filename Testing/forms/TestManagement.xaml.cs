using System;
using System.Windows;
using Testing.Properties;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для TestManagement.xaml
    /// </summary>
    public partial class TestManagement : Window
    {
        TestManagementList testManagementList;
        public bool isCancel = false;

        public TestManagement()
        {
            InitializeComponent();
            testManagementList = new TestManagementList(Settings.Default.list_of_tests);
            this.DataContext = testManagementList;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            testManagementList.UpdateSettings();
        }

        private void OpenTest(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            isCancel = true;
            this.Close();
        }
    }
}
