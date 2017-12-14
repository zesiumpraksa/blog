using Model.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Business.Interfaces
{
    [ServiceContract]
    public interface IBlogService
    {        
        List<Blog> GetAllBlogs();
        Blog GetFirstId();        
        Blog GetById(Guid id);
        int Save(Blog blog);
        bool IsNewAuthor(Guid id);
        List<Blog> GetAllBlogsOfAuthor(Guid id);
        List<BlogComment> GetAllComments();
        List<BlogComment> getCommentsForBlog(Blog blog);
        void SaveComment(BlogComment blogComment);
        bool IsNewCommAuthor(Guid id);
        BlogComment getCommentForId(Guid Id);
        BlogComment GetCommentForId(Guid commId);
        void NegativeVote(Guid CommentId);
        void PositiveVote(Guid CommentId);
       
    }
}
