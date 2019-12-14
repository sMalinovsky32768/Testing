using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для TestSelection.xaml
    /// </summary>
    public partial class Result : Window
    {
        int userID;
        string userName;
        string groupName;
        string testName;
        string connectionString;
        ObservableCollection<OneResult> result = new ObservableCollection<OneResult>();
        DataTable userTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;

        public Result(int UID, string test, ObservableCollection<OneResult> res)
        {
            InitializeComponent();
            userID = UID;
            connectionString = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            string sql = "select [User].id as id, [User].[name] as name, [group].[name] as groupName " +
                "from [User] inner join [group] on [group].[id]=[User].[group] Where [User].id=" + userID;
            userTable = new DataTable();
            command = new SqlCommand(sql, connection);
            adapter = new SqlDataAdapter(command);
            DataRow userRow;
            try
            {
                adapter.Fill(userTable);
                userRow = userTable.Rows[0];
                userID = (int)userRow["id"];
                userName = userRow["name"].ToString();
                groupName = userRow["groupName"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            testName = test;
            result = res;
            testNameBlock.Text = testName;
            userAndGroup.Text = String.Format("{0}: {1}", groupName, userName);
            viewResult.ItemsSource = result;
        }
    }
}
