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
        
        public void LaunchBrowser()
        {
            _maxTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MaxWaitTime"]);
            _browserName = ConfigurationManager.AppSettings["Browser"];
            switch (_browserName)
            {
                case "Chrome":
                    _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
            }
        }

        public void NavigateToURL(string URL)
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(URL);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_maxTimeOut));
        }

        public void ClickOn(By selector)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            element.Click();
        }

        public void ClickOn(IWebElement element)
        {
            element.Click();
        }

        public void EnterText(By selector, string text)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            element.Clear();
            element.SendKeys(text);
        }

        public string GetText(By selector)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return element.Text.Trim();
        }

        public bool IsElementVisible(By selector)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return element.Displayed;
        }

        public List<IWebElement> GetWebElements(By selector)
        {
            return _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(selector)).ToList();
        }

        public IWebElement GetWebElement(By selector)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(selector));
        }

        public void SelectElementByText(By selector,string text)
        {
            SelectElement selectElement = new SelectElement(GetWebElement(selector));
            selectElement.SelectByText(text);
        }

        public bool WaitForInvisibiltyOfElement(By selector)
        {
            return _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(selector));
        }

        public string GetAtttribute(By selector,string attributeName)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return element.GetAttribute(attributeName);
        }

        public void CloseBrowser()
        {
            _driver.Close();
            _driver.Quit();
        }
    }
}
