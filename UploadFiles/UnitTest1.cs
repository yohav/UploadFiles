using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;

namespace UploadFiles
{
    [TestClass]
    public class UnitTest1
    {
        private static string[] Files;
        private static Dictionary<string, Mime> extensionToMime = new Dictionary<string, Mime>()
        {
            {"doc", Mime.DOC },
            {"docx", Mime.DOCX },
            {"ppt", Mime.PPT },
            {"pptx", Mime.PPTX },
            {"xls", Mime.XLS },
            {"xlsx", Mime.XLSX }
        };
        private static NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static bool HasGoodPHP;

        private IWebDriver driver;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filesPath = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory)) + @"\Files";
            Files = Directory.GetFiles(filesPath);
            Boolean.TryParse(appSettings["HasPHPVersionHeigherThen5.5"], out HasGoodPHP);
        }

        [TestInitialize]
        public void TestInit()
        {
            driver = new ChromeDriver(appSettings["ChromeDriverPath"]);
            driver.Url = appSettings["Url"];
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestMethod1()
        {
            foreach (var file in Files)
            {
                //php 5.5 bug
                if(!HasGoodPHP && Path.GetFileNameWithoutExtension(file) == "PPT")
                {
                    continue;
                }
                IWebElement fileInput = driver.FindElement(By.CssSelector("input[type='file'"));
                IWebElement submit = driver.FindElement(By.CssSelector("input[type='submit']"));
                fileInput.SendKeys(file);
                submit.Click();
                string mimeType = driver.FindElement(By.ClassName("mime-type")).Text;
                string extension = Path.GetExtension(file).Trim('.');
                Assert.AreEqual(extensionToMime[extension].GetDescription(), mimeType);
                driver.Navigate().Back();
            }
        }
    }
}
