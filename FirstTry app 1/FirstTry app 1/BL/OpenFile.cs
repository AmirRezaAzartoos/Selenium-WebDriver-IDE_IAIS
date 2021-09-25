using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace FirstTry_app_1.BL
{
    class OpenFile
    {
        MainWindow _mainWindow = new MainWindow();

        #region Open

        public void OpenTestCase(string C)
        {
            if (C != null)
            {
                MainWindow._commandCounter = MainWindow.CommandCounter = 0;
                _mainWindow.CommandCounterTB.Text = Convert.ToString(MainWindow.CommandCounter);
                MainWindow.testCaseCounter++;
                _mainWindow.TestCaseCounterTB.Text = Convert.ToString(MainWindow.testCaseCounter);
                MainWindow._testCaseCounter = MainWindow.testCaseCounter;
                int _commandCounter = 0;
                int _counter = 0;
                int i = 0;
                string tabNeededTemp = "";
                List<string> lines = File.ReadLines(@C).ToList();

                while (i < lines.Count)
                {
                    if (lines.ElementAt(_counter).Contains("class ") && !lines.ElementAt(_counter).Contains("StoreEvalDB()"))
                        break;
                    else
                        i++;
                    _counter++;
                }
                _counter++;
                MainWindow.ListDB.Clear();

                while (lines.Count > _counter)
                {
                    string _currentTarget = "";
                    string _currentValue = "";
                    string _currentVariableName = "";
                    string _currentDescription = "";
                    string specifyTarget(string input)
                    {
                        string outType = "empty";
                        string outValue = "empty";
                        string OUT = "empty";
                        if (input.Contains("present_in_element_value"))
                        {
                            outType = _mainWindow.FindBetween(input, "present_in_element_value((\"", "\", \"");
                            outValue = _mainWindow.FindBetween(input, "\", \"", "\"), ");
                        }
                        else if (input.Contains("present_in_element"))
                        {
                            outType = _mainWindow.FindBetween(input, "present_in_element((\"", "\", \"");
                            outValue = _mainWindow.FindBetween(input, "\", \"", "\"), ");
                        }
                        else if (input.Contains("element_located"))
                        {
                            outType = _mainWindow.FindBetween(input, "element_located((\"", "\", \"");
                            outValue = _mainWindow.FindBetween(input, "\", \"", "\")))");
                        }
                        else if (input.Contains("elements_located"))
                        {
                            outType = _mainWindow.FindBetween(input, "elements_located((\"", "\", \"");
                            outValue = _mainWindow.FindBetween(input, "\", \"", "\")))");
                        }
                        else if (input.Contains("find_element_by"))
                        {
                            outType = _mainWindow.FindBetween(input, "find_element_by_", "(\"");
                            outValue = _mainWindow.FindBetween(input, "find_element_by_" + outType + "(\"", "\")");
                        }
                        switch (outType.Replace("_", " "))
                        {
                            case "class name":
                                outType = "class";
                                OUT = "class=" + outValue;
                                break;
                            case "css selector":
                                outType = "css";
                                OUT = "css=" + outValue;
                                break;
                            case "id":
                                outType = "id";
                                OUT = "id=" + outValue;
                                break;
                            case "link text":
                                outType = "link";
                                OUT = "link=" + outValue;
                                break;
                            case "name":
                                outType = "name";
                                OUT = "name=" + outValue;
                                break;
                            case "partial link text":
                                outType = "partial";
                                OUT = "partial=" + outValue;
                                break;
                            case "tag name":
                                outType = "tag";
                                OUT = "tag=" + outValue;
                                break;
                            case "xpath":
                                outType = "xpath";
                                OUT = outValue;
                                break;
                        }
                        return OUT;
                    }
                    if (lines.ElementAt(_counter).Contains(" | "))
                    {
                        _currentVariableName = lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2).Replace(" ", "") + _commandCounter.ToString();
                        tabNeededTemp = lines.ElementAt(_counter).Remove(lines.ElementAt(_counter).IndexOf("\t#"), lines.ElementAt(_counter).Length - lines.ElementAt(_counter).IndexOf("\t#"));
                        string tempCurrentCommand = lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2).Replace(" ", "");
                        switch (tempCurrentCommand)
                        {
                            #region ===> open
                            case "open":
                                _currentTarget = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "driver.get(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForElementPresent
                            case "waitForElementPresent":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForElementVisible
                            case "waitForElementVisible":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForElementNotPresent
                            case "waitForElementNotPresent":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForText
                            case "waitForText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "\"), ", "))"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForNotText
                            case "waitForNotText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForValue
                            case "waitForValue":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForAttribute
                            case "waitForAttribute":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 3));
                                _currentDescription = lines.ElementAt(_counter + 12).Remove(0, lines.ElementAt(_counter + 12).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForWindowPresent
                            case "waitForWindowPresent":
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForNumberOfWindowPresent
                            case "waitForNumberOfWindowPresent":
                                _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "number_of_windows_to_be(", "))");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> type
                            case "type":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 4), ".send_keys(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 5).Remove(0, lines.ElementAt(_counter + 5).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> sendKeys
                            case "sendKeys":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), ".send_keys(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> clearText
                            case "clearText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> click
                            case "click":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> select
                            case "select":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "//option[. = ", "]\")"));
                                _currentDescription = lines.ElementAt(_counter + 6).Remove(0, lines.ElementAt(_counter + 6).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> selectByVisibleText
                            case "selectByVisibleText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "select_by_visible_text(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> selectByValue
                            case "selectByValue":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "select_by_value(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> selectByIndex
                            case "selectByIndex":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "select_by_index(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeText
                            case "storeText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeValue
                            case "storeValue":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeWicketPath
                            case "storeWicketPath":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeInnerHTML
                            case "storeInnerHTML":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeName
                            case "storeName":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeId
                            case "storeId":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeHref
                            case "storeHref":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeEval
                            case "storeEval":
                                _currentTarget = _mainWindow.ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeElementPresent
                            case "storeElementPresent":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 2));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 6).Remove(0, lines.ElementAt(_counter + 6).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> alert
                            case "alert":
                                _currentDescription = lines.ElementAt(_counter + 3).Remove(0, lines.ElementAt(_counter + 3).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> replace
                            case "replace":
                                _currentTarget = _mainWindow.ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> runScript
                            case "runScript":
                                string temprunScript = specifyTarget(lines.ElementAt(_counter + 1));
                                if (temprunScript == "empty")
                                {
                                    if (lines.ElementAt(_counter + 1).Contains("execute_script(\""))
                                    {
                                        _currentValue = lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("execute_script(\"") + 16);
                                        _currentValue = _currentValue.Remove(_currentValue.LastIndexOf("\")"), 2);
                                    }
                                    if (lines.ElementAt(_counter + 1).Contains("execute_script('"))
                                        _currentValue = lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("execute_script('") + 16).Remove(lines.ElementAt(_counter + 1).LastIndexOf("')"), 2);
                                }
                                else
                                {
                                    if (lines.ElementAt(_counter + 1).Contains("execute_script(\""))
                                        _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "execute_script(\"", "\", ");
                                    if (lines.ElementAt(_counter + 1).Contains("execute_script('"))
                                        _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "execute_script('", "', ");
                                    _currentTarget = temprunScript;
                                }
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> switch
                            case "switch":
                                _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), ".switch_to.", "(");
                                string tempswitch = lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).LastIndexOf("driver.") + 7);
                                _currentValue = tempswitch.Remove(tempswitch.Length - 1, 1);
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> switchToDefault
                            case "switchToDefault":
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> scrollInto
                            case "scrollInto":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentDescription = lines.ElementAt(_counter + 3).Remove(0, lines.ElementAt(_counter + 3).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> while
                            case "while":
                                MainWindow.inWhileOrIf.Add(_commandCounter, true);
                                _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "while ", ":");
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> break
                            case "break":
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> if
                            case "if":
                                MainWindow.inWhileOrIf.Add(_commandCounter, true);
                                _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "if ", ":");
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> end
                            /*case "end":
                                break;*/
                            #endregion

                            #region ===> refresh
                            case "refresh":
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> close
                            case "close":
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> failTest
                            case "failTest":
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> pause
                            case "pause":
                                _currentTarget = (Convert.ToInt16(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "time.sleep(", ")")) * 1000).ToString();
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                                #endregion
                        }
                        _commandCounter++;
                        MainWindow.ListDB.Add(new Commands(_commandCounter, tabNeededTemp + lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
                    }

                    else if (lines.ElementAt(_counter).Contains("# end"))
                    {
                        _commandCounter++;
                        MainWindow.ListDB.Add(new Commands(_commandCounter, lines.ElementAt(_counter).Replace("\t# ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
                        if (MainWindow.sumTrue == 0)
                            throw new Exception("Unvalid Command : No operations exist");
                        else
                        {
                            MainWindow.sumTrue--;
                            MainWindow.tabNeeded = string.Concat(Enumerable.Repeat("\t", MainWindow.sumTrue));
                            MainWindow.inWhileOrIf.Remove(MainWindow.inWhileOrIf.Keys.LastOrDefault());
                        }
                    }

                    _counter++;

                    MainWindow.sumTrue = MainWindow.inWhileOrIf.Count(x => x.Value == true);
                    MainWindow.tabNeeded = string.Concat(Enumerable.Repeat("\t", MainWindow.sumTrue));
                }

                MainWindow.sumTrue = MainWindow.inWhileOrIf.Count(x => x.Value == true);
                MainWindow.tabNeeded = string.Concat(Enumerable.Repeat("\t", MainWindow.sumTrue));

                MainWindow._commandCounter = MainWindow.CommandCounter = _commandCounter;
                _mainWindow.CommandCounterTB.Text = Convert.ToString(MainWindow.CommandCounter);
                MainWindow.TestList.Add(new TestSuit() { TestNumber = MainWindow.testCaseCounter, TestName = MainWindow._openFileName, TestValue = new ObservableCollection<Commands>(MainWindow.ListDB), TestFolder = MainWindow.FolderName, SavedPath = C, IsSaved = true });
                _mainWindow.TestCaseListView.ItemsSource = MainWindow.TestList;
                _mainWindow.listView.ItemsSource = MainWindow.ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(MainWindow.TestList);
                view2.Refresh();
            }
        }

        ///////

        public void OpenTestSuit(string D)
        {
            if (D != null)
            {
                MainWindow.ListDB.Clear();
                string _testName = "";
                int _commandCounter = 1;
                int _counter = 0;
                int _counter1 = 0;
                int i = 1;
                int j = 0;
                bool passed = false;
                string tabNeededTemp = "";
                List<string> lines = File.ReadLines(@D).ToList();
                while (_counter < lines.Count)
                {
                    if (lines.ElementAt(_counter).Contains("try:"))
                        break;
                    else
                        _counter++;
                }
                while (j < lines.Count)
                {
                    if (lines.ElementAt(j).Contains("class ") && !lines.ElementAt(j).Contains("class StoreEvalDB()"))
                    {
                        j++;
                        _counter1++;
                    }
                    else
                        j++;
                }
                while (_counter1 > i)
                {
                    while (lines.Count > _counter)
                    {
                        if (lines.ElementAt(_counter).Contains("class ") && passed == false)
                        {
                            _testName = _mainWindow.FindBetween(lines.ElementAt(_counter), "class ", ":");
                            passed = true;
                            _counter++;
                            MainWindow.testCaseCounter++;
                        }

                        string _currentTarget = "";
                        string _currentValue = "";
                        string _currentVariableName = "";
                        string _currentDescription = "";
                        string specifyTarget(string input)
                        {
                            string outType = "empty";
                            string outValue = "empty";
                            string OUT = "empty";
                            if (input.Contains("present_in_element_value"))
                            {
                                outType = _mainWindow.FindBetween(input, "present_in_element_value((\"", "\", \"");
                                outValue = _mainWindow.FindBetween(input, "\", \"", "\"), ");
                            }
                            else if (input.Contains("present_in_element"))
                            {
                                outType = _mainWindow.FindBetween(input, "present_in_element((\"", "\", \"");
                                outValue = _mainWindow.FindBetween(input, "\", \"", "\"), ");
                            }
                            else if (input.Contains("element_located"))
                            {
                                outType = _mainWindow.FindBetween(input, "element_located((\"", "\", \"");
                                outValue = _mainWindow.FindBetween(input, "\", \"", "\")))");
                            }
                            else if (input.Contains("elements_located"))
                            {
                                outType = _mainWindow.FindBetween(input, "elements_located((\"", "\", \"");
                                outValue = _mainWindow.FindBetween(input, "\", \"", "\")))");
                            }
                            else if (input.Contains("find_element_by"))
                            {
                                outType = _mainWindow.FindBetween(input, "find_element_by_", "(\"");
                                outValue = _mainWindow.FindBetween(input, "find_element_by_" + outType + "(\"", "\")");
                            }
                            switch (outType.Replace("_", " "))
                            {
                                case "class name":
                                    outType = "class";
                                    OUT = "class=" + outValue;
                                    break;
                                case "css selector":
                                    outType = "css";
                                    OUT = "css=" + outValue;
                                    break;
                                case "id":
                                    outType = "id";
                                    OUT = "id=" + outValue;
                                    break;
                                case "link text":
                                    outType = "link";
                                    OUT = "link=" + outValue;
                                    break;
                                case "name":
                                    outType = "name";
                                    OUT = "name=" + outValue;
                                    break;
                                case "partial link text":
                                    outType = "partial";
                                    OUT = "partial=" + outValue;
                                    break;
                                case "tag name":
                                    outType = "tag";
                                    OUT = "tag=" + outValue;
                                    break;
                                case "xpath":
                                    outType = "xpath";
                                    OUT = outValue;
                                    break;
                            }
                            return OUT;
                        }
                        if (lines.ElementAt(_counter).Contains(" | "))
                        {
                            _currentVariableName = lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2).Replace(" ", "") + _commandCounter.ToString();
                            tabNeededTemp = lines.ElementAt(_counter).Remove(lines.ElementAt(_counter).IndexOf("\t#"), lines.ElementAt(_counter).Length - lines.ElementAt(_counter).IndexOf("\t#"));
                            string tempCurrentCommand = lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2).Replace(" ", "");
                            switch (tempCurrentCommand)
                            {
                                #region ===> open
                                case "open":
                                    _currentTarget = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "driver.get(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForElementPresent
                                case "waitForElementPresent":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForElementVisible
                                case "waitForElementVisible":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForElementNotPresent
                                case "waitForElementNotPresent":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForText
                                case "waitForText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "\"), ", "))"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForNotText
                                case "waitForNotText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForValue
                                case "waitForValue":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForAttribute
                                case "waitForAttribute":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 3));
                                    _currentDescription = lines.ElementAt(_counter + 12).Remove(0, lines.ElementAt(_counter + 12).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForWindowPresent
                                case "waitForWindowPresent":
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForNumberOfWindowPresent
                                case "waitForNumberOfWindowPresent":
                                    _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "number_of_windows_to_be(", "))");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> type
                                case "type":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 4), ".send_keys(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 5).Remove(0, lines.ElementAt(_counter + 5).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> sendKeys
                                case "sendKeys":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), ".send_keys(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> clearText
                                case "clearText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> click
                                case "click":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> select
                                case "select":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "//option[. = ", "]\")"));
                                    _currentDescription = lines.ElementAt(_counter + 6).Remove(0, lines.ElementAt(_counter + 6).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> selectByVisibleText
                                case "selectByVisibleText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "select_by_visible_text(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> selectByValue
                                case "selectByValue":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "select_by_value(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> selectByIndex
                                case "selectByIndex":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.ConvertTextFromIdeFormat(_mainWindow.FindBetween(lines.ElementAt(_counter + 3), "select_by_index(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeText
                                case "storeText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeValue
                                case "storeValue":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeWicketPath
                                case "storeWicketPath":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeInnerHTML
                                case "storeInnerHTML":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeName
                                case "storeName":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeId
                                case "storeId":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeHref
                                case "storeHref":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeEval
                                case "storeEval":
                                    _currentTarget = _mainWindow.ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeElementPresent
                                case "storeElementPresent":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 2));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 6).Remove(0, lines.ElementAt(_counter + 6).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> alert
                                case "alert":
                                    _currentDescription = lines.ElementAt(_counter + 3).Remove(0, lines.ElementAt(_counter + 3).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> replace
                                case "replace":
                                    _currentTarget = _mainWindow.ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                    _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> runScript
                                case "runScript":
                                    string temprunScript = specifyTarget(lines.ElementAt(_counter + 1));
                                    if (temprunScript == "empty")
                                    {
                                        if (lines.ElementAt(_counter + 1).Contains("execute_script(\""))
                                        {
                                            _currentValue = lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("execute_script(\"") + 16);
                                            _currentValue = _currentValue.Remove(_currentValue.LastIndexOf("\")"), 2);
                                        }
                                        if (lines.ElementAt(_counter + 1).Contains("execute_script('"))
                                            _currentValue = lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("execute_script('") + 16).Remove(lines.ElementAt(_counter + 1).LastIndexOf("')"), 2);
                                    }
                                    else
                                    {
                                        if (lines.ElementAt(_counter + 1).Contains("execute_script(\""))
                                            _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "execute_script(\"", "\", ");
                                        if (lines.ElementAt(_counter + 1).Contains("execute_script('"))
                                            _currentValue = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "execute_script('", "', ");
                                        _currentTarget = temprunScript;
                                    }
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> switch
                                case "switch":
                                    _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), ".switch_to.", "(");
                                    string tempswitch = lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).LastIndexOf("driver.") + 7);
                                    _currentValue = tempswitch.Remove(tempswitch.Length - 1, 1);
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> switchToDefault
                                case "switchToDefault":
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> scrollInto
                                case "scrollInto":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentDescription = lines.ElementAt(_counter + 3).Remove(0, lines.ElementAt(_counter + 3).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> while
                                case "while":
                                    MainWindow.inWhileOrIf.Add(_commandCounter, true);
                                    _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "while ", ":");
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> break
                                case "break":
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> if
                                case "if":
                                    MainWindow.inWhileOrIf.Add(_commandCounter, true);
                                    _currentTarget = _mainWindow.FindBetween(lines.ElementAt(_counter + 1), "if ", ":");
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> end
                                /*case "end":
                                    break;*/
                                #endregion

                                #region ===> refresh
                                case "refresh":
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> close
                                case "close":
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> failTest
                                case "failTest":
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> pause
                                case "pause":
                                    _currentTarget = (Convert.ToInt16(_mainWindow.FindBetween(lines.ElementAt(_counter + 1), "time.sleep(", ")")) * 1000).ToString();
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                    #endregion
                            }
                            _commandCounter++;
                            MainWindow.ListDB.Add(new Commands(_commandCounter, tabNeededTemp + lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
                        }

                        else if (lines.ElementAt(_counter).Contains("# end"))
                        {
                            _commandCounter++;
                            MainWindow.ListDB.Add(new Commands(_commandCounter, lines.ElementAt(_counter).Replace("\t# ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
                            if (MainWindow.sumTrue == 0)
                                throw new Exception("Unvalid Command : No operations exist");
                            else
                            {
                                MainWindow.sumTrue--;
                                MainWindow.tabNeeded = string.Concat(Enumerable.Repeat("\t", MainWindow.sumTrue));
                                MainWindow.inWhileOrIf.Remove(MainWindow.inWhileOrIf.Keys.LastOrDefault());
                            }
                        }

                        _counter++;

                        MainWindow.sumTrue = MainWindow.inWhileOrIf.Count(x => x.Value == true);
                        MainWindow.tabNeeded = string.Concat(Enumerable.Repeat("\t", MainWindow.sumTrue));
                    }
                    MainWindow.TestList.Add(new TestSuit() { TestNumber = MainWindow.testCaseCounter, TestName = _testName, TestValue = new ObservableCollection<Commands>(MainWindow.ListDB), IsSaved = true });
                    _mainWindow.TestCaseCounterTB.Text = Convert.ToString(MainWindow.testCaseCounter);
                    i++;
                    _commandCounter = 1;
                    passed = false;
                    MainWindow.ListDB.Clear();
                }
                MainWindow._testCaseCounter = MainWindow.testCaseCounter;
                _mainWindow.TestCaseListView.ItemsSource = MainWindow.TestList;
                _mainWindow.listView.ItemsSource = MainWindow.ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(MainWindow.ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(MainWindow.TestList);
                view2.Refresh();
            }
        }
        #endregion


    }
}
