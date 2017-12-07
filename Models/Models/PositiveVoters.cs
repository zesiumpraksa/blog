using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [DataContract]
    public class PositiveVoters
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid IdNumberOfComment { get; set; }
        [DataMember]
        public string AuthorOfComment { get; set; }

        [DataMember]
        public Guid AuthorId { get; set; }
        [DataMember]
        public virtual Author Author { get; set; }

    }
}
