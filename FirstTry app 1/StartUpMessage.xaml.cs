using System.Windows;
using System.Windows.Input;

namespace FirstTry_app_1
{
    /// <summary>
    /// Interaction logic for StartUpMessage.xaml
    /// </summary>
    public partial class StartUpMessage : Window
    {
        public StartUpMessage()
        {
            InitializeComponent();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
