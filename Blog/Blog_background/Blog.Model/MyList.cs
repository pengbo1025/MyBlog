using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class MyList
    {

        public int ID { get; set; }

        public int CategoryID { get; set; }

        public string Title { get; set; }

        public DateTime Release_time { get; set; }

        public string Release_people { get; set; }

        public int Click { get; set; }

        public bool IsHost { get; set; }

        public bool State { get; set; }

        public int NewsID { get; set; }

        public string N_Content { get; set; }

        public bool attribute { get; set; }

        public int weight { get; set; }

        public bool Headings { get; set; }

    }
}
