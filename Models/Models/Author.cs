using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Author
    {
                
        public string Id { get; set; }
        
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual List<Blog> Blogs { get; set; }
    }
}
