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

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test test = new Test("Пробный тест", 1800);
            test.AddQuestionOneCorrect(new QuestionOneCorrect("Вопрос1", 0, "1", "2", "3", "4"));
            test.AddQuestionManyCorrect(new QuestionManyCorrect("Вопрос2", 
                new AnswerForManyCorrect("0", false), new AnswerForManyCorrect("1", true), 
                new AnswerForManyCorrect("2", false), new AnswerForManyCorrect("3", true)));
            test.AddQuestionInputAnswer(new QuestionInputAnswer("Вопрос3", "123"));
            string path = Properties.Settings.Default.defaulf_test_file_save_path + "123.json";
            Task task = test.SaveTest(path, test);
            Task[] tasks = { task };
            Task.WaitAny(tasks, 1000);
            text2.Text = JsonSerializer.Serialize<Test>(test);
            text1.Text=task.ToString();
        }
    }
}
