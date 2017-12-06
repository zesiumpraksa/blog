using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthorWcfService" in both code and config file together.
    [ServiceContract]
    public interface IAuthorWcfService
    {
        [OperationContract]
        void CreateAuthor(Author authorComment);

        [OperationContract]
        Author GetById(Guid userId);

        [OperationContract]
        void InsertPositiveVote(BlogComment commentar, Guid userId);

        [OperationContract]
        void InsertNegativeVote(BlogComment commentar, Guid userId);

        [OperationContract]
        bool IsNewPositiveVote(Guid idBlogCommentar, Guid iDVoteAuthor);

        [OperationContract]
        bool IsNewNegativeVote(Guid idBlogCommentar, Guid iDVoteAuthor);

        [OperationContract]
        List<Author> GetAllAuthors();
    }
}
