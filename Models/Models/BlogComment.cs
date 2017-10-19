using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class BlogComment
    {
        public Guid Id { get; set; }

        [DisplayName("Author of comment")]
        public string AuthorName { get; set; }

        [Required]
        public string Commentar { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }

        public short Raiting { get; set; }
        
        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public Guid IdAuthor { get; set; }
        public virtual Author Author { get; set; }

    }
}
