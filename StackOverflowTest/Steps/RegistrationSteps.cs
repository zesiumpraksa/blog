using HtmlAgilityPack;
using Models.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StackOverflowTest.Registration
{
    [Binding]
    public class RegistrationSteps
    {

        private async Task CheckRegisterPage()
        {
            HtmlDocument htmlGlobal = new HtmlDocument();
            HttpClient client = new HttpClient();

            var responseGetMsg = client.GetAsync("http://localhost:49853/User/Register");

            var responseContent = await responseGetMsg.Result.Content.ReadAsStreamAsync();
            var responseContentString = await responseGetMsg.Result.Content.ReadAsStringAsync();

            htmlGlobal.Load(responseContent);
            ScenarioContext.Current["HtmlGet"] = htmlGlobal;
        }
        private async Task CheckRegisterPage(Table table)
        {
            HttpClient client = new HttpClient();
            HtmlDocument htmlGlobal = new HtmlDocument();

            User testUser = table.CreateInstance<User>();

            var dict = new Dictionary<string, string>();

            dict.Add("Email", testUser.Email);
            dict.Add("UserName", testUser.UserName);
            dict.Add("FirstName", testUser.FirstName);
            dict.Add("LastName", testUser.LastName);
            dict.Add("Password", testUser.Password);
            dict.Add("RepeatPassword", testUser.RepeatPassword);

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var responsePostMsg = await client.PostAsync("http://localhost:49853/User/Register", content);
            var responseContent = await responsePostMsg.Content.ReadAsStreamAsync();
            var sadrzaj = responsePostMsg.Content.ReadAsStringAsync();

            htmlGlobal.Load(responseContent);
            ScenarioContext.Current["HtmlPost"] = htmlGlobal;

        }
        [Given(@"Client is on Register page")]
        public void GivenUserIsOnRegisterPage()
        {
            CheckRegisterPage().Wait();

            HtmlDocument htmlObject = ScenarioContext.Current["HtmlGet"] as HtmlDocument;
            Assert.NotNull(htmlObject.DocumentNode.SelectNodes("//h2[@id='Register']"), "register not found :(");
            Assert.NotNull(htmlObject.DocumentNode.SelectNodes("//input[@value='Create']"), "register not found :(");
        }

        [When(@"Client enter valid values and press Create")]
        public void WhenUserEnterValidValues(Table table)
        {
            CheckRegisterPage(table).Wait();
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
          
            Assert.IsNotNull(htmlObject,"htmlObj is null :(");
        }

        [Then(@"Client is on Index page")]
        public void ThenUserIsOnIndexPage()
        {
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//div[@class='imgBackground']"), "imgBackground not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@id='UserName']"), "UserName not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@id='Password']"), "Password not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//input[@type='submit']"), "submit button not found :(");
        }

        
        [When(@"Client enter invalid values and press Create")]
        public void WhenClientEnterInvalidValuesAndPressCreate(Table table)
        {
            CheckRegisterPage(table).Wait();
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            Assert.IsNotNull(htmlObject);
        }
        //warning Password
        [Then(@"Client get Password warning message")]
        public void ThenClientGetWarningMessage()
        {
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            var warningNode = htmlObject.DocumentNode.SelectNodes("//div[@class='validation-summary-errors text-danger']");
            var warningText = warningNode.FindFirst("li").InnerText;
            Assert.AreEqual("Passwords must have at least one digit (&#39;0&#39;-&#39;9&#39;). Passwords must have at least one uppercase (&#39;A&#39;-&#39;Z&#39;).", warningText);
        }
        //warning Email       
        
        [Then(@"Client get another  warning message")]
        public void ThenClientGetAnotherWarningMessage()
        {
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            var warningNode = htmlObject.DocumentNode.SelectNodes("//div[@class='validation-summary-errors text-danger']");
            var warningText = warningNode.FindFirst("li").InnerText;
            Assert.AreEqual("Email &#39;testMail&#39; is invalid.", warningText);
        }
        //warning for requerd inputs
        [Then(@"Client get warning message for required fields")]
        public void ThenClientGetWarningMessageForRequiredFields()
        {
            var htmlObject = ScenarioContext.Current["HtmlPost"] as HtmlDocument;
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//span[@data-valmsg-for='UserName']"), "UserName input not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//span[@data-valmsg-for='Password']"), "Password input not found :(");
            Assert.IsNotNull(htmlObject.DocumentNode.SelectNodes("//span[@data-valmsg-for='RepeatPassword']"), "RepeatPassword input not found :(");
        }


        //Warning scenario

      
    }
}
