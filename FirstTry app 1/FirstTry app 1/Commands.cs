using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Effects;
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
        private bool _pass;

        public int Number
        {
            get
            {
                return _number;
            }
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
                    return "None";

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
                    return "None";

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
                    return "None";

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
                    return "None";

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
                    return "None";

                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public bool Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
                OnPropertyChanged("Pass");
            }
        }

        public override string ToString()
        {
            return this.Number + this.Command + this.Target + this.Value + this.VariableName + this.Description;
        }

        public static implicit operator Commands(ObservableCollection<Commands> v)
        {
            throw new NotImplementedException();
        }

        public Commands(int number, string command, string target, string value, string variableName, string description, bool pass)
        {
            this.Number = number;
            this.Command = command;
            this.Target = target;
            this.Value = value;
            this.VariableName = variableName;
            this.Pass = pass;
        }
    }
}
