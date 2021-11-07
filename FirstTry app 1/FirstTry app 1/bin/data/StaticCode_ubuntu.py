from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.common.exceptions import ElementNotSelectableException, ElementNotVisibleException, NoSuchElementException, TimeoutException, StaleElementReferenceException
from selenium.webdriver.support.ui import WebDriverWait, Select
from selenium.webdriver.support import expected_conditions as EC, expected_conditions
from selenium.webdriver.chrome.options import Options
from prettytable import PrettyTable
import time
import os
import random
import datetime
import platform
import sys
import traceback
import logging
from colorlog import ColoredFormatter
import socket

start_time = time.time()
now = datetime.datetime.now()

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

server = socket.gethostbyname('test.iais.co')

driver.get("http://test.iais.co:8080/PackingList-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
PackingList_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Customs-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
Customs_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Janbar-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
Janbar_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Baskool-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
Baskool_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Urban-Warehouse-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
UrbanWarehouse_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Coding-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "span[wicketpath$='label']")))
Coding_buildDate = driver.find_element_by_css_selector("span[wicketpath$='label']").text

driver.get("http://test.iais.co:8080/Center-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
Center_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Swich-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
Swich_buildDate = driver.find_element_by_css_selector("pre").text

driver.get("http://test.iais.co:8080/Coding-Rajae/buildDate")
WebDriverWait(driver, 10).until(EC.presence_of_all_elements_located(("css selector", "pre")))
Coding_buildDate = driver.find_element_by_css_selector("pre").text

logging.addLevelName(logging.DEBUG, 'GENERAL')
logging.addLevelName(logging.WARNING, 'SUCCESS')
logging.addLevelName(logging.CRITICAL, 'FAILURE')

formatter = ColoredFormatter(
    "  %(log_color)s%(levelname)-8s%(reset)s | %(log_color)s%(message)s%(reset)s",
    datefmt=None,
    reset=True,
    log_colors={
        'GENERAL': 'cyan',
        'INFO': 'yellow',
        'SUCCESS': 'green',
        'FAILURE': 'red',
        'ERROR': 'yellow,bold_red',
    },
    secondary_log_colors={},
    style='%'
)
LOG_LEVEL = logging.getLevelName(logging.DEBUG)
logging.root.setLevel(LOG_LEVEL)
stream = logging.StreamHandler()
stream.setLevel(LOG_LEVEL)
stream.setFormatter(formatter)
log = logging.getLogger('pythonConfig')
log.setLevel(LOG_LEVEL)
log.addHandler(stream)

Info = PrettyTable(['Info'])
Info.align = "l"
Info.left_padding_width = 2
Info.right_padding_width = 2
Info._min_width = {"Info" : tableWidth + 24}

TestCases = PrettyTable(['Number', 'TestCase', 'Status'])
TestCases.title = "testSuit"
TestCases.align = "l"
TestCases.left_padding_width = 2
TestCases.right_padding_width = 2
TestCases._min_width = {"Number" : 6,"TestCase" : tableWidth, "Status" : 8}

log.info("+" + ("-" * (tableWidth + 36)) + "+")
log.info("|" + (" *--> Info <--*".center((tableWidth + 36)) + "|"))
log.info("+" + ("-" * (tableWidth + 36)) + "+")
log.info("| Date and time : " + str(now.strftime("%d/%m/%Y & %H:%M:%S")) + (" ".center((tableWidth + 36) - 38) + "|"))
log.info("| Server : " + server + (" ".center((tableWidth + 36) - 24)) + "|")
log.info("| PackingList build date : = " + PackingList_buildDate + (" ".center((tableWidth + 36) - len(" PackingList build date : = " + Coding_buildDate))) + "|")
log.info("| Customs build date : = " + Customs_buildDate + (" ".center((tableWidth + 36) - len(" Customs build date : = " + Coding_buildDate))) + "|")
log.info("| Janbar build date : = " + Janbar_buildDate + (" ".center((tableWidth + 36) - len(" Janbar build date : = " + Coding_buildDate))) + "|")
log.info("| Baskool build date : = " + Baskool_buildDate + (" ".center((tableWidth + 36) - len(" Baskool build date : = " + Coding_buildDate))) + "|")
log.info("| Urban-Warehouse build date : = " + UrbanWarehouse_buildDate + (" ".center((tableWidth + 36) - len(" Urban-Warehouse build date : = " + Coding_buildDate))) + "|")
log.info("| Acc build date : = " + Acc_buildDate + (" ".center((tableWidth + 36) - len(" Acc build date : = " + Coding_buildDate))) + "|")
log.info("| Center build date : = " + Center_buildDate + (" ".center((tableWidth + 36) - len(" Center build date : = " + Coding_buildDate))) + "|")
log.info("| Swich build date : = " + Swich_buildDate + (" ".center((tableWidth + 36) - len(" Swich build date : = " + Coding_buildDate))) + "|")
log.info("| Coding build date : = " + Coding_buildDate + (" ".center((tableWidth + 36) - len(" Coding build date : = " + Coding_buildDate))) + "|")
log.info("+" + ("-" * (tableWidth + 36)) + "+")
log.debug("+" + ("-" * (tableWidth + 36)) + "+")
log.debug("|" + " *--> testSuit <--* ".center(tableWidth + 36) + "|")
log.debug("+" + ("-" * (tableWidth + 36)) + "+")

Info.add_row([" Date and time : " + str(now.strftime("%d/%m/%Y & %H:%M:%S"))])
Info.add_row([" Server : " + server])
Info.add_row([" PackingList build date : = " + PackingList_buildDate])
Info.add_row([" Customs build date : = " + Customs_buildDate])
Info.add_row([" Janbar build date : = " + Janbar_buildDate])
Info.add_row([" Baskool build date : = " + Baskool_buildDate])
Info.add_row([" Urban-Warehouse build date : = " + UrbanWarehouse_buildDate])
Info.add_row([" Acc build date : = " + Acc_buildDate])
Info.add_row([" Center build date : = " + Center_buildDate])
Info.add_row([" Swich build date : = " + Swich_buildDate])
Info.add_row([" Coding build date : = " + Coding_buildDate])

def highlight(element):
    driver = element._parent

    def apply_style(s):
        driver.execute_script("arguments[0].setAttribute('style', arguments[1]); ", element, s)

    original_style = element.get_attribute('style')
    apply_style("background: yellow; border: 2px solid red; ")
    time.sleep(.15)
    apply_style(original_style)


class StoreEvalDB:
    vars = {}


try:
#bodyCode#


    log.debug("+" + ("-" * (tableWidth + 36)) + "+")
    log.warning("Succeeded after %s minutes." % round(((time.time() - start_time) / 60), 2))
    log.info("Log file: http://172.16.111.229/testSuit(Passed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").log")
    log.info(StoreEvalDB.vars)
    driver.close()
    sys.stdout = open("//home//gitlab-runner//seleniums//Log//testSuit(Passed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").log", "w", encoding="utf-8")
    print(Info)
    print(TestCases)
    print("Succeeded after %s minutes." % round(((time.time() - start_time) / 60), 2))
    print(StoreEvalDB.vars)
    driver.quit()

except Exception:
    log.debug("+" + ("-" * (tableWidth + 36)) + "+")
    log.critical("Failed after %s minutes!!!" % round(((time.time() - start_time) / 60), 2))
    log.info("Screenshot: http://172.16.111.229/testSuit(Failed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").png")
    log.info("Log file: http://172.16.111.229/testSuit(Failed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").log")
    log.info(StoreEvalDB.vars)
    log.error(traceback.format_exc())
    original_size = driver.get_window_size()
    required_width = driver.execute_script('return document.body.parentNode.scrollWidth')
    required_height = driver.execute_script('return document.body.parentNode.scrollHeight')
    driver.set_window_size(required_width, required_height)
    driver.find_element_by_tag_name('body').screenshot("//home//gitlab-runner//seleniums//Log//testSuit(Failed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").png")
    sys.stdout = open("//home//gitlab-runner//seleniums//Log//testSuit(Failed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").log", "w", encoding="utf-8")
    print(Info)
    print(TestCases)
    print("Failed after %s minutes." % round(((time.time() - start_time) / 60), 2))
    print("http://172.16.111.229/testSuit(Failed-at-" + str(now.strftime("%Y-%m-%d-&-%H-%M-%S")) + ").png")
    print(StoreEvalDB.vars)
    print(traceback.format_exc())
    driver.quit()
    exit(1)
