using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class QuestionOneCorrect
    {
        private string QuestionWording { get; set; } // Формулировка вопроса
        private List<string> answerChoice = new List<string>();
        private List<string> AnswerChoice
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
        private int Correct { get; set; } // Номер правильного ответа (ключ AnswerChoice)

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
        private string QuestionWording { get; set; } // Формулировка вопроса
        private List<AnswerForManyCorrect> answerChoice = new List<AnswerForManyCorrect>(); // Словарь вариантов ответа с обозначением их правильности
        private List<AnswerForManyCorrect> AnswerChoice
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
        private string QuestionWording { get; set; } // Формулировка вопроса
        private string Answer { get; set; } // Ответ

        public QuestionInputAnswer(string question, string answer)
        {
            QuestionWording = question;
            Answer = answer;
        }
    }

    class QuestionAccordance
    {
        private string QuestionWording { get; set; } // Формулировка вопроса
        private List<string> AccordanceColunm1 { get; set; }
        private List<string> AccordanceColunm2 { get; set; }
        private Dictionary<int, int> Accordance { get; set; } // Словарь соответствия AccordanceColumn1 и AccordanceColumn2

        public QuestionAccordance()
        {

        }
    }

    class AnswerForManyCorrect // Класс ответов для вопросов с несколькими правильными ответами
    {
        private string Answer { get; set; }
        private Boolean IsCorrect { get; set; }

        public AnswerForManyCorrect(string answer, Boolean isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }
    }

    class QuestionWithType // Вопрос с типом
    {
        private TypeOfQuestion Type { get; set; }
        private object Question { get; set; }

        public QuestionWithType(TypeOfQuestion type, object question)
        {
            Type = type;
            Question = question;
        }
    }
}
