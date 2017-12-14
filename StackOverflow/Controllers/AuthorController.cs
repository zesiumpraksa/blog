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

            int numOfReplay;
            ViewBag.name = User.Identity.Name;

            Guid userId = new Guid(User.Identity.GetUserId());            

            string blogsString = wcfBlogservice.GetAllBlogsOfAuthor(userId);
            List<Blog> blogs = JsonConvert.DeserializeObject<List<Blog>>(blogsString);


            string commentsString = wcfBlogservice.GetAllComments();
            List<BlogComment> comments = JsonConvert.DeserializeObject<List<BlogComment>>(commentsString);
            

            numOfReplay = comments.Count(x => x.IdAuthor == userId);
            ViewBag.numOfReplays = numOfReplay;
            //List<BlogComment> comments = JsonConvert.DeserializeObject<List<BlogComment>>(allBlogsString);
            return View(blogs);
            
        }

        //public int NumOfReplayCommments(List<BlogComment>comments, Guid userId)
        //{
        //    int num = 0;
        //    foreach(BlogComment replay in comments)
        //    {
        //        num = replay.ReplayComment.Count(x => x.Id == userId);
        //        BlogComment replau
        //    }
        //    return num;
        //}

        public ActionResult Details(Guid id)
        {
            var z = wcfAuthorService.GetById(id);
            return View(wcfAuthorService.GetById(id));
        }

        public ActionResult AuthorCommentDetails(string id)
        {   
            return View(wcfAuthorService.GetById(new Guid(id)));
        }
        
        
    }
}