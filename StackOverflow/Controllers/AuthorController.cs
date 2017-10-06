using Business.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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

        public AuthorController() { }

        public AuthorController(IAutorService service)
        {
            
            autorService = service;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


       
        public ActionResult LogIn()
        {
            return View("LogIn");
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            var res = autorService.getAuthor(user.UserName, user.Password);
            if (res != null)
            {
                return RedirectToAction("Index","Blog");
            }else
            {
                return View("Index");
            }
            
        }


    }
}