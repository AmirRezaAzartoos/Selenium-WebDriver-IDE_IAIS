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
