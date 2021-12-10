using System.Windows;
using System.Windows.Input;

namespace FirstTry_app_1.MessageBox
{
    /// <summary>
    /// Interaction logic for TestSuitNameWarn.xaml
    /// </summary>
    public partial class TestSuitNameWarn : Window
    {
        public TestSuitNameWarn()
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
