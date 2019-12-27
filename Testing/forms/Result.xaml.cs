using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Testing
{
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

        public Result(int UID, TestResult test, ObservableCollection<OneResult> res)
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
            testName = test.PassTest.TestName;
            result = res;
            int countCorrect = 0;
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].IsCorrect)
                    countCorrect++;
            }
            double temp = ((double)countCorrect / result.Count) * 100;
            int perc = (int)Math.Round(temp);
            percent.Text = perc.ToString() + "%";
            if (perc < test.PassTest.RatingSatisfactory)
            {
                assessnent.Text = "2";
            }
            else
            {
                if (perc < test.PassTest.RatingGood)
                {
                    assessnent.Text = "3";
                }
                else
                {
                    if (perc < test.PassTest.RatingExcellent)
                    {
                        assessnent.Text = "4";
                    }
                    else
                    {
                        assessnent.Text = "5";
                    }
                }
            }
            testNameBlock.Text = testName;
            userAndGroup.Text = String.Format("{0}: {1}", groupName, userName);
            viewResult.ItemsSource = result;
        }
    }
}
