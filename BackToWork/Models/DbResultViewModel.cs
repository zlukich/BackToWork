using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackToWork.Interfaces;
namespace BackToWork.Models
{
    public class DbResultViewModel:IDBResultViewModel
    {
        public string[] column_names { get; set; }

        public List<object[]> exemplars { get; set; } = null;

        //public DbResultViewModel Email { 
        //    get 
        //    {
        //        int i = 0;
        //        for ( i= 0; i < column_names.Length; i++)
        //        {
        //            if (column_names[i] == "email_orderer") break;
        //        }
        //        List<object[]> email_list = new List<object[]>();
        //        foreach (object[] expl in exemplars)
        //            email_list.Add(new object[] { expl[i]});
        //        return new DbResultViewModel(new string[] {column_names[i]},email_list);
        //    }
        //    set { }
        //}

        public DbResultViewModel(string[] col, List<object[]> l)
        {
            column_names = col;
            exemplars = l;
        }
    }
}
