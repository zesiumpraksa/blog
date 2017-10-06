using Business.Interfaces;
using Model.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.Models;

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

        public ActionResult Details(int id)
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
        public void CreateBlog(Blog blog)
        {
            string blogAuthor = User.Identity.Name;
            var id = User.Identity.GetUserId();

            blog.Author = new Author
            {
                Name = blogAuthor,
                Id = id
            };
           

            blogService.Save(blog);
        }
    }
}