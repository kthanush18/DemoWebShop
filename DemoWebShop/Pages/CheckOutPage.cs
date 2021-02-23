using OpenQA.Selenium;
using System;
using UITests.Web.Common;

namespace UITests.Web.DemoWebShop.Pages
{
    public class CheckOutPage : WebBrowser
    {
        private static readonly By billing_Address_Dropdown_Locator = By.Id("billing-address-select");
        private static readonly By first_Name_TextBox_Locator = By.Id("BillingNewAddress_FirstName");
        private static readonly By last_Name_TextBox_Locator = By.Id("BillingNewAddress_LastName");
        private static readonly By email_TextBox_Locator = By.Id("BillingNewAddress_Email");
        private static readonly By country_Dropdown_Locator = By.Id("BillingNewAddress_CountryId");
        private static readonly By state_Dropdown_Locator = By.Id("BillingNewAddress_StateProvinceId");
        private static readonly By city_TextBox_Locator = By.Id("BillingNewAddress_City");
        private static readonly By address1_TextBox_Locator = By.Id("BillingNewAddress_Address1");
        private static readonly By postalCode_TextBox_Locator = By.Id("BillingNewAddress_ZipPostalCode");
        private static readonly By phoneNumber_TextBox_Locator = By.Id("BillingNewAddress_PhoneNumber");
        private static readonly By shipping_Address_Dropdown_Locator = By.Id("shipping-address-select");
        private static readonly By continue_Billing_Button_Locator = By.XPath("//input [@onclick = 'Billing.save()']");
        private static readonly By continue_Shipping_Button_Locator = By.XPath("//input [@onclick = 'Shipping.save()']");
        private static readonly By nextDayAir_Button_Locator = By.Id("shippingoption_1");
        private static readonly By continue_ShippingMethod_Button_Locator = By.XPath("//input [@onclick = 'ShippingMethod.save()']");
        private static readonly By COD_Option_Locator = By.XPath("//label [@for = 'paymentmethod_0']/..");
        private static readonly By continue_Payment_Button_Locator = By.XPath("//input [@onclick = 'PaymentMethod.save()']");
        private static readonly By payment_Method_Info_Locator = By.XPath("//div [@class = 'info']//p");
        private static readonly By continue_Payment_Info_Button_Locator = By.XPath("//input [@onclick = 'PaymentInfo.save()']");
        private static readonly By continue_Confirm_Order_Button_Locator = By.XPath("//input [@onclick = 'ConfirmOrder.save()']");
        private static readonly By order_Succesful_Message_Locator = By.XPath("//strong");
        private static readonly By order_Details_Locator = By.XPath("//ul [@class = 'details']/li");
        private static readonly By continue_Succesful_Order_Button_Locator = By.XPath("//input [@class = 'button-2 order-completed-continue-button']");

        public void SelectNewAddressFromDropDown()
        {
            SelectElementByText(billing_Address_Dropdown_Locator, "New Address");
        }

        //This method sends all the data required for address fields and enters data to proceed
        public void FillAllMandatoryFieldsAndContinue(string firstName,string lastName,string email,string countryName,
            string stateName,string city,string address1,string postalCode,string phoneNumber)
        {
            EnterText(first_Name_TextBox_Locator, firstName);
            EnterText(last_Name_TextBox_Locator, lastName);
            EnterText(email_TextBox_Locator, email);
            SelectElementByText(country_Dropdown_Locator, countryName);
            SelectElementByText(state_Dropdown_Locator, stateName);
            EnterText(city_TextBox_Locator, city);
            EnterText(address1_TextBox_Locator, address1);
            EnterText(postalCode_TextBox_Locator, postalCode);
            EnterText(phoneNumber_TextBox_Locator, phoneNumber);
            ClickOn(continue_Billing_Button_Locator);
        }

        //Sends data required to build dropdown address text and selects the newely added address
        public void SelectNewelyAddedShippingAddressAndContinue(string firstName,string lastName,string address1,string city,
            string postalCode, string countryName)
        {
            string buildAddressText = $"{firstName} {lastName}, {address1}, {city} {postalCode}, {countryName}";
            SelectElementByText(shipping_Address_Dropdown_Locator, buildAddressText);
            ClickOn(continue_Shipping_Button_Locator);
        }

        //This method will select 'Next Day Air' shipping method and proceed
        public void SelectNextDayAirAndContinue()
        {
            ClickOn(nextDayAir_Button_Locator);
            ClickOn(continue_ShippingMethod_Button_Locator);
        }

        //This method will select 'COD' payment method and proceed
        public void SelectCODAndContinue()
        {
            ClickOn(COD_Option_Locator);
            ClickOn(continue_Payment_Button_Locator);
        }

        //Returns payment info text message
        public string GetCODPaymentInfoText()
        {
            return GetText(payment_Method_Info_Locator);
        }

        //Method to confirm order using COD as payment method
        public void SelectContinueFromCODConfirm()
        {
            ClickOn(continue_Payment_Info_Button_Locator);
        }

        public void ConfirmOrder()
        {
            ClickOn(continue_Confirm_Order_Button_Locator);
        }

        public string GetOrderSuccesfulMessage()
        {
            return GetText(order_Succesful_Message_Locator);
        }

        //Prints order details and proceed
        public void PrintOrderNumberAndContinue()
        {
            Console.WriteLine(GetText(order_Details_Locator).Trim());
            ClickOn(continue_Succesful_Order_Button_Locator);
        }
    }
}
