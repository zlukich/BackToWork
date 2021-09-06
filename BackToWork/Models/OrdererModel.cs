using BackToWork.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class OrdererModel:IOrdererModel
    {
        [Required]
        [Key]
        public string login_orderer { get; set; }
        [Required]
        [EmailAddress]
        public string email_orderer { get; set; }

        public bool activated { get; set; }

        public string name_orderer { get; set; }
        [Required]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string mobile_orderer { get; set; }

        public string company_description { get; set; }
        
    }
}
