﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string TestName { get; set; } // название теста
        public int Duration { get; set; } // Длительность теста в секундах
        public ObservableCollection<QuestionWithType> questions = new ObservableCollection<QuestionWithType>();
        public ObservableCollection<QuestionWithType> Questions
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
        public Test()
        {
            TestName = "";
            Duration = 3600;
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
                await JsonSerializer.SerializeAsync(fileStream, test, test.GetType());
            }
        }

        public Test LoadTest(string filename)
        {
            if (File.Exists(filename))
            {
                string temp = "";
                temp = File.ReadAllText(filename);
                Test tempTest = new Test();
                tempTest = JsonSerializer.Deserialize<Test>(temp);
                QuestionWithType[] tempQuestions = new QuestionWithType[tempTest.Questions.Count];
                tempTest.Questions.CopyTo(tempQuestions, 0);
                tempTest.Questions.Clear();
                foreach(QuestionWithType questionWithType in tempQuestions)
                {
                    object obj = new object();
                    switch (questionWithType.Type)
                    {
                        case TypeOfQuestion.oneCorrect:
                            obj = JsonSerializer.Deserialize<QuestionOneCorrect>(questionWithType.Question.ToString());
                            break;
                        case TypeOfQuestion.manyCorrect:
                            obj = JsonSerializer.Deserialize<QuestionManyCorrect>(questionWithType.Question.ToString());
                            break;
                        case TypeOfQuestion.inputAnswer:
                            obj = JsonSerializer.Deserialize<QuestionInputAnswer>(questionWithType.Question.ToString());
                            break;
                        case TypeOfQuestion.accordance:
                            obj = JsonSerializer.Deserialize<QuestionAccordance>(questionWithType.Question.ToString());
                            break;
                    }
                    tempTest.Questions.Add(new QuestionWithType(questionWithType.Type, obj));
                }
                return tempTest;
            }
            return null;
        }
    }
}
