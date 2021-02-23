using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITests.Web.DemoWebShop.Pages;

namespace UITests.Web.DemoWebShop.Tests
{
    [TestClass]
    public class LandingPageTests : TestBase
    {
        [TestInitialize]
        public new void TestIntialize()
        {
            base.TestIntialize();
            _landingPage = new LandingPage();
        }

        [TestMethod]
        [Description("Navigate to login page and verify welcome message displayed")]
        public void TC_NavigateToLoginPage_VerifyWelcomeMessage()
        {
            //Arrange
            string messageText = "Welcome, Please Sign In!";

            //Act
            _login = _landingPage.NavigateToLoginPage();

            //Assert
            Assert.AreEqual(_login.GetWelcomeMessage(), messageText);
        }

        [TestCleanup]
        public new void TestCleanup()
        {
            base.TestCleanup();
        }
    }
}
