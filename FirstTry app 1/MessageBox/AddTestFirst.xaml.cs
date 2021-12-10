using System.Windows;
using System.Windows.Input;

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
