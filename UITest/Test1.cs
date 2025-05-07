using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UnitTest1 {
        private static readonly string DriverDirectory = "C:\\webdriver";
        
        //private static readonly string macdriver = "/users/Shared/webdrivers/chromedriver";
        

        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
            //_driver = new ChromeDriver(macdriver);
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
            IWebElement element = wait.Until(d => d.FindElement(By.Id("DataLoaded"))); 
            

            //I toppen er prisen lige nu, kaldet energipris tjekker at der står en pris
            IWebElement Elpris = _driver.FindElement(By.Id("energiPris"));
            Assert.IsNotNull(Elpris);
            Assert.AreEqual("0.61493 DKK/kWh", Elpris.Text); 

            
            
            //tjekker at datoen er rigtig. Ned til timen
            IWebElement DatoOgTid= _driver.FindElement(By.Id("DatoTid"));
            Assert.AreEqual(DateTime.Now.ToString("dd.MM.yyyy, HH"), DatoOgTid.Text);
            Assert.IsNotNull(DatoOgTid); 
            
            


            //tjekker at der nu er valgt københavn


            var selectElement = _driver.FindElement(By.Id("Prisområde"));
            var select = new SelectElement(selectElement);
            select.SelectByText("Øst");

            //tjekker om der er står øst efter vi har valgt dropwown menuen

            IWebElement vistområde = _driver.FindElement(By.Id("PrisområdeNu"));
            Assert.AreEqual("DK2", vistområde.Text);


            // tjekker om grafen bliver vist
            IWebElement inputElement3 = _driver.FindElement(By.TagName("Canvas"));
            bool ShowsGraph = inputElement3.Displayed;
            Assert.IsTrue(ShowsGraph);

        }
        [TestMethod]
        public void TestMethodGrænser()
        {

            //LavGrænse er en dropdown menu, skal vælge en specifik "entry" på listen, tjekke at det passer


        }
    }
}