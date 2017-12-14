using Model.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBlogWcfService" in both code and config file together.
    
    [ServiceContract]
    public interface IBlogWcfService
    {
        [OperationContract]
        string GetAllBlogs();

        [OperationContract]
        string GetBlogById(Guid id);

        [OperationContract]
        void SaveComment(BlogComment blogComment);

        [OperationContract]
        List<BlogComment> GetCommentsForBlog(Blog blog);

        [OperationContract]
        string GetCommentForId(Guid id);

        [OperationContract]
        void NegativeVote(Guid CommentId);

        [OperationContract]
        void PositiveVote(Guid CommentId);

        [OperationContract]
        bool IsNewAuthor(Guid id);

        [OperationContract]
        int Save(Blog blog);

        [OperationContract]
        Blog GetBlogByIdd(Guid id);

        [OperationContract]
        string GetAllBlogsOfAuthor(Guid authorId);

        [OperationContract]
        string GetAllComments();

    }
}
