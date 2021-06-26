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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
