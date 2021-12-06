from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.common.exceptions import ElementNotSelectableException, ElementNotVisibleException, NoSuchElementException
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC, expected_conditions
from selenium.webdriver.chrome.options import Options
import time
import random
import datetime


optionsChrome = Options()
optionsChrome.add_experimental_option("prefs", {"download.default_directory": r"C:\Seleniums\upload_Files"})
PATH = "C:\Program Files (x86)\chromedriver.exe"
driver = webdriver.Chrome(executable_path="C:\Program Files (x86)\chromedriver.exe", options=optionsChrome)
driver.maximize_window()
driver.implicitly_wait(30)

def highlight(element):
	driver = element._parent
	def apply_style(s):
		driver.execute_script("arguments[0].setAttribute('style', arguments[1]); ",element, s)
	original_style = element.get_attribute('style')
	apply_style("background: yellow; border: 2px solid red; ")
	time.sleep(.3)
	apply_style(original_style)

class StoreEvalDB():
	vars = {}
