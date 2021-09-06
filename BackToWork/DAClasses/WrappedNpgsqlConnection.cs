using BackToWork.Models.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Models
{
    public class WrappedNpgsqlConnection:IDBConnection
    {
       
        public NpgsqlConnection _connection { get; set; }
        public bool is_opened { get; set; } = false;

        public WrappedNpgsqlConnection(string connection)
        {
            _connection = new NpgsqlConnection(connection);
        }

        public async Task<bool> OpenAsync()
        {
            try 
            {
                await _connection.CloseAsync(); 
                await _connection.OpenAsync();
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
            try
            {
                _connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ExecuteQuery(string query,IDataSourceContext context)
        {
            try
            {
                List<object[]> list = new List<object[]>();
                list.Add(new object[1]);
                using (var cmd = new NpgsqlCommand($"{query}", _connection))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        for(int i = 0; i < 1; i++)
                        {
                            list[0][0] = reader.GetValue(i);
                        }
                    }
                context.Result = new DbResultViewModel(new string[0], list);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ExecuteAsync(OperationType ot,IDataSourceContext context)
        {
            try
            {
               // _connection.OpenAsync();
                switch (ot)
                {
                    case OperationType.Select:
                        {
                            List<object[]> list = new List<object[]>();
                            
                            int len = context.column_names.Length;
                            if((context.column_names.Length == 0))
                            {
                                using (var cmd = new NpgsqlCommand(@$"SELECT Count(*) FROM INFORMATION_SCHEMA.COLUMNS 
                                                                        WHERE TABLE_NAME = '{context.name_of_table}'", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        len = reader.GetInt32(0);
                                    }
                                context.column_names = new string[len];
                                int i = 0;
                                using (var cmd = new NpgsqlCommand(@$"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                                                                        WHERE TABLE_NAME = '{context.name_of_table}'", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        context.column_names[i++] = reader.GetString(0);
                                        
                                    }
                            }
                            if (len == 0)
                            {
                                using (var cmd = new NpgsqlCommand(@$"SELECT Count(*) FROM   pg_attribute WHERE  attrelid = '{context.name_of_table}'::regclass", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        len = reader.GetInt32(0);
                                    }
                                context.column_names = new string[len];
                                int i = 0;
                                using (var cmd = new NpgsqlCommand(@$"SELECT attname as column_name FROM pg_attribute WHERE attrelid = '{context.name_of_table}'::regclass order by attname", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        context.column_names[i++] = reader.GetString(0);

                                    }
                            }
                            string query_destination = String.Join(',', context.column_names);
                            if(query_destination == "")
                            {
                                query_destination = "*";
                            }
                            
                            using (var cmd = new NpgsqlCommand($"SELECT {query_destination} FROM {context.name_of_table} {context.condition}", _connection))
                            using (var reader = cmd.ExecuteReader())
                                while (reader.Read())
                                {
                                    list.Add(new object[context.column_names.Length]);
                                    for(int i = 0;i<context.column_names.Length;i++)
                                        list[list.Count-1][i] = (reader.GetValue(i));
                                }
                            context.Result = new DbResultViewModel(context.column_names,list);
                            break;
                        }
                    case OperationType.Insert:
                        {
                            List<object[]> list = new List<object[]>();
                            int len = context.column_names.Length;
                            if ((context.column_names.Length == 0))
                            {
                                using (var cmd = new NpgsqlCommand(@$"SELECT Count(*) FROM INFORMATION_SCHEMA.COLUMNS 
                                                                        WHERE TABLE_NAME = '{context.name_of_table}'", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        len = reader.GetInt32(0);
                                    }
                                context.column_names = new string[len];
                                int i = 0;
                                using (var cmd = new NpgsqlCommand(@$"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                                                                        WHERE TABLE_NAME = '{context.name_of_table}'", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        context.column_names[i++] = reader.GetString(0);

                                    }
                            }
                            string query_destination = "";//String.Join(',', context.column_names);
                            string val = "";
                            for(int i = 0; i < context.column_names.Length; i++)
                            {
                                if (context.content[i] == null) continue;
                                if (i != 0)
                                {
                                    query_destination += $",{context.column_names[i]}";
                                    val += $",@{i}";
                                }
                                else
                                {
                                    query_destination += $"{context.column_names[i]}";
                                    val += $"@{i}";
                                }
                            }
                            using (var cmd = new NpgsqlCommand($"INSERT INTO {context.name_of_table}({query_destination}) VALUES ({val})", _connection))
                            {
                                for (int i = 0; i < context.column_names.Length; i++)
                                {
                                    if (context.content[i] == null) continue;
                                    cmd.Parameters.AddWithValue($"@{i}", context.content[i]);
                                    
                                }
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        }
                    case OperationType.CondSelect:
                        {

                            break;
                        }
                    case OperationType.Update:
                        {
                            List<object[]> list = new List<object[]>();
                            int len = context.column_names.Length;
                            if ((context.column_names.Length == 0))
                            {
                                using (var cmd = new NpgsqlCommand(@$"SELECT Count(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{context.name_of_table}'", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        len = reader.GetInt32(0);
                                    }
                                context.column_names = new string[len];
                                int i = 0;
                                using (var cmd = new NpgsqlCommand(@$"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{context.name_of_table}'", _connection))
                                using (var reader = cmd.ExecuteReader())
                                    while (reader.Read())
                                    {
                                        context.column_names[i++] = reader.GetString(0);

                                    }
                            }
                            string query_destination = String.Join(',', context.column_names);
                            string val = "";
                            for (int i = 0; i < context.column_names.Length; i++)
                            {
                                if (i != context.column_names.Length - 1)
                                {

                                    val += $"{context.column_names[i]}=@{i},";
                                }
                                else
                                {
                                    val += $"{context.column_names[i]}=@{i}";
                                }
                            }
                            using (var cmd = new NpgsqlCommand($"UPDATE {context.name_of_table} SET {val} {context.condition}", _connection))
                            {
                                for (int i = 0; i < context.column_names.Length; i++)
                                {
                                    
                                    cmd.Parameters.AddWithValue($"@{i}", context.content[i]);

                                }
                                cmd.ExecuteNonQuery();
                            }

                            break;
                        }
                        case OperationType.Delete:
                        {
                            using (var cmd = new NpgsqlCommand($"DELETE FROM {context.name_of_table} {context.condition}", _connection))
                            {
                                
                                cmd.ExecuteNonQuery();
                            }

                            break;
                        }

                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
