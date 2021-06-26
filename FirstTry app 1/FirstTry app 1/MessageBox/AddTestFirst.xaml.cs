using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstTry_app_1.MessageBox
{
    /// <summary>
    /// Interaction logic for AddTestFirst.xaml
    /// </summary>
    public partial class AddTestFirst : Window
    {
        public AddTestFirst()
        {
            InitializeComponent();
            OKAddTestButton.Focus();
        }

        public void OKAddTestButton_Click(object sender, RoutedEventArgs e)
        {
            Okclicked();
        }
        public void Okclicked()
        {
            //MainWindow mainWindow = Owner as MainWindow;
            //mainWindow.AddTestCaseButton();
            //mainWindow.MainGrid.Effect = new BlurEffect();
            //mainWindow.Splash.Visibility = Visibility.Visible;
            //AddTestCase addTestCase = new AddTestCase();
            //addTestCase.Owner = this;
            //addTestCase.ShowDialog();
            //mainWindow.Splash.Visibility = Visibility.Collapsed;
            //mainWindow.MainGrid.Effect = null;
            Close();
        }
        private void OKAddTestButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Okclicked();
            }
        }
    }
}
