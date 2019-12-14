using System.Collections.ObjectModel;

namespace Testing
{
    class TestResult
    {
        public int UserID { get; private set; }
        public Test PassTest { get; private set; } = new Test();
        public ObservableCollection<ResponseToQuestion> Answers { get; set; } = new ObservableCollection<ResponseToQuestion>();

        public TestResult(int uID, Test test)
        {
            UserID = uID;
            PassTest = test;
            foreach (Question question in PassTest.Questions)
            {
                Answers.Add(new ResponseToQuestion(question.Type, question.QuestionWording, 
                    question.AnswerChoice, question.AnswerChoiceMany));
            }
        }

        public static ObservableCollection<OneResult> CheckResult(TestResult testResult)
        {
            ObservableCollection<OneResult> result = new ObservableCollection<OneResult>();
            for (int i = 0; i < testResult.PassTest.Questions.Count; i++)
            {
                switch (testResult.PassTest.Questions[i].Type)
                {
                    case TypeOfQuestion.oneCorrect:
                        bool tempOne = true;
                        for (int j = 0; j < testResult.PassTest.Questions[i].AnswerChoice.Count; j++)
                        {
                            if (testResult.PassTest.Questions[i].AnswerChoice[j].IsCorrect != testResult.Answers[i].AnswerOne[j].IsCorrect)
                                tempOne = false;
                        }
                        result.Add(new OneResult(i, tempOne));
                        break;
                    case TypeOfQuestion.manyCorrect:
                        bool tempMany = true;
                        for (int j = 0; j < testResult.PassTest.Questions[i].AnswerChoiceMany.Count; j++)
                        {
                            if (testResult.PassTest.Questions[i].AnswerChoiceMany[j].IsCorrect != testResult.Answers[i].AnswerMany[j].IsCorrect)
                                tempMany = false;
                        }
                        result.Add(new OneResult(i, tempMany));
                        break;
                    case TypeOfQuestion.inputAnswer:
                        result.Add(new OneResult(i, testResult.Answers[i].InputAnswer != null ? 
                            testResult.PassTest.Questions[i].Answer.ToLower().Trim(' ', ',', '.', '\n') ==
                            testResult.Answers[i].InputAnswer.ToLower().Trim(' ', ',', '.', '\n') : false));
                        break;
                    case TypeOfQuestion.accordance:
                        break;
                }
            }
            return result;
        }

        public ObservableCollection<OneResult> CheckResult()
        {
            ObservableCollection<OneResult> result = new ObservableCollection<OneResult>();
            for (int i = 0; i < this.PassTest.Questions.Count; i++)
            {
                switch (this.PassTest.Questions[i].Type)
                {
                    case TypeOfQuestion.oneCorrect:
                        bool tempOne = true;
                        for (int j = 0; j < this.PassTest.Questions[i].AnswerChoice.Count; j++)
                        {
                            if (this.PassTest.Questions[i].AnswerChoice[j].IsCorrect != this.Answers[i].AnswerOne[j].IsCorrect)
                                tempOne = false;
                        }
                        result.Add(new OneResult(i, tempOne));
                        break;
                    case TypeOfQuestion.manyCorrect:
                        bool tempMany = true;
                        for (int j = 0; j < this.PassTest.Questions[i].AnswerChoiceMany.Count; j++)
                        {
                            if (this.PassTest.Questions[i].AnswerChoiceMany[j].IsCorrect != this.Answers[i].AnswerMany[j].IsCorrect)
                                tempMany = false;
                        }
                        result.Add(new OneResult(i, tempMany));
                        break;
                    case TypeOfQuestion.inputAnswer:
                        result.Add(new OneResult(i, this.Answers[i].InputAnswer != null ? 
                            this.PassTest.Questions[i].Answer.ToLower().Trim(' ', ',', '.', '\n') ==
                            this.Answers[i].InputAnswer.ToLower().Trim(' ', ',', '.', '\n') : false));
                        break;
                    case TypeOfQuestion.accordance:
                        break;
                }
            }
            return result;
        }
    }

    class ResponseToQuestion
    {
        public TypeOfQuestion Type { get; private set; }
        public string QuestionWording { get; private set; }
        public ObservableCollection<AnswerCorrect> AnswerOne { get; set; } = new ObservableCollection<AnswerCorrect>();

        public ObservableCollection<AnswerCorrect> AnswerMany { get; set; } = new ObservableCollection<AnswerCorrect>();

        public string InputAnswer { get; set; }

        // public Dictionary<int, int> Accordance { get; set; } = new Dictionary<int, int>();

        public ResponseToQuestion() { }
        public ResponseToQuestion(TypeOfQuestion type, string wording, 
            ObservableCollection<AnswerCorrect> one, ObservableCollection<AnswerCorrect> many)
        {
            Type = type;
            QuestionWording = wording;
            switch (Type)
            {
                case TypeOfQuestion.oneCorrect:
                    foreach (AnswerCorrect answer in one)
                    {
                        AnswerOne.Add(new AnswerCorrect() { Answer = answer.Answer });
                    }
                    break;
                case TypeOfQuestion.manyCorrect:
                    foreach (AnswerCorrect answer in many)
                    {
                        AnswerMany.Add(new AnswerCorrect() { Answer = answer.Answer });
                    }
                    break;
                case TypeOfQuestion.inputAnswer:
                    // InputAnswer = inAnswer;
                    break;
                case TypeOfQuestion.accordance:
                    break;
            }
        }
    }

    public class OneResult
    {
        public int ID { get; set; }
        public bool IsCorrect { get; set; }

        public OneResult(int id, bool isCorrect) { ID = id; IsCorrect = isCorrect; }
    }
}
