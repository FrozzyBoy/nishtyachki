using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;
using System.Linq;
using NUnit.Framework;

namespace AdminApp.Spec
{
    [Binding]
    public class HomePageSteps
    {
        private Process _nishtiakQueue;
        private int _numberOfNishtiaks = 0;

        [AfterScenario]
        public void AfterScenario()
        {
            if (_nishtiakQueue != null)
            {
                _nishtiakQueue.Kill();
            }
        }

        [Given(@"I have run queue (.*)")]
        public void GivenIHaveRunQueue(string pathToExe)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo(pathToExe);
            startinfo.CreateNoWindow = false;
            startinfo.WindowStyle = ProcessWindowStyle.Minimized;

            _nishtiakQueue = Process.Start(startinfo);
        }

        private IWebElement[] FindDeleteButtons()
        {
            var btns = InitWebDriver.WebDriver.FindElements(By.TagName("button"));
            var btnsDel = (from btn in btns
                           where btn.Text == "Delete"
                           select btn).ToArray<IWebElement>();

            return btnsDel;
        }

        [When(@"I press add (.*) nishtiak")]
        public void WhenIPressAddNishtiak(bool ifAdd)
        {
            Thread.Sleep(5000);

            _numberOfNishtiaks = FindAllNishtiaksId().Length;

            if (ifAdd)
            {
                _numberOfNishtiaks = FindAllNishtiaksId().Length;
                InitWebDriver.WebDriver.FindElement(By.Id("nishtiak_add_btn")).Click();
            }
            else
            {
                var btn = FindDeleteButtons()[0];
                btn.Click();
            }

        }

        [Then(@"the result should be appeare (.*) new nihtiak")]
        public void ThenTheResultShouldBeAppeareNewNihtiak(bool isAdd)
        {
            Thread.Sleep(1000);

            if (isAdd)
            {
                _numberOfNishtiaks++;
            }
            else
            {
                _numberOfNishtiaks--;
            }

            int newNumberOfNishtiaks = FindAllNishtiaksId().Length;
            Assert.AreEqual(_numberOfNishtiaks, newNumberOfNishtiaks);
        }


        private IWebElement[] FindAllNishtiaksId()
        {
            return InitWebDriver.WebDriver.FindElements(By.ClassName("nishtiakId")).ToArray<IWebElement>();
        }


        [When(@"I press delete nishtiak")]
        public void WhenIPressDeleteNishtiak()
        {
            Thread.Sleep(3000);

            var btns = InitWebDriver.WebDriver.FindElements(By.TagName("button"));

            var btn = btns.Single<IWebElement>(x => x.Text == "Delete");

            btn.Click();
        }
    }
}
