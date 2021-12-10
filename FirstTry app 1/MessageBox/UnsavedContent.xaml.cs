using System.Windows;
using System.Windows.Input;

namespace FirstTry_app_1.MessageBox
{
    /// <summary>
    /// Interaction logic for UnsavedContent.xaml
    /// </summary>
    public partial class UnsavedContent : Window
    {
        public UnsavedContent()
        {
            InitializeComponent();
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

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Continue = true;
            Close();
        }
    }
}
