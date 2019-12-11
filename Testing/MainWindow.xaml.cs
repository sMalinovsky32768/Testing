﻿using System;
using System.Windows;
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
        SqlConnection connection;
        SqlCommand command;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString;
        }
        private void NullVoid() { }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string user = loginTextBox.Text;
            string group = groupTextBox.Text;
            string pass = passTextBox.Password;
            string sql = "select [User].id as id, [User].[name] as [name], [group].[name] as [group] from [User] inner join [group] on [group].[id]=[User].[group] Where [User].[name]=N'" + user + "' and [User].[pass]=N'" + pass + "' and [group].[name]=N'" + group + "'";
            usersTable = new DataTable();
            command = new SqlCommand(sql, connection);
            adapter = new SqlDataAdapter(command);
            DataRow userRow;
            int userID;
            string userName;
            string userGroup;
            try
            {
                adapter.Fill(usersTable);
                userRow = usersTable.Rows[0];
                userID = (int)userRow["id"];
                userName = userRow["name"].ToString();
                userGroup = userRow["group"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nПользователь не существует", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
                e.Handled = false;
                return;
            }
            if (group=="admin")
            {
                TestEditor testEditor = new TestEditor(userID, userName);
                testEditor.Show();
                this.Close();
            }
            else
            {
                TestSelection testSelection = new TestSelection(userID, userName, userGroup);
                testSelection.Show();
                this.Close();
            }
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            connection.Close();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            // command = new SqlCommand(Testing.Properties.Settings.Default.create_database, connection);
            // Task task = new Task(new Action(NullVoid));
            // task.Wait(10000);
        }
    }
}
