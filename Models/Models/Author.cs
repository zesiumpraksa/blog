using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [DataContract]
    public class Author
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public byte[] ImageFile { get; set; }

        [DataMember]
        public List<Blog> Blogs { get; set; }
        [DataMember]
        public  List<BlogComment> BlogComments { get; set; }
        [DataMember]
        public  List<PositiveVoters> PositiveVoters { get; set; }
        [DataMember]
        public  List<NegativeVoters> NegativeVoters { get; set; }
    }
}
