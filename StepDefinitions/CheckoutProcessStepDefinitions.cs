using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace NopCommerceCheckout.StepDefinitions
{
    [Binding]
    public class CheckoutProcessStepDefinitions
    {
        private readonly IWebDriver driver;

        public CheckoutProcessStepDefinitions()
        {
            // Initialize Chrome WebDriver
            driver = new ChromeDriver();
        }


        [Given(@"I have selected a product and added it to the cart")]
        public void GivenIHaveSelectedAProductAndAddedItToTheCart()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Navigate to the demo nopCommerce website
            driver.Navigate().GoToUrl("https://demo.nopcommerce.com");
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            
            // Find and click on a product
            WebElementHelper.WaitAndClick(driver, By.XPath("//img[@alt='Picture of Apple MacBook Pro 13-inch']"));

            // Add the product to the cart
            WebElementHelper.WaitAndClick(driver, By.XPath("//*[@id='add-to-cart-button-4']"));
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            // Click on the checkout button
            WebElementHelper.WaitAndClick(driver, By.XPath("//*[contains(@href,'cart')]"));
            WebElementHelper.WaitAndClick(driver, By.Id("termsofservice"));
            WebElementHelper.WaitAndClick(driver, By.Id("checkout"));
            WebElementHelper.WaitAndClick(driver, By.CssSelector(".button-1.checkout-as-guest-button"));
        }

        [When(@"I enter shipping and billing information")]
        public void WhenIEnterShippingAndBillinginfo(Table table)
        {

            foreach (var row in table.Rows)
            {
                var firstName = row["FirstName"];
                var lastName = row["LastName"];
                var email = row["Email"];
                var country = row["Country"];
                var city = row["City"];
                var address = row["Address"];
                var postalCode = row["PostalCode"];
                var phoneNumber = row["PhoneNumber"];

                // Enter billing information
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_FirstName"), firstName);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_LastName"), lastName);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_Email"), email);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_CountryId"), country);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_City"), city);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_Address1"), address);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_ZipPostalCode"), postalCode);
                WebElementHelper.WaitAndSendKeys(driver, By.Id("BillingNewAddress_PhoneNumber"), phoneNumber);
                
            }

            // Complete The Order
            WebElementHelper.WaitAndClick(driver, By.Name("save"));
            WebElementHelper.WaitAndClick(driver, By.XPath("//*[@class='button-1 shipping-method-next-step-button']"));
            WebElementHelper.WaitAndClick(driver, By.XPath("//*[@class='button-1 payment-method-next-step-button']"));
            WebElementHelper.WaitAndClick(driver, By.XPath("//*[@class='button-1 payment-info-next-step-button']"));
            WebElementHelper.WaitAndClick(driver, By.XPath("//*[@class='button-1 confirm-order-next-step-button']"));
            WebElementHelper.WaitAndClick(driver, By.Id("BillingNewAddress_LastName"));
        }


        [Then(@"I should be able to complete the order")]
        public void ThenIShouldBeAbleToCompleteTheOrder()
        {
            // Assertion For Order Completion
            Assert.AreEqual("https://demo.nopcommerce.com/checkout/completed", driver.Url);
            Assert.IsTrue(driver.PageSource.Contains("Your order has been successfully processed!"));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}



