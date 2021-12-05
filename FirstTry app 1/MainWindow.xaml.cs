using System;
using System.Configuration;
using System.Collections.Specialized;
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
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Drawing;

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
        bool RunFromMiddle = false;
        bool saveUbuntu = false;
        public static bool TestSuitSaved = false;
        public static bool Continue = false;
        public static bool updateAck = false;
        public static bool pause = false;
        public int pausedCommandIndex;
        public int pausedCaseIndex;
        public static bool caseFinished = false;
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

        //runner
        IDictionary<string, dynamic> StoreEvalDB = new Dictionary<string, dynamic>();
        int[] storedCounter;
        int[] logCounter;
        BindingList<string> LoggerList = new BindingList<string>();

        enum WaitType { _single, _case };
        WaitType waitType;



        ChromeDriver driver;
        ChromeDriverService chromeservice;
        ChromeOptions options;
        DesiredCapabilities capabilities;

        int index;
        bool handleRightClickBugListview = true;
        bool handleRightClickBugTestlist = true;
        enum Multiplication { copy, cut };
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
            Resources["StoreEvalDB"] = StoreEvalDB;
            Resources["storedCounter"] = storedCounter;
            Resources["LoggerList"] = LoggerList;
            //Resources["Logger"] = Logger;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TestCaseListView.ItemsSource);
            view.Filter = UserFilter;
            Refrence.Text = "  Selenium WebDriver IDE\n  Version : 0.3.8";

            string startupPath = Environment.CurrentDirectory;
            options = new ChromeOptions();
            chromeservice = ChromeDriverService.CreateDefaultService();
            chromeservice.HideCommandPromptWindow = true; //hide console window
            options.AddArguments("start-maximized");
            options.AddExtension(startupPath + "\\data\\extention-test.crx");
            options.AddArguments("user-data-dir=" + startupPath + "\\data\\User Data");
            //var temp124 = ConfigurationManager.AppSettings.Get("IAIS");
        }
        ///////BUILD
        #region Save
        public void TestMethod(string A, bool ubuntuIsEnable)
        {
            if (A != null)
            {
                int _counter = 0;
                //FileInfo file = new FileInfo(@A);
                //file.Create();
                //StreamWriter TestFile = file.CreateText();
                string mainString_testCase = (ubuntuIsEnable) ? File.ReadAllText(@"data\StaticCode-Case.py") : File.ReadAllText(@"data\StaticCode_ubuntu-Case.py");
                //int mainStringSplitterIndex = mainString_testCase.IndexOf("#bodyCode#");
                string mainString = mainString_testCase;
                mainString += "\r\n\r\nclass StoreEvalDB:\r\n\tvars = {}\r\n";
                mainString += "\nclass " + TestList.ElementAt(_testCaseCounter - 1).TestName + ":\n";
                string tabNeededTemp = "";
                while (ListDB.Count > _counter)
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
                            string _wait = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(EC.presence_of_all_elements_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _wait += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _wait += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait;
                            break;
                        #endregion

                        #region ===> waitForElementVisible
                        case "waitForElementVisible":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementVisible\n";
                            string _wait3 = tabNeededTemp + "\tWebDriverWait(driver, 30).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait3 += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _wait3 += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _wait3 += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait3;
                            break;
                        #endregion

                        #region ===> waitForElementNotPresent
                        case "waitForElementNotPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                            string _wait2 = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(expected_conditions.invisibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait2 += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait2;
                            break;
                        #endregion

                        #region ===> waitForNotText
                        case "waitForNotText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForNotText\n";
                            string _waitForNotText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until_not(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                            _waitForNotText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForNotText;
                            break;
                        #endregion

                        #region ===> waitForText
                        case "waitForText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForText\n";
                            string _waitForText = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                            _waitForText += tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForText += tabNeededTemp + "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForText += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForText;
                            break;
                        #endregion

                        #region ===> waitForValue
                        case "waitForValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForValue\n";
                            string _waitForValue = tabNeededTemp + "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(expected_conditions.text_to_be_present_in_element_value((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
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
                            string _waitForWindowPresent = tabNeededTemp + "\twaitForWindowPresent10 = WebDriverWait(driver, 30).until(EC.new_window_is_opened())\n";
                            mainString += _waitForWindowPresent;
                            break;
                        #endregion

                        #region ===> waitForNumberOfWindowPresent
                        case "waitForNumberOfWindowPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForNumberOfWindowPresent\n";
                            string _waitForNumberOfWindowPresent = tabNeededTemp + "\tWebDriverWait(driver, 30).until(EC.number_of_windows_to_be(1))\n";
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
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ListDB.ElementAt(_counter).Value + ".text");
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
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
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
                            //Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"wicketpath\")");
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
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"innerHTML\")");
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
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"name\")");
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
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"id\")");
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
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"href\")");
                            break;
                        #endregion

                        #region ===> storeEval
                        case "storeEval":
                            needCotBefore = needCotAfter = false;
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeEval\n";
                            string _storeEval = tabNeededTemp + "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ConvertTextToIdeFormat(tempTarget, false, true) + "\n";
                            _storeEval += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeEval;
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value, ConvertTextToIdeFormat(tempTarget, false, true));
                            break;
                        #endregion

                        #region ===> storeElementPresent
                        case "storeElementPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeElementPresent\n";
                            string _storeElementPresent = tabNeededTemp + "\ttry:\n";
                            string exist;
                            _storeElementPresent += tabNeededTemp + "\t\tWebDriverWait(driver, 30).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _storeElementPresent += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = True\n";
                            _storeElementPresent += tabNeededTemp + "\texcept TimeoutException:\n";
                            _storeElementPresent += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = False\n";
                            _storeElementPresent += tabNeededTemp + "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeElementPresent;
                            //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value);
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
                            //StoreEvalDB.Add(_replace);
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

        public void TestSuitMethod(string B, bool ubuntuIsEnable)
        {
            if (B != null)
            {
                int _counter1 = 0;
                int _counter = 0;
                //FileInfo file = new FileInfo(@B);
                //StreamWriter TestFile = file.CreateText();
                string StaticCodeNew = (ubuntuIsEnable) ? File.ReadAllText(@"data\StaticCode.py").Replace("    ", "\t").Replace("testSuit", ProjectName).Replace("tableWidth", (_testCaseNameCount).ToString()) :
                     File.ReadAllText(@"data\StaticCode_ubuntu.py").Replace("    ", "\t").Replace("testSuit", ProjectName).Replace("tableWidth", (_testCaseNameCount).ToString());
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
                    mainString += "\n\t\tlog.debug(\"{:< 8}{:< 50}{:< 18}\".format('|  #" + TestList.ElementAt(_counter1).TestNumber + "', '|  " + TestList.ElementAt(_counter1).TestName + "', '|  is running ...  |'))\n";
                    mainString += "\n\t\tTestCases.add_row(['" + TestList.ElementAt(_counter1).TestNumber + "', '" + TestList.ElementAt(_counter1).TestName + "', 'Failure'])\n";
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
                                string _wait = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(EC.presence_of_all_elements_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _wait += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _wait += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait;
                                break;
                            #endregion

                            #region ===> waitForElementVisible
                            case "waitForElementVisible":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementVisible\n";
                                string _wait3 = tabNeededTemp + "\t\tWebDriverWait(driver, 30).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait3 += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _wait3 += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _wait3 += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait3;
                                break;
                            #endregion

                            #region ===> waitForElementNotPresent
                            case "waitForElementNotPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                                string _wait2 = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(expected_conditions.invisibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait2 += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait2;
                                break;
                            #endregion

                            #region ===> waitForNotText
                            case "waitForNotText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForNotText\n";
                                string _waitForNotText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until_not(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                                _waitForNotText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForNotText;
                                break;
                            #endregion

                            #region ===> waitForText
                            case "waitForText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForText\n";
                                string _waitForText = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
                                _waitForText += tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForText += tabNeededTemp + "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForText += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForText;
                                break;
                            #endregion

                            #region ===> waitForValue
                            case "waitForValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForValue\n";
                                string _waitForValue = tabNeededTemp + "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 30).until(expected_conditions.text_to_be_present_in_element_value((\"" + targetType + "\", \"" + tempTarget + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value, true, true) + "))\n";
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
                                string _waitForWindowPresent = tabNeededTemp + "\t\twaitForWindowPresent10 = WebDriverWait(driver, 30).until(EC.new_window_is_opened())\n";
                                mainString += _waitForWindowPresent;
                                break;
                            #endregion

                            #region ===> waitForNumberOfWindowPresent
                            case "waitForNumberOfWindowPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForNumberOfWindowPresent\n";
                                string _waitForNumberOfWindowPresent = tabNeededTemp + "\t\tWebDriverWait(driver, 30).until(EC.number_of_windows_to_be(1))\n";
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"wicketpath\")");
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"innerHTML\")");
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"name\")");
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"id\")");
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
                                //StoreEvalDB.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"href\")");
                                break;
                            #endregion

                            #region ===> storeEval
                            case "storeEval":
                                needCotBefore = needCotAfter = false;
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeEval\n";
                                string _storeEval = tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ConvertTextToIdeFormat(tempTarget, false, true) + "\n";
                                _storeEval += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeEval;
                                //StoreEvalDB.Add(_storeEval);
                                break;
                            #endregion

                            #region ===> storeElementPresent
                            case "storeElementPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeElementPresent\n";
                                string _storeElementPresent = tabNeededTemp + "\t\ttry:\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tWebDriverWait(driver, 30).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = True\n";
                                _storeElementPresent += tabNeededTemp + "\t\texcept TimeoutException:\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = False\n";
                                _storeElementPresent += tabNeededTemp + "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeElementPresent;
                                //StoreEvalDB.Add(_storeElementPresent);
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
                                //StoreEvalDB.Add(_replace);
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
                    mainString += "\n\t\tTestCases.del_row(-1)\n";
                    mainString += "\n\t\tTestCases.add_row(['" + TestList.ElementAt(_counter1).TestNumber + "', '" + TestList.ElementAt(_counter1).TestName + "', 'Success'])\n";
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
                            default:
                                //_commandCounter++;
                                _counter++;
                                continue;
                        }
                        _commandCounter++;
                        ListDB.Add(new Commands(_commandCounter, tabNeededTemp + lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2).Replace(" ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
                    }

                    else if (lines.ElementAt(_counter).Contains("# end"))
                    {
                        _commandCounter++;
                        ListDB.Add(new Commands(_commandCounter, lines.ElementAt(_counter).Replace("\t# ", "").Replace(" ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
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

                                default:
                                    //_commandCounter++;
                                    _counter++;
                                    continue;
                            }
                            _commandCounter++;
                            ListDB.Add(new Commands(_commandCounter, tabNeededTemp + lines.ElementAt(_counter).Remove(0, lines.ElementAt(_counter).IndexOf("| ") + 2).Replace(" ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
                        }

                        else if (lines.ElementAt(_counter).Contains("# end"))
                        {
                            _commandCounter++;
                            ListDB.Add(new Commands(_commandCounter, lines.ElementAt(_counter).Replace("\t# ", "").Replace(" ", ""), _currentTarget, _currentValue, _currentVariableName, _currentDescription, false));
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
            if (Keyboard.IsKeyDown(Key.S) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                SaveTestCase((bool)SaveTestCaseClick_ubuntu.IsChecked);
            }
            else if (Keyboard.IsKeyDown(Key.S) && Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift))
            {
                SaveAsTestCase((bool)SaveAsFileIcon_ubuntu.IsChecked);
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
            OpenFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF00C6FF");
        }

        private void OpenFile_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            OpenFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
        }

        private void SaveAsFile_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            SaveAsFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF00C6FF");
        }

        private void SaveAsFile_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            SaveAsFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
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
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF00C6FF");
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF3B3B");
                    }
                }
                else
                {
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF00C6FF");
                }
            }
            catch (Exception ex)
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
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
                    }
                }
                else
                {
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("SaveFile_MouseLeave ---> Failed Because of error : " + ex.ToString());
            }
        }

        private void SaveTestCaseClick_ubuntu_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveFile.StaysOpen = true;
        }

        private void SaveTestCaseClick_ubuntu_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveFile.StaysOpen = false;
        }
        private void SaveAsTestCaseClick_ubuntu_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveAsFile.StaysOpen = true;
        }

        private void SaveAsTestCaseClick_ubuntu_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveAsFile.StaysOpen = false;
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
            catch (Exception ex)
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
                        if (CommandsList[i, 0] == CommandsComboBox.Text)
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

                        if ((CommandsComboBox.Text == "runScript" && TargetTB.Text != "") || (CommandsComboBox.Text != "" && TargetTB.Text != "" && ValueTB.Text != ""/* && DescriptionTB.Text != ""*/))
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
                                ListDB.Add(new Commands(CommandCounter, tabNeeded + CommandsComboBox.Text, TargetTB.Text, ValueTB.Text, CommandsComboBox.Text + Convert.ToString(CommandCounter + 1), DescriptionTB.Text, false));
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
                            SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
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
                for (int i = 0; i < CommandsList.Length / 2; i++)
                {
                    if (CommandsList[i, 0] == CommandsComboBox.Text)
                    {
                        tempDiscription = CommandsList[i, 1];
                        for (int j = 0; j < CommandsList.Length / 2; j++)
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
                    case "runScript":
                    case "select":
                    case "selectByIndex":
                    case "selectByValue":
                    case "selectByVisibleText":
                    case "sendKeys":
                    case "storeElementPresent":
                    case "storeEval":
                    case "storeHref":
                    case "storeId":
                    case "storeInnerHTML":
                    case "storeName":
                    case "storeText":
                    case "storeValue":
                    case "storeWicketPath":
                    case "switch":
                    case "type":
                    case "waitForAttribute":
                    case "waitForNotText":
                    case "waitForNumberOfWindowPresent":
                    case "waitForText":
                    case "waitForValue":
                        ValueTB.IsEnabled = true;
                        ValueTBBorder.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFFFFF");

                        TargetTB.IsEnabled = true;
                        TargetTBBorder.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFFFFF");

                        if (TargetTB.Text == "None")
                            TargetTB.Text = "";
                        if (ValueTB.Text == "None")
                            ValueTB.Text = "";

                        break;

                    case "clearText":
                    case "click":
                    case "if":
                    case "open":
                    case "pause":
                    case "replace":
                    case "scrollInto":
                    case "waitForElementNotPresent":
                    case "waitForElementPresent":
                    case "waitForElementVisible":
                    case "waitForWindowPresent":
                    case "while":
                        ValueTB.Text = "None";
                        ValueTB.IsEnabled = false;
                        ValueTBBorder.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFEBE7EE");

                        TargetTB.IsEnabled = true;
                        TargetTBBorder.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFFFFF");

                        if (TargetTB.Text == "None")
                            TargetTB.Text = "";

                        break;

                    case "alert":
                    case "break":
                    case "close":
                    case "end":
                    case "failTest":
                    case "refresh":
                    case "switchToDefault":
                        TargetTB.Text = "None";
                        TargetTB.IsEnabled = false;
                        TargetTBBorder.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFEBE7EE");

                        ValueTB.Text = "None";
                        ValueTB.IsEnabled = false;
                        ValueTBBorder.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFEBE7EE");

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
                for (int i = 0; i < testCaseCounter; i++)
                {
                    if (!TestList.ElementAt(i).IsSaved)
                    {
                        ListViewItem item = GetListViewItem(i);
                        if (item != null)
                        {
                            var bc = new BrushConverter();
                            item.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
                        }
                    }
                    if (0 != _testCaseCounter)
                    {
                        ListViewItem CurrentTestCaseitem = TestCaseListView.ItemContainerGenerator.ContainerFromIndex(_testCaseCounter - 1) as ListViewItem;
                        if (CurrentTestCaseitem != null && CurrentTestCaseitem.Background != System.Windows.Media.Brushes.LightGreen && CurrentTestCaseitem.Background != System.Windows.Media.Brushes.LightPink) CurrentTestCaseitem.Background = System.Windows.Media.Brushes.Lavender;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("HandleTestList ---> Failed Because of error : " + ex.ToString());
            }

        }

        public void SaveTestCase(bool ubuntuIsEnable)
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
                    TestMethod(TestList.ElementAt(_testCaseCounter - 1).SavedPath, ubuntuIsEnable);
                    Log.Items.Add(TestList.ElementAt(_testCaseCounter - 1).TestName + " TestCase Saved ---> Successfully");
                    var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    if (obj1 != null) obj1.IsSaved = true;
                    var bc = new BrushConverter();
                    SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
                }
                else if (TestList.ElementAt(_testCaseCounter - 1).SavedPath == null)
                {
                    c = true;
                    SaveDialog();
                    if (sPath != null)
                    {
                        TestMethod(sPath, ubuntuIsEnable);
                        Log.Items.Add(TestList.ElementAt(_testCaseCounter - 1).TestName + " TestCase Saved ---> Successfully");
                        var obj1 = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                        if (obj1 != null) obj1.IsSaved = true;
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
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

        public void SaveAsTestCase(bool ubuntuIsEnable)
        {
            try
            {
                if (testCaseCounter == 0)
                    AddTestDialog();
                else
                {
                    //BL.BuildFile _buildFile = new BL.BuildFile();
                    SaveDialog();
                    TestMethod(sPath, ubuntuIsEnable);
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
                if (multiplication == Multiplication.copy)
                {
                    CommandCounter++;
                    CommandCounterTB.Text = Convert.ToString(CommandCounter);
                    ListDB.Add(new Commands(CommandCounter, CopiedItem.Command, CopiedItem.Target, CopiedItem.Value, CopiedItem.VariableName, CopiedItem.Description, false));
                    var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                    if (obj != null) obj.TestValue = new ObservableCollection<Commands>(ListDB);
                    HandleComboBox();
                    _commandCounter = CommandCounter;
                }
                else if (multiplication == Multiplication.cut)
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
                    ListDB.Add(new Commands(CommandCounter, CopiedItem.Command, CopiedItem.Target, CopiedItem.Value, CopiedItem.VariableName, CopiedItem.Description, false));
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
                SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
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
                if (obj1 != null) obj1.TestValue = new ObservableCollection<Commands>(ListDB);
                if (obj1 != null) obj1.IsSaved = false;
                var bc = new BrushConverter();
                SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
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
            if (IN.Substring(0, 1) == "'" && IN.Substring(IN.Length - 1, 1) == "'")
            {
                IN = IN.Remove(0, 1);
                IN = IN.Remove(IN.Length - 1, 1);
                OUT = IN;
            }
            if (IN.Substring(0, 1) == "\"" && IN.Substring(IN.Length - 1, 1) == "\"")
            {
                IN = IN.Remove(0, 1);
                IN = IN.Remove(IN.Length - 1, 1);
                OUT = IN;
            }
            if (!IN.Contains("str(StoreEvalDB.vars") && !IN.Contains(" or "))
            {
                return IN;
            }
            else
            {
                OUT = IN.Replace("' + str(StoreEvalDB.vars[\"", "${").Replace("\" + str(StoreEvalDB.vars[\"", "${").Replace("str(StoreEvalDB.vars[\"", "${").Replace("\"]) + '", "}").Replace("\"]) + \"", "}").Replace("\"]) + ", "}").Replace("\"])", "}").Replace("' or '", "*").Replace("\" or \"", "*").Replace("' or ", "*").Replace("\" or ", "*").Replace(" or '", "*").Replace(" or \"", "*").Replace(" or ", "*");
                return OUT;
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void switchTestCase(int index)
        {
            try
            {
                HandleTestList();
                if (TestCaseListView.Items.Count != 0)
                {
                    CurrentTestCase = (TestSuit)TestCaseListView.Items[index];
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
                            SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
                        }
                        else
                        {
                            var bc = new BrushConverter();
                            SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
                        }
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("TestCaseListView_MouseDoubleClick ---> Failed Because of error : " + ex.ToString());
            }
        }

        bool notConvertedTest = false;
        private async Task updateMethod()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
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

                        string StaticCodeNew = File.ReadAllText(@"data\StaticCode.py").Replace("    ", "\t").Replace("testSuit", Suits[i]).Replace("tableWidth", (_testCaseNameCount).ToString());
                        int splitterIndex = StaticCodeNew.IndexOf("#bodyCode#");
                        string StaticCodeNew_First = StaticCodeNew.Substring(0, splitterIndex);
                        string StaticCodeNew_Last = StaticCodeNew.Remove(0, splitterIndex + 11);

                        string StaticCodeNew_ubuntu = File.ReadAllText(@"data\StaticCode_ubuntu.py").Replace("    ", "\t").Replace("testSuit", Suits[i]).Replace("tableWidth", (_testCaseNameCount).ToString());
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
                                openOldTestCase("C:\\seleniums\\" + TestCaseDR[j].ToString() + ".html");
                                TestMethod("C:\\run-test-selenium\\Source\\" + TestCaseDR[j].ToString() + ".py", false);
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
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Log.Items.Add("Successfully updated");
                    }));
                    updateAck = false;
                }
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    PBar.Opacity = 0;
                    PBar.Visibility = Visibility.Hidden;
                }));
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

        public string ConvertPythonToCSharp(string IN)
        {
            try
            {
                string OUT = "";
                string _out = "";
                //IN = IN.Replace(" and ", " && ").Replace(" or ", " || ").Replace("StoreEvalDB.vars", "StoreEvalDB");
                var tmp = IN.Split(" and ");
                for (int i = 0; i <= Regex.Matches(IN, " and ").Count; i++)
                {
                    if (!tmp[i].Contains(" or "))
                    {
                        _out = "";
                        if (tmp[i].Contains(" not in "))
                        {
                            var _not_in = tmp[i].Split(" not in ");
                            string tmpstr1 = _not_in[0].Contains('(') ? _not_in[0].Substring(_not_in[0].LastIndexOf('(') + 1, _not_in[0].Length - _not_in[0].LastIndexOf('(') - 1) : _not_in[0];
                            _not_in[0] = _not_in[0].Replace(tmpstr1, "");
                            string tmpstr2 = _not_in[1].Contains(')') ? _not_in[1].Substring(0, _not_in[1].IndexOf(')')) : _not_in[1];
                            _not_in[1] = _not_in[1].Replace(tmpstr2, "");
                            tmp[i] = _not_in[0] + " !" + tmpstr2 + ".Contains(" + tmpstr1 + ")" + _not_in[1];
                        }
                        else if (tmp[i].Contains(" in "))
                        {
                            var _in = tmp[i].Split(" in ");
                            string tmpstr1 = _in[0].Contains('(') ? _in[0].Substring(_in[0].LastIndexOf('(') + 1, _in[0].Length - _in[0].LastIndexOf('(') - 1) : _in[0];
                            _in[0] = _in[0].Replace(tmpstr1, "");
                            string tmpstr2 = _in[1].Contains(')') ? _in[1].Substring(_in[1].IndexOf(')'), _in[1].Length - 1) : _in[1];
                            _in[1] = _in[1].Replace(tmpstr2, "");
                            tmp[i] = _in[0] + tmpstr2 + ".Contains(" + tmpstr1 + ")" + _in[1];
                        }
                        _out = i < tmp.Length - 1 ? tmp[i] + " && " : tmp[i];
                    }
                    else
                    {
                        _out = "";
                        var count = Regex.Matches(tmp[i], " or ").Count;
                        var tmp1 = tmp[i].Split(" or ");
                        for (int j = 0; j <= Regex.Matches(tmp[i], " or ").Count; j++)
                        {
                            if (tmp1[j].Contains(" not in "))
                            {
                                var _not_in = tmp1[j].Split(" not in ");
                                string tmpstr1 = _not_in[0].Contains('(') ? _not_in[0].Substring(_not_in[0].LastIndexOf('(') + 1, _not_in[0].Length - _not_in[0].LastIndexOf('(') - 1) : _not_in[0];
                                _not_in[0] = _not_in[0].Replace(tmpstr1, "");
                                string tmpstr2 = _not_in[1].Contains(')') ? _not_in[1].Substring(0, _not_in[1].IndexOf(')')) : _not_in[1];
                                _not_in[1] = _not_in[1].Replace(tmpstr2, "");
                                tmp1[j] = _not_in[0] + " !" + tmpstr2 + ".Contains(" + tmpstr1 + ")" + _not_in[1];
                            }
                            else if (tmp1[j].Contains(" in "))
                            {
                                var _in = tmp1[j].Split(" in ");
                                string tmpstr1 = _in[0].Contains('(') ? _in[0].Substring(_in[0].LastIndexOf('(') + 1, _in[0].Length - _in[0].LastIndexOf('(') - 1) : _in[0];
                                _in[0] = _in[0].Replace(tmpstr1, "");
                                string tmpstr2 = _in[1].Contains(')') ? _in[1].Substring(_in[1].IndexOf(')'), _in[1].Length - 1) : _in[1];
                                _in[1] = _in[1].Replace(tmpstr2, "");
                                tmp1[j] = _in[0] + tmpstr2 + ".Contains(" + tmpstr1 + ")" + _in[1];
                            }
                            _out += j < tmp1.Length - 1 ? tmp1[j] + " || " : tmp1[j];
                            _out += (i < tmp.Length - 1) && (j == tmp1.Length - 1) ? " && " : "";
                        }
                    }
                    OUT += _out.Replace("not ", "!").Replace("StoreEvalDB.vars", "StoreEvalDB");
                }

                return OUT;
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("ConvertPythonToCSharp ---> Failed in line " + line + " Because of error : " + ex.ToString());
                return "none";
            }
        }

        public bool ConditionParser(string IN)
        {
            try
            {
                bool tempBool = false;
                IN = IN.Replace("not in", "notIn");
                string insideCond = IN;
                int counter = insideCond.Split('(').Length - 1;
                //IN = IN.Replace(" not ", " not ");
                string[] oprators = new string[10] { " < ", " <= ", " > ", " >= ", " == ", " != ", " in ", " notIn ", " and ", " or " };
                //get inside condition
                for (int i = 0; counter > i; i++)
                {
                    string insideCondtemp;
                    List<int> startIndexes = new List<int>();
                    List<int> finalIndexes = new List<int>();
                    foreach (var c in insideCond.Select((value, index) => new { value, index }))
                    {
                        if (c.value == '(')
                        {
                            startIndexes.Add(c.index);
                        }
                        else if (c.value == ')')
                        {
                            finalIndexes.Add(c.index);
                        }
                    }
                    int startIndex = startIndexes.LastOrDefault();
                    int finalIndex = finalIndexes.FirstOrDefault(x => x > startIndex);
                    insideCondtemp = insideCond.Substring(startIndex + 1, finalIndex - startIndex - 1);
                    string insideCondtemp2 = insideCond.Substring(startIndex, finalIndex - startIndex + 1);
                    //check inside Condition
                    SortedDictionary<int, string> opPos = new SortedDictionary<int, string>();
                    //export ops
                    foreach (var x in oprators)
                    {
                        if (insideCondtemp.Contains(x))
                        {
                            opPos.Add(insideCondtemp.IndexOf(x), x);
                        }
                    }
                    //get inside cond result
                    string[] separatedOps = new string[2];

                    for (int j = 0; j < opPos.Count; j++)
                    {
                        if (opPos.ElementAt(j).Value != " or " && opPos.ElementAt(j).Value != " and ")
                        {
                            //separate ops
                            separatedOps = insideCondtemp.Split(new[] { opPos.ElementAt(j).Value }, StringSplitOptions.None);
                            separatedOps[1] = j != opPos.Count - 1 ? separatedOps[1].Split(new[] { opPos.ElementAt(j + 1).Value }, StringSplitOptions.None)[0] :
                                separatedOps[1];
                            if (j != 0 && separatedOps[0].Contains(opPos.ElementAt(j - 1).Value))
                                separatedOps[0] = separatedOps[0].Split(opPos.ElementAt(j - 1).Value)[1];
                            //check if it contains DB data
                            bool DBdata0 = false;
                            bool DBdata1 = false;
                            if (separatedOps[0].Contains("StoreEvalDB.vars"))
                            {
                                separatedOps[0] = FindBetween(separatedOps[0], "StoreEvalDB.vars[\"", "\"]");
                            }
                            if (separatedOps[1].Contains("StoreEvalDB.vars"))
                            {
                                separatedOps[1] = FindBetween(separatedOps[1], "StoreEvalDB.vars[\"", "\"]");
                            }
                            //check true or false
                            switch (opPos.ElementAt(j).Value)
                            {
                                case " < ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) < Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) < Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) < Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) < Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " <= ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) <= Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) <= Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) <= Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) <= Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " > ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) > Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) > Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) > Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) > Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " >= ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) >= Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) >= Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToInt32(separatedOps[0]) >= Convert.ToInt32(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToInt32(StoreEvalDB[separatedOps[0]]) >= Convert.ToInt32(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " == ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[0]] == StoreEvalDB[separatedOps[1]])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (separatedOps[0] == separatedOps[1])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (separatedOps[0] == StoreEvalDB[separatedOps[1]])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[0]] == separatedOps[1])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " != ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[0]] != StoreEvalDB[separatedOps[1]])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (separatedOps[0] != separatedOps[1])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (separatedOps[0] != StoreEvalDB[separatedOps[1]])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[0]] != separatedOps[1])
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " in ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[1]].Contains(StoreEvalDB[separatedOps[0]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (separatedOps[1].Contains(separatedOps[0]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[1]].Contains(separatedOps[0]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (separatedOps[1].Contains(StoreEvalDB[separatedOps[0]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " notIn ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[1]].Contains(StoreEvalDB[separatedOps[0]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (separatedOps[1].Contains(separatedOps[0]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (StoreEvalDB[separatedOps[1]].Contains(separatedOps[0]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (separatedOps[1].Contains(StoreEvalDB[separatedOps[0]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;
                            }
                            insideCondtemp = insideCondtemp.Replace(separatedOps[0] + opPos.ElementAt(j).Value + separatedOps[1], tempBool.ToString());
                        }
                    }

                    for (int j = 0; j < opPos.Count; j++)
                    {
                        if (opPos.ElementAt(j).Value == " or " || opPos.ElementAt(j).Value == " and ")
                        {
                            //separate ops
                            separatedOps = insideCondtemp.Split(new[] { opPos.ElementAt(j).Value }, StringSplitOptions.None);
                            separatedOps[1] = j != opPos.Count - 1 ? separatedOps[1].Split(new[] { opPos.ElementAt(j + 1).Value }, StringSplitOptions.None)[0] :
                                separatedOps[1];
                            if (j != 0 && separatedOps[0].Contains(opPos.ElementAt(j - 1).Value))
                                separatedOps[0] = separatedOps[0].Split(opPos.ElementAt(j - 1).Value)[1];
                            //check if it contains DB data
                            bool DBdata0 = false;
                            bool DBdata1 = false;
                            if (separatedOps[0].Contains("StoreEvalDB.vars"))
                            {
                                separatedOps[0] = FindBetween(separatedOps[0], "StoreEvalDB.vars[\"", "\"]");
                            }
                            if (separatedOps[1].Contains("StoreEvalDB.vars"))
                            {
                                separatedOps[1] = FindBetween(separatedOps[1], "StoreEvalDB.vars[\"", "\"]");
                            }
                            //Not effect
                            if (separatedOps[0].Contains("not "))
                            {
                                if (separatedOps[0].Contains("True") || separatedOps[0].Contains("true"))
                                {
                                    insideCondtemp = insideCondtemp.Replace(separatedOps[0], "False");
                                    separatedOps[0] = "False";
                                }
                                else
                                {
                                    insideCondtemp = insideCondtemp.Replace(separatedOps[0], "True");
                                    separatedOps[0] = "True";
                                }
                            }
                            if (separatedOps[1].Contains("not "))
                            {
                                if (separatedOps[1].Contains("True") || separatedOps[1].Contains("true"))
                                {
                                    insideCondtemp = insideCondtemp.Replace(separatedOps[1], "False");
                                    separatedOps[1] = "False";
                                }
                                else
                                {
                                    insideCondtemp = insideCondtemp.Replace(separatedOps[1], "True");
                                    separatedOps[1] = "True";
                                }
                            }
                            //check true or false
                            switch (opPos.ElementAt(j).Value)
                            {
                                case " or ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToBoolean(StoreEvalDB[separatedOps[0]]) || Convert.ToBoolean(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToBoolean(separatedOps[0]) || Convert.ToBoolean(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToBoolean(separatedOps[0]) || Convert.ToBoolean(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToBoolean(StoreEvalDB[separatedOps[0]]) || Convert.ToBoolean(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;

                                case " and ":
                                    if (DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToBoolean(StoreEvalDB[separatedOps[0]]) && Convert.ToBoolean(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToBoolean(separatedOps[0]) && Convert.ToBoolean(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (!DBdata0 && DBdata1)
                                    {
                                        if (Convert.ToBoolean(separatedOps[0]) && Convert.ToBoolean(StoreEvalDB[separatedOps[1]]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    else if (DBdata0 && !DBdata1)
                                    {
                                        if (Convert.ToBoolean(StoreEvalDB[separatedOps[0]]) && Convert.ToBoolean(separatedOps[1]))
                                            tempBool = true;
                                        else
                                            tempBool = false;
                                    }
                                    break;
                            }
                            insideCondtemp = insideCondtemp.Replace(separatedOps[0] + opPos.ElementAt(j).Value + separatedOps[1], tempBool.ToString());
                        }
                    }

                    insideCond = insideCond.Replace(insideCondtemp2, insideCondtemp);
                }

                if (insideCond.Contains(" and ") || insideCond.Contains(" or "))
                {
                    string insideCondtemp = insideCond;
                    string[] separatedOps = new string[2];

                    SortedDictionary<int, string> opPos = new SortedDictionary<int, string>();
                    //export ops
                    foreach (var x in oprators)
                    {
                        if (insideCondtemp.Contains(x))
                        {
                            opPos.Add(insideCondtemp.IndexOf(x), x);
                        }
                    }
                    for (int j = 0; j < opPos.Count; j++)
                    {
                        //separate ops
                        separatedOps = insideCondtemp.Split(new[] { opPos.ElementAt(j).Value }, StringSplitOptions.None);
                        separatedOps[1] = j != opPos.Count - 1 ? separatedOps[1].Split(new[] { opPos.ElementAt(j + 1).Value }, StringSplitOptions.None)[0] :
                            separatedOps[1];
                        if (j != 0 && separatedOps[0].Contains(opPos.ElementAt(j - 1).Value))
                            separatedOps[0] = separatedOps[0].Split(opPos.ElementAt(j - 1).Value)[1];
                        //Not effect
                        if (separatedOps[0].Contains("not "))
                        {
                            if (separatedOps[0].Contains("True") || separatedOps[0].Contains("true"))
                            {
                                insideCondtemp = insideCondtemp.Replace(separatedOps[0], "False");
                                separatedOps[0] = "False";
                            }
                            else
                            {
                                insideCondtemp = insideCondtemp.Replace(separatedOps[0], "True");
                                separatedOps[0] = "True";
                            }
                        }
                        if (separatedOps[1].Contains("not "))
                        {
                            if (separatedOps[1].Contains("True") || separatedOps[1].Contains("true"))
                            {
                                insideCondtemp = insideCondtemp.Replace(separatedOps[1], "False");
                                separatedOps[1] = "False";
                            }
                            else
                            {
                                insideCondtemp = insideCondtemp.Replace(separatedOps[1], "True");
                                separatedOps[1] = "True";
                            }
                        }
                        //check true or false
                        switch (opPos.ElementAt(j).Value)
                        {
                            case " or ":
                                if (Convert.ToBoolean(separatedOps[0]) || Convert.ToBoolean(separatedOps[1]))
                                    tempBool = true;
                                else
                                    tempBool = false;
                                break;

                            case " and ":
                                if (Convert.ToBoolean(separatedOps[0]) && Convert.ToBoolean(separatedOps[1]))
                                    tempBool = true;
                                else
                                    tempBool = false;
                                break;
                        }
                        insideCondtemp = insideCondtemp.Replace(separatedOps[0] + opPos.ElementAt(j).Value + separatedOps[1], tempBool.ToString());
                    }

                    insideCond = insideCondtemp;
                }
                return Convert.ToBoolean(insideCond);
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("ConditionParser ---> Failed in line " + line + " Because of error : " + ex.ToString());
                return false;
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
                {
                    Application.Current.Shutdown();
                    try
                    {
                        if (driver != null) driver.Quit();
                    }
                    catch { }
                }
            }
            else
            {
                Application.Current.Shutdown();
                try
                {
                    if (driver != null) driver.Quit();
                }
                catch { }
            }
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
            SaveTestCase((bool)SaveTestCaseClick_ubuntu.IsChecked);
        }

        private void SaveTestSuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFile.StaysOpen = false;

                //BL.BuildFile _buildFile = new BL.BuildFile();
                if (gPath != null)
                {
                    TestSuitMethod(gPath, (bool)SaveTestSuitClick_ubuntu.IsChecked);
                    Log.Items.Add(testCaseFileName + " TestSuit Saved ---> Successfully ");
                }
                else if (testCaseCounter != 0)
                {
                    SaveTestSuitDialog();
                    if (gPath != null)
                    {
                        TestSuitMethod(gPath, (bool)SaveTestSuitClick_ubuntu.IsChecked);
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
            SaveAsTestCase((bool)SaveAsFileIcon_ubuntu.IsChecked);
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
                        TestSuitMethod(gPath, (bool)SaveAsTestSuitClick_ubuntu.IsChecked);
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
                openFileDialog.Filter = "Python file (*.py)|*.py|HTML file (*.html)|*.html";
                OpenDialog();
                if (ok)
                {
                    for (int i = 0; i < _openedFiles; i++)
                    {
                        switch (_openFileNameArray[i].Split('.')[1])
                        {
                            case "py":
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
                                break;
                            case "html":
                                _openFileAddress = _openFileAddressArray[i];
                                _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                                _openFileName = _openFileNameArray[i];
                                _openFileName = _openFileName.Remove(_openFileName.IndexOf('.'), _openFileName.Length - _openFileName.IndexOf('.'));
                                string mystring1 = _openFileAddress.Replace("\\" + _openFileNameArray[i], "");
                                for (int j = mystring1.Length; j > 0; j--)
                                    mystring1 = mystring1.Substring(mystring1.IndexOf("\\") + 1);
                                FolderName = mystring1;
                                TestCaseListView.ItemsSource = TestList;
                                listView.ItemsSource = ListDB;
                                ICollectionView view1 = CollectionViewSource.GetDefaultView(ListDB);
                                view1.Refresh();
                                ICollectionView view21 = CollectionViewSource.GetDefaultView(TestList);
                                view21.Refresh();
                                if (_openFileAddress != null)
                                    openOldTestCase(_openFileAddress);
                                break;
                            default:
                                EmptyFieldtDialog();
                                break;
                        }
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
                openFileDialog.Filter = "Python file (*.py)|*.py|Old Selenium IDE output (suit.*)|suit|NewSelenium IDE output (*.side)|*.side";
                OpenDialog();
                if (ok)
                {
                    if (_openFileNameArray[0].Contains('.'))
                    {
                        switch (_openFileNameArray[0].Split('.')[1])
                        {
                            case "py":
                                testCaseCounter = 0;
                                _openFileAddress = _openFileAddressArray[0];
                                _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                                _openFileName = _openFileNameArray[0];
                                _openFileName = _openFileName.Replace(".py", "");
                                string mystring1 = _openFileAddress.Replace("\\" + _openFileName, "");
                                for (int j = mystring1.Length; j > 0; j--)
                                    mystring1 = mystring1.Substring(mystring1.IndexOf("\\") + 1);
                                FolderName = mystring1;
                                TestCaseListView.ItemsSource = TestList;
                                listView.ItemsSource = ListDB;
                                ICollectionView view1 = CollectionViewSource.GetDefaultView(ListDB);
                                view1.Refresh();
                                ICollectionView view21 = CollectionViewSource.GetDefaultView(TestList);
                                view21.Refresh();
                                OpenTestSuit(_openFileAddress);
                                TestCaseCounterTB.Text = Convert.ToString(testCaseCounter);
                                break;
                            case "side":
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
                                break;
                            default:
                                EmptyFieldtDialog();
                                break;
                        }
                    }
                    else
                    {
                        testCaseCounter = 0;
                        _openFileAddress = _openFileAddressArray[0];
                        _openFileAddress = _openFileAddress.Replace("\\\\", "\\");
                        _openFileName = _openFileNameArray[0];
                        _openFileName = _openFileName;
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
                    //BL.OpenFile _openFile = new BL.OpenFile();
                }
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("OpenTestSuit_Click ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        private async void RunTestSuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pause = false;
                await Task.Run(() =>
                {
                    runSuit(1);
                });
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("RunTestSuit_Click ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        private async void RunCurrent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pause = false;
                await Task.Run(() =>
                {
                    runCase(_testCaseCounter, 0);
                });
            }
            catch (Exception ex)
            {

                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("RunCurrent_Click ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        private async void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                pause = false;
                await Task.Run(() =>
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            runCase(pausedCaseIndex + 1, pausedCommandIndex);
                        }
                        else if (i == 1)
                        {
                            runSuit(pausedCaseIndex + 2);
                        }
                    }

                });
            }
            else
                pause = true;
        }

        public void AddTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ; AddTestCaseButton();
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

        private async void RunFromHereCM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TestCaseListView.SelectedItems.Count != 0)
                {
                    RunFromMiddle = true;
                    TestSuit selected = (TestSuit)TestCaseListView.SelectedItem;
                    await Task.Run(() =>
                    {
                        runSuit(selected.TestNumber);
                    });
                    RunFromMiddle = false;
                }
            }
            catch (Exception ex)
            {
                Log.Items.Add("RunFromHereCM_Click ---> Failed Because of error : " + ex.ToString());
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

        private void CopyStoredItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                multiplication = Multiplication.copy;
                CopiedLogItem = Stored.SelectedItems[0].ToString();
                Clipboard.SetText(CopiedLogItem);
            }
            catch (Exception ex)
            {
                Log.Items.Add("CopyStoredItem ---> Failed Because of error : " + ex.ToString());
            }
        }
        #endregion

        /////////LISTEVENTS///////
        #region LISTEVENTS
        private async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count != 0)
                {
                    if (waitType == WaitType._case)
                    {
                        bool passed = false;
                    }
                    CurrentCommand = (Commands)listView.SelectedItems[0];
                    waitType = WaitType._single;
                    await Task.Run(() =>
                    {
                        runCommand(CurrentCommand, _testCaseCounter, CurrentCommand.Number - 1);
                    });
                }
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("ListView_MouseDoubleClick ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        private void TestCaseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                HandleTestList();
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
                            SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
                        }
                        else
                        {
                            var bc = new BrushConverter();
                            SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
                        }
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF1C1C1C");
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
                    RenameTestCM.IsEnabled = false;
                    RemoveTestCM.IsEnabled = false;
                    RunFromHereCM.IsEnabled = false;
                }
                else
                {
                    //CopyTestCM.IsEnabled = true;
                    //CutTestCM.IsEnabled = true;
                    //PasteTestCM.IsEnabled = true;
                    RenameTestCM.IsEnabled = true;
                    RemoveTestCM.IsEnabled = true;
                    RunFromHereCM.IsEnabled = true;
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

        public System.Windows.Point startPoint = new System.Windows.Point();
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
                System.Windows.Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if ((e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                            && handleRightClickBugListview && handleRightClickBugListview)
                {
                    // Get the dragged ListViewItem
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem != null && listViewItem.Background != System.Windows.Media.Brushes.LightGreen && listViewItem.Background != System.Windows.Media.Brushes.LightPink) listViewItem.Background = System.Windows.Media.Brushes.Lavender;
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
                    if (listViewItem != null) listViewItem.Background = System.Windows.Media.Brushes.White;
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
                        if (obj != null)
                        {
                            obj.TestValue = new ObservableCollection<Commands>(ListDB);
                        }

                    }
                    //startIndex = -1;         //Done!
                    for (int i = 1; i <= ListDB.Count; i++)
                    {
                        var obj = TestList.FirstOrDefault(x => x.TestNumber == _testCaseCounter);
                        if (obj != null)
                        {
                            obj.TestValue.ElementAt(i - 1).Number = i;
                            int tabsNeeded = ListDB.Skip(0).Take(i - 1).Count(x => x.Command.Contains("while") || x.Command.Contains("if")) -
                                ListDB.Skip(0).Take(i - 1).Count(x => x.Command.Contains("end"));
                            obj.TestValue.ElementAt(i - 1).Command = obj.TestValue.ElementAt(i - 1).Command.Contains("end") ?
                                String.Concat(Enumerable.Repeat("\t", tabsNeeded - 1)) + obj.TestValue.ElementAt(i - 1).Command.Replace("\t", "") :
                                String.Concat(Enumerable.Repeat("\t", tabsNeeded)) + obj.TestValue.ElementAt(i - 1).Command.Replace("\t", "");
                        }
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
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("ListView_Drop ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        public System.Windows.Point startPoint1 = new System.Windows.Point();
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
                System.Windows.Point mousePos = e.GetPosition(null);
                Vector diff = startPoint1 - mousePos;

                if ((e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                            && handleRightClickBugTestlist && handleRightClickBugListview)
                {
                    // Get the dragged ListViewItem
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem != null && listViewItem.Background != System.Windows.Media.Brushes.LightGreen && listViewItem.Background != System.Windows.Media.Brushes.LightPink) listViewItem.Background = System.Windows.Media.Brushes.Lavender;
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
                    if (listViewItem != null) listViewItem.Background = System.Windows.Media.Brushes.White;
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
                    SaveFileIcon.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFF6565");
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


        private void listView_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                if (listView.SelectedItems.Count != 0)
                {
                    CurrentCommand = (Commands)listView.SelectedItems[0];
                    ListViewItem CurrentContainer = listView.ItemContainerGenerator.ContainerFromIndex(CurrentCommand.Number - 1) as ListViewItem;
                    if (CurrentContainer != null && CurrentContainer.Background != System.Windows.Media.Brushes.LightGreen && CurrentContainer.Background != System.Windows.Media.Brushes.LightPink) CurrentContainer.Background = System.Windows.Media.Brushes.Lavender;
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
        }
        private void listView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (listView.SelectedItems.Count != 0)
                {
                    CurrentCommand = (Commands)listView.SelectedItems[0];
                    ListViewItem CurrentContainer = listView.ItemContainerGenerator.ContainerFromIndex(CurrentCommand.Number - 1) as ListViewItem;
                    if (CurrentContainer != null) CurrentContainer.Background = System.Windows.Media.Brushes.White;
                }
            }
        }

        private void TestCaseListView_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

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
                App.Current.Dispatcher.Invoke(delegate
                {
                    ListDB.Clear();
                });
                Task.Run(() => TestCaseConverter(oldCase)).Wait();
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

            //CommandCounter = 0;
            int CommandCounter1 = 0;

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "<tr>")
                {
                    //CommandCounter++;
                    CommandCounter1++;
                    string tempCommand = FindBetween(input[i + 1], "<td>", "</td>");
                    if (tempCommand == "open2" || tempCommand == "open2AndWait")
                        tempCommand = "open";
                    if (tempCommand == "clickAndWait" || tempCommand == "clickAt")
                        tempCommand = "click";
                    if (tempCommand == "fireEvent")
                        tempCommand = "runScript";
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        ListDB.Add(new Commands(CommandCounter1, tempCommand, FindBetween(input[i + 2], "<td>", "</td>").Replace("&quot;", "\"").Replace("&amp;", "&"), FindBetween(input[i + 3], "<td>", "</td>").Replace("&quot;", "\"").Replace("&amp;", "&"), FindBetween(input[i + 1], "<td>", "</td>") + Convert.ToString(CommandCounter1 + 1), "None", false));
                    });
                    i += 4;
                }
            }
            testCaseCounter++;
            _testCaseCounter++;
            App.Current.Dispatcher.Invoke(delegate
            {
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
            App.Current.Dispatcher.Invoke(delegate
            {
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

                    App.Current.Dispatcher.Invoke(delegate
                    {
                        tempCommands.Add(new Commands(CommandCounter1, myCurrentCommand, myCurrentTarget, myCurrentValue, myCurrentCommand + Convert.ToString(CommandCounter1 + 1), "None", false));
                    });
                }
                mainList.Add(new NewIDEType(TestCaseID, TestCaseNAME, tempCommands));
                testCaseCounter++;
                _testCaseCounter++;
                App.Current.Dispatcher.Invoke(delegate
                {
                    ProjectName = input[0];
                    TestList.Add(new TestSuit() { TestNumber = testCaseCounter, TestName = TestCaseNAME, TestValue = new ObservableCollection<Commands>(tempCommands), TestFolder = null, SavedPath = null, IsSaved = false });
                    TestCaseListView.ItemsSource = TestList;
                    listView.ItemsSource = ListDB;
                });
            }
        }
        #endregion

        /////////Runner///////
        ListViewItem lvitem;
        #region Runner
        public static Dictionary<int, bool> runnerInWhile = new Dictionary<int, bool>();
        public static Dictionary<int, bool> runnerInIf = new Dictionary<int, bool>();
        public static Dictionary<int, bool> runnerEnd = new Dictionary<int, bool>();
        //
        int startOfWhile = 0;
        int commandCounterWhile;
        bool _break = false;
        Commands currentWhile;
        int endPos;
        ListViewItem lvitemWhile;
        ListViewItem lvitemIf;
        public async Task runCommand(Commands thisCommand, int caseIndex, int commandIndex)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if(lvitem != null)
                    {
                        lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                        string itemBackGround2 = lvitem.Background.ToString();
                    }
                }));
                ListView temp2 = listView;
                try
                {
                    if (driver == null)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            loadDriverProgress.Visibility = Visibility.Visible;
                        }));
                        driver = new ChromeDriver(chromeservice, options);
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            loadDriverProgress.Visibility = Visibility.Hidden;
                        }));
                    }
                    string temp = driver.Url;
                }
                catch
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        loadDriverProgress.Visibility = Visibility.Visible;
                    }));
                    driver = new ChromeDriver(chromeservice, options);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        loadDriverProgress.Visibility = Visibility.Hidden;
                    }));
                }

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IJavaScriptExecutor jsExecutor = driver;
                //highligh css class
                jsExecutor.ExecuteScript("var styleContainer = document.createElement(\"style\"); styleContainer.innerHTML = \".myHighlight {background-color: rgba(255,255,0,0.9) !important;}\"; document.body.appendChild(styleContainer); ");

                Actions actions = new Actions(driver);

                if (lvitem != null && waitType == WaitType._single)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string itemBackGround = lvitem.Background.ToString();
                        var bc = new BrushConverter();
                        if (itemBackGround != "#00FFFFFF")
                            lvitem.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#00FFFFFF");
                    }));
                }


                ///////specifyTarget
                string targetType = "";
                if (thisCommand.Target.Contains('=') && !thisCommand.Target.Contains("//"))
                    targetType = thisCommand.Target.Split('=')[0];
                else if (!thisCommand.Target.Contains('=') || thisCommand.Target.Contains("//"))
                    targetType = "xpath";
                string tempTarget = "";
                switch (targetType)
                {
                    case "class":
                        targetType = "class name";
                        tempTarget = thisCommand.Target.Replace("class=", "");
                        break;
                    case "css":
                        targetType = "css selector";
                        tempTarget = thisCommand.Target.Replace("css=", "");
                        break;
                    case "id":
                        tempTarget = thisCommand.Target.Replace("id=", "");
                        break;
                    case "link":
                        targetType = "link text";
                        tempTarget = thisCommand.Target.Replace("link=", "");
                        break;
                    case "linkText":
                        targetType = "link text";
                        tempTarget = thisCommand.Target.Replace("link=", "");
                        break;
                    case "name":
                        tempTarget = thisCommand.Target.Replace("name=", "");
                        break;
                    case "partial":
                        targetType = "partial link text";
                        tempTarget = thisCommand.Target.Replace("partial=", "");
                        break;
                    case "tag":
                        targetType = "tag name";
                        tempTarget = thisCommand.Target.Replace("tag=", "");
                        break;
                    case "xpath":
                        tempTarget = thisCommand.Target.Replace("xpath=", "");
                        break;
                    default:
                        tempTarget = thisCommand.Target;
                        break;
                }
                double tempSpeed = 0;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    tempSpeed = Speed.Value;
                }));
                Thread.Sleep(Convert.ToInt16(tempSpeed * 3));

                if (!pause)
                    switch (thisCommand.Command.Replace("\t", ""))
                    {
                        #region ===> open
                        case "open":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;
                                }));
                                driver.Navigate().GoToUrl(thisCommand.Target);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;
                                }));
                            }
                            catch (Exception ex)
                            {


                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));
                            }
                            break;
                        #endregion

                        #region ===> waitForElementPresent
                        case "waitForElementPresent":

                            try
                            {
                                IWebElement el_waitForElementPresent;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.LinkText(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.PartialLinkText(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(tempTarget)));
                                        el_waitForElementPresent = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                //highlight
                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_waitForElementPresent);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_waitForElementPresent);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;
                                }));

                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForElementPresent " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForElementVisible
                        case "waitForElementVisible":

                            try
                            {
                                IWebElement el_waitForElementVisible;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.LinkText(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.PartialLinkText(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.TagName(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(tempTarget)));
                                        el_waitForElementVisible = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_waitForElementVisible);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_waitForElementVisible);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForElementVisible " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForElementNotPresent
                        case "waitForElementNotPresent":
                            try
                            {
                                IWebElement el_waitForElementNotPresent;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.LinkText(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Name(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.PartialLinkText(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.TagName(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(tempTarget)));
                                        el_waitForElementNotPresent = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForElementNotPresent " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForNotText
                        case "waitForNotText":
                            try
                            {
                                IWebElement el_waitForNotText;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.ClassName(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.CssSelector(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.Id(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.LinkText(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.Name(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.PartialLinkText(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.TagName(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath(tempTarget), thisCommand.Value));
                                        el_waitForNotText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForNotText " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForText
                        case "waitForText":
                            try
                            {
                                IWebElement el_waitForText;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.ClassName(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.LinkText(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Name(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.PartialLinkText(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.TagName(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(tempTarget), thisCommand.Value));
                                        el_waitForText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_waitForText);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_waitForText);


                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForText " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForValue
                        case "waitForValue":
                            try
                            {
                                IWebElement el_waitForValue;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.ClassName(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.CssSelector(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.LinkText(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Name(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.PartialLinkText(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.TagName(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.XPath(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.XPath(tempTarget), thisCommand.Value));
                                        el_waitForValue = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_waitForValue);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_waitForValue);


                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForValue " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));
                            }

                            break;
                        #endregion

                        #region ===> waitForAttribute
                        case "waitForAttribute":
                            try
                            {
                                IWebElement el_waitForAttribute;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_waitForAttribute = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_waitForAttribute = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_waitForAttribute = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_waitForAttribute = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_waitForAttribute = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_waitForAttribute = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_waitForAttribute = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_waitForAttribute = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_waitForAttribute = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                if (!el_waitForAttribute.GetAttribute("class").Contains("active"))
                                    throw new Exception("Element is not active");


                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_waitForAttribute);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_waitForAttribute);


                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForAttribute " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForWindowPresent
                        case "waitForWindowPresent":
                            try
                            {
                                IWebElement el_waitForWindowPresent;

                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForWindowPresent " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> waitForNumberOfWindowPresent
                        case "waitForNumberOfWindowPresent":
                            try
                            {
                                IWebElement el_waitForNumberOfWindowPresent;

                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;


                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("waitForNumberOfWindowPresent " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));
                            }

                            break;
                        #endregion

                        #region ===> type
                        case "type":
                            try
                            {
                                IWebElement el_type;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_type = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_type = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_type = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_type = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_type = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_type = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_type = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_type = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_type = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_type);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_type);
                                el_type.Clear();
                                if (!thisCommand.Value.Contains("${"))
                                    el_type.SendKeys(thisCommand.Value);
                                else
                                {
                                    string final = "";
                                    string[] temp = thisCommand.Value.Split(new[] { "${" }, StringSplitOptions.None);
                                    for (int i = 1; i < temp.Length; i++)
                                    {
                                        try
                                        {
                                            final += StoreEvalDB[temp[i].Substring(0, temp[i].IndexOf('}'))] + temp[i].Remove(0, temp[i].IndexOf('}') + 1);
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Items.Add(e.ToString());
                                            throw new Exception(e.ToString());
                                        }
                                    }
                                    el_type.SendKeys(final);
                                }
                                //el_type.Submit();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("type " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> sendKeys
                        case "sendKeys":
                            try
                            {
                                IWebElement el_sendKeys;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_sendKeys = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_sendKeys = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_sendKeys = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_sendKeys = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_sendKeys = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_sendKeys = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_sendKeys = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_sendKeys = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_sendKeys = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_sendKeys);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_sendKeys);
                                el_sendKeys.SendKeys(thisCommand.Value);
                                //el_sendKeys.Submit();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("sendKeys " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> clearText
                        case "clearText":
                            try
                            {
                                IWebElement el_clearText;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_clearText = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_clearText = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_clearText = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_clearText = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_clearText = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_clearText = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_clearText = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_clearText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_clearText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_clearText);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_clearText);
                                el_clearText.Clear();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("clearText " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> click
                        case "click":
                            try
                            {
                                IWebElement el_click;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_click = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_click = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_click = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_click = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_click = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_click = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_click = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_click = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_click = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_click);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_click);
                                el_click.Click();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("click " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> select
                        case "select1":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }
                            break;
                        #endregion

                        #region ===> selectByVisibleText
                        case "select":
                            try
                            {
                                IWebElement el_select;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_select = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_select = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_select = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_select = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_select = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_select = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_select = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_select = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_select = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_select);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_select);
                                SelectElement select = new SelectElement(el_select);
                                select.SelectByText(thisCommand.Value);


                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("select " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> selectByValue
                        case "selectByValue":
                            try
                            {
                                IWebElement el_selectByValue;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_selectByValue = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_selectByValue = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_selectByValue = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_selectByValue = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_selectByValue = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_selectByValue = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_selectByValue = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_selectByValue = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_selectByValue = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_selectByValue);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_selectByValue);
                                SelectElement select2 = new SelectElement(el_selectByValue);
                                select2.SelectByValue(thisCommand.Value);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("selectByValue " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }


                            break;
                        #endregion

                        #region ===> selectByIndex
                        case "selectByIndex":
                            try
                            {
                                IWebElement el_selectByIndex;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_selectByIndex = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_selectByIndex = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_selectByIndex = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_selectByIndex = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_selectByIndex = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_selectByIndex = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_selectByIndex = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_selectByIndex = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_selectByIndex = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_selectByIndex);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_selectByIndex);
                                SelectElement select3 = new SelectElement(el_selectByIndex);
                                select3.SelectByIndex(Convert.ToInt32(thisCommand.Value));


                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("selectByIndex " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeText
                        case "storeText":
                            try
                            {
                                IWebElement el_storeText;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeText = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeText = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeText = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeText = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeText = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeText = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeText = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeText = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeText.Text;

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeText);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeText);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeText " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeValue
                        case "storeValue":
                            try
                            {
                                IWebElement el_storeValue;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeValue = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeValue = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeValue = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeValue = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeValue = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeValue = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeValue = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeValue = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeValue = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeValue.GetAttribute("value");


                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeValue);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeValue);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeValue " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeWicketPath
                        case "storeWicketPath":
                            try
                            {
                                IWebElement el_storeWicketPath;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeWicketPath = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeWicketPath = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeWicketPath = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeWicketPath = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeWicketPath = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeWicketPath = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeWicketPath = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeWicketPath = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeWicketPath = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeWicketPath.GetAttribute("wicketPath");

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeWicketPath);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeWicketPath);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeWicketPath " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeInnerHTML
                        case "storeInnerHTML":
                            try
                            {
                                IWebElement el_storeInnerHTML;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeInnerHTML = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeInnerHTML = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeInnerHTML = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeInnerHTML = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeInnerHTML = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeInnerHTML = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeInnerHTML = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeInnerHTML = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeInnerHTML = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeInnerHTML.GetAttribute("innerHTML");

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeInnerHTML);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeInnerHTML);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeInnerHTML " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }

                            break;
                        #endregion

                        #region ===> storeName
                        case "storeName":
                            try
                            {
                                IWebElement el_storeName;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeName = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeName = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeName = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeName = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeName = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeName = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeName = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeName = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeName = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeName.GetAttribute("name");

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeName);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeName);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeName " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeId
                        case "storeId":
                            try
                            {
                                IWebElement el_storeId;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeId = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeId = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeId = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeId = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeId = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeId = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeId = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeId = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeId = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeId.GetAttribute("id");

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeId);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeId);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeId " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeHref
                        case "storeHref":
                            try
                            {
                                IWebElement el_storeHref;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_storeHref = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_storeHref = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_storeHref = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_storeHref = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_storeHref = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_storeHref = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_storeHref = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_storeHref = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_storeHref = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = el_storeHref.GetAttribute("href");

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeHref);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeHref);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeHref " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> storeEval
                        case "storeEval":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));

                                thisCommand.Pass = true;

                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = thisCommand.Target;

                                //change command color in listveiw
                                //lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;
                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeEval " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                                }));

                            }
                            break;
                        #endregion

                        #region ===> storeElementPresent
                        case "storeElementPresent":
                            try
                            {
                                IWebElement el_storeElementPresent;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        if (IsElementPresent(By.ClassName(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.ClassName(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.ClassName(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active");

                                        break;
                                    case "css selector":
                                        if (IsElementPresent(By.CssSelector(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.CssSelector(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.CssSelector(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active");
                                        break;
                                    case "id":
                                        if (IsElementPresent(By.Id(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.Id(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.Id(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active");
                                        break;
                                    case "link text":
                                        if (IsElementPresent(By.LinkText(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.LinkText(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.LinkText(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active"); break;
                                    case "name":
                                        if (IsElementPresent(By.Name(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.Name(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.Name(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active"); break;
                                    case "partial link text":
                                        if (IsElementPresent(By.PartialLinkText(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.PartialLinkText(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.PartialLinkText(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active"); break;
                                    case "tag name":
                                        if (IsElementPresent(By.TagName(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.TagName(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.TagName(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active"); break;
                                    case "xpath":
                                        if (IsElementPresent(By.XPath(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.XPath(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.XPath(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active"); break;
                                    default:
                                        if (IsElementPresent(By.XPath(tempTarget)))
                                        {
                                            el_storeElementPresent = driver.FindElement(By.XPath(tempTarget));
                                            /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                            StoreEvalDB[thisCommand.Value] = IsElementPresent(By.XPath(tempTarget));

                                            jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_storeElementPresent);
                                            Thread.Sleep(150);
                                            jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_storeElementPresent);
                                        }
                                        else
                                            throw new Exception("Element is not active"); break;
                                }

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("storeElementPresent " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> alert
                        case "alert":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                IAlert a = driver.SwitchTo().Alert();
                                a.Accept();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("alert " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }

                            break;
                        #endregion

                        #region ===> replace
                        case "replace":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                /*if (!StoreEvalDB.ContainsKey(thisCommand.Value))*/
                                StoreEvalDB[thisCommand.Value] = thisCommand.Target;

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    Stored.Items.Refresh();
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;
                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("replace " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }
                            break;
                        #endregion

                        #region ===> runScript
                        case "runScript":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                if (tempTarget == "" || tempTarget == "None")
                                {
                                    jsExecutor.ExecuteScript(thisCommand.Value);
                                }
                                else
                                {
                                    jsExecutor.ExecuteScript(thisCommand.Value, thisCommand.Target);
                                }

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("runScript " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> switch
                        case "switch":
                            try
                            {
                                IWebElement el_switch;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_switch = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_switch = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_switch = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_switch = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_switch = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_switch = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_switch = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_switch = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_switch = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }

                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_switch);
                                driver.SwitchTo().Frame(el_switch);
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_switch);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("switch " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }

                            break;
                        #endregion

                        #region ===> switchToDefault
                        case "switchToDefault":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                driver.SwitchTo().DefaultContent();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                //lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }
                            break;
                        #endregion

                        #region ===> scrollInto
                        case "scrollInto":
                            try
                            {
                                IWebElement el_scrollInto;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                switch (targetType)
                                {
                                    case "class name":
                                        el_scrollInto = driver.FindElement(By.ClassName(tempTarget));
                                        break;
                                    case "css selector":
                                        el_scrollInto = driver.FindElement(By.CssSelector(tempTarget));
                                        break;
                                    case "id":
                                        el_scrollInto = driver.FindElement(By.Id(tempTarget));
                                        break;
                                    case "link text":
                                        el_scrollInto = driver.FindElement(By.LinkText(tempTarget));
                                        break;
                                    case "name":
                                        el_scrollInto = driver.FindElement(By.Name(tempTarget));
                                        break;
                                    case "partial link text":
                                        el_scrollInto = driver.FindElement(By.PartialLinkText(tempTarget));
                                        break;
                                    case "tag name":
                                        el_scrollInto = driver.FindElement(By.TagName(tempTarget));
                                        break;
                                    case "xpath":
                                        el_scrollInto = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                    default:
                                        el_scrollInto = driver.FindElement(By.XPath(tempTarget));
                                        break;
                                }
                                jsExecutor.ExecuteScript("arguments[0].classList.add(\"myHighlight\");", el_scrollInto);
                                actions.MoveToElement(el_scrollInto);
                                actions.Perform();
                                Thread.Sleep(150);
                                jsExecutor.ExecuteScript("arguments[0].classList.remove(\"myHighlight\");", el_scrollInto);

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;

                                }));

                                // Get the line number from the stack frame
                                var st = new StackTrace(ex, true);
                                var frame = st.GetFrame(st.FrameCount - 1);
                                var line = frame.GetFileLineNumber();
                                Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());
                            }

                            break;
                        #endregion

                        #region ===> while
                        case "while":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitemWhile = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));

                                //runnerInWhile.Add(thisCommand.Number, true);
                                //currentWhile = thisCommand;
                                _break = false;
                                startOfWhile = thisCommand.Number;
                                commandCounterWhile = startOfWhile;
                                endPos = TestList.ElementAt(caseIndex).TestValue.FirstOrDefault(x => x.Number > TestList.ElementAt(caseIndex).TestValue.ElementAt(thisCommand.Number - 1).Number &&
                                    x.Command.Contains("end") &&
                                    Regex.Matches(x.Command, "\t").Count == Regex.Matches(TestList.ElementAt(caseIndex).TestValue.ElementAt(thisCommand.Number - 1).Command, "\t").Count).Number - 1;
                                thisCommand.Pass = true;
                                while (!_break && ConditionParser(thisCommand.Target))
                                {
                                    runCommand(TestList.ElementAt(caseIndex).TestValue.ElementAt(commandCounterWhile), caseIndex, commandCounterWhile);
                                    commandCounterWhile++;
                                }
                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                                return;
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("while " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }
                            break;
                        #endregion

                        #region ===> break
                        case "break":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));

                                _break = true;

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("break " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }
                            break;
                        #endregion

                        #region ===> if
                        case "if":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                lvitemIf = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;
                                }));

                                //runnerInWhile.Add(thisCommand.Number, true);
                                //currentWhile = thisCommand;
                                endPos = TestList.ElementAt(caseIndex).TestValue.FirstOrDefault(x => x.Number > TestList.ElementAt(caseIndex).TestValue.ElementAt(thisCommand.Number - 1).Number &&
                                    x.Command.Contains("end") &&
                                    Regex.Matches(x.Command, "\t").Count == Regex.Matches(TestList.ElementAt(caseIndex).TestValue.ElementAt(thisCommand.Number - 1).Command, "\t").Count).Number - 1;

                                _break = false;
                                thisCommand.Pass = true;
                                if (!_break && ConditionParser(thisCommand.Target))
                                {
                                    return;
                                }
                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;
                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("if " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }
                            break;
                        #endregion

                        #region ===> end
                        case "end":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));

                                commandCounterWhile = 0;
                                //inWhile = true;
                                _break = false;

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));

                                //change command color in lvitem While
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    if (lvitemWhile != null) lvitemWhile.Background = System.Windows.Media.Brushes.LightGreen;

                                }));

                                //change command color in lvitem If
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    if (lvitemIf != null) lvitemIf.Background = System.Windows.Media.Brushes.LightGreen;

                                }));

                                //return;
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("end " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }
                            break;
                        #endregion

                        #region ===> refresh
                        case "refresh":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                driver.Navigate().Refresh();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }
                            break;
                        #endregion

                        #region ===> close
                        case "close":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                driver.Quit();

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }
                            break;
                        #endregion

                        #region ===> failTest
                        case "failTest":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));

                            }
                            break;
                        #endregion

                        #region ===> pause
                        case "pause":
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.Yellow;

                                }));
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(Convert.ToInt32(thisCommand.Target));

                                thisCommand.Pass = true;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightGreen;

                                }));
                            }
                            catch (Exception ex)
                            {

                                thisCommand.Pass = false;

                                //change command color in listveiw
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    listView.SelectedIndex = thisCommand.Number - 1;
                                    listView.ScrollIntoView(listView.SelectedItem);
                                }));
                                lvitem = listView.ItemContainerGenerator.ContainerFromIndex(thisCommand.Number - 1) as ListViewItem;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    lvitem.Background = System.Windows.Media.Brushes.LightPink;
                                    // Get the line number from the stack frame
                                    var st = new StackTrace(ex, true);
                                    var frame = st.GetFrame(st.FrameCount - 1);
                                    var line = frame.GetFileLineNumber();
                                    Log.Items.Add("open " + thisCommand.Number + " ---> Failed in line " + line + " Because of error : " + ex.ToString());

                                }));
                            }
                            break;
                            #endregion

                    }
                else
                {
                    pausedCommandIndex = commandIndex;
                    pausedCaseIndex = caseIndex;
                }
                //if(Stored.Items.is) Stored.Items.Refresh();
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("runCommand ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        public async Task<bool> runCase(int testCaseNumber, int index)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    switchTestCase(testCaseNumber - 1);
                }));
                bool passed = true;
                _break = false;
                for (int i = index; TestList.ElementAt(testCaseNumber - 1).TestValue.Count > i; i++)
                {
                    waitType = WaitType._case;
                    /*
                    #region loops and If
                    ///////Check loops
                    if (TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i).Command == "while")
                        runnerInWhile.Add(TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i).Number - 1, true);

                    if (runnerInWhile.Count(x => x.Value == true) > 0)
                    {
                        int startOfWhile = i;
                        int endOfWhile = TestList.ElementAt(testCaseNumber - 1).TestValue.FirstOrDefault(x => x.Number > TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i).Number &&
                                x.Command.Contains("end") &&
                                Regex.Matches(x.Command, "\t").Count == Regex.Matches(TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i).Command, "\t").Count).Number - 1;
                        TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i).Pass = true;
                        i++;

                        do
                        {
                            if (TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i - 1).Pass)
                                await Task.Run(() => runCommand(TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i), testCaseNumber - 1, i));
                            else
                            {
                                passed = false;
                                break;
                            }
                            i++;
                        }
                        while (i == endOfWhile && ConditionParser(TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(startOfWhile).Target));
                    }

                    ///////Check IF conditions
                    if (runnerInIf.Count(x => x.Value == true) > 0)
                    {
                        //thisCommand = ListDB.ElementAt(runnerInIf.LastOrDefault(x => x.Value == true).Key);

                    }
                    #endregion
                    */
                    //normal commands
                    if (i == 0)
                        await Task.Run(() => runCommand(TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i), testCaseNumber - 1, i));
                    else
                    {
                        if (TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i - 1).Pass || _break)
                            await Task.Run(() => runCommand(TestList.ElementAt(testCaseNumber - 1).TestValue.ElementAt(i), testCaseNumber - 1, i));
                        else
                        {
                            passed = false;
                            break;
                        }
                    }
                    if (_break)
                        i = endPos - 1;
                }
                    
                if (passed)
                {
                    caseFinished = true;
                    TestList.ElementAt(testCaseNumber - 1).IsPassed = true;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ListViewItem lvitem1 = TestCaseListView.ItemContainerGenerator.ContainerFromIndex(_testCaseCounter - 1) as ListViewItem;
                        lvitem1.Background = System.Windows.Media.Brushes.LightGreen;
                    }));
                }
                else
                {
                    caseFinished = false;
                    TestList.ElementAt(testCaseNumber - 1).IsPassed = false;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ListViewItem lvitem1 = TestCaseListView.ItemContainerGenerator.ContainerFromIndex(_testCaseCounter - 1) as ListViewItem;
                        lvitem1.Background = System.Windows.Media.Brushes.LightPink;
                    }));
                }
                return true;
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("runCase ---> Failed in line " + line + " Because of error : " + ex.ToString());
                return false;
            }
        }

        public async Task runSuit(int index)
        {
            try
            {

                for (int i = index /*index nabashe*/; TestList.Count >= i; i++)
                {
                    bool passed = true;
                    waitType = WaitType._case;
                    if (i == 1)
                        await Task.Run(() => runCase(i, 0));
                    else
                    {
                        if (TestList.ElementAt(i - 2).IsPassed || RunFromMiddle)
                            await Task.Run(() => runCase(i, 0));
                        else
                        {
                            passed = false;
                            break;
                        }
                    }
                    if (passed)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                // Get the line number from the stack frame
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var line = frame.GetFileLineNumber();
                Log.Items.Add("runSuit ---> Failed in line " + line + " Because of error : " + ex.ToString());
            }
        }

        #endregion


    }
}
