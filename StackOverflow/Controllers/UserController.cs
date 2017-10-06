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
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController()
        {

        }

        public UserController(IUserService service)
        {
            userService = service;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<SOUserManager>();
                //var user = new User() { UserName = author.Name, Id = author.Id, Password = author.Password };
                //author = new Author() { Name = author.Name, Password = author.Password };              


                var m = await manager.CreateAsync(user, user.Password);
                              
                if (m.Succeeded)
                {
                    //userService.addUser(user);                    
                    return RedirectToAction("Index", "Main");
                }

                AddErrors(m);
            }
            return View();

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        

    }
}