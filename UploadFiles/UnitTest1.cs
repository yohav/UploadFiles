using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

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


        [ClassInitialize]
        public static void Init(TestContext context)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filesPath = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory)) + @"\Files";
            Files = Directory.GetFiles(filesPath);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var appSettings = ConfigurationManager.AppSettings;
            IWebDriver d = new ChromeDriver(appSettings["ChromeDriverPath"]);
            d.Url = appSettings["Url"];
            foreach (var file in Files)
            {
                //php 5.5 bug
                if(Path.GetFileNameWithoutExtension(file) == "PPT")
                {
                    continue;
                }
                IWebElement fileInput = d.FindElement(By.CssSelector("input[type='file'"));
                IWebElement submit = d.FindElement(By.CssSelector("input[type='submit']"));
                fileInput.SendKeys(file);
                submit.Click();
                string mimeType = d.FindElement(By.ClassName("mime-type")).Text;
                string extension = Path.GetExtension(file).Trim('.');
                Assert.AreEqual(extensionToMime[extension].GetDescription(), mimeType);
                d.Navigate().Back();
            }
            d.Quit();
        }
    }
}
