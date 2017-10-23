using Model.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAutorService
    {
        List<Author> getAllAuthors();        
        void CreateAuthor(Author author);
        Author GetById(Guid id);
        void InsertPositiveVote(BlogComment commentar, Guid userId);
        bool IsNewPositiveVote(Guid idBlogCommentar, Guid iDVoteAuthor);
        void InsertNegativeVote(BlogComment commentar, Guid userId);
        bool IsNewNegativeVote(Guid idBlogCommentar, Guid iDVoteAuthor);

    }
}
