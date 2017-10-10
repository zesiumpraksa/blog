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


            var blogs = new List<Blog>()
            {
                new Blog {Content="Null reference",Id=Guid.NewGuid(), Titile="C# problem null reference1",Date= DateTime.Now, AuthorId = A1.Id },
                new Blog {Content="Convert to String",Id=Guid.NewGuid(), Titile="Problem in project2",Date= DateTime.Now, AuthorId =A1.Id },
                new Blog {Content="JS problem",Id=Guid.NewGuid(), Titile="Java Script problem in project3",Date= DateTime.Now, AuthorId =A1.Id },
                new Blog {Content="Null reference",Id=Guid.NewGuid(), Titile="C# problem null reference4",Date= DateTime.Now , AuthorId =A3.Id},
                new Blog {Content="Convert to String",Id=Guid.NewGuid(), Titile="Problem in project5",Date= DateTime.Now, AuthorId =A3.Id },
                new Blog {Content="JS problem",Id=Guid.NewGuid(), Titile="Java Script problem in project6",Date= DateTime.Now, AuthorId =A3.Id },
                new Blog {Content="Null reference",Id=Guid.NewGuid(), Titile="C# problem null reference1",Date= DateTime.Now, AuthorId =A2.Id },
                new Blog {Content="Convert to String",Id=Guid.NewGuid(), Titile="Problem in project2",Date= DateTime.Now, AuthorId =A2.Id },
                new Blog {Content="JS problem",Id=Guid.NewGuid(), Titile="Java Script problem in project3",Date= DateTime.Now, AuthorId =A1.Id },
                new Blog {Content="Null reference",Id=Guid.NewGuid(), Titile="C# problem null reference4",Date= DateTime.Now, AuthorId =A2.Id },
                new Blog {Content="Convert to String",Id=Guid.NewGuid(), Titile="Problem in project5",Date= DateTime.Now, AuthorId =A3.Id },
                new Blog {Content="JS problem",Id=Guid.NewGuid(), Titile="Java Script problem in project6",Date= DateTime.Now, AuthorId =A2.Id }                
            };

            //var authors = new List<Author>()
            //{
            //    new Author {Name="Pera",Id=new Guid(),Password="A1" },
            //    new Author {Name="Mika",Id=new Guid(),Password="A2" },
            //    new Author {Name="Laza",Id=new Guid(),Password="A3" },
            //    new Author {Name="Maja",Id=new Guid(),Password="A4" },
            //    new Author {Name="Jelena",Id=new Guid(),Password="A5" }
            //};      
            
            context.Blogs.AddRange(blogs);
            context.Authors.AddRange(authors);
            
            base.Seed(context);
            
        }
    }
}
