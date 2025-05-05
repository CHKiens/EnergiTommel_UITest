using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\webdrivers";

        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory); 
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethod()
        {
            
            string url = "file:///C:/Users/caspe/Documents/EnergiTommel/index.html";
            _driver.Navigate().GoToUrl(url);

            Assert.AreEqual("Energi Tommel", _driver.Title);

            //Skal tjekket at energi pris har en værdi
            IWebElement inputElement1 = _driver.FindElement(By.Id("energiPris"));
            Assert.IsNotNull(inputElement1);

            //LavGrænse er en dropdown menu, skal vælge en specifik "entry" på listen, tjekke at det passer
            IWebElement inputElement2 = _driver.FindElement(By.Id("lavGrænse"));
            inputElement2.Click();



 
        }
    }
}