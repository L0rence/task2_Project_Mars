using System;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static Task2_Mars_Project.Utilities.CommonMethods;

namespace Task2_Mars_Project.Utilities
{
    public class Driver
    {
        //Initialize the browser

        public static IWebDriver driver;
        public static SignInPage SignIn;
        public static ExtentReports extent;
        public static ExtentHtmlReporter hTMLReporter;
        public static ExtentTest test;


        [OneTimeSetUp]
        public void Initialize()
        {
            hTMLReporter = new ExtentHtmlReporter(ConstantHelpers.ReportsPath);
            hTMLReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(hTMLReporter);

            //Defining the browser
            driver = new ChromeDriver(@"/Users/chriselyn/Projects/Task2_Mars_Project/Task2_Mars_Project/bin");
            //driver = new FirefoxDriver();

            //Maximise the window
            driver.Manage().Window.Maximize();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "Credentials");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ShareSkillTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ManageListingsTestData");

            //call the SignIn class
            SignIn = new SignInPage(driver);
            SignIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));

        }

        public static string BaseUrl
        {
            get { return ConstantHelpers.Url; }
        }


        public static void NavigateUrl()
        {
            driver.Navigate().GoToUrl(BaseUrl);
        }


        [OneTimeTearDown]
        public void FinalSteps()
        {
            // close the driver
            driver.Close();
            driver.Quit();
            extent.Flush();
        }


    }
}
