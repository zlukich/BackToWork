using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public interface IDataSourceContext
    {


        public string name_of_table { get; set; }

        public string[] column_names { get; set; }

        public object[] content { get; set; }

        public string condition { get; set; }

        public DbResultViewModel Result { get; set; }

    }
}
