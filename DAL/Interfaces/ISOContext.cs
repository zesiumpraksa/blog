using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using Models.Models;
using System.Data.Entity;


namespace DAL.Interfaces
{
    public interface ISOContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<BlogComment> BlogComments { get; set; }
        

        int SaveChanges();
    }
}
