﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Testing
{
    enum TypeOfQuestion
    {
        oneCorrect, // один правильный ответ
        manyCorrect, // несколько пправильных ответов
        inputAnswer, // ответ вводится с клавиатуры
        accordance // соответствие
    }

    class Question : INotifyPropertyChanged
    {
        private int id;
        [JsonIgnore]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public TypeOfQuestion type;
        public string questionWording; // Формулировка вопроса

        public TypeOfQuestion Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public string QuestionWording
        {
            get
            {
                return questionWording;
            }
            set
            {
                questionWording = value;
                OnPropertyChanged("QuestionWording");
            }
        }

        // oneCorrect
        public ObservableCollection<AnswerCorrect> answerChoice = new ObservableCollection<AnswerCorrect>();

        public ObservableCollection<AnswerCorrect> AnswerChoice
        {
            get
            {
                return answerChoice;
            }
            set
            {
                answerChoice = value;
                OnPropertyChanged("AnswerChoice");
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
                      AnswerCorrect tempAnswer = new AnswerCorrect();
                      AnswerChoice.Insert(AnswerChoice.Count, tempAnswer);
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
                      AnswerCorrect tempAnswer = obj as AnswerCorrect;
                      if (tempAnswer != null)
                      {
                          AnswerChoice.Remove(tempAnswer);
                      }
                  },
                 (obj) => AnswerChoice.Count > 0));
            }
        }

        // manyCorrect
        public ObservableCollection<AnswerCorrect> answerChoiceMany =
            new ObservableCollection<AnswerCorrect>(); // Словарь вариантов ответа с обозначением их правильности
        public ObservableCollection<AnswerCorrect> AnswerChoiceMany
        {
            get
            {
                return answerChoiceMany;
            }
            set
            {
                answerChoiceMany = value;
                OnPropertyChanged("AnswerChoiceMany");
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
                      AnswerCorrect tempAnswer = new AnswerCorrect();
                      AnswerChoiceMany.Insert(AnswerChoiceMany.Count, tempAnswer);
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
                      AnswerCorrect tempAnswer = obj as AnswerCorrect;
                      if (tempAnswer != null)
                      {
                          AnswerChoiceMany.Remove(tempAnswer);
                      }
                  },
                 (obj) => AnswerChoiceMany.Count > 0));
            }
        }

        // inputAnswer
        public string answer; // Ответ
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        // accordance
        /*public ObservableCollection<string> accordanceColunm1 = new ObservableCollection<string>();
        public ObservableCollection<string> AccordanceColunm1
        {
            get
            {
                return accordanceColunm1;
            }
            set
            {
                accordanceColunm1 = value;
                OnPropertyChanged("AccordanceColumn1");
            }
        }
        public ObservableCollection<string> accordanceColunm2 = new ObservableCollection<string>();
        public ObservableCollection<string> AccordanceColunm2
        {
            get
            {
                return accordanceColunm2;
            }
            set
            {
                accordanceColunm2 = value;
                OnPropertyChanged("AccordanceColumn2");
            }
        }
        public Dictionary<int, int> accordance = new Dictionary<int, int>(); // Словарь соответствия AccordanceColumn1 и AccordanceColumn2
        public Dictionary<int, int> Accordance
        {
            get
            {
                return accordance;
            }
            set
            {
                accordance = value;
                OnPropertyChanged("Accordance");
            }
        }*/

        public Question()
        {

        }
        
        public Question(string question, TypeOfQuestion typeOfQuestion = 0)
        {
            QuestionWording = question;
            Type = typeOfQuestion;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    class QuestionForDeserialize
    {
        [JsonIgnore]
        public int Id { get; set; }

        public TypeOfQuestion Type { get; set; }
        public string QuestionWording { get; set; }

        public List<AnswerCorrect> AnswerChoice { get; set; } = new List<AnswerCorrect>();
        public List<AnswerCorrect> AnswerChoiceMany { get; set; } = new List<AnswerCorrect>();
        public string Answer { get; set; }

        // accordance
        /*public ObservableCollection<string> accordanceColunm1 = new ObservableCollection<string>();
        public ObservableCollection<string> AccordanceColunm1
        {
            get
            {
                return accordanceColunm1;
            }
            set
            {
                accordanceColunm1 = value;
                OnPropertyChanged("AccordanceColumn1");
            }
        }
        public ObservableCollection<string> accordanceColunm2 = new ObservableCollection<string>();
        public ObservableCollection<string> AccordanceColunm2
        {
            get
            {
                return accordanceColunm2;
            }
            set
            {
                accordanceColunm2 = value;
                OnPropertyChanged("AccordanceColumn2");
            }
        }
        public Dictionary<int, int> accordance = new Dictionary<int, int>(); // Словарь соответствия AccordanceColumn1 и AccordanceColumn2
        public Dictionary<int, int> Accordance
        {
            get
            {
                return accordance;
            }
            set
            {
                accordance = value;
                OnPropertyChanged("Accordance");
            }
        }*/

        public QuestionForDeserialize()
        {

        }
    }

    class AnswerForOneCorrect : INotifyPropertyChanged // Класс ответов для вопросов с несколькими правильными ответами
    {
        public string answer;
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public AnswerForOneCorrect()
        {

        }

        public AnswerForOneCorrect(string answer)
        {
            Answer = answer;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    class AnswerForManyCorrect : INotifyPropertyChanged // Класс ответов для вопросов с несколькими правильными ответами
    {
        public string answer;
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }
        public Boolean isCorrect;
        public Boolean IsCorrect
        {
            get
            {
                return isCorrect;
            }
            set
            {
                isCorrect = value;
                OnPropertyChanged("IsCorrect");
            }
        }

        public AnswerForManyCorrect()
        {

        }

        public AnswerForManyCorrect(string answer, Boolean isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    class AnswerCorrect : INotifyPropertyChanged // Класс ответов для вопросов с несколькими правильными ответами
    {
        public string answer;
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }
        public Boolean isCorrect;
        public Boolean IsCorrect
        {
            get
            {
                return isCorrect;
            }
            set
            {
                isCorrect = value;
                OnPropertyChanged("IsCorrect");
            }
        }

        public AnswerCorrect()
        {

        }

        public AnswerCorrect(string answer, Boolean isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
