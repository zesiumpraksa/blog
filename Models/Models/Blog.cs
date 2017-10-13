using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Titile { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public virtual List<BlogComment> BlogComments { get; set; }
    }
}
