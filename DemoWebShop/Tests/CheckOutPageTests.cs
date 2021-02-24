using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITests.Web.DemoWebShop.Pages;

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
            _dashBoard.SelectFictionBook();
            _dashBoard.EnterRandomQuantity();
            _dashBoard.AddToCart();
            _dashBoard.NavigateToShoppingCart();
            _checkout = _dashBoard.NavigateToCheckOutPage();
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\TestData\BillingAddress.csv",
            "BillingAddress#csv", DataAccessMethod.Sequential)]
        [TestMethod]
        [Description("Add new address for shipping, pay using COD and verify payment method info")]
        public void TC_AddShippingAddressAndPayUsingCOD_VerifyPaymentMethod()
        {
            //Arrange
            string CODPaymentInfoMessage = "You will pay by COD";
            string firstName = TestContext.DataRow["firstName"].ToString();
            string lastName = TestContext.DataRow["lastName"].ToString();
            string email = TestContext.DataRow["email"].ToString();
            string countryName = TestContext.DataRow["countryName"].ToString();
            string stateName = TestContext.DataRow["stateName"].ToString();
            string city = TestContext.DataRow["city"].ToString();
            string address1 = TestContext.DataRow["address1"].ToString();
            string postalCode = TestContext.DataRow["postalCode"].ToString();
            string phoneNumber = TestContext.DataRow["phoneNumber"].ToString();

            //Act
            _checkout.SelectNewAddressFromDropDown();
            _checkout.FillAllMandatoryFieldsAndContinue(firstName, lastName, email, countryName, stateName, city,
                address1, postalCode, phoneNumber);
            _checkout.SelectNewelyAddedShippingAddressAndContinue(firstName, lastName, address1, city, postalCode, countryName);
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
        [Description("Confirm order on selecting payment method as COD and verify order placed successfully")]
        public void TC_ConfirmOrder_VerifyOrderSuccesfull()
        {
            //Arrange
            string orderSuccesfulMessage = "Your order has been successfully processed!";
            string firstName = TestContext.DataRow["firstName"].ToString();
            string lastName = TestContext.DataRow["lastName"].ToString();
            string email = TestContext.DataRow["email"].ToString();
            string countryName = TestContext.DataRow["countryName"].ToString();
            string stateName = TestContext.DataRow["stateName"].ToString();
            string city = TestContext.DataRow["city"].ToString();
            string address1 = TestContext.DataRow["address1"].ToString();
            string postalCode = TestContext.DataRow["postalCode"].ToString();
            string phoneNumber = TestContext.DataRow["phoneNumber"].ToString();

            //Act
            _checkout.SelectNewAddressFromDropDown();
            _checkout.FillAllMandatoryFieldsAndContinue(firstName, lastName, email, countryName, stateName, city,
                address1, postalCode, phoneNumber);
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
