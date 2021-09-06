using BackToWork.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public enum Region {Odesa, Kiyv, Lviv, Dnepr,Kharkiv}

    public class FreelancerModel:IFreelancerModel
    {
        [Required]
        public string login_unique { get; set; }
        [EmailAddress]
        public string email_unique { get; set; }
        [Required]
        public string name { get; set; }

        public bool activated { get; set; }

        public int age { get; set; }

        public Region region { get; set; }
    }
}
