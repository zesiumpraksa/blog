using Business.Interfaces;
using DAL.DBContext;
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

        public FileContentResult ShowUser()
        {
            var userId = User.Identity.GetUserId();
            var bdUsers = HttpContext.GetOwinContext().Get<SOContext>();
            var user = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (user.ImageFile == null)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/coder.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageLength = fileInfo.Length;

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageLength);

                return File(imageData, "image/png");
            }

            return new FileContentResult(user.ImageFile, "image/jpg");

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