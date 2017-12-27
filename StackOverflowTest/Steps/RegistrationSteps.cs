using HtmlAgilityPack;
using Models.Models;
using NUnit.Framework;
using StackOverflowTest.Steps;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StackOverflowTest.Registration
{
    [Binding]
    public class RegistrationSteps
    {

        public static HtmlDocument GetRequest(string Controler, string Action)
        {
            HtmlDocument htmlGet = new HtmlDocument();
            
            Uri uri = new Uri("http://localhost:49853/"+ Controler + "/"+ Action);

            var res = CookieAwareWebClient.InstanceCookie.OpenRead(uri);
            //var res = CookieAwareWebClient.InstanceCookie.OpenRead(uri);
            htmlGet.Load(res);
            return htmlGet;
        }

        private HtmlDocument PostRegistration(Table table)
        {
            var testUser = table.CreateInstance<User>();
            HtmlDocument htmlIndexPage = new HtmlDocument();

            Uri uri = new Uri("http://localhost:49853/User/Register");
            NameValueCollection nameValue = new NameValueCollection();

            nameValue.Add("Email", testUser.Email);
            nameValue.Add("UserName", testUser.UserName);
            nameValue.Add("FirstName", testUser.FirstName);
            nameValue.Add("LastName", testUser.LastName);
            nameValue.Add("Password", testUser.Password);
            nameValue.Add("RepeatPassword", testUser.RepeatPassword);


            //var res = CookieAwareWebClient.Cooke.UploadValues(uri, "POST", nameValue);
            //Stream streamContent = new MemoryStream(res);
            var res = CookieAwareWebClient.InstanceCookie.UploadValues(uri, "POST", nameValue);
            Stream streamContent = new MemoryStream(res);
            htmlIndexPage.Load(streamContent);

            return htmlIndexPage;
        }

        [Given(@"Client is on Index page")]
        public void GivenClientIsOnIndexPage()
        {
            
          
            var htmlIndexPage = GetRequest("Main","Index");         
            
            
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//div[@class='imgBackground']"), "imgBackground not found :(");
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//input[@id='UserName']"), "UserName not found :(");
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//input[@id='Password']"), "Password not found :(");
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//input[@type='submit']"), "submit button not found :(");
        }

        [Then(@"Client go on Register page")]
        public void ThenClientGoOnRegisterPage()
        {

            var htmlRegisterPage = GetRequest("User", "Register");

            Assert.IsNotNull(htmlRegisterPage.DocumentNode.SelectNodes("//input[@id='UserName']"), "UserName not found :(");
            Assert.IsNotNull(htmlRegisterPage.DocumentNode.SelectNodes("//input[@id='Password']"), "Password not found :(");
            Assert.IsNotNull(htmlRegisterPage.DocumentNode.SelectNodes("//input[@id='RepeatPassword']"), "RepeatPassword not found :(");

        }
       
        [When(@"Client enter valid values and press Create")]
        public void WhenUserEnterValidValues(Table table)
        {

            var htmlIndexPage = PostRegistration(table);
            Assert.IsNotNull(htmlIndexPage);

            ScenarioContext.Current["htmlIndex"] = htmlIndexPage;

        }

        [Then(@"Client is on Index page")]
        public void ThenUserIsOnIndexPage()
        {
            var htmlIndexPage = ScenarioContext.Current["htmlIndex"] as HtmlDocument;

            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//div[@class='imgBackground']"), "imgBackground not found :(");
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//input[@id='UserName']"), "UserName not found :(");
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//input[@id='Password']"), "Password not found :(");
            Assert.IsNotNull(htmlIndexPage.DocumentNode.SelectNodes("//input[@type='submit']"), "submit button not found :(");
        }


        //warning1

        [When(@"Client enter invalid values and press Create")]
        public void WhenClientEnterInvalidValuesAndPressCreate(Table table)
        {

            var htmlWarning = PostRegistration(table);           

            Assert.IsNotNull(htmlWarning);

            ScenarioContext.Current["warning"] = htmlWarning;



        }
        //warning Password
        [Then(@"Client get Password warning message")]
        public void ThenClientGetWarningMessage()
        {
            var warning = ScenarioContext.Current["warning"] as HtmlDocument;
            
            var warningNode = warning.DocumentNode.SelectNodes("//div[@class='validation-summary-errors text-danger']");
            var warningText = warningNode.FindFirst("li").InnerText;
            Assert.AreEqual("Passwords must have at least one digit (&#39;0&#39;-&#39;9&#39;). Passwords must have at least one uppercase (&#39;A&#39;-&#39;Z&#39;).", warningText);
        }
        ////warning Email       

        [Then(@"Client get another  warning message")]
        public void ThenClientGetAnotherWarningMessage()
        {
            var warning = ScenarioContext.Current["warning"] as HtmlDocument;
            
            var warningNode = warning.DocumentNode.SelectNodes("//div[@class='validation-summary-errors text-danger']");
            var warningText = warningNode.FindFirst("li").InnerText;
            Assert.NotNull(warningText);
            //Assert.AreEqual("Email &#39;testMail&#39; is invalid.", warningText);
        }
        ////warning for requerd inputs
        [Then(@"Client get warning message for required fields")]
        public void ThenClientGetWarningMessageForRequiredFields()
        {
            var warning = ScenarioContext.Current["warning"] as HtmlDocument;

            Assert.IsNotNull(warning.DocumentNode.SelectNodes("//span[@data-valmsg-for='UserName']"), "UserName input not found :(");
            Assert.IsNotNull(warning.DocumentNode.SelectNodes("//span[@data-valmsg-for='Password']"), "Password input not found :(");
            Assert.IsNotNull(warning.DocumentNode.SelectNodes("//span[@data-valmsg-for='RepeatPassword']"), "RepeatPassword input not found :(");
        }


        //Warning scenario


    }
}
