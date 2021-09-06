using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class DisplayFreelancerViewModel
    {
       public DbResultViewModel vm { get; set; }

       public List<FreelancerReview> reviews { get; set; } = new List<FreelancerReview>();

    }
}
