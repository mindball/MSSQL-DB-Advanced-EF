using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AdoNetExercise
{
    public class Configuration
    {
        public const string ConnectionString =
             @"Server=192.168.1.2;Database=MinionsDB;User=sa;Password=1234";

        public static int? GetIdFromMinionDB(SqlConnection connection, 
            string tableName, string criteria)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            int? id;

            using (command = new SqlCommand())
            {
               command.Connection = connection;

                command.CommandText =
                        $"SELECT Id FROM {tableName} WHERE Name = @criteria";
                //command.Parameters.AddWithValue("@fromTable", tableName);
                command.Parameters.AddWithValue("@criteria", criteria);
                //if (criteria != null)
                //{
                //    command.CommandText =
                //        $"SELECT Id FROM {tableName} WHERE Name = @criteria";
                //    //command.Parameters.AddWithValue("@fromTable", tableName);
                //    command.Parameters.AddWithValue("@criteria", criteria);

                //}
                //else
                //{
                //    command.CommandText =
                //        $"SELECT Id FROM {tableName}";
                //    //command.Parameters.AddWithValue("@fromTable", tableName);
                //}

                id = (int?)command.ExecuteScalar();
               
            }
            return id;
        }

        public static void InsertInToRelatedTableMinionAndVillain(
            SqlConnection connection, int? minionId, int? villainId)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            using (command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText =
                    $"Insert INTO MinionsVillains (MinionId, VillainId) Values (@minionId, @villainId)";  
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT Name FROM Minions WHERE @minionId = Id";
                string minion = (string)command.ExecuteScalar();

                command.CommandText = "SELECT Name FROM Villains WHERE @villainId = Id";
                string villain = (string)command.ExecuteScalar();

                Console.WriteLine(
                    $"Successfully added {minion} to be minion of {villain}.");
            }
        }

        public static string GetNameFromMinionDB(SqlConnection connection,
            string tableName, string criteria)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            string name;

           

            using (command = new SqlCommand())
            {
                command.Connection = connection;

                int parseStringToId;
                if (int.TryParse(criteria, out parseStringToId))
                {
                    command.CommandText =
                        $"SELECT Name FROM {tableName} WHERE Id = @criteria";
                    command.Parameters.AddWithValue("@criteria", parseStringToId);
                }
                else
                {
                    command.CommandText =
                        $"SELECT Name FROM {tableName} WHERE Name = @criteria";
                    command.Parameters.AddWithValue("@criteria", criteria);
                }
                    
                name = (string)command.ExecuteScalar();
            }

            return name;
        }
    }
}
