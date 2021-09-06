using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class FreelancerReview
    {
        public int id_review { get; set; }

        public int mark { get; set; }

        public string review_commet { get; set; }

        public string login_freelancer { get; set; }

        public string login_orderer { get; set; }

        public DateTime time { get; set; }
    }
}
