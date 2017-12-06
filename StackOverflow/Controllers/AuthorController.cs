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
        WcfService.BlogWcfServiceClient wcfBlogservice = new WcfService.BlogWcfServiceClient();
        WcfService.AuthorWcfServiceClient wcfAuthorService = new WcfService.AuthorWcfServiceClient();        

        public AuthorController() { }

       
        // GET: User
        public ActionResult Index()
        {
            //return View(autorService.getAllAuthors());
            return View(wcfAuthorService.GetAllAuthors());
        }
               
        public ActionResult LogIn()
        {
            return View("LogIn");
        }
       
        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.name = User.Identity.Name;
            Guid userId = new Guid(User.Identity.GetUserId());
            
            return View(wcfBlogservice.GetAllBlogsOfAuthor(userId));
            
        }

        public ActionResult Details(Guid id)
        {
            return View(wcfAuthorService.GetById(id));
        }

        public ActionResult AuthorCommentDetails(string id)
        {   
            return View(wcfAuthorService.GetById(new Guid(id)));
        }
        
        
    }
}