using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class AddCatalogViewModel
    {
        public List<PLModel> plList { get; set; } = new List<PLModel>();
        public List<TechModel> techList { get; set; } = new List<TechModel>();

        public string type { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "Нужно ввести описание")]
        public string desc { get; set; }
    }
}
