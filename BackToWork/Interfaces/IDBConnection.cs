using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models.Interfaces
{
    public interface IDBConnection
    {
        Task<bool> OpenAsync();

        bool ExecuteAsync(OperationType op, IDataSourceContext context);
        
        bool ExecuteQuery(string query, IDataSourceContext context);


        bool Open();
    }
}
