using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Interfaces
{
    interface IDBResultViewModel
    {
        public string[] column_names { get; set; }
        public List<object[]> exemplars { get; set; }
    }
}
