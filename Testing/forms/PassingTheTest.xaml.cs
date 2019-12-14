using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Testing.Properties;
using static Testing.Test;
using static Testing.TestResult;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для PassingTheTest.xaml
    /// </summary>
    public partial class PassingTheTest : Window
    {
        readonly int userID;
        TestResult testResult;
        ObservableCollection<OneResult> res = new ObservableCollection<OneResult>();
        Task task;

        public PassingTheTest(int UID, string testPath)
        {
            userID = UID;
            InitializeComponent();
            testResult = new TestResult(userID, LoadTest(testPath));
            this.DataContext = testResult;
            this.Title = testResult.PassTest.TestName;
        }

        private void ReplyClick(object sender, RoutedEventArgs e)
        {
            tabsQuestion.SelectedItem = tabsQuestion.SelectedIndex < tabsQuestion.Items.Count ? tabsQuestion.SelectedIndex + 1 : 0;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            res = CheckResult(testResult);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            File.WriteAllText(Settings.Default.default_result_path + userID + "_" + testResult.PassTest.TestName + ".json", 
                JsonSerializer.Serialize<ObservableCollection<OneResult>>(res, options));
            Result result = new Result(userID, testResult.PassTest.TestName, res);
            result.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var min = durationProgress.Minimum;
            var max = durationProgress.Maximum;
            Action action = () => { durationProgress.Value++; };
            task = new Task(() =>
            {
                try
                {
                    for (var i = min; i < max; i++)
                    {
                        durationProgress.Dispatcher.Invoke(action);
                        Thread.Sleep(1000);
                    }
                    Complete_Click(new object(), new RoutedEventArgs());
                }
                catch { }
            });
            task.Start();
        }
    }
}
