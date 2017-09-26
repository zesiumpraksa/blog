using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{
    

    public class BlogController: Controller
    {
        private List<Blog> blogs;

        public BlogController()
        {
            blogs = new List<Blog>()
            {

                new Blog {Author="Pera",Content="Null reference",Id=1, Titile="C# problem null reference",Date= DateTime.Now },
                new Blog {Author="Mika",Content="Convert to String",Id=1, Titile="Problem in project",Date= DateTime.Now },
                new Blog {Author="Laza",Content="JS problem",Id=1, Titile="Java Script problem in project",Date= DateTime.Now }
            };
        }

        public ActionResult Index()
        {
            return View(blogs);
        }

        public ActionResult Details(int id)
        {            
            return View(blogs.Where(x => x.Id == id).First());
        }
    }
}