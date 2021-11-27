﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstTry_app_1.BL
{
    class OldIDEConverter
    {
        public void openOldTestCase(string caseAddress)
        {
            List<string> oldCase = File.ReadLines(@caseAddress).ToList();
            string[] address = caseAddress.Split(new[] { "\\" }, StringSplitOptions.None);
            string caseName = address[address.Length - 1].Remove(address[address.Length - 1].IndexOf('.'), address[address.Length - 1].Length - address[address.Length - 1].IndexOf('.'));
            string caseFolderName = address[address.Length - 2];
            oldCase.RemoveRange(0, oldCase.IndexOf("</thead><tbody>") + 1);
            oldCase.Insert(0, caseFolderName);
            oldCase.Insert(0, caseName);
            //OldIDEConverter oldIDE = new OldIDEConverter();
            TestCaseConverter(oldCase);
        }
        public async void openOldTestSuit(string suitAddress)
        {
            List<string> oldSuit = File.ReadLines(suitAddress).ToList();
            string[] address = suitAddress.Split(new[] { "\\" }, StringSplitOptions.None);
            MainWindow.ProjectName = address[address.Length - 2];
            oldSuit.RemoveRange(0, oldSuit.IndexOf("<tr><td><b>Test Suite</b></td></tr>") + 1);
            //OldIDEConverter oldIDE = new OldIDEConverter();
            //_mainWindow.loadTestProgress.Visibility = Visibility.Visible;
            await Task.Run(() => TestSuitConverter(oldSuit));
            //_mainWindow.loadTestProgress.Visibility = Visibility.Hidden;
        }

        public void TestCaseConverter(List<string> input)
        {
            MainWindow.CommandCounter = 0;
            int CommandCounter = 0;
            MainWindow.ListDB.Clear();
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "<tr>")
                {
                    CommandCounter++;
                    MainWindow.CommandCounter++;
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        MainWindow _mainWindow = new MainWindow();
                        string tempCommand = _mainWindow.FindBetween(input[i + 1], "<td>", "</td>");
                        if (tempCommand == "open2" || tempCommand == "open2AndWait")
                            tempCommand = "open";
                        if (tempCommand == "clickAndWait")
                            tempCommand = "click";
                        MainWindow.ListDB.Add(new Commands(CommandCounter, tempCommand, _mainWindow.FindBetween(input[i + 2], "<td>", "</td>").Replace("&quot;", "\"").Replace("&amp;", "&"), _mainWindow.FindBetween(input[i + 3], "<td>", "</td>").Replace("&quot;", "\"").Replace("&amp;", "&"), _mainWindow.FindBetween(input[i + 1], "<td>", "</td>") + Convert.ToString(CommandCounter + 1), "None", false));
                    }));
                i += 4;
                }
            }
            MainWindow.testCaseCounter++;
            MainWindow._testCaseCounter++;
            MainWindow.TestList.Add(new TestSuit() { TestNumber = MainWindow.testCaseCounter, TestName = input[0], TestValue = new ObservableCollection<Commands>(MainWindow.ListDB), TestFolder = input[1], SavedPath = null, IsSaved = false });
            Application.Current.Dispatcher.Invoke(new Action(() => {
                MainWindow _mainWindow = new MainWindow();
                _mainWindow.TestCaseListView.ItemsSource = MainWindow.TestList;
                _mainWindow.listView.ItemsSource = MainWindow.ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(MainWindow.TestList);
                view2.Refresh();
            }));
        }
    public async Task TestSuitConverter(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Contains("href="))
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        MainWindow _mainWindow = new MainWindow();
                        string temp = "C:\\seleniums" + _mainWindow.FindBetween(input[i], "../..", "\">").Replace("/", "\\");
                        openOldTestCase(temp);
                    }));
                }
            }
        }
    }
}