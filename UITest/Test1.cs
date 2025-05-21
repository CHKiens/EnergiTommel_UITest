using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UnitTest1 {
        //Sti til windows driver (burde ikke behøves at ændre)
        private static readonly string DriverDirectory = "C:\\webdriver";
        
        //Sti til mac driver (skal ændres til din mac driver sti)
        private static readonly string macdriver = "/users/Shared/webdrivers/chromedriver";
        

        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
            _driver = new ChromeDriver(macdriver);
        }
        
        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }


        [TestMethod]
        public void TestTitel()
        {
            string url = "http://127.0.0.1:5500/Index.html";
            _driver.Navigate().GoToUrl(url);

            //tjekker vi har åbnet rigtig side ved at tjekke titlen.
            Assert.AreEqual("Energi Tommel", _driver.Title);
        }
        [TestMethod]
        public void TestMethodForside()
        {
            string url = "http://127.0.0.1:5500/Index.html";
            _driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(d => d.FindElement(By.Id("IsDataLoaded"))); 
            

            //I toppen er prisen lige nu, kaldet energipris tjekker at der står en pris
            IWebElement Elpris = _driver.FindElement(By.Id("energiPris"));
            Assert.IsNotNull(Elpris);
            // Assert.AreEqual("0.03731 DKK/kWh", Elpris.Text); 

            
            
            //tjekker at datoen er rigtig. Ned til timen
            IWebElement DatoOgTid= _driver.FindElement(By.Id("DatoTid"));
            Assert.AreEqual(DateTime.Now.ToString("dd.MM.yyyy, HH.00"), DatoOgTid.Text);
            Assert.IsNotNull(DatoOgTid); 
            
            


            //tjekker at der nu er valgt københavn


            var selectElement = _driver.FindElement(By.Id("Prisområde"));
            var select = new SelectElement(selectElement);
            select.SelectByText("East");

            //tjekker om der er står øst efter vi har valgt dropwown menuen

            IWebElement vistområde = _driver.FindElement(By.Id("PrisOmrådeNu"));
            Assert.AreEqual("East", vistområde.Text);


            // tjekker om grafen bliver vist
            IWebElement inputElement3 = _driver.FindElement(By.TagName("Canvas"));
            bool ShowsGraph = inputElement3.Displayed;
            Assert.IsTrue(ShowsGraph);

            
            
            //Test af Select price range funktionen
            //normale interval

            IWebElement inputElementHigh = _driver.FindElement(By.Id("highinterval"));
            inputElementHigh.SendKeys("1");

            IWebElement inputElementLow = _driver.FindElement(By.Id("lowinterval"));
            inputElementLow.SendKeys("");
            
            IWebElement submitButton = _driver.FindElement(By.Id("submit"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            Thread.Sleep(200); // lille delay for stabilitet
            submitButton.Click();
            
            WebDriverWait waitForError = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement elementError = wait.Until(d => d.FindElement(By.Id("formError")));
            
            Assert.AreEqual("Enter both high and low value.", _driver.FindElement(By.Id("formError")).Text);
            
            inputElementLow.Clear();
            inputElementHigh.Clear();
            

            inputElementHigh.SendKeys("1");
            
            inputElementLow.SendKeys("0.5");
            submitButton.Click();
            
            WebDriverWait waitForSuccess = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement elementSuccess = wait.Until(d => d.FindElement(By.Id("formsuccess"))); 
            
            Assert.AreEqual("success", _driver.FindElement(By.Id("formsuccess")).Text);
            

            

            
            //fejlmeddelelse

            
            

            
        }
    }
}