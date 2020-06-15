using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AdoNetExercise
{
    public class Configuration
    {
        public const string ConnectionString =
             @"Server=10.148.73.5;Database=MinionsDB;User=sa;Password=Q1w2e3r4";

        public static int? GetIdFromMinionDB(SqlConnection connection, 
            string tableName, string criteria)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            int? id;

            using (command = new SqlCommand())
            {
               command.Connection = connection;

                if (criteria != null)
                {
                    command.CommandText =
                        $"SELECT Id FROM {tableName} WHERE Name = @criteria";
                    //command.Parameters.AddWithValue("@fromTable", tableName);
                    command.Parameters.AddWithValue("@criteria", criteria);
                }
                else
                {
                    command.CommandText =
                        $"SELECT Id FROM {tableName}";
                    //command.Parameters.AddWithValue("@fromTable", tableName);
                }

                id = (int?)command.ExecuteScalar();
                return id;
            }

        }
    }
}
