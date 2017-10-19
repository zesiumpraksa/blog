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
        
        public ActionResult Details(Guid id)
        {
           
            return View(blogService.GetById(id));
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

        public ActionResult Votes(Guid IdCommentar, string operation)
        {
            var commentar = blogService.getCommentForId(IdCommentar);
            var userId = new Guid(User.Identity.GetUserId());
            var author = autorService.GetById(userId);
            if (author != null)
            {
                if (operation.Equals("+"))
                {
                    if (autorService.IsNewPositiveVote(commentar.Id) && (commentar.IdAuthor!=userId))
                    {
                        PositiveVoters vote = new PositiveVoters() { number = commentar.Id, AuthorId = userId, Id = Guid.NewGuid() };
                        autorService.InsertPositiveVote(vote);
                        commentar.Raiting++;
                    }
                    else
                    {
                        return RedirectToAction("Index", "Blog");
                    }
                }
                else
                {
                    if (autorService.IsNewNegativeVote(commentar.Id) && (commentar.IdAuthor != userId))
                    {
                        NegativeVoters vote = new NegativeVoters() { Number = commentar.Id, AuthorId = userId, Id = Guid.NewGuid() };
                        autorService.InsertNegativeVote(vote);
                        commentar.Raiting--;
                    }
                    else
                    {
                        return RedirectToAction("Index", "Blog");
                    }
                }

                blogService.UpdateBlogComment();
                return RedirectToAction("Index", "Blog");
            }
            return RedirectToAction("Index", "Blog");


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