using DAL.DBContext;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Models.Models;

namespace DAL.Initializer
{
    class SOInitializer:DropCreateDatabaseIfModelChanges<SOContext>
    {
        protected override void Seed(SOContext context)
        {
            Author A1 = new Author() { Name = "Pera", Id = Guid.NewGuid(), Password = "A1" };
            Author A2 = new Author() { Name = "Mika", Id = Guid.NewGuid(), Password = "A2" };
            Author A3 = new Author() { Name = "Laza", Id = Guid.NewGuid(), Password = "A3" };

            var authors = new List<Author>();

            authors.Add(A1);
            authors.Add(A2);
            authors.Add(A3);
          

            Blog B1 = new Blog() { Content = "Null reference", Id = Guid.NewGuid(), Titile = "C# problem null reference1", Date = DateTime.Now, AuthorId = A1.Id, };
            Blog B2 = new Blog() { Content = "Convert to String", Id = Guid.NewGuid(), Titile = "Problem in project2", Date = DateTime.Now, AuthorId = A1.Id };
            Blog B3 = new Blog() { Content = "JS problem", Id = Guid.NewGuid(), Titile = "Java Script problem in project3", Date = DateTime.Now, AuthorId = A1.Id };
            Blog B4 = new Blog() { Content = "Null reference", Id = Guid.NewGuid(), Titile = "C# problem null reference4", Date = DateTime.Now, AuthorId = A3.Id };
            Blog B5 = new Blog() { Content = "Convert to String", Id = Guid.NewGuid(), Titile = "Problem in project5", Date = DateTime.Now, AuthorId = A3.Id };
            Blog B6 = new Blog() { Content = "JS problem", Id = Guid.NewGuid(), Titile = "Java Script problem in project6", Date = DateTime.Now, AuthorId = A3.Id };
            Blog B7 = new Blog() { Content = "Null reference", Id = Guid.NewGuid(), Titile = "C# problem null reference1", Date = DateTime.Now, AuthorId = A2.Id };


            var blogs = new List<Blog>();
            blogs.Add(B1); blogs.Add(B2); blogs.Add(B3); blogs.Add(B4); blogs.Add(B5); blogs.Add(B6); blogs.Add(B7);


            BlogComment BC1 = new BlogComment() { Id = Guid.NewGuid(), BlogId = B1.Id, Commentar = "Commentar1" , IdAuthor=A1.Id ,AuthorName=A1.Name, Date = DateTime.Now};
            BlogComment BC2 = new BlogComment() { Id = Guid.NewGuid(), BlogId = B2.Id, Commentar = "Commentar2", IdAuthor = A2.Id,AuthorName=A2.Name, Date = DateTime.Now };
            BlogComment BC3 = new BlogComment() { Id = Guid.NewGuid(), BlogId = B3.Id, Commentar = "Commentar3", IdAuthor = A3.Id , AuthorName=A3.Name, Date = DateTime.Now };
            BlogComment BC4 = new BlogComment() { Id = Guid.NewGuid(), BlogId = B3.Id, Commentar = "Commentar4", IdAuthor = A1.Id , AuthorName=A1.Name, Date = DateTime.Now };

            var commentars = new List<BlogComment>();
            commentars.Add(BC1);   commentars.Add(BC2);  commentars.Add(BC3);     commentars.Add(BC4);
            

            context.Blogs.AddRange(blogs);
            context.Authors.AddRange(authors);
            context.BlogComments.AddRange(commentars);
            
            base.Seed(context);
            
        }
    }
}
