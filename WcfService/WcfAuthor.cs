using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public partial class WcfService : IAuthorWcfService
    {
        public void CreateAuthor(Author authorComment)
        {
            authorService.CreateAuthor(authorComment);
        }

        public List<Author> GetAllAuthors()
        {
            return authorService.getAllAuthors();
        }

        public Author GetById(Guid userId)
        {
            var z = authorService.GetById(userId);
            return authorService.GetById(userId);
        }

        public void InsertNegativeVote(BlogComment commentar, Guid userId)
        {
            authorService.InsertNegativeVote(commentar, userId);
        }

        public void InsertPositiveVote(BlogComment commentar, Guid userId)
        {
            authorService.InsertPositiveVote(commentar, userId);
        }

        public bool IsNewNegativeVote(Guid idBlogCommentar, Guid iDVoteAuthor)
        {
            return authorService.IsNewNegativeVote(idBlogCommentar, iDVoteAuthor);
        }

        public bool IsNewPositiveVote(Guid idBlogCommentar, Guid iDVoteAuthor)
        {
            return authorService.IsNewPositiveVote(idBlogCommentar, iDVoteAuthor);
        }
    }
}
