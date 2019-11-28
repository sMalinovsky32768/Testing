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

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для TestSelection.xaml
    /// </summary>
    public partial class TestSelection : Window
    {
        int userID;
        string userName;
        string userGroup;

        public TestSelection(int UID, string UName, string UGroup)
        {
            userID = UID;
            userName = UName;
            userGroup = UGroup;
            InitializeComponent();
        }
    }
}
