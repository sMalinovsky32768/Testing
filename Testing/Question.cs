using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class QuestionOneCorrect
    {
        public string QuestionWording { get; set; } // Формулировка вопроса
        public List<string> answerChoice = new List<string>();
        public List<string> AnswerChoice
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
        public List<AnswerForManyCorrect> answerChoice = 
            new List<AnswerForManyCorrect>(); // Словарь вариантов ответа с обозначением их правильности
        public List<AnswerForManyCorrect> AnswerChoice
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
        public List<string> AccordanceColunm1 { get; set; }
        public List<string> AccordanceColunm2 { get; set; }
        public Dictionary<int, int> Accordance { get; set; } // Словарь соответствия AccordanceColumn1 и AccordanceColumn2

        public QuestionAccordance()
        {

        }
    }

    class AnswerForManyCorrect // Класс ответов для вопросов с несколькими правильными ответами
    {
        public string Answer { get; set; }
        public Boolean IsCorrect { get; set; }

        public AnswerForManyCorrect()
        {

        }

        public AnswerForManyCorrect(string answer, Boolean isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
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
    }
}
