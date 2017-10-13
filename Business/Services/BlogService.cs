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

        public bool IsNewCommAuthor(Guid id)
        {
            bool newCommAuthor = false;
            var CommAuthor = db.BlogComments.Where(a => a.Id == id).ToList();
            if (CommAuthor == null)
                newCommAuthor = true;
            return newCommAuthor;
        }
        public List<Blog> GetAllBlogsOfAuthor(Guid id)
        {
            var a = db.Blogs.Where(x => x.Author.Id == id).ToList();
            return a = db.Blogs.Where(x => x.Author.Id == id).ToList();
        }

        public List<BlogComment> GetAllComments()
        {
            return db.BlogComments.ToList();
        }

        public  List<BlogComment>getCommentsForBlog(Blog blog)
        {
            var a = db.BlogComments.Where(x => x.Blog.Id == x.BlogId).ToList();
            return db.BlogComments.Where(x => x.Blog.Id == x.BlogId).ToList();
            
        }

        public void SaveComment(BlogComment blogComment)
        {
             db.BlogComments.Add(blogComment);
             db.SaveChanges();
        }
    }
}
