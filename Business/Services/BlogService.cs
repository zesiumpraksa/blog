using Business.Interfaces;
using DAL.Interfaces;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

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
            return db.Blogs.ToList();
        }

        public Blog GetFirstId()
        {           
            return db.Blogs.FirstOrDefault();
        }

        public Blog GetById(int id)
        {
            return db.Blogs.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void Save(Blog blog)
        {
            db.Blogs.Add(blog);
            db.SaveChanges();
        }
    }
}
