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
        public void TC_LoginToDashBoard_VerifyUserAccountID()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["username"];

            //Act
            _dashBoard = _login.EnterCredentialsAndLogin();

            //Assert
            Assert.IsTrue(_dashBoard.IsAccountIDDisplayed());
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
