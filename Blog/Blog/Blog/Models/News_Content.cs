using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class News_Content
    {
        public int ID { get; set; }

        public int NewsID { get; set; }

        public string N_Content { get; set; }

        public string attributes { get; set; }

        public int weight { get; set; }

        public bool State { get; set; }

        public bool Headings { get; set; }
    }
}