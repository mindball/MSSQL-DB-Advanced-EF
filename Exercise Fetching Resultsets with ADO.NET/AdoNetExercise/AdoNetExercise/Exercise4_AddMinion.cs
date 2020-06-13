using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AdoNetExercise
{
    public static class Exercise4_AddMinion
    {
        public static void Addminion()
        {

            string minionInput = Console.ReadLine();
            string[] vilionInput = Console.ReadLine().Split();

            string[] minioinArgs = minionInput.Split();
            string minionName = minioinArgs[1];
            int age = int.Parse(minioinArgs[2]);
            string townName = minioinArgs[3];

            string vilianName = vilionInput[1];

            SqlConnection connection;
            using (connection =
                new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                SqlCommand command;
                

                using (command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText =
                    $"SELECT Id FROM Towns WHERE Name = @townName";

                    string fromTable = "Towns";
                    string criteria = townName;

                    int? id = GetIdFromDBMinionsByCriteria(fromTable, criteria);

                    command.Parameters.AddWithValue("@townName", townName);
                    int? getTownID = (int?)command.ExecuteScalar();

                    if (getTownID is null)
                    {
                        AddTown(townName, connection);
                    }

                    getTownID = (int?)command.ExecuteScalar();

                }
            }
        }

        private static int? GetIdFromDBMinionsByCriteria(string fromTable, 
            string criteria, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            using (command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    $"SELECT Id FROM fromTable WHERE Name = criteria";
            }

        private static void AddTown(string townName, SqlConnection connection)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            using (command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    $"Insert INTO Towns (Name) Values (@townName)";
                command.Parameters.AddWithValue("@townName", townName);

                command.ExecuteNonQuery();
            }
        }
    }
}
