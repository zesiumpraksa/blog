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
        [When(@"User Test click Create new blog")]
        public void WhenUserTestClickCreateNewBlog()
        {
            HtmlDocument htmlGlobal = new HtmlDocument();
            Uri uri = new Uri("http://localhost:49853/Blog/CreateBlog");
            NameValueCollection nameValue = new NameValueCollection();           
            var res = CookieAwareWebClient.Cooke.OpenRead(uri);
            htmlGlobal.Load(res);
            
            ScenarioContext.Current["HtmlGet"] = htmlGlobal;
            ScenarioContext.Current["uri"] = uri;          

            Assert.NotNull(res, "values are empty");
            
        }
        [Then(@"User is on CreateNewBlogPage")]
        public void ThenUserIsOnCreateNewBlogPage()
        {
            
            var htmlGlobal = ScenarioContext.Current["HtmlGet"] as HtmlDocument ;
            var uri = ScenarioContext.Current["uri"] as Uri;
           // var res = CookieAwareWebClient.Cooke.OpenRead(uri);
            
            Assert.IsNotNull(htmlGlobal.DocumentNode.SelectNodes("//input[@id='Titile']"), "Ttile not found :(");
            Assert.IsNotNull(htmlGlobal.DocumentNode.SelectNodes("//label [@for='Content']"), "Content not found :(");
            Assert.IsNotNull(htmlGlobal.DocumentNode.SelectNodes("//input[@value='Create']"), "Submit create not found :(");
        }

        [When(@"Test insert values for new blogs and press Create")]
        public void WhenUsernameInsertValuesForNewBlogs(Table table)
        {
            var htmlGlobal = ScenarioContext.Current["HtmlGet"] as HtmlDocument;
            var uri = ScenarioContext.Current["uri"] as Uri;
            Blog blog = table.CreateInstance<Blog>();
            NameValueCollection newBlogValues = new NameValueCollection();

            newBlogValues.Add("Titile", blog.Titile);
            newBlogValues.Add("Content", blog.Content);

            var values = CookieAwareWebClient.Cooke.UploadValues(uri, "POST", newBlogValues);
            Stream streamNewBlogContent = new MemoryStream(values);
            htmlGlobal.Load(streamNewBlogContent);
            ScenarioContext.Current["HtmlBlogsPost"] = htmlGlobal;
            Assert.NotNull(values, "values are null");          
        }

        [Then(@"User is on Blog Index page")]
        public void ThenUserIsOnBlogIndexPageWithNewBlogInformations()
        {
            var htmlBlogs = ScenarioContext.Current["HtmlBlogsPost"] as HtmlDocument;          
            
            //var Title = ScenarioContext.Current["Titile"] as string;
            //var Content = ScenarioContext.Current["Content"] as string;

            //HtmlNodeCollection table = htmlBlogs.DocumentNode.SelectNodes("//table[@class='table']");            
            Assert.IsNotNull(htmlBlogs.DocumentNode.SelectNodes("//table[@class='table']"), "Ttile not found :(");
        }

    }
}
