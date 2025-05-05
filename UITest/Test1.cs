using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UnitTest1 {
        private static readonly string DriverDirectory = "C:\\webdriver";
        
        private static readonly string macdriver = "/Users/david/Desktop/Webdriver mac/chromedriver";
        

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
        public void TestMethodFrontPage()
        {

            string url = "";
            _driver.Navigate().GoToUrl(url);

            //tjekker vi har åbnet rigtig side ved at tjekke titlen.
            Assert.AreEqual("Energi Tommel", _driver.Title);

            //I toppen er prisen lige nu, kaldet energipris tjekker at der står en pris
            IWebElement inputElement1 = _driver.FindElement(By.Id("energiPris"));
            Assert.IsNotNull(inputElement1);

            //tjekker at datoen er rigtig. Ned til timen
            IWebElement inputElement2 = _driver.FindElement(By.Id("dato og tid nu"));
            Assert.AreEqual(DateTime.Now.ToString("dd/MM/yyyy HH"), inputElement2.Text);

            // tjekker om grafen bliver vist
            IWebElement inputElement3 = _driver.FindElement(By.TagName("Canvas"));
            bool ShowsGraph = inputElement3.Displayed;
            Assert.IsTrue(ShowsGraph);

            //tjekker at der nu er valgt københavn
            
            
            
            IWebElement prisvalg = _driver.FindElement(By.Id("Prisområde"));
            SelectElement select = new SelectElement(prisvalg);
            select.SelectByText("øst"); 
            
                //tjekker om der er står øst efter vi har valgt dropwown menuen
                
                IWebElement vistområde = _driver.FindElement(By.Id("PrisområdeNu"));
            Assert.AreEqual("øst", vistområde.Text);





        }
        [TestMethod]
        public void TestMethodLimists()
        {

            //LavGrænse er en dropdown menu, skal vælge en specifik "entry" på listen, tjekke at det passer


        }
    }
}