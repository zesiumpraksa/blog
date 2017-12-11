using Business.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models.Models;
using StackOverflow.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{
    
    public class UserController : Controller
    {
       

        public UserController() { }             

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user, HttpPostedFileBase image)
        {

            
            if (ModelState.IsValid)
            {
                byte[] imageData = null; 
                using(var binary = new BinaryReader(image.InputStream))
                {
                    imageData = binary.ReadBytes(image.ContentLength);
                }


                user.ImageFile = imageData;

                var manager = HttpContext.GetOwinContext().GetUserManager<SOUserManager>();
                var userResault = await manager.CreateAsync(user, user.Password);
                
                if (userResault.Succeeded)
                {
                    return RedirectToAction("Index", "Main");
                }

                AddErrors(userResault);
            }
            return View();

        }

        public ActionResult Show()
        {
            return()
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