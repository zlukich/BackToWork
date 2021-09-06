using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class CatalogViewModel
    {
        public List<PLModel> plCatalog { get; set; } = new List<PLModel>();
        public List<TechModel> techCatalog { get; set; } = new List<TechModel>();

        public List<int> selectedPl { get; set; } = new List<int>();
        public List<int> selectedTech { get; set; } = new List<int>();

    }
}
