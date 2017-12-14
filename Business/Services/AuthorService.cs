using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Interfaces;
using Model.Models;

namespace Business.Services
{
    public class AuthorService : IAutorService
    {
        ISOContext db;

        public AuthorService(ISOContext service)
        {
            db = service;
        }

        public void CreateAuthor(Author author)
        {
            db.Authors.Add(author);
            var x = db.SaveChanges();
        }

        public List<Author> getAllAuthors()
        {
            return db.Authors.ToList();
        }      

        public Author GetById(Guid id)
        {   
            var z = db.Authors.FirstOrDefault(x => x.Id == id); 
            return db.Authors.FirstOrDefault(x=>x.Id == id);
        }

       
        public bool IsNewPositiveVote(Guid idBlogCommentar, Guid iDVoteAuthor)
        {
            var positiveVote = db.PositiveVoters.FirstOrDefault(x => x.IdNumberOfComment == idBlogCommentar && x.AuthorId==iDVoteAuthor);
            return (positiveVote==null);
        }

        public bool IsNewNegativeVote(Guid idBlogCommentar, Guid iDVoteAuthor)
        {
            var negativeVote = db.NegativeVoters.FirstOrDefault(x => x.IdNumberOfComment == idBlogCommentar && x.AuthorId == iDVoteAuthor);
            return (negativeVote == null);
        }

        public void InsertPositiveVote(BlogComment commentar, Guid userId)
        {
            PositiveVoters positive = new PositiveVoters() { Id = Guid.NewGuid(), AuthorId = userId, IdNumberOfComment = commentar.Id, AuthorOfComment = commentar.AuthorName };
            db.PositiveVoters.Add(positive);            
            db.SaveChanges();            
        }

        public void InsertNegativeVote(BlogComment commentar, Guid userId)
        {
            NegativeVoters negative = new NegativeVoters() { Id = Guid.NewGuid(), AuthorId = userId, IdNumberOfComment = commentar.Id, AuthorOfComment = commentar.AuthorName };
            db.NegativeVoters.Add(negative);
            db.SaveChanges();
        }
    }
}
