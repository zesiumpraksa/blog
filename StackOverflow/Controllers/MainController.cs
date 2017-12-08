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
                return View("UserIndex");
            }
            else
            {
                ViewBag.msg = "Not logged in";                
            }
            return View();
        }      
        
        public ActionResult UserIndex()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Index(User user)
        {            
            if ((user.UserName==null) || (user.Password==null))
            {
                ViewBag.warning1 = "Set username or password";
                return View("Index");
            }

            var signInManager = HttpContext.GetOwinContext().Get<SOSignInManager>();
            var result = signInManager.PasswordSignIn(user.UserName, user.Password, isPersistent: true, shouldLockout: true);
            var userName = HttpContext.User.Identity.Name;

            if(result == SignInStatus.Failure)   
            {
                ViewBag.warning2 = "Bad username or password";
                ModelState.AddModelError("", "Bad login parameters...");
                return View("Index");
            }

            //Response.Cookies.Add(new HttpCookie("test", "test"));
                    
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