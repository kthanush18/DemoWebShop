using OpenQA.Selenium;
using UITests.Web.Common;

namespace UITests.Web.DemoWebShop.Pages
{
    public class LandingPage : WebBrowser
    {
        private static readonly By login_Link_Locator = By.XPath("//a [text() = 'Log in']");

        //Method for navigating to login page and creating instance
        public LoginPage NavigateToLoginPage()
        {
            ClickOn(login_Link_Locator);
            return new LoginPage();
        }
    }
}
