using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using UITests.Web.DemoWebShop.Pages;

namespace UITests.Web.DemoWebShop.Tests
{
    [TestClass]
    public class LoginPageTests : TestBase
    {

        [TestInitialize]
        public new void TestIntialize()
        {
            base.TestIntialize();
            _landingPage = new LandingPage();
            _login = _landingPage.NavigateToLoginPage();
        }

        [TestMethod]
        [Description("Login with user credentials and verify user account ID")]
        public void TC_LoginToDashBoard_VerifyUserAccountID()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["username"];

            //Act
            _dashBoard = _login.EnterCredentialsAndLogin();

            //Assert
            Assert.AreEqual(_dashBoard.GetUserAccountID(), username);
        }

        [TestCleanup]
        public new void TestCleanup()
        {
            _dashBoard.LogOut();
            base.TestCleanup();
        }
    }
}
