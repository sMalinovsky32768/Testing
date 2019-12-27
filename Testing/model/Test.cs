using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Testing
{
    public class Test : INotifyPropertyChanged
    {
        private string testName; // название теста
        private int duration; // Длительность теста в секундах
        private Question selectedQuestion;
        private ObservableCollection<Question> questions = new ObservableCollection<Question>(new List<Question>());
        private int ratingExcellent;
        private int ratingGood;
        private int ratingSatisfactory;

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

        public int RatingExcellent
        {
            get
            {
                return ratingExcellent;
            }
            set
            {
                ratingExcellent = value;
                OnPropertyChanged("RatingExcellent");
            }
        }
        public int RatingGood
        {
            get
            {
                return ratingGood;
            }
            set
            {
                ratingGood = value;
                OnPropertyChanged("RatingGood");
            }
        }
        public int RatingSatisfactory
        {
            get
            {
                return ratingSatisfactory;
            }
            set
            {
                ratingSatisfactory = value;
                OnPropertyChanged("RatingSatisfactory");
            }
        }

        public Test()
        {
            TestName = "";
            Duration = 3600;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
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

        public void SaveTest(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize<Test>(this, options);
            File.WriteAllText(fileName, json);
        }

        public static Test LoadTest(string fileName)
        {
            if (File.Exists(fileName))
            {
                string temp = "";
                temp = File.ReadAllText(fileName);
                Test tempTest = new Test();
                TestForDeserialize testForDeserialize = new TestForDeserialize();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                };
                testForDeserialize = JsonSerializer.Deserialize<TestForDeserialize>(temp, options);
                tempTest.TestName = testForDeserialize.TestName;
                tempTest.Duration = testForDeserialize.Duration;
                tempTest.RatingExcellent = testForDeserialize.RatingExcellent;
                tempTest.RatingGood = testForDeserialize.RatingGood;
                tempTest.RatingSatisfactory = testForDeserialize.RatingSatisfactory;
                foreach (QuestionForDeserialize q in testForDeserialize.Questions)
                {
                    Question question = new Question();
                    question.Type = q.Type;
                    question.QuestionWording = q.QuestionWording;
                    question.Answer = q.Answer;
                    question.AnswerChoice = new ObservableCollection<AnswerCorrect>(q.AnswerChoice);
                    question.AnswerChoiceMany = new ObservableCollection<AnswerCorrect>(q.AnswerChoiceMany);
                    tempTest.Questions.Add(question);
                }
                tempTest.SelectedQuestion = tempTest.Questions[tempTest.Questions.Count - 1];
                return tempTest;
            }
            return null;
        }
    }

    public class TestForDeserialize
    {
        public string TestName { get; set; }

        public int Duration { get; set; }

        public List<QuestionForDeserialize> Questions { get; set; } = new List<QuestionForDeserialize>();

        public int RatingExcellent { get; set; }
        public int RatingGood { get; set; }
        public int RatingSatisfactory { get; set; }

        public TestForDeserialize()
        {

        }
    }
}
