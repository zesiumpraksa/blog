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

        public BlogController(IBlogService service, IAutorService autoService)
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
            List<BlogComment>comments = blogService.getCommentsForBlog(blogService.GetById(id));
            return View(blogService.GetById(id));
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
            //slucajno radi dobro, proveriti!!!
            var a = User.Identity.GetUserId();
            var id = new Guid(User.Identity.GetUserId());
            
            blog.Date = DateTime.Now;
            var x = a.GetType();

            if (blogService.IsNewAuthor(id))
            {
                blog.Id = Guid.NewGuid();
                blog.AuthorId = id;
                blog.Author = new Author
                {
                    Name = blogAuthor,
                    Id = id
                };
            }else
            {
                blog.Id = Guid.NewGuid();
                blog.AuthorId = id;
            }
            blogService.Save(blog);

            return RedirectToAction("Index","Blog");
        }


        public ActionResult CreateComment(string Id, string commentText)
        {
            var blogId = new Guid(Id);
            
            BlogComment bc = new BlogComment() { BlogId = blogId, Commentar = commentText, Id = Guid.NewGuid()};

            blogService.SaveComment(bc);

            return RedirectToAction("Index", "Blog");
        }

        public ActionResult getCommentsForBlog(Blog blog)
        {
            return View();
        }
    }

    
}