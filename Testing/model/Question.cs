using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Testing
{
    public enum TypeOfQuestion
    {
        oneCorrect, // один правильный ответ
        manyCorrect, // несколько пправильных ответов
        inputAnswer, // ответ вводится с клавиатуры
        accordance // соответствие
    }

    public class Question : INotifyPropertyChanged
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

        public Question() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    public class QuestionForDeserialize
    {
        [JsonIgnore]
        public int Id { get; set; }

        public TypeOfQuestion Type { get; set; }
        public string QuestionWording { get; set; }

        public List<AnswerCorrect> AnswerChoice { get; set; } = new List<AnswerCorrect>();
        public List<AnswerCorrect> AnswerChoiceMany { get; set; } = new List<AnswerCorrect>();
        public string Answer { get; set; }
        public QuestionForDeserialize() { }
    }

    public class AnswerCorrect : INotifyPropertyChanged // Класс ответов для вопросов с несколькими правильными ответами
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

        public AnswerCorrect() { }

        public AnswerCorrect(string answer, Boolean isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
