using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AdminApp.Spec
{
    [Binding]
    public class InitWebDriver
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private static IWebDriver _webDriver;
        public static IWebDriver WebDriver
        {
            get
            {
                if(_webDriver == null)
                {
                    _webDriver = new ChromeDriver();
                }
                return _webDriver;
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            WebDriver.Navigate().GoToUrl(@"http://localhost/AdminApp/");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _webDriver.Quit();
            _webDriver = null;
        }
    }
}
