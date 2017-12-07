using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Model.Models
{
    [DataContract]
    public class Blog
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Titile { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public Guid AuthorId { get; set; }

        [DataMember]
        public virtual Author Author { get; set; }

        [DataMember]
        public  List<BlogComment> BlogComments { get; set; }
    }
}
