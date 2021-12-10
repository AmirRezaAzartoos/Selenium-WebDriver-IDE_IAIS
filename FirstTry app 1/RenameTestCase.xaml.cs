using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace FirstTry_app_1
{
    /// <summary>
    /// Interaction logic for RenameTestCase.xaml
    /// </summary>
    public partial class RenameTestCase : Window
    {
        public RenameTestCase()
        {
            InitializeComponent();
            RenameTestCaseTB.Focus();
        }
        public void Enter()
        {
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.TestCaseListView.ItemsSource = MainWindow.TestList;
            RenameTestCase renameTestCase = new RenameTestCase
            {
                Owner = this
            };
            TestSuit obj = MainWindow.TestList.FirstOrDefault(x => x.TestName == MainWindow.SelectedTest.TestName);
            if (obj != null)
            {
                obj.TestName = RenameTestCaseTB.Text;
            }

            ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.TestList);
            view.Refresh();
            Close();
        }
        public void EmptyFieldtDialog()
        {
            MainBorder.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            MessageBox.EmptyField _emptyFieldDialog = new MessageBox.EmptyField();
            _emptyFieldDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainBorder.Effect = null;
        }
        private void RenameTestCaseTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (RenameTestCaseTB.Text != "")
                    {
                        Enter();
                    }
                    else
                    {
                        EmptyFieldtDialog();
                    }
                }
                catch (Exception)
                {
                }
            }
        }


        private void RenameTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RenameTestCaseTB.Text != "")
                {
                    Enter();
                }
                else
                {
                    EmptyFieldtDialog();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
