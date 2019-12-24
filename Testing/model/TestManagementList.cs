using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            Settings.Default.list_of_tests.Clear();
            DirectoryInfo dir = new DirectoryInfo(Settings.Default.defaulf_test_file_save_path);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                Settings.Default.list_of_tests.Add(file.FullName, file.Name.Replace(".json", ""));
            }
            foreach (string s in collection.AllKeys)
            {
                TestsCollection.Add(new TestWithPath() { Path = s, TestName = collection[s] });
            }
            Settings.Default.Save();
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
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

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
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
