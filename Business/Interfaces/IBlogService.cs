using Model.Models;
using System.Collections.Generic;

namespace Business.Interfaces
{

    public interface IBlogService
    {
        List<Blog> GetAllBlogs();
        Blog GetFirstId();
        Blog GetById(int id);
        void Save(Blog blog);
    }
}
