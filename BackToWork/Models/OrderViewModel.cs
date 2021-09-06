using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class OrderViewModel
    {
        public List<PLModel> plCatalog { get; set; } = new List<PLModel>();
        public List<TechModel> techCatalog { get; set; } = new List<TechModel>();

        public string OrderDescription { get; set; }

        public DateTime Time { get; set; }

        public int Days { get; set; }

        public int Payment { get; set; }

        public string ProjectDescription { get; set; }

        public int projectId { get; set; }

        public int orderId { get; set; }

        public string login_freelancer { get; set; }

        public string login_orderer { get; set; }

        public string fileLink { get; set; }

        public bool isCompleted { get; set; }

        public List<OrderComment> comments {get;set;}

        public List<PLModel> plList { get; set; } = new List<PLModel>();
        public List<TechModel> techList { get; set; } = new List<TechModel>();
    }
}
