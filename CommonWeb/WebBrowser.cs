using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UITests.Web.Common
{
    public class WebBrowser
    {
        private static IWebDriver _driver;
        private static WebDriverWait _wait;
        private int _maxTimeOut;
        private string _browserName;

        //Launches local browser and creates instance for webdriverwait
        public void LaunchBrowser()
        {
            _maxTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MaxWaitTime"]);
            _browserName = ConfigurationManager.AppSettings["Browser"];
            switch (_browserName)
            {
                case "Chrome":
                    _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_maxTimeOut));
                    break;
            }
        }

        //To maximize browser window and navigate to URL
        public void NavigateToURL(string URL)
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(URL);
        }

        //Sends selector and clicks on the webelement
        public void ClickOn(By selector)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            element.Click();
        }

        //Sends webelement and clicks on it
        public void ClickOn(IWebElement element)
        {
            element.Click();
        }

        //Clears the text box content and sends the text to enter in it
        public void EnterText(By selector, string text)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            element.Clear();
            element.SendKeys(text);
        }

        //Returns the inner text present in the webelement
        public string GetText(By selector)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return element.Text.Trim();
        }

        //Checks whether element is displayed on webpage
        public bool IsElementVisible(By selector)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return element.Displayed;
        }

        //Sends the selector and returns collection of webelements 
        public List<IWebElement> GetWebElements(By selector)
        {
            return _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(selector)).ToList();
        }

        //Sends the selector and returns single webelement
        public IWebElement GetWebElement(By selector)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(selector));
        }

        //Selects an option using text, from a dropdown
        //Works for dropdowns with select kind of tags
        public void SelectElementByText(By selector, string text)
        {
            SelectElement selectElement = new SelectElement(GetWebElement(selector));
            selectElement.SelectByText(text);
        }

        //Waits untill webelement is disappeared on webpage
        public bool WaitForInvisibiltyOfElement(By selector)
        {
            return _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(selector));
        }

        //Sends selector, attributeName and returns attribute value
        public string GetAtttribute(By selector, string attributeName)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return element.GetAttribute(attributeName);
        }

        //Close & quit browser window
        public void CloseBrowser()
        {
            _driver.Close();
            _driver.Quit();
        }
    }
}
