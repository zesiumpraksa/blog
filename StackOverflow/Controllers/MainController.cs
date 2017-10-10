using Microsoft.AspNet.Identity.Owin;
using Models.Models;
using StackOverflow.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StackOverflow.Controllers
{
   
    public class MainController : Controller
    {

        // GET: Main
        [HttpGet]
        public ActionResult Index()
        {
            var userName = HttpContext.User.Identity.Name;
            if (!userName.Equals(""))
            {
                ViewBag.user = userName;
            }
            else
            {
                ViewBag.user = "Not logged in";
            }
            return View();
        }
        

        [HttpPost]
        public ActionResult Index(User user)
        {
            //var manager = HttpContext.GetOwinContext().GetUserManager<SOUserManager>();

            var signInManager = HttpContext.GetOwinContext().Get<SOSignInManager>();            
           
            var result = signInManager.PasswordSignIn(user.UserName, user.Password, isPersistent: true, shouldLockout: true);

            var userName = HttpContext.User.Identity.Name;
            if (!userName.Equals(""))
            {
                ViewBag.user = userName;
            }
            if (result == SignInStatus.Success)
            {
                //return View("Index");
               // RedirectToAction("Dashboard", "Author");
            }
            else if(result == SignInStatus.Failure)
            {
                ViewBag.error = "Bad username or password";
                ModelState.AddModelError("", "Bad login parameters...");
                return View("Index");
            }          

            return RedirectToAction("Dashboard", "Author");
        }

        public ActionResult LogOut()
        {
            var AuthManager = HttpContext.GetOwinContext().Authentication;
            AuthManager.SignOut();

            return RedirectToAction("Index", "Main");
        }

        

    }
}