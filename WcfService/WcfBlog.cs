using Model.Models;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace WcfService
{
    public partial class WcfService : IBlogWcfService
    {
        public string GetAllBlogs()
        {
            var blogs = blogService.GetAllBlogs();
            return JsonConvert.SerializeObject(blogs, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public Blog GetBlogByIdd(Guid id)
        {
            var blog = blogService.GetById(id);
            return blog;
        }

        public string GetBlogById(Guid id)
        {
            var blog = blogService.GetById(id);
            //return blog;
            return JsonConvert.SerializeObject(blog, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public void SaveComment(BlogComment blogComment)
        {
            blogService.SaveComment(blogComment);
        }

        public List<BlogComment> GetCommentsForBlog(Blog blog)
        {
            return blogService.getCommentsForBlog(blog);
        }

        public string GetCommentForId(Guid id)
        {
            BlogComment comment = blogService.getCommentForId(id);
            string commentString = JsonConvert.SerializeObject(comment, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return commentString;
        }

        public void NegativeVote(Guid CommentId)
        {
            blogService.NegativeVote(CommentId);
        }

        public void PositiveVote(Guid CommentId)
        {
            blogService.PositiveVote(CommentId);
        }

        public bool IsNewAuthor(Guid id)
        {
            return blogService.IsNewAuthor(id);
        }

        public int Save(Blog blog)
        {
            return blogService.Save(blog);
        }

        public string GetAllBlogsOfAuthor(Guid authorId)
        {
            List<Blog>blogsOfAuthor = blogService.GetAllBlogsOfAuthor(authorId);
            string blogsOfAuthorStr = JsonConvert.SerializeObject(blogsOfAuthor, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return blogsOfAuthorStr;
        }

        public string GetAllComments()
        {
            List<BlogComment> comments = blogService.GetAllComments();
            string commentsString = JsonConvert.SerializeObject(comments, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return commentsString;
        }
    }
}
