using Business.Interfaces;
using DAL.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model.Models;
using Models.Models;
using Newtonsoft.Json;
using StackOverflow.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{

    public class UserController : Controller
    {
        WcfService.BlogWcfServiceClient wcfBlogservice = new WcfService.BlogWcfServiceClient();
        WcfService.AuthorWcfServiceClient wcfAuthorService = new WcfService.AuthorWcfServiceClient();

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
                if (image != null)
                {
                    byte[] imageData = null;
                    
                    using (var binary = new BinaryReader(image.InputStream))
                    {
                        imageData = binary.ReadBytes(image.ContentLength);
                    }
                    user.ImageFile = imageData;
                }
                Author author = new Author()
                {
                    Id = new Guid(user.Id),
                    Name = user.FirstName,
                    Password = user.Password,
                    ImageFile = user.ImageFile
                };

                var manager = HttpContext.GetOwinContext().GetUserManager<SOUserManager>();
                var userResault = await manager.CreateAsync(user, user.Password);                

                if (userResault.Succeeded)
                {
                    try
                    {
                        wcfAuthorService.CreateAuthor(author);
                    }
                    catch (Exception e)
                    {
                        var msg = e.Message;
                    }

                    return RedirectToAction("Index", "Main");
                }

                AddErrors(userResault);
            }
            return View();

        }

        public FileContentResult ShowUser()
        {
            var userId = User.Identity.GetUserId();


            Author author = wcfAuthorService.GetById(new Guid(userId));

            if (author.ImageFile == null)
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

            return new FileContentResult(author.ImageFile, "image/jpg");

        }

        public FileContentResult ShowAuthorDetails(Guid blogId)
        {
            string details = wcfBlogservice.GetBlogById(blogId);
            var detailsBlog = JsonConvert.DeserializeObject<Blog>(details);

            Guid authorOfBlogId = detailsBlog.AuthorId;

            List<Author> allAuthors = wcfAuthorService.GetAllAuthors();
            Author author = allAuthors.FirstOrDefault(x => x.Id == authorOfBlogId);

            if (author.ImageFile == null)
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

            return new FileContentResult(author.ImageFile, "image/jpg");

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