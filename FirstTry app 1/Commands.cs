using System;
using System.Collections.ObjectModel;

namespace FirstTry_app_1
{
    public class Commands : ObservableObject
    {
        private int _number;
        private string _command;
        private string _target;
        private string _value;
        private string _variableName;
        private string _description;
        private MainWindow.Pass _pass;

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }


        public string Command
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_command))
                {
                    return "None";
                }

                return _command;
            }
            set
            {
                _command = value;
                OnPropertyChanged("Command");
            }
        }
        public string Target
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_target))
                {
                    return "None";
                }

                return _target;
            }
            set
            {
                _target = value;
                OnPropertyChanged("Target");
            }
        }

        public string Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_value))
                {
                    return "None";
                }

                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public string VariableName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_variableName))
                {
                    return "None";
                }

                return _variableName;
            }
            set
            {
                _variableName = value;
                OnPropertyChanged("VariableName");
            }
        }

        public string Description
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    return "None";
                }

                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public MainWindow.Pass Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                OnPropertyChanged("Pass");
            }
        }

        public override string ToString()
        {
            return Number + Command + Target + Value + VariableName + Description;
        }

        public static implicit operator Commands(ObservableCollection<Commands> v)
        {
            throw new NotImplementedException();
        }

        public Commands(int number, string command, string target, string value, string variableName, string description, MainWindow.Pass pass)
        {
            Number = number;
            Command = command;
            Target = target;
            Value = value;
            VariableName = variableName;
            Pass = pass;
        }
    }
}
