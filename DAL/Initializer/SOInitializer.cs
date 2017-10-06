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
            var blogs = new List<Blog>()
            {
                new Blog {Content="Null reference",Id="1", Titile="C# problem null reference1",Date= DateTime.Now, AuthorId ="1"},
                new Blog {Content="Convert to String",Id="2", Titile="Problem in project2",Date= DateTime.Now, AuthorId ="2" },
                new Blog {Content="JS problem",Id="3", Titile="Java Script problem in project3",Date= DateTime.Now, AuthorId ="3" },
                new Blog {Content="Null reference",Id="4", Titile="C# problem null reference4",Date= DateTime.Now , AuthorId ="4"},
                new Blog {Content="Convert to String",Id="5", Titile="Problem in project5",Date= DateTime.Now, AuthorId ="5" },
                new Blog {Content="JS problem",Id="6", Titile="Java Script problem in project6",Date= DateTime.Now, AuthorId ="1" },
                new Blog {Content="Null reference",Id="7", Titile="C# problem null reference1",Date= DateTime.Now, AuthorId ="2" },
                new Blog {Content="Convert to String",Id="8", Titile="Problem in project2",Date= DateTime.Now, AuthorId ="3" },
                new Blog {Content="JS problem",Id="9", Titile="Java Script problem in project3",Date= DateTime.Now, AuthorId ="4" },
                new Blog {Content="Null reference",Id="10", Titile="C# problem null reference4",Date= DateTime.Now, AuthorId ="5" },
                new Blog {Content="Convert to String",Id="11", Titile="Problem in project5",Date= DateTime.Now, AuthorId ="1" },
                new Blog {Content="JS problem",Id="12", Titile="Java Script problem in project6",Date= DateTime.Now, AuthorId ="2" }                
            };

            var authors = new List<Author>()
            {
                new Author {Name="Pera",Id="1",Password="A1" },
                new Author {Name="Mika",Id="2",Password="A2" },
                new Author {Name="Laza",Id="3",Password="A3" },
                new Author {Name="Maja",Id="4",Password="A4" },
                new Author {Name="Jelena",Id="5",Password="A5" }
            };
            
            
            context.Blogs.AddRange(blogs);
            context.Authors.AddRange(authors);
            
            base.Seed(context);
            
        }
    }
}
