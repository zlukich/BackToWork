using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class AddOrderViewModel
    {

        public List<PLModel> plList { get; set; } = new List<PLModel>();
        public List<TechModel> techList { get; set; } = new List<TechModel>();

        public List<int> selectedPl { get; set; } = new List<int>();

        public List<int> selectedTech { get; set; } = new List<int>();

        public int id { get; set; }

        public int id_project { get; set; }

        public List<object[]> projects { get; set; }

        [Required(ErrorMessage = "Нужно назвать проект")]
        public string projectDescription { get; set; }

        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Нужно указать количество дней")]
        public int Days { get; set; }

        public string login_orderer { get; set; }

        public bool is_completed { get; set; }

        [Required(ErrorMessage = "Нужно назвать специфику работы")]
        public string workDescription { get; set; }

        [Required(ErrorMessage = "Нужно указать хотя бы приблизительную стоимость")]
        public int Payment { get; set; }

        public string Link { get; set; }
    }
}
