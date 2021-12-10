using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace FirstTry_app_1
{
    /// <summary>
    /// Interaction logic for CreateNewProject.xaml
    /// </summary>
    public partial class CreateNewProject : Window
    {

        public CreateNewProject()
        {
            InitializeComponent();
            ProjectNameTB.Focus();
            MainWindow.TestSuitSaved = false;
            //this.WindowStartupLocation = WindowStartupLocation.Manual;
            //this.Left = Application.Current.MainWindow.Left;
            //this.Top = Application.Current.MainWindow.Top;
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

        public void UnsavedContentDialog()
        {
            MainBorder.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            MessageBox.UnsavedContent _unsavedContentDialog = new MessageBox.UnsavedContent();
            _unsavedContentDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainBorder.Effect = null;
        }

        public void Enter()
        {
            try
            {
                if (ProjectNameTB.Text != "")
                {
                    if (MainWindow.gPath == null && MainWindow.Continue == false && MainWindow.testCaseCounter != 0)
                    {
                        UnsavedContentDialog();
                        if (MainWindow.Continue == true)
                        {
                            MainWindow mainWindow = Owner as MainWindow;
                            CreateNewProject createNewProject = new CreateNewProject
                            {
                                Owner = this
                            };
                            MainWindow.ProjectName = ProjectNameTB.Text;
                            MainWindow.gPath = null;
                            MainWindow.ListDB.Clear();
                            MainWindow.TestList.Clear();
                            MainWindow.CommandCounter = 0;
                            mainWindow.CommandCounterTB.Text = Convert.ToString(MainWindow.CommandCounter);
                            MainWindow._commandCounter = 0;
                            MainWindow.testCaseCounter = 0;
                            mainWindow.TestCaseCounterTB.Text = Convert.ToString(MainWindow.testCaseCounter);
                            MainWindow._testCaseCounter = 0;
                            MainWindow.sPath = null;
                            MainWindow.gPath = null;
                            MainWindow.testCaseFileName = null;
                            MainWindow._testCaseFileName = null;
                            MainWindow.FolderName = null;
                            MainWindow._openFileNameArray = null;
                            MainWindow._openFileName = null;
                            MainWindow._openFileAddressArray = null;
                            MainWindow._openFileAddress = null;
                            MainWindow._openedFiles = 0;
                            MainWindow.openTestCaseContent = null;
                            ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.TestList);
                            view.Refresh();
                            ICollectionView view2 = CollectionViewSource.GetDefaultView(MainWindow.ListDB);
                            view2.Refresh();
                            Close();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    else
                    {
                        MainWindow mainWindow = Owner as MainWindow;
                        CreateNewProject createNewProject = new CreateNewProject
                        {
                            Owner = this
                        };
                        MainWindow.ProjectName = ProjectNameTB.Text;
                        MainWindow.gPath = null;
                        MainWindow.ListDB.Clear();
                        MainWindow.TestList.Clear();
                        MainWindow.CommandCounter = 0;
                        mainWindow.CommandCounterTB.Text = Convert.ToString(MainWindow.CommandCounter);
                        MainWindow._commandCounter = 0;
                        MainWindow.testCaseCounter = 0;
                        mainWindow.TestCaseCounterTB.Text = Convert.ToString(MainWindow.testCaseCounter);
                        MainWindow._testCaseCounter = 0;
                        MainWindow.sPath = null;
                        MainWindow.gPath = null;
                        MainWindow.testCaseFileName = null;
                        MainWindow._testCaseFileName = null;
                        MainWindow.FolderName = null;
                        MainWindow._openFileNameArray = null;
                        MainWindow._openFileName = null;
                        MainWindow._openFileAddressArray = null;
                        MainWindow._openFileAddress = null;
                        MainWindow._openedFiles = 0;
                        MainWindow.openTestCaseContent = null;
                        ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.TestList);
                        view.Refresh();
                        ICollectionView view2 = CollectionViewSource.GetDefaultView(MainWindow.ListDB);
                        view2.Refresh();
                        Close();
                    }
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

        private void ProjectNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Enter();
            }
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Enter();
        }
    }
}

