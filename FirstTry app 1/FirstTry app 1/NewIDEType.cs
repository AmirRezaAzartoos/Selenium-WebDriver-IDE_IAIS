using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FirstTry_app_1
{
    class NewIDEType : ObservableObject
    {
        private string _id;
        private string _name;
        private Commands _comands;

        public string Id
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_id))
                    return "None";

                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Number");
            }
        }


        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_name))
                    return "None";

                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Command");
            }
        }

        public Commands Commands
        {
            get
            {
                return _comands;
            }
            set
            {
                _comands = value;
                OnPropertyChanged("Target");
            }
        }


        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        }
}