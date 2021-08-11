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

namespace FirstTry_app_1
{
    /////// <summary>
    /////// Interaction logic for MainWindow.xaml
    /////// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {

        ///////variables///////
        System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

        public static string ProjectName;
        public static int CommandCounter = 0;
        public static int _commandCounter = 0;
        string TestName;
        public static int testCaseCounter = 0;
        public static int _testCaseCounter = 0;
        public static string sPath;
        public static string _sPath;
        public static string gPath;
        public static string testCaseFileName;
        public static string _testCaseFileName;
        public static string FolderName;
        public static string[] _openFileNameArray;
        public static string _openFileName;
        public static string[] _openFileAddressArray;
        public static string _openFileAddress;
        public static int _openedFiles;
        public static string openTestCaseContent;
        string BaseDiractory = "C:\\Selenium\\";
        public static string CopiedLogItem;
        public static Commands CopiedItem;
        public static Commands SelectedItem;
        public static TestSuit SelectedTest = new TestSuit() { };
        bool edit;
        bool ok = false;
        public static bool TestSuitSaved = false;
        public static bool Continue = false;
        public static bool updateAck = false;
        public static int _testCaseNameCount = 32;
        public static Dictionary<int, bool> inWhileOrIf = new Dictionary<int, bool>();
        //Dictionary<int, bool> inIf = new Dictionary<int, bool>();
        public static int sumTrue = 0;
        public static string tabNeeded = "";
        public static string tabInCommandForEdit = "";
        public static string[,] CommandsList = new string[,]
        {
            {"alert" , "هشدار را تایید کن" } ,
            {"break" , "gg" } ,
            {"clearText" , "gg" } ,
            {"click" , "بر روی المنت target کلیک کن" } ,
            {"close" , "gg" } ,
            {"end" , "gg" } ,
            {"failTest" , "gg" } ,
            {"if" , "gg" } ,
            {"open" , "صفحه target را باز کن" } ,
            {"pause" , "به اندازه مقدار target منتظر بمان" } ,
            {"refresh" , "gg" } ,
            {"replace" , "مقدار را جایگزین کن" } ,
            {"runScript" , "gg" } ,
            {"scrollInto" , "gg" } ,
            {"select" , "از منوی target مقدار value را انتخاب کن" } ,
            {"selectByIndex" , "از منوی target مقدار value را انتخاب کن" } ,
            {"selectByValue" , "از منوی target مقدار value را انتخاب کن" } ,
            {"selectByVisibleText" , "از منوی target مقدار value را انتخاب کن" } ,
            {"sendKeys" , "مقدار value را در المنت target بنویس" } ,
            {"storeElementPresent" , "حضور المنت target را ذخیره کن" } ,
            {"storeEval" , "target را برابر با مقدار value قرار بده" } ,
            {"storeHref" , "پیوند المنت target را ذخیره کن" } ,
            {"storeId" , "شناسه المنت target را ذخیره کن" } ,
            {"storeInnerHTML" , "متن داخلی المنت target را ذخیره کن" } ,
            {"storeName" , "نام المنت target را ذخیره کن" } ,
            {"storeText" , "متن المنت target را ذخیره کن" } ,
            {"storeValue" , "مقدار المنت target را ذخیره کن" } ,
            {"storeWicketPath" , "آدرس ویکت المنت target را ذخیره کن" } ,
            {"switch" , "gg" } ,
            {"switchToDefault" , "gg" } ,
            {"type" , "مقدار value را در المنت target بنویس" } ,
            {"waitForAttribute" , "gg" } ,
            {"waitForElementNotPresent" , "منتظر عدم حضور المنت target باشید" } ,
            {"waitForElementPresent" , "منتظر حضور المنت target باشید" } ,
            {"waitForElementVisible" , "منتظر حضور المنت target باشید" } ,
            {"waitForNotText" , "منتظر عدم حضور متن value در المنت target بمانید" } ,
            {"waitForNumberOfWindowPresent" , "gg" } ,
            {"waitForText" , "منتظر حضور متن value در المنت target بمانید" } ,
            {"waitForValue" , "منتظر حضور مقدار value در المنت target بمانید" } ,
            {"waitForWindowPresent" , "gg" } ,
            {"while" , "gg" }
        };

        int index;
        bool handleRightClickBugListview = true;
        bool handleRightClickBugTestlist = true;
        enum Multiplication { copy,cut };
        Multiplication multiplication;


        Commands CurrentCommand;
        public TestSuit CurrentTestCase = new TestSuit { };

        public static ObservableCollection<Commands> ListDB = new ObservableCollection<Commands>();
        public static ObservableCollection<TestSuit> TestList = new ObservableCollection<TestSuit>();

        public Commands Command { get; private set; }

        sealed class ViewModel : INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            void SetField<X>(ref X field, X value, [CallerMemberName] string propertyCommand = null)
            {
                if (EqualityComparer<X>.Default.Equals(field, value)) return;

                field = value;

                var h = PropertyChanged;
                if (h != null) h(this, new PropertyChangedEventArgs(propertyCommand));
            }
            #endregion

            long? selectedValue;
            public long? SelectedValue
            {
                get { return selectedValue; }
                set { SetField(ref selectedValue, value); }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            MainGrid.Focus();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //listView.AddHandler(Thumb.DragDeltaEvent, new DragDeltaEventHandler(Thumb_DragDelta), true);
            TestCaseListView.ItemsSource = TestList;
            listView.ItemsSource = ListDB;
            //listView.DataContext = ListDB;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TestCaseListView.ItemsSource);
            view.Filter = UserFilter;
            Refrence.Text = "  Selenium WebDriver IDE\n  Version : 0.106";

        }

        ///////BUILD
        #region Save
        public void TestMethod(string A)
        {
            if (A != null)
            {
                int _counter = 0;
                //FileInfo file = new FileInfo(@A);
                //file.Create();
                //StreamWriter TestFile = file.CreateText();
                string mainString_testCase = File.ReadAllText(@"StaticCode.py");
                int mainStringSplitterIndex = mainString_testCase.IndexOf("durationError");
                string mainString = mainString_testCase.Substring(0, mainStringSplitterIndex);
                mainString += "\r\n\r\nclass StoreEvalDB:\r\n\tvars = {}\r\n";
                mainString += "\nclass " + TestList.ElementAt(_testCaseCounter - 1).TestName + ":\n";
                string tabNeededTemp = "";
                while (CommandCounter > _counter)
                {
                    needCotBefore = needCotAfter = true;

                    ///////specifyTarget
                    string targetType = "";
                    if (ListDB.ElementAt(_counter).Target.Contains('=') && !ListDB.ElementAt(_counter).Target.Contains("//"))
                        targetType = ListDB.ElementAt(_counter).Target.Split('=')[0];
                    else if (!ListDB.ElementAt(_counter).Target.Contains('=') || ListDB.ElementAt(_counter).Target.Contains("//"))
                        targetType = "xpath";
                    string tempTarget = "";
                    switch (targetType)
                    {
                        case "class":
                            targetType = "class name";
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("class=", "");
                            break;
                        case "css":
                            targetType = "css selector";
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                            break;
                        case "id":
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("id=", "");
                            break;
                        case "link":
                            targetType = "link text";
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                            break;
                        case "linkText":
                            targetType = "link text";
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                            break;
                        case "name":
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("name=", "");
                            break;
                        case "partial":
                            targetType = "partial link text";
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("partial=", "");
                            break;
                        case "tag":
                            targetType = "tag name";
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("tag=", "");
                            break;
                        case "xpath":
                            tempTarget = ListDB.ElementAt(_counter).Target.Replace("xpath=", "");
                            break;
                        default:
                            tempTarget = ListDB.ElementAt(_counter).Target;
                            break;
                    }
                    string tempCommand = ListDB.ElementAt(_counter).Command.Replace("\t", "").Replace(" ", "");
                    tabNeededTemp = ListDB.ElementAt(_counter).Command.Replace(tempCommand, "");
                    switch (tempCommand)
                    {
                        #region ===> open
                        case "open":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | open\n";
                            string _open = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.get(" + ConvertTextToIdeFormat(tempTarget, false, true) + ")\n";
                            mainString += _open;
                            mainString += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            break;
                        #endregion

                        #region ===> waitForElementPresent
                        case "waitForElementPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementPresent\n";
                            string _wait = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _wait += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _wait += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait;
                            break;
                        #endregion

                        #region ===> waitForElementVisible
                        case "waitForElementVisible":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementVisible\n";
                            string _wait3 = tabNeededTemp + "\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait3 += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _wait3 += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _wait3 += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait3;
                            break;
                        #endregion

                        #region ===> waitForElementNotPresent
                        case "waitForElementNotPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                            string _wait2 = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait2 += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait2;
                            break;
                        #endregion

                        #region ===> waitForNotText
                        case "waitForNotText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForNotText\n";
                            string _waitForNotText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until_not(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                            _waitForNotText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForNotText;
                            break;
                        #endregion

                        #region ===> waitForText
                        case "waitForText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForText\n";
                            string _waitForText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                            _waitForText += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForText += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForText;
                            break;
                        #endregion

                        #region ===> waitForValue
                        case "waitForValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForValue\n";
                            string _waitForValue = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element_value((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                            _waitForValue += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForValue += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForValue += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForValue;
                            break;
                        #endregion

                        #region ===> waitForAttribute
                        case "waitForAttribute":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForAttribute\n";
                            string _waitForAttribute = tabNeededTemp + "\tfor i in range(30):\n";
                            _waitForAttribute += tabNeededTemp + "\t\ttry:\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\tif \"active\" in att.get_attribute(\"class\"):\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t\tbreak\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\telif i == 29:\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t\traise Exception(\"Element is not active\")\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\ttime.sleep(0.5)\n";
                            _waitForAttribute += tabNeededTemp + "\t\texcept StaleElementReferenceException:\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\tcontinue\n";
                            _waitForAttribute += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForAttribute;
                            break;
                        #endregion

                        #region ===> waitForWindowPresent
                        case "waitForWindowPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForWindowPresent\n";
                            string _waitForWindowPresent = tabNeededTemp + "\twaitForWindowPresent10 = WebDriverWait(driver, 10).until(EC.new_window_is_opened())\n";
                            mainString += _waitForWindowPresent;
                            break;
                        #endregion

                        #region ===> waitForNumberOfWindowPresent
                        case "waitForNumberOfWindowPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForNumberOfWindowPresent\n";
                            string _waitForNumberOfWindowPresent = tabNeededTemp + "\tWebDriverWait(driver, 10).until(EC.number_of_windows_to_be(1))\n";
                            mainString += _waitForNumberOfWindowPresent;
                            break;
                        #endregion

                        #region ===> type
                        case "type":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | type\n";
                            string _type = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _type += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _type += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                            _type += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, false, true) + ")\n";
                            _type += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _type;
                            break;
                        #endregion

                        #region ===> sendKeys
                        case "sendKeys":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | sendKeys\n";
                            string _sendKeys = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _sendKeys += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _sendKeys += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, false, true) + ")\n";
                            _sendKeys += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _sendKeys;
                            break;
                        #endregion

                        #region ===> clearText
                        case "clearText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | clearText\n";
                            string _clearText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _clearText += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _clearText += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                            _clearText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _clearText;
                            break;
                        #endregion

                        #region ===> click
                        case "click":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | click\n";
                            string _click = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _click += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _click += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                            _click += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _click;
                            break;
                        #endregion

                        #region ===> select
                        case "select1":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | select\n";
                            string tempLabel = ListDB.ElementAt(_counter).Value;
                            if (tempLabel.Contains("label="))
                                tempLabel = tempLabel.Replace("label=", "");
                            string _select = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _select += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _select += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_" + targetType.Replace(" ", "_") + "(\"option[. = '" + ConvertTextToIdeFormat(tempLabel, false, true) + "']\")\n";
                            _select += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                            _select += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                            _select += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _select;
                            break;
                        #endregion

                        #region ===> selectByVisibleText
                        case "select":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | selectByVisibleText\n";
                            string tempLabel1 = ListDB.ElementAt(_counter).Value;
                            if (tempLabel1.Contains("label="))
                                tempLabel1 = tempLabel1.Replace("label=", "");
                            string _selectByVisibleText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByVisibleText += tabNeededTemp + "\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByVisibleText += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".select_by_visible_text(" + ConvertTextToIdeFormat(tempLabel1, false, true) + ")\n";
                            _selectByVisibleText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _selectByVisibleText;
                            break;
                        #endregion

                        #region ===> selectByValue
                        case "selectByValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | selectByValue\n";
                            string tempLabel2 = ListDB.ElementAt(_counter).Value;
                            if (tempLabel2.Contains("label="))
                                tempLabel2 = tempLabel2.Replace("label=", "");
                            string _selectByValue = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByValue += tabNeededTemp + "\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByValue += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".select_by_value(" + ConvertTextToIdeFormat(tempLabel2, false, true) + ")\n";
                            _selectByValue += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _selectByValue;
                            break;
                        #endregion

                        #region ===> selectByIndex
                        case "selectByIndex":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | selectByIndex\n";
                            string _selectByIndex = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByIndex += tabNeededTemp + "\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByIndex += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".select_by_index(" + ListDB.ElementAt(_counter).Value + ")\n";
                            _selectByIndex += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _selectByIndex;
                            break;
                        #endregion

                        #region ===> storeText
                        case "storeText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeText\n";
                            string _storeText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeText += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                            _storeText += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                            _storeText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeText;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                            break;
                        #endregion

                        #region ===> storeValue
                        case "storeValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeValue\n";
                            string _storeValue = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeValue += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeValue += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeValue += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeValue;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                            break;
                        #endregion

                        #region ===> storeWicketPath
                        case "storeWicketPath":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeWicketPath\n";
                            string _storeWicketPath = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeWicketPath += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeWicketPath += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeWicketPath += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeWicketPath;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"wicketpath\")");
                            break;
                        #endregion

                        #region ===> storeInnerHTML
                        case "storeInnerHTML":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeInnerHTML\n";
                            string _storeInnerHTML = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeInnerHTML += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeInnerHTML += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeInnerHTML += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeInnerHTML;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"innerHTML\")");
                            break;
                        #endregion

                        #region ===> storeName
                        case "storeName":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeName\n";
                            string _storeName = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeName += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeName += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeName += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeName;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"name\")");
                            break;
                        #endregion

                        #region ===> storeId
                        case "storeId":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeId\n";
                            string _storeId = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeId += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeId += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeId += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeId;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"id\")");
                            break;
                        #endregion

                        #region ===> storeHref
                        case "storeHref":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeHref\n";
                            string _storeHref = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeHref += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeHref += tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeHref += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeHref;
                            Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"href\")");
                            break;
                        #endregion

                        #region ===> storeEval
                        case "storeEval":
                            needCotBefore = needCotAfter = false;
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeEval\n";
                            string _storeEval = tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ConvertTextToIdeFormat(tempTarget, false, true) + "\n";
                            _storeEval += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeEval;
                            Stored.Items.Add(_storeEval);
                            break;
                        #endregion

                        #region ===> storeElementPresent
                        case "storeElementPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeElementPresent\n";
                            string _storeElementPresent = tabNeededTemp + "\ttry:\n";
                            _storeElementPresent += tabNeededTemp + "\t\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _storeElementPresent += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = True\n";
                            _storeElementPresent += tabNeededTemp + "\texcept TimeoutException:\n";
                            _storeElementPresent += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = False\n";
                            _storeElementPresent += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeElementPresent;
                            Stored.Items.Add(_storeElementPresent);
                            break;
                        #endregion

                        #region ===> alert
                        case "alert":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | alert\n";
                            string _alert = tabNeededTemp + "\talert" + _counter + " = driver.switch_to_alert()\n";
                            _alert += tabNeededTemp + "\talert" + _counter + ".accept()\n";
                            _alert += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _alert;
                            break;
                        #endregion

                        #region ===> replace
                        case "replace":
                            needCotBefore = needCotAfter = false;
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | replace\n";
                            string _replace = tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).VariableName + "\"] = " + ConvertTextToIdeFormat(tempTarget, false, true) + "\n";
                            _replace += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _replace;
                            Stored.Items.Add(_replace);
                            break;
                        #endregion

