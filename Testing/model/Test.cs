﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Testing
{
    class Test : INotifyPropertyChanged
    {
        public string testName; // название теста
        public int duration; // Длительность теста в секундах
        private Question selectedQuestion;
        public ObservableCollection<Question> questions = new ObservableCollection<Question>();

        public string TestName
        {
            get
            {
                return testName;
            }
            set
            {
                testName = value;
                OnPropertyChanged("TestName");
            }
        }

        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        [JsonIgnore]
        public Question SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }
            set
            {
                selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
            }
        }

        public ObservableCollection<Question> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
                OnPropertyChanged("Questions");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private TestCommand addCommand;
        [JsonIgnore]
        public TestCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new TestCommand(obj =>
                  {
                      Question tempQuestion = new Question();
                      Questions.Insert(Questions.Count, tempQuestion);
                      SelectedQuestion = Questions[Questions.Count - 1];
                      // SelectedQuestion = tempQuestion;
                  }));
            }
        }

        private TestCommand removeCommand;
        [JsonIgnore]
        public TestCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new TestCommand(obj =>
                  {
                      Question tempQuestion = obj as Question;
                      if (tempQuestion != null)
                      {
                          Questions.Remove(tempQuestion);
                      }
                  },
                 (obj) => Questions.Count > 0));
            }
        }

        private TestCommand addCommandForOneCorrect;
        [JsonIgnore]
        public TestCommand AddCommandForOneCorrect
        {
            get
            {
                return addCommandForOneCorrect ??
                  (addCommandForOneCorrect = new TestCommand(obj =>
                  {
                      AnswerForOneCorrect tempAnswer = new AnswerForOneCorrect();
                      SelectedQuestion.AnswerChoice.Insert(SelectedQuestion.AnswerChoice.Count, tempAnswer);
                      // SelectedQuestion = tempQuestion;
                  }));
            }
        }

        private TestCommand removeCommandForOneCorrect;
        [JsonIgnore]
        public TestCommand RemoveCommandForOneCorrect
        {
            get
            {
                return removeCommandForOneCorrect ??
                  (removeCommandForOneCorrect = new TestCommand(obj =>
                  {
                      AnswerForOneCorrect tempAnswer = obj as AnswerForOneCorrect;
                      if (tempAnswer != null)
                      {
                          SelectedQuestion.AnswerChoice.Remove(tempAnswer);
                      }
                  },
                 (obj) => SelectedQuestion.AnswerChoice.Count > 0));
            }
        }

        private TestCommand addCommandForManyCorrect;
        [JsonIgnore]
        public TestCommand AddCommandForManyCorrect
        {
            get
            {
                return addCommandForManyCorrect ??
                  (addCommandForManyCorrect = new TestCommand(obj =>
                  {
                      AnswerForManyCorrect tempAnswer = new AnswerForManyCorrect();
                      SelectedQuestion.AnswerChoiceMany.Insert(SelectedQuestion.AnswerChoice.Count, tempAnswer);
                      // SelectedQuestion = tempQuestion;
                  }));
            }
        }

        private TestCommand removeCommandForManyCorrect;
        [JsonIgnore]
        public TestCommand RemoveCommandForManyCorrect
        {
            get
            {
                return removeCommandForManyCorrect ??
                  (removeCommandForManyCorrect = new TestCommand(obj =>
                  {
                      AnswerForManyCorrect tempAnswer = obj as AnswerForManyCorrect;
                      if (tempAnswer != null)
                      {
                          SelectedQuestion.AnswerChoiceMany.Remove(tempAnswer);
                      }
                  },
                 (obj) => SelectedQuestion.AnswerChoiceMany.Count > 0));
            }
        }

        /*public void AddQuestionOneCorrect(string question)
        {
            Questions.Add(new Question(question, TypeOfQuestion.oneCorrect));
        }

        public void AddQuestionManyCorrect(string question)
        {
            Questions.Add(new Question(question, TypeOfQuestion.manyCorrect));
        }

        public void AddQuestionInputAnswer(string question)
        {
            Questions.Add(new Question(question, TypeOfQuestion.inputAnswer));
        }

        public void AddQuestionAccordance(string question)
        {
            Questions.Add(new Question(question, TypeOfQuestion.accordance));
        }*/

        public async Task SaveTest(string filename, Test test)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                };
                await JsonSerializer.SerializeAsync(fileStream, test, test.GetType(), options);
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
                return tempTest;
            }
            return null;
        }
    }
}