using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class EditFreelancerViewModel
    {
        public string Login { get; set; }
        [RegularExpression(@"[a-z0-9._%-]+@[a-z0-9._%-]+\.[a-z]{2,}",
         ErrorMessage = "Введите действительный email")]
        public string Email { get; set; }
        [RegularExpression(@"\w* \w*",
         ErrorMessage = "Имя должно состоять из 2 слов")]
        public string Name { get; set; }
        public bool Activated { get; set; }
        [Range(0, 100, ErrorMessage = "Вам может быть от 0 до 100")]
        public int Age { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
    }
}
