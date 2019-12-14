using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Testing.Properties;

namespace Testing
{
    class TestManagementList : INotifyPropertyChanged
    {

        public ObservableCollection<TestWithPath> testsCollection = new ObservableCollection<TestWithPath>();
        public ObservableCollection<TestWithPath> TestsCollection
        {
            get
            {
                return testsCollection;
            }
            set
            {
                testsCollection = value;
                OnPropertyChanged("TestsCollection");
            }
        }

        public TestManagementList()
        {

        }

        public TestManagementList(System.Collections.Specialized.NameValueCollection collection)
        {
            foreach(string s in collection.AllKeys)
            {
                TestsCollection.Add(new TestWithPath() { Path = collection[s], TestName = s });
            }
        }

        public void UpdateCollection(System.Collections.Specialized.NameValueCollection collection)
        {
            this.TestsCollection.Clear();
            foreach (string s in collection.AllKeys)
            {
                TestsCollection.Add(new TestWithPath() { Path = collection[s], TestName = s });
            }
        }

        public void UpdateSettings()
        {
            Settings.Default.list_of_tests.Clear();
            foreach (TestWithPath test in TestsCollection)
            {
                Settings.Default.list_of_tests.Add(test.TestName, test.Path);
            }
            Settings.Default.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /*private TestCommand addTest;
        public TestCommand AddTest
        {
            get
            {
                return addTest ??
                  (addTest = new TestCommand(obj =>
                  {
                      TestWithPath test = new TestWithPath() { Path = ((TestWithPath)obj).Path, TestName = ((TestWithPath)obj).TestName };
                      TestsCollection.Insert(TestsCollection.Count, test);
                  }));
            }
        }*/

        private TestCommand removeTest;
        public TestCommand RemoveTest
        {
            get
            {
                return removeTest ??
                  (removeTest = new TestCommand(obj =>
                  {
                      TestWithPath test = obj as TestWithPath;
                      if (test != null)
                      {
                          TestsCollection.Remove(test);
                      }
                  },
                 (obj) => TestsCollection.Count > 0));
            }
        }
    }

    class TestWithPath : INotifyPropertyChanged
    {
        private string testName;
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
        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
