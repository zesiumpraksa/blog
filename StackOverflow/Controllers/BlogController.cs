using Model.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Models.Models;

namespace StackOverflow.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        WcfService.BlogWcfServiceClient wcfBlogservice = new WcfService.BlogWcfServiceClient();
        WcfService.AuthorWcfServiceClient wcfAuthorService = new WcfService.AuthorWcfServiceClient();


        public BlogController() { }

        [HttpGet]
        public ActionResult Index()
        {
            string asd = wcfBlogservice.GetAllBlogs();
            var a = JsonConvert.DeserializeObject<List<Blog>>(asd);
            return View(a);
        }

        [HttpPost]
        public ActionResult Details(Guid id)
        {
            return View(wcfBlogservice.GetBlogById(id));
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            string details = wcfBlogservice.GetBlogById(new Guid(id));
            var detailsBlog = JsonConvert.DeserializeObject<Blog>(details);
            return View(detailsBlog);

            //return View(blogService.GetById(new Guid(id)));
        }

        public ActionResult AuthorDetail(Guid id)
        {
            var author = wcfAuthorService.GetById(id);
            return View();
        }

        [HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlog(Blog blog)
        {
            string blogAuthor = User.Identity.Name;
            var idAuthor = new Guid(User.Identity.GetUserId());
            bool executeQuery;

            if (wcfBlogservice.IsNewAuthor(idAuthor))
            {
                executeQuery = SaveBlogWithNewAuthor(blog, idAuthor, blogAuthor);
            }
            else
            {
                executeQuery = SaveBlogWithExistingAuthor(blog, idAuthor);
            }

            if (executeQuery == false)
                return RedirectToAction("Error", "Index");
            return RedirectToAction("Index", "Blog");
        }

        public ActionResult CreateComment(string Id, string commentText)
        {
            var user = User.Identity.GetUserId();
            var blogId = new Guid(Id);

            //mozda bi trebalo refaktorisati
            //var author = autorService.GetById(new Guid(user));
            var author = wcfBlogservice.GetBlogById(new Guid(user));
            if (author == null)
            {
                Author authorComment = new Author() { Id = new Guid(User.Identity.GetUserId()), Name = User.Identity.Name };
                BlogComment blogComment = new BlogComment()
                {
                    BlogId = blogId,
                    Commentar = commentText,
                    Id = Guid.NewGuid(),
                    AuthorName = authorComment.Name,
                    Date = DateTime.Now,
                    IdAuthor = authorComment.Id,

                };

                //blogService.SaveComment(blogComment);//****
                wcfBlogservice.SaveComment(blogComment);

                //autorService.CreateAuthor(authorComment);//****
                wcfAuthorService.CreateAuthor(authorComment);
            }
            else
            {
                BlogComment blogComment = new BlogComment()
                {
                    BlogId = blogId,
                    Commentar = commentText,
                    Id = Guid.NewGuid(),
                    AuthorName = User.Identity.Name,
                    Date = DateTime.Now,
                    IdAuthor = new Guid(User.Identity.GetUserId()),

                };
                wcfBlogservice.SaveComment(blogComment);
            }

            return RedirectToAction("Index", "Blog");
        }

        public ActionResult getCommentsForBlog(Blog blog)
        {
            return View();
        }

        public ActionResult AuthorDetails(Author id)
        {
            var ID = id.Id;
            return View();
        }

        public ActionResult VoteUp(Guid IdCommentar, Guid blogId)
        {
            string commentString = wcfBlogservice.GetCommentForId(IdCommentar);
            BlogComment commentar = JsonConvert.DeserializeObject<BlogComment>(commentString);

            var userId = new Guid(User.Identity.GetUserId());

            Author author = wcfAuthorService.GetById(userId);

            if (author == null)
            {
                Author newAuthor = new Author() { Id = userId, Name = User.Identity.Name };
                wcfAuthorService.CreateAuthor(newAuthor);
                wcfAuthorService.InsertPositiveVote(commentar, userId);
            }
            else
            {
                if (!wcfAuthorService.IsNewPositiveVote(commentar.Id, userId) || (commentar.IdAuthor == userId))
                    return RedirectToAction("Index", "Blog");
                wcfAuthorService.InsertPositiveVote(commentar, userId);
            }

            wcfBlogservice.PositiveVote(IdCommentar);

            return RedirectToAction("Index", "Blog");
        }

        public ActionResult VoteDown(Guid IdCommentar)
        {
            string commentString = wcfBlogservice.GetCommentForId(IdCommentar);
            BlogComment commentar = JsonConvert.DeserializeObject<BlogComment>(commentString);


            var userId = new Guid(User.Identity.GetUserId());
            Author author = wcfAuthorService.GetById(userId);

            if (author == null)
            {
                Author newAuthor = new Author() { Id = userId, Name = User.Identity.Name };
                wcfAuthorService.CreateAuthor(newAuthor);
                wcfAuthorService.InsertNegativeVoteAsync(commentar, userId);
            }
            else
            {
                if (!wcfAuthorService.IsNewNegativeVote(commentar.Id, userId) || (commentar.IdAuthor == userId))
                    return RedirectToAction("Index", "Blog");
                wcfAuthorService.InsertNegativeVote(commentar, userId);
            }

            wcfBlogservice.NegativeVote(IdCommentar);

            return RedirectToAction("Index", "Blog");
        }


        [HttpPost]
        public ActionResult CreateReplayComment(Guid BlogCommId, string ReplyComment, Guid Id)
        {
            string commentString = wcfBlogservice.GetCommentForId(BlogCommId);
            BlogComment ParentCommentar = JsonConvert.DeserializeObject<BlogComment>(commentString);

            BlogComment replayComment = new BlogComment()
            {
                Id = Guid.NewGuid(),
                Commentar = ReplyComment,
                AuthorName = User.Identity.Name,
                IdAuthor = new Guid(User.Identity.GetUserId()),
                ParentCommentId = ParentCommentar.Id,
                BlogId = ParentCommentar.BlogId,
                Date = DateTime.Now
            };

            ParentCommentar.ReplayComment.Add(replayComment);
            wcfBlogservice.SaveComment(replayComment);

            return RedirectToAction("Details", new { Id = Id });
        }


        private bool SaveBlogWithNewAuthor(Blog blog, Guid idAuthor, string blogAuthor)
        {

            blog.Id = Guid.NewGuid();
            blog.AuthorId = idAuthor;
            blog.Author = new Author
            {
                Name = blogAuthor,
                Id = idAuthor
            };
            blog.Date = DateTime.Now;
            var numberRows = wcfBlogservice.Save(blog);

            return (numberRows == 2);
        }

        private bool SaveBlogWithExistingAuthor(Blog blog, Guid idAuthor)
        {
            blog.Id = Guid.NewGuid();
            blog.AuthorId = idAuthor;
            blog.Date = DateTime.Now;
            var numberRows = wcfBlogservice.Save(blog);

            return (numberRows == 1);
        }

    }


}