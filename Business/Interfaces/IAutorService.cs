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
        void InsertPositiveVote(PositiveVoters positive);
        bool IsNewPositiveVote(Guid idBlogCommentar);
        void InsertNegativeVote(NegativeVoters negative);
        bool IsNewNegativeVote(Guid idBlogCommentar);

    }
}
