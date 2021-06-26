using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTry_app_1.BL
{
    class BuildFile
    {
        /*#region Save
        public void TestMethod(string A)
        {
            if (A != null)
            {
                int _counter = 0;
                FileInfo file = new FileInfo(@A);
                //file.Create();
                StreamWriter TestFile = file.CreateText();
                string mainString_testCase = File.ReadAllText(@"StaticCode.py");
                int mainStringSplitterIndex = mainString_testCase.IndexOf("durationError");
                string mainString = mainString_testCase.Substring(0, mainStringSplitterIndex);
                mainString += "\r\n\r\nclass StoreEvalDB :\r\n\tvars = { }\r\n";
                mainString += "\nclass " + TestList.ElementAt(_testCaseCounter - 1).TestName + ":\n";
                while (CommandCounter > _counter)
                {
                    switch (ListDB.ElementAt(_counter).Command)
                    {
                        ////////////////////////////////////open/////////////////////////////////////
                        case "open":
                            mainString += "\t# " + (_counter + 1) + " | open\n";
                            string _open = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.get(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Target) + ")\n";
                            mainString += _open;
                            mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            break;

                        //////////////////////////waitForElementPresent//////////////////////////////
                        case "waitForElementPresent":
                            mainString += "\t# " + (_counter + 1) + " | waitForElementPresent\n";
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"name\", \"" + temp + "\")))\n";
                                _wait += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _wait += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"id\", \"" + temp + "\")))\n";
                                _wait += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _wait += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"css selector\", \"" + temp + "\")))\n";
                                _wait += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _wait += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"link text\", \"" + temp + "\")))\n";
                                _wait += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _wait += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"xpath\", \"" + ListDB.ElementAt(_counter).Target + "\")))\n";
                                _wait += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _wait += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;

                        //////////////////////////waitForElementNotPresent//////////////////////////////
                        case "waitForElementNotPresent":
                            mainString += "\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"name\", \"" + temp + "\")))\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"id\", \"" + temp + "\")))\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"css selector\", \"" + temp + "\")))\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"link text\", \"" + temp + "\")))\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _wait = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"xpath\", \"" + ListDB.ElementAt(_counter).Target + "\")))\n";
                                mainString += _wait;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;

                        ///////////////////////////////waitForText//////////////////////////////////
                        case "waitForText":
                            mainString += "\t# " + (_counter + 1) + " | waitForText\n";
                            string condition = "";
                            string _value2;
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _waitForText = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "name" + "\", \"" + temp + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForText += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _waitForText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _waitForText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _waitForText = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "id" + "\", \"" + temp + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForText += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _waitForText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _waitForText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _waitForText = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "css selector" + "\", \"" + temp + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForText += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _waitForText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _waitForText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _waitForText = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "link text" + "\", \"" + temp + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForText += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _waitForText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _waitForText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _waitForText = "\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "xpath" + "\", \"" + ListDB.ElementAt(_counter).Target + "\"), " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "))\n";
                                _waitForText += "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _waitForText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                mainString += _waitForText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;


                        ////////////////////////////////////////type////////////////////////////////////////////
                        case "type":
                            mainString += "\t# " + (_counter + 1) + " | type\n";
                            string _value;
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;

                        ////////////////////////////////////////sendKeys////////////////////////////////////////////
                        case "sendKeys":
                            mainString += "\t# " + (_counter + 1) + " | sendKeys\n";
                            string _value1;
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _type = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _type += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _type += "\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + ")\n";
                                mainString += _type;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;

                        ////////////////////////////////////////click////////////////////////////////////////////
                        case "click":
                            mainString += "\t# " + (_counter + 1) + " | click\n";
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _click = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _click += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += "\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                mainString += _click;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _click = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _click += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += "\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                mainString += _click;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _click = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _click += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += "\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                mainString += _click;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _click = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _click += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += "\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                mainString += _click;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _click = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _click += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _click += "\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                mainString += _click;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;

                        ////////////////////////////////////////select////////////////////////////////////////////
                        case "select":
                            mainString += "\t# " + (_counter + 1) + " | select\n";
                            string _tempValue;
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _select = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "']\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                mainString += _select;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _select = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "']\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                mainString += _select;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _select = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "']\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                mainString += _select;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _select = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "']\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                mainString += _select;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            else
                            {
                                string _select = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Value) + "']\")\n";
                                _select += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                _select += "\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                mainString += _select;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            }
                            break;

                        ////////////////////////////////////////storeText////////////////////////////////////////////
                        case "storeText":
                            mainString += "\t# " + (_counter + 1) + " | storeText\n";
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                            }
                            else
                            {
                                string _storeText = "\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");

                            }
                            break;

                        ////////////////////////////////////////storeValue////////////////////////////////////////////
                        case "storeValue":
                            mainString += "\t# " + (_counter + 1) + " | storeValue\n";
                            if (ListDB.ElementAt(_counter).Target.Contains("name="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                            {
                                string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                            }
                            else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                            }
                            else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                            {
                                string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                string _storeText = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                            }
                            else
                            {
                                string _storeText = "\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                _storeText += "\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                _storeText += "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                mainString += _storeText;
                                mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");

                            }
                            break;

                        ////////////////////////////////////////storeEval////////////////////////////////////////////
                        case "storeEval":
                            mainString += "\t# " + (_counter + 1) + " | storeEval\n";
                            string _value3;
                            string _storeEval = "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Target) + "\n";
                            mainString += _storeEval;
                            mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            Stored.Items.Add(_storeEval);
                            break;

                        ///////////////////////////////////////////alert//////////////////////////////////////////////
                        case "alert":
                            mainString += "\t# " + (_counter + 1) + " | alert\n";
                            string _alert = "\talert" + _counter + " = driver.switch_to_alert()\n";
                            _alert += "\talert" + _counter + ".accept()\n";
                            mainString += _alert;
                            mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            break;

                        /////////////////////////////////////////////replace////////////////////////////////////////////
                        case "replace":
                            mainString += "\t# " + (_counter + 1) + " | replace\n";
                            string _replace = "\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).VariableName + "\"] = " + ConvertTextToIdeFormat(ListDB.ElementAt(_counter).Target) + "\n";
                            mainString += _replace;
                            mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            Stored.Items.Add(_replace);
                            break;

                        /////////////////////////////////////////////pause////////////////////////////////////////////
                        case "pause":
                            mainString += "\t# " + (_counter + 1) + " | pause\n";
                            int tempTarget = Convert.ToInt16(ListDB.ElementAt(_counter).Target) / 1000;
                            string _pause = "\ttime.sleep(" + Convert.ToString(tempTarget) + ")\n";
                            mainString += _pause;
                            mainString += "\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                            break;

                    }
                    _counter++;
                }
                TestFile.WriteLine(mainString);
                TestFile.Close();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void TestSuitMethod(string B)
        {
            if (B != null)
            {
                int _counter1 = 0;
                int _counter = 0;
                FileInfo file = new FileInfo(@B);
                StreamWriter TestFile = file.CreateText();
                string StaticCodeNew = File.ReadAllText(@"StaticCode.py").Replace("    ", "\t").Replace("testSuit", ProjectName).Replace("tableWidth", (_testCaseNameCount).ToString());
                int splitterIndex = StaticCodeNew.IndexOf("#bodyCode#");
                string mainString = StaticCodeNew.Substring(0, splitterIndex);
                string mainString_Last = StaticCodeNew.Remove(0, splitterIndex + 11);
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
                        switch (ListDB.ElementAt(_counter).Command)
                        {
                            ////////////////////////////////////open/////////////////////////////////////
                            case "open":
                                mainString += "\t\t# " + (_counter + 1) + " | open\n";
                                string _temptargetopen;
                                if (ListDB.ElementAt(_counter).Target.Contains("${"))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("${}", "");
                                    int count1 = temp.Split('$').Length;
                                    string[] splited1 = temp.Split('$');
                                    _temptargetopen = "";
                                    for (int i = 0; i < count1; i++)
                                    {
                                        if (splited1[i] != "" && splited1[i].Substring(0, 1) == "{")
                                        {
                                            if (splited1[i].IndexOf('}') == splited1[i].Length - 1)
                                            {
                                                splited1[i] = splited1[i].Replace("{", "str(StoreEvalDB.vars[\"");
                                                splited1[i] = splited1[i].Replace("}", "\"])");
                                                _temptargetopen += splited1[i];
                                                while (i + 1 != count1)
                                                {
                                                    _temptargetopen += " + ";
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                splited1[i] = splited1[i].Replace("{", "str(StoreEvalDB.vars[\"");
                                                splited1[i] = splited1[i].Replace("}", "\"]) + '");
                                                _temptargetopen += splited1[i] + "'";
                                                while (i + 1 != count1)
                                                {
                                                    _temptargetopen += " + ";
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _temptargetopen += "'" + splited1[i] + "'";
                                            while (i + 1 != count1)
                                            {
                                                _temptargetopen += " + ";
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _temptargetopen = "'" + ListDB.ElementAt(_counter).Target + "'";
                                }
                                string _open = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.get(" + _temptargetopen + ")\n";
                                mainString += _open;
                                mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                break;

                            //////////////////////////waitForElementPresent//////////////////////////////
                            case "waitForElementPresent":
                                mainString += "\t\t# " + (_counter + 1) + " | waitForElementPresent\n";
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"name\", \"" + temp + "\")))\n";
                                    _wait += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _wait += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"id\", \"" + temp + "\")))\n";
                                    _wait += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _wait += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"css selector\", \"" + temp + "\")))\n";
                                    _wait += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _wait += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"link text\", \"" + temp + "\")))\n";
                                    _wait += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _wait += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located((\"xpath\", \"" + ListDB.ElementAt(_counter).Target + "\")))\n";
                                    _wait += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _wait += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;

                            //////////////////////////waitForElementNotPresent//////////////////////////////
                            case "waitForElementNotPresent":
                                mainString += "\t\t# " + (_counter + 1) + " | waitForElementNotPresent\n";
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"name\", \"" + temp + "\")))\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"id\", \"" + temp + "\")))\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"css selector\", \"" + temp + "\")))\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"link text\", \"" + temp + "\")))\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _wait = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.invisibility_of_element_located((\"xpath\", \"" + ListDB.ElementAt(_counter).Target + "\")))\n";
                                    mainString += _wait;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;

                            ///////////////////////////////waitForText//////////////////////////////////
                            case "waitForText":
                                mainString += "\t\t# " + (_counter + 1) + " | waitForText\n";
                                string condition = "";
                                string _value2;
                                if (ListDB.ElementAt(_counter).Value.Contains("${"))
                                {
                                    string temp = ListDB.ElementAt(_counter).Value.Replace("${}", "");
                                    int count1 = temp.Split('$').Length;
                                    int count12 = count1;
                                    string[] splited1 = temp.Split('$');
                                    _value2 = "";
                                    for (int i = 0; i < count1; i++)
                                    {
                                        if (splited1[i] == "''")
                                        {
                                            count12--;
                                        }
                                        else
                                        {
                                            if (splited1[i] != "" && splited1[i].Substring(0, 1) == "{")
                                            {
                                                if (splited1[i].IndexOf('}') == splited1[i].Length - 1)
                                                {
                                                    splited1[i] = splited1[i].Replace("{", "str(StoreEvalDB.vars[\"");
                                                    splited1[i] = splited1[i].Replace("}", "\"])");
                                                    _value2 += splited1[i];
                                                    if (i < count12 - 1)
                                                    {
                                                        _value2 += " + ";
                                                    }
                                                }
                                                else
                                                {
                                                    splited1[i] = splited1[i].Replace("{", "str(StoreEvalDB.vars[\"");
                                                    splited1[i] = splited1[i].Replace("}", "\"]) + '");
                                                    _value2 += splited1[i] + "'";
                                                    if (i < count12 - 1)
                                                    {
                                                        _value2 += " + ";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _value2 += "'" + splited1[i] + "'";
                                                if (i < count12 - 1)
                                                {
                                                    _value2 += " + ";
                                                }
                                            }
                                        }
                                    }
                                    condition += _value2;
                                }
                                else
                                {
                                    condition += "'" + ListDB.ElementAt(_counter).Value + "'";
                                }
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _waitForText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "name" + "\", \"" + temp + "\"), " + condition + "))\n";
                                    _waitForText += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _waitForText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _waitForText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _waitForText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "id" + "\", \"" + temp + "\"), " + condition + "))\n";
                                    _waitForText += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _waitForText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _waitForText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _waitForText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "css selector" + "\", \"" + temp + "\"), " + condition + "))\n";
                                    _waitForText += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _waitForText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _waitForText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _waitForText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "link text" + "\", \"" + temp + "\"), " + condition + "))\n";
                                    _waitForText += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _waitForText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _waitForText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _waitForText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = WebDriverWait(driver, 10).until(expected_conditions.text_to_be_present_in_element((\"" + "xpath" + "\", \"" + ListDB.ElementAt(_counter).Target + "\"), " + condition + "))\n";
                                    _waitForText += "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _waitForText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    mainString += _waitForText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;


                            ////////////////////////////////////////type////////////////////////////////////////////
                            case "type":
                                mainString += "\t\t# " + (_counter + 1) + " | type\n";
                                string _value;
                                if (ListDB.ElementAt(_counter).Value.Contains("${"))
                                {
                                    string temp = ListDB.ElementAt(_counter).Value.Replace("${}", "");
                                    int count1 = temp.Split('$').Length;
                                    string[] splited1 = temp.Split('$');
                                    _value = "";
                                    for (int i = 0; i < count1; i++)
                                    {
                                        if (splited1[i] != "" && splited1[i].Substring(0, 1) == "{")
                                        {
                                            if (splited1[i].IndexOf('}') == splited1[i].Length - 1)
                                            {
                                                splited1[i] = splited1[i].Replace("{", "str(StoreEvalDB.vars[\"");
                                                splited1[i] = splited1[i].Replace("}", "\"])");
                                                _value += splited1[i];
                                                while (i + 1 != count1)
                                                {
                                                    _value += " + ";
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                splited1[i] = splited1[i].Replace("{", "str(StoreEvalDB.vars[\"");
                                                splited1[i] = splited1[i].Replace("}", "\"]) + '");
                                                _value += splited1[i] + "'";
                                                while (i + 1 != count1)
                                                {
                                                    _value += " + ";
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _value += "'" + splited1[i] + "'";
                                            while (i + 1 != count1)
                                            {
                                                _value += " + ";
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _value = "'" + ListDB.ElementAt(_counter).Value + "'";
                                }
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".clear()\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;

                            ////////////////////////////////////////sendKeys////////////////////////////////////////////
                            case "sendKeys":
                                mainString += "\t\t# " + (_counter + 1) + " | sendKeys\n";
                                string _value1;
                                if (ListDB.ElementAt(_counter).Value.Substring(0, 1) == "$")
                                {
                                    _value1 = FindBetween(ListDB.ElementAt(_counter).Value, "{", "}");
                                    if (_value1.Contains("ENTER"))
                                        _value1 = "Keys.ENTER";
                                    else if (_value1.Contains("ESCAPE"))
                                        _value1 = "Keys.ESCAPE";
                                    else if (_value1.Contains("TAB"))
                                        _value1 = "Keys.TAB";
                                    else
                                        _value1 = ListDB.ElementAt(_counter).Value;
                                }
                                else
                                {
                                    _value1 = ListDB.ElementAt(_counter).Value;
                                }
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value1 + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value1 + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value1 + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value1 + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _type = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _type += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _type += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".send_keys(" + _value1 + ")\n";
                                    mainString += _type;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;

                            ////////////////////////////////////////click////////////////////////////////////////////
                            case "click":
                                mainString += "\t\t# " + (_counter + 1) + " | click\n";
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _click = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _click += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _click += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                    mainString += _click;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _click = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _click += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _click += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                    mainString += _click;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _click = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _click += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _click += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                    mainString += _click;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _click = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _click += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _click += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                    mainString += _click;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _click = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _click += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _click += "\t\t" + ListDB.ElementAt(_counter).VariableName + ".click()\n";
                                    mainString += _click;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;
                            ////////////////////////////////////////select////////////////////////////////////////////
                            case "select":
                                mainString += "\t\t# " + (_counter + 1) + " | select\n";
                                string _tempValue;
                                if (ListDB.ElementAt(_counter).Value.Contains("${"))
                                    _tempValue = ListDB.ElementAt(_counter).Value.Replace("${", "' + str(StoreEvalDB.vars[\"").Replace("}", "\"]) + '");
                                else
                                    _tempValue = ListDB.ElementAt(_counter).Value;
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _select = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + _tempValue + "']\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                    mainString += _select;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _select = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + _tempValue + "']\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                    mainString += _select;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _select = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + _tempValue + "']\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                    mainString += _select;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _select = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + _tempValue + "']\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                    mainString += _select;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                else
                                {
                                    string _select = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2 = " + ListDB.ElementAt(_counter).VariableName + ".find_element_by_xpath(\"//option[. = '" + _tempValue + "']\")\n";
                                    _select += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + "_2)\n";
                                    _select += "\t\t" + ListDB.ElementAt(_counter).VariableName + "_2.click()\n";
                                    mainString += _select;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                }
                                break;

                            ////////////////////////////////////////storeText////////////////////////////////////////////
                            case "storeText":
                                mainString += "\t\t# " + (_counter + 1) + " | storeText\n";
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");
                                }
                                else
                                {
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).Value + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).Value + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).Value + ".text\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).Value + ".text");

                                }
                                break;

                            ////////////////////////////////////////storeValue////////////////////////////////////////////
                            case "storeValue":
                                mainString += "\t\t# " + (_counter + 1) + " | storeValue\n";
                                if (ListDB.ElementAt(_counter).Target.Contains("name="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "name='", "']");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_name(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("id="))
                                {
                                    string temp = FindBetween(ListDB.ElementAt(_counter).Target, "id='", "']");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_id(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                                }
                                else if (!ListDB.ElementAt(_counter).Target.Contains("name=") && !ListDB.ElementAt(_counter).Target.Contains("id=") && ListDB.ElementAt(_counter).Target.Contains("css="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("css=", "");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_css_selector(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                                }
                                else if (ListDB.ElementAt(_counter).Target.Contains("link="))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("link=", "");
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_link_text(\"" + temp + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");
                                }
                                else
                                {
                                    string _storeText = "\t\t" + ListDB.ElementAt(_counter).VariableName + " = driver.find_element_by_xpath(\"" + ListDB.ElementAt(_counter).Target + "\")\n";
                                    _storeText += "\t\thighlight(" + ListDB.ElementAt(_counter).VariableName + ")\n";
                                    _storeText += "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")\n";
                                    mainString += _storeText;
                                    mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                    Stored.Items.Add(ListDB.ElementAt(_counter).Value + " = " + ListDB.ElementAt(_counter).VariableName + ".get_attribute(\"value\")");

                                }
                                break;

                            ////////////////////////////////////////storeEval////////////////////////////////////////////
                            case "storeEval":
                                mainString += "\t\t# " + (_counter + 1) + " | storeEval\n";
                                string _value3;
                                if (ListDB.ElementAt(_counter).Target.Contains("${"))
                                {
                                    string temp = ListDB.ElementAt(_counter).Target.Replace("${}", "");
                                    int count1 = temp.Split('$').Length;
                                    string[] splited1 = temp.Split('$');
                                    _value3 = "";
                                    for (int i = 0; i < count1; i++)
                                    {
                                        if (splited1[i] != "" && splited1[i].Substring(0, 1) == "{")
                                        {
                                            if (splited1[i].IndexOf('}') == splited1[i].Length - 1)
                                            {
                                                splited1[i] = splited1[i].Replace("{", "StoreEvalDB.vars[\"");
                                                splited1[i] = splited1[i].Replace("}", "\"]");
                                                _value3 += splited1[i];
                                                while (i + 1 != count1)
                                                {
                                                    _value3 += " + ";
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                splited1[i] = splited1[i].Replace("{", "StoreEvalDB.vars[\"");
                                                splited1[i] = splited1[i].Replace("}", "\"] + '");
                                                _value3 += splited1[i] + "'";
                                                while (i + 1 != count1)
                                                {
                                                    _value3 += " + ";
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _value3 += "'" + splited1[i] + "'";
                                            while (i + 1 != count1)
                                            {
                                                _value3 += " + ";
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _value3 = "'" + ListDB.ElementAt(_counter).Value + "'";
                                }
                                string _storeEval = "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).Value + "\"] = " + _value3 + "\n";
                                mainString += _storeEval;
                                mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(_storeEval);
                                break;

                            ///////////////////////////////////////////alert//////////////////////////////////////////////
                            case "alert":
                                mainString += "\t\t# " + (_counter + 1) + " | alert\n";
                                string _alert = "\t\talert" + _counter + " = driver.switch_to_alert()\n";
                                _alert += "\t\talert" + _counter + ".accept()\n";
                                mainString += _alert;
                                mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                break;

                            /////////////////////////////////////////////replace////////////////////////////////////////////
                            case "replace":
                                mainString += "\t\t# " + (_counter + 1) + " | replace\n";
                                string temptarget = ListDB.ElementAt(_counter).Target.Replace("storeVars[\"", "StoreEvalDB.vars[\"");
                                string _replace = "\t\tStoreEvalDB.vars[\"" + ListDB.ElementAt(_counter).VariableName + "\"] = " + temptarget + "\n";
                                mainString += _replace;
                                mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                Stored.Items.Add(_replace);
                                break;

                            /////////////////////////////////////////////pause////////////////////////////////////////////
                            case "pause":
                                mainString += "\t\t# " + (_counter + 1) + " | pause\n";
                                int tempTarget = Convert.ToInt16(ListDB.ElementAt(_counter).Target) / 1000;
                                string _pause = "\t\ttime.sleep(" + Convert.ToString(tempTarget) + ")\n";
                                mainString += _pause;
                                mainString += "\t\t# Description: " + ListDB.ElementAt(_counter).Description + "\n";
                                break;

                        }
                        _counter++;
                    }
                    _counter1++;
                    _counter = 0;
                }
                mainString += mainString_Last;
                TestFile.WriteLine(mainString);
                TestFile.Close();
                TestSuitSaved = true;
            }
        }
        #endregion*/

    }
}
