using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackToWork.Models.Interfaces;
namespace BackToWork.Models
{
    public interface IDBDao
    {
        IDBConnection Connection { get; set; }

        DbResultViewModel OrdererSet { get; }
        DbResultViewModel OrdersSet { get; }
        DbResultViewModel FreelancerSet { get; }
        DbResultViewModel Users { get; }
        DbResultViewModel Reports { get; }

        bool InsertAsync(string name_of_table, string[] column_names, IDataSourceContext context);

        bool UpdateAsync(string name_of_table, string[] column_names, IDataSourceContext context);

        bool SelectAsync(string name_of_table, string[] column_names, IDataSourceContext context);

        bool DeleteAsync(string name_of_table, string[] column_names, IDataSourceContext context);

        bool ExecuteQuery(string query, IDataSourceContext context);

        Task<bool> OpenAsync();

        bool Open();

        void Select(string name_of_table, string[] column_names, IDataSourceContext context);

    }
}
