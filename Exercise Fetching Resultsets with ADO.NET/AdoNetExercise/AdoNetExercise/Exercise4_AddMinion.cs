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
                    Towns town = GetTown(connection, townName); 

                    //Looking for Minion
                    Minions minion = GetMinion(connection, minionName, age, town);

                    //Looking for Villain
                    Villains villain = GetVillain(connection, vilianName);                  


                    Configuration.InsertInToRelatedTableMinionAndVillain(
                        connection, minion.Id, villain.Id);
                }
            }
        }

        private static Villains GetVillain(SqlConnection connection, string villainName)
        {
            Villains villain = new Villains();

            string getVillain =
                Configuration.GetNameFromMinionDB(connection, "Villains", villainName);
            if(getVillain is null)
            {
                //default evilness value = evil with id 4
                AddVillain(connection, villainName, 4);
            }

            int? villainId =
                Configuration.GetIdFromMinionDB(connection, "Villains", villainName);

            SqlCommand command = new SqlCommand();
            using (command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Villains WHERE Id = @villainId";
                command.Parameters.AddWithValue("@villainId", villainId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        villain.Id = (int)reader[0];
                        villain.Name = (string)reader[1];
                        villain.EvilnessFactorId = Convert.IsDBNull(reader[2]) ? null : (int?)reader[2];                       
                    }
                }
            }

            return villain;
        }

        private static Minions GetMinion(SqlConnection connection, 
            string minionName, int age, Towns town)
        {
            string getMinionName = Configuration
                        .GetNameFromMinionDB(connection, "Minions", minionName);

            if (getMinionName is null)
            {
                AddMinion(connection, minionName, age, town);
            }

            int? minionId = Configuration.GetIdFromMinionDB(connection, "Minions", minionName);
            Minions minion = new Minions();

            SqlCommand command = new SqlCommand();
            using (command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Minions WHERE Id = @minionId";
                command.Parameters.AddWithValue("@minionId", minionId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        minion.Id = (int)reader[0];
                        minion.Name = (string)reader[1];
                        minion.Age = Convert.IsDBNull(reader[2]) ? null : (int?)reader[2];
                        minion.TownId = Convert.IsDBNull(reader[3]) ? null : (int?)reader[3];
                    }
                }
            }

            return minion;
        }

        private static Towns GetTown(SqlConnection connection, string townName)
        {
            string getTownName = Configuration.GetNameFromMinionDB(connection, "Towns", townName);

            if (getTownName is null)
            {
                AddTown(townName, connection);
            }

            Towns town = new Towns();
            
            SqlCommand command = new SqlCommand();
            using (command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Towns WHERE Name = @name";
                command.Parameters.AddWithValue("@name", townName);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        town.Id = (int)reader[0];
                        town.Name = (string)reader[1];
                        town.CountryCode = Convert.IsDBNull(reader[2]) ? null : (int?)reader[2];
                    }
                }
            }

            return town;
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
            string minionName, int Age, Towns town)
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
                command.Parameters.AddWithValue("@townId", town.Id);

                command.ExecuteNonQuery();
            }
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
                Console.WriteLine($"Town {townName} was added to the database.");
            }
        }
    }
}
