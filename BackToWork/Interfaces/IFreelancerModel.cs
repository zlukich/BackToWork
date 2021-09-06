using BackToWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Interfaces
{
    public interface IFreelancerModel
    {
         string login_unique { get; set; }
         string email_unique { get; set; }
         string name { get; set; }

         bool activated { get; set; }

         int age { get; set; }

         Region region { get; set; }
    }
}
