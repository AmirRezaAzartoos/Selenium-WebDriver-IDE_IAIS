using System.Windows;
using System.Windows.Input;

namespace FirstTry_app_1.MessageBox
{
    /// <summary>
    /// Interaction logic for UpdateAck.xaml
    /// </summary>
    public partial class UpdateAck : Window
    {
        public UpdateAck()
        {
            InitializeComponent();
        }

        private void YesBT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.updateAck = true;
            Close();
        }

        private void CancelBT_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void YesBT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainWindow.updateAck = true;
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