                        #region ===> runScript
                        case "runScript":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | runScript\n";
                            string _runScript = tabNeededTemp + "\tdriver.execute_script(\"" + ListDB.ElementAt(_counter).Value;
                            _runScript += tempTarget == "" || tempTarget == "None" ? "\")\n" : "\", driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _runScript += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _runScript;
                            break;
                        #endregion

                        #region ===> switch
                        case "switch":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | switch\n";
                            string _switch = tabNeededTemp + "\tdriver.switch_to." + tempTarget + "(" + ListDB.ElementAt(_counter).Value + ")\n";
                            _switch += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _switch;
                            break;
                        #endregion

                        #region ===> switchToDefault
                        case "switchToDefault":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | switchToDefault\n";
                            string _switchToDefault = tabNeededTemp + "\tdriver.switch_to.default_content()\n";
                            _switchToDefault += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _switchToDefault;
                            break;
                        #endregion

                        #region ===> scrollInto
                        case "scrollInto":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | scrollInto\n";
                            string _scrollInto = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _scrollInto += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + ".location_once_scrolled_into_view\n";
                            _scrollInto += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _scrollInto;
                            break;
                        #endregion

                        #region ===> while
                        case "while":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | while\n";
                            string _while = tabNeededTemp + "\twhile " + ListDB.ElementAt(_counter).Target + ":\n";
                            _while += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _while;
                            break;
                        #endregion

                        #region ===> break
                        case "break":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | break\n";
                            string _break = tabNeededTemp + "\tbreak\n";
                            _break += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _break;
                            break;
                        #endregion

                        #region ===> if
                        case "if":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | if\n";
                            string _if = tabNeededTemp + "\tif " + ListDB.ElementAt(_counter).Target + ":\n";
                            _if += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _if;
                            break;
                        #endregion

                        #region ===> end
                        case "end":
                            string _end = tabNeededTemp + "\t# end\n";
                            mainString += _end;
                            break;
                        #endregion

                        #region ===> refresh
                        case "refresh":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | refresh\n";
                            string _refresh = tabNeededTemp + "\tdriver.refresh()\n";
                            _refresh += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _refresh;
                            break;
                        #endregion

                        #region ===> close
                        case "close":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | close\n";
                            string _close = tabNeededTemp + "\tdriver.close()\n";
                            _close += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _close;
                            break;
                        #endregion

                        #region ===> failTest
                        case "failTest":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | failTest\n";
                            string _failTest = tabNeededTemp + "\traise Exception(\"You failed the test intentionally\")\n";
                            _failTest += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _failTest;
                            break;
                        #endregion

