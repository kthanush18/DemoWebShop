using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using UITests.Web.Common;

namespace UITests.Web.DemoWebShop.Pages
{
    public class DashBoardPage : WebBrowser
    {
        private static readonly By user_Account_Locator = By.XPath("//div [@class = 'header-links']//a [@class = 'account']");
        private static readonly By shopping_Cart_Quantity_Locator = By.XPath("//span [@class = 'cart-qty']");
        private static readonly By shopping_Cart_Link_Locator = By.Id("topcartlink");
        private static readonly By remove_CheckBox_Locator = By.Name("removefromcart");
        private static readonly By update_Cart_Button_Locator = By.Name("updatecart");
        private static readonly By dashBoard_Link_Locator = By.XPath("//a [@href = '/']");
        private static readonly By books_Link_Locator = By.XPath("//strong [text() = 'Categories']//following::a [contains(text(),'Books')]");
        private static readonly By fiction_Book_Image_Locator = By.XPath("//img [@title = 'Show details for Fiction']");
        private static readonly By quantity_Text_Box_Locator = By.XPath("//input [@class = 'qty-input']");
        private static readonly By addtocart_Button_Locator = By.Id("add-to-cart-button-45");
        private static readonly By page_Loading_Locator = By.XPath("//div [@style = 'display: block;']");
        private static readonly By product_Added_Message_Locator = By.XPath("//div [@id = 'bar-notification']/p");
        private static readonly By product_Unit_Price_Locator = By.XPath("//span [@class = 'product-unit-price']");
        private static readonly By product_Quantity_Locator = By.XPath("//input [@class = 'qty-input']");
        private static readonly By product_Sub_Total_Price_Locator = By.XPath("//span [@class = 'product-price']");
        private static readonly By checkout_Button_Locator = By.Id("checkout");
        private static readonly By termsOfService_Checkbox_Locator = By.Id("termsofservice");
        private static readonly By logout_Link_Locator = By.XPath("//a [text() = 'Log out']");


        public string GetUserAccountID()
        {
            return GetText(user_Account_Locator);
        }

        public void NavigateToShoppingCart()
        {
            ClickOn(shopping_Cart_Link_Locator);
        }

        //Removes all the products in the shopping cart
        public void ClearShoppingCart()
        {
            string s = GetText(shopping_Cart_Quantity_Locator);
            if (GetText(shopping_Cart_Quantity_Locator) != "(0)")
            {
                NavigateToShoppingCart();
                List<IWebElement> removeCheckBoxes = GetWebElements(remove_CheckBox_Locator);
                foreach (IWebElement removeCheckBox in removeCheckBoxes)
                {
                    ClickOn(removeCheckBox);
                }
                ClickOn(update_Cart_Button_Locator);
                ClickOn(dashBoard_Link_Locator);
            }
        }

        //Selects books from categories and fiction book from books list
        public void SelectFictionBook()
        {
            ClickOn(books_Link_Locator);
            ClickOn(fiction_Book_Image_Locator);
        }

        //This method sends minimum, maximum values and returns a random value within the range
        public int GetRandomNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        //Generates random quantity and enters in quantity text box
        public void EnterRandomQuantity()
        {
            int randomQuantity = GetRandomNumber(2, 6);
            EnterText(quantity_Text_Box_Locator, randomQuantity.ToString());
        }

        //This method adds products to cart and waits for preloader to load
        public void AddToCart()
        {
            ClickOn(addtocart_Button_Locator);
            WaitForInvisibiltyOfElement(page_Loading_Locator);
        }

        //Returns confirmation message on product added to cart
        public string GetProductAddedMessage()
        {
            return GetText(product_Added_Message_Locator);
        }

        //Evaluates sub total price from product price and quantity
        public string GetSubTotalFromPriceAndQuantity()
        {
            double unitProductPrice = double.Parse(GetText(product_Unit_Price_Locator));
            int quantity = Int32.Parse(GetAtttribute(product_Quantity_Locator, "value"));
            return $"{(unitProductPrice * quantity).ToString()}.00";
        }

        //Returns sub total price displayed in shopping cart
        public string GetSubTotalFromUI()
        {
            return GetText(product_Sub_Total_Price_Locator);
        }

        //This method checks terms of service check box and navigates to checkoutpage
        public CheckOutPage NavigateToCheckOutPage()
        {
            ClickOn(termsOfService_Checkbox_Locator);
            ClickOn(checkout_Button_Locator);
            return new CheckOutPage();
        }

        //Logout from demo webpage
        public void LogOut()
        {
            ClickOn(logout_Link_Locator);
        }
    }
}
