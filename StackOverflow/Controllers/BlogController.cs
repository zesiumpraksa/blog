using Business.Interfaces;
using Model.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.Models;
using System;
using System.Collections.Generic;

namespace StackOverflow.Controllers
{
    [Authorize]
    public class BlogController: Controller
    {
       
        private IBlogService blogService;
        private IAutorService autorService;

        public BlogController() { }

        public BlogController(IBlogService service, IAutorService autorService)
        {
            this.autorService = autorService;
            blogService = service;
        }
  
        public ActionResult Index()
        {                   
            
            return View(blogService.GetAllBlogs());
        }
        [HttpPost]
        public ActionResult Details(Guid id)
        {           
            return View(blogService.GetById(id));
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(blogService.GetById(new Guid(id)));
        }

        //pokusaj1

        public string RenderPartial(List<BlogComment> comments )
        {
            foreach (BlogComment comm in comments)
            {
                if (comm.ReplayComment.Count == 0)
                {
                    return comm.Commentar;                    
                }
                else
                {
                    RenderPartial(comm.ReplayComment);
                }
            }
            return "";
        }

        [HttpGet]
        public ActionResult Details1(string id)
        {
            Blog blog = blogService.GetById(new Guid(id));
            
            
            return View();
        }



        public ActionResult AuthorDetail(Guid id)
        {
            var author = autorService.GetById(id);
            return View();
        }

        public ActionResult GetByFirstId()
        {
            return View(blogService.GetFirstId());
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

            if (blogService.IsNewAuthor(idAuthor))
            {
                executeQuery = SaveBlogWithNewAuthor(blog,idAuthor,blogAuthor);
            } else
            {
                executeQuery = SaveBlogWithExistingAuthor(blog, idAuthor);
            }

            if(executeQuery==false)
                return RedirectToAction("Error", "Index");
            return RedirectToAction("Index", "Blog");
        }      

        public ActionResult CreateComment(string Id, string commentText)
        {
            var user = User.Identity.GetUserId();
            var blogId = new Guid(Id);             

            //mozda bi trebalo refaktorisati
            var author = autorService.GetById(new Guid(user));
            if (author == null)
            {
                Author authorComment = new Author() { Id = new Guid(User.Identity.GetUserId()), Name = User.Identity.Name  };
                BlogComment blogComment = new BlogComment() {
                    BlogId = blogId,
                    Commentar = commentText,
                    Id = Guid.NewGuid(),
                    AuthorName = authorComment.Name,
                    Date = DateTime.Now,
                    IdAuthor = authorComment.Id,
                    
                };
                blogService.SaveComment(blogComment);
                autorService.CreateAuthor(authorComment);
            }else
            {
                BlogComment blogComment = new BlogComment() {
                    BlogId = blogId,
                    Commentar = commentText,
                    Id = Guid.NewGuid(),
                    AuthorName = User.Identity.Name,
                    Date = DateTime.Now,
                    IdAuthor = new Guid(User.Identity.GetUserId()),
                    
                };
                blogService.SaveComment(blogComment);
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
            var commentar = blogService.getCommentForId(IdCommentar);
            var userId = new Guid(User.Identity.GetUserId());
            var author = autorService.GetById(userId);
                       
            if (author == null)
            {
                Author newAuthor = new Author() { Id = userId, Name = User.Identity.Name };                
                autorService.CreateAuthor(newAuthor);                
                autorService.InsertPositiveVote(commentar, userId);
                //commentar.Raiting++ nece da radi u author service?????????(mozda zato sto je izvucen iz blogServicea!?)
                commentar.Raiting++;                            
            }
            else
            {
                if (!autorService.IsNewPositiveVote(commentar.Id, userId) || (commentar.IdAuthor == userId))
                    return RedirectToAction("Index", "Blog");

                autorService.InsertPositiveVote(commentar, userId);
                commentar.Raiting++;                
            }

            blogService.UpdateBlogComment();
            return RedirectToAction("Index", "Blog");
            //return RedirectToAction("Details","Blog", new { id = IdCommentar });
        }

        public ActionResult VoteDown(Guid IdCommentar)
        {
            var commentar = blogService.getCommentForId(IdCommentar);
            var userId = new Guid(User.Identity.GetUserId());
            var author = autorService.GetById(userId);

            if (author == null)
            {
                Author newAuthor = new Author() { Id = userId, Name = User.Identity.Name };
                autorService.CreateAuthor(newAuthor);
                autorService.InsertNegativeVote(commentar, userId);                
                commentar.Raiting--;
            }else
            {
                if (!autorService.IsNewNegativeVote(commentar.Id, userId) || (commentar.IdAuthor == userId))
                    return RedirectToAction("Index", "Blog");
                autorService.InsertNegativeVote(commentar, userId);
                commentar.Raiting--;
            }

            blogService.UpdateBlogComment();
            return RedirectToAction("Index", "Blog");
        }       

        [HttpPost]
        public ActionResult CreateReplayComment(Guid BlogCommId, string ReplyComment, Guid Id)
        {
            BlogComment ParentCommentar = blogService.getCommentForId(BlogCommId);

            BlogComment replayComment = new BlogComment() {
                Id = Guid.NewGuid(),
                Commentar = ReplyComment,
                AuthorName = User.Identity.Name,
                IdAuthor = new Guid(User.Identity.GetUserId()),
                ParentCommentId = ParentCommentar.Id,
                BlogId = ParentCommentar.BlogId,
                Date = DateTime.Now
            };


            ParentCommentar.ReplayComment.Add(replayComment);

            blogService.SaveComment(replayComment);
           

            return RedirectToAction("Details", new {Id = Id });
        }

        [HttpPost]
        public ActionResult CreateReplayComment1(Guid idReplayComment, string ReplyComment, Guid Id)
        {
            BlogComment ParentCommentar = blogService.getCommentForId(idReplayComment);

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

            blogService.SaveComment(replayComment);


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
            var numberRows = blogService.Save(blog);

            return (numberRows == 2);
        }

        private bool SaveBlogWithExistingAuthor(Blog blog, Guid idAuthor)
        {
            blog.Id = Guid.NewGuid();
            blog.AuthorId = idAuthor;
            blog.Date = DateTime.Now;
            var numberRows = blogService.Save(blog);

            return (numberRows == 1);
        }
       
    }

    
}