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
        private int incId = 0;

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
                      Question tempQuestion = new Question() { Id = incId };
                      incId++;
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

        public async Task SaveTest(string filename, Test test)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    WriteIndented = true,
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
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    WriteIndented = true,
                };
                tempTest = JsonSerializer.Deserialize<Test>(temp, options);
                return tempTest;
            }
            return null;
        }
    }
}
