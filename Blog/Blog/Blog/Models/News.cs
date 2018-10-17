using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class News
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }

        public string Title { get; set; }

        public DateTime Release_time { get; set; }

        public string Release_people { get; set; }

        public int Click { get; set; }

        public bool IsHost { get; set; }

        public bool State { get; set; }
    }
}