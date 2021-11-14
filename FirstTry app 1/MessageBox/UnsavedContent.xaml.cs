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
