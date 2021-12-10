using System.Collections.ObjectModel;

namespace FirstTry_app_1
{
    public class TestSuit : ObservableObject
    {
        private int _testNumber;
        private string _testName;
        private string _testFolder;
        private ObservableCollection<Commands> _testValue;
        private string _savedPath;
        private bool _isSaved;
        private MainWindow.Pass _isPassed;

        public int TestNumber
        {
            get => _testNumber;
            set
            {
                _testNumber = value;
                OnPropertyChanged("TestNumber");
            }
        }
        public string TestName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_testName))
                {
                    return "Unknown";
                }

                return _testName;
            }
            set
            {
                _testName = value;
                OnPropertyChanged("TestName");
            }
        }
        public ObservableCollection<Commands> TestValue
        {
            get => _testValue;
            set
            {
                _testValue = value;
                OnPropertyChanged("TestValue");
            }
        }
        public string TestFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_testFolder))
                {
                    return "Unknown";
                }

                return _testFolder;
            }
            set
            {
                _testFolder = value;
                OnPropertyChanged("TestFolder");
            }
        }
        public string SavedPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_savedPath))
                {
                    return null;
                }

                return _savedPath;
            }
            set
            {
                _savedPath = value;
                OnPropertyChanged("SavedPath");
            }
        }
        public bool IsSaved
        {
            get => _isSaved;
            set
            {
                _isSaved = value;
                OnPropertyChanged("IsSaved");
            }
        }
        public MainWindow.Pass IsPassed
        {
            get => _isPassed;
            set
            {
                _isPassed = value;
                OnPropertyChanged("IsPassed");
            }
        }
    }
}
