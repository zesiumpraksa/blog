using NUnit.Framework;
using System.Net.Http;
using TechTalk.SpecFlow;
using HtmlAgilityPack;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;
using Models.Models;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System;
using StackOverflowTest.Steps;
using StackOverflowTest.Registration;

namespace StackOverflowTest.Login
{

    [Binding]
    public class LoginSteps
    {

        private HtmlDocument PostLogin(Table table)
        {
            var testUser = table.CreateInstance<User>();

            HtmlDocument htmlIndexPage = new HtmlDocument();

            Uri uri = new Uri("http://localhost:49853/Main/Index");
            NameValueCollection nameValue = new NameValueCollection();

            nameValue.Add("UserName", testUser.UserName);
            nameValue.Add("Password", testUser.Password);

            var res = CookieAwareWebClient.InstanceCookie.UploadValues(uri, "POST", nameValue);
            Stream streamContent = new MemoryStream(res);
            htmlIndexPage.Load(streamContent);

            return htmlIndexPage;
        }



        [When(@"Client enter UserName and Password in form and press Login")]
        public void WhenClientEnterUserNameAndPasswordInFormAndPressLogin(Table table)
        {
            HtmlDocument htmlIndexPage = PostLogin(table);          

            ScenarioContext.Current["html"] = htmlIndexPage;
            Assert.IsNotNull(htmlIndexPage);
        }

        [Then(@"Proba is on his Dashboard")]
        public void ThenUserIsOnHisDashboard()
        {
            var html = ScenarioContext.Current["html"] as HtmlDocument;
            
            Assert.IsNotNull(html.DocumentNode.SelectNodes("//h2[@id='Dashboard']"), "Dashboard not found :(");
        }

        //    //warning1 scenario

        [Then(@"User is on Index page with first warning information")]
        public void ThenUserIsOnIndexPageWithWarningInformation()
        {
            var html = ScenarioContext.Current["html"] as HtmlDocument;
            var warningMsg = html.GetElementbyId("warning").InnerText;
            Assert.AreEqual("Bad username or password", warningMsg.Trim());
        }

        //    //warning2 scenario

        [Then(@"User is on Index page with second warning information")]
        public void ThenUserIsOnIndexPageWithSecondWarningInformation()
        {
            var htmlObject = ScenarioContext.Current["html"] as HtmlDocument;
            var warningMsg = htmlObject.GetElementbyId("warning").InnerText.Trim();
            Assert.AreEqual("Set username or password", warningMsg);
        }

        //    //LogOut

        [When(@"User click on Logout")]
        public void WhenUserClickOnLogout()
        {

            // Assert.IsNotNull(html.DocumentNode.SelectNodes("//input[@value='Logout']"), "submit button for log out is not found :(");

            var htmlIndexPage = RegistrationSteps.GetRequest("Main", "LogOut");
            Assert.IsNotNull(htmlIndexPage);
            ScenarioContext.Current["htmlIndex"] = htmlIndexPage;
        }

        [Then(@"User is on on Index page and have message to log in")]
        public void ThenUserIsOnOnIndexPageAndHaveMessageToLogIn()
        {
            var htmlIndex = ScenarioContext.Current["htmlIndex"] as HtmlDocument;
            var warningMsg = htmlIndex.GetElementbyId("warning").InnerText;

            Assert.IsNotNull(htmlIndex.DocumentNode.SelectNodes("//div[@class='imgBackground']"), "imgBackground not found :(");
            Assert.IsNotNull(htmlIndex.DocumentNode.SelectNodes("//input[@id='UserName']"), "UserName not found :(");
            Assert.IsNotNull(htmlIndex.DocumentNode.SelectNodes("//input[@id='Password']"), "Password not found :(");
            Assert.IsNotNull(htmlIndex.DocumentNode.SelectNodes("//input[@type='submit']"), "submit button not found :(");

            Assert.AreEqual("Not logged in", warningMsg.Trim());
        }
    }
}

