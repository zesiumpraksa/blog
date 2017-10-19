using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class PositiveVoters
    {
        public Guid Id { get; set; }
        public Guid number { get; set; }     
        
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        
    }
}
