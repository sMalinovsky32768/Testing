using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ObservableCollection<AnswerForOneCorrect> answerChoice = new ObservableCollection<AnswerForOneCorrect>();
        public int correct; // Номер правильного ответа (ключ AnswerChoice)

        public ObservableCollection<AnswerForOneCorrect> AnswerChoice
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
        public int Correct
        {
            get
            {
                return correct;
            }
            set
            {
                correct = value;
                OnPropertyChanged("Correct");
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
                      AnswerForOneCorrect tempAnswer = obj as AnswerForOneCorrect;
                      if (tempAnswer != null)
                      {
                          AnswerChoice.Remove(tempAnswer);
                      }
                  },
                 (obj) => AnswerChoice.Count > 0));
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
                      AnswerForManyCorrect tempAnswer = obj as AnswerForManyCorrect;
                      if (tempAnswer != null)
                      {
                          AnswerChoiceMany.Remove(tempAnswer);
                      }
                  },
                 (obj) => AnswerChoiceMany.Count > 0));
            }
        }

        // manyCorrect
        public ObservableCollection<AnswerForManyCorrect> answerChoiceMany =
            new ObservableCollection<AnswerForManyCorrect>(); // Словарь вариантов ответа с обозначением их правильности
        public ObservableCollection<AnswerForManyCorrect> AnswerChoiceMany
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
}
