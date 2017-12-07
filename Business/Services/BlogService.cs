using Business.Interfaces;
using DAL.Interfaces;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Models.Models;
using System.Data.Entity;

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
            //var blogs = db.Blogs
            //     .Include(x => x.Author)
            //     .OrderBy(x => x.Titile)
            //     .ToList();
            //return blogs;
            return db.Blogs.OrderBy(x => x.Titile).ToList();
        }

        public Blog GetById(Guid id)
        {
            var a = db.Blogs
                .Include(x => x.Author)   
                .Include(x=>x.BlogComments)           
                .FirstOrDefault(x => x.Id == id);
            return a;
            //return db.Blogs.FirstOrDefault(x => x.Id == id);
        }

        public Blog GetFirstId()
        {
            return db.Blogs.Include(x => x.Author).FirstOrDefault();
            //return db.Blogs.FirstOrDefault();
        }
        
        

        public int Save(Blog blog)
        {
            db.Blogs.Add(blog);
            return db.SaveChanges();
        }

        public bool IsNewAuthor(Guid id)
        {
            var author = db.Authors.FirstOrDefault(a => a.Id == id);
           
            return (author == null);
        }

        public bool IsNewCommAuthor(Guid id)
        {           
            var CommAuthor = db.BlogComments.Where(a => a.Id == id).ToList();          
                
            return (CommAuthor==null);
        }

        public List<Blog> GetAllBlogsOfAuthor(Guid id)
        {
            var allBlogs = db.Blogs.Where(x => x.Author.Id == id).ToList();

            return allBlogs;
        }

        public List<BlogComment> GetAllComments()
        {
            return db.BlogComments.ToList();
        }

        public List<BlogComment>getCommentsForBlog(Blog blog)
        {
            var comments = db.BlogComments.Where(x => x.Blog.Id == blog.Id).ToList();

            return comments;            
        }

        public void SaveComment(BlogComment blogComment)
        {

            //var a = db.Blogs
            //  .Include(x => x.Author)
            //  .Include(x => x.BlogComments)
            //  .FirstOrDefault(x => x.Id == id);


            db.BlogComments.Add(blogComment);
            db.SaveChanges();
        }

        public BlogComment getCommentForId(Guid Id)
        {
            return db.BlogComments.FirstOrDefault(x => x.Id == Id);
        }

        public BlogComment GetCommentForId(Guid commId)
        {
            return db.BlogComments.FirstOrDefault(a => a.Id == commId);
        }

        public void NegativeVote(Guid CommentId)
        {
            BlogComment commentar = GetCommentForId(CommentId);
            commentar.Raiting--;
            var x = db.SaveChanges();
        }

        public void PositiveVote(Guid CommentId)
        {
            BlogComment commentar = GetCommentForId(CommentId);
            commentar.Raiting++;
            var x = db.SaveChanges();
        }

        
    }
}