                        #region ===> pause
                        case "pause":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | pause\n";
                            int tempTarget1 = Convert.ToInt16(tempTarget) / 1000;
                            string _pause = tabNeededTemp + "\ttime.sleep(" + Convert.ToString(tempTarget1) + ")\n";
                            _pause += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _pause;
                            break;
                            #endregion
                    }
                    _counter++;
                }
                //var sumTrueWhiles = inWhile.Values.Count(true);
                //TestFile.WriteLine(mainString);
                //TestFile.Close();
                System.IO.File.WriteAllText(@A, mainString.Replace(" 	", "	"), System.Text.Encoding.UTF8);

            }
        }

        ///////

        public void TestSuitMethod(string B)
        {
            if (B != null)
            {
                int _counter1 = 0;
                int _counter = 0;
                //FileInfo file = new FileInfo(@B);
                //StreamWriter TestFile = file.CreateText();
                string StaticCodeNew = File.ReadAllText(@"StaticCode.py").Replace("    ", "\t").Replace("testSuit", ProjectName).Replace("tableWidth", (_testCaseNameCount).ToString());
                int splitterIndex = StaticCodeNew.IndexOf("#bodyCode#");
                string mainString = StaticCodeNew.Substring(0, splitterIndex);
                string mainString_Last = StaticCodeNew.Remove(0, splitterIndex + 11);
                string tabNeededTemp = "";

                while (testCaseCounter > _counter1)
                {

                    var obj = TestList.FirstOrDefault(x => x.TestNumber == _counter1 + 1);
                    if (obj != null)
                    {
                        ListDB = new ObservableCollection<Commands>(obj.TestValue);
                        CommandCounter = obj.TestValue.Count;
                    }
                    mainString += "\n\tclass " + TestList.ElementAt(_counter1).TestName + ":\n";
                    while (CommandCounter > _counter)
                    {
                        needCotBefore = needCotAfter = true;

                        ///////specifyTarget
                        string targetType = "";
                        if (ListDB.ElementAt(_counter).Target.Contains('=') && !ListDB.ElementAt(_counter).Target.Contains("//"))
                            targetType = ListDB.ElementAt(_counter).Target.Split('=')[0];
                        else if (!ListDB.ElementAt(_counter).Target.Contains('=') || ListDB.ElementAt(_counter).Target.Contains("//"))
                            targetType = "xpath";
                        string tempTarget = "";
                        switch (targetType)
                        {
                            case "class":
                                targetType = "class name";
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("class=", "");
                                break;
                            case "css":
                                targetType = "css selector";
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                break;
                            case "id":
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("id=", "");
                                break;
                            case "link":
                                targetType = "link text";
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                break;
                            case "linkText":
                                targetType = "link text";
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                break;
                            case "name":
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("name=", "");
                                break;
                            case "partial":
                                targetType = "partial link text";
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("partial=", "");
                                break;
                            case "tag":
                                targetType = "tag name";
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("tag=", "");
                                break;
                            case "xpath":
                                tempTarget = ListDB.ElementAt(_counter).Target.Replace("xpath=", "");
                                break;
                            default:
                                tempTarget = ListDB.ElementAt(_counter).Target;
                                break;
                        }
                        string tempCommand = ListDB.ElementAt(_counter).Command.Replace("\t", "").Replace(" ", "");
                        tabNeededTemp = ListDB.ElementAt(_counter).Command.Replace(tempCommand, "");
                        switch (tempCommand)
                        {
                            #region ===> open
                            case "open":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | open\n";
                                string _open = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.get(" + ConvertTextToIdeFormat(tempTarget, false, true) + ")\n";
                                mainString += _open;
                                mainString += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                break;
                            #endregion

                            #region ===> waitForElementPresent
                            case "waitForElementPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementPresent\n";
                                string _wait = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _wait += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _wait += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait;
                                break;
                            #endregion

                            #region ===> waitForElementVisible
                            case "waitForElementVisible":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementVisible\n";
                                string _wait3 = tabNeededTemp + "\t\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait3 += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _wait3 += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _wait3 += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait3;
                                break;
                            #endregion

                            #region ===> waitForElementNotPresent
                            case "waitForElementNotPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                                string _wait2 = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait2 += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait2;
                                break;
                            #endregion

                            #region ===> waitForNotText
                            case "waitForNotText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForNotText\n";
                                string _waitForNotText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until_not(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                                _waitForNotText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForNotText;
                                break;
                            #endregion

                            #region ===> waitForText
                            case "waitForText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForText\n";
                                string _waitForText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                                _waitForText += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForText += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForText;
                                break;
                            #endregion

                            #region ===> waitForValue
                            case "waitForValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForValue\n";
                                string _waitForValue = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element_value((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                                _waitForValue += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForValue += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForValue += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForValue;
                                break;
                            #endregion

                            #region ===> waitForAttribute
                            case "waitForAttribute":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForAttribute\n";
                                string _waitForAttribute = tabNeededTemp + "\t\tfor i in range(30):\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\ttry:\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\tif \"active\" in att.get_attribute(\"class\"):\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t\tbreak\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\telif i == 29:\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t\traise Exception(\"Element is not active\")\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\ttime.sleep(0.5)\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\texcept StaleElementReferenceException:\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\tcontinue\n";
                                _waitForAttribute += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForAttribute;
                                break;
                            #endregion

                            #region ===> waitForWindowPresent
                            case "waitForWindowPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForWindowPresent\n";
                                string _waitForWindowPresent = tabNeededTemp + "\t\twaitForWindowPresent10 = WebDriverWait(driver, 10).until(EC.new_window_is_opened())\n";
                                mainString += _waitForWindowPresent;
                                break;
                            #endregion

                            #region ===> waitForNumberOfWindowPresent
                            case "waitForNumberOfWindowPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForNumberOfWindowPresent\n";
                                string _waitForNumberOfWindowPresent = tabNeededTemp + "\t\tWebDriverWait(driver, 10).until(EC.number_of_windows_to_be(1))\n";
                                mainString += _waitForNumberOfWindowPresent;
                                break;
                            #endregion

                            #region ===> type
                            case "type":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | type\n";
                                string _type = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _type += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, false, true) + ")\n";
                                _type += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _type;
                                break;
                            #endregion

                            #region ===> sendKeys
                            case "sendKeys":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | sendKeys\n";
                                string _sendKeys = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _sendKeys += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _sendKeys += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, false, true) + ")\n";
                                _sendKeys += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _sendKeys;
                                break;
                            #endregion

                            #region ===> clearText
                            case "clearText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | clearText\n";
                                string _clearText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _clearText += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _clearText += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _clearText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _clearText;
                                break;
                            #endregion

                            #region ===> click
                            case "click":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | click\n";
                                string _click = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _click += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                _click += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _click;
                                break;
                            #endregion

                            #region ===> select
                            case "select1":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | select\n";
                                string tempLabel = ListDB.ElementAt(_counter).Value;
                                if (tempLabel.Contains("label="))
                                    tempLabel = tempLabel.Replace("label=", "");
                                string _select = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _select += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_" + targetType.Replace(" ", "_") + "(\"option[. = '" + ConvertTextToIdeFormat(tempLabel, false, true) + "']\")\n";
                                _select += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                _select += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _select;
                                break;
                            #endregion

                            #region ===> selectByVisibleText
                            case "select":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | selectByVisibleText\n";
                                string tempLabel1 = ListDB.ElementAt(_counter).Value;
                                if (tempLabel1.Contains("label="))
                                    tempLabel1 = tempLabel1.Replace("label=", "");
                                string _selectByVisibleText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByVisibleText += tabNeededTemp + "\t\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByVisibleText += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".select_by_visible_text(" + ConvertTextToIdeFormat(tempLabel1, false, true) + ")\n";
                                _selectByVisibleText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _selectByVisibleText;
                                break;
                            #endregion

                            #region ===> selectByValue
                            case "selectByValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | selectByValue\n";
                                string tempLabel2 = ListDB.ElementAt(_counter).Value;
                                if (tempLabel2.Contains("label="))
                                    tempLabel2 = tempLabel2.Replace("label=", "");
                                string _selectByValue = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByValue += tabNeededTemp + "\t\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByValue += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".select_by_value(" + ConvertTextToIdeFormat(tempLabel2, false, true) + ")\n";
                                _selectByValue += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _selectByValue;
                                break;
                            #endregion

                            #region ===> selectByIndex
                            case "selectByIndex":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | selectByIndex\n";
                                string _selectByIndex = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByIndex += tabNeededTemp + "\t\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByIndex += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".select_by_index(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _selectByIndex += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _selectByIndex;
                                break;
                            #endregion

                            #region ===> storeText
                            case "storeText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeText\n";
                                string _storeText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeText += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                _storeText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeText;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                                break;
                            #endregion

                            #region ===> storeValue
                            case "storeValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeValue\n";
                                string _storeValue = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeValue += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeValue += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeValue += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeValue;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                                break;
                            #endregion

                            #region ===> storeWicketPath
                            case "storeWicketPath":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeWicketPath\n";
                                string _storeWicketPath = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeWicketPath += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeWicketPath += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeWicketPath += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeWicketPath;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"wicketpath\")");
                                break;
                            #endregion

                            #region ===> storeInnerHTML
                            case "storeInnerHTML":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeInnerHTML\n";
                                string _storeInnerHTML = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeInnerHTML += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeInnerHTML += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeInnerHTML += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeInnerHTML;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"innerHTML\")");
                                break;
                            #endregion

                            #region ===> storeName
                            case "storeName":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeName\n";
                                string _storeName = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeName += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeName += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeName += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeName;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"name\")");
                                break;
                            #endregion

                            #region ===> storeId
                            case "storeId":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeId\n";
                                string _storeId = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeId += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeId += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeId += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeId;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"id\")");
                                break;
                            #endregion

                            #region ===> storeHref
                            case "storeHref":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeHref\n";
                                string _storeHref = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeHref += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeHref += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeHref += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeHref;
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"href\")");
                                break;
                            #endregion

                            #region ===> storeEval
                            case "storeEval":
                                needCotBefore = needCotAfter = false;
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeEval\n";
                                string _storeEval = tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ConvertTextToIdeFormat(tempTarget, false, true) + "\n";
                                _storeEval += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeEval;
                                Stored.Items.Add(_storeEval);
                                break;
                            #endregion

                            #region ===> storeElementPresent
                            case "storeElementPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeElementPresent\n";
                                string _storeElementPresent = tabNeededTemp + "\t\ttry:\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = True\n";
                                _storeElementPresent += tabNeededTemp + "\t\texcept TimeoutException:\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = False\n";
                                _storeElementPresent += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeElementPresent;
                                Stored.Items.Add(_storeElementPresent);
                                break;
                            #endregion

                            #region ===> alert
                            case "alert":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | alert\n";
                                string _alert = tabNeededTemp + "\t\talert" + _counter + " = driver.switch_to_alert()\n";
                                _alert += tabNeededTemp + "\t\talert" + _counter + ".accept()\n";
                                _alert += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _alert;
                                break;
                            #endregion

                            #region ===> replace
                            case "replace":
                                needCotBefore = needCotAfter = false;
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | replace\n";
                                string _replace = tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).VariableName + "\"] = " + ConvertTextToIdeFormat(tempTarget, false, true) + "\n";
                                _replace += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _replace;
                                Stored.Items.Add(_replace);
                                break;
                            #endregion

                            #region ===> runScript
                            case "runScript":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | runScript\n";
                                string _runScript = tabNeededTemp + "\t\tdriver.execute_script(\"" + ListDB.ElementAt(_counter).Value;
                                _runScript += tempTarget == "" || tempTarget == "None" ? "\")\n" : "\", driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _runScript += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _runScript;
                                break;
                            #endregion

                            #region ===> switch
                            case "switch":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | switch\n";
                                string _switch = tabNeededTemp + "\t\tdriver.switch_to." + tempTarget + "(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _switch += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _switch;
                                break;
                            #endregion

                            #region ===> switchToDefault
                            case "switchToDefault":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | switchToDefault\n";
                                string _switchToDefault = tabNeededTemp + "\t\tdriver.switch_to.default_content()\n";
                                _switchToDefault += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _switchToDefault;
                                break;
                            #endregion

                            #region ===> scrollInto
                            case "scrollInto":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | scrollInto\n";
                                string _scrollInto = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _scrollInto += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + ".location_once_scrolled_into_view\n";
                                _scrollInto += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _scrollInto;
                                break;
                            #endregion

                            #region ===> while
                            case "while":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | while\n";
                                string _while = tabNeededTemp + "\t\twhile " + ListDB.ElementAt(_counter).Target + ":\n";
                                _while += tabNeededTemp + "\t\t\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _while;
                                break;
                            #endregion

                            #region ===> break
                            case "break":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | break\n";
                                string _break = tabNeededTemp + "\t\tbreak\n";
                                _break += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _break;
                                break;
                            #endregion

                            #region ===> if
                            case "if":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | if\n";
                                string _if = tabNeededTemp + "\t\tif " + ListDB.ElementAt(_counter).Target + ":\n";
                                _if += tabNeededTemp + "\t\t\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _if;
                                break;
                            #endregion

                            #region ===> end
                            case "end":
                                string _end = tabNeededTemp + "\t\t# end\n";
                                mainString += _end;
                                break;
                            #endregion

                            #region ===> refresh
                            case "refresh":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | refresh\n";
                                string _refresh = tabNeededTemp + "\t\tdriver.refresh()\n";
                                _refresh += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _refresh;
                                break;
                            #endregion

                            #region ===> close
                            case "close":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | close\n";
                                string _close = tabNeededTemp + "\t\tdriver.close()\n";
                                _close += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _close;
                                break;
                            #endregion

                            #region ===> failTest
                            case "failTest":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | failTest\n";
                                string _failTest = tabNeededTemp + "\t\traise Exception(\"You failed the test intentionally\")\n";
                                _failTest += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _failTest;
                                break;
                            #endregion

                            #region ===> pause
                            case "pause":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | pause\n";
                                int tempTarget1 = Convert.ToInt16(tempTarget) / 1000;
                                string _pause = tabNeededTemp + "\t\ttime.sleep(" + Convert.ToString(tempTarget1) + ")\n";
                                _pause += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _pause;
                                break;
                                #endregion
                        }
                        _counter++;
                    }
                    _counter1++;
                    _counter = 0;
                }
                mainString += mainString_Last;
                //TestFile.WriteLine(mainString);
                //TestFile.Close();
                System.IO.File.WriteAllText(@B, mainString.Replace(" 	", "	"), System.Text.Encoding.UTF8);

                TestSuitSaved = true;
            }
        }
        #endregion


        #region Open

        public void OpenTestCase(string C)
        {
            if (C != null)
            {
                _commandCounter = CommandCounter = 0;
                CommandCounterTB.Text = Convert.ToString(CommandCounter);
                testCaseCounter++;
                TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                _testCaseCounter = testCaseCounter;
                //int _commandCounter = 0;
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
                ListDB.Clear();

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
                            outType = FindBetween(input, "present_in_element_value((\"", "\", \"");
                            outValue = FindBetween(input, "\", \"", "\"), ");
                        }
                        else if (input.Contains("present_in_element"))
                        {
                            outType = FindBetween(input, "present_in_element((\"", "\", \"");
                            outValue = FindBetween(input, "\", \"", "\"), ");
                        }
                        else if (input.Contains("element_located"))
                        {
                            outType = FindBetween(input, "element_located((\"", "\", \"");
                            outValue = FindBetween(input, "\", \"", "\")))");
                        }
                        else if (input.Contains("elements_located"))
                        {
                            outType = FindBetween(input, "elements_located((\"", "\", \"");
                            outValue = FindBetween(input, "\", \"", "\")))");
                        }
                        else if (input.Contains("find_element_by"))
                        {
                            outType = FindBetween(input, "find_element_by_", "(\"");
                            outValue = FindBetween(input, "find_element_by_" + outType + "(\"", "\")");
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
                                _currentTarget = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "driver.get(", ")"));
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
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "\"), ", "))"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForNotText
                            case "waitForNotText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> waitForValue
                            case "waitForValue":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
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
                                _currentTarget = FindBetween(lines.ElementAt(_counter + 1), "number_of_windows_to_be(", "))");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> type
                            case "type":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 4), ".send_keys(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 5).Remove(0, lines.ElementAt(_counter + 5).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> sendKeys
                            case "sendKeys":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), ".send_keys(", ")"));
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
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "//option[. = ", "]\")"));
                                _currentDescription = lines.ElementAt(_counter + 6).Remove(0, lines.ElementAt(_counter + 6).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> selectByVisibleText
                            case "selectByVisibleText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "select_by_visible_text(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> selectByValue
                            case "selectByValue":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "select_by_value(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> selectByIndex
                            case "selectByIndex":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "select_by_index(", ")"));
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeText
                            case "storeText":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeValue
                            case "storeValue":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeWicketPath
                            case "storeWicketPath":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeInnerHTML
                            case "storeInnerHTML":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeName
                            case "storeName":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeId
                            case "storeId":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeHref
                            case "storeHref":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeEval
                            case "storeEval":
                                _currentTarget = ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> storeElementPresent
                            case "storeElementPresent":
                                _currentTarget = specifyTarget(lines.ElementAt(_counter + 2));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
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
                                _currentTarget = ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                _currentValue = FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
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
                                        _currentValue = FindBetween(lines.ElementAt(_counter + 1), "execute_script(\"", "\", ");
                                    if (lines.ElementAt(_counter + 1).Contains("execute_script('"))
                                        _currentValue = FindBetween(lines.ElementAt(_counter + 1), "execute_script('", "', ");
                                    _currentTarget = temprunScript;
                                }
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                            #endregion

                            #region ===> switch
                            case "switch":
                                _currentTarget = FindBetween(lines.ElementAt(_counter + 1), ".switch_to.", "(");
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
                                inWhileOrIf.Add(_commandCounter, true);
                                _currentTarget = FindBetween(lines.ElementAt(_counter + 1), "while ", ":");
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
                                inWhileOrIf.Add(_commandCounter, true);
                                _currentTarget = FindBetween(lines.ElementAt(_counter + 1), "if ", ":");
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
                                _currentTarget = (Convert.ToInt16(FindBetween(lines.ElementAt(_counter + 1), "time.sleep(", ")")) * 1000).ToString();
                                _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                break;
                                #endregion
                        }
                        _commandCounter++;
                        ListDB.Add(new Commands(_commandCounter, tabNeededTemp + lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2), _currentTarget, _currentValue, _currentVariableName, _currentDescription));
                    }

                    else if (lines.ElementAt(_counter).Contains("# end"))
                    {
                        _commandCounter++;
                        ListDB.Add(new Commands(_commandCounter, lines.ElementAt(_counter).Replace("\t# ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription));
                        if (sumTrue == 0)
                            throw new Exception("Unvalid Command : No operations exist");
                        else
                        {
                            sumTrue--;
                            tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));
                            inWhileOrIf.Remove(inWhileOrIf.Keys.LastOrDefault());
                        }
                    }

                    _counter++;

                    sumTrue = inWhileOrIf.Count(x => x.Value == true);
                    tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));
                }

                sumTrue = inWhileOrIf.Count(x => x.Value == true);
                tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));

                _commandCounter = CommandCounter = _commandCounter;
                CommandCounterTB.Text = Convert.ToString(CommandCounter);
                TestList.Add(new TestSuit() { TestNumber = testCaseCounter, TestName = _openFileName, TestValue = new ObservableCollection<Commands>(ListDB), TestFolder = FolderName, SavedPath = C, IsSaved = true });
                TestCaseListView.ItemsSource = TestList;
                listView.ItemsSource = ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                view2.Refresh();
            }
        }

        ///////

        public void OpenTestSuit(string D)
        {
            if (D != null)
            {
                ListDB.Clear();
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
                            _testName = FindBetween(lines.ElementAt(_counter), "class ", ":");
                            passed = true;
                            _counter++;
                            testCaseCounter++;
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
                                outType = FindBetween(input, "present_in_element_value((\"", "\", \"");
                                outValue = FindBetween(input, "\", \"", "\"), ");
                            }
                            else if (input.Contains("present_in_element"))
                            {
                                outType = FindBetween(input, "present_in_element((\"", "\", \"");
                                outValue = FindBetween(input, "\", \"", "\"), ");
                            }
                            else if (input.Contains("element_located"))
                            {
                                outType = FindBetween(input, "element_located((\"", "\", \"");
                                outValue = FindBetween(input, "\", \"", "\")))");
                            }
                            else if (input.Contains("elements_located"))
                            {
                                outType = FindBetween(input, "elements_located((\"", "\", \"");
                                outValue = FindBetween(input, "\", \"", "\")))");
                            }
                            else if (input.Contains("find_element_by"))
                            {
                                outType = FindBetween(input, "find_element_by_", "(\"");
                                outValue = FindBetween(input, "find_element_by_" + outType + "(\"", "\")");
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
                                    _currentTarget = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "driver.get(", ")"));
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
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "\"), ", "))"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForNotText
                                case "waitForNotText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> waitForValue
                                case "waitForValue":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 1), "), ", "))"));
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
                                    _currentTarget = FindBetween(lines.ElementAt(_counter + 1), "number_of_windows_to_be(", "))");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> type
                                case "type":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 4), ".send_keys(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 5).Remove(0, lines.ElementAt(_counter + 5).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> sendKeys
                                case "sendKeys":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), ".send_keys(", ")"));
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
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "//option[. = ", "]\")"));
                                    _currentDescription = lines.ElementAt(_counter + 6).Remove(0, lines.ElementAt(_counter + 6).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> selectByVisibleText
                                case "selectByVisibleText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "select_by_visible_text(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> selectByValue
                                case "selectByValue":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "select_by_value(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> selectByIndex
                                case "selectByIndex":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = ConvertTextFromIdeFormat(FindBetween(lines.ElementAt(_counter + 3), "select_by_index(", ")"));
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeText
                                case "storeText":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeValue
                                case "storeValue":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeWicketPath
                                case "storeWicketPath":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeInnerHTML
                                case "storeInnerHTML":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeName
                                case "storeName":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeId
                                case "storeId":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeHref
                                case "storeHref":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 1));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 4).Remove(0, lines.ElementAt(_counter + 4).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeEval
                                case "storeEval":
                                    _currentTarget = ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> storeElementPresent
                                case "storeElementPresent":
                                    _currentTarget = specifyTarget(lines.ElementAt(_counter + 2));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 3), "StoreEvalDB.vars[\"", "\"] = ");
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
                                    _currentTarget = ConvertTextFromIdeFormat(lines.ElementAt(_counter + 1).Remove(0, lines.ElementAt(_counter + 1).IndexOf("\"] = ") + 5));
                                    _currentValue = FindBetween(lines.ElementAt(_counter + 1), "StoreEvalDB.vars[\"", "\"] = ");
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
                                            _currentValue = FindBetween(lines.ElementAt(_counter + 1), "execute_script(\"", "\", ");
                                        if (lines.ElementAt(_counter + 1).Contains("execute_script('"))
                                            _currentValue = FindBetween(lines.ElementAt(_counter + 1), "execute_script('", "', ");
                                        _currentTarget = temprunScript;
                                    }
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                #endregion

                                #region ===> switch
                                case "switch":
                                    _currentTarget = FindBetween(lines.ElementAt(_counter + 1), ".switch_to.", "(");
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
                                    inWhileOrIf.Add(_commandCounter, true);
                                    _currentTarget = FindBetween(lines.ElementAt(_counter + 1), "while ", ":");
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
                                    inWhileOrIf.Add(_commandCounter, true);
                                    _currentTarget = FindBetween(lines.ElementAt(_counter + 1), "if ", ":");
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
                                    _currentTarget = (Convert.ToInt16(FindBetween(lines.ElementAt(_counter + 1), "time.sleep(", ")")) * 1000).ToString();
                                    _currentDescription = lines.ElementAt(_counter + 2).Remove(0, lines.ElementAt(_counter + 2).IndexOf("Description: ") + 13);
                                    break;
                                    #endregion
                            }
                            _commandCounter++;
                            ListDB.Add(new Commands(_commandCounter, tabNeededTemp + lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2), _currentTarget, _currentValue, _currentVariableName, _currentDescription));
                        }

                        else if (lines.ElementAt(_counter).Contains("# end"))
                        {
                            _commandCounter++;
                            ListDB.Add(new Commands(_commandCounter, lines.ElementAt(_counter).Replace("\t# ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription));
                            if (sumTrue == 0)
                                throw new Exception("Unvalid Command : No operations exist");
                            else
                            {
                                sumTrue--;
                                tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));
                                inWhileOrIf.Remove(inWhileOrIf.Keys.LastOrDefault());
                            }
                        }

                        _counter++;

                        sumTrue = inWhileOrIf.Count(x => x.Value == true);
                        tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));
                    }
                    TestList.Add(new TestSuit() { TestNumber = testCaseCounter, TestName = _testName, TestValue = new ObservableCollection<Commands>(ListDB), IsSaved = true });
                    TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                    i++;
                    _commandCounter = 1;
                    passed = false;
                    ListDB.Clear();
                }
                _testCaseCounter = testCaseCounter;
                TestCaseListView.ItemsSource = TestList;
                listView.ItemsSource = ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                view2.Refresh();
            }
        }
        #endregion

        ///////SysFUNCTIONS///////
        #region SysFUNCTIONS

        private void listView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double listViewSize = listView.ActualWidth;
            row.Width = 33.7;
            command.Width = listViewSize / 100 * 18.7;
            target.Width = listViewSize / 100 * 22.2;
            value.Width = listViewSize / 100 * 22.2;
            description.Width = listViewSize / 100 * 22.2;
        }

        private void TestCaseListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double listViewSize = TestCaseListView.ActualWidth;
            testNameRow.Width = listViewSize - 20;
        }

        void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb senderAsThumb = e.OriginalSource as Thumb;
            GridViewColumnHeader header
              = senderAsThumb.TemplatedParent as GridViewColumnHeader;
            if (header.Column.ActualWidth < 90)
            {
                header.Column.Width = 90;
            }
            /*else if (header.Column.ActualWidth > 270)
            {
                header.Column.Width = 270;
            }*/
            else;
        }

        private void CommandBorder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddCommand();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //BL.OpenFile _openFile = new BL.OpenFile();
            if (Keyboard.IsKeyDown(Key.S) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                SaveTestCase();
            }
            else if (Keyboard.IsKeyDown(Key.S) && Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift))
            {
                SaveAsTestCase();
            }
            else if (Keyboard.IsKeyDown(Key.N) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                AddTestCaseButton();
            }
            else if (Keyboard.IsKeyDown(Key.O) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                OpenDialog();
                if (_openFileAddress != null)
                    OpenTestCase(_openFileAddress);
            }
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void move_window(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OpenFile_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            OpenFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF00C6FF");
        }

        private void OpenFile_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            OpenFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
        }

        private void SaveAsFile_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            SaveAsFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF00C6FF");
        }

        private void SaveAsFile_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            SaveAsFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
        }

        ListViewItem GetListViewItem(int index)
        {
            try
            {
                /*if (TestCaseListView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    return null;

                return TestCaseListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;*/
                /*if((ListViewItem)TestCaseListView.ItemContainerGenerator.ContainerFromIndex(index) == null)
                {
                    TestCaseListView.UpdateLayout();
                    TestCaseListView.ScrollIntoView(TestCaseListView.Items[index]);
                    return TestCaseListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
                }
                else
                {
                    return TestCaseListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
                }*///////
                return TestCaseListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;

            }
            catch (Exception ex)
            {
                Log.Items.Add("GetListViewItem ---> Failed Because of error : " + ex.ToString());
                return null;
            }
        }

        private void SaveFile_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (testCaseCounter != 0)
                {
                    if (TestList.ElementAt(_testCaseCounter - 1).IsSaved)
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF00C6FF");
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF3B3B");
                    }
                }
                else
                {
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF00C6FF");
                }
            }
            catch(Exception ex)
            {
                Log.Items.Add("SaveFile_MouseEnter ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void SaveFile_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                if (testCaseCounter != 0)
                {
                    if (TestList.ElementAt(_testCaseCounter - 1).IsSaved)
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
                    }
                }
                else
                {
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
                }
            }
            catch(Exception ex)
            {
                Log.Items.Add("SaveFile_MouseLeave ---> Failed Because of error : " + ex.ToString());
            }
        }
        #endregion

        ///////Dialog///////
        #region Dialog
        public void SaveDialog()
        {
            ProjectName = ProjectNameText.Text;
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = TestList.ElementAt(_testCaseCounter - 1).TestName;
            saveFileDialog.Filter = "Python file (*.py)|*.py";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                CurrentTestCase.SavedPath = null;
                sPath = null;
            }
            else
            {
                sPath = saveFileDialog.FileName;
                testCaseFileName = System.IO.Path.GetFileName(@sPath);
                string mystring = sPath.Replace("\\" + testCaseFileName, "");
                for (int i = mystring.Length; i > 0; i--)
                    mystring = mystring.Substring(mystring.IndexOf("\\") + 1);
                FolderName = mystring;
                var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                if (obj != null) obj.TestFolder = FolderName;
                var obj2 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                if (obj2 != null) obj2.SavedPath = sPath;
            }
        }

        public void SaveTestSuitDialog()
        {
            ProjectName = ProjectNameText.Text;
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            if (ProjectName == "")
                TestSuitNameWarnDialog();
            else
                saveFileDialog.FileName = ProjectName;
            saveFileDialog.Filter = "Python file (*.py)|*.py";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                CurrentTestCase.SavedPath = null;
                gPath = null;
            }
            else
            {
                gPath = saveFileDialog.FileName;
                testCaseFileName = Path.GetFileName(@gPath);
            }
        }

        public void OpenDialog()
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                ok = false;
            }
            else
            {
                ok = true;
                _openFileAddressArray = openFileDialog.FileNames;
                _openFileNameArray = openFileDialog.SafeFileNames;
                _openedFiles = openFileDialog.FileNames.Count();
            }
        }

        public void EmptyFieldtDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var _emptyFieldDialog = new MessageBox.EmptyField();
            _emptyFieldDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        public void UnvalidCommandDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var _unvalidCommandDialog = new MessageBox.UnvalidCommand();
            _unvalidCommandDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        public void AddTestDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var _addTestDialog = new MessageBox.AddTestFirst();
            _addTestDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        public void StartUpMessageDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            StartUpMessage startUpMessage = new StartUpMessage();
            startUpMessage.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        public void AddTestCaseButton()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            AddTestCase addTestCase = new AddTestCase();
            addTestCase.Owner = this;
            addTestCase.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var createproject = new CreateNewProject();
            createproject.Owner = this;
            createproject.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
            ProjectNameText.Text = ProjectName;
        }

        public void UnsavedContentDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var _unsavedContentDialog = new MessageBox.UnsavedContent();
            _unsavedContentDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        public void TestSuitNameWarnDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var _testSuitNameWarnDialog = new MessageBox.TestSuitNameWarn();
            _testSuitNameWarnDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        public void UpdateAckDialog()
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            var _updateAckDialog = new MessageBox.UpdateAck();
            _updateAckDialog.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }
        #endregion

        ///////FUNCTIONS///////
        #region FUNCTIONS
        public string FindBetween(string st, string strat, string end)
        {
            try
            {
                int pFrom = st.IndexOf(strat) + strat.Length;
                int pTo = st.LastIndexOf(end);
                if (pTo == -1)
                    return "empty";
                else
                {
                    String result = st.Substring(pFrom, pTo - pFrom);
                    return result;
                }
            }
            catch(Exception ex)
            {
                Log.Items.Add("FindBetween ---> Failed Because of error : " + ex.ToString());
                return "null(Exception happened)";
            }
        }

        public string ValidName(string st)
        {
            try
            {
                st = st.Replace("~", "");
                st = st.Replace("`", "");
                st = st.Replace("!", "");
                st = st.Replace("@", "");
                st = st.Replace("#", "");
                st = st.Replace("$", "");
                st = st.Replace("%", "");
                st = st.Replace("^", "");
                st = st.Replace("&", "");
                st = st.Replace("*", "");
                st = st.Replace("(", "");
                st = st.Replace(")", "");
                st = st.Replace("-", "_");
                st = st.Replace("+", "");
                st = st.Replace("=", "");
                st = st.Replace("]", "");
                st = st.Replace("[", "");
                st = st.Replace("}", "");
                st = st.Replace("{", "");
                st = st.Replace("'", "");
                st = st.Replace("\"", "");
                st = st.Replace(";", "");
                st = st.Replace(":", "");
                st = st.Replace("?", "");
                st = st.Replace(".", "_");
                st = st.Replace("<", "");
                st = st.Replace(">", "");
                st = st.Replace("|", "");
                return st;
            }
            catch (Exception ex)
            {
                Log.Items.Add("ValidName ---> Failed Because of error : " + ex.ToString());
                return "null(Exception happened)";
            }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTB.Text))
                return true;
            else
                return ((item as TestSuit).TestName.IndexOf(SearchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void AddCommand()
        {
            try
            {
                if (testCaseCounter != 0)
                {
                    Commands temp = null;
                    bool unvalidCommand = true;
                    for (int i = 0; i < CommandsList.Length / 2; i++)
                    {
                        if (CommandsList[i,0] == CommandsComboBox.Text)
                        {
                            unvalidCommand = false;
                            break;
                        }
                    }
                    if (unvalidCommand)
                    {
                        UnvalidCommandDialog();
                    }
                    else
                    {
                        listView.ItemsSource = ListDB;
                        TestCaseListView.ItemsSource = TestList;

                        if (CommandsComboBox.Text != "" && TargetTB.Text != "" && ValueTB.Text != ""/* && DescriptionTB.Text != ""*/)
                        {
                            if (edit == true)
                            {
                                var obj = ListDB.FirstOrDefault(x => x.Number == _commandCounter);
                                if (obj != null)
                                {
                                    obj.Command = tabInCommandForEdit + CommandsComboBox.Text;
                                    obj.Target = TargetTB.Text;
                                    obj.Value = ValueTB.Text;
                                    obj.VariableName = CommandsComboBox.Text + Convert.ToString(_commandCounter + 1);
                                    obj.Description = DescriptionTB.Text;
                                }
                                listView.SelectedIndex = _commandCounter - 1;
                            }
                            else
                            {
                                CommandCounter++;

                                if (CommandsComboBox.Text == "while" || CommandsComboBox.Text == "if")
                                    inWhileOrIf.Add(CommandCounter, true);

                                if (CommandsComboBox.Text == "end")
                                    if (sumTrue == 0)
                                        throw new Exception("Unvalid Command : No operation exist");
                                    else
                                    {
                                        sumTrue--;
                                        tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));
                                        inWhileOrIf.Remove(inWhileOrIf.Keys.LastOrDefault());
                                    }

                                CommandCounterTB.Text = Convert.ToString(CommandCounter);
                                ListDB.Add(new Commands(CommandCounter, tabNeeded + CommandsComboBox.Text, TargetTB.Text, ValueTB.Text, CommandsComboBox.Text + Convert.ToString(CommandCounter + 1), DescriptionTB.Text));
                                var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                                if (obj != null) obj.TestValue = new ObservableCollection<Commands>(ListDB);
                                CommandsComboBox.Focus();
                                HandleComboBox();
                                _commandCounter = CommandCounter;
                                CommandCounterTB.Text = Convert.ToString(CommandCounter);
                                listView.SelectedIndex = CommandCounter - 1;
                                temp = ListDB.ElementAt(CommandCounter - 1);

                                sumTrue = inWhileOrIf.Count(x => x.Value == true);
                                tabNeeded = string.Concat(Enumerable.Repeat("\t", sumTrue));
                            }
                            var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                            if (obj1 != null) obj1.IsSaved = false;
                            var bc = new BrushConverter();
                            SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
                            HandleTestList();
                            edit = false;
                            CommandsComboBox.Text = "";
                            TargetTB.Text = "";
                            ValueTB.Text = "";
                            DescriptionTB.Text = "";
                            ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                            view.Refresh();
                            listView.ScrollIntoView(temp);
                        }
                        else
                        {
                            EmptyFieldtDialog();
                        }
                    }
                }
                else
                {
                    AddTestDialog();
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("AddCommand ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void HandleComboBox()
        {
            try
            {
                string tempDiscription = "";
                for (int i = 0; i < CommandsList.Length/2; i++)
                {
                    if (CommandsList[i, 0] == CommandsComboBox.Text)
                    {
                        tempDiscription = CommandsList[i, 1];
                        for(int j = 0; j < CommandsList.Length/2; j++)
                            if (DescriptionTB.Text == "" || DescriptionTB.Text == "None" || CommandsList[j, 1] == DescriptionTB.Text)
                            {
                                DescriptionTB.Text = tempDiscription;
                                break;
                            }
                    }
                }

                var bc = new BrushConverter();

                switch (CommandsComboBox.Text)
                {
                    case "runScript": case "select": case "selectByIndex": case "selectByValue": case "selectByVisibleText": case "sendKeys": case "storeElementPresent": 
                    case "storeEval": case "storeHref": case "storeId": case "storeInnerHTML": case "storeName": case "storeText": case "storeValue": case "storeWicketPath":
                    case "switch": case "type": case "waitForAttribute": case "waitForNotText": case "waitForNumberOfWindowPresent": case "waitForText": case "waitForValue":
                        ValueTB.IsEnabled = true;
                        ValueTBBorder.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");

                        TargetTB.IsEnabled = true;
                        TargetTBBorder.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");

                        if (TargetTB.Text == "None")
                            TargetTB.Text = "";
                        if (ValueTB.Text == "None")
                            ValueTB.Text = "";

                        break;

                    case "clearText": case "click": case "if": case "open": case "pause": case "replace": case "scrollInto": case "waitForElementNotPresent":
                    case "waitForElementPresent": case "waitForElementVisible": case "waitForWindowPresent": case "while":
                        ValueTB.Text = "None";
                        ValueTB.IsEnabled = false;
                        ValueTBBorder.Background = (Brush)bc.ConvertFrom("#FFEBE7EE");

                        TargetTB.IsEnabled = true;
                        TargetTBBorder.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");

                        if (TargetTB.Text == "None")
                            TargetTB.Text = "";

                        break;

                    case "alert": case "break": case "close": case "end": case "failTest": case "refresh": case "switchToDefault":
                        TargetTB.Text = "None";
                        TargetTB.IsEnabled = false;
                        TargetTBBorder.Background = (Brush)bc.ConvertFrom("#FFEBE7EE");

                        ValueTB.Text = "None";
                        ValueTB.IsEnabled = false;
                        ValueTBBorder.Background = (Brush)bc.ConvertFrom("#FFEBE7EE");

                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Items.Add("HandleComboBox ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void HandleTestList()
        {
            try
            {
                for(int i = 0; i < testCaseCounter; i++)
                {
                    if (!TestList.ElementAt(i).IsSaved)
                    {
                        ListViewItem item = GetListViewItem(i);
                        if (item != null)
                        {
                            var bc = new BrushConverter();
                            item.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("HandleTestList ---> Failed Because of error : " + ex.ToString());
            }

        }

        public void SaveTestCase()
        {
            bool a = false;
            bool b = false;
            bool c = false;
            try
            {
                if (_testCaseCounter == 0)
                {
                    a = true;
                    AddTestDialog();
                }
                else if (TestList.ElementAt(_testCaseCounter - 1).SavedPath != null)
                {
                    b = true;
                    TestMethod(TestList.ElementAt(_testCaseCounter - 1).SavedPath);
                    Log.Items.Add(TestList.ElementAt(_testCaseCounter - 1).TestName + " TestCase Saved ---> Successfully");
                    var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    if (obj1 != null) obj1.IsSaved = true;
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
                }
                else if (TestList.ElementAt(_testCaseCounter - 1).SavedPath == null)
                {
                    c = true;
                    SaveDialog();
                    if (sPath != null)
                    {
                        TestMethod(sPath);
                        Log.Items.Add(TestList.ElementAt(_testCaseCounter - 1).TestName + " TestCase Saved ---> Successfully");
                        var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                        if (obj1 != null) obj1.IsSaved = true;
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
                    }
                }
                HandleTestList();
            }
            catch (Exception ex)
            {
                if (b)
                    Log.Items.Add("SaveTestCase ---> Failed " + "Because of error: " + ex.ToString() + "(ERROR: select a test case first)");
                else
                    Log.Items.Add("SaveTestCase ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void SaveAsTestCase()
        {
            try
            {
                if (testCaseCounter == 0)
                    AddTestDialog();
                else
                {
                    //BL.BuildFile _buildFile = new BL.BuildFile();
                    SaveDialog();
                    TestMethod(sPath);
                    Log.Items.Add(TestList.ElementAt(_testCaseCounter - 1).TestName + " Saved ---> Successfully");
                    _testCaseFileName = testCaseFileName;
                }
                HandleTestList();
            }
            catch (Exception ex)
            {
                Log.Items.Add("SaveAsTestCase ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void CopyItem()
        {
            try
            {
                multiplication = Multiplication.copy;
                CopiedItem = (Commands)listView.SelectedItems[0];
            }
            catch (Exception ex)
            {
                Log.Items.Add("CopyItem ---> Failed Because of error : " + ex.ToString());
            }
        }
        
        public void CutItem()
        {
            try
            {
                multiplication = Multiplication.cut;
                CopiedItem = (Commands)listView.SelectedItems[0];
            }
            catch (Exception ex)
            {
                Log.Items.Add("CutItem ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void PasteItem()
        {
            try
            {
                if(multiplication == Multiplication.copy)
                {
                    CommandCounter++;
                    CommandCounterTB.Text = Convert.ToString(CommandCounter);
                    ListDB.Add(new Commands(CommandCounter, CopiedItem.Command, CopiedItem.Target, CopiedItem.Value, CopiedItem.VariableName, CopiedItem.Description));
                    var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    if (obj != null) obj.TestValue = new ObservableCollection<Commands>(ListDB);
                    HandleComboBox();
                    _commandCounter = CommandCounter;
                }
                else if(multiplication == Multiplication.cut)
                {
                    Commands removed = CopiedItem;
                    ListDB.RemoveAt(CopiedItem.Number - 1);
                    for (int i = removed.Number + 1; i < CommandCounter + 1; i++)
                    {
                        var obj2 = ListDB.FirstOrDefault(x => x.Number == i);
                        if (obj2 != null) obj2.Number = i - 1;
                    }
                    listView.ItemsSource = ListDB;
                    ICollectionView view1 = CollectionViewSource.GetDefaultView(ListDB);
                    view1.Refresh();
                    _commandCounter--;
                    CommandCounter--;

                    CommandCounterTB.Text = Convert.ToString(CommandCounter);
                    ListDB.Add(new Commands(CommandCounter, CopiedItem.Command, CopiedItem.Target, CopiedItem.Value, CopiedItem.VariableName, CopiedItem.Description));
                    var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    if (obj != null) obj.TestValue = new ObservableCollection<Commands>(ListDB);
                    CommandsComboBox.Focus();
                    HandleComboBox();
                    _commandCounter = CommandCounter;

                    for (int i = 1; i <= ListDB.Count; i++)
                    {
                        var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                        if (obj1 != null) obj1.TestValue.ElementAt(i - 1).Number = i;
                    }
                }
                listView.ItemsSource = ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                view.Refresh();
                var obj3 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                if (obj3 != null) obj3.IsSaved = false;
                var bc = new BrushConverter();
                SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
            }
            catch (Exception ex)
            {
                Log.Items.Add("PasteItem ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void RemoveItem()
        {
            try
            {
                Commands removed = (Commands)listView.SelectedItem;
                ListDB.RemoveAt(listView.Items.IndexOf(listView.SelectedItem));
                for (int i = removed.Number + 1; i < CommandCounter + 1; i++)
                {
                    var obj = ListDB.FirstOrDefault(x => x.Number == i);
                    if (obj != null) obj.Number = i - 1;
                }
                listView.ItemsSource = ListDB;
                ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                view.Refresh();
                _commandCounter--;
                CommandCounter--;
                CommandCounterTB.Text = Convert.ToString(CommandCounter);
                var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                if (obj1 != null) obj1.IsSaved = false;
                var bc = new BrushConverter();
                SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
            }
            catch (Exception ex)
            {
                Log.Items.Add("RemoveItem ---> Failed Because of error : " + ex.ToString());
            }
        }

        public void CopyTestCase()
        {
        }

        public void CutTestCase()
        {
        }

        public void PasteTestCase()
        {
        }

        public void RemoveTestCase()
        {
            try
            {
                TestSuit removed = (TestSuit)TestCaseListView.SelectedItem;
                TestList.Remove(removed);
                for (int i = removed.TestNumber + 1; i < testCaseCounter + 1; i++)
                {
                    var obj = TestList.FirstOrDefault(x => x.TestNumber == i);
                    if (obj != null) obj.TestNumber = i - 1;
                }
                TestCaseListView.ItemsSource = TestList;
                ICollectionView view = CollectionViewSource.GetDefaultView(TestList);
                view.Refresh();
                listView.ItemsSource = ListDB;
                ICollectionView view1 = CollectionViewSource.GetDefaultView(ListDB);
                view1.Refresh();
                _testCaseCounter--;
                testCaseCounter--;
                TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                if (removed.TestValue == CurrentTestCase.TestValue || TestList.Count == 0)
                {
                    ListDB.Clear();
                    listView.ItemsSource = ListDB;
                    ICollectionView view2 = CollectionViewSource.GetDefaultView(ListDB);
                    view2.Refresh();
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("RemoveTestCase ---> Failed Because of error : " + ex.ToString());
            }
        }

        bool needCotBefore = true;
        bool needCotAfter = true;

        public string ConvertTextToIdeFormat(string IN, bool considerStar, bool considerDolar)
        {
            try
            {
                if (IN == "None") return IN;
                if (IN == "") return IN;
                string OUT = "";
                if (IN.Contains("Math.floor"))
                {
                    string period = FindBetween(IN, "random()*", ")").Remove(FindBetween(IN, "random()*", ")").Length - 2, 1);
                    OUT = "random.randint(" + period + ", " + period.Replace("1", "9").Replace("0", "9") + ")";
                }
                if (IN.Contains('*') && considerStar)
                {
                    /*if (IN.Substring(0, 1) == "*")
                        needCotBefore = false;
                    if (IN.Substring(IN.Length - 1, 1) == "*")
                        needCotAfter = false;*/
                    string[] INStar = IN.Split('*');
                    INStar = INStar.Where(val => val != "").ToArray();
                    for (int i = 0; i < INStar.Length; i++)
                    {
                        OUT += INStar[i];
                        if (i < INStar.Length - 1)
                        {
                            if (INStar[i + 1].Substring(0) == "${" && INStar[i].Contains("${") && INStar[i].Substring(INStar[i].Length - 1) == "}")
                            {
                                OUT += " or ";
                            }
                            else if (INStar[i + 1].Substring(0, 2) == "${")
                            {
                                OUT += "' or ";
                            }
                            else if (INStar[i].Contains("${") && INStar[i].Substring(INStar[i].Length - 1) == "}")
                            {
                                OUT += " or '";
                            }
                            else
                            {
                                OUT += "' or '";
                            }
                        }
                    }
                    IN = OUT;
                    OUT = "";
                }
                if (IN.Contains("${") && considerDolar)
                {
                    if (IN.Substring(0, 2) == "${")
                        needCotBefore = false;
                    if (IN.Substring(IN.Length - 1, 1) == "}")
                        needCotAfter = false;
                    string[] INDolar = IN.Split('$');
                    INDolar = INDolar.Where(val => val != "").ToArray();
                    for (int i = 0; i < INDolar.Length; i++)
                    {
                        if (INDolar[i].Contains('{') && INDolar[i].Contains('}'))
                        {
                            // start
                            if (i == 0)
                                INDolar[i] = INDolar[i].Replace("{", "str(StoreEvalDB.vars[\"");
                            else
                                INDolar[i] = (INDolar[i - 1].Contains(" or ") && INDolar[i - 1].Substring(INDolar[i - 1].Length - 4, 4) == " or ") ?
                                    INDolar[i].Replace("{", "str(StoreEvalDB.vars[\"") : INDolar[i].Replace("{", "' + str(StoreEvalDB.vars[\"");

                            if (INDolar[i].Contains(" or ") && INDolar[i].Substring(INDolar[i].IndexOf("}") + 1, 4) == " or ")
                                INDolar[i] = INDolar[i].Replace("}", "\"])");
                            else if (INDolar[i].IndexOf('}') == INDolar[i].Length - 1 && i == INDolar.Length - 1)
                                INDolar[i] = INDolar[i].Replace("}", "\"])");
                            else if (INDolar[i].IndexOf('}') == INDolar[i].Length - 1)
                                INDolar[i] = INDolar[i].Replace("}", "\"]) + ");
                            else
                                INDolar[i] = INDolar[i].Replace("}", "\"]) + '");
                            // end
                            INDolar[i] = INDolar[i].IndexOf('}') + 1 != null ? INDolar[i].Replace("}", "\"])") : INDolar[i].Replace("}", "\"]) + '");
                            OUT += INDolar[i];
                        }
                        else
                        {
                            OUT += INDolar[i];
                        }
                    }
                }
                if (OUT == "")
                    OUT = IN;
                if (needCotBefore)
                    OUT = "'" + OUT;
                if (needCotAfter)
                    OUT = OUT + "'";
                return OUT;
            }
            catch (Exception ex)
            {
                Log.Items.Add("ConvertTextToIdeFormat ---> Failed Because of error : " + ex.ToString() + " in line ");
                return IN;
            }
        }

        public string ConvertTextFromIdeFormat(string IN)
        {
            string OUT = "";
            if (IN.Substring(0, 1) == "'")
            {
                IN = IN.Remove(0, 1);
                OUT = IN;
            }
            if (IN.Substring(IN.Length - 1, 1) == "'")
            {
                IN = IN.Remove(IN.Length - 1, 1);
                OUT = IN;
            }
            if (!IN.Contains("str(StoreEvalDB.vars") && !IN.Contains(" or "))
            {
                return OUT;
            }
            else
            {
                OUT = IN.Replace("' + str(StoreEvalDB.vars[\"", "${").Replace("\" + str(StoreEvalDB.vars[\"", "${").Replace("str(StoreEvalDB.vars[\"", "${").Replace("\"]) + '", "}").Replace("\"]) + \"", "}").Replace("\"]) + ", "}").Replace("\"])", "}").Replace("' or '", "*").Replace("\" or \"", "*").Replace("' or ", "*").Replace("\" or ", "*").Replace(" or '", "*").Replace(" or \"", "*").Replace(" or ", "*");
                return OUT;
            }
        }

        #endregion

        ///////BUTTON///////
        #region BUTTON
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            bool a = false;
            for (int i = 0; i < testCaseCounter; i++)
                if (!TestList.ElementAt(i).IsSaved)
                {
                    a = true;
                    break;
                }
            if (a)
            {
                UnsavedContentDialog();
                if (Continue)
                    Application.Current.Shutdown();
            }
            else
                Application.Current.Shutdown();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WindowState == WindowState.Normal)
                {
                   WindowState = WindowState.Maximized;
                }
                else
                    this.WindowState = WindowState.Normal;
            }
            catch (Exception ex)
            {
                Log.Items.Add("Maximize_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                Log.Items.Add("Minimize_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void SaveTestCase_Click(object sender, RoutedEventArgs e)
        {
            SaveTestCase();
        }

        private void SaveTestSuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //BL.BuildFile _buildFile = new BL.BuildFile();
                if (gPath != null)
                {
                    TestSuitMethod(gPath);
                    Log.Items.Add(testCaseFileName + " TestSuit Saved ---> Successfully ");
                }
                else if (testCaseCounter != 0)
                {
                    SaveTestSuitDialog();
                    if (gPath != null)
                    {
                        TestSuitMethod(gPath);
                        Log.Items.Add(testCaseFileName + " TestSuit Saved ---> Successfully");
                    }
                }
                else if (testCaseCounter == 0)
                    AddTestDialog();
                HandleTestList();
            }
            catch (Exception ex)
            {
                Log.Items.Add("SaveTestSuit_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void SaveAsTestCase_Click(object sender, RoutedEventArgs e)
        {
            SaveAsTestCase();
        }

        private void SaveAsTestSuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //BL.BuildFile _buildFile = new BL.BuildFile();
                if (testCaseCounter != 0)
                {
                    SaveTestSuitDialog();
                    if (gPath != null)
                    {
                        TestSuitMethod(gPath);
                        Log.Items.Add(testCaseFileName + " Saved ---> Successfully");
                    }
                }
                else if (testCaseCounter == 0)
                    AddTestDialog();
                HandleTestList();
            }
            catch (Exception ex)
            {
                Log.Items.Add("SaveAsTestSuit_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void OpenTestCase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Python file (*.py)|*.py";
                OpenDialog();
                if (ok)
                {
                    //BL.OpenFile _openFile = new BL.OpenFile();
                    for (int i = 0; i < _openedFiles; i++)
                    {
                        _openFileAddress = _openFileAddressArray[i];
                        _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                        _openFileName = _openFileNameArray[i];
                        _openFileName = _openFileName.Replace(".py", "");
                        string mystring = _openFileAddress.Replace("\\" + _openFileName + ".py", "");
                        for (int j = mystring.Length; j > 0; j--)
                            mystring = mystring.Substring(mystring.IndexOf("\\") + 1);
                        FolderName = mystring;
                        TestCaseListView.ItemsSource = TestList;
                        listView.ItemsSource = ListDB;
                        ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                        view.Refresh();
                        ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                        view2.Refresh();
                        if (_openFileAddress != null)
                            OpenTestCase(_openFileAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("OpenTestCase_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void OpenTestSuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Python file (*.py)|*.py";
                OpenDialog();
                if (ok)
                {
                    //BL.OpenFile _openFile = new BL.OpenFile();
                    testCaseCounter = 0;
                    _openFileAddress = _openFileAddressArray[0];
                    _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                    _openFileName = _openFileNameArray[0];
                    _openFileName = _openFileName.Replace(".py", "");
                    string mystring = _openFileAddress.Replace("\\" + _openFileName, "");
                    for (int j = mystring.Length; j > 0; j--)
                        mystring = mystring.Substring(mystring.IndexOf("\\") + 1);
                    FolderName = mystring;
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                    view2.Refresh();
                    OpenTestSuit(_openFileAddress);
                    TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("OpenTestSuit_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void OpenOldTestCaseClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "All files (*.*)|*.*";
                OpenDialog();
                if (ok)
                {
                    for (int i = 0; i < _openedFiles; i++)
                    {
                        _openFileAddress = _openFileAddressArray[i];
                        _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                        _openFileName = _openFileNameArray[i];
                        _openFileName = _openFileName.Remove(_openFileName.IndexOf('.'), _openFileName.Length - _openFileName.IndexOf('.'));
                        string mystring = _openFileAddress.Replace("\\" + _openFileNameArray[i], "");
                        for (int j = mystring.Length; j > 0; j--)
                            mystring = mystring.Substring(mystring.IndexOf("\\") + 1);
                        FolderName = mystring;
                        TestCaseListView.ItemsSource = TestList;
                        listView.ItemsSource = ListDB;
                        ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                        view.Refresh();
                        ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                        view2.Refresh();
                        if (_openFileAddress != null)
                        {
                            openOldTestCase(_openFileAddress);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("OpenOldTestCaseClick_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void OpenOldTestSuitClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "All files (*.*)|*.*";
                OpenDialog();
                if (ok)
                {
                    testCaseCounter = 0;
                    _openFileAddress = _openFileAddressArray[0];
                    _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                    _openFileName = _openFileNameArray[0];
                    _openFileName = _openFileName.Replace(".py", "");
                    string mystring = _openFileAddress.Replace("\\" + _openFileName, "");
                    for (int j = mystring.Length; j > 0; j--)
                        mystring = mystring.Substring(mystring.IndexOf("\\") + 1);
                    FolderName = mystring;
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                    view2.Refresh();
                    openOldTestSuit(_openFileAddress);
                    TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("OpenOldTestSuitClick_Click ---> Failed Because of error : " + ex.ToString());
            }
        }
        private void OpenNewTestSuitClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Selenium IDE output (*.side)|*.side";
                OpenDialog();
                if (ok)
                {
                    testCaseCounter = 0;
                    _openFileAddress = _openFileAddressArray[0];
                    _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                    _openFileName = _openFileNameArray[0];
                    _openFileName = _openFileName.Replace(".side", "");
                    string mystring = _openFileAddress.Replace("\\" + _openFileName, "");
                    for (int j = mystring.Length; j > 0; j--)
                        mystring = mystring.Substring(mystring.IndexOf("\\") + 1);
                    FolderName = mystring;
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                    view2.Refresh();
                    openNewTestSuit(_openFileAddress);
                    TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("OpenNewTestSuitClick_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void RunAllTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("C:\\Users\\AmirReza\\PycharmProjects\\FirstTry\\LoginEpl.py");
            }
            catch (Exception ex)
            {
                Log.Items.Add("RunCurrent_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void RunCurrent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("C:\\Users\\AmirReza\\PycharmProjects\\FirstTry\\LoginEpl.py");
            }
            catch (Exception ex)
            {
                Log.Items.Add("RunCurrent_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Speed_Click(object sender, RoutedEventArgs e)
        {
        }

        public void AddTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddTestCaseButton();
            }
            catch (Exception ex)
            {
                Log.Items.Add("AddTest_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void AddCommand_Click(object sender, RoutedEventArgs e)
        {
            AddCommand();
        }

        private void SelectTarget_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            Log.Items.Clear();
        }

        private void CopyItem_Click(object sender, RoutedEventArgs e)
        {
            CopyItem();
        }

        private void CutItem_Click(object sender, RoutedEventArgs e)
        {
            CutItem();
        }

        private void PasteItem_Click(object sender, RoutedEventArgs e)
        {
            PasteItem();
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count != 0)
                {
                    CurrentCommand = (Commands)listView.SelectedItems[0];
                    _commandCounter = CurrentCommand.Number;
                    CommandsComboBox.Text = CurrentCommand.Command;
                    TargetTB.Text = CurrentCommand.Target;
                    ValueTB.Text = CurrentCommand.Value;
                    DescriptionTB.Text = CurrentCommand.Description;
                    HandleComboBox();
                    edit = true;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("EditItem_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem();
        }

        private void CopyItemTestCaseList_Click(object sender, RoutedEventArgs e)
        {
            CopyTestCase();
        }

        private void CutItemTestCaseList_Click(object sender, RoutedEventArgs e)
        {
            CutTestCase();
        }

        private void PasteItemTestCaseList_Click(object sender, RoutedEventArgs e)
        {
            PasteTestCase();
        }

        private void EditItemTestCaseList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TestCaseListView.SelectedItems.Count != 0)
                {
                    CurrentTestCase = (TestSuit)TestCaseListView.SelectedItems[0];
                    _testCaseCounter = CurrentTestCase.TestNumber;
                    edit = false;
                    if (CurrentTestCase.TestValue != null)
                    {
                        var obj = TestList.FirstOrDefault(x => x.TestName == CurrentTestCase.TestName);
                        if (obj != null) ListDB = new ObservableCollection<Commands>(obj.TestValue);
                        CommandCounter = _commandCounter = obj.TestValue.Count;
                        CommandCounterTB.Text = Convert.ToString(CommandCounter);
                    }
                    else
                    {
                        var obj = TestList.FirstOrDefault(x => x.TestName == CurrentTestCase.TestName);
                        CommandCounter = _commandCounter = 0;
                        CommandCounterTB.Text = Convert.ToString(CommandCounter);
                        ListDB.Clear();
                        listView.ItemsSource = ListDB;
                        ICollectionView view3 = CollectionViewSource.GetDefaultView(ListDB);
                        view3.Refresh();
                    }
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                    view2.Refresh();
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("EditItemTestCaseList_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void RenameItemTestCaseList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedTest = (TestSuit)TestCaseListView.SelectedItem;
                MainGrid.Effect = new BlurEffect();
                Splash.Visibility = Visibility.Visible;
                RenameTestCase renameTestCase = new RenameTestCase
                {
                    Owner = this
                };
                renameTestCase.RenameTestCaseTB.Text = SelectedTest.TestName;
                renameTestCase.ShowDialog();
                Splash.Visibility = Visibility.Collapsed;
                MainGrid.Effect = null;
            }
            catch (Exception ex)
            {
                Log.Items.Add("RenameItemTestCaseList_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void RemoveItemTestCaseList_Click(object sender, RoutedEventArgs e)
        {
            RemoveTestCase();
        }
        #endregion

        /////////LISTEVENTS///////
        #region LISTEVENTS
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count != 0)
                {
                    CurrentCommand = (Commands)listView.SelectedItems[0];
                    _commandCounter = CurrentCommand.Number;
                    CommandsComboBox.Text = CurrentCommand.Command.Replace("\t", "");
                    tabInCommandForEdit = CurrentCommand.Command.Replace(CommandsComboBox.Text, "");
                    TargetTB.Text = CurrentCommand.Target;
                    ValueTB.Text = CurrentCommand.Value;
                    DescriptionTB.Text = CurrentCommand.Description;
                    HandleComboBox();
                    edit = true;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("ListView_MouseDoubleClick ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void TestCaseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TestCaseListView.SelectedItems.Count != 0)
                {
                    CurrentTestCase = (TestSuit)TestCaseListView.SelectedItems[0];
                    _testCaseCounter = CurrentTestCase.TestNumber;
                    edit = false;
                    if (CurrentTestCase.TestValue != null)
                    {
                        var obj = TestList.FirstOrDefault(x => x.TestName == CurrentTestCase.TestName);
                        if (obj != null) ListDB = new ObservableCollection<Commands>(obj.TestValue);
                        CommandCounter = _commandCounter = obj.TestValue.Count;
                        CommandCounterTB.Text = Convert.ToString(CommandCounter);
                    }
                    else
                    {
                        var obj = TestList.FirstOrDefault(x => x.TestName == CurrentTestCase.TestName);
                        CommandCounter = _commandCounter = 0;
                        CommandCounterTB.Text = Convert.ToString(CommandCounter);
                        ListDB.Clear();
                        listView.ItemsSource = ListDB;
                        ICollectionView view3 = CollectionViewSource.GetDefaultView(ListDB);
                        view3.Refresh();
                    }
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                    view2.Refresh();
                    if (testCaseCounter != 0)
                    {
                        if (TestList.ElementAt(_testCaseCounter - 1).IsSaved)
                        {
                            var bc = new BrushConverter();
                            SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
                        }
                        else
                        {
                            var bc = new BrushConverter();
                            SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
                        }
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FF1C1C1C");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_MouseDoubleClick ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void CommandsComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            HandleComboBox();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TestCaseListView.ItemsSource = TestList;
            CollectionViewSource.GetDefaultView(TestCaseListView.ItemsSource).Refresh();
        }

        private void CommandsComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            HandleComboBox();
        }

        private void ListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int a = TestList.ElementAt(_testCaseCounter - 1).TestValue.Count;
                if (a == 0)
                {
                    CopyItemCM.IsEnabled = false;
                    CutItemCM.IsEnabled = false;
                    PasteItemCM.IsEnabled = false;
                    EditItemCM.IsEnabled = false;
                    RemoveItemCM.IsEnabled = false;
                }
                else
                {
                    CopyItemCM.IsEnabled = true;
                    CutItemCM.IsEnabled = true;
                    PasteItemCM.IsEnabled = true;
                    EditItemCM.IsEnabled = true;
                    RemoveItemCM.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("ListView_MouseRightButtonDown ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void TestCaseListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int a = TestList.Count;
                if (a == 0)
                {
                    CopyTestCM.IsEnabled = false;
                    CutTestCM.IsEnabled = false;
                    PasteTestCM.IsEnabled = false;
                    EditTestCM.IsEnabled = false;
                    RenameTestCM.IsEnabled = false;
                    RemoveTestCM.IsEnabled = false;
                }
                else
                {
                    //CopyTestCM.IsEnabled = true;
                    //CutTestCM.IsEnabled = true;
                    //PasteTestCM.IsEnabled = true;
                    EditTestCM.IsEnabled = true;
                    RenameTestCM.IsEnabled = true;
                    RemoveTestCM.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_MouseRightButtonDown ---> Failed Because of error : " + ex.ToString());
            }
}

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                HandleTestList();
                if (listView.SelectedItem != null)
                {
                    if (e.Key == Key.Delete)
                    {
                        RemoveItem();
                    }
                    else if (Keyboard.IsKeyDown(Key.C) && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        CopyItem();
                    }
                    else if (Keyboard.IsKeyDown(Key.X) && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        CutItem();
                    }
                    else if (Keyboard.IsKeyDown(Key.V) && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        PasteItem();
                    }
                    else if (e.Key == Key.Escape)
                    {
                        listView.SelectedIndex = -1;
                        _commandCounter = CommandCounter;
                        edit = false;
                        CommandsComboBox.Text = "";
                        TargetTB.Text = "";
                        TargetTB.IsEnabled = true;
                        ValueTB.Text = "";
                        ValueTB.IsEnabled = true;
                        DescriptionTB.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("ListView_KeyDown ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void TestCaseListView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                HandleTestList();
                if (listView.SelectedItem != null)
                {
                    if (e.Key == Key.Delete)
                    {
                        RemoveTestCase();
                    }
                    else if (Keyboard.IsKeyDown(Key.C) && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        CopyTestCase();
                    }
                    else if (Keyboard.IsKeyDown(Key.X) && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        CutTestCase();
                    }
                    else if (Keyboard.IsKeyDown(Key.V) && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        PasteTestCase();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_KeyDown ---> Failed Because of error : " + ex.ToString());
            }
        }

        public Point startPoint = new Point();
        public int startIndex = -1;

        public static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleTestList();
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                HandleTestList();

                // Get the current mouse position
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if ((e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                            && handleRightClickBugListview && handleRightClickBugListview)
                {
                    // Get the dragged ListViewItem
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem == null) return;            // Abort
                                                                 // Find the data behind the ListViewItem
                    Commands item = (Commands)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    if (item == null) return;                    // Abort
                                                                 // Initialize the drag & drop operation
                    startIndex = listView.SelectedIndex;
                    DataObject dragData = new DataObject("Commands", item);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("ListView_MouseMove ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent("Commands") || sender != e.Source)
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("ListView_DragEnter ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            try
            {
                index = -1;

                if (e.Data.GetDataPresent("Commands") && sender == e.Source)
                {
                    // Get the drop ListViewItem destination
                    ListView listView1 = sender as ListView;
                    ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem == null)
                    {
                        // Abort
                        e.Effects = DragDropEffects.None;
                        return;
                    }
                    // Find the data behind the ListViewItem
                    Commands item = (Commands)listView1.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    // Move item into observable collection 
                    // (this will be automatically reflected to lstView.ItemsSource)
                    e.Effects = DragDropEffects.Move;
                    index = ListDB.IndexOf(item);
                    if (startIndex >= 0 && index >= 0)
                    {
                        ListDB.Move(startIndex, index);
                        var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                        if (obj != null) obj.TestValue = new ObservableCollection<Commands>(ListDB);
                    }
                    //startIndex = -1;         //Done!
                    for (int i = 1; i <= ListDB.Count; i++)
                    {
                        var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                        if (obj != null) obj.TestValue.ElementAt(i - 1).Number = i;
                    }
                    var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    ListDB = new ObservableCollection<Commands>(obj1.TestValue);
                    listView.ItemsSource = ListDB;
                    TestCaseListView.ItemsSource = TestList;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view1 = CollectionViewSource.GetDefaultView(ListDB);
                    view1.Refresh();
                    listView.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("ListView_Drop ---> Failed Because of error : " + ex.ToString());
            }
        }

        public Point startPoint1 = new Point();
        public int startIndex1 = -1;

        public static T FindAnchestor1<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void TestCaseListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleTestList();
            startPoint1 = e.GetPosition(null);
        }

        private void TestCaseListView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                HandleTestList();
                // Get the current mouse position
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint1 - mousePos;

                if ((e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                            && handleRightClickBugTestlist && handleRightClickBugListview)
                {
                    // Get the dragged ListViewItem
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem == null) return;            //Abort
                                                                 //Find the data behind the ListViewItem
                    TestSuit item = (TestSuit)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    if (item == null) return;                    //Abort
                                                                 //Initialize the drag & drop operation
                    startIndex1 = listView.SelectedIndex;
                    DataObject dragData = new DataObject("TestSuit", item);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_MouseMove ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void TestCaseListView_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent("TestSuit") || sender != e.Source)
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_DragEnter ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void TestCaseListView_Drop(object sender, DragEventArgs e)
        {
            try
            {
                index = -1;

                if (e.Data.GetDataPresent("TestSuit") && sender == e.Source)
                {
                    // Get the drop ListViewItem destination
                    ListView listView1 = sender as ListView;
                    ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem == null)
                    {
                        // Abort
                        e.Effects = DragDropEffects.None;
                        return;
                    }
                    // Find the data behind the ListViewItem
                    TestSuit item = (TestSuit)listView1.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    // Move item into observable collection 
                    // (this will be automatically reflected to lstView.ItemsSource)
                    e.Effects = DragDropEffects.Move;
                    index = TestList.IndexOf(item);
                    if (startIndex1 >= 0 && index >= 0)
                    {
                        TestList.Move(startIndex1, index);
                    }
                    startIndex1 = -1;        // Done!
                    for (int i = 1; i <= TestList.Count; i++)
                    {
                        TestList.ElementAt(i - 1).TestNumber = i;
                    }
                    var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    if (obj1 != null) obj1.IsSaved = false;
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (Brush)bc.ConvertFrom("#FFFF6565");
                    HandleTestList();
                    edit = false;
                    listView.ItemsSource = ListDB;
                    TestCaseListView.ItemsSource = TestList;
                    ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                    view.Refresh();
                    ICollectionView view1 = CollectionViewSource.GetDefaultView(ListDB);
                    view1.Refresh();
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_Drop ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void ListViewContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            handleRightClickBugListview = false;
            handleRightClickBugTestlist = false;
        }

        private void TestCaseContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            handleRightClickBugListview = false;
            handleRightClickBugTestlist = false;
        }

        private void ListViewContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            handleRightClickBugListview = true;
            handleRightClickBugTestlist = true;
        }

        private void TestCaseContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            handleRightClickBugListview = true;
            handleRightClickBugTestlist = true;
        }

        string suittemp;
        int indexError = 0;
        double step;
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateAckDialog();
                string AllSuit = "C:\\run-test-selenium\\Source\\Test-suits\\AllSuits.txt";
                List<string> Suits = File.ReadLines(@AllSuit).ToList();
                PBar.Maximum = mainWindow.Width;
                step = mainWindow.Width / Suits.Count;
                PBar.Visibility = Visibility.Visible;
                PBar.Value = 0;
                PBar.Opacity = 0.3;
                await Task.Run(() => updateMethod());
                PBar.Opacity = 0;
                PBar.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Log.Items.Add("Update_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        bool notConvertedTest = false;
        private async Task updateMethod()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    BL.BuildFile _buildFile = new BL.BuildFile();
                }));
                if (updateAck)
                {
                    string AllSuit = "C:\\run-test-selenium\\Source\\Test-suits\\AllSuits.txt";
                    List<string> Suits = File.ReadLines(@AllSuit).ToList();
                    /*List<string> builtSuits = Directory.GetFiles(@"C:\\run-test-selenium\\Source\\Test-suits-ubuntu", "*", SearchOption.AllDirectories).ToList();
                    for (int j = 0; j < builtSuits.Count; j++)
                        builtSuits[j] = FindBetween(builtSuits[j], "Test-suits-ubuntu\\", "_ubuntu.py");
                    var needsBuild = Suits.Except(builtSuits).ToList();
                    if (needsBuild.Count != 0)
                    {
                        notConvertedTest = true;
                        for (int k = 0; k < needsBuild.Count; k++)
                        {
                            string tempneedsBuild = "C:\\seleniums\\Test_suits\\" + needsBuild[k] + "\\suit";
                            string needsBuildSuit = File.ReadAllText(@tempneedsBuild);
                            List<string> needsBuildTestCaseDR = TestCase(needsBuildSuit, true);
                            for (int k1 = 0; k1 < needsBuildTestCaseDR.Count; k1++)
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() => {
                                    BL.OldIDEConverter.openOldTestCase("C:\\seleniums\\" + needsBuildTestCaseDR[k1].ToString() + ".html");
                                    if (!File.Exists("C:\\run-test-selenium\\Source\\" + needsBuildTestCaseDR[k1].ToString() + ".py"))
                                        TestMethod("C:\\run-test-selenium\\Source\\" + needsBuildTestCaseDR[k1].ToString() + ".py");
                                }));
                            }
                        }
                        notConvertedTest = false;
                    }*/
                    for (int i = 0; i < Suits.Count; i++)
                    {
                        string initJ = null;
                        string temp1 = "C:\\seleniums\\Test_suits\\" + Suits[i] + "\\suit";
                        string temp2 = "C:\\run-test-selenium\\Source\\Test-suits\\" + Suits[i] + "\\suit";
                        Directory.CreateDirectory("C:\\run-test-selenium\\Source\\Test-suits\\" + Suits[i]);
                        string Suit = File.ReadAllText(@temp1);

                        //notConvertedTest = true;

                        List<string> TestCaseDR = TestCase(Suit, true);
                        List<string> TestCaseNames = TestCase(Suit, false);

                        //notConvertedTest = false;

                        string StaticCodeNew = File.ReadAllText(@"StaticCode.py").Replace("    ", "\t").Replace("testSuit", Suits[i]).Replace("tableWidth", (_testCaseNameCount).ToString());
                        int splitterIndex = StaticCodeNew.IndexOf("#bodyCode#");
                        string StaticCodeNew_First = StaticCodeNew.Substring(0, splitterIndex);
                        string StaticCodeNew_Last = StaticCodeNew.Remove(0, splitterIndex + 11);

                        string StaticCodeNew_ubuntu = File.ReadAllText(@"StaticCode_ubuntu.py").Replace("    ", "\t").Replace("testSuit", Suits[i]).Replace("tableWidth", (_testCaseNameCount).ToString());
                        int splitterIndex_ubuntu = StaticCodeNew_ubuntu.IndexOf("#bodyCode#");
                        string StaticCodeNew_First_ubuntu = StaticCodeNew_ubuntu.Substring(0, splitterIndex_ubuntu);
                        string StaticCodeNew_Last_ubuntu = StaticCodeNew_ubuntu.Remove(0, splitterIndex_ubuntu + 11);

                        string tempSuit = StaticCodeNew_First;
                        string tempSuit_ubuntu = StaticCodeNew_First_ubuntu;
                        for (int j = 0; j < TestCaseDR.Count; j++)
                        {
                            string temp3 = "C:\\run-test-selenium\\Source\\" + TestCaseDR[j] + ".py";
                            tempSuit += "\n";
                            tempSuit_ubuntu += "\n";
                            string testCaseName = TestCaseDR[j].Split(new[] { "\\" }, StringSplitOptions.None)[1].Replace("-", "_").Replace(".", "_").Replace(" ", "_").Replace("&", "_");
                            if (!File.Exists(@temp3))
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() => {
                                    openOldTestCase("C:\\seleniums\\" + TestCaseDR[j].ToString() + ".html");
                                    TestMethod("C:\\run-test-selenium\\Source\\" + TestCaseDR[j].ToString() + ".py");
                                }));
                            }
                            string tempSuit2 = File.ReadAllText(@temp3);
                            int classStartLine = tempSuit2.IndexOf(":\r\n\t#");
                            if (classStartLine == -1)
                                classStartLine = tempSuit2.IndexOf(":\n\t#");
                            tempSuit2 = "\r\nclass " + testCaseName + ":" + tempSuit2.Remove(0, classStartLine + 1);
                            tempSuit += tempSuit2.Replace("\t", "\t\t").Replace("\r\nclass ", "\r\n\tclass ").Replace(testCaseName + ":", testCaseName + ":\r\n\t\tlog.debug(\"{:<8} {:<" + (_testCaseNameCount + 8) + "} {:<18}\".format('|  #" + (j + 1) + "', '|  " + testCaseName + "', '|  is running ...  |'))\r\n\t\tTestCases.add_row(['" + (j + 1) + "', '" + testCaseName + "', 'Failure'])");
                            tempSuit += "\t\tTestCases.del_row(-1)\r\n\t\tTestCases.add_row(['" + (j + 1) + "', '" + testCaseName + "', 'Success'])\r\n";
                            tempSuit_ubuntu += tempSuit2.Replace("\t", "\t\t").Replace("\r\nclass ", "\r\n\tclass ").Replace(testCaseName + ":", testCaseName + ":\r\n\t\tlog.debug(\"{:<8} {:<" + (_testCaseNameCount + 8) + "} {:<18}\".format('|  #" + (j + 1) + "', '|  " + testCaseName + "', '|  is running ...  |'))\r\n\t\tTestCases.add_row(['" + (j + 1) + "', '" + testCaseName + "', 'Failure'])");
                            tempSuit_ubuntu += "\t\tTestCases.del_row(-1)\r\n\t\tTestCases.add_row(['" + (j + 1) + "', '" + testCaseName + "', 'Success'])\r\n";
                            initJ = temp3;
                        }
                        suittemp = initJ + " in " + Suits[i];
                        string A = "C:\\run-test-selenium\\Source\\Test-suits\\" + Suits[i] + "\\" + Suits[i] + ".py";
                        string A_ubuntu = "C:\\run-test-selenium\\Source\\Test-suits-ubuntu\\" + Suits[i] + "_ubuntu.py";
                        FileInfo file1 = new FileInfo(@A);
                        FileInfo file1_ubuntu = new FileInfo(@A_ubuntu);
                        StreamWriter tempSuit1 = file1.CreateText();
                        StreamWriter tempSuit1_ubuntu = file1_ubuntu.CreateText();
                        tempSuit += StaticCodeNew_Last;
                        tempSuit = tempSuit.Replace("\t\t\t\t", "\t\t\t");
                        tempSuit = tempSuit.Replace("\t\t\t\t\t", "\t\t\t\t");
                        tempSuit = tempSuit.Replace("\t\t\t\t\t\t", "\t\t\t\t\t");
                        tempSuit1.WriteLine(tempSuit);
                        tempSuit1.Close();

                        tempSuit_ubuntu += StaticCodeNew_Last_ubuntu;
                        tempSuit_ubuntu = tempSuit_ubuntu.Replace("\t\t\t\t", "\t\t\t");
                        tempSuit_ubuntu = tempSuit_ubuntu.Replace("\t\t\t\t\t", "\t\t\t\t");
                        tempSuit_ubuntu = tempSuit_ubuntu.Replace("\t\t\t\t\t\t", "\t\t\t\t\t");
                        tempSuit1_ubuntu.WriteLine(tempSuit_ubuntu);
                        tempSuit1_ubuntu.Close();

                        string[] fixDates = File.ReadAllLines(@A_ubuntu);
                        int _currentLine = 0;
                        string className = "";
                        using (StreamWriter writer = new StreamWriter(@A_ubuntu))
                        {
                            for (int currentLine = 0; currentLine < fixDates.Length - 4; currentLine++)
                            {
                                int temp = 0;
                                if (fixDates[currentLine].Contains("class "))
                                {
                                    className = FindBetween(fixDates[currentLine], "class ", ":");
                                    if (fixDates[currentLine].Contains("class downloadQoarri"))
                                    {
                                        temp = currentLine;
                                    }
                                }
                                if (fixDates[currentLine].Contains("class downloadQoarri"))
                                {
                                    while (!fixDates[currentLine + 1].Contains("class "))
                                    {
                                        if (currentLine == temp + 3)
                                        {
                                            writer.WriteLine("\t\t# 2 | storeEval");
                                            writer.WriteLine("\t\tStoreEvalDB.vars[\"allText\"] = \"\"");
                                            writer.WriteLine("\t\t# Description: None");
                                        }
                                        if (fixDates[currentLine].Contains(" | sendKeys"))
                                        {
                                            string element = FindBetween(fixDates[currentLine + 1], " = ", ")") + ")";
                                            string elementValue;
                                            elementValue = FindBetween(fixDates[currentLine + 3], "send_keys(", ")");
                                            if (elementValue.Contains("Keys.ENTER"))
                                            {
                                                writer.WriteLine("\t\t# 5 | runScript");
                                                writer.WriteLine("\t\tdriver.execute_script('var ele=arguments[0]; ele.innerHTML += \"\\\\r\"; ', " + element + ")");
                                                writer.WriteLine("\t\t# Description: None");
                                            }
                                            else
                                            {
                                                writer.WriteLine("\t\t# 5 | runScript");
                                                writer.WriteLine("\t\tdriver.execute_script('var ele=arguments[0]; ele.innerHTML += " + elementValue.Replace("\'", "\"").Replace("\" + str(", "' + str(").Replace("]) + \"", "]) + '").Replace(")) + \"", ")) + '")/**/ + "; ', " + element + ")");
                                                writer.WriteLine("\t\t# Description: None");
                                            }
                                            currentLine += 5;
                                        }
                                        else
                                        {
                                            writer.WriteLine(fixDates[currentLine].Replace("highlight", "# highlight"));
                                            currentLine++;
                                        }
                                    }
                                    writer.WriteLine(fixDates[currentLine]);
                                }

                                if (fixDates[currentLine].Contains("| type") && fixDates[currentLine + 4].Contains("/"))
                                {
                                    string element = FindBetween(fixDates[currentLine + 1], " = ", ")") + ")";
                                    string elementValue;
                                    if (fixDates[currentLine + 4].Contains("'"))
                                    {
                                        elementValue = FindBetween(fixDates[currentLine + 4], "send_keys('", "')").Replace("\"", "\\\"");
                                    }
                                    else
                                    {
                                        elementValue = FindBetween(fixDates[currentLine + 4], "send_keys(\"", "\")").Replace("\"", "\\\"");
                                    }
                                    writer.WriteLine("\t\t# 5 | runScript");
                                    writer.WriteLine("\t\tdriver.execute_script('arguments[0].focus();', " + element + ")");
                                    writer.WriteLine("\t\t# Description: None");
                                    writer.WriteLine("\t\t# 2 | pause");
                                    writer.WriteLine("\t\ttime.sleep(3)");
                                    writer.WriteLine("\t\t# Description: None");
                                    writer.WriteLine("\t\t# 5 | runScript");
                                    writer.WriteLine("\t\tdriver.execute_script('arguments[0].removeAttribute(\"value\");', " + element + ")");
                                    writer.WriteLine("\t\t# Description: None");
                                    writer.WriteLine("\t\t# 2 | pause");
                                    writer.WriteLine("\t\ttime.sleep(3)");
                                    writer.WriteLine("\t\t# Description: None");
                                    writer.WriteLine("\t\t# 5 | runScript");
                                    writer.WriteLine("\t\tdriver.execute_script('arguments[0].setAttribute(\"value\", \"" + elementValue.Replace("[\\\"", "[\"").Replace("\\\"])", "\"])") + "\");', " + element + ")");
                                    writer.WriteLine("\t\t# Description: None");
                                    writer.WriteLine("\t\t# 2 | pause");
                                    writer.WriteLine("\t\ttime.sleep(2)");
                                    writer.WriteLine("\t\t# Description: None");
                                    writer.WriteLine("\t\t# 5 | runScript");
                                    writer.WriteLine("\t\tdriver.execute_script(\"arguments[0].click();\", " + element + ")");
                                    writer.WriteLine("\t\t# Description: None");
                                    currentLine += 6;
                                }
                                else if (fixDates[currentLine].Contains("number_of_windows_to_be") || (fixDates[currentLine].Contains("window_handles") && fixDates[currentLine + 3].Contains("document.querySelector('print-preview-app').shadowRoot"))
                                     || (fixDates[currentLine].Contains("window_handles") && fixDates[currentLine - 9].Contains("document.querySelector('print-preview-app').shadowRoot")) || fixDates[currentLine].Contains("document.querySelector('print-preview-app').shadowRoot"))
                                {
                                    writer.WriteLine(fixDates[currentLine].Replace("\t\t", "\t\t# "));
                                }
                                else if (fixDates[currentLine].Contains("\thighlight"))
                                {
                                    writer.WriteLine(fixDates[currentLine].Replace("highlight", "# highlight"));
                                }
                                else
                                {
                                    writer.WriteLine(fixDates[currentLine]);
                                }
                                _currentLine = currentLine;
                            }
                            writer.WriteLine(fixDates[_currentLine + 1]);
                            writer.WriteLine(fixDates[_currentLine + 2]);
                            writer.WriteLine(fixDates[_currentLine + 3]);
                            writer.WriteLine(fixDates[_currentLine + 4]);
                        }
                        indexError = i;
                        Application.Current.Dispatcher.Invoke(new Action(() => { PBar.Value += step; }));
                    }
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        Log.Items.Add("Successfully updated");
                    }));
                    updateAck = false;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("Update_Click ---> Failed Because of error : " + ex.ToString());
            }
        }

        public List<string> TestCase(string suit, bool wich)
        {
            string[] temp = null;
            if (suit.Contains("</b></td></tr>\r\n"))
            {
                suit = FindBetween(suit, "</b></td></tr>\r\n", "\r\n</tbody></table>");
                temp = suit.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (suit.Contains("</b></td></tr>\n"))
            {
                suit = FindBetween(suit, "</b></td></tr>\n", "\n</tbody></table>");
                temp = suit.Split(new string[] { "\n" }, StringSplitOptions.None);
            }
            int lines = suit.Split(new string[] { "\n" }, StringSplitOptions.None).Length;
            List<string> temp2 = new List<string>();
            List<string> temp3 = new List<string>();
            if (wich)
            {
                for (int i = 0; i < lines; i++)
                {
                    string temp4 = FindBetween(temp[i], "<tr><td><a href=\"../../", "\">").Replace(".html", "").Replace(".htm", "");
                    if (temp4.Contains("setGeneralPort") && temp4.Contains("setUploadPath") && temp4.Contains("pause2s"))
                        continue;
                    else
                    {
                        if (notConvertedTest)
                            temp2.Add(temp4.Replace("/", "\\"));
                        else
                            temp2.Add(temp4.Replace(".", "_").Replace("/", "\\"));
                    }
                    if (temp4.Contains("ezharPishAzVorud3"))
                    {

                    }
                }
                return temp2;
            }
            else
            {
                _testCaseNameCount = 32;
                for (int i = 0; i < lines; i++)
                {
                    string temp5 = FindBetween(temp[i], "\">", "</a></td></tr>");
                    if (temp5.Contains("setGeneralPort") && temp5.Contains("setUploadPath") && temp5.Contains("pause2s"))
                        continue;
                    else
                    {
                        if (notConvertedTest)
                            temp3.Add(temp5);
                        else
                            temp3.Add(temp5.Replace(".", "_"));
                    }
                    if (temp5.Contains("ezharPishAzVorud3"))
                    {

                    }
                    _testCaseNameCount = (temp3[i].Length > _testCaseNameCount) ? temp3[i].Length : _testCaseNameCount;
                }
                return temp3;
            }
        }

        private void CopyLogItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                multiplication = Multiplication.copy;
                CopiedLogItem = Log.SelectedItems[0].ToString();
                Clipboard.SetText(CopiedLogItem);
            }
            catch (Exception ex)
            {
                Log.Items.Add("CopiedLogItem ---> Failed Because of error : " + ex.ToString());
            }
        }
        #endregion


        /////////OldIDEConverter///////
        #region OldIDEConverter
        bool isSuit = false;
        public async void openOldTestCase(string caseAddress)
        {
            try
            {
                List<string> oldCase = File.ReadLines(@caseAddress).ToList();
                string[] address = caseAddress.Split(new[] { "\\" }, StringSplitOptions.None);
                string caseName = address[address.Length - 1].Remove(address[address.Length - 1].IndexOf('.'), address[address.Length - 1].Length - address[address.Length - 1].IndexOf('.'));
                string caseFolderName = address[address.Length - 2];
                oldCase.RemoveRange(0, oldCase.IndexOf("</thead><tbody>") + 1);
                oldCase.Insert(0, caseFolderName);
                oldCase.Insert(0, caseName);
                await Task.Run(() => TestCaseConverter(oldCase));
            }
            catch (Exception ex)
            {
                Log.Items.Add("openOldTestCase ---> Failed Because of error : " + ex.ToString());
            }
        }
        public async void openOldTestSuit(string suitAddress)
        {
            try
            {
                isSuit = true;
                List<string> oldSuit = File.ReadLines(suitAddress).ToList();
                string[] address = suitAddress.Split(new[] { "\\" }, StringSplitOptions.None);
                MainWindow.ProjectName = address[address.Length - 2];
                oldSuit.RemoveRange(0, oldSuit.IndexOf("<tr><td><b>Test Suite</b></td></tr>") + 1);
                loadTestProgress.Visibility = Visibility.Visible;
                await Task.Run(() => TestSuitConverter(oldSuit));
                loadTestProgress.Visibility = Visibility.Hidden;
                ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                view2.Refresh();
                isSuit = false;
            }
            catch (Exception ex)
            {
                Log.Items.Add("openOldTestSuit ---> Failed Because of error : " + ex.ToString());
            }
        }

        public async Task TestCaseConverter(List<string> input)
        {
            CommandCounter = 0;
            int CommandCounter1 = 0;
            App.Current.Dispatcher.Invoke(delegate{
                ListDB.Clear();
            });
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "<tr>")
                {
                    CommandCounter++;
                    CommandCounter1++;
                    string tempCommand = FindBetween(input[i + 1], "<td>", "</td>");
                    if (tempCommand == "open2" || tempCommand == "open2AndWait")
                        tempCommand = "open";
                    if (tempCommand == "clickAndWait")
                        tempCommand = "click";
                    App.Current.Dispatcher.Invoke(delegate {
                        ListDB.Add(new Commands(CommandCounter1, tempCommand, FindBetween(input[i + 2], "<td>", "</td>").Replace("&quot;", "\"").Replace("&amp;", "&"), FindBetween(input[i + 3], "<td>", "</td>").Replace("&quot;", "\"").Replace("&amp;", "&"), FindBetween(input[i + 1], "<td>", "</td>") + Convert.ToString(CommandCounter1 + 1), "None"));
                    });
                    i += 4;
                }
            }
            testCaseCounter++;
            _testCaseCounter++;
            App.Current.Dispatcher.Invoke(delegate {
                TestList.Add(new TestSuit() { TestNumber = testCaseCounter, TestName = input[0], TestValue = new ObservableCollection<Commands>(ListDB), TestFolder = input[1], SavedPath = null, IsSaved = false });
                TestCaseListView.ItemsSource = TestList;
                listView.ItemsSource = ListDB;
            });
        }
        public async Task TestSuitConverter(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Contains("href="))
                {
                    string temp = "C:\\seleniums" + FindBetween(input[i], "../..", "\">").Replace("/", "\\");
                    openOldTestCase(temp);
                }
            }
        }
        #endregion

        /////////NewIDEConverter///////
        #region NewIDEConverter

        List<NewIDEType> mainList = new List<NewIDEType>();
        public async void openNewTestSuit(string caseAddress)
        {
            try
            {
                List<string> oldCase = File.ReadLines(@caseAddress).ToList();
                string[] address = caseAddress.Split(new[] { "\\" }, StringSplitOptions.None);
                string caseName = address[address.Length - 1].Remove(address[address.Length - 1].IndexOf('.'), address[address.Length - 1].Length - address[address.Length - 1].IndexOf('.'));
                string caseFolderName = address[address.Length - 2];
                oldCase.RemoveRange(0, oldCase.IndexOf("</thead><tbody>") + 1);
                oldCase.Insert(0, caseFolderName);
                oldCase.Insert(0, caseName);
                loadTestProgress.Visibility = Visibility.Visible;
                await Task.Run(() => NewTestCaseConverter(oldCase));
                loadTestProgress.Visibility = Visibility.Hidden;
                ICollectionView view = CollectionViewSource.GetDefaultView(ListDB);
                view.Refresh();
                ICollectionView view2 = CollectionViewSource.GetDefaultView(TestList);
                view2.Refresh();
            }
            catch (Exception ex)
            {
                Log.Items.Add("openNewTestSuit ---> Failed Because of error : " + ex.ToString());
            }
        }

        public async Task NewTestCaseConverter(List<string> input)
        {
            App.Current.Dispatcher.Invoke(delegate {
                ListDB.Clear();
            });

            //var myTestCases = input.Where(stringToCheck => stringToCheck.Contains("    \"name\": \"")).ToList();
            var testCaseList = input.Where(stringToCheck => stringToCheck.Contains("  \"tests\": [\"")).ToList();
            var _testCaseSequence = testCaseList[0].Replace("    \"tests\": [\"", "").Replace("\"]", "");
            string[] testCaseSequence = _testCaseSequence.Split(new string[] { "\", \"" }, StringSplitOptions.None);
            var testCaseSequenceORG = input.Where(stringToCheck => stringToCheck.Contains("  \"id\": \"")).ToList();
            int m = 0;
            while (m < testCaseSequenceORG.Count)
            {
                if (!testCaseSequence.Contains(FindBetween(testCaseSequenceORG[m], " \"id\": \"", "\",")))
                {
                    testCaseSequenceORG.RemoveAt(m);
                    continue;
                }
                m++;
            }

            List<Commands> tempCommands = new List<Commands>();
            string TestCaseID = "";
            string TestCaseNAME = "";

            for (int j = 0; j < testCaseSequence.Length; j++)
            {
                CommandCounter = 0;
                int CommandCounter1 = 0;
                tempCommands.Clear();
                int firstIndex = input.FindIndex(r => r.Contains(testCaseSequence[j]));

                int secondIndex;
                int secondIndexTemp = testCaseSequenceORG.FindIndex(stringToCheck => stringToCheck.Contains(testCaseSequence[j])) + 1;
                if (secondIndexTemp != testCaseSequenceORG.Count)
                {
                    string secondIndexID = FindBetween(testCaseSequenceORG[secondIndexTemp], " \"id\": \"", "\",");
                    secondIndex = input.FindIndex(r => r.Contains(secondIndexID));
                }
                else secondIndex = input.Count;

                var result = input.Skip(firstIndex + 1).Take(secondIndex - (firstIndex));

                var myCommands = result.Where(stringToCheck => stringToCheck.Contains("\"command\"")).ToList();
                var myTargets = result.Where(stringToCheck => stringToCheck.Contains("\"target\"")).ToList();
                var myValues = result.Where(stringToCheck => stringToCheck.Contains("\"value\"")).ToList();

                for (int i = 0; i < myCommands.Count; i++)
                {
                    TestCaseID = FindBetween(input[firstIndex], " \"id\": \"", "\",");
                    TestCaseNAME = FindBetween(input[firstIndex + 1], " \"name\": \"", "\",");

                    CommandCounter++;
                    CommandCounter1++;

                    var myCurrentCommand = FindBetween(myCommands[i], " \"command\": \"", "\",");
                    if (myCurrentCommand == "store") myCurrentCommand = "storeEval";

                    var myCurrentTarget = FindBetween(myTargets[i], " \"target\": \"", "\",");
                    if (myCurrentCommand == "storeEval")
                        myCurrentTarget = ConvertTextToIdeFormat(myCurrentTarget, false, false);

                    var myCurrentValue = FindBetween(myValues[i], " \"value\": \"", "\"");
                    if (myCurrentCommand == "empty") 
                        myCurrentValue = ConvertTextToIdeFormat(myCurrentValue, false, false);

                    App.Current.Dispatcher.Invoke(delegate {
                        tempCommands.Add(new Commands(CommandCounter1, myCurrentCommand, myCurrentTarget, myCurrentValue, myCurrentCommand + Convert.ToString(CommandCounter1 + 1), "None"));
                    });
                }
                mainList.Add(new NewIDEType(TestCaseID, TestCaseNAME, tempCommands));
                testCaseCounter++;
                _testCaseCounter++;
                App.Current.Dispatcher.Invoke(delegate {
                    ProjectName = input[0];
                    TestList.Add(new TestSuit() { TestNumber = testCaseCounter, TestName = TestCaseNAME, TestValue = new ObservableCollection<Commands>(tempCommands), TestFolder = null, SavedPath = null, IsSaved = false });
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                });
            }
        }
        #endregion

    }
}
