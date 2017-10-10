using Business.Interfaces;
using Model.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.Models;
using System;

namespace StackOverflow.Controllers
{
    [Authorize]
    public class BlogController: Controller
    {
        private IBlogService blogService;
        
        public BlogController() { }

        public BlogController(IBlogService service)
        {
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
            
            var id = new Guid(User.Identity.GetUserId());
            
            blog.Date = DateTime.Now;


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
    }
}