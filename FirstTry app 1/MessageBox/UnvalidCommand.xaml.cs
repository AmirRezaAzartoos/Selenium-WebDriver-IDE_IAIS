using System.Windows;
using System.Windows.Input;

namespace FirstTry_app_1.MessageBox
{
    /// <summary>
    /// Interaction logic for EmptyField.xaml
    /// </summary>
    public partial class UnvalidCommand : Window
    {
        public UnvalidCommand()
        {
            InitializeComponent();
            OKButton.Focus();
        }
        public void Okclicked()
        {
            Close();
        }

        private void OKButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Okclicked();
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Okclicked();
        }
    }
}
