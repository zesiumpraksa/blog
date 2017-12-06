using Model.Models;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public partial class WcfService : IBlogWcfService
    {
        public List<Blog> GetAllBlogs()
        {
            return (blogService.GetAllBlogs());
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

        public BlogComment GetCommentForId(Guid id)
        {
            return blogService.getCommentForId(id);
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

        public List<Blog> GetAllBlogsOfAuthor(Guid authorId)
        {
            return blogService.GetAllBlogsOfAuthor(authorId);
        }
    }
}
