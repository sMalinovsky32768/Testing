using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Testing
{
    enum TypeOfQuestion
    {
        oneCorrect, // один правильный ответ
        manyCorrect, // несколько пправильных ответов
        inputAnswer, // ответ вводится с клавиатуры
        accordance // соответствие
    }

    class Test
    {
        private string TestName { get; set; } // название теста
        private int Duration { get; set; } // Длительность теста в секундах
        private List<QuestionWithType> questions = new List<QuestionWithType>();
        private List<QuestionWithType> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
            }
        }

        public Test(string name, int duration = 3600)
        {
            TestName = name;
            Duration = duration;
        }

        public void AddQuestionOneCorrect(QuestionOneCorrect question)
        {
            QuestionWithType q = new QuestionWithType(TypeOfQuestion.oneCorrect, (object)question);
            Questions.Add(q);
        }

        public void AddQuestionManyCorrect(QuestionManyCorrect question)
        {
            Questions.Add(new QuestionWithType(TypeOfQuestion.manyCorrect, question));
        }

        public void AddQuestionInputAnswer(QuestionInputAnswer question)
        {
            Questions.Add(new QuestionWithType(TypeOfQuestion.inputAnswer, question));
        }

        public void AddQuestionAccordance(QuestionAccordance question)
        {
            Questions.Add(new QuestionWithType(TypeOfQuestion.accordance, question));
        }

        public async Task SaveTest(string filename, Test test)
        {
            /*System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";*/
            using (FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<Test>(fileStream, test);
            }
        }

        /*public async Task LoadTest(string filename)
        {
            if (File.Exists(filename))
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    Test newTest = await JsonSerializer.DeserializeAsync<Test>(fileStream);
                }
            }
        }*/
    }
}
