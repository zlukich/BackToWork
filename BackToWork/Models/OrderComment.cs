using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class OrderComment
    {
        public int id_comment { get; set; }
        public string comment_msg { get; set; }
        public string link_file { get; set; }
        public DateTime comment_date { get; set; }
        public DateTime comment_time { get; set; }
        public int id_orders { get; set; }
        public string login_freelancer { get; set; }
    }
}
