using Business.Interfaces;
using Business.Services;
using Model.Models;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestService" in both code and config file together.
    public partial class WcfService : IWcfService
    {
        IBlogService blogService;
        IAutorService authorService;

        public WcfService() { }

        public WcfService(IBlogService blogService,IAutorService authorService)
        {
            this.blogService = blogService;
            this.authorService = authorService;
        }
        

        public void DoWork()
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// ////////////////////////////////
    /// </summary>
    public partial class WcfService: IBlogWcfService
    {
        public List<Blog> GetAllBlogs()
        {
            return (blogService.GetAllBlogs());
        }

        public string GetBlogById(Guid id)
        {
            var blog = blogService.GetById(id);
            return JsonConvert.SerializeObject(blog, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public void SaveComment(BlogComment blogComment)
        {
            blogService.SaveComment(blogComment);
        }

        public List<BlogComment> getCommentsForBlog(Blog blog)
        {
            return blogService.getCommentsForBlog(blog);
        }
    }

    /// <summary>
    /// //////////////////////////////////
    /// </summary>

    public partial class WcfService : IAuthorWcfService
    {
        public void CreateAuthor(Author authorComment)
        {
            authorService.CreateAuthor(authorComment);
        }
    }
}