using HtmlAgilityPack;
using Models.Models;
using NUnit.Framework;
using StackOverflowTest.Login;
using StackOverflowTest.Steps;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StackOverflowTest
{
    [Binding]
    public class BlogComentSteps
    {
        [When(@"Proba click on Blogs")]
        public void WhenTestGoToBlogIndexPage()
        {
            HtmlDocument htmlBlogIndex = new HtmlDocument();
            Uri uri = new Uri("http://localhost:49853/Blog/Index");

            var res = CookieAwareWebClient.InstanceCookie.OpenRead(uri);

            htmlBlogIndex.Load(res);

            Assert.IsNotNull(res);

            ScenarioContext.Current["htmlBlogIndex"] = htmlBlogIndex;

        }

        [Then(@"Proba is on Blog page")]
        public void ThenProbaIsOnBlogPage()
        {
            var htmlBlogIndex = ScenarioContext.Current["htmlBlogIndex"] as HtmlDocument;
            var tableNode = htmlBlogIndex.DocumentNode.SelectSingleNode("//table[@class='table tableBackground']");
            IEnumerable<HtmlNode> lista = tableNode.ChildNodes.Nodes();

            //potrebna je kvalitetnija provera
            Assert.IsNotNull(htmlBlogIndex.DocumentNode.SelectNodes("//table[@class='table tableBackground']"), "Table is not found:(");

        }

        [When(@"Proba clicks on blog details")]
        public void WhenTestClicksOnBlogDetails(Table table)
        {
            HtmlDocument htmlDetailsBlog = new HtmlDocument();
            var comment = table.CreateInstance<BlogComment>();

            Uri uri = new Uri("http://localhost:49853/Blog/Details");

            NameValueCollection blogId = new NameValueCollection();

            blogId.Add("id", comment.BlogId.ToString());

            //blogId
            var res = CookieAwareWebClient.InstanceCookie.UploadValues(uri, "POST", blogId);
            Stream streamContent = new MemoryStream(res);
            htmlDetailsBlog.Load(streamContent);

            Assert.IsNotNull(res);

            ScenarioContext.Current["blogDetails"] = htmlDetailsBlog;
        }

        [Then(@"Proba is Blog Details page")]
        public void ThenProbaIsBlogDetailsPage()
        {
            var htmlDetailsBlog = ScenarioContext.Current["blogDetails"] as HtmlDocument;
            Assert.NotNull(htmlDetailsBlog);
            //Assert.IsNotNull(htmlDetailsBlog.DocumentNode.SelectNodes("//div[@class='detailsHeader']"), "detailsHeader not found :(");
            //Assert.IsNotNull(htmlDetailsBlog.DocumentNode.SelectNodes("//input[@id='commentText']"), "commentText not found :(");
            //Assert.IsNotNull(htmlDetailsBlog.DocumentNode.SelectNodes("//input[@type='submit']"), "submit not found :(");
        }


        [When(@"Proba insert new Blog Comment")]
        public void ThenTestInsertNewBlogComment(Table table)
        {
            HtmlDocument htmlAddCommentResponse = new HtmlDocument();

            var BlogComment = table.CreateInstance<BlogComment>();

            Uri uri = new Uri("http://localhost:49853/Blog/CreateComment");
            NameValueCollection blogCommentContent = new NameValueCollection();
            blogCommentContent.Add("commentText", BlogComment.Commentar);
            blogCommentContent.Add("Id", BlogComment.BlogId.ToString());

            var arryValues = CookieAwareWebClient.InstanceCookie.UploadValues(uri, "POST", blogCommentContent);
            Stream streamContent = new MemoryStream(arryValues);

            htmlAddCommentResponse.Load(streamContent);
            Assert.NotNull(arryValues);
            FeatureContext.Current["newBlog"] = htmlAddCommentResponse;
        }


    }
}
