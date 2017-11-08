using HtmlAgilityPack;
using Models.Models;
using NUnit.Framework;
using StackOverflowTest.Login;
using System;
using System.Collections.Specialized;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StackOverflowTest
{
    [Binding]
    public class BlogComentSteps
    {
        [When(@"Test go to Blog Index page")]
        public void WhenTestGoToBlogIndexPage()
        {
            HtmlDocument blogIndexHtml = new HtmlDocument();
            Uri uri = new Uri("http://localhost:49853/Blog/Index");
            var res = CookieAwareWebClient.Cooke.OpenRead(uri);
            blogIndexHtml.Load(res);
            ScenarioContext.Current["blogIndexHtml"] = blogIndexHtml;
            Assert.NotNull(res);
        }


        [Then(@"Test clicks on blog details")]
        public void WhenTestClicksOnBlogDetails(Table table)
        {
            HtmlDocument htmlDetailsBlog = new HtmlDocument();
            var comment = table.CreateInstance<BlogComment>();
            Uri uri = new Uri("http://localhost:49853/Blog/Details");
            NameValueCollection blogId = new NameValueCollection();
            blogId.Add("id",comment.BlogId.ToString());

            //blogId
            var res = CookieAwareWebClient.Cooke.UploadValues(uri, "POST", blogId);
            Stream streamContent = new MemoryStream(res);
            htmlDetailsBlog.Load(streamContent);
            

            Assert.IsNotNull(htmlDetailsBlog.DocumentNode.SelectNodes("//div[@class='detailsHeader']"), "detailsHeader not found :(");

        }

        [When(@"Test insert new Blog Comment")]
        public void ThenTestInsertNewBlogComment(Table table)
        {
            HtmlDocument htmlAddCommentResponse = new HtmlDocument();            

            var BlogComment = table.CreateInstance<BlogComment>();
            Uri uri = new Uri("http://localhost:49853/Blog/CreateComment");
            NameValueCollection blogCommentContent = new NameValueCollection();
            blogCommentContent.Add("commentText", BlogComment.Commentar);
            blogCommentContent.Add("Id", BlogComment.BlogId.ToString());
            
            var arryValues = CookieAwareWebClient.Cooke.UploadValues(uri,"POST",blogCommentContent);
            Stream streamContent = new MemoryStream(arryValues);

            htmlAddCommentResponse.Load(streamContent);
            Assert.NotNull(arryValues);
            ScenarioContext.Current["HtmlBlogsPost"] = htmlAddCommentResponse;
        }


    }
}
