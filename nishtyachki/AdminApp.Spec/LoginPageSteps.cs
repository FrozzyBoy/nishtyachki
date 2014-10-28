using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace AdminApp.Spec
{
    [Binding]
    public class LoginPageSteps
    {
        private IWebDriver WebDriver = InitWebDriver.WebDriver;

        private const string USER_NAME = "Allonsi";
        private const string USER_PASSWORD = "Fidelio";

        public const string BtnLogInID = "btnLogIn";
        public const string LinkLogOff = "Log off";

        private bool IsAuthenticated()
        {
            bool result = true;
            try
            {
                this.WebDriver.FindElement(By.LinkText(LinkLogOff));
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                result = false;
            }

            return result;
        }

        [Given(@"I have entered (.*) in field (.*)")]
        public void GivenIHaveEnteredInUserNameField(string value, string fieldId)
        {
            var btnPas = this.WebDriver.FindElement(By.Id(fieldId));
            btnPas.SendKeys(value);
        }

        [Given(@"I have been authenticated (.*)")]
        public void GivenIHaveBeenAuthenticated(bool isAuthenticated)
        {
            if (IsAuthenticated() != isAuthenticated)
            {
                if (isAuthenticated)
                {
                    GivenIHaveEnteredInUserNameField(USER_NAME, "UserName");
                    GivenIHaveEnteredInUserNameField(USER_PASSWORD, "Password");
                }

                WhenIPressLog(isAuthenticated);
            }
        }

        [When(@"I press to log (.*)")]
        public void WhenIPressLog(bool isLogIn)
        {
            IWebElement btnLog = null;

            if (isLogIn)
            {
                btnLog = this.WebDriver.FindElement(By.Id(BtnLogInID));
            }
            else
            {
                btnLog = this.WebDriver.FindElement(By.LinkText("Log off"));
            }

            btnLog.Submit();
        }

        [Then(@"log in result must be (.*)")]
        public void ThenLogInResultMustBe(bool result)
        {
            Thread.Sleep(500);
            Assert.AreEqual(IsAuthenticated(), result);
        }
    }
}
