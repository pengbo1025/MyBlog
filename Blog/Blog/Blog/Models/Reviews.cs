using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Reviews
    {
        public int ID { get; set; }

        public int NewsID { get; set; }

        public string ReviewContent { get; set; }

        public string ReviewName { get; set; }

        public string Email { get; set; }

        public int replyID { get; set; }
    }
}