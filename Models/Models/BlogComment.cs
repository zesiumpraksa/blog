using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Models
{
    [DataContract]
    public class BlogComment
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [DisplayName("Author of comment")]
        public string AuthorName { get; set; }

        [DataMember]
        [Required]
        public string Commentar { get; set; }

        [DataMember]
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }

        [DataMember]
        public short Raiting { get; set; }

        [DataMember]
        public List<BlogComment> ReplayComment { get; set; } = new List<BlogComment>();

        [DataMember]
        [ForeignKey("ParentComment")]
        public Guid? ParentCommentId { get; set; }

        [DataMember]
        public virtual BlogComment ParentComment { get; set; }

        [DataMember]
        public Guid BlogId { get; set; }
        [DataMember]
        public virtual Blog Blog { get; set; }

        [DataMember]
        public Guid IdAuthor { get; set; }

        [DataMember]
        public virtual Author Author { get; set; }

    }
}
