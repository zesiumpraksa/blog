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

namespace StackOverflowTest.Login
{
    public class CookieAwareWebClient : WebClient
    {
        public static CookieAwareWebClient Cooke { get; set; }
        public CookieAwareWebClient()
        {
            Cooke = this;
            CookieContainer = new CookieContainer();
            this.ResponseCookies = new CookieCollection();

        }

        public CookieContainer CookieContainer { get; private set; }

        public CookieCollection ResponseCookies { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = (HttpWebResponse)base.GetWebResponse(request);
            this.ResponseCookies = response.Cookies;
            return response;
        }
    }
    [Binding]
    public class LoginSteps
    {      

        private async Task CheckIndexPage()
        {
            HtmlDocument htmlGlobal = new HtmlDocument();
            var client = new HttpClient();
            var responseGetMsg = client.GetAsync("http://localhost:49853/Main/Index");

            var responseContent = await responseGetMsg.Result.Content.ReadAsStreamAsync();
            var responseContentString = await responseGetMsg.Result.Content.ReadAsStringAsync();            
                        
            htmlGlobal.Load(responseContent);
            ScenarioContext.Current["HtmlGet"] = htmlGlobal;
        }

        private void CheckLogIn(Table table)
        {
            CookieAwareWebClient.Cooke = new CookieAwareWebClient();
            HtmlDocument htmlGlobal = new HtmlDocument();
            var user = table.CreateInstance<User>();
            
            NameValueCollection nameValue = new NameValueCollection();

            nameValue.Add("username", user.UserName);
            nameValue.Add("password", user.Password);

            Uri uri = new Uri("http://localhost:49853/Main/Index");
            var res = CookieAwareWebClient.Cooke.UploadValues(uri, "POST", nameValue);
            //var response = Encoding.UTF8.GetString(res);
            //var values = CookieAwareWebClient.Cooke.CookieContainer.GetCookies(uri);
            Stream streamContent = new MemoryStream(res);
           

            htmlGlobal.Load(streamContent);
            ScenarioContext.Current["HtmlPost"] = htmlGlobal;
        }

        [Given(@"User is on Index page")]
        public void GivenUserIsInIndexPage()
        {
            CheckIndexPage().Wait();   

            var htmlObject = ScenarioContext.Current["HtmlGet"] as HtmlDocument;
            //var htmlObject1 = FeatureContext.Current["Feature"] as HtmlDocument;       

            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//div[@class='imgBackground']"), "imgBackground not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@id='UserName']"), "UserName not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@id='Password']"), "Password not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@type='submit']"), "submit button not found :(");
        }

        [When(@"User enter UserName and Password in form and press Login")]
        public void WhenUserEnterUserNameAndPasswordInFormAndPressLogin(Table table)
        {
            CheckLogIn(table);
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;            
            Assert.IsNotNull(htmlObject);
        }

        [Then(@"User is on his Dashboard")]
        public void ThenUserIsOnHisDashboard()
        {            
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//h2[@id='Dashboard']"), "Dashboard not found :(");           
        }

        //warning1 scenario

        [Then(@"User is on Index page with first warning information")]
        public void ThenUserIsOnIndexPageWithWarningInformation()
        {            
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            var warningMsg = htmlObject.GetElementbyId("warning").InnerText;
            Assert.AreEqual(" Bad username or password  ", warningMsg);
        }

        //warning2 scenario

        [Then(@"User is on Index page with second warning information")]
        public void ThenUserIsOnIndexPageWithSecondWarningInformation()
        {            
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            var warningMsg = htmlObject.GetElementbyId("warning").InnerText;
            Assert.AreEqual("Set username or password   ", warningMsg);
        }

        //LogOut

        [When(@"User click on Logout")]
        public void WhenUserClickOnLogout()
        {
            CheckIndexPage().Wait();
            var htmlObject = ScenarioContext.Current["HtmlGet"] as HtmlDocument;            
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@value='Logout']"), "submit button for log out is not found :(");
        }

        [Then(@"User is on on Index page and have message to log in")]
        public void ThenUserIsOnOnIndexPageAndHaveMessageToLogIn()
        {            
            var htmlObject = ScenarioContext.Current["HtmlGet"] as HtmlDocument;
            var warningMsg = htmlObject.GetElementbyId("warning").InnerText;
            Assert.AreEqual("  Not logged in ", warningMsg);
        }
    }
}
