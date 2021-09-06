using BackToWork.Interfaces;
using BackToWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.DAClasses
{
    public class DAContext: IDataSourceContext
    {
        public string name_of_table { get; set; }

        public string[] column_names { get; set; }

        public object[] content { get; set; }

        public string condition { get; set; }

        public DbResultViewModel Result { get; set; }

        public DAContext()
        {
            
        }

    }
}
