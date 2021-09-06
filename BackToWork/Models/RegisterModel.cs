using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Login { get; set; }

        [RegularExpression(@"[a-z0-9._%-]+@[a-z0-9._%-]+\.[a-z]{2,}",
         ErrorMessage = "Введите действительный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "не выбрана роль")]
        public string Role { get; set; }
    }
}
