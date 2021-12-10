using FirstTry_app_1.MessageBox;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace FirstTry_app_1
{
    /// <summary>
    /// Interaction logic for AddTestCase.xaml
    /// </summary>
    public partial class AddTestCase : Window
    {
        public AddTestCase()
        {
            InitializeComponent();
            TestCaseTB.Focus();
        }
        public void Enter()
        {
            TestCaseTB.Text = TestCaseTB.Text.Replace("-", "_").Replace(".", "_").Replace(" ", "_").Replace("&", "_").Replace("=", "_").Replace("*", "_").Replace("!", "_").Replace("#", "_");
            MainWindow._testCaseNameCount = (TestCaseTB.Text.Length > MainWindow._testCaseNameCount) ? TestCaseTB.Text.Length : MainWindow._testCaseNameCount;
            MainWindow.ListDB.Clear();
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.CommandsComboBox.Text = "";
            mainWindow.TargetTB.Text = "";
            mainWindow.ValueTB.Text = "";
            mainWindow.DescriptionTB.Text = "";
            mainWindow.TestCaseListView.ItemsSource = MainWindow.TestList;
            AddTestFirst addTestFirst = new AddTestFirst
            {
                Owner = this
            };
            MainWindow.testCaseCounter++;
            mainWindow.TestCaseListView.SelectedIndex = MainWindow._testCaseCounter;
            MainWindow.TestList.Add(new TestSuit() { TestNumber = MainWindow.testCaseCounter, TestName = TestCaseTB.Text, IsSaved = false });
            MainWindow._testCaseCounter = MainWindow.testCaseCounter;
            mainWindow.TestCaseCounterTB.Text = Convert.ToString(MainWindow.testCaseCounter);
            MainWindow._commandCounter = MainWindow.CommandCounter = 0;
            mainWindow.CommandCounterTB.Text = Convert.ToString(MainWindow.CommandCounter);
            ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.TestList);
            view.Refresh();
            ICollectionView view2 = CollectionViewSource.GetDefaultView(MainWindow.ListDB);
            view2.Refresh();
            Close();
            MainWindow._testCaseFileName = TestCaseTB.Text + ".py";
        }
        public void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TestCaseTB.Text != "")
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

        public void EmptyFieldtDialog()
        {
            MainBorder.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            EmptyField _emptyFieldDialog = new MessageBox.EmptyField();
            _emptyFieldDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainBorder.Effect = null;
        }

        private void TestCaseTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (TestCaseTB.Text != "")
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
            else if (e.Key == Key.Space)
            {
                try
                {
                    TestCaseTB.Text += "_";
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
