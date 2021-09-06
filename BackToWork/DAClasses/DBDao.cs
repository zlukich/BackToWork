using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using BackToWork.Models.Interfaces;
using BackToWork.DAClasses;
using BackToWork.Interfaces;

namespace BackToWork.Models
{
    public enum OperationType { Select,Insert,Update,Delete,CondSelect}

    public class DBDao:IDBDao
    {
        public IDBConnection Connection { get; set; }
        public bool is_opened { get; set; } = false;

        public DbResultViewModel Reports
        {
            get
            {
                DAContext cont = new DAContext();
                Select("order_reports", new string[0], cont);
                return cont.Result;
            }

        }

        public DbResultViewModel OrdererSet 
        {
            get 
            {
                DAContext cont = new DAContext();
                Select("orderers", new string[0], cont);
                return cont.Result; 
            }
         
        }
        public DbResultViewModel Users
        {
            get
            {
                DAContext cont = new DAContext();
                Select("my_user", new string[0], cont);
                return cont.Result;
            }
            
        }
        public DbResultViewModel FreelancerSet
        {
            get
            {
                DAContext cont = new DAContext();
                Select("freelancers", new string[0], cont);
                return cont.Result;
            }
           
        }
        public DbResultViewModel OrdersSet
        {
            get
            {
                DAContext cont = new DAContext();
                Select("Orders_full", new string[0], cont);
                return cont.Result;
            }

        }
        /*
         Todo:
            Properties of DBDao as Models.
                For example:
                After selecting of DataBase Orderers we should save it as a List<OrdererModel>.
                After Acces of this List we should do select.
            Make it for every model, that represents table in our Database.
            
            Learn how to implement the basic links for the future profiles for every user in our system.

            ***Extra***
            |Create simple Thread
            |Authorization system
            |Create different Schemes for different types of the users
            |Asynchronous Methods for DBAcces
            |AJAX for paging
            |Add in DB columns: Profile Photo(Orderer, Freelancer),
            |
            |
         */


        public DBDao(IDBConnection connection)
        {
            Connection = connection;
            
        }

        public async Task<bool> OpenAsync()
        {
            try 
            {
                await Connection.OpenAsync();
                is_opened = true;
                return true;
            }
            catch 
            {
                is_opened = false;
                return false;
            }
        }

        public bool Open()
        {
            if (Connection.Open())
            {
                is_opened = true;
                return true;
            }
            
            return false;
        }
        public bool ExecuteQuery(string query, IDataSourceContext context)
        {
            try
            {

                if (is_opened)
                    Connection.ExecuteQuery(query, context);
                else
                {
                    this.Open();
                    is_opened = true;
                    Connection.ExecuteQuery(query, context);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Select(string name_of_table, string[] column_names, IDataSourceContext context)
        {
            try
            {
                context.name_of_table = name_of_table;
                context.column_names = column_names;

                if (is_opened)
                    Connection.ExecuteAsync(OperationType.Select, context);
                else
                {
                    this.Open();
                    is_opened = true;
                    Connection.ExecuteAsync(OperationType.Select, context);
                }
            }
            catch
            {
            }
        }
        public bool DeleteAsync(string name_of_table, string[] column_names, IDataSourceContext context)
        {
            bool x = false;
            try
            {
                context.name_of_table = name_of_table;
                context.column_names = column_names;

                if (is_opened)
                { 
                    x = Connection.ExecuteAsync(OperationType.Delete, context); 
                }
                else
                {
                    this.Open();
                    is_opened = true;
                    x = Connection.ExecuteAsync(OperationType.Delete, context);
                    
                }
                return x;
            }
            catch
            {
                return x;
            }
        }
        public bool InsertAsync(string name_of_table, string[] column_names, IDataSourceContext context)
        {
            bool x = false;
            context.name_of_table = name_of_table;
            context.column_names = column_names;
            try
            {
                if(is_opened)
                    x = Connection.ExecuteAsync(OperationType.Insert,context);
                else
                {
                    this.Open();
                    x = Connection.ExecuteAsync(OperationType.Insert, context);
                }
                return x;
            }
            catch
            {
                return x;
            }
        }
        public bool UpdateAsync(string name_of_table, string[] column_names, IDataSourceContext context)
        {
            bool x = false;
            context.name_of_table = name_of_table;
            context.column_names = column_names;
            try
            {
                if (is_opened)
                    x = Connection.ExecuteAsync(OperationType.Update, context);
                else
                {
                    this.Open();
                    x = Connection.ExecuteAsync(OperationType.Update, context);
                }
                return x;
            }
            catch
            {
                return x;
            }
        }

        public bool SelectAsync(string name_of_table,string[] column_names, IDataSourceContext context)
        {
            bool x = false;
            try
            {
                context.name_of_table = name_of_table;
                context.column_names = column_names;

                if (is_opened)
                    x = Connection.ExecuteAsync(OperationType.Select, context);
                else
                {
                    this.Open();
                    x = Connection.ExecuteAsync(OperationType.Select, context);
                }
                return x;
                
            }
            catch
            {
                return x;
            }
        }


    }
}
