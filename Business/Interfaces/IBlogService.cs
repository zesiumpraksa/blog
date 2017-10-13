using Model.Models;
using Models.Models;
using System;
using System.Collections.Generic;

namespace Business.Interfaces
{

    public interface IBlogService
    {
        List<Blog> GetAllBlogs();
        Blog GetFirstId();
        Blog GetById(Guid id);
        void Save(Blog blog);
        bool IsNewAuthor(Guid id);
        List<Blog> GetAllBlogsOfAuthor(Guid id);
        List<BlogComment> GetAllComments();
        List<BlogComment> getCommentsForBlog(Blog blog);
        void SaveComment(BlogComment blogComment);
        bool IsNewCommAuthor(Guid id);
    }
}
