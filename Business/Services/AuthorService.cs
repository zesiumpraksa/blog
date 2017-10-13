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
        }

        public List<Author> getAllAuthors()
        {
            return db.Authors.ToList();
        }

        public User getAuthor(string user, string pass)
        {
            //User bloger = db.Authors.Where(x => x. == user && x.Password == pass).FirstOrDefault();
            if (user == "test" && pass == "test")
            {
                return new User() { FirstName = "aaaaaa" };
            }else
            {
                return null;
            }
        }

        public Author GetById(Guid id)
        {   
            return db.Authors.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
