using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Titile { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}