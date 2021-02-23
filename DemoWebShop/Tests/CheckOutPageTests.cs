using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITests.Web.DemoWebShop.Pages;
using static UITests.Web.Common.WebBrowser;

namespace UITests.Web.DemoWebShop.Tests
{
    [TestClass]
    public class CheckOutPageTests : TestBase
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public new void TestIntialize()
        {
            base.TestIntialize();
            _landingPage = new LandingPage();
            _login = _landingPage.NavigateToLoginPage();
            _dashBoard = _login.EnterCredentialsAndLogin();
            _dashBoard.ClearShoppingCart();
            _dashBoard.SelectRequiredBook();
            _dashBoard.EnterRandomQuantity();
            _dashBoard.AddToCart();
            _dashBoard.NavigateToShoppingCart();
            _checkout = _dashBoard.NavigateToCheckOutPage();
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\TestData\BillingAddress.csv",
            "BillingAddress#csv", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TC_AddShippingAddressAndPayUsingCOD_VerifyPaymentMethod()
        {
            //Arrange
            string CODPaymentInfoMessage = "You will pay by COD";
            string firstName = TestContext.DataRow[0].ToString();
            string lastName = TestContext.DataRow[1].ToString();
            string email = TestContext.DataRow[2].ToString();
            string countryName = TestContext.DataRow[3].ToString();
            string stateName = TestContext.DataRow[4].ToString();
            string city = TestContext.DataRow[5].ToString();
            string address1 = TestContext.DataRow[6].ToString();
            string postalCode = TestContext.DataRow[7].ToString();
            string phoneNumber = TestContext.DataRow[8].ToString();

            //Act
            _checkout.SelectNewAddressFromDropDown();
            _checkout.FillAllMandatoryFieldsAndContinue(firstName,lastName,email,countryName,stateName,city,address1,postalCode,phoneNumber);
            _checkout.SelectNewelyAddedShippingAddressAndContinue(firstName,lastName,address1,city,postalCode,countryName);
            _checkout.SelectNextDayAirAndContinue();
            _checkout.SelectCODAndContinue();
            string CODPaymentInfoText = _checkout.GetCODPaymentInfoText();
            _checkout.SelectContinueFromCODConfirm();

            //Assert
            Assert.AreEqual(CODPaymentInfoText, CODPaymentInfoMessage);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\TestData\BillingAddress.csv",
            "BillingAddress#csv", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TC_ConfirmOrder_VerifyOrderSuccesfull()
        {
            //Arrange
            string orderSuccesfulMessage = "Your order has been successfully processed!";
            string firstName = TestContext.DataRow[0].ToString();
            string lastName = TestContext.DataRow[1].ToString();
            string email = TestContext.DataRow[2].ToString();
            string countryName = TestContext.DataRow[3].ToString();
            string stateName = TestContext.DataRow[4].ToString();
            string city = TestContext.DataRow[5].ToString();
            string address1 = TestContext.DataRow[6].ToString();
            string postalCode = TestContext.DataRow[7].ToString();
            string phoneNumber = TestContext.DataRow[8].ToString();

            //Act
            _checkout.SelectNewAddressFromDropDown();
            _checkout.FillAllMandatoryFieldsAndContinue(firstName, lastName, email, countryName, stateName, city, address1, postalCode, phoneNumber);
            _checkout.SelectNewelyAddedShippingAddressAndContinue(firstName, lastName, address1, city, postalCode, countryName);
            _checkout.SelectNextDayAirAndContinue();
            _checkout.SelectCODAndContinue();
            _checkout.SelectContinueFromCODConfirm();
            _checkout.ConfirmOrder();
            string orderSuccesfulInfo = _checkout.GetOrderSuccesfulMessage();
            _checkout.PrintOrderNumberAndContinue();

            //Assert
            Assert.AreEqual(orderSuccesfulInfo, orderSuccesfulMessage);
        }

        [TestCleanup]
        public new void TestCleanup()
        {
            _dashBoard.LogOut();
            base.TestCleanup();
        }
    }
}
