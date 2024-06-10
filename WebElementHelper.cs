using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

public static class WebElementHelper
{
    public static void WaitAndClick(IWebDriver driver, By locator, int timeoutInSeconds = 20)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while waiting and clicking: {ex.Message}");
        }
    }

    public static void WaitAndSendKeys(IWebDriver driver, By locator, string text, int timeoutInSeconds = 10)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            element.SendKeys(text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while waiting and sending keys: {ex.Message}");
        }
    }
}
