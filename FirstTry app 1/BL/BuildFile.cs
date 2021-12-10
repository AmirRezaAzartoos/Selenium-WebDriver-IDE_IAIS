namespace FirstTry_app_1.BL
{
    internal class BuildFile
    {
        private readonly MainWindow _mainWindow = new MainWindow();
        ///////BUILD
        #region Save
        /*public void TestMethod(string A)
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
                mainString += "\nclass " + MainWindow.TestList.ElementAt(MainWindow._testCaseCounter - 1).TestName + ":\n";
                string tabNeededTemp = "";
                while (MainWindow.CommandCounter > _counter)
                {
                    ///////specifyTarget
                    string targetType = "";
                    if (MainWindow.ListDB.ElementAt(_counter).Target.Contains('=') && !MainWindow.ListDB.ElementAt(_counter).Target.Contains("//"))
                        targetType = MainWindow.ListDB.ElementAt(_counter).Target.Split('=')[0];
                    else if (!MainWindow.ListDB.ElementAt(_counter).Target.Contains('=') || MainWindow.ListDB.ElementAt(_counter).Target.Contains("//"))
                        targetType = "xpath";
                    string tempTarget = "";
                    switch (targetType)
                    {
                        case "class":
                            targetType = "class name";
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("class=", "");
                            break;
                        case "css":
                            targetType = "css selector";
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("css=", "");
                            break;
                        case "id":
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("id=", "");
                            break;
                        case "link":
                            targetType = "link text";
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("class=", "");
                            break;
                        case "name":
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("name=", "");
                            break;
                        case "partial":
                            targetType = "partial link text";
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("partial=", "");
                            break;
                        case "tag":
                            targetType = "tag name";
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("tag=", "");
                            break;
                        case "xpath":
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target;
                            break;
                        default:
                            tempTarget = MainWindow.ListDB.ElementAt(_counter).Target;
                            break;
                    }
                    string tempCommand = MainWindow.ListDB.ElementAt(_counter).Command.Replace("\t", "").Replace(" ", "");
                    tabNeededTemp = MainWindow.ListDB.ElementAt(_counter).Command.Replace(tempCommand, "");
                    switch (tempCommand)
                    {
                        #region ===> open
                        case "open":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | open\n";
                            string _open = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.get(" + _mainWindow.ConvertTextToIdeFormat(tempTarget) + ")\n";
                            mainString += _open;
                            mainString += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            break;
                        #endregion

                        #region ===> waitForElementPresent
                        case "waitForElementPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementPresent\n";
                            string _wait = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _wait += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _wait += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait;
                            break;
                        #endregion

                        #region ===> waitForElementVisible
                        case "waitForElementVisible":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementVisible\n";
                            string _wait3 = tabNeededTemp + "\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait3 += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _wait3 += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _wait3 += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait3;
                            break;
                        #endregion

                        #region ===> waitForElementNotPresent
                        case "waitForElementNotPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                            string _wait2 = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _wait2 += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _wait2;
                            break;
                        #endregion

                        #region ===> waitForNotText
                        case "waitForNotText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForNotText\n";
                            string _waitForNotText = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until_not(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "))\n";
                            _waitForNotText += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForNotText;
                            break;
                        #endregion

                        #region ===> waitForText
                        case "waitForText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForText\n";
                            string _waitForText = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "))\n";
                            _waitForText += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForText += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForText += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForText;
                            break;
                        #endregion

                        #region ===> waitForValue
                        case "waitForValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForValue\n";
                            string _waitForValue = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element_value((\"" + targetType + "\", \"" + tempTarget + "\"), " + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "))\n";
                            _waitForValue += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForValue += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForValue += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _waitForValue;
                            break;
                        #endregion

                        #region ===> waitForAttribute
                        case "waitForAttribute":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | waitForAttribute\n";
                            string _waitForAttribute = tabNeededTemp + "\tfor i in range(30):\n";
                            _waitForAttribute += tabNeededTemp + "\t\ttry:\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\tif \"active\" in att.get_attribute(\"class\"):\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t\tbreak\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\telif i == 29:\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\t\traise Exception(\"Element is not active\")\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\ttime.sleep(0.5)\n";
                            _waitForAttribute += tabNeededTemp + "\t\texcept StaleElementReferenceException:\n";
                            _waitForAttribute += tabNeededTemp + "\t\t\tcontinue\n";
                            _waitForAttribute += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
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
                            string _type = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _type += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _type += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                            _type += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + ")\n";
                            _type += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _type;
                            break;
                        #endregion

                        #region ===> sendKeys
                        case "sendKeys":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | sendKeys\n";
                            string _sendKeys = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _sendKeys += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _sendKeys += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + ")\n";
                            _sendKeys += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _sendKeys;
                            break;
                        #endregion

                        #region ===> clearText
                        case "clearText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | clearText\n";
                            string _clearText = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _clearText += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _clearText += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                            _clearText += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _clearText;
                            break;
                        #endregion

                        #region ===> click
                        case "click":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | click\n";
                            string _click = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _click += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _click += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".click()\n";
                            _click += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _click;
                            break;
                        #endregion

                        #region ===> select
                        case "select1":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | select\n";
                            string tempLabel = MainWindow.ListDB.ElementAt(_counter).Value;
                            if (tempLabel.Contains("label="))
                                tempLabel = tempLabel.Replace("label=", "");
                            string _select = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _select += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _select += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + "_2 = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".find_element_by_" + targetType.Replace(" ", "_") + "(\"option[. = '" + _mainWindow.ConvertTextToIdeFormat(tempLabel) + "']\")\n";
                            _select += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + "_2)\n";
                            _select += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                            _select += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _select;
                            break;
                        #endregion

                        #region ===> selectByVisibleText
                        case "select":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | selectByVisibleText\n";
                            string tempLabel1 = MainWindow.ListDB.ElementAt(_counter).Value;
                            if (tempLabel1.Contains("label="))
                                tempLabel1 = tempLabel1.Replace("label=", "");
                            string _selectByVisibleText = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByVisibleText += tabNeededTemp + "\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByVisibleText += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".select_by_visible_text(" + _mainWindow.ConvertTextToIdeFormat(tempLabel1) + ")\n";
                            _selectByVisibleText += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _selectByVisibleText;
                            break;
                        #endregion

                        #region ===> selectByValue
                        case "selectByValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | selectByValue\n";
                            string tempLabel2 = MainWindow.ListDB.ElementAt(_counter).Value;
                            if (tempLabel2.Contains("label="))
                                tempLabel2 = tempLabel2.Replace("label=", "");
                            string _selectByValue = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByValue += tabNeededTemp + "\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByValue += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".select_by_value(" + _mainWindow.ConvertTextToIdeFormat(tempLabel2) + ")\n";
                            _selectByValue += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _selectByValue;
                            break;
                        #endregion

                        #region ===> selectByIndex
                        case "selectByIndex":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | selectByIndex\n";
                            string _selectByIndex = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByIndex += tabNeededTemp + "\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _selectByIndex += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".select_by_index(" + MainWindow.ListDB.ElementAt(_counter).Value + ")\n";
                            _selectByIndex += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _selectByIndex;
                            break;
                        #endregion

                        #region ===> storeText
                        case "storeText":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeText\n";
                            string _storeText = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).Value + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeText += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).Value + ")\n";
                            _storeText += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).Value + ".text\n";
                            _storeText += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeText;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).Value + ".text");
                            break;
                        #endregion

                        #region ===> storeValue
                        case "storeValue":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeValue\n";
                            string _storeValue = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeValue += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeValue += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeValue += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeValue;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                            break;
                        #endregion

                        #region ===> storeWicketPath
                        case "storeWicketPath":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeWicketPath\n";
                            string _storeWicketPath = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeWicketPath += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeWicketPath += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeWicketPath += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeWicketPath;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"wicketpath\")");
                            break;
                        #endregion

                        #region ===> storeInnerHTML
                        case "storeInnerHTML":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeInnerHTML\n";
                            string _storeInnerHTML = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeInnerHTML += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeInnerHTML += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeInnerHTML += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeInnerHTML;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"innerHTML\")");
                            break;
                        #endregion

                        #region ===> storeName
                        case "storeName":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeName\n";
                            string _storeName = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeName += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeName += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeName += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeName;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"name\")");
                            break;
                        #endregion

                        #region ===> storeId
                        case "storeId":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeId\n";
                            string _storeId = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeId += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeId += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeId += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeId;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"id\")");
                            break;
                        #endregion

                        #region ===> storeHref
                        case "storeHref":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeHref\n";
                            string _storeHref = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _storeHref += tabNeededTemp + "\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                            _storeHref += tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                            _storeHref += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeHref;
                            _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"href\")");
                            break;
                        #endregion

                        #region ===> storeEval
                        case "storeEval":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeEval\n";
                            string _storeEval = tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + _mainWindow.ConvertTextToIdeFormat(tempTarget) + "\n";
                            _storeEval += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeEval;
                            _mainWindow.Stored.Items.Add(_storeEval);
                            break;
                        #endregion

                        #region ===> storeElementPresent
                        case "storeElementPresent":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | storeElementPresent\n";
                            string _storeElementPresent = tabNeededTemp + "\ttry:\n";
                            _storeElementPresent += tabNeededTemp + "\t\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                            _storeElementPresent += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = True\n";
                            _storeElementPresent += tabNeededTemp + "\texcept TimeoutException:\n";
                            _storeElementPresent += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = False\n";
                            _storeElementPresent += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _storeElementPresent;
                            _mainWindow.Stored.Items.Add(_storeElementPresent);
                            break;
                        #endregion

                        #region ===> alert
                        case "alert":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | alert\n";
                            string _alert = tabNeededTemp + "\talert" + _counter + " = driver.switch_to_alert()\n";
                            _alert += tabNeededTemp + "\talert" + _counter + ".accept()\n";
                            _alert += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _alert;
                            break;
                        #endregion

                        #region ===> replace
                        case "replace":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | replace\n";
                            string _replace = tabNeededTemp + "\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).VariableName + "\"] = " + _mainWindow.ConvertTextToIdeFormat(tempTarget) + "\n";
                            _replace += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _replace;
                            _mainWindow.Stored.Items.Add(_replace);
                            break;
                        #endregion

                        #region ===> runScript
                        case "runScript":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | runScript\n";
                            string _runScript = tabNeededTemp + "\tdriver.execute_script(\"" + MainWindow.ListDB.ElementAt(_counter).Value;
                            _runScript += tempTarget == "" || tempTarget == "None" ? "\")\n" : "\", driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                            _runScript += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _runScript;
                            break;
                        #endregion

                        #region ===> switch
                        case "switch":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | switch\n";
                            string _switch = tabNeededTemp + "\tdriver.switch_to." + tempTarget + "(" + MainWindow.ListDB.ElementAt(_counter).Value + ")\n";
                            _switch += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _switch;
                            break;
                        #endregion

                        #region ===> switchToDefault
                        case "switchToDefault":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | switchToDefault\n";
                            string _switchToDefault = tabNeededTemp + "\tdriver.switch_to.default_content()\n";
                            _switchToDefault += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _switchToDefault;
                            break;
                        #endregion

                        #region ===> scrollInto
                        case "scrollInto":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | scrollInto\n";
                            string _scrollInto = tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                            _scrollInto += tabNeededTemp + "\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".location_once_scrolled_into_view\n";
                            _scrollInto += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _scrollInto;
                            break;
                        #endregion

                        #region ===> while
                        case "while":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | while\n";
                            string _while = tabNeededTemp + "\twhile " + MainWindow.ListDB.ElementAt(_counter).Target + ":\n";
                            _while += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _while;
                            break;
                        #endregion

                        #region ===> break
                        case "break":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | break\n";
                            string _break = tabNeededTemp + "\tbreak\n";
                            _break += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _break;
                            break;
                        #endregion

                        #region ===> if
                        case "if":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | if\n";
                            string _if = tabNeededTemp + "\tif " + MainWindow.ListDB.ElementAt(_counter).Target + ":\n";
                            _if += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
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
                            _refresh += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _refresh;
                            break;
                        #endregion

                        #region ===> close
                        case "close":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | close\n";
                            string _close = tabNeededTemp + "\tdriver.close()\n";
                            _close += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _close;
                            break;
                        #endregion

                        #region ===> failTest
                        case "failTest":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | failTest\n";
                            string _failTest = tabNeededTemp + "\traise Exception(\"You failed the test intentionally\")\n";
                            _failTest += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                            mainString += _failTest;
                            break;
                        #endregion

                        #region ===> pause
                        case "pause":
                            mainString += tabNeededTemp + "\t# " + (_counter + 1) + " | pause\n";
                            int tempTarget1 = Convert.ToInt16(tempTarget) / 1000;
                            string _pause = tabNeededTemp + "\ttime.sleep(" + Convert.ToString(tempTarget1) + ")\n";
                            _pause += tabNeededTemp + "\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
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
                string StaticCodeNew = File.ReadAllText(@"StaticCode.py").Replace("    ", "\t").Replace("testSuit", MainWindow.ProjectName).Replace("tableWidth", (MainWindow._testCaseNameCount).ToString());
                int splitterIndex = StaticCodeNew.IndexOf("#bodyCode#");
                string mainString = StaticCodeNew.Substring(0, splitterIndex);
                string mainString_Last = StaticCodeNew.Remove(0, splitterIndex + 11);
                string tabNeededTemp = "";

                while (MainWindow.testCaseCounter > _counter1)
                {
                    var obj = MainWindow.TestList.FirstOrDefault(x => x.TestNumber == _counter1 + 1);
                    if (obj != null)
                    {
                        MainWindow.ListDB = new ObservableCollection<Commands>(obj.TestValue);
                        MainWindow.CommandCounter = obj.TestValue.Count;
                    }
                    mainString += "\n\tclass " + MainWindow.TestList.ElementAt(_counter1).TestName + ":\n";
                    while (MainWindow.CommandCounter > _counter)
                    {
                        ///////specifyTarget
                        string targetType = "";
                        if (MainWindow.ListDB.ElementAt(_counter).Target.Contains('=') && !MainWindow.ListDB.ElementAt(_counter).Target.Contains("//"))
                            targetType = MainWindow.ListDB.ElementAt(_counter).Target.Split('=')[0];
                        else if (!MainWindow.ListDB.ElementAt(_counter).Target.Contains('=') || MainWindow.ListDB.ElementAt(_counter).Target.Contains("//"))
                            targetType = "xpath";
                        string tempTarget = "";
                        switch (targetType)
                        {
                            case "class":
                                targetType = "class name";
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("class=", "");
                                break;
                            case "css":
                                targetType = "css selector";
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                break;
                            case "id":
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("id=", "");
                                break;
                            case "link":
                                targetType = "link text";
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("class=", "");
                                break;
                            case "name":
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("name=", "");
                                break;
                            case "partial":
                                targetType = "partial link text";
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("partial=", "");
                                break;
                            case "tag":
                                targetType = "tag name";
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target.Replace("tag=", "");
                                break;
                            case "xpath":
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target;
                                break;
                            default:
                                tempTarget = MainWindow.ListDB.ElementAt(_counter).Target;
                                break;
                        }
                        string tempCommand = MainWindow.ListDB.ElementAt(_counter).Command.Replace("\t", "").Replace(" ", "");
                        tabNeededTemp = MainWindow.ListDB.ElementAt(_counter).Command.Replace(tempCommand, "");
                        switch (tempCommand)
                        {
                            #region ===> open
                            case "open":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | open\n";
                                string _open = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.get(" + _mainWindow.ConvertTextToIdeFormat(tempTarget) + ")\n";
                                mainString += _open;
                                mainString += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                break;
                            #endregion

                            #region ===> waitForElementPresent
                            case "waitForElementPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementPresent\n";
                                string _wait = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _wait += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _wait += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait;
                                break;
                            #endregion

                            #region ===> waitForElementVisible
                            case "waitForElementVisible":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementVisible\n";
                                string _wait3 = tabNeededTemp + "\t\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait3 += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _wait3 += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _wait3 += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait3;
                                break;
                            #endregion

                            #region ===> waitForElementNotPresent
                            case "waitForElementNotPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                                string _wait2 = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _wait2 += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _wait2;
                                break;
                            #endregion

                            #region ===> waitForNotText
                            case "waitForNotText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForNotText\n";
                                string _waitForNotText = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until_not(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForNotText += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForNotText;
                                break;
                            #endregion

                            #region ===> waitForText
                            case "waitForText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForText\n";
                                string _waitForText = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + targetType + "\", \"" + tempTarget + "\"), " + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForText += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForText += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForText += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForText;
                                break;
                            #endregion

                            #region ===> waitForValue
                            case "waitForValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForValue\n";
                                string _waitForValue = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element_value((\"" + targetType + "\", \"" + tempTarget + "\"), " + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForValue += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForValue += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForValue += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _waitForValue;
                                break;
                            #endregion

                            #region ===> waitForAttribute
                            case "waitForAttribute":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | waitForAttribute\n";
                                string _waitForAttribute = tabNeededTemp + "\t\tfor i in range(30):\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\ttry:\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\tif \"active\" in att.get_attribute(\"class\"):\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t\tbreak\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\telif i == 29:\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\t\traise Exception(\"Element is not active\")\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\ttime.sleep(0.5)\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\texcept StaleElementReferenceException:\n";
                                _waitForAttribute += tabNeededTemp + "\t\t\t\tcontinue\n";
                                _waitForAttribute += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
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
                                string _type = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _type += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + ")\n";
                                _type += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _type;
                                break;
                            #endregion

                            #region ===> sendKeys
                            case "sendKeys":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | sendKeys\n";
                                string _sendKeys = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _sendKeys += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _sendKeys += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + ")\n";
                                _sendKeys += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _sendKeys;
                                break;
                            #endregion

                            #region ===> clearText
                            case "clearText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | clearText\n";
                                string _clearText = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _clearText += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _clearText += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _clearText += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _clearText;
                                break;
                            #endregion

                            #region ===> click
                            case "click":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | click\n";
                                string _click = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _click += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                _click += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _click;
                                break;
                            #endregion

                            #region ===> select
                            case "select":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | select\n";
                                string _select = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _select += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + "_2 = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".find_element_by_" + targetType.Replace(" ", "_") + "(\"option[. = '" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "']\")\n";
                                _select += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                _select += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _select;
                                break;
                            #endregion

                            #region ===> selectByVisibleText
                            case "selectByVisibleText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | selectByVisibleText\n";
                                string _selectByVisibleText = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByVisibleText += tabNeededTemp + "\t\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByVisibleText += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".select_by_visible_text(\"" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "\")\n";
                                _selectByVisibleText += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _selectByVisibleText;
                                break;
                            #endregion

                            #region ===> selectByValue
                            case "selectByValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | selectByValue\n";
                                string _selectByValue = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByValue += tabNeededTemp + "\t\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByValue += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".select_by_value(\"" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "\")\n";
                                _selectByValue += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _selectByValue;
                                break;
                            #endregion

                            #region ===> selectByIndex
                            case "selectByIndex":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | selectByIndex\n";
                                string _selectByIndex = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = Select(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByIndex += tabNeededTemp + "\t\thighlight(driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _selectByIndex += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".select_by_index(\"" + _mainWindow.ConvertTextToIdeFormat(MainWindow.ListDB.ElementAt(_counter).Value) + "\")\n";
                                _selectByIndex += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _selectByIndex;
                                break;
                            #endregion

                            #region ===> storeText
                            case "storeText":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeText\n";
                                string _storeText = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).Value + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeText += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).Value + ".text\n";
                                _storeText += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeText;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).Value + ".text");
                                break;
                            #endregion

                            #region ===> storeValue
                            case "storeValue":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeValue\n";
                                string _storeValue = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeValue += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeValue += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeValue += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeValue;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                                break;
                            #endregion

                            #region ===> storeWicketPath
                            case "storeWicketPath":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeWicketPath\n";
                                string _storeWicketPath = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeWicketPath += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeWicketPath += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeWicketPath += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeWicketPath;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"wicketpath\")");
                                break;
                            #endregion

                            #region ===> storeInnerHTML
                            case "storeInnerHTML":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeInnerHTML\n";
                                string _storeInnerHTML = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeInnerHTML += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeInnerHTML += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeInnerHTML += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeInnerHTML;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"innerHTML\")");
                                break;
                            #endregion

                            #region ===> storeName
                            case "storeName":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeName\n";
                                string _storeName = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeName += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeName += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeName += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeName;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"name\")");
                                break;
                            #endregion

                            #region ===> storeId
                            case "storeId":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeId\n";
                                string _storeId = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeId += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeId += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeId += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeId;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"id\")");
                                break;
                            #endregion

                            #region ===> storeHref
                            case "storeHref":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeHref\n";
                                string _storeHref = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _storeHref += tabNeededTemp + "\t\thighlight(" + MainWindow.ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeHref += tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                _storeHref += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeHref;
                                _mainWindow.Stored.Items.Add(MainWindow.ListDB.ElementAt(_counter).Value + " = " + MainWindow.ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"href\")");
                                break;
                            #endregion

                            #region ===> storeEval
                            case "storeEval":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeEval\n";
                                string _storeEval = tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = " + _mainWindow.ConvertTextToIdeFormat(tempTarget) + "\n";
                                _storeEval += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeEval;
                                _mainWindow.Stored.Items.Add(_storeEval);
                                break;
                            #endregion

                            #region ===> storeElementPresent
                            case "storeElementPresent":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | storeElementPresent\n";
                                string _storeElementPresent = tabNeededTemp + "\t\ttry:\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tWebDriverWait(driver, 10).until(EC.visibility_of_element_located((\"" + targetType + "\", \"" + tempTarget + "\")))\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = True\n";
                                _storeElementPresent += tabNeededTemp + "\t\texcept TimeoutException:\n";
                                _storeElementPresent += tabNeededTemp + "\t\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).Value + "\"] = False\n";
                                _storeElementPresent += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _storeElementPresent;
                                _mainWindow.Stored.Items.Add(_storeElementPresent);
                                break;
                            #endregion

                            #region ===> alert
                            case "alert":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | alert\n";
                                string _alert = tabNeededTemp + "\t\talert" + _counter + " = driver.switch_to_alert()\n";
                                _alert += tabNeededTemp + "\t\talert" + _counter + ".accept()\n";
                                _alert += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _alert;
                                break;
                            #endregion

                            #region ===> replace
                            case "replace":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | replace\n";
                                string _replace = tabNeededTemp + "\t\tStoreEvalDB.vars[\"" + MainWindow.ListDB.ElementAt(_counter).VariableName + "\"] = " + _mainWindow.ConvertTextToIdeFormat(tempTarget) + "\n";
                                _replace += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _replace;
                                _mainWindow.Stored.Items.Add(_replace);
                                break;
                            #endregion

                            #region ===> runScript
                            case "runScript":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | runScript\n";
                                string _runScript = tabNeededTemp + "\t\tdriver.execute_script(\"" + MainWindow.ListDB.ElementAt(_counter).Value;
                                _runScript += tempTarget == "" || tempTarget == "None" ? "\")\n" : "\", driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\"))\n";
                                _runScript += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _runScript;
                                break;
                            #endregion

                            #region ===> switch
                            case "switch":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | switch\n";
                                string _switch = tabNeededTemp + "\t\tdriver.switch_to." + tempTarget + "(" + MainWindow.ListDB.ElementAt(_counter).Value + ")\n";
                                _switch += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _switch;
                                break;
                            #endregion

                            #region ===> switchToDefault
                            case "switchToDefault":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | switchToDefault\n";
                                string _switchToDefault = tabNeededTemp + "\t\tdriver.switch_to.default_content()\n";
                                _switchToDefault += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _switchToDefault;
                                break;
                            #endregion

                            #region ===> scrollInto
                            case "scrollInto":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | scrollInto\n";
                                string _scrollInto = tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_" + targetType.Replace(" ", "_") + "(\"" + tempTarget + "\")\n";
                                _scrollInto += tabNeededTemp + "\t\t" + MainWindow.ListDB.ElementAt(_counter).VariableName + ".location_once_scrolled_into_view\n";
                                _scrollInto += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _scrollInto;
                                break;
                            #endregion

                            #region ===> while
                            case "while":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | while\n";
                                string _while = tabNeededTemp + "\t\twhile " + MainWindow.ListDB.ElementAt(_counter).Target + ":\n";
                                _while += tabNeededTemp + "\t\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _while;
                                break;
                            #endregion

                            #region ===> break
                            case "break":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | break\n";
                                string _break = tabNeededTemp + "\t\tbreak\n";
                                _break += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _break;
                                break;
                            #endregion

                            #region ===> if
                            case "if":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | if\n";
                                string _if = tabNeededTemp + "\t\tif " + MainWindow.ListDB.ElementAt(_counter).Target + ":\n";
                                _if += tabNeededTemp + "\t\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
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
                                _refresh += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _refresh;
                                break;
                            #endregion

                            #region ===> close
                            case "close":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | close\n";
                                string _close = tabNeededTemp + "\t\tdriver.close()\n";
                                _close += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _close;
                                break;
                            #endregion

                            #region ===> failTest
                            case "failTest":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | failTest\n";
                                string _failTest = tabNeededTemp + "\t\traise Exception(\"You failed the test intentionally\")\n";
                                _failTest += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
                                mainString += _failTest;
                                break;
                            #endregion

                            #region ===> pause
                            case "pause":
                                mainString += tabNeededTemp + "\t\t# " + (_counter + 1) + " | pause\n";
                                int tempTarget1 = Convert.ToInt16(tempTarget) / 1000;
                                string _pause = tabNeededTemp + "\t\ttime.sleep(" + Convert.ToString(tempTarget1) + ")\n";
                                _pause += tabNeededTemp + "\t\t# Description: " + MainWindow.ListDB.ElementAt(_counter).Description + "\n";
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

                MainWindow.TestSuitSaved = true;
            }
        }*/
        #endregion
    }
}
