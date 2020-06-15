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

                    //Looking for Towns
                    int? getTownID = Configuration
                        .GetIdFromMinionDB(connection, "Towns", townName);

                    if (getTownID is null)
                    {
                        getTownID = AddTown(townName, connection);
                    }

                    //Lookinh for Minion
                    int? getMinionID = Configuration
                        .GetIdFromMinionDB(connection, "Minions", minionName);

                    if (getMinionID is null)
                    {
                        AddMinion(connection, minionName, age, getTownID);
                    }

                    //Lookinh for Villain
                    int? getVillainId = Configuration
                        .GetIdFromMinionDB(connection, "Minions", vilianName);

                    if (getVillainId is null)
                    {
                        //default evilness value = evil with id 4
                        AddVillain(connection, vilianName, 4);
                    }
                    ;
                }
            }
        }

        private static void AddVillain(SqlConnection connection, 
            string vilianName, int evilnessFactorId)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            using (command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText =
                    $"Insert INTO Villains (Name, EvilnessFactorId) Values (@name, @evilnessFactorId)";
                command.Parameters.AddWithValue("@name", vilianName);
                command.Parameters.AddWithValue("@evilnessFactorId", evilnessFactorId);                

                command.ExecuteNonQuery();

                Console.WriteLine($"Villain {vilianName} was added to the database."); 
            }
        }

        private static void AddMinion(SqlConnection connection,
            string minionName, int Age, int? townId)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            using (command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText =
                    $"Insert INTO Minions (Name, Age, TownId) Values (@name, @age, @townId)";
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", Age);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }
        }

        private static int AddTown(string townName, SqlConnection connection)
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
                Console.WriteLine($"Town {townName} was added to the database.");
            }

            return (int)Configuration.GetIdFromMinionDB(connection, "Towns", townName);
        }
    }
}
