using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class EditOrdererViewModel
    {
        public string Login { get; set; }
        [RegularExpression(@"[a-z0-9._%-]+@[a-z0-9._%-]+\.[a-z]{2,}",
         ErrorMessage = "Введите действительный email")]
        public string Email { get; set; }
        [RegularExpression(@"\w* \w*",
         ErrorMessage = "Имя должно состоять минимум из 2 слов")]
        public string Name { get; set; }
        public bool Activated { get; set; }
        [RegularExpression(@"(\+38)?[0-9]{10}",
         ErrorMessage = "Введите действительный телефон")]
        public string Number { get; set; }
        public string Description { get; set; }


    }
}
