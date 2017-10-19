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
            return db.Authors.FirstOrDefault(x=>x.Id == id);
        }

        public bool IsNewPositiveVote(Guid idBlogCommentar)
        {
            var positiveVote = db.PositiveVoters.FirstOrDefault(x => x.number == idBlogCommentar);
            return (positiveVote==null);
        }

        public bool IsNewNegativeVote(Guid idBlogCommentar)
        {
            var negativeVote = db.PositiveVoters.FirstOrDefault(x => x.number == idBlogCommentar);
            return (negativeVote == null);
        }

        public void InsertPositiveVote(PositiveVoters positive)
        {
            db.PositiveVoters.Add(positive);
            var a = db.SaveChanges();
        }

        public void InsertNegativeVote(NegativeVoters negative)
        {
            db.NegativeVoters.Add(negative);
            var a = db.SaveChanges();
        }
    }
}
