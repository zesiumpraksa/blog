using Business.Interfaces;
using DAL.Interfaces;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Models.Models;

namespace Business.Services
{
    public class BlogService:IBlogService
    {        
        ISOContext db;

        public BlogService() { }

        public BlogService(ISOContext service)
        {
            db = service;
        }

        public List<Blog> GetAllBlogs()
        {
            return db.Blogs.OrderBy(x => x.Titile).ToList();
        }

        public Blog GetFirstId()
        {           
            return db.Blogs.FirstOrDefault();
        }
        
        public Blog GetById(Guid id)
        {            
            return db.Blogs.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Save(Blog blog)
        {
            db.Blogs.Add(blog);
            //db.Blogs.Attach(blog);
            
            var r = db.SaveChanges();
        }

        public bool IsNewAuthor(Guid id)
        {
            bool newAuthor = false;

            var author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
            if (author == null)
                newAuthor = true;           

            return newAuthor;
        }

        public List<Blog> GetAllBlogsOfAuthor(Guid id)
        {
            var a = db.Blogs.Where(x => x.Author.Id == id).ToList();
            return a = db.Blogs.Where(x => x.Author.Id == id).ToList();
        }
    }
}
