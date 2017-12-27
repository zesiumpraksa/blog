using HtmlAgilityPack;
using Model.Models;
using Models.Models;
using NUnit.Framework;
using StackOverflowTest.Login;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StackOverflowTest.Steps
{
    [Binding]
    public class BlogSteps
    {
        [When(@"User Proba click Create new blog")]
        public void WhenUserTestClickCreateNewBlog()
        {
            HtmlDocument htmlNewBlog = new HtmlDocument();

            Uri uri = new Uri("http://localhost:49853/Blog/CreateBlog");

            var res = CookieAwareWebClient.InstanceCookie.OpenRead(uri);
            htmlNewBlog.Load(res);

            ScenarioContext.Current["Blog"] = htmlNewBlog;

            Assert.NotNull(res, "New Blog is null");

        }
        [Then(@"User is on CreateNewBlogPage")]
        public void ThenUserIsOnCreateNewBlogPage()
        {
            var htmlNewBlog = ScenarioContext.Current["Blog"] as HtmlDocument;

            Assert.IsNotNull(htmlNewBlog.DocumentNode.SelectNodes("//input[@id='Titile']"), "Ttile not found :(");
            Assert.IsNotNull(htmlNewBlog.DocumentNode.SelectNodes("//label [@for='Content']"), "Content not found :(");
            Assert.IsNotNull(htmlNewBlog.DocumentNode.SelectNodes("//input[@value='Create']"), "Submit create not found :(");
        }

        [When(@"Test insert values for new blogs and press Create")]
        public void WhenUsernameInsertValuesForNewBlogs(Table table)
        {
            var blog = table.CreateInstance<Blog>();
            HtmlDocument htmlNewBlog = new HtmlDocument();

            Uri uri = new Uri("http://localhost:49853/Blog/CreateBlog");
            NameValueCollection nameValue = new NameValueCollection();

            nameValue.Add("Titile", blog.Titile);
            nameValue.Add("Content", blog.Content);

            var res = CookieAwareWebClient.InstanceCookie.UploadValues(uri, "POST", nameValue);
            Stream streamContent = new MemoryStream(res);

            htmlNewBlog.Load(streamContent);
            Assert.IsNotNull(res);

            FeatureContext.Current["newBlog"] = htmlNewBlog;
        }

        [Then(@"User is on Blog Index page")]
        public void ThenUserIsOnBlogIndexPageWithNewBlogInformations()
        {
            var htmlNewBlog = FeatureContext.Current["newBlog"] as HtmlDocument;
            //var index = FeatureContext.Current["Index"] as HtmlDocument;
            //var Title = ScenarioContext.Current["Titile"] as string;
            //var Content = ScenarioContext.Current["Content"] as string;
            Assert.IsNotNull(htmlNewBlog.DocumentNode.SelectNodes("//table[@class='table tableBackground']"), "table not found :(");
        }

    }
}
