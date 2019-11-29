using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        public TypeOfQuestion type;
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
        public string questionWording; // Формулировка вопроса
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
        public ObservableCollection<string> answerChoice = new ObservableCollection<string>();
        public ObservableCollection<string> AnswerChoice
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
        public int correct; // Номер правильного ответа (ключ AnswerChoice)
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
        public ObservableCollection<string> accordanceColunm1 = new ObservableCollection<string>();
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
        }

        public Question()
        {

        }
        
        public Question(string question, TypeOfQuestion typeOfQuestion)
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

    /*class QuestionOneCorrect
    {
        public string QuestionWording { get; set; } // Формулировка вопроса
        public ObservableCollection<string> answerChoice = new ObservableCollection<string>();
        public ObservableCollection<string> AnswerChoice
        {
            get
            {
                return answerChoice;
            }
            set
            {
                answerChoice = value;
            }
        }
        public int Correct { get; set; } // Номер правильного ответа (ключ AnswerChoice)

        public QuestionOneCorrect()
        {

        }

        public QuestionOneCorrect(string question, int correct, params string[] choices)
        {
            QuestionWording = question;
            Correct = correct;
            foreach(string choice in choices)
            {
                AnswerChoice.Add(choice);
            }
        }
    }

    class QuestionManyCorrect
    {
        public string QuestionWording { get; set; } // Формулировка вопроса
        public ObservableCollection<AnswerForManyCorrect> answerChoice = 
            new ObservableCollection<AnswerForManyCorrect>(); // Словарь вариантов ответа с обозначением их правильности
        public ObservableCollection<AnswerForManyCorrect> AnswerChoice
        {
            get
            {
                return answerChoice;
            }
            set
            {
                answerChoice = value;
            }
        }

        public QuestionManyCorrect()
        {

        }

        public QuestionManyCorrect(string question, params AnswerForManyCorrect[] choices)
        {
            QuestionWording = question;
            foreach (AnswerForManyCorrect choice in choices)
            {
                AnswerChoice.Add(choice);
            }
        }
    }

    class QuestionInputAnswer
    {
        public string QuestionWording { get; set; } // Формулировка вопроса
        public string Answer { get; set; } // Ответ

        public QuestionInputAnswer()
        {

        }

        public QuestionInputAnswer(string question, string answer)
        {
            QuestionWording = question;
            Answer = answer;
        }
    }

    class QuestionAccordance
    {
        public string QuestionWording { get; set; } // Формулировка вопроса
        public ObservableCollection<string> AccordanceColunm1 { get; set; }
        public ObservableCollection<string> AccordanceColunm2 { get; set; }
        public Dictionary<int, int> Accordance { get; set; } // Словарь соответствия AccordanceColumn1 и AccordanceColumn2

        public QuestionAccordance()
        {

        }
    }

    class QuestionWithType // Вопрос с типом
    {
        public TypeOfQuestion Type { get; set; }
        public object Question { get; set; }

        public QuestionWithType()
        {

        }

        public QuestionWithType(TypeOfQuestion type, object question)
        {
            Type = type;
            Question = question;
        }
    }*/
}
