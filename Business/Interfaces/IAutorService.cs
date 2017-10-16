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
        User getAuthor(string user, string pass);
        void CreateAuthor(Author author);
        Author GetById(Guid id);

    }
}
