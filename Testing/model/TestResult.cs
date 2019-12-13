using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    /*class TestResult
    {
        public int UserID { get; set; }
        public Test PassTest { get; set; } = new Test();
        public ObservableCollection<ResponseToQuestion> Responses { get; set; } = new ObservableCollection<ResponseToQuestion>();

        public TestResult(int uID, Test test)
        {
            UserID = uID;
            PassTest = test;
        }
    }

    class ResponseToQuestion
    {
        public int Answer { get; set; } // OneCorrect
        public ObservableCollection<bool> AnswerMany { get; set; } = new ObservableCollection<bool>(); // ManyCorrect
        public string InputAnswer { get; set; } // Input
        // public Dictionary<int, int> Accordance { get; set; } = new Dictionary<int, int>(); // Accordance
    }*/

    class TestResult
    {
        public int UserID { get; private set; }
        public Test PassTest { get; private set; } = new Test();
        public ObservableCollection<ResponseToQuestion> Responses { get; set; } = new ObservableCollection<ResponseToQuestion>();

        public TestResult(int uID, Test test)
        {
            UserID = uID;
            PassTest = test;
            foreach (Question question in PassTest.Questions)
            {
                Responses.Add(new ResponseToQuestion(question.Type, question.QuestionWording, 
                    question.AnswerChoice, question.AnswerChoiceMany, question.Answer));
            }
        }
    }

    class ResponseToQuestion
    {
        public TypeOfQuestion Type { get; private set; }
        public string QuestionWording { get; private set; }
        public ObservableCollection<AnswerCorrect> Answer { get; set; } = new ObservableCollection<AnswerCorrect>();

        public ObservableCollection<AnswerCorrect> AnswerMany { get; set; } = new ObservableCollection<AnswerCorrect>();

        public string InputAnswer { get; set; }

        // public Dictionary<int, int> Accordance { get; set; } = new Dictionary<int, int>();

        public ResponseToQuestion() { }
        public ResponseToQuestion(TypeOfQuestion type, string wording, 
            ObservableCollection<AnswerCorrect> one, ObservableCollection<AnswerCorrect> many, string inAnswer)
        {
            Type = type;
            QuestionWording = wording;
            switch (Type)
            {
                case TypeOfQuestion.oneCorrect:
                    foreach (AnswerCorrect answer in one)
                    {
                        Answer.Add(new AnswerCorrect() { Answer = answer.Answer });
                    }
                    break;
                case TypeOfQuestion.manyCorrect:
                    foreach (AnswerCorrect answer in many)
                    {
                        AnswerMany.Add(new AnswerCorrect() { Answer = answer.Answer });
                    }
                    break;
                case TypeOfQuestion.inputAnswer:
                    InputAnswer = inAnswer;
                    break;
                case TypeOfQuestion.accordance:
                    break;
            }
        }
    }

    /*class CheckResult
    {
        public static ObservableCollection<bool> Check(TestResult testResult)
        {
            ObservableCollection<bool> result = new ObservableCollection<bool>();
            for (int i = 0; i < testResult.PassTest.Questions.Count; i++)
            {
                switch (testResult.PassTest.Questions[i].Type)
                {
                    case TypeOfQuestion.oneCorrect:
                        result.Add(testResult.PassTest.Questions[i].Correct == testResult.Responses[i].Answer);
                        break;
                    case TypeOfQuestion.manyCorrect:
                        ObservableCollection<bool> vs = new ObservableCollection<bool>();
                        foreach(AnswerForManyCorrect answer in testResult.PassTest.Questions[i].AnswerChoiceMany)
                        {
                            vs.Add(answer.IsCorrect);
                        }
                        result.Add(vs == testResult.Responses[i].AnswerMany);
                        break;
                    case TypeOfQuestion.inputAnswer:
                        result.Add(testResult.PassTest.Questions[i].Answer == testResult.Responses[i].InputAnswer);
                        break;
                    case TypeOfQuestion.accordance:
                        break;
                }
            }
            return result;
        }
    }*/

    class CheckResult
    {
        public static ObservableCollection<bool> Check(TestResult testResult)
        {
            ObservableCollection<bool> result = new ObservableCollection<bool>();
            for (int i = 0; i < testResult.PassTest.Questions.Count; i++)
            {
                switch (testResult.PassTest.Questions[i].Type)
                {
                    case TypeOfQuestion.oneCorrect:
                        result.Add(testResult.PassTest.Questions[i].AnswerChoice == testResult.Responses[i].Answer);
                        break;
                    case TypeOfQuestion.manyCorrect:
                        result.Add(testResult.PassTest.Questions[i].AnswerChoiceMany == testResult.Responses[i].AnswerMany);
                        break;
                    case TypeOfQuestion.inputAnswer:
                        result.Add(testResult.PassTest.Questions[i].Answer == testResult.Responses[i].InputAnswer);
                        break;
                    case TypeOfQuestion.accordance:
                        break;
                }
            }
            return result;
        }
    }
}
