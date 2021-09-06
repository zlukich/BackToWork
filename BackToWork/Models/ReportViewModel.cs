using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class ReportViewModel
    {
        public string login_freelancer { get; set; }

        public string login_orderer { get; set; }

        public int id_orders { get; set; }

        [Required(ErrorMessage = "Нужно ввести жалобу")]
        public string comment { get; set; }

        public string answer { get; set; }

        public string id_report { get; set; }
        public string order_desc { get; set; }
    }
}
