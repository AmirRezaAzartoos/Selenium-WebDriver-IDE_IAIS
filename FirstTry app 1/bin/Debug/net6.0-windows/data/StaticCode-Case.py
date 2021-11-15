from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.common.exceptions import ElementNotSelectableException, ElementNotVisibleException, NoSuchElementException
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC, expected_conditions
from selenium.webdriver.chrome.options import Options
import time
import random
import datetime


chrome_options = webdriver.ChromeOptions()
chrome_options.add_experimental_option("prefs", { "download.default_directory": "/home/gitlab-runner/seleniums/upload_Files"})
chrome_options.add_argument('--headless')
chrome_options.add_argument('--no-sandbox')
chrome_options.add_argument('--disable-dev-shm-usage')
chrome_options.add_argument("--start-maximized")
driver = webdriver.Chrome('/usr/local/bin/chromedriver', options = chrome_options)
driver.set_window_size(1920, 1080)
TestCases = PrettyTable(['Number', 'TestCase', 'Status'])
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
