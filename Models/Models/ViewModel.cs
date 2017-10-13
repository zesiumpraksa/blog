using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<BlogComment> BlogComments { get; set; }
    }
}
