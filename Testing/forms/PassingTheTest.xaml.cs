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
using static Testing.Test;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для PassingTheTest.xaml
    /// </summary>
    public partial class PassingTheTest : Window
    {
        int userID;
        string userName;
        string userGroup;
        Test test;

        public PassingTheTest(int UID, string UName, string UGroup, string testPath)
        {
            userID = UID;
            userName = UName;
            userGroup = UGroup;
            InitializeComponent();
            test = new Test();
            test = LoadTest(testPath);
            this.DataContext = test;
        }
    }
}
