﻿using System;
using System.Windows;
using System.Windows.Forms;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для TestEditor.xaml
    /// </summary>
    public partial class TestEditor : Window
    {
        int userID;
        string userName;
        Test test;

        public TestEditor(int UID, string UName)
        {
            userID = UID;
            userName = UName;
            InitializeComponent();
        }

        private void CreateTest(object sender, RoutedEventArgs e)
        {
            if (test==null)
            {
                test = new Test();
                testGrid.DataContext = test;
            }
            else
            {
                TestEditor editor = new TestEditor(userID, userName);
                editor.Show();
                editor.CreateTest((object)editor, new RoutedEventArgs());
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (test != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON|*.json";
                saveFileDialog.InitialDirectory = Properties.Settings.Default.defaulf_test_file_save_path;
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;
                test.SaveTest(saveFileDialog.FileName);
            }
        }

        private void LoadTest(object sender, RoutedEventArgs e)
        {
            if (test != null)
            {
                test.SaveTest(Properties.Settings.Default.defaulf_test_file_save_path + test.TestName + ".json");
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON|*.json";
            openFileDialog.InitialDirectory = Properties.Settings.Default.defaulf_test_file_save_path;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;
            test = new Test();
            test = test.LoadTest(openFileDialog.FileName);
            testGrid.DataContext = test;
        }
    }
}
