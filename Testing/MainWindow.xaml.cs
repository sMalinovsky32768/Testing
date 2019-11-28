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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable usersTable;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString;
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string user = loginTextBox.Text;
            string group = groupTextBox.Text;
            string pass = passTextBox.Text;
            string sql = "select [User].id as id, [User].[name] as [name], [group].[name] as [group] from [User] inner join [group] on [group].[id]=[User].[group] Where [User].[name]=N'" + user + "' and [User].[pass]=N'" + pass + "' and [group].[name]=N'" + group + "'";
            usersTable = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            adapter = new SqlDataAdapter(command);

            adapter.Fill(usersTable);
            DataRow userRow = usersTable.Rows[0];
            int userID = (int)userRow["id"];
            string userName = userRow["name"].ToString();
            string userGroup = userRow["group"].ToString();
            connection.Close();
            if (group=="admin")
            {
                TestEditor testEditor = new TestEditor(userID, userName);
                testEditor.Show();
                this.Close();
            }
        }
    }
}
