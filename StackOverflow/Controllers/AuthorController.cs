using Business.Interfaces;
using DAL.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model.Models;
using Models.Models;
using StackOverflow.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{
    
    public class AuthorController : Controller
    {
        IAutorService autorService;
        IBlogService blogService;

        public AuthorController() { }

        public AuthorController(IAutorService service, IBlogService blgService)
        {            
            autorService = service;
            blogService = blgService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View(autorService.getAllAuthors());
        }
               
        public ActionResult LogIn()
        {
            return View("LogIn");
        }
       
        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.name = User.Identity.Name;
            string user = User.Identity.GetUserId();
            Guid g = new Guid(user);
           
            return View(blogService.GetAllBlogsOfAuthor(g));
        }

        public ActionResult Details(Guid id)
        {
            return View(autorService.GetById(id));
        }

        public ActionResult AuthorCommentDetails(string id)
        {   
            return View(autorService.GetById(new Guid(id)));
        }
        
        
    }
}