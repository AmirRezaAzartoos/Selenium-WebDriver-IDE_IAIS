using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstTry_app_1.MessageBox
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window, INotifyPropertyChanged
    {
        private BackgroundWorker _bgWorker = new BackgroundWorker();

        private int _progressState;

        public int ProgressState
        {
            get { return _progressState; }
            set
            {
                _progressState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressState"));
            }
        }
        public ProgressBar()
        {
            InitializeComponent();
            DataContext = this;
            _bgWorker.DoWork += (s, e) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    ProgressState = i;
                }
            };
            _bgWorker.RunWorkerAsync();
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
