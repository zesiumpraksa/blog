using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class BlogComment
    {
        public Guid Id { get; set; }
        public Author Author { get; set; }
        public string Commentar { get; set; }

        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }

    }
}
