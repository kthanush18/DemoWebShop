using OpenQA.Selenium;
using System.Configuration;
using UITests.Web.Common;

namespace UITests.Web.DemoWebShop.Pages
{
    public class LoginPage : WebBrowser
    {
        private static readonly By welcome_Heading_Locator = By.TagName("h1");
        private static readonly By username_Textbox_Locator = By.Id("Email");
        private static readonly By password_Textbox_Locator = By.Id("Password");
        private static readonly By login_Button_Locator = By.XPath("//input [@value = 'Log in']");
        private static readonly string _username = ConfigurationManager.AppSettings["username"];
        private static readonly string _password = ConfigurationManager.AppSettings["password"];

        public string GetWelcomeMessage()
        {
            return GetText(welcome_Heading_Locator);
        }

        //Method for user login and creating dashboard instance
        public DashBoardPage EnterCredentialsAndLogin()
        {
            EnterText(username_Textbox_Locator, _username);
            EnterText(password_Textbox_Locator, _password);
            ClickOn(login_Button_Locator);
            return new DashBoardPage();
        }
    }
}
