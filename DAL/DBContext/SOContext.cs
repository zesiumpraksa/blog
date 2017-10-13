﻿using DAL.Initializer;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using Models.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace DAL.DBContext
{
    public class SOContext: IdentityDbContext<User>, ISOContext
    {
        public SOContext() : base()
        {
            Database.SetInitializer(new SOInitializer());
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        public override int SaveChanges()
        {            
             return base.SaveChanges();                      
        }
    }
}
